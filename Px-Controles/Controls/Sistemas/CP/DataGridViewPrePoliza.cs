using Px_Controles.Controls.DataGridView;
using Px_Controles.Controls.Sistemas.CP;
using Px_Utiles.Models.Sistemas.Contabilidad.Procesos;
using Px_Utiles.Utiles.Cadenas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Px_Controles.Controls.Sistemas.CP
{
    public partial class DataGridViewPrePoliza : System.Windows.Forms.DataGridView
    {
        private List<CuentaPrePol> cuentas;
        public event EventHandler<RowEventArgs> RowInserted;

        public DataGridViewPrePoliza()
        {
            cuentas = new List<CuentaPrePol>(); // En tu caso, esto debe ser llenado desde la base de datos

            this.AllowUserToAddRows = true;
            this.AllowUserToDeleteRows = true;

            this.Tag = cuentas; // Almacenar las cuentas en el Tag del DataGridView para acceder desde la celda personalizada

            InitializeColumns();
        }

        private void InitializeColumns()
        {
            this.Columns.Clear();

            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "EMPRESA", DataPropertyName = "EMPRESA", HeaderText = "Empresa", Visible = false });
            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "EJERCICIO", DataPropertyName = "EJERCICIO", HeaderText = "Ejercicio", Visible = false });
            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "PERIODO", DataPropertyName = "PERIODO", HeaderText = "Periodo", Visible = false });
            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "TIPO_POLIZA", DataPropertyName = "TIPO_POLIZA", HeaderText = "TipoPoliza", Visible = false });
            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "POLIZA_TMP", DataPropertyName = "POLIZA_TMP", HeaderText = "PolizaTmp", Visible = false });
            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "CONSEC", DataPropertyName = "CONSEC", HeaderText = "Consec", Visible = false });

            //for (int i = 0; i <= 5; i++)
            //    this.Columns[1].Visible = false;

            DataGridViewButtonColumn searchButtonColumn = new DataGridViewButtonColumn();
            searchButtonColumn.HeaderText = "🔍";
            searchButtonColumn.Name = "SearchButton";
            searchButtonColumn.Text = "🔍";
            searchButtonColumn.UseColumnTextForButtonValue = true;
            searchButtonColumn.Width = 50;
            this.Columns.Add(searchButtonColumn);

            DataGridViewAutoCompleteColumn autoCompleteColumn = new DataGridViewAutoCompleteColumn
            {
                HeaderText = "Cuenta",
                Name = "Cuenta",
                DataPropertyName = "Cuenta",
                Width = 200,
                DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.Navy, BackColor = Color.FromArgb(249, 252, 255) }
            };
            Columns.Add(autoCompleteColumn);

            //DataGridViewComboBoxColumn comboColumn = new DataGridViewComboBoxColumn();
            //comboColumn.HeaderText = "Cuenta";
            //comboColumn.Name = "Cuenta";
            //comboColumn.DataSource = cuentas;
            //comboColumn.DisplayMember = "DisplayValue"; //"CUENTACONTABLE";
            //comboColumn.ValueMember = "CVE_CUENTA";
            //comboColumn.AutoComplete = true; // Habilitar autocompletar
            //this.Columns.Add(comboColumn);

            //this.Columns.Add("CuentaID", "ID Cuenta");
            //this.Columns.Add("Fecha", "Fecha");
            //this.Columns.Add("Cuentacontable", "Cuenta Contable");

            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "CuentaID", DataPropertyName = "CVE_CUENTA", HeaderText = "CuentaID", Visible = false });
            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cuentacontable", DataPropertyName = "Cuentacontable", HeaderText = "Cuentacontable", Visible = false });
            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "Fecha", DataPropertyName = "FECHA", HeaderText = "Fecha", Visible = false });

            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "Descr", DataPropertyName = "DESCR", HeaderText = "Descripción", ReadOnly = true, Width = 350, DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.Navy, BackColor = Color.FromArgb(249, 252, 255) } });
            //this.Columns.Add("Desc", "Decsripción Cuenta");

            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cargo", DataPropertyName = "CARGO", HeaderText = "Cargo", ValueType = typeof(decimal), DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = Color.FromArgb(255, 249, 249), Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "Abono", DataPropertyName = "ABONO", HeaderText = "Abono", ValueType = typeof(decimal), DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.Green, BackColor = Color.FromArgb(246, 255, 244), Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });

            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "Concepto", DataPropertyName = "CONCEPTO", HeaderText = "Concepto" });
            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "Referencia", DataPropertyName = "REFERENCIA", HeaderText = "Referencia" });
            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "Folio", DataPropertyName = "FOLIO", HeaderText = "Folio", ValueType = typeof(decimal) });
            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tramite", DataPropertyName = "TRAMITE", HeaderText = "Tramite", ValueType = typeof(Int64) });
            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "Pedido", DataPropertyName = "PEDIDO", HeaderText = "Pedido" });
            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "Comprobante", DataPropertyName = "COMPROBANTE", HeaderText = "Comprobante" });
            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "Fondo", DataPropertyName = "FONDO", HeaderText = "Fondo" });
            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "RFC", DataPropertyName = "RFC", HeaderText = "RFC" });
            this.Columns.Add(new DataGridViewTextBoxColumn { Name = "Homoclave", DataPropertyName = "HOMOCLAVE", HeaderText = "Homoclave" });


            //this.Columns.Add("Referencia", "Referencia");
            //this.Columns.Add("Cargo", "Cargo");
            //this.Columns.Add("Abono", "Abono");

            this.BorderStyle = BorderStyle.None;
            this.BackgroundColor = Color.White;


            this.CellClick += ContableDataGridView_CellClick;
            this.EditingControlShowing += ContableDataGridView_EditingControlShowing;

            this.KeyDown += CustomDataGridView_KeyDown;
            //this.CellEndEdit += ContableDataGridView_CellEndEdit;
            //this.CellLeave += ContableDataGridView_CellLeave;
            this.CellValidating += ContableDataGridView_CellValidating;

            this.CellValidating += ContableDataGridView_CellValidating;
            this.CellEndEdit += ContableDataGridView_CellEndEdit;
            this.CellValueChanged += ContableDataGridView_CellValueChanged;
            this.CurrentCellDirtyStateChanged += ContableDataGridView_CurrentCellDirtyStateChanged;
            this.DefaultValuesNeeded += ContableDataGridView_DefaultValuesNeeded;

            this.KeyDown += ContableDataGridView_KeyDown;

            this.CellPainting += new DataGridViewCellPaintingEventHandler(CustomCellPainting);

            this.UserAddedRow += ContableDataGridView_UserAddedRow;
        }


        private void CustomCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == this.Columns["SearchButton"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                // Dibujar el icono del carácter usando una fuente que soporte emojis
                Font font = new Font("Segoe UI Emoji", 12, GraphicsUnit.Pixel);

                // Medir el tamaño del texto
                SizeF textSize = e.Graphics.MeasureString("🔍", font);

                // Calcular la posición para centrar el texto
                float x = e.CellBounds.Left + (e.CellBounds.Width - textSize.Width) / 2;
                float y = e.CellBounds.Top + (e.CellBounds.Height - textSize.Height) / 2;

                Brush oColor = new SolidBrush(Color.FromArgb(106, 47, 66));
                e.Graphics.DrawString("🔍", font, oColor, x, y);
                e.Handled = true;

                //e.PaintBackground(e.CellBounds, true);
                //e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                //// Dibujar el icono del carácter usando una fuente que soporte emojis
                //Brush oColor = new SolidBrush(Color.FromArgb(106,47,66));
                //Font font = new Font("Segoe UI Emoji", 14, GraphicsUnit.Pixel);
                //e.Graphics.DrawString("🔍", font, oColor, e.CellBounds, StringFormat.GenericDefault);
                //e.Handled = true;
            }
        }


        private void CustomDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Insert:
                    AddNewRow();
                    e.Handled = true;  // Evitar el comportamiento por defecto
                    break;

                case Keys.Delete:
                    if (this.SelectedRows.Count > 0)
                    {
                        DeleteSelectedRows();
                        e.Handled = true;  // Evitar el comportamiento por defecto
                    }
                    break;

                // Habilitar la tecla Tab para añadir una nueva fila al final si es que esta en la celda final
                case Keys.Tab:

                    if (this.CurrentRow != null && this.CurrentRow.Index == this.RowCount - 1)
                        AddNewRow();


                    break;

                case Keys.F2:

                    MuestraAyudaCuentas(sender, new DataGridViewCellEventArgs(this.CurrentCell.ColumnIndex, this.CurrentCell.RowIndex));

                    break;

                case Keys.Enter:
                    if (this.CurrentRow != null && this.CurrentRow.Index == this.RowCount - 1)
                    {
                        AddNewRow();
                    }
                    else
                    {
                        MoveToNextCell();
                    }

                    break;
            }

        }

        private void MoveToNextCell()
        {
            try
            {
                if (this.CurrentCell != null)
                {
                    int nextColumnIndex = this.CurrentCell.ColumnIndex + 1;
                    int rowIndex = this.CurrentCell.RowIndex;

                    // Si estamos en la última columna, movemos al inicio de la siguiente fila
                    if (nextColumnIndex >= this.ColumnCount)
                    {
                        nextColumnIndex = 0;
                        rowIndex++;
                    }

                    // Si estamos en la última fila, agregamos una nueva fila
                    if (rowIndex >= this.RowCount)
                    {
                        AddNewRow();
                        rowIndex = this.RowCount - 1; // Mover al índice de la nueva fila
                    }

                    // Verifica que la nueva fila y celda sean visibles
                    int newRowIndex = this.Rows.Count - 1;
                    int newColumnIndex = 0;

                    if (newRowIndex >= 0 && newRowIndex < this.Rows.Count &&
                        newColumnIndex >= 0 && newColumnIndex < this.Columns.Count &&
                        this.Rows[newRowIndex].Visible && this.Columns[newColumnIndex].Visible)
                    {
                        // Establece el foco en la primera celda de la nueva fila
                        this.CurrentCell = this.Rows[newRowIndex].Cells[newColumnIndex];

                        // Inicia la edición de la nueva celda
                        this.BeginEdit(true);
                    }


                    //if (rowIndex < this.RowCount && nextColumnIndex < this.ColumnCount)
                    //{
                    //    this.CurrentCell = this[nextColumnIndex, rowIndex];
                    //}
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }


        private void AddNewRow()
        {
            // Añade una nueva fila al final
            if (!this.AllowUserToAddRows)
            {
                this.AllowUserToAddRows = true;
            }
            var rowIndex = this.Rows.Add();
            this.CurrentCell = this.Rows[rowIndex].Cells["Cuenta"];  // Focalizar la primera celda de la nueva fila
        }

        private void DeleteSelectedRows()
        {
            foreach (DataGridViewRow row in this.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    this.Rows.Remove(row);
                }
            }
        }

        private void CustomDataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row.IsNewRow)
            {
                e.Cancel = true;  // Evitar borrar la fila nueva por defecto
            }
        }


        public void SetCuentasDataSource(List<CuentaPrePol> cuentasDataSource)
        {
            this.cuentas = cuentasDataSource;


            try
            {
                //(this.Columns["Cuenta"] as DataGridViewComboBoxColumn).DataSource = cuentas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        private void ContableDataGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            // establece el valor por defecto en 0 para columnas de tipo decimal
            e.Row.Cells["Cargo"].Value = 0;
            e.Row.Cells["Abono"].Value = 0;

            e.Row.Cells["Folio"].Value = 0;
            e.Row.Cells["Tramite"].Value = 0;


        }
        private int currentRowIndex;
        private int currentColumnIndex;
        private bool keepFocus = false;

        private void ContableDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is System.Windows.Forms.ComboBox comboBox && this.CurrentCell.OwningColumn is DataGridViewAutoCompleteColumn)
            {
                comboBox.Leave += (s, args) =>
                {
                    var selectedText = comboBox.Text;
                    var accountInfo = selectedText.Split(new[] { " - " }, StringSplitOptions.None);
                    if (accountInfo.Length > 1)
                    {

                        //Debug.Print($"Entra aqui {accountInfo[0]} | {accountInfo[1]}...");

                        var cuenta = cuentas.FirstOrDefault(c => c.CUENTACONTABLE == accountInfo[0]);
                        if (cuenta != null)
                        {
                            this.CurrentCell.Value = cuenta.CUENTACONTABLE; // Set only CUENTACONTABLE in the cell
                            var row = this.CurrentRow;
                            row.Cells["CuentaID"].Value = cuenta.CVE_CUENTA;
                            row.Cells["Cuentacontable"].Value = cuenta.CUENTACONTABLE;
                            row.Cells["Descr"].Value = cuenta.DESCR;

                            row.Cells["CONCEPTO"].Value = "";
                            row.Cells["FOLIO"].Value = 0;
                            row.Cells["TRAMITE"].Value = 0;

                            //this.CurrentCell = this.Rows[e.RowIndex].Cells[e.ColumnIndex];
                            //this.CurrentRow = row.RowCount - 1;
                            //this.BeginEdit(true);
                            currentRowIndex = row.Index;
                            currentColumnIndex = this.Columns["Cargo"].Index;

                            // Configura la bandera para mantener el foco
                            keepFocus = true;

                            //this.CurrentCell = row.Cells[currentColumnIndex];
                            //this.BeginEdit(true);
                        }
                    }
                };
            }
            else if (e.Control is TextBox textBox)
            {
                // Remove any previous event handlers
                textBox.KeyPress -= DecimalColumn_KeyPress;

                //if ((this.CurrentCell.OwningColumn.Name == "Cargo" || this.CurrentCell.OwningColumn.Name == "Abono"))
                //{
                // Add the KeyPress event handler to allow only numbers and a single decimal point
                //textBox.KeyPress += DecimalColumn_KeyPress;
                DataGridViewColumn column = this.CurrentCell.OwningColumn;
                if (column.ValueType == typeof(decimal) || column.ValueType == typeof(int) || column.ValueType == typeof(double))
                    textBox.KeyPress += DecimalColumn_KeyPress;

                //}
            }
        }

        private void ContableDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (keepFocus)
            {
                e.Cancel = true; // Cancela el movimiento de la celda
                keepFocus = false; // Resetea la bandera

                // Vuelve a enfocar la celda deseada y comienza la edición
                this.BeginInvoke(new Action(() =>
                {
                    this.CurrentCell = this.Rows[currentRowIndex].Cells[currentColumnIndex];
                    this.BeginEdit(true);
                }));
            }
        }


        private void ContableDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            ValidateAndToggleCells(e.RowIndex);

            //// Aquí revisas si la edición finalizó y si la celda debería enfocarse nuevamente

            //if (e.RowIndex == currentRowIndex && e.ColumnIndex == currentColumnIndex)
            //{
            //    // Re-establecer el foco en la celda deseada después de la edición
            //    this.CurrentCell = this.Rows[currentRowIndex].Cells[currentColumnIndex];
            //    this.BeginEdit(true);
            //}
            ////if (e.RowIndex == currentRowIndex)
            ////{
            ////    this.CurrentCell = this.Rows[currentRowIndex].Cells[currentColumnIndex];
            ////    this.BeginEdit(true);
            ////}
        }
        private void ContableDataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == currentRowIndex && e.ColumnIndex == currentColumnIndex)
            {
                // Re-establecer el foco en la celda deseada antes de dejar la celda
                this.CurrentCell = this.Rows[currentRowIndex].Cells[currentColumnIndex];
                this.BeginEdit(true);
            }
        }

        private void ContableDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.Columns[e.ColumnIndex].Name == "Cargo" || this.Columns[e.ColumnIndex].Name == "Abono")
            {
                ValidateAndToggleCells(e.RowIndex);
            }
        }

        private void ContableDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.IsCurrentCellDirty && (this.CurrentCell.OwningColumn.Name == "Cargo" || this.CurrentCell.OwningColumn.Name == "Abono"))
            {
                this.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }


        private void ValidateAndToggleCells(int rowIndex)
        {
            var row = this.Rows[rowIndex];
            var cargoCell = row.Cells["Cargo"];
            var abonoCell = row.Cells["Abono"];

            // Convertir los valores de las celdas a decimal, manejando los valores nulos.
            decimal cargoValue;
            decimal abonoValue;

            // Intentar convertir el valor del cargo
            if (!decimal.TryParse((cargoCell.Value ?? 0).ToString(), out cargoValue))
                cargoValue = 0; // Valor por defecto si la conversión falla

            // Intentar convertir el valor del abono
            if (!decimal.TryParse((abonoCell.Value ?? 0).ToString(), out abonoValue))
                abonoValue = 0; // Valor por defecto si la conversión falla

            // Validar y bloquear/desbloquear las celdas basándonos en los valores
            abonoCell.ReadOnly = cargoValue > 0;
            cargoCell.ReadOnly = abonoValue > 0;
        }

        //private void ValidateAndToggleCells(int rowIndex)
        //{
        //    var row = this.Rows[rowIndex];
        //    var cargoCell = row.Cells["Cargo"];
        //    var abonoCell = row.Cells["Abono"];

        //    decimal cargoValue = Convert.ToDecimal(cargoCell.Value ?? 0);
        //    decimal abonoValue = Convert.ToDecimal(abonoCell.Value ?? 0);

        //    if (cargoValue > 0)
        //    {
        //        abonoCell.ReadOnly = true;
        //    }
        //    else
        //    {
        //        abonoCell.ReadOnly = false;
        //    }

        //    if (abonoValue > 0)
        //    {
        //        cargoCell.ReadOnly = true;
        //    }
        //    else
        //    {
        //        cargoCell.ReadOnly = false;
        //    }
        //}


        private void ContableDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == Columns["SearchButton"].Index && e.RowIndex >= 0)
            {

                // ubicación de la ventana de búsqueda se extrae l ubicación de la celda y del grid.
                var cellRect = this.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                Point cellLocation = this.PointToScreen(new Point(cellRect.X + Location.X, cellRect.Y + Location.Y + cellRect.Height));

                //var searchForm = new SearchForm(cuentas);
                var searchForm = new SearchForm(cuentas, cellLocation.X, cellLocation.Y);
                var result = searchForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    var selectedCuenta = searchForm.SelectedCuenta;
                    Rows[e.RowIndex].Cells["Cuenta"].Value = selectedCuenta.CUENTACONTABLE;
                    Rows[e.RowIndex].Cells["CuentaID"].Value = selectedCuenta.CVE_CUENTA;
                    Rows[e.RowIndex].Cells["Cuentacontable"].Value = selectedCuenta.CUENTACONTABLE;
                    Rows[e.RowIndex].Cells["Descr"].Value = selectedCuenta.DESCR;

                    Rows[e.RowIndex].Cells["CONCEPTO"].Value = "";
                    Rows[e.RowIndex].Cells["FOLIO"].Value = 0;
                    Rows[e.RowIndex].Cells["TRAMITE"].Value = 0;


                    //this.CurrentCell = this.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    this.CurrentCell = this.Rows[e.RowIndex].Cells[12];   // Cargo
                    this.BeginEdit(true);
                }
            }
        }

        private void MuestraAyudaCuentas(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == Columns["SearchButton"].Index && e.RowIndex >= 0)
            //{

            // ubicación de la ventana de búsqueda se extrae l ubicación de la celda y del grid.
            var cellRect = this.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
            Point cellLocation = this.PointToScreen(new Point(cellRect.X + Location.X, cellRect.Y + Location.Y + cellRect.Height));

            //var searchForm = new SearchForm(cuentas);
            var searchForm = new SearchForm(cuentas, cellLocation.X, cellLocation.Y);
            var result = searchForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                var selectedCuenta = searchForm.SelectedCuenta;
                Rows[e.RowIndex].Cells["Cuenta"].Value = selectedCuenta.CUENTACONTABLE;
                Rows[e.RowIndex].Cells["CuentaID"].Value = selectedCuenta.CVE_CUENTA;
                Rows[e.RowIndex].Cells["Cuentacontable"].Value = selectedCuenta.CUENTACONTABLE;
                Rows[e.RowIndex].Cells["Desc"].Value = selectedCuenta.DESCR;

                Rows[e.RowIndex].Cells["CONCEPTO"].Value = "";
                Rows[e.RowIndex].Cells["FOLIO"].Value = 0;
                Rows[e.RowIndex].Cells["TRAMITE"].Value = 0;


                //this.CurrentCell = this.Rows[e.RowIndex].Cells[e.ColumnIndex];
                this.CurrentCell = this.Rows[e.RowIndex].Cells[12];   // Cargo
                this.BeginEdit(true);

            }
            //}
        }

        private void ContableDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // Detecta si la tecla presionada es Tab
            if (e.KeyCode == Keys.Tab)
            {
                // Detecta si estamos en la última celda de la última fila
                DataGridViewCell currentCell = this.CurrentCell;
                if (currentCell != null &&
                    currentCell.RowIndex == this.Rows.Count - 1 &&
                    currentCell.ColumnIndex == this.Columns.Count - 1)
                {
                    // Cancela el evento para que no se propague el movimiento del tabulador
                    e.Handled = true;

                    // Añade una nueva fila al DataGridView
                    this.Rows.Add();

                    // Verifica que la nueva fila y celda sean visibles
                    int newRowIndex = this.Rows.Count - 1;
                    int newColumnIndex = 0;

                    if (newRowIndex >= 0 && newRowIndex < this.Rows.Count &&
                        newColumnIndex >= 0 && newColumnIndex < this.Columns.Count &&
                        this.Rows[newRowIndex].Visible && this.Columns[newColumnIndex].Visible)
                    {
                        // Establece el foco en la primera celda de la nueva fila
                        this.CurrentCell = this.Rows[newRowIndex].Cells[newColumnIndex];

                        // Inicia la edición de la nueva celda
                        this.BeginEdit(true);
                    }
                }
            }
        }


        private void DecimalColumn_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;


            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && (textBox.Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        //private void ContableDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    if (e.Control is TextBox textBox && this.CurrentCell.OwningColumn is DataGridViewAutoCompleteColumn)
        //    {
        //        textBox.Leave += (s, args) =>
        //        {
        //            var accountInfo = textBox.Text.Split(new[] { " - " }, StringSplitOptions.None);
        //            if (accountInfo.Length > 1)
        //            {
        //                var cuenta = cuentas.FirstOrDefault(c => c.CUENTACONTABLE == accountInfo[0]);
        //                if (cuenta != null)
        //                {
        //                    this.CurrentRow.Cells["CuentaID"].Value = cuenta.CVE_CUENTA;
        //                    this.CurrentRow.Cells["Cuentacontable"].Value = cuenta.CUENTACONTABLE;
        //                    this.CurrentRow.Cells["Desc"].Value = cuenta.DESCR;

        //                    //this.CurrentRow.Cells["Cuentacontable"].Value = cuenta.CUENTACONTABLE;
        //                    //this.CurrentRow.Cells["Concepto"].Value = cuenta.DESCR;
        //                }
        //            }
        //        };
        //    }
        //}

        //private void ContableDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == this.Columns["SearchButton"].Index && e.RowIndex >= 0)
        //    {
        //        // Muestra la forma de búsqueda de cuentas (implementar la forma y su lógica)
        //        var searchForm = new SearchForm(cuentas);
        //        var result = searchForm.ShowDialog();

        //        if (result == DialogResult.OK)
        //        {
        //            var selectedCuenta = searchForm.SelectedCuenta;
        //            this.Rows[e.RowIndex].Cells["CuentaID"].Value = selectedCuenta.CVE_CUENTA;
        //            this.Rows[e.RowIndex].Cells["Cuentacontable"].Value = selectedCuenta.CUENTACONTABLE;
        //            this.Rows[e.RowIndex].Cells["Desc"].Value = selectedCuenta.DESCR;
        //        }
        //    }
        //}

        private void ContableDataGridView_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            RowInserted?.Invoke(this, new RowEventArgs(e.Row));
        }

        public AutoCompleteStringCollection GetAutoCompleteSourceItems()
        {
            var autoCompleteSource = new AutoCompleteStringCollection();
            foreach (var cuenta in cuentas)
            {
                autoCompleteSource.Add($"{cuenta.CUENTACONTABLE} - {cuenta.DESCR}");
            }
            return autoCompleteSource;
        }

        public List<CuentaPrePol> GetCuentas()
        {
            return cuentas;
        }





        public new List<eMOVTOS_POLIZA_TMP> DataSource
        {
            get
            {
                List<eMOVTOS_POLIZA_TMP> rowsData = new List<eMOVTOS_POLIZA_TMP>();

                // Itera a través de cada fila en el DataGridView
                foreach (DataGridViewRow row in this.Rows)
                {
                    // Verifica que la fila no sea una fila de nuevo
                    if (!row.IsNewRow)
                    {
                        if (row.Cells["CuentaID"].Value != null)
                        {
                            eMOVTOS_POLIZA_TMP rowData = new eMOVTOS_POLIZA_TMP
                            {
                                CVE_CUENTA = Convert.ToInt64(row.Cells["CuentaID"].Value ?? 0),
                                DESCR = row.Cells["DESCR"].Value?.ToString() ?? "",
                                CONCEPTO = object.ReferenceEquals(row.Cells["CONCEPTO"].Value, null) ? "" : row.Cells["CONCEPTO"].Value.ToString(),
                                REFERENCIA = object.ReferenceEquals(row.Cells["REFERENCIA"].Value, null) ? "" : row.Cells["REFERENCIA"].Value.ToString(),
                                FECHA = Convert.ToDateTime(row.Cells["FECHA"].Value ?? DateTime.Today),
                                CARGO = Convert.ToDecimal(row.Cells["CARGO"].Value ?? 0),
                                ABONO = Convert.ToDecimal(row.Cells["ABONO"].Value ?? 0),
                                FOLIO = Convert.ToDecimal((row.Cells["FOLIO"].Value.ToString().IsDecimal() ? row.Cells["FOLIO"].Value : 0)),
                                TRAMITE = Convert.ToInt64((row.Cells["TRAMITE"].Value.ToString().IsNumber() ? row.Cells["TRAMITE"].Value : 0)),
                                PEDIDO = object.ReferenceEquals(row.Cells["PEDIDO"].Value, null) ? "" : row.Cells["PEDIDO"].Value.ToString(),
                                COMPROBANTE = object.ReferenceEquals(row.Cells["COMPROBANTE"].Value, null) ? "" : row.Cells["COMPROBANTE"].Value.ToString(),
                                FONDO = object.ReferenceEquals(row.Cells["FONDO"].Value, null) ? "" : row.Cells["FONDO"].Value.ToString(),
                                RFC = object.ReferenceEquals(row.Cells["RFC"].Value, null) ? "" : row.Cells["RFC"].Value.ToString(),
                                HOMOCLAVE = object.ReferenceEquals(row.Cells["HOMOCLAVE"].Value, null) ? "" : row.Cells["HOMOCLAVE"].Value.ToString()

                                ////EMPRESA = Convert.ToInt64(row.Cells["EMPRESA"].Value ?? 0),
                                ////EJERCICIO = Convert.ToInt64(row.Cells["EJERCICIO"].Value ?? 0),
                                ////PERIODO = Convert.ToInt64(row.Cells["PERIODO"].Value ?? 0),
                                ////TIPO_POLIZA = row.Cells["TIPO_POLIZA"].Value?.ToString() ?? "",
                                ////POLIZA_TMP = Convert.ToInt64(row.Cells["POLIZA_TMP"].Value ?? 0),
                                ////CONSEC = Convert.ToInt64(row.Cells["CONSEC"].Value ?? 0),
                                //CVE_CUENTA = Convert.ToInt64(row.Cells["CuentaID"].Value ?? 0),
                                ////DESCR = row.Cells["DESCR"].Value?.ToString() ?? "",
                                //CONCEPTO = object.ReferenceEquals(row.Cells["CONCEPTO"].Value, null) ? "": row.Cells["CONCEPTO"].Value.ToString(), //row.Cells["CONCEPTO"].Value?.ToString() ?? "",
                                //REFERENCIA = object.ReferenceEquals(row.Cells["REFERENCIA"].Value, null) ? "": row.Cells["REFERENCIA"].Value.ToString(),
                                ////FECHA = Convert.ToDateTime(row.Cells["FECHA"].Value ?? DateTime.Today),
                                //CARGO = Convert.ToDecimal(row.Cells["CARGO"].Value ?? 0),
                                //ABONO = Convert.ToDecimal(row.Cells["ABONO"].Value ?? 0),
                                //FOLIO = Convert.ToDecimal(row.Cells["FOLIO"].Value ?? 0),
                                //TRAMITE = Convert.ToInt64(row.Cells["TRAMITE"].Value ?? 0),
                                //PEDIDO = object.ReferenceEquals(row.Cells["PEDIDO"].Value, null) ? "": row.Cells["PEDIDO"].Value.ToString(),                        //row.Cells["PEDIDO"].Value?.ToString() ?? "",
                                //COMPROBANTE = object.ReferenceEquals(row.Cells["COMPROBANTE"].Value, null) ? "" : row.Cells["COMPROBANTE"].Value.ToString(),        //row.Cells["COMPROBANTE"].Value?.ToString() ?? "",
                                //FONDO = object.ReferenceEquals(row.Cells["FONDO"].Value, null) ? "" : row.Cells["FONDO"].Value.ToString(),                          //row.Cells["FONDO"].Value?.ToString() ?? "",
                                //RFC = object.ReferenceEquals(row.Cells["RFC"].Value, null) ? "" : row.Cells["RFC"].Value.ToString(),                                //row.Cells["RFC"].Value?.ToString() ?? "",
                                //HOMOCLAVE = object.ReferenceEquals(row.Cells["HOMOCLAVE"].Value, null) ? "" : row.Cells["HOMOCLAVE"].Value.ToString()              //row.Cells["HOMOCLAVE"].Value?.ToString() ?? ""
                            };
                            // Agrega el objeto rowData a la lista de resultados
                            rowsData.Add(rowData);
                        }
                    }
                }

                // Devuelve la lista de eMOVTOS_POLIZA_TMP
                return rowsData;
            }
            set
            {
                this.Rows.Clear();


                foreach (var item in value)
                {
                    this.Rows.Add(
                        item.EMPRESA,
                        item.EJERCICIO,
                        item.PERIODO,
                        item.TIPO_POLIZA,
                        item.POLIZA_TMP,
                        item.CONSEC,
                        item.CVE_CUENTA,
                        item.DESCR,
                        item.CONCEPTO,
                        item.REFERENCIA,
                        item.FECHA,
                        item.CARGO,
                        item.ABONO,
                        item.FOLIO,
                        item.TRAMITE,
                        item.PEDIDO,
                        item.COMPROBANTE,
                        item.FONDO,
                        item.RFC,
                        item.HOMOCLAVE
                    );
                }
            }
        }

    }

}


