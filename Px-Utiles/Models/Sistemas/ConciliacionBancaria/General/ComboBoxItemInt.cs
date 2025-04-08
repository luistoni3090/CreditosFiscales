using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.ConciliacionBancaria.General
{
    /// <summary>
    /// 
    /// </summary>
    public class ComboBoxItemInt
    {
        public Int64 Value { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
