using System;
using System.Collections.Generic;
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
using System.Net.NetworkInformation;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Security.Principal;
using System.Reflection;

namespace PrintManager
{
	/// <summary>
    /// Ventana inicial, para checar si se puede establecer una conexión con el servidor de base de datos.
	/// </summary>
	public partial class ConnToDB_Window : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor de la ventana.
        /// </summary>
        public ConnToDB_Window()
		{
            //Agregamos los dlls al exe.
            AppendDllsToExe();

            //Si el usuario no es administrador, le informamos que corra el programa con más privilegios.
            if (!IsCurrentUserAnAdministrator())
            {
                MessageBox.Show("Run the application as administrator.", "Privileges requiered", MessageBoxButton.OK, MessageBoxImage.Error);

                this.Close();
            }

			this.InitializeComponent();

            //Centramos la ventana
            WindowFormat.CenterWindowOnScreen(this);
		}

        #endregion

        #region Events

        private void btnConnect_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DoConnection();
        }

        private void btnExit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void imgLogoITCHII_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Abrimos en el navegador de internet la siguiente url.
            System.Diagnostics.Process.Start("http://www.itchihuahuaii.edu.mx/");
        }

        private void btnAbout_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("PrintManager V1.0\n\nDeveloped by:\nI.S.C Enrique Alonso Rivera Nevárez.\n\nContact:\nenriqueriveranevarez@gmail.com", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnPing_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DoPing();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeyCombinations(e);
        }

        #endregion
        
        #region Validations

        /// <summary>
        /// Valida si un elemento de un Grid es nulo o vacío.
        /// </summary>
        /// <param name="gridTextBox">Contiene el TextBox o PasswordBox a validar</param>
        /// <param name="errorMessage">Se le concatena el tag de cada control vacío</param>
        /// <returns>true si al menos un elemento en el Grid está vacío</returns>
        private bool IsAFieldEmpty(Grid gridTextBox, StringBuilder errorMessage)
        {
            //Vaciamos el mensaje de error
            errorMessage.Append("");

            //Iteramos los elementos del Grid
            foreach (var item in gridTextBox.Children)
            {
                if ((item is TextBox) && String.IsNullOrEmpty(((TextBox)item).Text))
                {
                    //Si el TextBox no tiene nada, agregamos el tag al mensaje de error.
                    errorMessage.AppendLine(((TextBox)item).Tag.ToString());
                }
                else if ((item is PasswordBox) && String.IsNullOrEmpty(((PasswordBox)item).Password))
                {
                    //Si el PasswordBox no tiene nada, agregamos el tag al mensaje de error.
                    errorMessage.AppendLine(((PasswordBox)item).Tag.ToString());
                }
            }

            return !errorMessage.ToString().Equals("");
        }

        /// <summary>
        /// Valida si el usuario actual tiene privilegios de administrador.
        /// </summary>
        /// <returns>true si el usuario es administrador, falso si no es así</returns>
        private bool IsCurrentUserAnAdministrator()
        {
            Thread.GetDomain().SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

            WindowsPrincipal currentUser = (WindowsPrincipal)Thread.CurrentPrincipal;

            if (!currentUser.IsInRole(WindowsBuiltInRole.Administrator))
            {
                //Si el usuario no tiene un rol de administrador, retornamos false
                return false;
            }

            return true;
        }

        #endregion

        #region Functionallity

        /// <summary>
        /// Método que ayuda a poner los dlls dentro del exe.
        /// </summary>
        private void AppendDllsToExe()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string resourceName = new AssemblyName(args.Name).Name + ".dll";
                string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };
        }

        /// <summary>
        /// Método para saber si se presionó una combinación correcta de teclas.
        /// </summary>
        /// <param name="e">Encapsula propiedades de la tecla presionada</param>
        private void KeyCombinations(KeyEventArgs e)
        {
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.P)) // Ctrl + P
            {
                DoPing();
            }
            else if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.Q)) // Ctrl + Q
            {
                DoConnection();
            }
            else if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.E)) // Ctrl + E
            {
                this.Close();
            }
            else if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.A)) // Ctrl + A
            {
                MessageBox.Show("PrintManager V1.0\n\nDeveloped by:\nI.S.C Enrique Alonso Rivera Nevárez.\n\nContact:\nenriqueriveranevarez@gmail.com", "About", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Realizamos una conexión de prueba a la base de datos.
        /// </summary>
        private void DoConnection()
        {
            //Colocamos el cursor en estado de espera.
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                StringBuilder errorMessage = new StringBuilder();
                if (IsAFieldEmpty(grdTxtContainer, errorMessage))
                {
                    errorMessage.Insert(0, "Check the following fields:\n\n");

                    //Si un campo está vació, le mostramos el siguiente mensaje al usuario:
                    MessageBox.Show(errorMessage.ToString(), "Missing fields", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    string sqlException = "";

                    //Creamos la variable que encapsula todos los datos de nuestra conexión.
                    SqlConnection sqlConn = new SqlConnection(txtServerIP.Text, txtPort.Text, txtSchema.Text,
                                                    txtUsername.Text, pwdBox.Password);

                    //Realizamos la conexión de prueba con la base de datos.
                    if (sqlConn.IsConnSuccessful(ref sqlException))
                    {
                        //Si fue exitosa, mostramos la ventana donde se selecciona la impresora.
                        SelectPrinter_Window selPrintWnd = new SelectPrinter_Window(sqlConn);
                        selPrintWnd.Show();

                        //Cerramos esta ventana.
                        this.Close();
                    }
                    else
                    {
                        //Si no se pudo realizar la conexión, mostramos el siguiente mensaje:
                        MessageBox.Show(sqlException, "Database connection error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            finally
            {
                //Colocamos el cursor en su estado normal.
                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// Realizamos un ping con el host indicado.
        /// </summary>
        private void DoPing()
        {
            try
            {
                //Si no está vacío el campo de la IP del servidor, realizamos el ping.
                if (!String.IsNullOrEmpty(txtServerIP.Text))
                {
                    if (new Ping().Send(txtServerIP.Text, 2000).Status == IPStatus.Success)
                    {
                        //Si es exitoso el ping, mostramos el siguiente mensaje:
                        MessageBox.Show("Successful.", "Ping", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        //Si no es exitoso el ping, mostramos el siguiente mensaje:
                        MessageBox.Show("Failed.", "Ping", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    //Le informamos al usuario que debe teclear el nombre del servidor o la IP.
                    MessageBox.Show("Enter a host name or IP address.", "Ping", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (PingException)
            {
                //Ha fallado el ping, por algún tipo de excepción.
                MessageBox.Show("Failed.", "Ping", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

    }
}