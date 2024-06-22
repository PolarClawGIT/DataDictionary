using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.ScriptingData.Template;
using System.ComponentModel;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributeValue : IDomainAttributeItem, IAttributeIndex, IAttributeIndexName
    { }

    /// <inheritdoc/>
    public class AttributeValue : DomainAttributeItem, IAttributeValue, INamedScopeSourceValue, IScripting<IDomainData>
    {
        /// <inheritdoc cref="DomainAttributeItem()"/>
        public AttributeValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new AttributeIndex(this); }

        /// <inheritdoc/>
        public String GetTitle()
        { return AttributeTitle ?? Scope.ToName(); }

        /// <inheritdoc/>
        /// <remarks>Partial Path</remarks>
        public NamedScopePath GetPath()
        { return new NamedScopePath(AttributeTitle); }

        internal XElement? GetXElement(IEnumerable<TemplateNodeValue> templates)
        {
            XElement? result = null;

            foreach (TemplateNodeValue node in templates)
            {
                XObject? value = null;

                switch (node.PropertyName)
                {
                    case nameof(AttributeTitle): value = node.BuildXObject(AttributeTitle); break;
                    case nameof(AttributeDescription): value = node.BuildXObject(AttributeDescription); break;
                    case nameof(IsCompositeType): value = node.BuildXObject(IsCompositeType); break;
                    case nameof(IsDerived): value = node.BuildXObject(IsDerived); break; ;
                    case nameof(IsIntegral): value = node.BuildXObject(IsIntegral); break; ;
                    case nameof(IsKey): value = node.BuildXObject(IsKey); break; ;
                    case nameof(IsMultiValue): value = node.BuildXObject(IsMultiValue); break; ;
                    case nameof(IsNonKey): value = node.BuildXObject(IsNonKey); break; ;
                    case nameof(IsNullable): value = node.BuildXObject(IsNullable); break; ;
                    case nameof(IsSimpleType): value = node.BuildXObject(IsSimpleType); break; ;
                    case nameof(IsSingleValue): value = node.BuildXObject(IsSingleValue); break; ;
                    case nameof(IsValued): value = node.BuildXObject(IsValued); break; ;
                    default:
                        break;
                }

                if (value is XObject)
                {
                    if (result is null) { result = new XElement(Scope.ToName()); }
                    result.Add(value);
                }
            }

            return result;
        }

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
