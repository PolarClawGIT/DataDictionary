using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.NamedScope
{
    /// <summary>
    /// Interface for the methods needed to support NamedScope
    /// </summary>
    public interface INamedScopeValue : IScopeKey
    {
        /// <summary>
        /// Get the System ID for the NamedScope
        /// </summary>
        /// <returns></returns>
        NamedScopeKey GetSystemId();

        /// <summary>
        /// Get the Title for the NamedScope
        /// </summary>
        /// <returns></returns>
        String GetTitle();

        /// <summary>
        /// Get the Path (NameSpace) for the NamedScope
        /// </summary>
        /// <returns></returns>
        NamedScopePath GetPath();

        /// <summary>
        /// Get the Position for the NamedScope. Default is zero.
        /// </summary>
        /// <returns></returns>
        Int32 GetPosition() { return 0; }

        /// <summary>
        /// Event to fire when the Title or Path changes
        /// </summary>
        event EventHandler? OnTitleChanged;
    }
}
