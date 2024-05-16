﻿using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.LibraryData.Source;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Library
{
    /// <inheritdoc/>
    public interface ILibrarySourceValue : ILibrarySourceItem, ILibrarySourceIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class LibrarySourceValue : LibrarySourceItem, ILibrarySourceValue, INamedScopeSourceValue
    {
        /// <inheritdoc cref="LibrarySourceItem()"/>
        public LibrarySourceValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new DataLayerIndex(LibraryId); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(AssemblyName); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return LibraryTitle ?? Scope.ToName(); }
    }
}
