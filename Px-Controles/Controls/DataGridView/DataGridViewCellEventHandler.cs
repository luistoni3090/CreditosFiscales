using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Px_Controles.Controls.DataGridView
{
    /// <summary>
    /// Delegate DataGridViewEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="DataGridViewEventArgs" /> instance containing the event data.</param>
    [Serializable]
    [ComVisible(true)]
    public delegate void DataGridViewEventHandler(object sender, DataGridViewEventArgs e);

    /// <summary>
    /// Delegate DataGridViewRowCustomEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="DataGridViewRowCustomEventArgs" /> instance containing the event data.</param>
    [Serializable]
    [ComVisible(true)]
    public delegate void DataGridViewRowCustomEventHandler(object sender, DataGridViewRowCustomEventArgs e);
}
