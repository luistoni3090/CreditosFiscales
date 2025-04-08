using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Utiles.Cadenas
{
    public static class Cadena
    {


        /// <summary>
        /// Valida si la cadena indicada es decimal
        /// </summary>
        /// <param name="aNumber">cadena a validad</param>
        /// <returns>true si es decimal, falso si no</returns>
        public static bool IsDecimal(this string aNumber)
        {
            bool bExito = false;
            try
            {
                decimal xDec;
                bExito = decimal.TryParse(aNumber, out xDec);
            }
            catch { }

            return bExito;
        }

        /// <summary>
        /// Valida si la cadena indicada es entero
        /// </summary>
        /// <param name="aNumber">cadena a validad</param>
        /// <returns>true si es decimal, falso si no</returns>
        public static bool IsNumber(this string aNumber)
        {
            bool bExito = false;
            try
            {
                BigInteger temp_big_int;
                bExito = BigInteger.TryParse(aNumber, out temp_big_int);
            }
            catch { }
            return bExito;
        }
        /// <summary>
        /// Verifica si la cadena es del tipo solicitado
        /// Llamado
        /// "123".IsParseableAs<double>() 
        /// </summary>
        /// <typeparam name="TInput">Tipo de datos</typeparam>
        /// <param name="value">Cadena a combertir</param>
        /// <returns></returns>
        public static bool IsParseableAs<TInput>(this string value)
        {
            var type = typeof(TInput);

            var tryParseMethod = type.GetMethod("TryParse", BindingFlags.Static | BindingFlags.Public, Type.DefaultBinder,
                new[] { typeof(string), type.MakeByRefType() }, null);
            if (tryParseMethod == null) return false;

            var arguments = new[] { value, Activator.CreateInstance(type) };
            return (bool)tryParseMethod.Invoke(null, arguments);
        }

        /// <summary>
        /// Valida una cadena pasada con el tipo de dato solicitado
        /// Llamados
        /// "123".ParseAs<int>(10);
        /// "abc".ParseAs<int>(25);
        /// "123,78".ParseAs<double>(10);
        /// "abc".ParseAs<double>(107.4);
        /// "2014-10-28".ParseAs<DateTime>(DateTime.MinValue);
        /// "monday".ParseAs<DateTime>(DateTime.MinValue);
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TOutput ParseAs<TOutput>(this string value, TOutput defaultValue)
        {
            var type = typeof(TOutput);

            var tryParseMethod = type.GetMethod("TryParse", BindingFlags.Static | BindingFlags.Public, Type.DefaultBinder,
                new[] { typeof(string), type.MakeByRefType() }, null);
            if (tryParseMethod == null) return defaultValue;

            var arguments = new object[] { value, null };
            return ((bool)tryParseMethod.Invoke(null, arguments)) ? (TOutput)arguments[1] : defaultValue;
        }

    }
}
