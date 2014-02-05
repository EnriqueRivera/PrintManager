using Microsoft.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrintManager
{
    /// <summary>
    /// Ventana donde se administra la cola de impresión.
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private members

        //Encapsula el trabajo que se está imprimiendo, así como también una bandera
        //que indica que se está imprimiendo un trabajo.
        private PrintJobWrapper _printJobWrapper;

        //Almacena los datos de la conexión que se realizó en la primer ventana.
        private SqlConnection _sqlConn;

        //Almacena el servidor de impresión.
        private LocalPrintServer _currentPrintServer;

        //Almacena la impresora seleccionada en la segunda ventana.
        private PrintQueue _currentPrintQueue;

        //Instancia del segundo hilo.
        private Thread _secondThread;

        //Guardamos el número de trabajos que están en la cola de impresión.
        //Si hay algún cambio en el numero de trabajos, entonces actualizamos
        //la cola de impresión.
        private int _numOfJobs = 0;

        //Bandera que nos indica si el usuario decidió cambiar la impresora.
        private bool _changePrinter = false;

        //Bandera que nos indica si se está cargando un archivo.
        //Se usará para saber si actualizamos la cola de impresión,
        //cuando haya llegado completamente un trabajo.
        private bool _jobSpooling = false;

        //Almacenamos el color que va a tener la barra de estado cuando
        //esté lista la impresora para imprimir.
        private SolidColorBrush _greenColor = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));

        //Almacenamos el color que va a tener la barra de estado cuando
        //la impresora esté imprimiendo un trabajo.
        private SolidColorBrush _redColor = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));

        #endregion

        #region Delegates

        /// <summary>
        /// Delegados usuados para llamar a métodos que ejecutarán
        /// cambios en variables y componentes del hilo principal.
        /// Ya que a través de un hilo secundario no puedes modificar
        /// elementos del hilo principal.
        /// </summary>
        
        private delegate void PrintJobFinishedCallback();
        private delegate void ResumePrinterAndPrintJobCallback();
        private delegate void UpdateNewJobDetectedCallback();
        private delegate void PauseAllJobsCallback();
        private delegate void ShowPrintingStatusCallback();
        private delegate void PausePrintQueueCallback();
        private delegate void UpdateJobSpoolingCallback();
        private delegate void RefreshPrintQueueCallback();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor de la ventana principal.
        /// </summary>
        /// <param name="printServer">Servidor acutal de impresión</param>
        /// <param name="printQueueSelected">Impresora seleccionada</param>
        /// <param name="sqlConn">Datos de la conexión a la base de datos</param>
        public MainWindow(LocalPrintServer printServer, string printQueueSelected, SqlConnection sqlConn)
        {
            InitializeComponent();

            //Centramos la ventana.
            WindowFormat.CenterWindowOnScreen(this);

            //Almacenamos los parametros recibidos en variables de instancia.
            _sqlConn = sqlConn;
            _currentPrintServer = printServer;

            //Creamos la cola de impresión, donde le indicamos el servidor de impresión,
            //la impresora seleccionada y los privilegios.
            _currentPrintQueue = new PrintQueue(printServer, printQueueSelected, PrintSystemDesiredAccess.AdministratePrinter);

            //Colocamos en el label el nombre de la impresora seleccionada.
			lblSelectedPrinter.Content = printQueueSelected;

            //Creamos el objeto que nos indica si se está imprimiendo o no un trabajo.
            _printJobWrapper = new PrintJobWrapper(false, null);

            //Creamos e iniciamos el hilo secundario.
            _secondThread = new Thread(new ThreadStart(SecondaryThread));
            _secondThread.Start();
        }

        #endregion

        #region Events

        private void btnChangePrinter_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ChangePrinter();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Si se está imprimiendo un trabajo, le avisamos al usuario que debe esperar.
            if (_printJobWrapper.CurrentPrintingJob != null)
            {
                _changePrinter = false;
                e.Cancel = true;
                MessageBox.Show("Wait until the current job is printed.", "Print queue", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                //Si queremoscambiar la impresora o deseamos cerrar la ventana, entonces terminamos el hilo.
                if (_changePrinter || (MessageBox.Show("Are you sure you want to exit?", "Print queue", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes))
                {
                    _secondThread.Abort();
                }
                else
                {
                    //Sino, solo cancelamos la operación del cierre de la ventana.
                    e.Cancel = true;
                }
            }
        }

        private void dgdPrintQueue_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PrintJob();
        }

        private void btnPrintJob_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PrintJob();
        }

        private void btnDeleteJob_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DeleteJob();
        }

        private void btnRefresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateGrid();
        }

        private void btnManageSheets_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ManageSheets();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeyCombinations(e);
        }

        #endregion

        #region Threads

        /// <summary>
        /// Hilo que controla los trabajos de impresión.
        /// </summary>
        private void SecondaryThread()
        {
            while (true)
            {
                Thread.Sleep(100);

                //Refrescamos la cola de impresión.
                Dispatcher.Invoke(new RefreshPrintQueueCallback(RefreshPrintQueue), new object[] { });

                //Si se mandó a imprimir, quitamos la pausa a la impresora y al trabajo a imprimir.
                Dispatcher.Invoke(new ResumePrinterAndPrintJobCallback(ResumePrinterAndPrintJob), new object[] { });
                
                //Pausamos todos los trabajos, excepto el que se está imprimiendo.
                Dispatcher.Invoke(new PauseAllJobsCallback(PauseAllJobs), new object[] { });

                //Pausamos la impresora seleccionada.
                Dispatcher.Invoke(new PausePrintQueueCallback(PausePrintQueue), new object[] { });

                //Si se está imprimiendo un trabajo ponemos la progress bar en rojo.
                Dispatcher.Invoke(new ShowPrintingStatusCallback(ShowPrintingStatus), new object[] { });

                //Ponemos en null el currentPrintJob si ya terminó de imprimirse.
                Dispatcher.Invoke(new PrintJobFinishedCallback(PrintJobFinished), new object[] { });

                //Actualizamos el grid si es necesario.
                Dispatcher.Invoke(new UpdateNewJobDetectedCallback(UpdateNewJobDetected), new object[] { });

                //Actualizamos si se ha terminado de cargar un archivo.
                Dispatcher.Invoke(new UpdateJobSpoolingCallback(UpdateJobSpooling), new object[] { });
            }
        }

        #endregion

        #region Functionality

        /// <summary>
        /// Actualizamos la cola de impresión.
        /// </summary>
        private void RefreshPrintQueue()
        {
            _currentPrintQueue.Refresh();
        }

        /// <summary>
        /// Método para cambiar impresora.
        /// </summary>
        private void ChangePrinter()
        {
            //Si se está imprimiendo algo, le informamos al usuario que debe esperar.
            if (_printJobWrapper.CurrentPrintingJob != null)
            {
                MessageBox.Show("Wait until the current job is printed.", "Change printer", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                //Mostramos la ventana donde se elige la impresora.
                SelectPrinter_Window selPrintWnd = new SelectPrinter_Window(_sqlConn);
                selPrintWnd.Show();

                //Cambiamos la bandera a true, para que en el evento Closing de la forma
                //sepamos que queremos cambiar sólo de ventana.
                _changePrinter = true;

                this.Close();
            }
        }

        /// <summary>
        /// Método para acutalizar el DataGrid
        /// </summary>
        private void UpdateGrid()
        {
            //Limpiamos todo lo que tiene el DataGrid
            dgdPrintQueue.Items.Clear();

            //Actualizamos la variable que almacena el número de trabajos
            //que están en la cola de impresión.
            _numOfJobs = _currentPrintQueue.GetPrintJobInfoCollection().Count();

            //Iteramos todos los trabajos en la cola de impresión y los vamos
            //agregando al DataGrid.
            foreach (var job in _currentPrintQueue.GetPrintJobInfoCollection())
            {
                dgdPrintQueue.Items.Add(
                    new Job
                    {
                        jobID = job.JobIdentifier,
                        documentName = job.Name,
                        owner = job.Submitter,
                        pages = job.NumberOfPages,
                        size = (job.JobSize / 1000000d),
                        status = job.JobStatus.ToString(),
                        submitted = job.TimeJobSubmitted.ToLocalTime()
                    }
                );
            }
        }

        /// <summary>
        /// Método que actualiza el DataGrid si se detectó que cambió el número de trabajos existentes.
        /// </summary>
        private void UpdateNewJobDetected()
        {
            //Si cambió el número de trabajos, entonces actualizaos el DataGrid
            if (_numOfJobs != _currentPrintQueue.GetPrintJobInfoCollection().Count())
            {
                UpdateGrid();
            }
        }

        /// <summary>
        /// Ponemos en pausa todos los trabajos que no estén pausados, excepto el que se está imprmiendo.
        /// </summary>
        private void PauseAllJobs()
        {
            //Iteramos todos los trabajos de impresión.
            foreach (var job in _currentPrintQueue.GetPrintJobInfoCollection())
            {
                //Si no hay un trabajo imprimiendose, entonces:
                if (_printJobWrapper.CurrentPrintingJob == null)
                {
                    //Si el trabajo no está en pausa, entonces:
                    if (!job.IsPaused)
                    {
                        //Pausamos el trabajo.
                        job.Pause();
                    }
                }
                //Si el ID del trabajo es igual al trabajo que se está imprimiendo, entonces:
                else if (_printJobWrapper.CurrentPrintingJob.JobIdentifier == job.JobIdentifier)
                {
                    //Si el trabajo no se está imprimiendo, entonces:
                    if (!job.IsPrinting)
                    {
                        //Imprimimos el trabajo.
                        job.Resume();
                    }
                }
                //Si el trabajo no está pausado, entonces:
                else if (!job.IsPaused)
                {
                    //Pausamos el trabajo.
                    job.Pause();
                }

                //Si detectamos un trabajo que está cargandose, 
                //entonces cambiamos la bandera (_jobSpooling) a true.
                _jobSpooling = job.IsSpooling ? true : _jobSpooling;
            }
        }

        /// <summary>
        /// Método que cambia la barra de estado de color, cuando es necesario.
        /// </summary>
        private void ShowPrintingStatus()
        {
            //Si no se está imprimiendo, entonces:
            if (_printJobWrapper.CurrentPrintingJob == null)
            {
                //Cambiamos el contenido del label que está a un lado de la 
                //progress bar.
                lblPrintStatus.Content = "Ready to print.";

                //Cambiamos a falsa la propiedad de indetermiando de la barra.
                prgJob.IsIndeterminate = false;

                //Pintamos la barra de color verde.
                prgJob.Foreground = _greenColor;
            }
            else
            {
                //Cambiamos el contenido del label que está a un lado de la 
                //progress bar. Le indicamos que trabajo se está imprimiendo.
                lblPrintStatus.Content = "Printing job " + _printJobWrapper.CurrentPrintingJob.JobIdentifier;

                //Cambiamos a falsa la propiedad de indetermiando de la barra.
                prgJob.IsIndeterminate = true;

                //Pintamos la barra de color verde.
                prgJob.Foreground = _redColor;
            }
        }

        /// <summary>
        /// Pausamos siempre la impresora acutal.
        /// </summary>
        private void PausePrintQueue()
        {
            try
            {
                if (!_currentPrintQueue.IsPaused)
                {
                    _currentPrintQueue.Pause();   
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Actualizamos el DataGrid si ya no hay trabajos cargandose.
        /// </summary>
        private void UpdateJobSpooling()
        {
            //Si al menos un trabajo se estaba cargando
            //y
            //ya no hay algún trabajo cargandose, entonces:
            if (_jobSpooling && IsZeroSpoolingCount())
            {
                //Actualizamos el DataGrid.
                UpdateGrid();

                //Cambiamos la bandera a false.
                _jobSpooling = false;
            }
        }

        /// <summary>
        /// Método que checa si ya no hay trabajos cargandose.
        /// </summary>
        /// <returns>true si no hay trabajos cargandose, false en caso contrario</returns>
        private bool IsZeroSpoolingCount()
        {
            return _currentPrintQueue.GetPrintJobInfoCollection().Count(job => job.IsSpooling) == 0;
        }

        /// <summary>
        /// Método que checa si se puede proceder a abrir la ventana donde se imprime el trabajo.
        /// </summary>
        private void PrintJob()
        {
            try
            {
                //Creamos un objeto del trabajoq ue se desea imprimir.
                PrintSystemJobInfo jobToPrint = _currentPrintQueue.GetJob(((Job)dgdPrintQueue.SelectedItem).jobID);

                //Si no se está imprimiendo un trabajo
                //y
                //el trabajo que se intenta imprimir ya se cargó completamente
                //entonces:
                if (!IsPrintingAJob() && !IsJobSpooling(jobToPrint))
                {
                    //Abrimos la siguiente ventana.
                    Print_Window pntWnd = new Print_Window(_printJobWrapper, jobToPrint, _sqlConn);
                    pntWnd.ShowDialog();
                }
            }
            catch (NullReferenceException)
            {
                //Esta excepción sucede si no se selecciona algún elemento en el DataGrid.
                MessageBox.Show("Select a print job.", "Print queue", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Método que checa si se está cargando o no el trabajo.
        /// </summary>
        /// <param name="job">Trabajo que checaremos, para comprobar que se está cargando</param>
        /// <returns>true si se está cargando aún el archivo, false en caso contrario</returns>
        private bool IsJobSpooling(PrintSystemJobInfo job)
        {
            if (job.IsSpooling)
            {
                MessageBox.Show("Wait until the file is completely loaded.", "Print queue", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Métotodo que checa si se está imprimiendo un trabajo.
        /// </summary>
        /// <returns>true si se está imprimiendo actualmente un trabajo, false en caso contrario</returns>
        private bool IsPrintingAJob()
        {
            if (_printJobWrapper.CurrentPrintingJob != null)
            {
                MessageBox.Show("Wait until the current job is printed.", "Print queue", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método para eliminar un trabajo de la cola de impresión.
        /// </summary>
        private void DeleteJob()
        {
            try
            {
                int jobID = ((Job)dgdPrintQueue.SelectedItem).jobID;

                if (MessageBox.Show("Are you sure you want to eliminate the job " + jobID + "?", "Print queue", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (_printJobWrapper.CurrentPrintingJob != null)
                    {
                        //Checamos que no quiera eliminar el trabajo que se está imprimiendo actualmente.
                        if (jobID != _printJobWrapper.CurrentPrintingJob.JobIdentifier)
                        {
                            _currentPrintQueue.GetJob(jobID).Cancel();
                        }
                        else
                        {
                            MessageBox.Show("You can't delete the current printing job.", "Print queue", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        _currentPrintQueue.GetJob(jobID).Cancel();
                    }
                }
            }
            catch (NullReferenceException)
            {
                //Esta excepción sucede si no se selecciona algún elemento en el DataGrid.
                MessageBox.Show("Select a print job.", "Print queue", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Método que abre la ventana para administrar hojas.
        /// </summary>
        private void ManageSheets()
        {
            ManageSheets_Window uiWnd = new ManageSheets_Window(_sqlConn);
            uiWnd.ShowDialog();
        }

        /// <summary>
        /// Método para saber si se presionó una combinación correcta de teclas.
        /// </summary>
        /// <param name="e">Encapsula propiedades de la tecla presionada</param>
        private void KeyCombinations(KeyEventArgs e)
        {
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.P)) // Ctrl + P
            {
                PrintJob();
            }
            else if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.D)) // Ctrl + D
            {
                DeleteJob();
            }
            else if (e.Key == Key.F5) // F5
            {
                UpdateGrid();
            }
            else if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.O)) // Ctrl + O
            {
                ChangePrinter();
            }
            else if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.M)) // Ctrl + M
            {
                ManageSheets();
            }
        }

        /// <summary>
        /// Método que checa si el trabajo que se estaba imprimiendo ha terminado.
        /// </summary>
        private void PrintJobFinished()
        {
            if ((_printJobWrapper.CurrentPrintingJob != null) && IsJobFinished())
            {
                //Ponemos nula la variable que almacena el trabajo que se está imprimiendo.
                _printJobWrapper.CurrentPrintingJob = null;
            }
        }

        /// <summary>
        /// Método que busca el trabajo que se está imprimiendo en la cola de impresión.
        /// </summary>
        /// <returns>true si el trabajo no se encuentra, false en caso contrario</returns>
        private bool IsJobFinished()
        {
            return _currentPrintQueue.GetPrintJobInfoCollection().Count(
                job => (job.JobIdentifier == _printJobWrapper.CurrentPrintingJob.JobIdentifier)) == 0;
        }

        /// <summary>
        /// Método que quita la pausa a la impresora y al trabajo que se desea imprimir.
        /// </summary>
        private void ResumePrinterAndPrintJob()
        {
            if (_printJobWrapper.PrintJob)
            {
                //Quitamos la pausa a la impresora.
                _currentPrintQueue.Resume();

                //Quitamos la pausa al trabajo que deseamos imprimir.
                _printJobWrapper.CurrentPrintingJob.Resume();

                //Actualizamos el DataGrid
                UpdateGrid();

                //Cambiamos a false la bandera.
                _printJobWrapper.PrintJob = false;
            }
        }
		
        #endregion
    }

}