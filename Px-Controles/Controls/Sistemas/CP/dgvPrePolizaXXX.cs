using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Px_Controles.Controls.Sistemas.CP.dgvPrePolizaXXX;


namespace Px_Controles.Controls.Sistemas.CP
{
    public class dgvPrePolizaXXX : System.Windows.Forms.DataGridView
    {

        public class Cuenta
        {
            public decimal CVE_CUENTA { get; set; } = 0;
            public string DESCR { get; set; } = "";
            public string CUENTACONTABLE { get; set; } = "";
        }

        private List<Cuenta> _Cuentas; // List of accounts (datasource for combo box)
        private AutoCompleteTextBox _autoCompleteTextBox;


        public dgvPrePolizaXXX()
        {

            this.CellBeginEdit += CustomDataGridView_CellBeginEdit;
            this.CellEndEdit += CustomDataGridView_CellEndEdit;
            this.CellValidating += CustomDataGridView_CellValidating;
            this.KeyDown += CustomDataGridView_KeyDown;
            this.CellClick += CustomDataGridView_CellClick;

            InitializeGrid();
        }

        private void InitializeGrid()
        {
            this.Columns.Add("EMPRESA", "Empresa");
            this.Columns.Add("EJERCICIO", "Ejercicio");
            this.Columns.Add("PERIODO", "Periodo");
            this.Columns.Add("TIPO_POLIZA", "Tipo Poliza");
            this.Columns.Add("POLIZA_TMP", "Poliza Tmp");
            this.Columns.Add("CONSEC", "Consec");

            //var searchButtonColumn = new DataGridViewButtonColumn
            //{
            //    Name = "SearchButton",
            //    HeaderText = "Buscar",
            //    Text = "Buscar",
            //    UseColumnTextForButtonValue = true
            //};
            //this.Columns.Add(searchButtonColumn);

            this.Columns.Add("CVE_CUENTA", "Cuenta");
            var cuentaColumn = new DataGridViewTextBoxColumn { Name = "CUENTACONTABLE", HeaderText = "Clave Cuenta" };
            this.Columns.Add(cuentaColumn);
            var descrColumn = new DataGridViewTextBoxColumn { Name = "DESCR", HeaderText = "Descripcion" };
            this.Columns.Add(descrColumn);
            //this.Columns.Add("DESCR", "Descripcion");
            this.Columns.Add("CONCEPTO", "Concepto");
            this.Columns.Add("REFERENCIA", "Referencia");
            this.Columns.Add("FECHA", "Fecha");
            this.Columns.Add("CARGO", "Cargo");
            this.Columns.Add("ABONO", "Abono");
            this.Columns.Add("FOLIO", "Folio");
            this.Columns.Add("TRAMITE", "Tramite");
            this.Columns.Add("PEDIDO", "Pedido");
            this.Columns.Add("COMPROBANTE", "Comprobante");
            this.Columns.Add("FONDO", "Fondo");
            this.Columns.Add("RFC", "RFC");
            this.Columns.Add("HOMOCLAVE", "Homoclave");


            AddNewRow();
        }

        private void AddNewRow()
        {
            this.Rows.Add();
        }

        public void SetCuentas(List<Cuenta> cuentas)
        {
            _Cuentas = cuentas;
        }

        private void CustomDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try 
            {
                if (e.ColumnIndex == this.Columns["SearchButton"].Index && e.RowIndex >= 0)
                {
                    ShowAutoComplete(e.RowIndex, this.Columns["CVE_CUENTA"].Index);
                }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
            }
        }

        private void ShowAutoComplete(int rowIndex, int columnIndex)
        {
            var cellRect = this.GetCellDisplayRectangle(columnIndex, rowIndex, true);

            if (_autoCompleteTextBox != null)
            {
                this.Controls.Remove(_autoCompleteTextBox);
            }

            _autoCompleteTextBox = new AutoCompleteTextBox(_Cuentas);
            _autoCompleteTextBox.SetParent(this);
            _autoCompleteTextBox.Bounds = cellRect;
            _autoCompleteTextBox.Text = this.Rows[rowIndex].Cells[columnIndex].Value?.ToString() ?? string.Empty;
            _autoCompleteTextBox.CuentaSelected += (s, ev) =>
            {
                this.Rows[rowIndex].Cells["CVE_CUENTA"].Value = ev.CVE_CUENTA;
                this.Rows[rowIndex].Cells["CUENTACONTABLE"].Value = ev.CUENTACONTABLE;
                this.Rows[rowIndex].Cells["DESCR"].Value = ev.DESCR;

                //this.Rows[rowIndex].Cells["CVE_CUENTA"].Value = ev.CVE_CUENTA;
                //this.Rows[rowIndex].Cells["DESCR"].Value = ev.DESCR;
                this.EndEdit(); // Ensure cell edit ends after selection
            };

            this.Controls.Add(_autoCompleteTextBox);
            _autoCompleteTextBox.BringToFront();
            _autoCompleteTextBox.Focus();
        }


