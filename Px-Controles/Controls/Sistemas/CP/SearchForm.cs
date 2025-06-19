using Px_Utiles.Models.Sistemas.Contabilidad.Procesos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Controles.Controls.Sistemas.CP
{
    public partial class SearchForm : Form
    {
        private List<CuentaPrePol> cuentas;
        public CuentaPrePol SelectedCuenta { get; private set; }

        public SearchForm(List<CuentaPrePol> cuentasDataSource)
        {

            Size = new Size(1, 1);

            InitializeComponent();
            this.cuentas = cuentasDataSource;
            InitializeSearchForm();
        }

        public SearchForm(List<CuentaPrePol> cuentasDataSource, int x, int y)
        {
            Size = new Size(1, 1);

            InitializeComponent();
            this.cuentas = cuentasDataSource;
            InitializeSearchForm();

            Location = new Point(x, y);
        }

        private void InitializeSearchForm()
        {

            Text = "Buscar Cuentas Contables";
            Size = new Size(440, 300);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            StartPosition = FormStartPosition.Manual;
            //Location = new Point(this.Location.X + 100, this.Location.Y + 100);

            TextBox searchTextBox = new TextBox { Dock = DockStyle.Top };
            //TextBox searchTextBox = new TextBox { Dock = DockStyle.Top, PlaceholderText = "Buscar por cuenta o descripción..." };
            ListView resultListView = new ListView { Dock = DockStyle.Fill, View = View.Details, FullRowSelect = true };
            resultListView.Columns.Add("Cuenta", 150);
            resultListView.Columns.Add("Descripción", 250);

            searchTextBox.TextChanged += (sender, args) =>
            {
                string filter = searchTextBox.Text.ToLower();
                var filteredCuentas = cuentas.Where(c => c.CUENTACONTABLE.ToLower().Contains(filter) || c.DESCR.ToLower().Contains(filter)).ToList();

                resultListView.Items.Clear();
                foreach (var cuenta in filteredCuentas)
                {
                    ListViewItem item = new ListViewItem(cuenta.CUENTACONTABLE);
                    item.SubItems.Add(cuenta.DESCR);
                    item.Tag = cuenta;
                    resultListView.Items.Add(item);
                }
            };

            resultListView.DoubleClick += (sender, args) =>
            {
                if (resultListView.SelectedItems.Count > 0)
                {
                    SelectedCuenta = (CuentaPrePol)resultListView.SelectedItems[0].Tag;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            };

            Button okButton = new Button { Text = "Aceptar", Dock = DockStyle.Bottom };
            okButton.Click += (sender, args) =>
            {
                if (resultListView.SelectedItems.Count > 0)
                {
                    SelectedCuenta = (CuentaPrePol)resultListView.SelectedItems[0].Tag;
                    this.DialogResult = DialogResult.OK;
                }
                this.Close();
            };

            this.Controls.Add(resultListView);
            this.Controls.Add(searchTextBox);
            this.Controls.Add(okButton);


            searchTextBox.Focus();
        }
        //private List<Cuenta> cuentas;

        //public Cuenta SelectedCuenta { get; private set; }

        //public SearchForm(List<Cuenta> cuentasDataSource)
        //{
        //    InitializeComponent();
        //    this.cuentas = cuentasDataSource;
        //    InitializeSearchForm();
        //}

        //private void InitializeSearchForm()
        //{
        //    TextBox searchTextBox = new TextBox { Dock = DockStyle.Top };
        //    ListBox resultListBox = new ListBox { Dock = DockStyle.Fill };

        //    searchTextBox.TextChanged += (sender, args) =>
        //    {
        //        string filter = searchTextBox.Text.ToLower();
        //        var filteredCuentas = cuentas.Where(c => c.CUENTACONTABLE.ToLower().Contains(filter) || c.DESCR.ToLower().Contains(filter)).ToList();
        //        resultListBox.DataSource = filteredCuentas;
        //        resultListBox.DisplayMember = "DisplayValue"; // "DESCR";
        //    };

        //    resultListBox.DoubleClick += (sender, args) =>
        //    {
        //        SelectedCuenta = (Cuenta)resultListBox.SelectedItem;
        //        this.DialogResult = DialogResult.OK;
        //        this.Close();
        //    };

        //    this.Controls.Add(resultListBox);
        //    this.Controls.Add(searchTextBox);
        //}
    }
}
