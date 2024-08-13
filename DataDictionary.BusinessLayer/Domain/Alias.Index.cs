using DataDictionary.BusinessLayer.Database;
using DataDictionary.DataLayer.DatabaseData.Table;
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

        /// <inheritdoc cref="AliasKey(IDbTableColumnItem)"/>
        public AliasIndex(ITableColumnValue source) : base(source) {  }

        /// <inheritdoc cref="AliasKey(IDbTableItem)"/>
        public AliasIndex(ITableValue source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(AliasIndex? other)
        { return other is IAliasIndex key && Equals(new AliasIndex(key)); }

        /// <inheritdoc/>
        public Boolean Equals(IAliasIndex? other)
        { return other is IAliasIndex key && Equals(new AliasIndex(key)); }
    }
}
