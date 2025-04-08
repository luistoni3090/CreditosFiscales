using Px_ConciliacionBancaria.Utiles.Formas;
using Px_Utiles.Models.Api;
using Px_Utiles.Servicio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Px_ConciliacionBancaria.Utiles.Emun.Enumerados;

using Px_ConciliacionBancaria.Utiles.Generales;
using Px_Controles.Colors;
using Px_Controles.Forms.Msg;
using Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos;
using Px_Utiles.Models.Sistemas.Contabilidad.Catalogos;




namespace PX_ConciliacionBancaria
{
    public partial class FrmConciliacionManualAuxiliarvsAuxiliar : FormaGenBar
    {


        private void panContent_Paint(object sender, PaintEventArgs e)
        {

        }


        private int i_selbco, i_selaux, i_argbco, i_argcta;
        private char I_variosmov, I_tipo_moneda, I_tipobco;
        private decimal I_Impbco_tipo1, I_Impbco_tipo2, I_secconcilia;
        private DateTime I_argfecha, I_argfechaini;
        private string is_candado;

        eRequest oReq = new eRequest();
        DataGridView oGrid1 = new DataGridView();
        DataGridView oGrid2 = new DataGridView();


        public FrmConciliacionManualAuxiliarvsAuxiliar()
        {
            InitializeComponent();
            Start();
        }


        private async Task Start()
        {
            _Titulo = "Conciliación Manual Auxiliar vs Auxiliar";

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            await StartForm();
            await LoadGrid1();
            await LoadGrid2();

        }

        private void comboBanco_SelectedValueChanged(object sender, EventArgs e)
        {
            string IdBanco = comboBanco.SelectedValue.ToString();

            var algo = "";
            cargaComboCuenta(IdBanco);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async Task StartForm()
        {


            this.Text = string.Empty;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(800, 450);

            this.BackColor = Color.White;

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            if (_Main != null)
                await _Main.Status($"{_Titulo}", (int)MensajeTipo.Info);

            await cargaComboBanco();
            await cargaComboMes();
            txtAnio.Text = DateTime.Now.Year.ToString();

            fechaInicial.Format = DateTimePickerFormat.Custom;
            fechaInicial.CustomFormat = "dd/MM/yyyy";

            FechaFinal.Format = DateTimePickerFormat.Custom;
            FechaFinal.CustomFormat = "dd/MM/yyyy";


        }

        private async Task LoadGrid1()
        {
            // 1. Configurar las propiedades básicas del DataGridView
            oGrid1.Dock = DockStyle.Fill; // Hacer que ocupe todo el espacio del contenedor
            oGrid1.Name = "dataGridView1";
            oGrid1.AutoGenerateColumns = false; // Desactivar generación automática de columnas


            // 2. Agregar columnas manualmente
            oGrid1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Movimiento",
                HeaderText = "Movimiento",
                DataPropertyName = "Movimiento"
            });

