using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IAttributePropertyValue : IAttributePropertyItem, IPropertyIndex,
        IScopeType, ITemporalValue
    { }

    /// <inheritdoc/>
    public class AttributePropertyValue : AttributePropertyItem, IAttributePropertyValue, IScopeType
    {
        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelAttributeProperty; } }

        /// <inheritdoc/>
        public DataIndex Index => throw new NotImplementedException();

        /// <inheritdoc/>
        public String Title => throw new NotImplementedException();

        /// <inheritdoc/>
        public AttributePropertyValue() : base() { }

        /// <inheritdoc/>
        public AttributePropertyValue(IAttributeIndex attributeKey) : base(attributeKey) { }

        /// <inheritdoc/>
        public AttributePropertyValue(IAttributeKey attributeKey, DataLayer.AppModel.IPropertyKey propertyKey, DataLayer.AppCatalog.IPropertyItem value) : base(attributeKey, propertyKey, value)
        { }

        internal static IReadOnlyList<NodePropertyValue> GetXColumns()
        {
            ScopeType scope = ScopeType.ModelAttributeProperty;
            IAttributePropertyValue attributeNames;
            IPropertyValue propertyNames;
            List<NodePropertyValue> result = new List<NodePropertyValue>()
            {
                new NodePropertyValue() {PropertyName = nameof(propertyNames.PropertyTitle),  DataType = typeof(String), AllowDBNull = false, PropertyScope = scope},
                new NodePropertyValue() {PropertyName = nameof(propertyNames.PropertyType),   DataType = typeof(String), AllowDBNull = true,  PropertyScope = scope},
                new NodePropertyValue() {PropertyName = nameof(propertyNames.PropertyData),   DataType = typeof(String), AllowDBNull = true,  PropertyScope = scope},
                new NodePropertyValue() {PropertyName = nameof(attributeNames.PropertyValue), DataType = typeof(String), AllowDBNull = true,  PropertyScope = scope},
            };

            return result;
        }

    }
}
