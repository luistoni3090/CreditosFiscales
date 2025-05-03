using Px_Utiles.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Px_Utiles.Utiles.Controles;


namespace Px_Utiles.Utiles.Controles
{
    public static class Grid
    {
        //public static void Diseño(this DataGridView xGrid, Color Fore, Color ColorSel)
        //{
        //    //await Task.Delay(0);

        //    //await Status("Aplicando estilos al grid . . .", (int)MensajeTipo.Info);

        //    //if (Fore == Color.White)
        //    Fore = UIHelper.ObtenerForeColorContraste(ColorSel); //Color.FromArgb(9, 9, 9);

        //    double proporciónPastel = 0.2;
        //    var ColorSel1 = UIHelper.ConvertToPastel(ColorSel, proporciónPastel, 255);

        //    var sBGColor = "#ffffff";

        //    xGrid.DefaultCellStyle.SelectionBackColor = ColorSel; //Color.FromArgb(106, 47, 66); // Color de fondo cuando se selecciona
        //    xGrid.DefaultCellStyle.SelectionForeColor = Fore; // Color.White; // Color del texto cuando se selecciona

        //    // Opcional: Configurar el estilo de las filas alternas, si es necesario
        //    xGrid.AlternatingRowsDefaultCellStyle.SelectionBackColor = ColorSel1; //Color.Red; // Color.FromArgb(149, 90, 109); //Color.FromArgb(125, 66, 85); //Color.DarkCyan;
        //    xGrid.AlternatingRowsDefaultCellStyle.SelectionForeColor = Fore; // Color.DarkGray; // Color.White;

        //    //xGrid.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //    xGrid.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(sBGColor); // Color.FloralWhite;
        //    xGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(106, 47, 66);        //Color.DimGray;
        //    xGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

        //    xGrid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

        //    xGrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;

        //    xGrid.EnableHeadersVisualStyles = false;

        //    xGrid.AutoResizeColumns();
        //    xGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        //    DataGridViewCellStyle estilo = new DataGridViewCellStyle();
        //    estilo.BackColor = System.Drawing.ColorTranslator.FromHtml(sBGColor); //Color.FloralWhite;
        //    estilo.ForeColor = Color.FromArgb(106, 47, 66);        //Color.DimGray;
        //    estilo.Alignment = DataGridViewContentAlignment.BottomRight;
        //    estilo.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        //    foreach (DataGridViewRow row in xGrid.Rows)
        //    {
        //        row.HeaderCell.Value = (row.Index + 1).ToString("N0");
        //        row.HeaderCell.Style = estilo;
        //        row.DefaultCellStyle.ForeColor = Color.DimGray;
        //        row.DefaultCellStyle.BackColor = Color.Red;
        //    }
        //    xGrid.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

        //    foreach (DataGridViewRow row in xGrid.Rows)
        //    {
        //        row.DefaultCellStyle.ForeColor = Color.FromArgb(33,33,33);
        //        row.DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(sBGColor);
        //    }

        //    //foreach (Control oCon in xGrid.Controls)
        //    //{
        //    //    oCon.BackColor = Color.Red;
        //    //}




        //}




        public static void Diseño(this DataGridView xGrid, Color Fore, Color ColorSel)
        {
            // Suspender actualizaciones de dibujo
            xGrid.SuspendLayout();

            try
            {
                Fore = UIHelper.ObtenerForeColorContraste(ColorSel);

                double proporciónPastel = 0.2;
                var ColorSel1 = UIHelper.ConvertToPastel(ColorSel, proporciónPastel, 255);

                var sBGColor = "#ffffff";

                // Establecer estilos predeterminados para evitar iteraciones sobre las filas
                xGrid.DefaultCellStyle.SelectionBackColor = ColorSel;
                xGrid.DefaultCellStyle.SelectionForeColor = Fore;
                xGrid.AlternatingRowsDefaultCellStyle.SelectionBackColor = ColorSel1;
                xGrid.AlternatingRowsDefaultCellStyle.SelectionForeColor = Fore;

                xGrid.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(sBGColor);
                xGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(106, 47, 66);
                xGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                xGrid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                xGrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
                xGrid.EnableHeadersVisualStyles = false;

                xGrid.AutoResizeColumns();
                xGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                // Establecer estilos predeterminados para filas y celdas
                DataGridViewCellStyle estilo = new DataGridViewCellStyle();
                estilo.BackColor = System.Drawing.ColorTranslator.FromHtml(sBGColor);
                estilo.ForeColor = Color.FromArgb(106, 47, 66);
                estilo.Alignment = DataGridViewContentAlignment.BottomRight;
                estilo.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                xGrid.RowHeadersDefaultCellStyle = estilo;

                xGrid.DefaultCellStyle.ForeColor = Color.FromArgb(33, 33, 33);
                xGrid.DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(sBGColor);

                xGrid.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

                // Manejar el evento RowPostPaint para mostrar los números de fila
                xGrid.RowPostPaint -= xGrid_RowPostPaint; // Evitar múltiples suscripciones
                xGrid.RowPostPaint += xGrid_RowPostPaint;
            }
            finally
            {
                // Reanudar actualizaciones de dibujo
                xGrid.ResumeLayout();
                xGrid.Refresh(); // Opcional: refrescar el control si es necesario
            }
        }


        private static void xGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            Rectangle headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);

            e.Graphics.DrawString(rowIdx, grid.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }


    }
}
