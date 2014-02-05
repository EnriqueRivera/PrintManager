using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintManager
{
    /// <summary>
    /// Clase que encapsula las propiedades necesarias para agregar registros al DataGrid.
    /// </summary>
    class Job
    {
        #region Reference Variables

        public int jobID { get; set; }
        public string owner { get; set; }
        public string documentName { get; set; }
        public int pages { get; set; }
        public string status { get; set; }
        public double size { get; set; }
        public DateTime submitted { get; set; }

        #endregion
    }
}
