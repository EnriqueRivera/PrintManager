using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintManager
{
    /// <summary>
    /// Clase que almacena las querys.
    /// </summary>
    class Query
    {
        #region QueryStrings

        internal static string AvailableSheets = "SELECT HojasDisponibles FROM Users WHERE idUser = @idUser";
        internal static string UserInfo = "SELECT Nombre_1, Nombre_2, Apellido_1, Apellido_2, Carrera, HojasDisponibles  FROM Users WHERE idUser = @idUser";
        internal static string CheckPassword = "SELECT idUser FROM Users WHERE idUser = @idUser AND password = @password";
        internal static string UpdateAvailableSheets = "UPDATE users SET HojasDisponibles = @Sheets WHERE idUser = @idUser";
        internal static string InsertHistorial = "INSERT INTO historialtrabajos (nombreArchivo, numPaginas, fecha, idUser) VALUES (@jobName, @pages, @date, @idUser)";

        #endregion
    }
}
