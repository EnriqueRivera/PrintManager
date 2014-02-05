using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;

namespace PrintManager
{
    /// <summary>
    /// Clase que envuelve las variables necesarias para imprimir un trabajo.
    /// </summary>
    public class PrintJobWrapper
    {
        #region Reference Variables

        /// <summary>
        /// Variable que indica que se desea imprimir un trabajo
        /// </summary>
        public bool PrintJob { get; set; }

        /// <summary>
        /// Trabajo que se está imprimiendo actualmente
        /// </summary>
        public PrintSystemJobInfo CurrentPrintingJob { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="printJob">Variable que indica que se desea imprimir un trabajo</param>
        /// <param name="currentPrintJob">Trabajo que se está imprimiendo actualmente</param>
        public PrintJobWrapper(bool printJob, PrintSystemJobInfo currentPrintJob)
        {
            PrintJob = printJob;
            CurrentPrintingJob = currentPrintJob;
        }

        #endregion
    }   
}
