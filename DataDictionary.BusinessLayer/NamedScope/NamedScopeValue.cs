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
    public interface INamedScopeValue : IScopeKey, IGetNamedScopeKey
    {
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
        /// Get the Path (NameSpace) for the NamedScope
        /// </summary>
        public NamedScopePath NamedPath { get { return GetPath(); } }

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

    /// <summary>
    /// Interface for Named
    /// </summary>
    interface IGetNamedScopes
    {
        /// <summary>
        /// Returns a list of NamedScopes
        /// </summary>
        /// <returns></returns>
        IEnumerable<NamedScopePair> GetNamedScopes();
    }

    /// <summary>
    /// Class to build NamedScope Pairs (Parent Key and Value).
    /// </summary>
    /// <remarks>This is just for constructing a list of parameters needed to load the NamedScopeData.</remarks>
    struct NamedScopePair
    {
        public NamedScopeIndex? ParentKey { get; } = null;
        public INamedScopeValue Value { get; }
        public Func<NamedScopePath> GetPath { get; init; }

        public NamedScopePair(INamedScopeValue value)
        {
            this.Value = value;
            GetPath = value.GetPath;
        }

        public NamedScopePair(NamedScopeIndex parent, INamedScopeValue value) : this(value)
        { this.ParentKey = parent; }
    }
}