        private void CustomDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

            if (e.ColumnIndex == this.Columns["CUENTACONTABLE"].Index)
            {
                ShowAutoComplete(e.RowIndex, this.Columns["CUENTACONTABLE"].Index);
            }

            //if (e.ColumnIndex == this.Columns["CUENTACONTABLE"].Index)
            //{
            //    var cellRect = this.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

            //    if (_autoCompleteTextBox != null)
            //        this.Controls.Remove(_autoCompleteTextBox);

            //    _autoCompleteTextBox = new AutoCompleteTextBox(_Cuentas);
            //    _autoCompleteTextBox.SetParent(this); // Ensure parent context for suggestion list
            //    _autoCompleteTextBox.Bounds = cellRect;
            //    _autoCompleteTextBox.Text = this.CurrentCell.Value?.ToString() ?? string.Empty;

            //    _autoCompleteTextBox.CuentaSelected += (s, ev) =>
            //    {
            //        this.Rows[e.RowIndex].Cells["CVE_CUENTA"].Value = ev.CVE_CUENTA;
            //        this.Rows[e.RowIndex].Cells["CUENTACONTABLE"].Value = ev.CUENTACONTABLE;
            //        this.Rows[e.RowIndex].Cells["DESCR"].Value = ev.DESCR;
            //        this.EndEdit(); // Ensure cell edit ends after selection
            //    };


            //    // Attach a handler to remove the AutoCompleteTextBox when it loses focus
            //    _autoCompleteTextBox.LostFocus += (s, ev) =>
            //    {
            //        if (!this.ContainsFocus)
            //        {
            //            this.CurrentCell.Value = _autoCompleteTextBox.Text.Split('-')[0].Trim();
            //            this.EndEdit();
            //            this.Controls.Remove(_autoCompleteTextBox);
            //            _autoCompleteTextBox = null;
            //        }
            //    };

            //    //_autoCompleteTextBox.LostFocus += (s, ev) =>
            //    //{
            //    //    if (_autoCompleteTextBox != null && !this.ContainsFocus) // Modified condition to only hide when focus is completely lost
            //    //    {
            //    //        this.CurrentCell.Value = _autoCompleteTextBox.Text.Split('-')[0].Trim();
            //    //        _autoCompleteTextBox.Visible = false;
            //    //    }
            //    //};



