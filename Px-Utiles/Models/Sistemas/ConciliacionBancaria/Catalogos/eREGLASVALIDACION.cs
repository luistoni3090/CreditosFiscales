/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 eMONEDA.cs
/// Creación: 	 2024.07.15
/// Ult Mod: 	 2024.07.15
/// Descripción:
/// Clase para estructura de la tabla MONEDA

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos
{
    public class eREGLASVALIDACION
    {
        public decimal BANCO { get; set; } = 0;
        public decimal CUENTA { get; set; } = 0;
        public decimal ORDEN { get; set; } = 0;
        public string DESCR { get; set; } = "";
        public string VALIDA { get; set; } = "";
        public string QUERY { get; set; } = "";
        public string REGRESA_QUERY { get; set; } = "";
        public string Nuevo_Editar { get; set; } = "";
        public string ALLCOLUMNS { get; set; } = "";
    }

    public class ComboBoxItem
    {
        public string Value { get; set; } = "";
        public string Text { get; set; } = "";
    }
}
