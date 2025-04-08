/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 ColorExt.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Clase para re establecimiento de colores

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Controles.Colors
{
    /// <summary>
    /// Clase ColorExt.
    /// </summary>
    public static class ColorExt
    {
        #region Restablecer colores integrados
        /// <summary>
        /// Restablecer colores integrados.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <param name="light">Color light.</param>
        /// <param name="medium">Color medium.</param>
        /// <param name="dark">Color dark.</param>
        public static void ResetColor(
            this BasisColors type,
            Color light,
            Color medium,
            Color dark)
        {
            BasisColors.Light = light;
            BasisColors.Medium = medium;
            BasisColors.Dark = dark;
        }

        /// <summary>
        /// Restablecer colores.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <param name="green">Color green.</param>
        /// <param name="blue">Color blue.</param>
        /// <param name="red">Color red.</param>
        /// <param name="yellow">Color yellow.</param>
        public static void ResetColor(
            this BorderColors type,
            Color green,
            Color blue,
            Color red,
            Color yellow)
        {
            BorderColors.Green = green;
            BorderColors.Blue = blue;
            BorderColors.Red = red;
            BorderColors.Yellow = yellow;
        }

        /// <summary>
        /// Restablecer colores.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <param name="orange">Color orange.</param>
        /// <param name="lightGreen">Color light green.</param>
        /// <param name="green">Color green.</param>
        /// <param name="blue">Color blue.</param>
        /// <param name="blueGreen">Color blue green.</param>
        /// <param name="lightViolet">Color light violet.</param>
        /// <param name="violet">Color violet.</param>
        /// <param name="gray">Color gray.</param>
        public static void ResetColor(
            this GradientColors type,
            Color[] orange,
            Color[] lightGreen,
            Color[] green,
            Color[] blue,
            Color[] blueGreen,
            Color[] lightViolet,
            Color[] violet,
            Color[] gray
            )
        {
            if (orange != null && orange.Length == 2)
                GradientColors.Orange = orange;
            if (orange != null && orange.Length == 2)
                GradientColors.LightGreen = lightGreen;
            if (orange != null && orange.Length == 2)
                GradientColors.Green = green;
            if (orange != null && orange.Length == 2)
                GradientColors.Blue = blue;
            if (orange != null && orange.Length == 2)
                GradientColors.BlueGreen = blueGreen;
            if (orange != null && orange.Length == 2)
                GradientColors.LightViolet = lightViolet;
            if (orange != null && orange.Length == 2)
                GradientColors.Violet = violet;
            if (orange != null && orange.Length == 2)
                GradientColors.Gray = gray;
        }
        /// <summary>
        /// Restablecer colores.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <param name="moreLight">Color more light.</param>
        /// <param name="light">Color light.</param>
        /// <param name="dark">Color dark.</param>
        /// <param name="moreDark">Color more dark.</param>
        public static void ResetColor(
            this LineColors type,
            Color moreLight,
            Color light,
            Color dark,
            Color moreDark)
        {
            LineColors.MoreLight = moreLight;
            LineColors.Light = light;
            LineColors.Dark = dark;
            LineColors.MoreDark = moreDark;
        }
        /// <summary>
        /// Restablecer colores.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <param name="primary">Color primary.</param>
        /// <param name="success">Color success.</param>
        /// <param name="warning">Color warning.</param>
        /// <param name="danger">Color danger.</param>
        /// <param name="info">Información.</param>
        public static void ResetColor(
            this StatusColors type,
            Color primary,
            Color success,
            Color warning,
            Color danger,
            Color info
        )
        {
            StatusColors.Primary = primary;
            StatusColors.Success = success;
            StatusColors.Warning = warning;
            StatusColors.Danger = danger;
            StatusColors.Info = info;
        }
        /// <summary>
        /// Restablecer colores.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <param name="green">Color green.</param>
        /// <param name="blue">Color blue.</param>
        /// <param name="red">Color red.</param>
        /// <param name="yellow">Color yellow.</param>
        /// <param name="gray">Color gray.</param>
        public static void ResetColor(
            this TableColors type,
            Color green,
            Color blue,
            Color red,
            Color yellow,
            Color gray
       )
        {
            TableColors.Green = green;
            TableColors.Blue = blue;
            TableColors.Red = red;
            TableColors.Yellow = yellow;
            TableColors.Gray = gray;
        }

        /// <summary>
        /// Restablecer colores.
        /// </summary>
        /// <param name="type">Tipo.</param>
        /// <param name="moreLight">Color more light.</param>
        /// <param name="light">Color light.</param>
        /// <param name="dark">Color dark.</param>
        /// <param name="moreDark">Color more dark.</param>
        public static void ResetColor(
            this TextColors type,
            Color moreLight,
            Color light,
            Color dark,
            Color moreDark)
        {
            TextColors.MoreLight = moreLight;
            TextColors.Light = light;
            TextColors.Dark = dark;
            TextColors.MoreDark = moreDark;
        }
        #endregion

        #region 获取一个内置颜色    English:Get a built-in color
        /// <summary>
        /// Descripción de la función: obtener un color incorporado Genérico
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="t">t Valor genérico</param>
        /// <returns>lista de colores</returns>
        public static Color[] GetInternalColor<T>(T t)
        {
            Type type = null;
            if (t is BasisColorsTypes)
            {
                type = typeof(BasisColors);
            }
            else if (t is BorderColorsTypes)
            {
                type = typeof(BorderColors);
            }
            else if (t is GradientColorsTypes)
            {
                type = typeof(GradientColors);
            }
            else if (t is LineColorsTypes)
            {
                type = typeof(LineColors);
            }
            else if (t is StatusColorsTypes)
            {
                type = typeof(StatusColors);
            }
            else if (t is TableColorsTypes)
            {
                type = typeof(TableColors);
            }
            else if (t is TextColorsTypes)
            {
                type = typeof(TextColors);
            }
            if (type == null)
                return new Color[] { Color.Empty };
            else
            {
                string strName = t.ToString();
                var pi = type.GetProperty(strName);
                if (pi == null)
                    return new Color[] { Color.Empty };
                else
                {
                    var c = pi.GetValue(null, null);
                    if (c == null)
                        return new Color[] { Color.Empty };
                    else if (c is Color[])
                        return (Color[])c;
                    else
                        return new Color[] { (Color)c };
                }
            }
        }
        #endregion
    }
}
