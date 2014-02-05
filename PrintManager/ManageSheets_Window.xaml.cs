using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    /// Ventana para la administración de hojas.
	/// </summary>
	public partial class ManageSheets_Window : Window
    {
        #region Private members

        /// <summary>
        /// Almacena los datos de la conexión que se realizó en la primer ventana.
        /// </summary>
        private SqlConnection _sqlConn;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor de la ventana para administrar hojas.
        /// </summary>
        /// <param name="sqlConn">Datos de la conexión a la base de datos</param>
        public ManageSheets_Window(SqlConnection sqlConn)
		{
			this.InitializeComponent();

            WindowFormat.CenterWindowOnScreen(this);

            _sqlConn = sqlConn;			
		}

        #endregion

        #region Events

        private void btnAddSheets_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddSheets();
        }

        private void btnRemoveSheets_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RemoveSheets();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeyCombinations(e);
        }

        private void btnSearchUser_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SearchUser();
        }

        #endregion

        #region Functionallity

        /// <summary>
        /// Método que checa las hojas disponibles de determinado usuario.
        /// </summary>
        private void SearchUser()
        {
            if (!String.IsNullOrEmpty(txtControlNumber.Text))
            {
                //Obtenemos las hojas disponibles.
                int? sheets = GetAvailableSheets(txtControlNumber.Tag.ToString(), txtControlNumber.Text, "Available sheets");

                if (sheets != null)
                {
                    //Le mostramos al usuario cuantas hojas disponibles tiene.
                    MessageBox.Show("Available sheets: " + sheets + ".", "Available sheets", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                //No ingresó algún usuario.
                MessageBox.Show("Enter a Control number.", "Missing field", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Método para obtener el número de hojas disponibles del usuario.
        /// </summary>
        /// <param name="param">Nombre del parametro que será usado en la consulta parametrizada</param>
        /// <param name="controlNumber">Id del usuario</param>
        /// <param name="errorTitle">Titulo que llevará la ventana si llegase a surgir un error</param>
        /// <returns>Npumero de hojas disponibles</returns>
        private int? GetAvailableSheets(string param, string controlNumber, string errorTitle)
        {
            string sqlException = "";
            List<ParamValue> paramList = new List<ParamValue>();

            //Agregamos a la lista parametros sólo un elemento, ya que para la consulta
            //"AvailableSheets" es necesario solo un parametro (@idUser).
            paramList.Add(new ParamValue(param, controlNumber));

            //Realizamos la consulta y obtenemos un DataTable
            DataTable queryResult = SqlConnection.Select(_sqlConn, ref sqlException, Query.AvailableSheets, paramList);

            if (queryResult == null)
            {
                //Si el DataTable es nulo, entonces surgió un error.
                MessageBox.Show(sqlException, errorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            else
            {
                if (queryResult.Rows.Count == 0)
                {
                    //Si el DataTable tiene 0 filas, quiere decir que no existe el usuario.
                    MessageBox.Show("The user is not found in the database.", errorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                else
                {
                    //Retornamos las hojas disponibles.
                    return Convert.ToInt32(queryResult.Rows[0][0]);
                }
            }
        }


        /// <summary>
        /// Método que realiza una consulta para checar la contraseña del Administrador.
        /// </summary>
        /// <returns>true si la contraseña es correcta, false en caso contrario</returns>
        private bool IsCorrectPassword()
        {
            string sqlException = "";
            List<ParamValue> paramList = new List<ParamValue>();

            paramList.Add(new ParamValue(txtControlNumber.Tag.ToString(), "Administrador"));
            paramList.Add(new ParamValue(pwdBox.Tag.ToString(), pwdBox.Password));

            DataTable queryResult = SqlConnection.Select(_sqlConn, ref sqlException, Query.CheckPassword, paramList);

            if (queryResult == null)
            {
                MessageBox.Show(sqlException, "Add/Remove sheets", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                if (queryResult.Rows.Count == 0)
                {
                    MessageBox.Show("Incorrect password.", "Add/Remove sheets", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Método utilizado para agregar hojas a algún usuario.
        /// </summary>
        private void AddSheets()
        {
            StringBuilder errorMessage = new StringBuilder();
            if (IsAFieldEmpty(errorMessage))
            {
                errorMessage.Insert(0, "Check the following fields:\n\n");
                MessageBox.Show(errorMessage.ToString(), "Missing fields", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (!IsValidNumber(txtNumSheets.Text))
            {
                MessageBox.Show("Enter a valid value in number of sheets.", "Invalid field", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DoQueryAdd();
            }
        }

        /// <summary>
        /// Método utilizado para remover hojas a algún usuario.
        /// </summary>
        private void RemoveSheets()
        {
            StringBuilder errorMessage = new StringBuilder();
            if (IsAFieldEmpty(errorMessage))
            {
                errorMessage.Insert(0, "Check the following fields:\n\n");
                MessageBox.Show(errorMessage.ToString(), "Missing fields", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (!IsValidNumber(txtNumSheets.Text))
            {
                MessageBox.Show("Enter a valid value in number of sheets.", "Invalid field", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DoQueryRemove();
            }
        }

        /// <summary>
        /// Método que calcula las hojas que son requeridas para hacer el update en la base de datos.
        /// </summary>
        private void DoQueryAdd()
        {
            if (IsCorrectPassword())
            {
                int? currentSheets = GetAvailableSheets(txtControlNumber.Tag.ToString(), txtControlNumber.Text, "Available sheets");
                int sheetsToAdd = Convert.ToInt32(txtNumSheets.Text);

                if (currentSheets != null)
                {
                    if (MessageBox.Show("Are you sure you want to add " + txtNumSheets.Text + " sheet(s) to user " + txtControlNumber.Text + "?", "Add sheets", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        currentSheets = (currentSheets < 1) ? sheetsToAdd : (currentSheets + sheetsToAdd);

                        DoUpdateQuery(Convert.ToInt32(currentSheets), "added", "Add sheets");
                    }
                }
            }
        }

        /// <summary>
        /// Método que calcula las hojas que son requeridas para hacer el update en la base de datos.
        /// </summary>
        private void DoQueryRemove()
        {
            if (IsCorrectPassword())
            {
                int? currentSheets = GetAvailableSheets(txtControlNumber.Tag.ToString(), txtControlNumber.Text, "Available sheets");
                int sheetsToRemove = Convert.ToInt32(txtNumSheets.Text);

                if (currentSheets != null)
                {
                    if (currentSheets <= 0)
                    {
                        MessageBox.Show("You can't remove due to the user hasn't enough sheets.", "Remove sheets", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else if (MessageBox.Show("Are you sure you want to remove " + sheetsToRemove + " sheet(s) to user " + txtControlNumber.Text + "?", "Remove sheets", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        sheetsToRemove = Convert.ToInt32(currentSheets) - sheetsToRemove;
                        sheetsToRemove = (sheetsToRemove < 0) ? 0 : sheetsToRemove;

                        //Hacer update
                        DoUpdateQuery(sheetsToRemove, "removed", "Remove sheets");
                    }
                }
            }
        }

        /// <summary>
        /// Método que realiza el update de las hojas.
        /// </summary>
        /// <param name="sheets">Valor en el que quedará el registro HojasDisponibles</param>
        /// <param name="addRemove">Indicamos si la consulta es para agregar o remover</param>
        /// <param name="title">Título de las ventanas emergentes</param>
        private void DoUpdateQuery(int sheets, string addRemove, string title)
        {
            string sqlException = "";
            List<ParamValue> paramList = new List<ParamValue>();

            paramList.Add(new ParamValue(txtNumSheets.Tag.ToString(), sheets.ToString()));
            paramList.Add(new ParamValue(txtControlNumber.Tag.ToString(), txtControlNumber.Text));

            if (SqlConnection.UpdateInsert(_sqlConn, ref sqlException, Query.UpdateAvailableSheets, paramList))
            {
                MessageBox.Show(txtNumSheets.Text + " sheets were " + addRemove + " to user " + txtControlNumber.Text + ".", title, MessageBoxButton.OK, MessageBoxImage.Information);
                pwdBox.Password = "";
                txtNumSheets.Text = "";
            }
            else
            {
                MessageBox.Show(sqlException, title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Método que checa si hay algún campo vacío en la ventana.
        /// </summary>
        /// <param name="errorMessage">Variable que almacenará el nombre de los campos que están vacíos</param>
        /// <returns>true si un campo es vacío, false en caso contrario</returns>
        private bool IsAFieldEmpty(StringBuilder errorMessage)
        {
            errorMessage.Append("");

            if (String.IsNullOrEmpty(txtControlNumber.Text))
            {
                errorMessage.AppendLine(lblControlNumber.Tag.ToString());
            }

            if (String.IsNullOrEmpty(pwdBox.Password))
            {
                errorMessage.AppendLine(lblPassword.Tag.ToString());
            }

            if (String.IsNullOrEmpty(txtNumSheets.Text))
            {
                errorMessage.AppendLine(lblNumSheets.Tag.ToString());
            }

            return !errorMessage.ToString().Equals("");
        }

        /// <summary>
        /// Método para saber si se presionó una combinación correcta de teclas.
        /// </summary>
        /// <param name="e">Encapsula propiedades de la tecla presionada</param>
        private void KeyCombinations(KeyEventArgs e)
        {
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.S)) // Ctrl + S
            {
                SearchUser();
            }
            else if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.A)) // Ctrl + A
            {
                AddSheets();
            }
            else if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.R)) // Ctrl + R
            {
                RemoveSheets();
            }
        }

        #endregion

        #region Validation

        /// <summary>
        /// Método que checa si una cadena es un número válido (si es menor a 1 se considera inválido).
        /// </summary>
        /// <param name="num">Número a evaluar</param>
        /// <returns>true si es válido, false en caso contrario</returns>
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