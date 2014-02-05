using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace PrintManager
{
    /// <summary>
    /// Clase para aplicar formato a ventanas.
    /// </summary>
    class WindowFormat
    {
        /// <summary>
        /// Método que coloca la ventana en el centro.
        /// </summary>
        /// <param name="window">Ventana que será centrada</param>
        internal static void CenterWindowOnScreen(Window window)
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = window.Width;
            double windowHeight = window.Height;
            window.Left = (screenWidth / 2) - (windowWidth / 2);
            window.Top = (screenHeight / 2) - (windowHeight / 2);
        }
    }
}