            oGrid1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Referencia",
                HeaderText = "Referencia",
                DataPropertyName = "Referencia"
            });

            oGrid1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                HeaderText = "Fecha",
                DataPropertyName = "Fecha"
            });

            oGrid1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Importe",
                HeaderText = "Importe",
                DataPropertyName = "Importe"
            });

            oGrid1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Concepto",
                HeaderText = "Concepto",
                DataPropertyName = "Concepto"
            });

            oGrid1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tipo",
                HeaderText = "Tipo",
                DataPropertyName = "Tipo"
            });

            oGrid1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FormaPago",
                HeaderText = "FormaPago",
                DataPropertyName = "FormaPago"
            });

            oGrid1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ComprobantePago",
                HeaderText = "ComprobantePago",
                DataPropertyName = "ComprobantePago"
            });

            oGrid1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Caja",
                HeaderText = "Caja",
                DataPropertyName = "Caja"
            });

            // 3. Crear datos de ejemplo
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Movimiento", typeof(string));
            dataTable.Columns.Add("Referencia", typeof(string));
            dataTable.Columns.Add("Fecha", typeof(DateTime));
            dataTable.Columns.Add("Oficina", typeof(decimal));
            dataTable.Columns.Add("Importe", typeof(decimal));
            dataTable.Columns.Add("Concepto", typeof(string));
            dataTable.Columns.Add("Tipo", typeof(string));
            dataTable.Columns.Add("FormaPago", typeof(string));
            dataTable.Columns.Add("ComprobantePago", typeof(string));
            dataTable.Columns.Add("Caja", typeof(string));

            // 4. Asignar la fuente de datos al DataGridView
            oGrid1.DataSource = dataTable;

            // 5. Ajustar las columnas
            oGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Ajustar al ancho del grid
            oGrid1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells); // Ajustar según el contenido


            // 5. Agregar el DataGridView al formulario o al panel
            if (panelGridMovimientosBancoTipo1 != null)
            {
                //panContent.Controls.Clear(); // Limpiar el panel antes de agregar el grid
                panelGridMovimientosBancoTipo1.Controls.Add(oGrid1); // Agregar el DataGridView al panel
            }
            else
            {
                this.Controls.Add(oGrid1); // Si no hay panel, agregar directamente al formulario
            }



        }


        private async Task LoadGrid2()
        {
            // 1. Configurar las propiedades básicas del DataGridView
            oGrid2.Dock = DockStyle.Fill; // Hacer que ocupe todo el espacio del contenedor
            oGrid2.Name = "dataGridView2";
            oGrid2.AutoGenerateColumns = false; // Desactivar generación automática de columnas


            // 2. Agregar columnas manualmente
            oGrid2.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Movimiento",
                HeaderText = "Movimiento",
                DataPropertyName = "Movimiento"
            });

            oGrid2.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Referencia",
                HeaderText = "Referencia",
                DataPropertyName = "Referencia"
            });

            oGrid2.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                HeaderText = "Fecha",
                DataPropertyName = "Fecha"
            });

            oGrid2.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Importe",
                HeaderText = "Importe",
                DataPropertyName = "Importe"
            });

            oGrid2.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Concepto",
                HeaderText = "Concepto",
                DataPropertyName = "Concepto"
            });

            oGrid2.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tipo",
                HeaderText = "Tipo",
                DataPropertyName = "Tipo"
            });

            oGrid2.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FormaPago",
                HeaderText = "FormaPago",
                DataPropertyName = "FormaPago"
            });

            oGrid2.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ComprobantePago",
                HeaderText = "ComprobantePago",
                DataPropertyName = "ComprobantePago"
            });

            oGrid2.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Caja",
                HeaderText = "Caja",
                DataPropertyName = "Caja"
            });

            // 3. Crear datos de ejemplo
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Movimiento", typeof(string));
            dataTable.Columns.Add("Referencia", typeof(string));
            dataTable.Columns.Add("Fecha", typeof(DateTime));
            dataTable.Columns.Add("Importe", typeof(decimal));
            dataTable.Columns.Add("Concepto", typeof(string));
            dataTable.Columns.Add("Tipo", typeof(string));
            dataTable.Columns.Add("FormaPago", typeof(string));
            dataTable.Columns.Add("ComprobantePago", typeof(string));
            dataTable.Columns.Add("Caja", typeof(string));

            // 4. Asignar la fuente de datos al DataGridView
            oGrid2.DataSource = dataTable;

            // 5. Ajustar las columnas
            oGrid2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Ajustar al ancho del grid
            oGrid2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells); // Ajustar según el contenido


            // 5. Agregar el DataGridView al formulario o al panel
            if (panelGridMovimientosBancoTipo2 != null)
            {
                //panContent.Controls.Clear(); // Limpiar el panel antes de agregar el grid
                panelGridMovimientosBancoTipo2.Controls.Add(oGrid2); // Agregar el DataGridView al panel
            }
            else
            {
                this.Controls.Add(oGrid2); // Si no hay panel, agregar directamente al formulario
            }



        }

        #region "Carga Combo Banco"
        private async Task cargaComboBanco()
        {

            try
            {
                oReq.Tipo = 0;
                oReq.Query = "SELECT BANCO IDBanco, LPAD(BANCO, 2, '0') BANCO, DESCR FROM BANCO ORDER BY BANCO";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables[0] != null)
                {
                    var dataTable = oRes.Data.Tables[0];

                    // Crear una lista para los items del ComboBox
                    var comboList = new List<ComboBoxItem>();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["IDBanco"].ToString(),
                            Text = "(" + row["BANCO"].ToString() + ") " + row["DESCR"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboBanco.DataSource = comboList;
                    comboBanco.DisplayMember = "Text";
                    comboBanco.ValueMember = "Value";

                }

            }
            catch (Exception ex)
            { }

        }
        #endregion

        #region "Carga Combo Cuenta"
        private async Task cargaComboCuenta(string IdBanco)
        {

            try
            {
                oReq.Tipo = 0;
                oReq.Query = "SELECT CUENTA IDCUENTA, LPAD(CUENTA, 3, '0') CUENTA, DESCR FROM CUENTA WHERE BANCO = " + IdBanco + " ORDER BY CUENTA asc";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables[0] != null)
                {
                    var dataTable = oRes.Data.Tables[0];

                    // Crear una lista para los items del ComboBox
                    var comboList = new List<ComboBoxItem>();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["IDCUENTA"].ToString(),
                            Text = "(" + row["CUENTA"].ToString() + ") " + row["DESCR"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboCuenta.DataSource = comboList;
                    comboCuenta.DisplayMember = "Text";
                    comboCuenta.ValueMember = "Value";
                    comboCuenta.SelectedIndex = -1;

                }

            }
            catch (Exception ex)
            { }

        }

        private void cbx_filtro_periodo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_filtro_periodo.Checked)
            {
                fechaInicial.Enabled = true;
                FechaFinal.Enabled = true;

            }
            else
            {

                fechaInicial.Enabled = false;
                FechaFinal.Enabled = false;
            }
        }

        private void rbConciliar_CheckedChanged(object sender, EventArgs e)
        {
            string IdBanco = comboBanco.SelectedValue.ToString();
            string IdCuenta = comboCuenta.SelectedValue.ToString();

            string fechaFormateada = fechaInicial.Value.ToString("yyyy-MM-dd");

            wf_retrieve(IdBanco, IdCuenta, fechaFormateada);
        }

        private async void  btnAplicarFiltro_Click(object sender, EventArgs e)
        {
            DateTime ld_fecini, ld_fecfin;

            if (cbx_filtro_periodo.Checked)
            {

                DateTime fechaMinima = new DateTime(1900, 1, 1);

                if (fechaInicial.Value <= fechaMinima || FechaFinal.Value <= fechaMinima)
                {
                    MessageBoxMX.ShowDialog(null, "Las fechas seleccionadas son inválidas.", "Error", (int)StatusColorsTypes.Danger, false);
                }
                else if (FechaFinal.Value < fechaInicial.Value)
                {
                    MessageBoxMX.ShowDialog(null, "La fecha final no puede ser menor que la fecha inicial.", "Error", (int)StatusColorsTypes.Danger, false);
                }
                else
                {
                    
                    bool bExito = await detValida();

                    if (!bExito)
                        return;

                    string anio = txtAnio.Text;
                    string IdBanco = comboBanco.SelectedValue.ToString();
                    string IdCuenta = comboCuenta.SelectedValue.ToString();
                    string IdMes = comboMes.SelectedValue.ToString();

                    string fechaFormateada = fechaInicial.Value.ToString("yyyy-MM-dd");

                    wf_retrieve(IdBanco, IdCuenta, fechaFormateada);
                }
            }

            /*
            if (cbx_filtro_periodo.Checked)
           {
               I_argfechaini = ld_fecini;
               I_argfecha = ld_fecfin;
           }

           if (cb_conciliar.Text == "&Conciliar")
           {
               await dw_movbco_tipo1.Retrieve(i_argbco, i_argcta, I_argfechaini, I_argfecha);
           }
           else
           {
               await dw_movbco_tipo1.Retrieve(i_argbco, i_argcta, I_argfechaini, I_argfecha, I_secconcilia);
           }

           if (cb_conciliar.Text == "&Conciliar")
           {
               await wf_retrieve(i_argbco, i_argcta, I_argfechaini, I_argfecha);
           }*/

            rbConciliar.Enabled = true;
        }
        #endregion

        #region "Carga Combo Mes"
        private async Task cargaComboMes()
        {

            var comboList = new List<ComboBoxItem>();

            var item1 = new ComboBoxItem
            {
                Value = "1",
                Text = "ENERO"
            };
            var item2 = new ComboBoxItem
            {
                Value = "2",
                Text = "FEBRERO"
            };
            var item3 = new ComboBoxItem
            {
                Value = "3",
                Text = "MARZO"
            };
            var item4 = new ComboBoxItem
            {
                Value = "4",
                Text = "ABRIL"
            };
            var item5 = new ComboBoxItem
            {
                Value = "5",
                Text = "MAYO"
            };
            var item6 = new ComboBoxItem
            {
                Value = "6",
                Text = "JUNIO"
            };
            var item7 = new ComboBoxItem
            {
                Value = "7",
                Text = "JULIO"
            };
            var item8 = new ComboBoxItem
            {
                Value = "8",
                Text = "AGOSTO"
            };
            var item9 = new ComboBoxItem
            {
                Value = "9",
                Text = "SEPTIEMBRE"
            };
            var item10 = new ComboBoxItem
            {
                Value = "10",
                Text = "OCTUBRE"
            };
            var item11 = new ComboBoxItem
            {
                Value = "11",
                Text = "NOVIEMBRE"
            };
            var item12 = new ComboBoxItem
            {
                Value = "12",
                Text = "DICIEMBRE"
            };
            comboList.Add(item1);
            comboList.Add(item2);
            comboList.Add(item3);
            comboList.Add(item4);
            comboList.Add(item5);
            comboList.Add(item6);
            comboList.Add(item7);
            comboList.Add(item8);
            comboList.Add(item9);
            comboList.Add(item10);
            comboList.Add(item11);
            comboList.Add(item12);

            // Asignar la lista al ComboBox
            comboMes.DataSource = comboList;
            comboMes.DisplayMember = "Text";
            comboMes.ValueMember = "Value";
            comboMes.SelectedIndex = -1;

        }
        #endregion

        #region "Validar datos"
        private async Task<bool> detValida()
        {
            bool bExito = true;
            string sErr = string.Empty;


            if (string.IsNullOrWhiteSpace(txtAnio.Text))
                sErr += "Ingrese el mes ";

            if (string.IsNullOrWhiteSpace(comboCuenta.Text))
                sErr += "Seleccione la cuenta. ";

            if (string.IsNullOrWhiteSpace(comboMes.Text))
                sErr += "Seleccione el mes. ";

            if (sErr.Length > 0)
            {
                bExito = false;
                await _Main.Status($"Favor de atender los siguientes mensajes: {sErr}", (int)MensajeTipo.Warning);
            }
            else
            {

                string anio = txtAnio.Text;
                string IdBanco = comboBanco.SelectedValue.ToString();
                string IdCuenta = comboCuenta.SelectedValue.ToString();
                string IdMes = comboMes.SelectedValue.ToString();

            }

            return bExito;
        }
        #endregion


        public class ComboBoxItem
        {
            public string Text { get; set; } // Texto que se muestra en el ComboBox
            public object Value { get; set; } // Valor asociado al item

            public override string ToString()
            {
                return Text; // Esto es lo que se muestra en el ComboBox
            }
        }

        private void comboCuenta_SelectedValueChanged(object sender, EventArgs e)
        {
            comboMes.SelectedIndex = -1;
        }

        private void comboMes_SelectedValueChanged(object sender, EventArgs e)
        {


            if (comboMes.SelectedIndex != -1 && comboMes.SelectedIndex != 0)
            {
                rbConciliar.Enabled = true;
            }

        }


        public async Task wf_retrieve(string banco, string cuenta, string fecha)
        {

            oReq.Tipo = 0;
            oReq.Query = $@"
            SELECT FECHA_CON, FECHA_DEP, CONCILIACION
            FROM DGI.CONCILIACION
            WHERE BANCO = {banco} AND CUENTA = {cuenta} AND FECHA = TO_DATE('{fecha}','YYYY/MM/DD')";
            var oResProcesa = await WSServicio.Servicio(oReq);
            
            if (oResProcesa.Data.Tables.Count < 1)
            {
                MessageBoxMX.ShowDialog(null, "No se encontraron datos para la conciliación.", "Error", (int)StatusColorsTypes.Danger, false);
                return;
            }
            else
            MessageBoxMX.ShowDialog(null, "La Cuenta no ha sido Conciliada Automáticamente o ya ha sido Depurada", "Error", (int)StatusColorsTypes.Danger, false);

            string tmp_fechacon = oResProcesa.Data.Tables[0].Rows[0][0].ToString();
            string tmp_fechadep = oResProcesa.Data.Tables[0].Rows[0][1].ToString();
            string conciliacion = oResProcesa.Data.Tables[0].Rows[0][2].ToString();

            if (tmp_fechacon != null && tmp_fechadep == null)
            {
                oReq.Query = $@"
                SELECT ADD_MONTHS({fecha}, -PARAMETRO_CB.VIGENCIABCO)
                FROM DGI.PARAMETRO_CB
            ";
                oResProcesa = await WSServicio.Servicio(oReq);
                DateTime fec_bco = Convert.ToDateTime(oResProcesa.Data.Tables[0].Rows[0][0]);
                //I_argfechaini = F_pro_fechainimes(fec_bco);

                oReq.Query = $@"
                SELECT ADD_MONTHS({fecha}, -PARAMETRO_CB.VIGENCIAAUX)
                FROM DGI.PARAMETRO_CB
            ";
                oResProcesa = await WSServicio.Servicio(oReq);
                DateTime fec_aux = Convert.ToDateTime(oResProcesa.Data.Tables[0].Rows[0][0]);
               

                rbConciliar.Enabled = true;
            }
            else
            {
                MessageBoxMX.ShowDialog(null, "La Cuenta no ha sido Conciliada Automáticamente o ya ha sido Depurada", "Error", (int)StatusColorsTypes.Danger, false);
            }
        }

    }

}


