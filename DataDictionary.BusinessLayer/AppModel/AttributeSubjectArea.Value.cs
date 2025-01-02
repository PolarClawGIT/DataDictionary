using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IAttributeSubjectAreaValue : IAttributeSubjectAreaItem, IAttributeIndex, ISubjectAreaIndex,
        IScopeType, ITemporalValue
    { }

    /// <inheritdoc/>
    public class AttributeSubjectAreaValue : AttributeSubjectAreaItem, IAttributeSubjectAreaValue, IScopeType
    {
        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelAttributeSubjectArea; } }

        /// <inheritdoc/>
        public DataIndex Index => throw new NotImplementedException();

        /// <inheritdoc/>
        public String Title => throw new NotImplementedException();

        /// <inheritdoc/>
        public AttributeSubjectAreaValue() : base() { }

        /// <inheritdoc cref="AttributeSubjectAreaItem(IAttributeKey, ISubjectAreaKey)"/>
        public AttributeSubjectAreaValue(IAttributeIndex attribute, ISubjectAreaIndex subjectArea) : base(attribute, subjectArea)
        { }
    }
}
