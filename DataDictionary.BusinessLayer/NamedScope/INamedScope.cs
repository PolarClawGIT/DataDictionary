using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.NamedScope
{
    /// <summary>
    /// Interface for Event OnTitleChanged
    /// </summary>
    public interface IOnTitleChanged
    {
        /// <summary>
        /// Event to fire when the Title or Path changes
        /// </summary>
        event EventHandler? OnTitleChanged;
    }
}
