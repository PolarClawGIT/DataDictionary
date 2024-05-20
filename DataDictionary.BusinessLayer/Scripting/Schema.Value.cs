﻿using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.ScriptingData.Schema;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ISchemaValue : ISchemaItem, ISchemaIndex, ISchemaIndexName, ISchemaKeyName
    { }

    /// <inheritdoc/>
    public class SchemaValue : SchemaItem, ISchemaValue, INamedScopeSourceValue
    {
        /// <inheritdoc/>
        public SchemaValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new SchemaIndex(this); }


        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(Scope); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return SchemaTitle ?? Scope.ToName(); }
    }
}
