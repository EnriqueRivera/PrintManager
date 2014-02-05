using System;
using System.Collections.Generic;
using System.Data;
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
	/// Ventana que administra el trabajo que se desea imprimir.
	/// </summary>
	public partial class Print_Window : Window
    {
        #region Private members

        //Encapsula el trabajo que se está imprimiendo, así como también una bandera
        //que indica que se está imprimiendo un trabajo.
        private PrintJobWrapper _printJobWrapper;

        //Trabajo que se desea imprimir.
        private PrintSystemJobInfo _jobToPrint;

        //Almacena los datos de la conexión que se realizó en la primer ventana.
        private SqlConnection _sqlConn;

        //Hojas que le quedarán al final al usuario.
        private int _resultSheets = 0;

        //Tamaño máximo para la cadena almacenada el campo "nombreArchivo" 
        //de la tabla "HistorialTrabajos"
        private const int _jobNameMaxLenght = 45;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor de la ventna.
        /// </summary>
        /// <param name="printWrapper">Encapsula si se imprime o no el trabajo</param>
        /// <param name="jobToPrint">Trabajo que se desea imprimir</param>
        /// <param name="sqlConn">Datos de la conexión a la base de datos</param>
        public Print_Window(PrintJobWrapper printWrapper, PrintSystemJobInfo jobToPrint, SqlConnection sqlConn)
        {
            this.InitializeComponent();

            WindowFormat.CenterWindowOnScreen(this);

            _jobToPrint = jobToPrint;
            _printJobWrapper = printWrapper;
            _sqlConn = sqlConn;

            FillJobInfoFields();

            FillUserInfoFields();
        }

        #endregion

        #region Events

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeyCombinations(e);
        }

        private void btnPrint_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PrintJob();
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Functionallity

        /// <summary>
        /// Método que coloca la cadena dada en la propiedad Content y Tooltip del label indicado.
        /// </summary>
        /// <param name="lbl">Label al que se le aplicará el cambio en sus propiedades</param>
        /// <param name="s">Cadena a aplicar en las propiedades del Label</param>
        private void SetContentAndTooltip(Label lbl, string s)
        {
            lbl.Content = s;
            lbl.ToolTip = s;
        }

        /// <summary>
        /// Método para llenar los campos de la información del trabajo que se desea imprimir.
        /// </summary>
        private void FillJobInfoFields()
        {
            SetContentAndTooltip(lblID, _jobToPrint.JobIdentifier.ToString());
            SetContentAndTooltip(lblJobName, _jobToPrint.Name);
            SetContentAndTooltip(lblPages, _jobToPrint.NumberOfPages.ToString());
            SetContentAndTooltip(lblSize, (_jobToPrint.JobSize / 1000000d).ToString());
            SetContentAndTooltip(lblSubmitted, _jobToPrint.TimeJobSubmitted.ToLocalTime().ToString());
        }

        /// <summary>
        /// Método para llenar los campos de la información del usuario.
        /// </summary>
        private void FillUserInfoFields()
        {
            SetContentAndTooltip(lblUser, _jobToPrint.Submitter);

            //Realizamos una consulta para obtener el nombre completo del usuario,
            //carrera y HojasDisponibles.
            String userInfo = GetUserInfo(lblUser.Tag.ToString(), lblUser.Content.ToString(), "Print job information");

            if (userInfo != null)
            {
                //La información vendrá concatenada con comas, en el siguiente formato:
                //"Nombre completo", "Carrera", "HojasDisponibles"
                //Ejemplo:
                //ENRIQUE ALONSO RIVERA NEVÁREZ,ISC,100

                //Realizamos un split para separar los elementos dentro de un arreglo.
                string[] userInfoArray = userInfo.Split(',');

                SetContentAndTooltip(lblUserName, (String.IsNullOrEmpty(userInfoArray[0]) ? "-----" : userInfoArray[0]));
                SetContentAndTooltip(lblCareer, (String.IsNullOrEmpty(userInfoArray[1]) ? "-----" : userInfoArray[1]));
                SetContentAndTooltip(lblAvailableSheets, userInfoArray[2]);

                //Checamos si tiene suficientes hojas
                HasEnoughSheets(Convert.ToInt32(lblAvailableSheets.Content), Convert.ToInt32(lblPages.Content));
            }
            else
            {
                //Llamamos al siguiente método si el usuario no se encuentra en la base de datos.
                UserDontExist();
            }
        }

        /// <summary>
        /// Método que nos indica si el usuario posee suficientes hojas.
        /// </summary>
        /// <param name="availableSheets">Hojas que tiene disponibles</param>
        /// <param name="pagesToRemove">Hojas que hay que removerle</param>
        /// <returns>true si el usuario cuenta con hojas suficientes, false en caso contrario</returns>
        private bool HasEnoughSheets(int availableSheets, int pagesToRemove)
        {
            _resultSheets = availableSheets - pagesToRemove;
            if (_resultSheets < 0)
            {
                btnPrint.IsEnabled = false;
                MessageBox.Show("The user doesn't have enough sheets.", "Print job information", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Método que realiza acciones en la ventana, cuando el usuario no está en la base de datos.
        /// </summary>
        private void UserDontExist()
        {
            SetContentAndTooltip(lblUserName, "-----");
            SetContentAndTooltip(lblAvailableSheets, "-----");

            //Deshabilitamos el botón para imprimir.
            btnPrint.IsEnabled = false;
        }

        /// <summary>
        /// Método que nos proporciona la información del usuario (Nombre completo, carrera y hojas disponibles).
        /// </summary>
        /// <param name="param">Parámetro indicado en la consulta parametrizada</param>
        /// <param name="controlNumber">Número de control que se usa para encontrar al usuario</param>
        /// <param name="errorTitle">Título que llevarán las ventanas de error</param>
        /// <returns>
        /// Una cadena con el siguiente formato:
        /// Nombre_completo,Carrera,HojasDisponibles
        /// </returns>
        private String GetUserInfo(string param, string controlNumber, string errorTitle)
        {
            string sqlException = "";
            List<ParamValue> paramList = new List<ParamValue>();

            paramList.Add(new ParamValue(param, controlNumber));

            DataTable queryResult = _sqlConn.Select(ref sqlException, Query.UserInfo, paramList);

            if (queryResult == null)
            {
                MessageBox.Show(sqlException, errorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            else
            {
                if (queryResult.Rows.Count == 0)
                {
                    MessageBox.Show("The user is not found in the database.", errorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                else
                {
                    return queryResult.Rows[0][0].ToString().Replace(",", "") + " " +    //Nombre_1
                           queryResult.Rows[0][1].ToString().Replace(",", "") + " " +    //Nombre_2
                           queryResult.Rows[0][2].ToString().Replace(",", "") + " " +    //Nombre_3
                           queryResult.Rows[0][3].ToString().Replace(",", "") + "," +    //Nombre_4
                           queryResult.Rows[0][4].ToString().Replace(",", "") + "," +    //Carrera
                           queryResult.Rows[0][5].ToString().Replace(",", "");           //HojasDisponibles
                }
            }
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
            else if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.Q)) // Ctrl + Q
            {
                this.Close();
            }
        }

        /// <summary>
        /// Método que pregunta sobre el número de copias enviadas.
        /// </summary>
        private void PrintJob()
        {
            if (MessageBox.Show("Has the user sent copies of this file?", "Print job information", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string response = (Microsoft.VisualBasic.Interaction.InputBox("Number of copies:", "Copies"));
                int totalPages;

                if (!response.Equals(""))
                {
                    if (IsValidNumber(response))
                    {
                        if (HasEnoughSheets(Convert.ToInt32(lblAvailableSheets.Content), totalPages = ((Convert.ToInt32(response) * _jobToPrint.NumberOfPages))))
                        {
                            DoUpdateQuery(totalPages.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter a valid value in number of sheets.", "Invalid field", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else
            {
                DoUpdateQuery(lblPages.Content.ToString());
            }            
        }

        /// <summary>
        /// Método que realiza la actualizacion e inserción de un registro en la base de datos.
        /// </summary>
        /// <param name="sheetsToRemove">Hojas totales que se van a remover</param>
        private void DoUpdateQuery(string sheetsToRemove)
        {
            String sqlException = "";
            List<ParamValue> paramList = new List<ParamValue>();

            paramList.Add(new ParamValue(lblAvailableSheets.Tag.ToString(), _resultSheets.ToString()));
            paramList.Add(new ParamValue(lblUser.Tag.ToString(), lblUser.Content.ToString()));

            //Actualizamos en la base de datos las hojas que le van a quedar al usuario.
            if (_sqlConn.UpdateInsert(ref sqlException, Query.UpdateAvailableSheets, paramList))
            {
                //Hacemos la inserción de un registro en la tabla HistorialTrabajos.
                sqlException = DoInsertQuery();
                if (sqlException != null)
                {
                    MessageBox.Show(sqlException, "Print job information", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                MessageBox.Show(sheetsToRemove + " sheets were removed to user " + lblUser.Content + ".", "Print job information", MessageBoxButton.OK, MessageBoxImage.Information);

                //Cambiar las propiedades del wrapper, para que el hilo secundario 
                //pueda dejar que se imprima el trabajo.
                _printJobWrapper.CurrentPrintingJob = _jobToPrint;
                _printJobWrapper.PrintJob = true;

                this.Close();
            }
            else
            {
                MessageBox.Show(sqlException, "Print job information", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Método para insertar un registro en la tabla HistorialTrabajos.
        /// </summary>
        /// <returns>Una cadena con la excepción que resulto</returns>
        private String DoInsertQuery()
        {
            string sqlException = "";
            List<ParamValue> paramList = new List<ParamValue>();

            string jobName = lblJobName.Content.ToString();
            if (jobName.Length > _jobNameMaxLenght)
            {
                jobName = jobName.Substring(0, _jobNameMaxLenght);
            }

            paramList.Add(new ParamValue(lblJobName.Tag.ToString(), jobName));  //Param = nombreArchivo
            paramList.Add(new ParamValue(lblPages.Tag.ToString(), lblPages.Content.ToString()));    //Param = numPaginas
            paramList.Add(new ParamValue(lblSubmitted.Tag.ToString(), _jobToPrint.TimeJobSubmitted.Date.ToString("yyyy-MM-dd HH:mm:ss")));  //Param = fecha
            paramList.Add(new ParamValue(lblUser.Tag.ToString(), lblUser.Content.ToString()));  //Param = idUser

            //Hacemos la inserción del registro.
            if (!_sqlConn.UpdateInsert(ref sqlException, Query.InsertHistorial, paramList))
            {
                return sqlException;
            }

            return null;
        }

        #endregion

        #region Validation

        private bool IsValidNumber(string num)
        {
            try
            {
                return !(Convert.ToInt32(num) < 1);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        #endregion

	}
}