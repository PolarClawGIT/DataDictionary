using DataDictionary.BusinessLayer.Model;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.ModelData.SubjectArea;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IEntitySubjectAreaValue : IDomainEntitySubjectAreaItem, IEntityIndex, ISubjectAreaIndex
    { }

    /// <inheritdoc/>
    public class EntitySubjectAreaValue : DomainEntitySubjectAreaItem, IEntitySubjectAreaValue
    {
        /// <inheritdoc/>
        public EntitySubjectAreaValue() : base() { }

        /// <inheritdoc cref="DomainEntitySubjectAreaItem(IDomainEntityKey, IModelSubjectAreaKey)"/>
        public EntitySubjectAreaValue(IEntityIndex Entity, ISubjectAreaIndex subjectArea) : base(Entity, subjectArea)
        { }
    }
}
