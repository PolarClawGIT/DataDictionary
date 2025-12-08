using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IAttributePropertyValue : IAttributePropertyItem,
        IPropertyIndex, IAttributeIndex, IPropertySubType,
        IScopeType
    { }

    /// <inheritdoc/>
    public partial class AttributePropertyValue : AttributePropertyItem, IAttributePropertyValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelAttributeProperty; } }

        /// <inheritdoc cref="AttributePropertyItem.AttributePropertyItem()"/>
        public AttributePropertyValue() : base() { }

        /// <inheritdoc cref="AttributePropertyItem.AttributePropertyItem(IAttributeKey)"/>
        public AttributePropertyValue(IAttributeIndex attribute) : base(attribute) { }

        /// <inheritdoc cref="AttributePropertyItem.AttributePropertyItem(IAttributeKey, IPropertyKey)"/>
        public AttributePropertyValue(IAttributeIndex attribute, IPropertyIndex property) : base(attribute, property)
        { }
    }
}