public class RowEventArgs : EventArgs
{
    public DataGridViewRow Row { get; }

    public RowEventArgs(DataGridViewRow row)
    {
        this.Row = row;
    }
}


// AutoComplete cell
public class DataGridViewAutoCompleteTextBoxCell : DataGridViewTextBoxCell
{
    public List<CuentaPrePol> CuentasDataSource { get; set; }

    public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
    {
        base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
        var ctl = DataGridView.EditingControl as DataGridViewAutoCompleteEditingControl;

        if (ctl != null)
        {
            var dgv = DataGridView as DataGridViewPrePoliza;
            CuentasDataSource = dgv?.GetCuentas();

            ctl.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ctl.AutoCompleteSource = AutoCompleteSource.CustomSource;
            ctl.AutoCompleteCustomSource = GetAutoCompleteSourceItems(CuentasDataSource);

            ctl.DropDownWidth = 550;  // Ajusta el tamaño de la lista desplegable

            ctl.SelectedIndexChanged += (s, e) =>
            {
                //var selectedText = ctl.Text;
                //var accountInfo = selectedText.Split(new[] { " - " }, StringSplitOptions.None);
                //if (accountInfo.Length > 1)
                //{
                //    var cuenta = CuentasDataSource.FirstOrDefault(c => c.CUENTACONTABLE == accountInfo[0] || c.DESCR.ToLower().Contains(accountInfo[0]) );
                //    if (cuenta != null)
                //    {
                //        DataGridView.CurrentCell.Value = cuenta.CUENTACONTABLE; // Set only CUENTACONTABLE in the cell
                //        var row = DataGridView.CurrentRow;
                //        row.Cells["CuentaID"].Value = cuenta.CVE_CUENTA;
                //        row.Cells["Cuentacontable"].Value = cuenta.CUENTACONTABLE;
                //        row.Cells["Desc"].Value = cuenta.DESCR;

                //        Console.WriteLine("Entra aqui ...");
                //    }
                //}
            };
        }
    }

