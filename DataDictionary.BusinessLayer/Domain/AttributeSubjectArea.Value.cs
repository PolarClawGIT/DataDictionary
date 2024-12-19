using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.DataLayer.DomainData.Attribute;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributeSubjectAreaValue  : IDomainAttributeSubjectAreaItem, IAttributeIndex, ISubjectAreaIndex
    { }

    /// <inheritdoc/>
    public class AttributeSubjectAreaValue : DomainAttributeSubjectAreaItem, IAttributeSubjectAreaValue
    {
        /// <inheritdoc/>
        public AttributeSubjectAreaValue() : base () { }

        /// <inheritdoc cref="DomainAttributeSubjectAreaItem(IDomainAttributeKey, ISubjectAreaKey)"/>
        public AttributeSubjectAreaValue(IAttributeIndex attribute, ISubjectAreaIndex subjectArea): base (attribute, subjectArea)
        { }
    }
}
