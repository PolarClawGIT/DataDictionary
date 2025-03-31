using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IEntitySubjectAreaValue : IEntitySubjectAreaItem, IEntityIndex, ISubjectAreaIndex,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public class EntitySubjectAreaValue : EntitySubjectAreaItem, IEntitySubjectAreaValue
    {
        /// <inheritdoc/>
        public DataIndex Index => throw new NotImplementedException();

        /// <inheritdoc/>
        public String Title => throw new NotImplementedException();

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelEntitySubjectArea; } }

        /// <inheritdoc/>
        public EntitySubjectAreaValue() : base() { }

        /// <inheritdoc cref="EntitySubjectAreaItem(IEntityKey, ISubjectAreaKey)"/>
        public EntitySubjectAreaValue(IEntityIndex Entity, ISubjectAreaIndex subjectArea) : base(Entity, subjectArea)
        { }


    }
}
