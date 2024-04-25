using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DomainData.Attribute;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributePropertyValue : IDomainAttributePropertyItem
    { }

    /// <inheritdoc/>
    public class AttributePropertyValue : DomainAttributePropertyItem, IAttributePropertyValue
    {
        /// <inheritdoc/>
        public AttributePropertyValue() : base() { }

        /// <inheritdoc/>
        public AttributePropertyValue(IAttributeIndex attributeKey) : base(attributeKey) { }

        /// <inheritdoc/>
        public AttributePropertyValue(IAttributeIndex attributeKey,
                                     IPropertyKey propertyKey,
                                     IDbExtendedPropertyItem value)
            : base(attributeKey, propertyKey, value) { }

        internal XElement? GetXElement(IPropertyItem property, IEnumerable<ElementValue>? options = null)
        {
            XElement? result = null;
            IAttributePropertyValue attributeNames;
            IPropertyItem propertyNames;

            if (options is not null)
            {
                foreach (ElementValue option in options)
                {
                    Object? value = null;

                    switch (option.ColumnName)
                    {
                        case nameof(propertyNames.PropertyTitle): value = property.PropertyTitle; break;
                        case nameof(propertyNames.ExtendedProperty): value = property.ExtendedProperty; break;
                        case nameof(attributeNames.PropertyValue): value = PropertyValue; break;
                        case nameof(attributeNames.DefinitionText): value = DefinitionText; break;
                        default:
                            break;
                    }

                    if (value is not null)
                    { result = new XElement(Scope.ToName(), option.GetXElement(value)); }
                }
            }

            return result;
        }

        internal static IReadOnlyList<ColumnValue> GetXColumns()
        {
            ScopeType scope = ScopeType.ModelAttributeProperty;
            IAttributePropertyValue attributeNames;
            IPropertyItem propertyNames;
            List<ColumnValue> result = new List<ColumnValue>()
            {
                new ColumnValue() {ColumnName = nameof(propertyNames.PropertyTitle),    DataType = typeof(String), AllowDBNull = false, Scope = scope},
                new ColumnValue() {ColumnName = nameof(propertyNames.ExtendedProperty), DataType = typeof(String), AllowDBNull = true,  Scope = scope},
                new ColumnValue() {ColumnName = nameof(attributeNames.PropertyValue),   DataType = typeof(String), AllowDBNull = true,  Scope = scope},
                new ColumnValue() {ColumnName = nameof(attributeNames.DefinitionText),  DataType = typeof(String), AllowDBNull = true,  Scope = scope},
            };

            return result;
        }
    }
}
