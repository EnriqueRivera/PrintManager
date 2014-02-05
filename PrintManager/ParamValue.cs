using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
* Ejemplo:
* Query = SELECT HojasDisponibles FROM Users WHERE idUser = @idUser
* Param deberá contener la cadena "idUser"
* Value deberá contener el valor que reemplazará el parámetro indicado (@idUser).
* para poder realizar la consulta.
* 
* Entonces, Param = "idUser" y Value = "09550563"
* Al hacer la consulta quedaría de la siguiente forma:
* SELECT HojasDisponibles FROM Users WHERE idUser = 09550563
*/

/// <summary>
/// Clase que encapsula el parámetro y valor para cada consulta parametrizada.
/// </summary>
public class ParamValue
{
    #region Instance variables

    public string Param { get; set; }
    public string Value { get; set; }

    #endregion

    #region Constructor

    public ParamValue(string param, string value)
    {
        Param = param;
        Value = value;
    }

    #endregion
}
