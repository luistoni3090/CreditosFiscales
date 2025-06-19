using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Windows.Forms.GroupBox;

namespace Px_Controles.Helpers
{
    public static class GroupBox
    {
        public static void Beauty(this System.Windows.Forms.GroupBox groupBox, Color colorFuente, Color colorLinea)
        {
            // Almacenar los colores en la propiedad Tag
            groupBox.Tag = new Tuple<Color, Color>(colorFuente, colorLinea);

            // Eliminar el controlador existente si ya ha sido agregado
            groupBox.Paint -= GroupBox_Paint;
            // Agregar el controlador
            groupBox.Paint += GroupBox_Paint;
            groupBox.Invalidate();
        }

        private static void GroupBox_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.GroupBox box = sender as System.Windows.Forms.GroupBox;

            if (box != null)
            {
                // Recuperar los colores desde la propiedad Tag
                if (box.Tag is Tuple<Color, Color> colores)
                {
                    Color colorFuente = colores.Item1;
                    Color colorLinea = colores.Item2;

                    Font textFont = box.Font;

                    // Medir el texto
                    SizeF textSize = e.Graphics.MeasureString(box.Text, textFont);

                    // Determinar el rectángulo del borde
                    Rectangle borderRect = new Rectangle(
                        box.ClientRectangle.X,
                        box.ClientRectangle.Y + (int)(textSize.Height / 2),
                        box.ClientRectangle.Width - 1,
                        box.ClientRectangle.Height - (int)(textSize.Height / 2) - 1);

                    // Dibujar el borde
                    using (Pen borderPen = new Pen(colorLinea))
                    {
                        e.Graphics.DrawRectangle(borderPen, borderRect);
                    }

                    // Dibujar el texto
                    using (SolidBrush textBrush = new SolidBrush(colorFuente))
                    {
                        // Determinar la posición del texto
                        Rectangle textRect = new Rectangle(
                            box.ClientRectangle.X + 6,
                            box.ClientRectangle.Y,
                            (int)textSize.Width + 1,
                            (int)textSize.Height);

                        // Dibujar un rectángulo de fondo para tapar la línea del borde detrás del texto
                        using (SolidBrush backBrush = new SolidBrush(box.BackColor))
                        {
                            e.Graphics.FillRectangle(backBrush, textRect);
                        }

                        // Dibujar el texto
                        e.Graphics.DrawString(box.Text, textFont, textBrush, textRect);
                    }
                }
            }
        }
    }
}

