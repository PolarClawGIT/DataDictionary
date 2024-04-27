using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Schema;
<<<<<<< HEAD
using System.ComponentModel;
=======
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> RenameIndexValue
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ISchemaValue : IDbSchemaItem, ISchemaIndex, ISchemaIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class SchemaValue : DbSchemaItem, ISchemaValue, INamedScopeValue
    {
        /// <inheritdoc cref="DbSchemaItem()"/>
        public SchemaValue() : base()
<<<<<<< HEAD
        { PropertyChanged += OnPropertyChanged; }

        /// <inheritdoc/>
        public NamedScopeKey GetSystemId()
=======
        { PropertyChanged += CatalogValue_PropertyChanged; }

        /// <inheritdoc/>
        public virtual NamedScopeKey GetSystemId()
>>>>>>> RenameIndexValue
        { return new NamedScopeKey(SchemaId); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(DatabaseName, SchemaName); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return SchemaName ?? String.Empty; }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
<<<<<<< HEAD
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(SchemaName) or nameof(DatabaseName)
=======
        private void CatalogValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(DatabaseName) or nameof(SchemaName)
>>>>>>> RenameIndexValue
                && OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
    }
}
