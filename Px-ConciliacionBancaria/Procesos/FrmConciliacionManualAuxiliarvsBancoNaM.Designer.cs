using Px_ConciliacionBancaria.Utiles.Formas;
using System;
using System.Windows.Forms;

namespace PX_ConciliacionBancaria
{
    public partial class FrmConciliacionManualAuxiliarvsBancoNaM : FormaGenBar
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConciliacionManualAuxiliarvsBancoNaM));
            this.panContent = new System.Windows.Forms.Panel();
            this.rbAbonos = new System.Windows.Forms.RadioButton();
            this.rbCargos = new System.Windows.Forms.RadioButton();
            this.txtTotalSelec2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtImporteSelec2 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTotalSelec1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtImporteSelec1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panelGridMovimientosBancoTipo2 = new System.Windows.Forms.Panel();
            this.panelGridMovimientosBancoTipo1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.FechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.fechaInicial = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAplicarFiltro = new System.Windows.Forms.Button();
            this.cbx_filtro_periodo = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdDesconciliar = new System.Windows.Forms.RadioButton();
            this.rbConciliar = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.comboMes = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboCuenta = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBanco = new System.Windows.Forms.ComboBox();
            this.stPoliza = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMensajes = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFormatoPoliza = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtAnio = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbCargar = new System.Windows.Forms.Button();
            this.cbProcesar = new System.Windows.Forms.Button();
            this.stFormato = new System.Windows.Forms.Label();
            this.stErroneos = new System.Windows.Forms.Label();
            this.stCuentasErroneas = new System.Windows.Forms.Label();
            this.panMenu = new System.Windows.Forms.Panel();
            this.btnmnuImprime = new System.Windows.Forms.Button();
            this.btnmnuAyuda = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panContent.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panContent
            // 
            this.panContent.Controls.Add(this.button2);
            this.panContent.Controls.Add(this.rbAbonos);
            this.panContent.Controls.Add(this.rbCargos);
            this.panContent.Controls.Add(this.txtTotalSelec2);
            this.panContent.Controls.Add(this.label11);
            this.panContent.Controls.Add(this.txtImporteSelec2);
            this.panContent.Controls.Add(this.label12);
            this.panContent.Controls.Add(this.txtTotalSelec1);
            this.panContent.Controls.Add(this.label10);
            this.panContent.Controls.Add(this.txtImporteSelec1);
            this.panContent.Controls.Add(this.label9);
            this.panContent.Controls.Add(this.label8);
            this.panContent.Controls.Add(this.label7);
            this.panContent.Controls.Add(this.panelGridMovimientosBancoTipo2);
            this.panContent.Controls.Add(this.panelGridMovimientosBancoTipo1);
            this.panContent.Controls.Add(this.panel2);
            this.panContent.Controls.Add(this.panel1);
            this.panContent.Controls.Add(this.button1);
            this.panContent.Controls.Add(this.comboMes);
            this.panContent.Controls.Add(this.label4);
            this.panContent.Controls.Add(this.comboCuenta);
            this.panContent.Controls.Add(this.label3);
            this.panContent.Controls.Add(this.comboBanco);
            this.panContent.Controls.Add(this.stPoliza);
            this.panContent.Controls.Add(this.statusStrip1);
            this.panContent.Controls.Add(this.txtAnio);
            this.panContent.Controls.Add(this.label2);
            this.panContent.Controls.Add(this.cbCargar);
            this.panContent.Controls.Add(this.cbProcesar);
            this.panContent.Controls.Add(this.stFormato);
            this.panContent.Controls.Add(this.stErroneos);
            this.panContent.Controls.Add(this.stCuentasErroneas);
            this.panContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panContent.Location = new System.Drawing.Point(0, 76);
            this.panContent.Name = "panContent";
            this.panContent.Size = new System.Drawing.Size(1172, 663);
            this.panContent.TabIndex = 10;
            this.panContent.Paint += new System.Windows.Forms.PaintEventHandler(this.panContent_Paint);
            // 
            // rbAbonos
            // 
            this.rbAbonos.AutoSize = true;
            this.rbAbonos.Enabled = false;
            this.rbAbonos.Location = new System.Drawing.Point(606, 73);
            this.rbAbonos.Name = "rbAbonos";
            this.rbAbonos.Size = new System.Drawing.Size(61, 17);
            this.rbAbonos.TabIndex = 39;
            this.rbAbonos.TabStop = true;
            this.rbAbonos.Text = "Abonos";
            this.rbAbonos.UseVisualStyleBackColor = true;
            // 
            // rbCargos
            // 
            this.rbCargos.AutoSize = true;
            this.rbCargos.Enabled = false;
            this.rbCargos.Location = new System.Drawing.Point(604, 34);
            this.rbCargos.Name = "rbCargos";
            this.rbCargos.Size = new System.Drawing.Size(58, 17);
            this.rbCargos.TabIndex = 39;
            this.rbCargos.TabStop = true;
            this.rbCargos.Text = "Cargos";
            this.rbCargos.UseVisualStyleBackColor = true;
            this.rbCargos.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // txtTotalSelec2
            // 
            this.txtTotalSelec2.Location = new System.Drawing.Point(855, 476);
            this.txtTotalSelec2.Name = "txtTotalSelec2";
            this.txtTotalSelec2.ReadOnly = true;
            this.txtTotalSelec2.Size = new System.Drawing.Size(155, 20);
            this.txtTotalSelec2.TabIndex = 51;
            this.txtTotalSelec2.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(855, 453);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 13);
            this.label11.TabIndex = 50;
            this.label11.Text = "Total Selec:";
            // 
            // txtImporteSelec2
            // 
            this.txtImporteSelec2.Location = new System.Drawing.Point(854, 407);
            this.txtImporteSelec2.Name = "txtImporteSelec2";
            this.txtImporteSelec2.ReadOnly = true;
            this.txtImporteSelec2.Size = new System.Drawing.Size(155, 20);
            this.txtImporteSelec2.TabIndex = 49;
            this.txtImporteSelec2.Text = "$0.00";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(854, 384);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 13);
            this.label12.TabIndex = 48;
            this.label12.Text = "Importe Selec:";
            // 
            // txtTotalSelec1
            // 
            this.txtTotalSelec1.Location = new System.Drawing.Point(857, 275);
            this.txtTotalSelec1.Name = "txtTotalSelec1";
            this.txtTotalSelec1.ReadOnly = true;
            this.txtTotalSelec1.Size = new System.Drawing.Size(155, 20);
            this.txtTotalSelec1.TabIndex = 47;
            this.txtTotalSelec1.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(857, 252);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 13);
            this.label10.TabIndex = 46;
            this.label10.Text = "Total Selec:";
            // 
            // txtImporteSelec1
            // 
            this.txtImporteSelec1.Location = new System.Drawing.Point(856, 206);
            this.txtImporteSelec1.Name = "txtImporteSelec1";
            this.txtImporteSelec1.ReadOnly = true;
            this.txtImporteSelec1.Size = new System.Drawing.Size(155, 20);
            this.txtImporteSelec1.TabIndex = 45;
            this.txtImporteSelec1.Text = "$0.00";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(856, 183);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 44;
            this.label9.Text = "Importe Selec:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 342);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(179, 13);
            this.label8.TabIndex = 43;
            this.label8.Text = "Movimientos del Auxiliar Contable (0)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 137);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 13);
            this.label7.TabIndex = 42;
            this.label7.Text = "Movimientos del Banco (0)";
            // 
            // panelGridMovimientosBancoTipo2
            // 
            this.panelGridMovimientosBancoTipo2.Location = new System.Drawing.Point(22, 361);
            this.panelGridMovimientosBancoTipo2.Name = "panelGridMovimientosBancoTipo2";
            this.panelGridMovimientosBancoTipo2.Size = new System.Drawing.Size(819, 167);
            this.panelGridMovimientosBancoTipo2.TabIndex = 39;
            // 
            // panelGridMovimientosBancoTipo1
            // 
            this.panelGridMovimientosBancoTipo1.Location = new System.Drawing.Point(21, 156);
            this.panelGridMovimientosBancoTipo1.Name = "panelGridMovimientosBancoTipo1";
            this.panelGridMovimientosBancoTipo1.Size = new System.Drawing.Size(820, 165);
            this.panelGridMovimientosBancoTipo1.TabIndex = 38;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.FechaFinal);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.fechaInicial);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.btnAplicarFiltro);
            this.panel2.Controls.Add(this.cbx_filtro_periodo);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(713, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(424, 86);
            this.panel2.TabIndex = 37;
            // 
            // FechaFinal
            // 
            this.FechaFinal.Enabled = false;
            this.FechaFinal.Location = new System.Drawing.Point(210, 48);
            this.FechaFinal.Name = "FechaFinal";
            this.FechaFinal.Size = new System.Drawing.Size(186, 20);
            this.FechaFinal.TabIndex = 41;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(164, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 40;
            this.label6.Text = "Final:";
            // 
            // fechaInicial
            // 
            this.fechaInicial.Enabled = false;
            this.fechaInicial.Location = new System.Drawing.Point(210, 9);
            this.fechaInicial.Name = "fechaInicial";
            this.fechaInicial.Size = new System.Drawing.Size(186, 20);
            this.fechaInicial.TabIndex = 38;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(164, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "Inicial:";
            // 
            // btnAplicarFiltro
            // 
            this.btnAplicarFiltro.Location = new System.Drawing.Point(25, 45);
            this.btnAplicarFiltro.Name = "btnAplicarFiltro";
            this.btnAplicarFiltro.Size = new System.Drawing.Size(86, 30);
            this.btnAplicarFiltro.TabIndex = 38;
            this.btnAplicarFiltro.Text = "Aplicar Filtro";
            this.btnAplicarFiltro.UseVisualStyleBackColor = true;
            this.btnAplicarFiltro.Click += new System.EventHandler(this.btnAplicarFiltro_Click);
            // 
            // cbx_filtro_periodo
            // 
            this.cbx_filtro_periodo.AutoSize = true;
            this.cbx_filtro_periodo.Location = new System.Drawing.Point(108, 12);
            this.cbx_filtro_periodo.Name = "cbx_filtro_periodo";
            this.cbx_filtro_periodo.Size = new System.Drawing.Size(15, 14);
            this.cbx_filtro_periodo.TabIndex = 38;
            this.cbx_filtro_periodo.UseVisualStyleBackColor = true;
            this.cbx_filtro_periodo.CheckedChanged += new System.EventHandler(this.cbx_filtro_periodo_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Filtrar por Periodo";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdDesconciliar);
            this.panel1.Controls.Add(this.rbConciliar);
            this.panel1.Location = new System.Drawing.Point(458, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(108, 86);
            this.panel1.TabIndex = 36;
            // 
            // rdDesconciliar
            // 
            this.rdDesconciliar.AutoSize = true;
            this.rdDesconciliar.Enabled = false;
            this.rdDesconciliar.Location = new System.Drawing.Point(15, 52);
            this.rdDesconciliar.Name = "rdDesconciliar";
            this.rdDesconciliar.Size = new System.Drawing.Size(83, 17);
            this.rdDesconciliar.TabIndex = 38;
            this.rdDesconciliar.TabStop = true;
            this.rdDesconciliar.Text = "Desconciliar";
            this.rdDesconciliar.UseVisualStyleBackColor = true;
            // 
            // rbConciliar
            // 
            this.rbConciliar.AutoSize = true;
            this.rbConciliar.Enabled = false;
            this.rbConciliar.Location = new System.Drawing.Point(15, 12);
            this.rbConciliar.Name = "rbConciliar";
            this.rbConciliar.Size = new System.Drawing.Size(65, 17);
            this.rbConciliar.TabIndex = 37;
            this.rbConciliar.TabStop = true;
            this.rbConciliar.Text = "Conciliar";
            this.rbConciliar.UseVisualStyleBackColor = true;
            this.rbConciliar.CheckedChanged += new System.EventHandler(this.rbConciliar_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(140, 599);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 30);
            this.button1.TabIndex = 35;
            this.button1.Text = "Actualizar";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // comboMes
            // 
            this.comboMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMes.FormattingEnabled = true;
            this.comboMes.Location = new System.Drawing.Point(286, 83);
            this.comboMes.Name = "comboMes";
            this.comboMes.Size = new System.Drawing.Size(156, 21);
            this.comboMes.TabIndex = 34;
            this.comboMes.SelectedIndexChanged += new System.EventHandler(this.comboMes_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(250, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Mes:";
            // 
            // comboCuenta
            // 
            this.comboCuenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCuenta.FormattingEnabled = true;
            this.comboCuenta.Location = new System.Drawing.Point(66, 83);
            this.comboCuenta.Name = "comboCuenta";
            this.comboCuenta.Size = new System.Drawing.Size(164, 21);
            this.comboCuenta.TabIndex = 32;
            this.comboCuenta.SelectedIndexChanged += new System.EventHandler(this.comboCuenta_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Cuenta:";
            // 
            // comboBanco
            // 
            this.comboBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBanco.FormattingEnabled = true;
            this.comboBanco.Location = new System.Drawing.Point(66, 28);
            this.comboBanco.Name = "comboBanco";
            this.comboBanco.Size = new System.Drawing.Size(164, 21);
            this.comboBanco.TabIndex = 14;
            this.comboBanco.SelectedValueChanged += new System.EventHandler(this.comboBanco_SelectedValueChanged);
            // 
            // stPoliza
            // 
            this.stPoliza.AutoSize = true;
            this.stPoliza.Location = new System.Drawing.Point(22, 31);
            this.stPoliza.Name = "stPoliza";
            this.stPoliza.Size = new System.Drawing.Size(41, 13);
            this.stPoliza.TabIndex = 13;
            this.stPoliza.Text = "Banco:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMensajes,
            this.lblFormatoPoliza});
            this.statusStrip1.Location = new System.Drawing.Point(0, 641);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1172, 22);
            this.statusStrip1.TabIndex = 28;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMensajes
            // 
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(0, 17);
            // 
            // lblFormatoPoliza
            // 
            this.lblFormatoPoliza.Name = "lblFormatoPoliza";
            this.lblFormatoPoliza.Size = new System.Drawing.Size(0, 17);
            // 
            // txtAnio
            // 
            this.txtAnio.Location = new System.Drawing.Point(287, 29);
            this.txtAnio.Name = "txtAnio";
            this.txtAnio.Size = new System.Drawing.Size(155, 20);
            this.txtAnio.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Año:";
            // 
            // cbCargar
            // 
            this.cbCargar.Enabled = false;
            this.cbCargar.Location = new System.Drawing.Point(18, 599);
            this.cbCargar.Name = "cbCargar";
            this.cbCargar.Size = new System.Drawing.Size(100, 30);
            this.cbCargar.TabIndex = 11;
            this.cbCargar.Text = "Conciliar";
            this.cbCargar.UseVisualStyleBackColor = true;
            // 
            // cbProcesar
            // 
            this.cbProcesar.Location = new System.Drawing.Point(387, 599);
            this.cbProcesar.Name = "cbProcesar";
            this.cbProcesar.Size = new System.Drawing.Size(100, 30);
            this.cbProcesar.TabIndex = 12;
            this.cbProcesar.Text = "Salir";
            this.cbProcesar.UseVisualStyleBackColor = true;
            this.cbProcesar.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // stFormato
            // 
            this.stFormato.AutoSize = true;
            this.stFormato.Location = new System.Drawing.Point(1250, 1060);
            this.stFormato.Name = "stFormato";
            this.stFormato.Size = new System.Drawing.Size(0, 13);
            this.stFormato.TabIndex = 18;
            // 
            // stErroneos
            // 
            this.stErroneos.AutoSize = true;
            this.stErroneos.Location = new System.Drawing.Point(3220, 900);
            this.stErroneos.Name = "stErroneos";
            this.stErroneos.Size = new System.Drawing.Size(0, 13);
            this.stErroneos.TabIndex = 19;
            // 
            // stCuentasErroneas
            // 
            this.stCuentasErroneas.AutoSize = true;
            this.stCuentasErroneas.Location = new System.Drawing.Point(3120, 800);
            this.stCuentasErroneas.Name = "stCuentasErroneas";
            this.stCuentasErroneas.Size = new System.Drawing.Size(91, 13);
            this.stCuentasErroneas.TabIndex = 20;
            this.stCuentasErroneas.Text = "Cuentas Erroneas";
            // 
            // panMenu
            // 
            this.panMenu.Controls.Add(this.btnmnuImprime);
            this.panMenu.Controls.Add(this.btnmnuAyuda);
            this.panMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panMenu.Location = new System.Drawing.Point(0, 37);
            this.panMenu.Name = "panMenu";
            this.panMenu.Size = new System.Drawing.Size(1172, 39);
            this.panMenu.TabIndex = 9;
            // 
            // btnmnuImprime
            // 
            this.btnmnuImprime.BackColor = System.Drawing.Color.White;
            this.btnmnuImprime.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnmnuImprime.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnmnuImprime.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(28)))), ((int)(((byte)(50)))));
            this.btnmnuImprime.FlatAppearance.BorderSize = 0;
            this.btnmnuImprime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmnuImprime.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmnuImprime.ForeColor = System.Drawing.Color.White;
            this.btnmnuImprime.Image = ((System.Drawing.Image)(resources.GetObject("btnmnuImprime.Image")));
            this.btnmnuImprime.Location = new System.Drawing.Point(1102, 0);
            this.btnmnuImprime.Name = "btnmnuImprime";
            this.btnmnuImprime.Size = new System.Drawing.Size(35, 39);
            this.btnmnuImprime.TabIndex = 9;
            this.btnmnuImprime.Text = "&i";
            this.btnmnuImprime.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnmnuImprime.UseVisualStyleBackColor = false;
            // 
            // btnmnuAyuda
            // 
            this.btnmnuAyuda.BackColor = System.Drawing.Color.White;
            this.btnmnuAyuda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnmnuAyuda.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnmnuAyuda.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(28)))), ((int)(((byte)(50)))));
            this.btnmnuAyuda.FlatAppearance.BorderSize = 0;
            this.btnmnuAyuda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmnuAyuda.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmnuAyuda.ForeColor = System.Drawing.Color.White;
            this.btnmnuAyuda.Image = ((System.Drawing.Image)(resources.GetObject("btnmnuAyuda.Image")));
            this.btnmnuAyuda.Location = new System.Drawing.Point(1137, 0);
            this.btnmnuAyuda.Name = "btnmnuAyuda";
            this.btnmnuAyuda.Size = new System.Drawing.Size(35, 39);
            this.btnmnuAyuda.TabIndex = 8;
            this.btnmnuAyuda.Text = "&h";
            this.btnmnuAyuda.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnmnuAyuda.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(266, 599);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 30);
            this.button2.TabIndex = 52;
            this.button2.Text = "Semiautomática";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // FrmConciliacionManualAuxiliarvsBancoNaM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 739);
            this.Controls.Add(this.panContent);
            this.Controls.Add(this.panMenu);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(798, 444);
            this.Name = "FrmConciliacionManualAuxiliarvsBancoNaM";
            this.Text = "Conciliación Automática";
            this.Controls.SetChildIndex(this.panMenu, 0);
            this.Controls.SetChildIndex(this.panContent, 0);
            this.panContent.ResumeLayout(false);
            this.panContent.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private Panel panContent;
        private ComboBox comboMes;
        private Label label4;
        private ComboBox comboCuenta;
        private Label label3;
        private Label stPoliza;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblMensajes;
        private ToolStripStatusLabel lblFormatoPoliza;
        private TextBox txtAnio;
        private Label label2;
        private Button cbCargar;
        private Button cbProcesar;
        private Label stFormato;
        private Label stErroneos;
        private Label stCuentasErroneas;
        private Panel panMenu;
        private ComboBox comboBanco;
        private Button btnmnuImprime;
        private Button btnmnuAyuda;
        private Button button1;
        private Panel panel2;
        private Panel panel1;
        private RadioButton rdDesconciliar;
        private RadioButton rbConciliar;
        private Button btnAplicarFiltro;
        private CheckBox cbx_filtro_periodo;
        private Label label1;
        private Label label5;
        private DateTimePicker fechaInicial;
        private Label label6;
        private DateTimePicker FechaFinal;
        private Panel panelGridMovimientosBancoTipo1;
        private Panel panelGridMovimientosBancoTipo2;
        private TextBox txtTotalSelec1;
        private Label label10;
        private TextBox txtImporteSelec1;
        private Label label9;
        private Label label8;
        private Label label7;
        private TextBox txtTotalSelec2;
        private Label label11;
        private TextBox txtImporteSelec2;
        private Label label12;
        private RadioButton rbAbonos;
        private RadioButton rbCargos;
        private Button button2;
    }
}
