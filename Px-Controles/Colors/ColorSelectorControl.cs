using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Controles.Colors
{
    public partial class ColorSelectorControl : UserControl
    {
        // Evento que se dispara cuando se selecciona un color
        public event EventHandler<ColorSelectedEventArgs> ColorSelected;

        public ColorSelectorControl()
        {
            InitializeComponent();
            // Suscribirse al evento Click del botón
            xbtnSelectColor.Click += BtnSelectColor_Click;
        }

        private void BtnSelectColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                // Opcional: establecer propiedades iniciales del ColorDialog
                colorDialog.AllowFullOpen = true;
                colorDialog.AnyColor = true;
                colorDialog.SolidColorOnly = false;

                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    // Obtener el color seleccionado
                    Color selectedColor = colorDialog.Color;

                    // Disparar el evento con el color seleccionado
                    OnColorSelected(selectedColor);
                }
            }
        }

        protected virtual void OnColorSelected(Color color)
        {
            ColorSelected?.Invoke(this, new ColorSelectedEventArgs(color));
        }
    }



    // Clase personalizada para pasar el color seleccionado en el evento
    public class ColorSelectedEventArgs : EventArgs
    {
        public Color SelectedColor { get; }

        public ColorSelectedEventArgs(Color color)
        {
            SelectedColor = color;
        }
    }


}
