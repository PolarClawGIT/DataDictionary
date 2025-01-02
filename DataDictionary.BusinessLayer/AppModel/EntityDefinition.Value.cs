using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IEntityDefinitionValue : IEntityDefinitionItem, IDefinitionIndex, IEntityIndex,
        IScopeType, ITemporalValue
    { }

    /// <inheritdoc/>
    public class EntityDefinitionValue : EntityDefinitionItem, IEntityDefinitionValue
    {
        /// <inheritdoc/>
        public DataIndex Index => throw new NotImplementedException();

        /// <inheritdoc/>
        public String Title => throw new NotImplementedException();

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelEntityDefinition; } }

        /// <inheritdoc/>
        public EntityDefinitionValue() : base() { }

        /// <inheritdoc/>
        public EntityDefinitionValue(IEntityIndex EntityKey) : base(EntityKey) { }
    }
}
