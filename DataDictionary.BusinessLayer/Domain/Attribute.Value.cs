using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributeValue : IDomainAttributeItem, IAttributeIndex, IAttributeIndexName
    { }

    /// <inheritdoc/>
    public class AttributeValue : DomainAttributeItem, IAttributeValue, IPathValue, INamedScopeSourceValue
    {
        /// <inheritdoc cref="DomainAttributeItem()"/>
        public AttributeValue() : base()
        { }

        /// <inheritdoc/>
        public IPathValue AsPathValue()
        {
            if (pathValue is null)
            {
                pathValue = new PathValue(this)
                {
                    GetIndex = () => new AttributeIndex(this),
                    GetPath = () =>
                    {
                        if (String.IsNullOrWhiteSpace(MemberName))
                        { return new PathIndex(AttributeTitle); }
                        else { return new PathIndex(new PathIndex(PathIndex.Parse(MemberName).ToArray())); }
                    },
                    GetScope = () => Scope,
                    GetTitle = () => AttributeTitle ?? ScopeEnumeration.Cast(Scope).Name,
                    IsPathChanged = (e) => e.PropertyName is nameof(AttributeTitle) or nameof(MemberName),
                    IsTitleChanged = (e) => e.PropertyName is nameof(AttributeTitle)
                };
            }

            return pathValue;
        }
        IPathValue? pathValue; // Backing field for AsPathValue

        internal static IReadOnlyList<NodePropertyValue> GetXColumns()
        {
            ScopeType scope = ScopeType.ModelAttribute;
            IAttributeValue attributeNames;
            List<NodePropertyValue> result = new List<NodePropertyValue>()
            {
                new NodePropertyValue() {PropertyName = nameof(attributeNames.AttributeId),          DataType = typeof(Guid),    AllowDBNull = false, PropertyScope = scope},
                new NodePropertyValue() {PropertyName = nameof(attributeNames.AttributeTitle),       DataType = typeof(String),  AllowDBNull = false, PropertyScope = scope},
                new NodePropertyValue() {PropertyName = nameof(attributeNames.AttributeDescription), DataType = typeof(String),  AllowDBNull = true,  PropertyScope = scope},

              //new ColumnItem() {ColumnName = nameof(attributeNames.IsCompositeType),      DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},
                new NodePropertyValue() {PropertyName = nameof(attributeNames.IsSimpleType),         DataType = typeof(Boolean), AllowDBNull = true,  PropertyScope = scope},

                new NodePropertyValue() {PropertyName = nameof(attributeNames.IsDerived),            DataType = typeof(Boolean), AllowDBNull = true,  PropertyScope = scope},
              //new ColumnItem() {ColumnName = nameof(attributeNames.IsIntegral),           DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},

                new NodePropertyValue() {PropertyName = nameof(attributeNames.IsKey),                DataType = typeof(Boolean), AllowDBNull = true,  PropertyScope = scope},
                //new ColumnItem() {ColumnName = nameof(attributeNames.IsNonKey),             DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},

              //new ColumnItem() {ColumnName = nameof(attributeNames.IsMultiValue),         DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},
                new NodePropertyValue() {PropertyName = nameof(attributeNames.IsSingleValue),        DataType = typeof(Boolean), AllowDBNull = true,  PropertyScope = scope},

                new NodePropertyValue() {PropertyName = nameof(attributeNames.IsNullable),           DataType = typeof(Boolean), AllowDBNull = true,  PropertyScope = scope},
              //new ColumnItem() {ColumnName = nameof(attributeNames.IsValued),             DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},
            };

            return result;
        }

    }
}
