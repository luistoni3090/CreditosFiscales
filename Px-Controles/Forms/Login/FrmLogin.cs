using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Controles.Forms.Login
{
    /// <summary>
    /// Formulario de login modal, bloquea el formulario padre con una máscara hasta cerrar.
    /// </summary>
    public partial class FrmLogin : FrmBase
    {
        // Delegados
        public delegate Task<bool> LoginDelegate(string usuario, string password);
        public delegate Task SalirDelegate();

        public LoginDelegate LoginValida;
        public SalirDelegate LoginSalir;

        // Propiedades de acceso
        public string _Usuario => txtUsu.Text.Trim();
        public string _Password => txtPws.Text;

        public FrmLogin()
        {
            InitializeComponent();
            InitFormMove(this.lblTitle);

            txtPws.UseSystemPasswordChar = true;
            //txtPws.PasswordChar = '•'; // Caracter de contraseña personalizado

            // Navegación con Enter
            txtUsu.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtPws.Focus();
                    e.SuppressKeyPress = true;
                }
            };
            txtPws.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnOk.PerformClick();
                    e.SuppressKeyPress = true;
                }
            };

            btnOk.Click += btnOK_Click;
            btnCancel.Click += btnCancel_Click;

            // No mostrar en taskbar
            this.ShowInTaskbar = false;
        }

        private async void btnOK_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Enabled = false;

            bool exito = await LoginValida(_Usuario, _Password);
            if (exito)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                //MessageBox.Show(
                //    this,
                //    "Usuario o contraseña incorrectos.",
                //    "Error de login",
                //    MessageBoxButtons.OK,
                //    MessageBoxIcon.Warning
                //);
            }

            this.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            if (LoginSalir != null)
                await LoginSalir();
        }

        /// <summary>
        /// Cada vez que se muestra el formulario, enfoca el textbox de usuario.
        /// </summary>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            txtUsu.Focus();
        }

        /// <summary>
        /// Invoca el login con máscara modal sobre el formulario padre.
        /// </summary>
        public static async Task<bool> MostrarLogin(
            Form formularioPadre,
            LoginDelegate funcionValidacion,
            SalirDelegate funcionSalir,
            string sBaseDatos,
            string sTitulo = "Gobierno de Baja California",
            bool Institución = false
        )
        {
            await Task.Yield();  // permite que sea verdaderamente async

            using (var loginForm = new FrmLogin())
            {
                loginForm.lblTitle.Text = sTitulo;
                loginForm.LoginValida = funcionValidacion;
                loginForm.LoginSalir = funcionSalir;
                loginForm.StartPosition = FormStartPosition.CenterParent;

                loginForm.txtDB.Text = sBaseDatos;
                loginForm.label1.Visible = Institución;
                loginForm.txtInstitucuón.Visible = Institución;

                // --- Creamos y mostramos la máscara ---
                using (var mask = new Form())
                {
                    // Configuración de la máscara
                    mask.FormBorderStyle = FormBorderStyle.None;
                    mask.ShowInTaskbar = false;
                    mask.StartPosition = FormStartPosition.Manual;
                    mask.BackColor = Color.White;
                    mask.Opacity = 0.5;

                    // Ajustamos tamaño y posición para cubrir el padre
                    var parent = formularioPadre;
                    mask.Size = parent.ClientSize;
                    mask.Location = parent.PointToScreen(Point.Empty);

                    // Mostramos la máscara *sin* focus propio
                    mask.Show(parent);

                    // Ahora mostramos el login (modal sobre el padre)
                    var result = loginForm.ShowDialog(formularioPadre);

                    // Al cerrar el login, cerramos la máscara
                    mask.Close();

                    return result == DialogResult.OK;
                }
            }
        }
    }
}