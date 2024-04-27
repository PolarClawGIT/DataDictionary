<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
﻿using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ScriptingData.Schema;
using System.ComponentModel;
>>>>>>> RenameIndexValue

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
<<<<<<< HEAD
    public interface ISchemaItem : DataLayer.ScriptingData.Schema.ISchemaItem
    { }

    /// <inheritdoc/>
    public class SchemaItem : DataLayer.ScriptingData.Schema.SchemaItem, ISchemaItem
    {
        /// <inheritdoc/>
        public SchemaItem() : base() { }
=======
    public interface ISchemaValue : ISchemaItem
    { }

    /// <inheritdoc/>
    public class SchemaValue : SchemaItem, ISchemaValue, INamedScopeValue
    {
        /// <inheritdoc/>
        public SchemaValue() : base()
        { PropertyChanged += SchemaValue_PropertyChanged; }

        /// <inheritdoc/>
        public virtual NamedScopeKey GetSystemId()
        { return new NamedScopeKey(SchemaId); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(Scope); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return SchemaTitle ?? String.Empty; }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
        private void SchemaValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(SchemaTitle)
                && OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
>>>>>>> RenameIndexValue
    }
}
