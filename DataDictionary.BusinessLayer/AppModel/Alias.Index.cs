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

        /// <inheritdoc/>
        public Boolean Equals(AliasIndex? other)
        { return other is IAliasIndex key && Equals(new AliasKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(IAliasIndex? other)
        { return other is IAliasIndex key && Equals(new AliasKey(key)); }
    }
}
