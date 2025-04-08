using Px_Licencias.Licencias.licmanto;
using Px_Licencias.Licencias.licmanto2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Licencias
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new w_tipoojo());
            //Application.Run(new w_cat_usr_list());
            //Application.Run(new Form1());
        }
    }
}
