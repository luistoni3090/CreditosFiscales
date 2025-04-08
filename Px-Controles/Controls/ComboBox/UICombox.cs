/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 UICombox.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// ComboBox bonito

using Px_Controles.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Controles.Controls.ComboBox
{
    /// <summary>
    /// UICombox.
    /// Implements the <see cref="HZH_Controls.Controls.UCControlBase" />
    /// </summary>
    /// <seealso cref="HZH_Controls.Controls.UCControlBase" />
    [DefaultEvent("SelectedChangedEvent")]
    public partial class UICombox : UIControlBase
    {
        /// <summary>
        /// Color primario
        /// </summary>
        Color _ForeColor = Color.FromArgb(64, 64, 64);
        /// <summary>
        /// color de texto
        /// </summary>
        /// <value>Color de texto.</value>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        [Description("文字颜色"), Category("personalizar")]
        public override Color ForeColor
        {
            get
            {
                return _ForeColor;
            }
            set
            {
                _ForeColor = value;
                lblInput.ForeColor = value;
                txtInput.ForeColor = value;
            }
        }
        /// <summary>
        /// Evento seleccionado
        /// </summary>
        [Description("Evento seleccionado"), Category("personalizar")]
        public event EventHandler SelectedChangedEvent;
        /// <summary>
        /// Evento de cambio de texto
        /// </summary>
        [Description("Evento de cambio de texto"), Category("personalizar")]
        public event EventHandler TextChangedEvent;

        /// <summary>
        /// Estilo del borde
        /// </summary>
        private ComboBoxStyle _BoxStyle = ComboBoxStyle.DropDown;
        /// <summary>
        /// Estilo de control
        /// </summary>
        /// <value>Estilo del borde.</value>
        [Description("Estilo de control"), Category("personalizar")]
        public ComboBoxStyle BoxStyle
        {
            get { return _BoxStyle; }
            set
            {
                _BoxStyle = value;
                if (value == ComboBoxStyle.DropDownList)
                {
                    lblInput.Visible = true;
                    txtInput.Visible = false;
                }
                else
                {
                    lblInput.Visible = false;
                    txtInput.Visible = true;
                }

                if (this._BoxStyle == ComboBoxStyle.DropDownList)
                {
                    txtInput.BackColor = _BackColor;
                    base.FillColor = _BackColor;
                    base.RectColor = _BackColor;
                }
                else
                {
                    txtInput.BackColor = Color.White;
                    base.FillColor = Color.White;
                    base.RectColor = Color.FromArgb(220, 220, 220);
                }
            }
        }

        /// <summary>
        /// Fuente
        /// </summary>
        private Font _Font = new Font("微软雅黑", 12);
        /// <summary>
        /// Fuente
        /// </summary>
        /// <value>Fuente.</value>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        ///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        [Description("Fuente"), Category("personalizar")]
        public new Font Font
        {
            get { return _Font; }
            set
            {
                _Font = value;
                lblInput.Font = value;
                txtInput.Font = value;
                //txtInput.PromptFont = value;
                this.txtInput.Location = new Point(this.txtInput.Location.X, (this.Height - txtInput.Height) / 2);
                this.lblInput.Location = new Point(this.lblInput.Location.X, (this.Height - lblInput.Height) / 2);
            }
        }


        /// <summary>
        /// Color de relleno cuando se utiliza borde, no rellenar cuando el valor es color de fondo o color transparente o valor vacío
        /// </summary>
        /// <value>El color del relleno.</value>
        [Obsolete("Propiedades ya no disponibles")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        private new Color FillColor
        {
            get;
            set;
        }

        /// <summary>
        /// color del borde
        /// </summary>
        /// <value>Color del borde.</value>
        [Obsolete("Propiedades ya no disponibles")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        private new Color RectColor
        {
            get;
            set;
        }

        /// <summary>
        /// Valor del texto
        /// </summary>
        private string _TextValue;
        /// <summary>
        /// Texto
        /// </summary>
        /// <value>The text value.</value>
        [Description("Texto"), Category("personalizar")]
        public string TextValue
        {
            get { return _TextValue; }
            set
            {
                _TextValue = value;
                if (lblInput.Text != value)
                    lblInput.Text = value;
                if (txtInput.Text != value)
                    txtInput.Text = value;
            }
        }

        /// <summary>
        /// Origen
        /// </summary>
        private List<KeyValuePair<string, string>> _source = null;
        /// <summary>
        /// Origen de datos
        /// </summary>
        /// <value>Origen de datos.</value>
        [Description("Origen de datos"), Category("personalizar")]
        public List<KeyValuePair<string, string>> Source
        {
            get { return _source; }
            set
            {
                _source = value;
                _selectedIndex = -1;
                _selectedValue = "";
                _selectedItem = new KeyValuePair<string, string>();
                _selectedText = "";
                lblInput.Text = "";
                txtInput.Text = "";
            }
        }

        /// <summary>
        /// Elemento seleccionado
        /// </summary>
        private KeyValuePair<string, string> _selectedItem = new KeyValuePair<string, string>();

        /// <summary>
        /// Índice seleccionado
        /// </summary>
        private int _selectedIndex = -1;
        /// <summary>
        /// Subíndice de datos seleccionados
        /// </summary>
        /// <value>The index of the selected.</value>
        [Description("Subíndice de datos seleccionados"), Category("personalizar")]
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                if (value < 0 || _source == null || _source.Count <= 0 || value >= _source.Count)
                {
                    _selectedIndex = -1;
                    _selectedValue = "";
                    _selectedItem = new KeyValuePair<string, string>();
                    SelectedText = "";
                }
                else
                {
                    _selectedIndex = value;
                    _selectedItem = _source[value];
                    _selectedValue = _source[value].Key;
                    SelectedText = _source[value].Value;
                }
            }
        }

        /// <summary>
        /// El valor seleccionado
        /// </summary>
        private string _selectedValue = "";
        /// <summary>
        /// Valor seleccionado
        /// </summary>
        /// <value>El valor seleccionado.</value>
        [Description("Valor seleccionado"), Category("personalizar")]
        public string SelectedValue
        {
            get
            {
                return _selectedValue;
            }
            set
            {
                if (_source == null || _source.Count <= 0)
                {
                    SelectedText = "";
                    _selectedValue = "";
                    _selectedIndex = -1;
                    _selectedItem = new KeyValuePair<string, string>();
                }
                else
                {
                    for (int i = 0; i < _source.Count; i++)
                    {
                        if (_source[i].Key == value)
                        {
                            _selectedValue = value;
                            _selectedIndex = i;
                            _selectedItem = _source[i];
                            SelectedText = _source[i].Value;
                            return;
                        }
                    }
                    _selectedValue = "";
                    _selectedIndex = -1;
                    _selectedItem = new KeyValuePair<string, string>();
                    SelectedText = "";
                }
            }
        }

        /// <summary>
        /// Texto seleccionado
        /// </summary>
        private string _selectedText = "";
        /// <summary>
        /// Texto seleccionado
        /// </summary>
        /// <value>Texto seleccionado.</value>
        [Description("Texto seleccionado"), Category("personalizar")]
        public string SelectedText
        {
            get { return _selectedText; }
            private set
            {
                _selectedText = value;
                lblInput.Text = _selectedText;
                txtInput.Text = _selectedText;
                if (SelectedChangedEvent != null)
                {
                    SelectedChangedEvent(this, null);
                }
            }
        }

        /// <summary>
        /// Ancho de elemento
        /// </summary>
        private int _ItemWidth = 70;
        /// <summary>
        /// Ancho de elemento
        /// </summary>
        /// <value>The width of the item.</value>
        [Description("Ancho de elemento"), Category("personalizar")]
        public int ItemWidth
        {
            get { return _ItemWidth; }
            set { _ItemWidth = value; }
        }

        /// <summary>
        /// La altura del panel desplegable
        /// </summary>
        private int _dropPanelHeight = -1;
        /// <summary>
        /// La altura del panel desplegable
        /// </summary>
        /// <value>La altura del panel desplegable.</value>
        [Description("La altura del panel desplegable"), Category("personalizar")]
        public int DropPanelHeight
        {
            get { return _dropPanelHeight; }
            set { _dropPanelHeight = value; }
        }
        /// <summary>
        /// Obtiene o establece el color de fondo del control.
        /// </summary>
        /// <value>Obtiene o establece el color de fondo del control.</value>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        [Obsolete("Atributos que ya no están disponibles. Si necesita cambiar el color de fondo, utilice BackColorExt.")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        private new Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = Color.Transparent;
            }
        }

        /// <summary>
        /// Color de fondo
        /// </summary>
        private Color _BackColor = Color.FromArgb(240, 240, 240);
        /// <summary>
        /// Color de fondo
        /// </summary>
        /// <value>Color de fondo.</value>
        [Description("Color de fondo"), Category("personalizar")]
        public Color BackColorExt
        {
            get
            {
                return _BackColor;
            }
            set
            {
                if (value == Color.Transparent)
                    return;
                _BackColor = value;
                lblInput.BackColor = value;

                if (this._BoxStyle == ComboBoxStyle.DropDownList)
                {
                    txtInput.BackColor = value;
                    base.FillColor = value;
                    base.RectColor = value;
                }
                else
                {
                    txtInput.BackColor = Color.White;
                    base.FillColor = Color.White;
                    base.RectColor = Color.FromArgb(220, 220, 220);
                }
            }
        }

        /// <summary>
        /// Color del botón de desplegable
        /// </summary>
        private Color triangleColor = Color.FromArgb(255, 77, 59);
        /// <summary>
        /// Color del botón de desplegable
        /// </summary>
        /// <value>Color del botón de desplegable.</value>
        [Description("Color del botón de desplegable"), Category("personalizar")]
        public Color TriangleColor
        {
            get { return triangleColor; }
            set
            {
                triangleColor = value;
                Bitmap bit = new Bitmap(12, 10);
                Graphics g = Graphics.FromImage(bit);
                g.SetGDIHigh();
                GraphicsPath path = new GraphicsPath();
                path.AddLines(new Point[]
                {
                    new Point(1,1),
                    new Point(11,1),
                    new Point(6,10),
                    new Point(1,1)
                });
                g.FillPath(new SolidBrush(value), path);
                g.Dispose();
                panel1.BackgroundImage = bit;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public UICombox()
        {
            InitializeComponent();
            lblInput.BackColor = _BackColor;
            if (this._BoxStyle == ComboBoxStyle.DropDownList)
            {
                txtInput.BackColor = _BackColor;
                base.FillColor = _BackColor;
                base.RectColor = _BackColor;
            }
            else
            {
                txtInput.BackColor = Color.White;
                base.FillColor = Color.White;
                base.RectColor = Color.FromArgb(220, 220, 220);
            }
            base.BackColor = Color.Transparent;
        }

        /// <summary>
        /// Maneja el evento SizeChanged del control UIComboBox.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void UIComboBox_SizeChanged(object sender, EventArgs e)
        {
            this.txtInput.Location = new Point(this.txtInput.Location.X, (this.Height - txtInput.Height) / 2);
            this.lblInput.Location = new Point(this.lblInput.Location.X, (this.Height - lblInput.Height) / 2);
        }

        /// <summary>
        /// Maneja el evento TextChanged del control txtInput.
        /// </summary>
        /// <param name="sender">Origen del evento</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            TextValue = txtInput.Text;
            if (TextChangedEvent != null)
            {
                TextChangedEvent(this, null);
            }
        }

        /// <summary>
        /// Maneja el evento MouseDown del control de clic.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">La instancia <see cref="MouseEventArgs" /> que contiene los datos del evento.</param>
        protected virtual void click_MouseDown(object sender, MouseEventArgs e)
        {
            if (_frmAnchor == null || _frmAnchor.IsDisposed || _frmAnchor.Visible == false)
            {

                if (this.Source != null && this.Source.Count > 0)
                {
                    int intRow = 0;
                    int intCom = 1;
                    var p = this.PointToScreen(this.Location);
                    while (true)
                    {
                        int intScreenHeight = Screen.PrimaryScreen.Bounds.Height;
                        if ((p.Y + this.Height + this.Source.Count / intCom * 50 < intScreenHeight || p.Y - this.Source.Count / intCom * 50 > 0)
                            && (_dropPanelHeight <= 0 ? true : (this.Source.Count / intCom * 50 <= _dropPanelHeight)))
                        {
                            intRow = this.Source.Count / intCom + (this.Source.Count % intCom != 0 ? 1 : 0);
                            break;
                        }
                        intCom++;
                    }

                    //UCTimePanel ucTime = new UCTimePanel();
                    //ucTime.IsShowBorder = true;
                    //int intWidth = this.Width / intCom;
                    //if (intWidth < _ItemWidth)
                    //    intWidth = _ItemWidth;
                    //Size size = new Size(intCom * intWidth, intRow * 50);
                    //ucTime.Size = size;
                    //ucTime.FirstEvent = true;
                    //ucTime.SelectSourceEvent += ucTime_SelectSourceEvent;
                    //ucTime.Row = intRow;
                    //ucTime.Column = intCom;
                    //List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();
                    //foreach (var item in this.Source)
                    //{
                    //    lst.Add(new KeyValuePair<string, string>(item.Key, item.Value));
                    //}
                    //ucTime.Source = lst;

                    //ucTime.SetSelect(_selectedValue);

                    //_frmAnchor = new Forms.FrmAnchor(this, ucTime);
                    //_frmAnchor.Load += (a, b) => { (a as Form).Size = size; };

                    _frmAnchor.Show(this.FindForm());

                }
            }
            else
            {
                _frmAnchor.Close();
            }
        }


        /// <summary>
        /// FRM ancho
        /// </summary>
        Forms.FrmAnchor _frmAnchor;
        /// <summary>
        /// Maneja el evento SelectSourceEvent del control ucTime.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        void ucTime_SelectSourceEvent(object sender, EventArgs e)
        {
            if (_frmAnchor != null && !_frmAnchor.IsDisposed && _frmAnchor.Visible)
            {
                SelectedValue = sender.ToString();
                _frmAnchor.Close();
            }
        }

        /// <summary>
        /// Maneja el evento Load del control UIComboBox.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void UIComboBox_Load(object sender, EventArgs e)
        {
            if (this._BoxStyle == ComboBoxStyle.DropDownList)
            {
                txtInput.BackColor = _BackColor;
                base.FillColor = _BackColor;
                base.RectColor = _BackColor;
            }
            else
            {
                txtInput.BackColor = Color.White;
                base.FillColor = Color.White;
                base.RectColor = Color.FromArgb(220, 220, 220);
            }
        }
    }
}
