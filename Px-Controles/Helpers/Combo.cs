using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Controles.Helpers
{
    public static class Combo
    {
        public static void Lineas(this ComboBox comboBox)
        {
            return;
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.DropDownStyle = ComboBoxStyle.DropDown;

            comboBox.Paint += (sender, e) =>
            {
                // Draw the underline with a thicker pen to simulate a border underline
                using (Pen pen = new Pen(Color.Black, 3))
                {
                    e.Graphics.DrawLine(pen, 0, comboBox.ClientSize.Height - 1, comboBox.ClientSize.Width, comboBox.ClientSize.Height - 1);
                }
            };

            comboBox.DropDown += (sender, e) =>
            {
                comboBox.Invalidate();
            };

            comboBox.DropDownClosed += (sender, e) =>
            {
                comboBox.Invalidate();
            };
        }
    }
}

