using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Utiles.Utiles.Controles
{
    public static class Genericos
    {

        /// <summary>
        /// Método estático y genérico para obtener todos los controles de un tipo específico y es recursivo
        /// </summary>
        /// <typeparam name="T">Tipo del control a buscar</typeparam>
        /// <param name="parent">Forma o control padre donde se va a buscar los controles</param>
        /// <returns>Lista de controles encontrados</returns>
        public static IEnumerable<T> TraeTodosLosControlesDe<T>(this Control parent) where T : Control
        {
            var controls = parent.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => ctrl.TraeTodosLosControlesDe<T>()).Concat(controls).OfType<T>();
        }
    }
}