            //    //{
            //    //    Bounds = cellRect,
            //    //    Text = this.CurrentCell.Value?.ToString() ?? string.Empty
            //    //};
            //    //_autoCompleteTextBox.LostFocus += (s, ev) =>
            //    //{
            //    //    if (_autoCompleteTextBox != null && !_autoCompleteTextBox.Focused)
            //    //    {
            //    //        this.CurrentCell.Value = _autoCompleteTextBox.Text.Split('-')[0].Trim();
            //    //        _autoCompleteTextBox.Visible = false;
            //    //    }
            //    //};
            //    this.Controls.Add(_autoCompleteTextBox);
            //    _autoCompleteTextBox.BringToFront();
            //    _autoCompleteTextBox.Focus();
            //}
        }


        //private void CustomDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        //{
        //    if (e.ColumnIndex == this.Columns["CVE_CUENTA"].Index)
        //    {
        //        if (_autoCompleteTextBox != null)
        //        {
        //            this.Controls.Remove(_autoCompleteTextBox);
        //        }

        //        _autoCompleteTextBox = new AutoCompleteTextBox(_Cuentas)
        //        {
        //            Bounds = this.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true),
        //            Text = this.CurrentCell.Value?.ToString() ?? string.Empty
        //        };
        //        _autoCompleteTextBox.LostFocus += (s, ev) =>
        //        {
        //            if (_autoCompleteTextBox != null)
        //            {
        //                this.CurrentCell.Value = _autoCompleteTextBox.Text.Split('-')[0].Trim();
        //                _autoCompleteTextBox.Visible = false;
        //            }
        //        };
        //        this.Controls.Add(_autoCompleteTextBox);
        //        _autoCompleteTextBox.BringToFront();
        //        _autoCompleteTextBox.Focus();
        //    }
        //}

        private void CustomDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.Columns["CUENTACONTABLE"].Index && _autoCompleteTextBox != null)
            {
                this.Controls.Remove(_autoCompleteTextBox);
                _autoCompleteTextBox = null;
            }
        }

        private void CustomDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == this.Columns["CUENTACONTABLE"].Index)
            {
                var value = e.FormattedValue.ToString();

                if (string.IsNullOrEmpty(value))
                    return;

                var cuenta = _Cuentas.FirstOrDefault(c => c.CUENTACONTABLE.ToString() == value || c.DESCR == value);

                if (cuenta != null)
                {
                    this.Rows[e.RowIndex].Cells["CVE_CUENTA"].Value = cuenta.CVE_CUENTA;
                    this.Rows[e.RowIndex].Cells["CUENTACONTABLE"].Value = cuenta.CUENTACONTABLE;
                    this.Rows[e.RowIndex].Cells["DESCR"].Value = cuenta.DESCR;
                }
                else
                {
                    MessageBox.Show("Cuenta no encontrada.");
                    e.Cancel = true;
                }
            }
        }

        private void CustomDataGridView_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    var rowIndex = this.CurrentCell.RowIndex;
                    var columnIndex = this.CurrentCell.ColumnIndex;

                    if (rowIndex == this.Rows.Count - 1 && columnIndex == this.Columns["HOMOCLAVE"].Index)
                    {
                        AddNewRow();
                    }
                    break;
                case Keys.Delete:
                    if (this.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow row in this.SelectedRows)
                        {
                            if (!row.IsNewRow)
                            {
                                this.Rows.Remove(row);
                            }
                        }
                    }
                    break;
                case Keys.Insert:
                    AddNewRow();
                    break;
            }

        }
    }



    public class AutoCompleteTextBox : TextBox
    {
        private List<dgvPrePolizaXXX.Cuenta> _cuentas;
        private ListBox _suggestionList;

        public event EventHandler<CuentaSelectedEventArgs> CuentaSelected;

        public AutoCompleteTextBox(List<dgvPrePolizaXXX.Cuenta> cuentas)
        {
            _cuentas = cuentas;
            _suggestionList = new ListBox();
            _suggestionList.Visible = false;
            _suggestionList.Click += SuggestionList_Click;

            this.TextChanged += AutoCompleteTextBox_TextChanged;
            this.LostFocus += AutoCompleteTextBox_LostFocus;
            this.KeyDown += AutoCompleteTextBox_KeyDown;

            _suggestionList.LostFocus += (s, e) => _suggestionList.Visible = false;
        }

        public void SetParent(Control parent)
        {
            parent.Controls.Add(_suggestionList);
            _suggestionList.BringToFront();
            _suggestionList.Visible = false;
        }

        private void AutoCompleteTextBox_TextChanged(object sender, EventArgs e)
        {
            var searchText = this.Text;
            var suggestions = _cuentas
                .Where(c => c.CUENTACONTABLE.ToString().Trim().ToLower().Contains(searchText.ToLower()) || c.DESCR.ToString().ToLower().Contains(searchText.ToLower()))
                .ToList();

            if (suggestions.Any())
            {
                _suggestionList.Items.Clear();
                foreach (var suggestion in suggestions)
                {
                    _suggestionList.Items.Add($"{suggestion.CUENTACONTABLE} - {suggestion.DESCR}");
                }

                _suggestionList.Location = new Point(this.Left, this.Bottom);
                _suggestionList.Size = new Size(350, 250);
                _suggestionList.Visible = true;
                _suggestionList.BringToFront();
            }
            else
            {
                _suggestionList.Visible = false;
            }
        }

        private void SuggestionList_Click(object sender, EventArgs e)
        {
            if (_suggestionList.SelectedItem != null)
            {
                var selectedText = _suggestionList.SelectedItem.ToString();
                var parts = selectedText.Split('-');
                if (parts.Length == 2)
                {
                    var cuenta = parts[0].Trim();
                    var descr = parts[1].Trim();

                    this.Text = cuenta;
                    _suggestionList.Visible = false;

                    OnCuentaSelected(new CuentaSelectedEventArgs
                    {
                        //CVE_CUENTA = cuenta,
                        CUENTACONTABLE = cuenta,
                        DESCR = descr
                    });
                }
            }
        }

        private void AutoCompleteTextBox_LostFocus(object sender, EventArgs e)
        {
            if (!_suggestionList.Focused)
            {
                _suggestionList.Visible = false;
            }
        }

        protected virtual void OnCuentaSelected(CuentaSelectedEventArgs e)
        {
            CuentaSelected?.Invoke(this, e);
        }

        private void AutoCompleteTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (_suggestionList.Visible)
            {
                if (e.KeyCode == Keys.Down && _suggestionList.SelectedIndex < _suggestionList.Items.Count - 1)
                {
                    _suggestionList.SelectedIndex++;
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Up && _suggestionList.SelectedIndex > 0)
                {
                    _suggestionList.SelectedIndex--;
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    SelectItemFromListBox();
                    e.Handled = true;
                }
            }
        }

        private void SelectItemFromListBox()
        {
            if (_suggestionList.SelectedItem != null)
            {
                var selectedText = _suggestionList.SelectedItem.ToString();
                var parts = selectedText.Split('-');
                if (parts.Length == 2)
                {
                    var cuenta = parts[0].Trim();
                    var descr = parts[1].Trim();

                    this.Text = cuenta;
                    _suggestionList.Visible = false;

                    OnCuentaSelected(new CuentaSelectedEventArgs
                    {
                        CVE_CUENTA = cuenta,
                        DESCR = descr
                    });
                }
            }
        }


    }


    public class CuentaSelectedEventArgs : EventArgs
    {
        public string CVE_CUENTA { get; set; }
        public string CUENTACONTABLE { get; set; }
        public string DESCR { get; set; }
    }

}



