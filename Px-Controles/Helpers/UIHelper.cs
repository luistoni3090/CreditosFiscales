using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Controles.Helpers
{
    public static class UIHelper
    {
        /// <summary>
        /// Cambia el color de fondo de los controles cuyo nombre comienza con "UI" y ajusta el ForeColor para asegurar la legibilidad.
        /// </summary>
        /// <param name="parent">El contenedor principal que contiene los controles.</param>
        /// <param name="newBackgroundColor">El nuevo color de fondo a aplicar.</param>
        public static Color CambiarColorControlesUI(Control parent, Color newBackgroundColor)
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));

            // Suspender el layout y el repintado
            parent.SuspendLayout();


            Color xFore = ObtenerForeColorContraste(newBackgroundColor);

            foreach (Control ctrl in parent.Controls)
            {
                // Status bar
                if (ctrl.Name.StartsWith("LBLTSTATUS", StringComparison.OrdinalIgnoreCase))
                    ctrl.ForeColor = newBackgroundColor;

                //// Tree
                //if (ctrl.Name.StartsWith("UITREE", StringComparison.OrdinalIgnoreCase))
                //    ctrl.NodeSelected = newBackgroundColor;

                // Verifica si el nombre del control comienza con "UI" (puede ajustarse según se necesite)
                if (ctrl.Name.StartsWith("UI", StringComparison.OrdinalIgnoreCase) || 
                    ctrl.Name.StartsWith("PANTITTLE", StringComparison.OrdinalIgnoreCase) || 
                    ctrl.Name.StartsWith("PANXTITTLE", StringComparison.OrdinalIgnoreCase) || 
                    ctrl.Name.StartsWith("LBLTITULO", StringComparison.OrdinalIgnoreCase) ||
                    ctrl.Name.StartsWith("PANTMTX", StringComparison.OrdinalIgnoreCase)
                    )
                {
                    // Cambia el color de fondo
                    ctrl.BackColor = newBackgroundColor;

                    // Calcula y establece el ForeColor para asegurar la legibilidad
                    ctrl.ForeColor = xFore;

                    if (ctrl.Name.StartsWith("UITAB", StringComparison.OrdinalIgnoreCase) || ctrl.Name.StartsWith("UITREE", StringComparison.OrdinalIgnoreCase))
                        ctrl.BackColor = Color.White;


                }

                // Si el control tiene sub controles, aplica la función recursivamente
                if (ctrl.HasChildren)
                    CambiarColorControlesUI(ctrl, newBackgroundColor);
            }

            // Reanudar el redibujado
            parent.ResumeLayout();

            return xFore;
        }

        /// <summary>
        /// Calcula un color de primer plano que contraste bien con el color de fondo proporcionado.
        /// Utiliza la luminancia relativa para determinar si debe ser negro o blanco.
        /// </summary>
        /// <param name="background">El color de fondo.</param>
        /// <returns>Color negro o blanco dependiendo del contraste.</returns>
        private static Color ObtenerForeColorContraste(Color background)
        {


            //// Calcula la luminancia relativa según la fórmula estándar
            //double luminance = (0.299 * background.R + 0.587 * background.G + 0.114 * background.B) / 255;

            //// Si la luminancia es alta, usa color negro; de lo contrario, blanco
            //return luminance > 0.5 ? Color.Black : Color.White;


            // Convertir los componentes RGB a valores entre 0 y 1
            double r = background.R / 255.0;
            double g = background.G / 255.0;
            double b = background.B / 255.0;

            // Aplicar la corrección gamma
            r = (r <= 0.03928) ? r / 12.92 : Math.Pow((r + 0.055) / 1.055, 2.4);
            g = (g <= 0.03928) ? g / 12.92 : Math.Pow((g + 0.055) / 1.055, 2.4);
            b = (b <= 0.03928) ? b / 12.92 : Math.Pow((b + 0.055) / 1.055, 2.4);

            // Calcular la luminancia relativa
            double luminance = 0.2126 * r + 0.7152 * g + 0.0722 * b;

            // Definir el umbral según las recomendaciones de accesibilidad
            return luminance > 0.179 ? Color.Black : Color.White;
        }


        /// <summary>
        /// Convierte un color dado a una versión pastel mezclándolo con blanco.
        /// </summary>
        /// <param name="color">El color original.</param>
        /// <param name="ratio">La proporción de mezcla con blanco (0.0 a 1.0).</param>
        /// <returns>El color pastel resultante.</returns>
        public static Color ConvertToPastel(Color color, double ratio = 0.5)
        {
            // Asegurarse de que el ratio esté entre 0 y 1
            ratio = Math.Max(0, Math.Min(1, ratio));

            int r = (int)(color.R + (255 - color.R) * ratio);
            int g = (int)(color.G + (255 - color.G) * ratio);
            int b = (int)(color.B + (255 - color.B) * ratio);

            return Color.FromArgb( 80,r, g, b);
        }
    }
}
