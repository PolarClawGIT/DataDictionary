using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.DomainData;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAliasIndex : IAliasKey
    { }

    /// <inheritdoc/>
    public class AliasIndex: AliasKey, IAliasIndex
    {
        /// <inheritdoc cref="AliasKey(IAliasKey)"/>
        public AliasIndex(IAliasIndex source) : base(source) { }

        /// <inheritdoc cref="AliasKey(ITableColumnItem)"/>
        public AliasIndex(ITableColumnValue source) : base(source) {  }

        /// <inheritdoc cref="AliasKey(ITableItem)"/>
        public AliasIndex(ITableValue source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(AliasIndex? other)
        { return other is IAliasIndex key && Equals(new AliasKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(IAliasIndex? other)
        { return other is IAliasIndex key && Equals(new AliasKey(key)); }
    }
}
