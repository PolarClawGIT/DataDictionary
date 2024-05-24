using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Attribute;
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

        /// <inheritdoc/>
        public XElement? GetXElement(IDomainData data, IEnumerable<SchemaElementValue>? options)
        {
            XElement? result = null;

            if (options is not null && options.Count() > 0)
            {
                AttributeIndex key = new AttributeIndex(this);

                foreach (SchemaElementValue option in options)
                {
                    Object? value = null;

                    switch (option.ColumnName)
                    {
                        case nameof(AttributeId): value = AttributeId.ToString(); break;
                        case nameof(AttributeTitle): value = AttributeTitle; break;
                        case nameof(AttributeDescription): value = AttributeDescription; break;
                        case nameof(IsCompositeType): value = IsCompositeType; break;
                        case nameof(IsDerived): value = IsDerived; break;
                        case nameof(IsIntegral): value = IsIntegral; break;
                        case nameof(IsKey): value = IsKey; break;
                        case nameof(IsMultiValue): value = IsMultiValue; break;
                        case nameof(IsNonKey): value = IsNonKey; break;
                        case nameof(IsNullable): value = IsNullable; break;
                        case nameof(IsSimpleType): value = IsSimpleType; break;
                        case nameof(IsSingleValue): value = IsSingleValue; break;
                        case nameof(IsValued): value = IsValued; break;
                        default:
                            break;
                    }

                    if (value is not null)
                    {
                        if (result is null) { result = new XElement(this.Scope.ToName()); }
                        result.Add(option.GetXElement(value));
                    }
                }

                foreach (AttributePropertyValue item in data.DomainModel.Attributes.Properties.Where(w => key.Equals(w)))
                {
                    if(item.GetXElement(data.DomainModel.ModelProperty, options) is XElement value)
                    {
                        if (result is null) { result = new XElement(this.Scope.ToName()); }
                        result.Add(value);
                    }
                }

                foreach (AttributeAliasValue item in data.DomainModel.Attributes.Aliases.Where(w => key.Equals(w)))
                {
                    if (item.GetXElement(this, options) is XElement value)
                    {
                        if (result is null) { result = new XElement(this.Scope.ToName()); }
                        result.Add(value);
                    }
                }
            }

            return result;
        }

        internal static IReadOnlyList<ColumnValue> GetXColumns()
        {
            ScopeType scope = ScopeType.ModelAttribute;
            IAttributeValue attributeNames;
            List<ColumnValue> result = new List<ColumnValue>()
            {
                new ColumnValue() {ColumnName = nameof(attributeNames.AttributeId),          DataType = typeof(Guid),    AllowDBNull = false, Scope = scope},
                new ColumnValue() {ColumnName = nameof(attributeNames.AttributeTitle),       DataType = typeof(String),  AllowDBNull = false, Scope = scope},
                new ColumnValue() {ColumnName = nameof(attributeNames.AttributeDescription), DataType = typeof(String),  AllowDBNull = true,  Scope = scope},

              //new ColumnItem() {ColumnName = nameof(attributeNames.IsCompositeType),      DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},
                new ColumnValue() {ColumnName = nameof(attributeNames.IsSimpleType),         DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},

                new ColumnValue() {ColumnName = nameof(attributeNames.IsDerived),            DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},
              //new ColumnItem() {ColumnName = nameof(attributeNames.IsIntegral),           DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},

                new ColumnValue() {ColumnName = nameof(attributeNames.IsKey),                DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},
                //new ColumnItem() {ColumnName = nameof(attributeNames.IsNonKey),             DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},

              //new ColumnItem() {ColumnName = nameof(attributeNames.IsMultiValue),         DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},
                new ColumnValue() {ColumnName = nameof(attributeNames.IsSingleValue),        DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},

                new ColumnValue() {ColumnName = nameof(attributeNames.IsNullable),           DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},
              //new ColumnItem() {ColumnName = nameof(attributeNames.IsValued),             DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},
            };

            return result;
        }

    }
}
