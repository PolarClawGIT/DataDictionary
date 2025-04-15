using DataDictionary.DataLayer.AppModel;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IAliasIndexName : IAliasKeyName
    { }

    /// <inheritdoc/>
    public class AliasIndexName : AliasKeyName, IAliasIndexName
    {
        /// <inheritdoc cref="AliasKeyName(IAliasKeyName)"/>
        public AliasIndexName(IAliasIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(AliasIndexName? other)
        { return other is IAliasIndexName key && Equals(new AliasIndexName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(IAliasIndexName? other)
        { return other is IAliasIndexName key && Equals(new AliasIndexName(key)); }

        /// <inheritdoc cref="AliasKeyName(IAttributeAliasItem)"/>
        public AliasIndexName(IAttributeAliasValue alias) : base(alias)
        { }

        /// <inheritdoc cref="AliasKeyName(IEntityAliasItem)"/>
        public AliasIndexName(IEntityAliasValue alias) : base(alias)
        { }

        /// <inheritdoc cref="AliasKeyName(IEntityAttributeItem)"/>
        public AliasIndexName(IEntityAttributeValue_Old alias) : base(alias)
        { }
    }
}
