using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

/// <summary>
/// Clase que contiene una serie de métodos y propiedades que son utilies para trabajar con consultas en la base de datos.
/// </summary>
public class SqlConnection
{
    #region Reference Variables

    //Almacenamos los datos correspondientes para la conexión con la base de datos.
    private string _serverIP, _port, _schema, _username, _password, _connString;

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    /// <param name="serverIP">Host name o IP del servidor de la base de datos</param>
    /// <param name="port">Puerto por el cuál se harán las peticiones al servidor de base de datos</param>
    /// <param name="schema">Base de datos seleccionada</param>
    /// <param name="username">Nombre de usuario con el cuál se realizarán las consultas en la base de datos</param>
    /// <param name="password">Contraseña del usuario</param>
    public SqlConnection(string serverIP, string port, string schema, string username, string password)
    {
        //Almacenamos las valores en las variables de instancia

        _serverIP = serverIP;
        _port = port;
        _schema= schema;
        _username = username;
        _password = password;

        _connString = string.Format("server={0};user={1};database={2};port={3};password={4};",
                                _serverIP, _username, _schema, _port, _password);
    }

    #endregion

    #region Getters

    /// <summary>
    /// Método para obterner la cadena de conexión para la base de datos.
    /// </summary>
    /// <returns>Cadena de conexión</returns>
    public string GetConnectionString()
    {
        return _connString;
    }

    #endregion

    #region Database Utilities
    
    /// <summary>
    /// Método para realizar una conexión de prueba con la base de datos.
    /// </summary>
    /// <param name="sqlException">Cadena donde se almacena la excepción producida</param>
    /// <returns>true si la conexión fue exitosa, false en caso contrario</returns>
    public bool IsConnSuccessful(ref string sqlException)
    {
        using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
        {
            try
            {
                conn.Open();
                conn.Close();

                return true;
            }
            catch (MySqlException mySqlEx)
            {
                sqlException = "Unable to connect to the database server.\n\n" + mySqlEx.Message;
                return false;
            }
        }
    }

    /// <summary>
    /// Método que realiza la consulta SELECT en la base de datos
    /// </summary>
    /// <param name="sqlConn">Objeto que encapsula los datos de la conexión con la base de datos</param>
    /// <param name="sqlException">Cadena donde se almacena la excepción producida</param>
    /// <param name="query">Consulta SELECT que se desea hacer</param>
    /// <param name="paramList">Lista de parámetros indicados en la consulta parametrizada</param>
    /// <returns>Un objeto de tipo DataTable</returns>
    public DataTable Select(ref string sqlException, string query, List<ParamValue> paramList)
    {
        using (MySqlConnection conn = new MySqlConnection(_connString))
        {
            try
            {
                conn.Open();

                MySqlCommand command = new MySqlCommand(query, conn);

                if (paramList != null)
                {
                    foreach (var item in paramList)
                    {
                        command.Parameters.AddWithValue(item.Param, item.Value);
                    }   
                }

                MySqlDataReader reader = command.ExecuteReader();
                DataTable tableResult = new DataTable();

                tableResult.Load(reader);

                reader.Close();
                conn.Close();

                return tableResult;
            }
            catch (MySqlException mySqlEx)
            {
                sqlException = "Error in select query.\n\n" + mySqlEx.Message;
                return null;
            }
        }
    }

    /// <summary>
    /// Método que puede realizar una actualización o una inserción de un registro en la base de datos.
    /// </summary>
    /// <param name="sqlConn">Objeto que encapsula los datos de la conexión con la base de datos</param>
    /// <param name="sqlException">Cadena donde se almacena la excepción producida</param>
    /// <param name="query">Consulta SELECT que se desea hacer</param>
    /// <param name="paramList">Lista de parámetros indicados en la consulta parametrizada</param>
    /// <returns>true si la inserción o actialización se realizó correctamente, false en caso contrario</returns>
    public bool UpdateInsert(ref string sqlException, string query, List<ParamValue> paramList)
    {
        using (MySqlConnection conn = new MySqlConnection(_connString))
        {
            try
            {
                conn.Open();

                MySqlCommand command = new MySqlCommand(query, conn);

                if (paramList != null)
                {
                    foreach (var item in paramList)
                    {
                        command.Parameters.AddWithValue(item.Param, item.Value);
                    }
                }

                command.ExecuteNonQuery();

                conn.Close();

                return true;
            }
            catch (MySqlException mySqlEx)
            {
                sqlException = "Error in update query.\n\n" + mySqlEx.Message;
                return false;
            }
        }
    }

    #endregion
}