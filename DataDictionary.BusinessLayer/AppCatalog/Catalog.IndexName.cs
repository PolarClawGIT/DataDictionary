using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface ICatalogIndexName : ICatalogKeyName
    { }

    /// <inheritdoc/>
    public class CatalogIndexName : CatalogKeyName, ICatalogIndexName,
        IKeyEquality<ICatalogIndexName>, IKeyEquality<CatalogIndexName>
    {
        /// <inheritdoc cref="CatalogKeyName(ICatalogKeyName)"/>
        public CatalogIndexName(ICatalogIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ICatalogIndexName? other)
        { return other is ICatalogKeyName value && Equals(new CatalogKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(CatalogIndexName? other)
        { return other is ICatalogKeyName value && Equals(new CatalogKeyName(value)); }
    }
}
