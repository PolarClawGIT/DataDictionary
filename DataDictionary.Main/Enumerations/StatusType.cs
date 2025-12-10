using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Enumerations
{
    /// <summary>
    /// List of different Images used by Status within the UI
    /// </summary>
    enum StatusType
    {
        /// <summary>
        /// Status Ok, Green Checkmark
        /// </summary>
        Ok,

        /// <summary>
        /// Status In Error, Red X
        /// </summary>
        Error,

        /// <summary>
        /// Status Informational, Blue explnation mark
        /// </summary>
        Information,

        /// <summary>
        /// Status Invalid, Red explnation mark
        /// </summary>
        Invalid,

        /// <summary>
        /// Status No, Red No Entry
        /// </summary>
        No,


    }
}
