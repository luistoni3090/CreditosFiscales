//      Wero MX
//Autor:      Marco Antonio Acuña Rosas
//Nombre:     DTConvierte.cs
//Creación:   2024.05.25
//Ult Mod:    2024.05.25
//Descripción:
// Realiza conversións con data tables
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Utiles.DataTables
{
    public static class DTConvierte
    {

        /// <summary>
        /// Convierte un datatable en una lista de T
        /// </summary>
        /// <typeparam name="T">Estructura a convertir</typeparam>
        /// <param name="PoDT">DataTable a convertir</param>
        /// <returns>Una lista con el tipo de datos de <T> enviado</returns>
        /// <remarks>Wero MX</remarks>
        public static List<T> AListaDe<T>(this DataTable PoDT)
        {
            var columnNames = PoDT.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName)
                    .ToList();
            var properties = typeof(T).GetProperties();
            return PoDT.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name))
                    {
                        PropertyInfo pI = objT.GetType().GetProperty(pro.Name);

                        if (pro.PropertyType.Name == "Byte[]")
                        {
                            try
                            {
                                string val = (string)row[pro.Name];

                                if (val != null)
                                {
                                    byte[] byteBLOBData = (Byte[])Convert.FromBase64String(val);
                                    pro.SetValue(objT, byteBLOBData, null);
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        else
                        {
                            pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : Convert.ChangeType(row[pro.Name], pI.PropertyType));

                        }

                    }
                }
                return objT;
            }).ToList();
        }


        public static T ARegDe<T>(this DataTable PoDT) where T : new()
        {
            List<T> Lista = PoDT.AListaDe<T>();

            T Res = new T();

            foreach (T pro in Lista)
            {
                Res = pro;
            }

            return Res;
        }



        /// <summary>
        /// Convierte un datarow a una estructura
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public static T RowAObjetoDe<T>(this DataRow dataRow) where T : new()
        {
            T item = new T();
            foreach (DataColumn column in dataRow.Table.Columns)
            {
                if (dataRow[column] != DBNull.Value)
                {
                    PropertyInfo prop = item.GetType().GetProperty(column.ColumnName);
                    if (prop != null)
                    {
                        if (prop.PropertyType.Name == "Byte[]")
                        {
                            try
                            {
                                string val = (string)dataRow[column.ColumnName];

                                if (val != null)
                                {
                                    byte[] byteBLOBData = (Byte[])Convert.FromBase64String(val);

                                    //Byte[] byteBLOBData = new Byte[0];
                                    //byteBLOBData = (Byte[])(dataRow[column.ColumnName]);

                                    prop.SetValue(item, byteBLOBData, null);
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        else
                        {
                            object result = Convert.ChangeType(dataRow[column], prop.PropertyType);
                            prop.SetValue(item, result, null);

                        }

                        continue;
                    }
                    else
                    {
                        FieldInfo fld = item.GetType().GetField(column.ColumnName);
                        if (fld != null)
                        {
                            object result = Convert.ChangeType(dataRow[column], fld.FieldType);
                            fld.SetValue(item, result);
                        }
                    }
                }
            }
            return item;
        }

    }
}
