using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Px_CreditosFiscales.Utiles.Generales
{
    
    public static class ConfigHelper
    {
        public static void ActualizarAppSettings(string key, string value)
        {
            // Abrir el archivo de configuración del ejecutable
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Verificar si la clave existe
            if (config.AppSettings.Settings[key] != null)
            {
                // Actualizar el valor existente
                config.AppSettings.Settings[key].Value = value;
            }
            else
            {
                // Agregar una nueva clave
                config.AppSettings.Settings.Add(key, value);
            }

            // Guardar los cambios y refrescar la sección
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
