using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface ICatalogIndex : ICatalogKey
    { }

    /// <inheritdoc/>
    public class CatalogIndex : CatalogKey, ICatalogIndex,
        IKeyEquality<ICatalogIndex>, IKeyEquality<CatalogIndex>
    {
        /// <inheritdoc cref="CatalogKey(ICatalogKey)"/>
        public CatalogIndex(ICatalogIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ICatalogIndex? other)
        { return other is ICatalogKey value && Equals(new CatalogKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(CatalogIndex? other)
        { return other is ICatalogKey value && Equals(new CatalogKey(value)); }

        /// <summary>
        /// Convert CatalogIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(CatalogIndex source)
        { return new DataIndex() { SystemId = source.CatalogId ?? Guid.Empty }; }
    }


}
