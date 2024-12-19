using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.DataLayer.DomainData.Entity;

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

        /// <inheritdoc cref="DomainEntitySubjectAreaItem(IDomainEntityKey, ISubjectAreaKey)"/>
        public EntitySubjectAreaValue(IEntityIndex Entity, ISubjectAreaIndex subjectArea) : base(Entity, subjectArea)
        { }
    }
}
