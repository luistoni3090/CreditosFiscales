using System;
using System.Drawing;
using System.Windows.Forms;

namespace Px_Controles.Controls.Imagen
{
    public partial class UIImageHD : UserControl
    {
        private Image _image;

        public Image Image
        {
            get { return _image; }
            set
            {
                if (value != null)
                {
                    // Embellecer la imagen aumentando la calidad antes de asignarla
                    _image = BeautifyImage(value, this.Width, this.Height);
                }
                else
                {
                    _image = null;
                }
                this.Invalidate(); // Redibuja el control cuando la imagen cambia
            }
        }

        public UIImageHD()
        {
            this.DoubleBuffered = true;
            this.Size = new Size(200, 200); // Tamaño por defecto
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_image != null)
            {
                // Configurar la calidad del renderizado
                e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                // Calcular el rectángulo para mantener la relación de aspecto
                Rectangle destRect = GetAspectRatioRect(this.ClientSize, _image.Size);

                // Dibujar la imagen en el rectángulo calculado
                e.Graphics.DrawImage(_image, destRect);
            }
        }

        private Rectangle GetAspectRatioRect(Size containerSize, Size imageSize)
        {
            float containerAspect = (float)containerSize.Width / containerSize.Height;
            float imageAspect = (float)imageSize.Width / imageSize.Height;

            int width, height;
            if (containerAspect > imageAspect)
            {
                // Ajustar a la altura del contenedor
                height = containerSize.Height;
                width = (int)(height * imageAspect);
            }
            else
            {
                // Ajustar al ancho del contenedor
                width = containerSize.Width;
                height = (int)(width / imageAspect);
            }

            int x = (containerSize.Width - width) / 2;
            int y = (containerSize.Height - height) / 2;

            return new Rectangle(x, y, width, height);
        }

        private Image BeautifyImage(Image originalImage, int targetWidth, int targetHeight)
        {
            Bitmap beautifiedImage = new Bitmap(targetWidth, targetHeight);
            beautifiedImage.SetResolution(originalImage.HorizontalResolution, originalImage.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(beautifiedImage))
            {
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                // Dibujar la imagen original redimensionada al nuevo Bitmap
                graphics.DrawImage(originalImage, new Rectangle(0, 0, targetWidth, targetHeight));
            }

            return beautifiedImage;
        }
    }
}
