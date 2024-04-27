<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{

    /// <inheritdoc/>
    public interface ISelectionItem : DataLayer.ScriptingData.Selection.ISelectionItem
    { }

    /// <inheritdoc/>
    public class SelectionItem : DataLayer.ScriptingData.Selection.SelectionItem, ISelectionItem
    {
        /// <inheritdoc/>
        public SelectionItem() : base() { }
=======
﻿using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ScriptingData.Selection;
using System.ComponentModel;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ISelectionValue : ISelectionItem, ISelectionIndex
    { }

    /// <inheritdoc/>
    public class SelectionValue : SelectionItem, ISelectionValue, INamedScopeValue
    {
        /// <inheritdoc/>
        public SelectionValue() : base()
        { PropertyChanged += SchemaValue_PropertyChanged; }

        /// <inheritdoc/>
        public virtual NamedScopeKey GetSystemId()
        { return new NamedScopeKey(SchemaId); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(Scope); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return SelectionTitle ?? String.Empty; }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
        private void SchemaValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(SelectionTitle)
                && OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
>>>>>>> RenameIndexValue
    }
}
