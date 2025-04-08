using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Bancos.Utiles.Formas
{
    public  class FormUtils
    {
        public static void AttachKeyPressEventHandler(System.Windows.Forms.TextBox textBox)
        {
            textBox.KeyPress += new KeyPressEventHandler(NumericTextBox_KeyPress);
        }

        public static void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Validar RFC 
        /// </summary>
        /// <param name="rfc"></param>
        /// <returns></returns>
        public static bool ValidaRFC(string rfc)
        {
            rfc = rfc.Trim().ToUpper();

            string pattern = @"^[A-ZÑ&]{3,4}\d{6}[A-Z0-9]{3}$";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(rfc))
            {
                return false;
            }

            return true;
        }

    }
}
