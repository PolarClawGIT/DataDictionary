using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ICatalogValue : IDbCatalogItem, ICatalogKey,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class CatalogValue : DbCatalogItem, ICatalogValue, INamedScopeValue
    {
        /// <inheritdoc cref="DbCatalogItem()"/>
        public CatalogValue() : base()
        { PropertyChanged += CatalogValue_PropertyChanged; }

        /// <inheritdoc/>
        public virtual NamedScopeKey GetSystemId()
        { return new NamedScopeKey() { SystemId = CatalogId ?? Guid.Empty }; }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(DatabaseName ?? String.Empty); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return CatalogTitle ?? String.Empty; }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
        private void CatalogValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(CatalogTitle) or nameof(DatabaseName)
                && OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
    }
}