//public class AutoCompleteTextBox : TextBox
//{
//    private List<dgvPrePoliza.Cuenta> _cuentas;
//    private ListBox _suggestionList;

//    public AutoCompleteTextBox(List<dgvPrePoliza.Cuenta> cuentas)
//    {
//        _cuentas = cuentas;
//        _suggestionList = new ListBox();
//        _suggestionList.Visible = false;
//        _suggestionList.Click += SuggestionList_Click;

//        this.TextChanged += AutoCompleteTextBox_TextChanged;

//        this.GotFocus += (s, e) => _suggestionList.Visible = true;
//        this.LostFocus += (s, e) =>
//        {
//            // If focus is lost to the ListBox, don't hide it
//            if (!_suggestionList.Focused)
//                _suggestionList.Visible = false;
//        };
//        _suggestionList.LostFocus += (s, e) => _suggestionList.Visible = false;
//    }

//    public void SetParent(Control parent)
//    {
//        parent.Controls.Add(_suggestionList);
//        _suggestionList.BringToFront();
//    }

//    private void AutoCompleteTextBox_TextChanged(object sender, EventArgs e)
//    {
//        var searchText = this.Text;
//        var suggestions = _cuentas
//            .Where(c => c.CUENTACONTABLE.ToString().Trim().ToLower().Contains(searchText.ToLower()) || c.DESCR.ToString().ToLower().Contains(searchText.ToLower()))
//            .ToList();

//        if (suggestions.Any())
//        {
//            _suggestionList.Items.Clear();
//            foreach (var suggestion in suggestions)
//            {
//                _suggestionList.Items.Add($"{suggestion.CUENTACONTABLE} - {suggestion.DESCR}");
//            }

//            _suggestionList.Location = new Point(this.Left, this.Bottom);
//            _suggestionList.Size = new Size(350, 250);
//            //_suggestionList.Size = new Size(this.Width, 100);
//            _suggestionList.Visible = true;
//            _suggestionList.BringToFront();
//        }
//        else
//        {
//            _suggestionList.Visible = false;
//        }
//    }

//    private void SuggestionList_Click(object sender, EventArgs e)
//    {
//        if (_suggestionList.SelectedItem != null)
//        {
//            var selectedText = _suggestionList.SelectedItem.ToString();
//            this.Text = selectedText.Split('-')[0].Trim();
//            _suggestionList.Visible = false;
//        }
//    }
//}

//}
