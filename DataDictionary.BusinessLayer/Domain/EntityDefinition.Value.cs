using DataDictionary.DataLayer.DomainData.Entity;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IEntityDefinitionValue : IDomainEntityDefinitionItem, IDefinitionIndex, IEntityIndex
    { }

    /// <inheritdoc/>
    public class EntityDefinitionValue : DomainEntityDefinitionItem, IEntityDefinitionValue
    {
        /// <inheritdoc/>
        public EntityDefinitionValue() : base() { }

        /// <inheritdoc/>
        public EntityDefinitionValue(IEntityIndex EntityKey) : base(EntityKey) { }
    }
}
