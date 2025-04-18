﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Controles.Controls.DataGridView
{
    /// <summary>
    /// Class DataGridViewRowCustomEventArgs.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class DataGridViewRowCustomEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the name of the event.
        /// </summary>
        /// <value>The name of the event.</value>
        public string EventName { get; set; }

        public object Data { get; set; }

    }
}