    private AutoCompleteStringCollection GetAutoCompleteSourceItems(List<CuentaPrePol> cuentas)
    {
        var autoCompleteSource = new AutoCompleteStringCollection();
        foreach (var cuenta in cuentas)
            autoCompleteSource.Add($"{cuenta.CUENTACONTABLE} - {cuenta.DESCR}");

        return autoCompleteSource;
    }

    public override Type EditType => typeof(DataGridViewAutoCompleteEditingControl);
}

public class DataGridViewAutoCompleteEditingControl : System.Windows.Forms.ComboBox, IDataGridViewEditingControl
{
    public System.Windows.Forms.DataGridView EditingControlDataGridView { get; set; }
    public object EditingControlFormattedValue
    {
        get => Text;
        set => Text = value?.ToString();
    }

    public DataGridViewAutoCompleteEditingControl()
    {
        this.DropDownWidth = 550;
        //Width = 350;
    }

    public int EditingControlRowIndex { get; set; }
    public bool EditingControlValueChanged { get; set; }
    public Cursor EditingPanelCursor => Cursors.IBeam;
    public bool RepositionEditingControlOnValueChange => false;

    public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
    {
        return Text;
    }

    public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
    {
        Font = new Font(dataGridViewCellStyle.Font, FontStyle.Bold);
        ForeColor = Color.FromArgb(106, 46, 66); // dataGridViewCellStyle.ForeColor;
        BackColor = dataGridViewCellStyle.BackColor;
        //Width = 350;
    }

    public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
    {
        return key == Keys.Left || key == Keys.Right || key == Keys.Up || key == Keys.Down || key == Keys.Home ||
               key == Keys.End || key == Keys.PageDown || key == Keys.PageUp;
    }

    public void PrepareEditingControlForEdit(bool selectAll)
    {
        if (selectAll)
        {
            SelectAll();
        }
    }

    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
        if (EditingControlDataGridView != null && EditingControlDataGridView.IsCurrentCellInEditMode)
        {
            EditingControlValueChanged = true;
            EditingControlDataGridView.NotifyCurrentCellDirty(true);
        }
    }
}

// AutoComplete column
public class DataGridViewAutoCompleteColumn : DataGridViewColumn
{
    public DataGridViewAutoCompleteColumn()
        : base(new DataGridViewAutoCompleteTextBoxCell())
    {
        //Width = 250;
    }

    public override object Clone()
    {
        return base.Clone();
    }
}







