using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Clase que almacena las querys...
/// </summary>
public class Query
{
    #region QueryStrings
        
    public static string AvailableSheets = "SELECT HojasDisponibles FROM Users WHERE idUser = @idUser";
    public static string UserInfo = "SELECT Nombre_1, Nombre_2, Apellido_1, Apellido_2, Carrera, HojasDisponibles  FROM Users WHERE idUser = @idUser";
    public static string CheckPassword = "SELECT idUser FROM Users WHERE idUser = @idUser AND password = @password";
    public static string UpdateAvailableSheets = "UPDATE users SET HojasDisponibles = @Sheets WHERE idUser = @idUser";
    public static string InsertHistorial = "INSERT INTO historialtrabajos (nombreArchivo, numPaginas, fecha, idUser) VALUES (@jobName, @pages, @date, @idUser)";

    #endregion
}
