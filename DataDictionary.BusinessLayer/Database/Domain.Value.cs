using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Domain;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IDomainValue : IDbDomainItem, IDomainIndex, IDomainIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class DomainValue : DbDomainItem, IDomainValue, INamedScopeValue
    {
        /// <inheritdoc cref="DbDomainItem()"/>
        public DomainValue() : base()
<<<<<<< HEAD
        { PropertyChanged += OnPropertyChanged; }

        /// <inheritdoc/>
        public NamedScopeKey GetSystemId()
=======
        { PropertyChanged += CatalogValue_PropertyChanged; }

        /// <inheritdoc/>
        public virtual NamedScopeKey GetSystemId()
>>>>>>> RenameIndexValue
        { return new NamedScopeKey(DomainId); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(DatabaseName, SchemaName, DomainName); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return DomainName ?? String.Empty; }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
<<<<<<< HEAD
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(SchemaName) or nameof(DatabaseName) or nameof(DomainName)
=======
        private void CatalogValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(DatabaseName) or nameof(SchemaName) or nameof(DomainName)
>>>>>>> RenameIndexValue
                && OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
    }
}
