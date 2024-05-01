using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ScriptingData.Schema;
using System.ComponentModel;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ISchemaValue : ISchemaItem, ISchemaIndex
    { }

    /// <inheritdoc/>
    public class SchemaValue : SchemaItem, ISchemaValue, INamedScopeValue
    {
        /// <inheritdoc/>
        public SchemaValue() : base()
        { PropertyChanged += SchemaValue_PropertyChanged; }

        /// <inheritdoc/>
        public virtual NamedScopeKey GetKey()
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
    }
}
