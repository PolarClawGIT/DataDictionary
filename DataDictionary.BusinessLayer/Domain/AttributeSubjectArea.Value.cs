using DataDictionary.BusinessLayer.Model;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.ModelData;

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

        /// <inheritdoc cref="DomainAttributeSubjectAreaItem(IDomainAttributeKey, IModelSubjectAreaKey)"/>
        public AttributeSubjectAreaValue(IAttributeIndex attribute, ISubjectAreaIndex subjectArea): base (attribute, subjectArea)
        { }
    }
}
