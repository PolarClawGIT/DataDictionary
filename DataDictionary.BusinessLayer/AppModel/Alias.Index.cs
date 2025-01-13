using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.DataLayer.AppModel;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IAliasIndex : IAliasKey
    { }

    /// <inheritdoc/>
    public class AliasIndex : AliasKey, IAliasIndex
    {
        /// <inheritdoc cref="AliasKey(IAliasKey)"/>
        public AliasIndex(IAliasIndex source) : base(source) { }

        /// <summary>
        /// Converts a Catalog TableColumn into an AliasIndex
        /// </summary>
        /// <param name="tableColumn"></param>
        public AliasIndex(ITableColumnValue tableColumn) : base(tableColumn, tableColumn.Scope)
        { }

        /// <summary>
        /// Converts a Catalog Table into an AliasIndex
        /// </summary>
        /// <param name="tableValue"></param>
        public AliasIndex(ITableValue tableValue) : base(tableValue, tableValue.Scope)
        { }

        /// <inheritdoc/>
        public Boolean Equals(AliasIndex? other)
        { return other is IAliasIndex key && Equals(new AliasKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(IAliasIndex? other)
        { return other is IAliasIndex key && Equals(new AliasKey(key)); }
    }
}
