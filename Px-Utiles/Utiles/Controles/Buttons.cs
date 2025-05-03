using Px_Utiles.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Utiles.Utiles.Controles
{
    public static class Buttons
    {
        public static void Colapsable(this Button button, Color Fore, Color ColorSel, string text, float fontSize = 12)
        {
            Size originalSize = button.Size;
            Font originalFont = button.Font;
            Font expandedFont = new Font(button.Font.FontFamily, fontSize);
            double proporciónPastel = 0.2;
            var ColorSel1 = UIHelper.ConvertToPastel(ColorSel, proporciónPastel, 255);
            Fore = UIHelper.ObtenerForeColorContraste(ColorSel);

            Color oColorOri = button.BackColor;

            button.ForeColor = Fore;
            //button.BackColor = ColorSel1;
            button.TextAlign = ContentAlignment.MiddleRight;

            using (Graphics g = button.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(text, expandedFont);
                int expandedWidth = originalSize.Width + (int)textSize.Width + 20; // Ajuste para incluir margen
                Size expandedSize = new Size(expandedWidth, originalSize.Height);

                button.MouseEnter += (sender, e) =>
                {
                    // Expandir el botón y mostrar el texto
                    button.Size = expandedSize;
                    button.Font = expandedFont;
                    button.Text = text;
                    button.ImageAlign = ContentAlignment.MiddleLeft;
                    button.BackColor = ColorSel1;
                };

                button.MouseLeave += (sender, e) =>
                {
                    // Restaurar el tamaño original del botón y ocultar el texto
                    button.Size = originalSize;
                    button.Font = originalFont;
                    button.Text = string.Empty;
                    button.ImageAlign = ContentAlignment.MiddleCenter;
                    button.BackColor = oColorOri;
                };
            }
        }
    }
}
