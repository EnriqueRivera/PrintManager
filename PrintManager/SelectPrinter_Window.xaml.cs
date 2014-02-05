using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrintManager
{
	/// <summary>
	/// Window for selecting a printer
	/// </summary>
	public partial class SelectPrinter_Window : Window
    {
        #region Private members

        //Almacena el servidor de impresión.
        private LocalPrintServer _printServer;

        //Almacena los datos de la conexión que se realizó en la primer ventana.
        private SqlConnection _sqlConn;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor de la ventana.
        /// </summary>
        /// <param name="sqlConn">Datos de la conexión a la base de datos</param>
        public SelectPrinter_Window(SqlConnection sqlConn)
        {
            this.InitializeComponent();

            //Centramos la ventana.
            WindowFormat.CenterWindowOnScreen(this);

            //Asignamos el valor de la conexión a nuestra variable de instancia.
            _sqlConn = sqlConn;

            //Creamos el servidor de impresión con privilegios de administrador.
            _printServer = new LocalPrintServer(PrintSystemDesiredAccess.AdministrateServer);

            //Rellenamos el ComboBox con las impresoras actuales.
            FillWithPrinters();
        }

        #endregion

        #region Events

        private void btnSelect_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            SelectPrinter();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.S))   // Ctrl + S
            {
                SelectPrinter();
            }
        }

        #endregion

        #region Functionality

        /// <summary>
        /// Método para llenar el ComboBox con las impresoras.
        /// </summary>
        private void FillWithPrinters()
        {
            PrintQueue printQueue;

            for (int i = 0; i < _printServer.GetPrintQueues().Count(); i++)
            {
                printQueue = _printServer.GetPrintQueues().ElementAt(i);
                cboPrinters.Items.Add(printQueue.FullName);

                //Seleccionamos la impresora que esté por defualt.
                if (_printServer.DefaultPrintQueue.FullName.Equals(printQueue.FullName))
                {
                    cboPrinters.SelectedIndex = i;
                }
            }

        }

        /// <summary>
        /// Método que se utiliza para cuando se selecciona una impresora.
        /// </summary>
        private void SelectPrinter()
        {
            if (cboPrinters.SelectedIndex == -1)
            {
                MessageBox.Show("Select a printer.", "Printer selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                //Abrimos la ventana donde se administra la cola de impresión.
                MainWindow mainWnd = new MainWindow(_printServer, cboPrinters.SelectedValue.ToString(), _sqlConn);
                mainWnd.Show();

                this.Close();
            }
        }

        #endregion
    }
}