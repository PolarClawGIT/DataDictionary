using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Attribute;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributeItem : IDomainAttributeItem, IAttributeKey
    { }

    /// <inheritdoc/>
    public class AttributeItem : DomainAttributeItem, IAttributeItem
    {
        /// <inheritdoc/>
        public AttributeItem() : base() { }

        internal XElement? GetXElement(IEnumerable<ElementItem>? options = null)
        {
            XElement? result = new XElement(this.Scope.ToName());

            if (options is not null)
            {
                foreach (ElementItem option in options)
                {
                    Object? value = null;

                    switch (option.ColumnName)
                    {
                        case nameof(this.AttributeId): value = AttributeId.ToString(); break;
                        case nameof(this.AttributeTitle): value = AttributeTitle; break;
                        case nameof(this.AttributeDescription): value = AttributeDescription; break;
                        case nameof(this.IsCompositeType): value = IsCompositeType; break;
                        case nameof(this.IsDerived): value = IsDerived; break;
                        case nameof(this.IsIntegral): value = IsIntegral; break;
                        case nameof(this.IsKey): value = IsKey; break;
                        case nameof(this.IsMultiValue): value = IsMultiValue; break;
                        case nameof(this.IsNonKey): value = IsNonKey; break;
                        case nameof(this.IsNullable): value = IsNullable; break;
                        case nameof(this.IsSimpleType): value = IsSimpleType; break;
                        case nameof(this.IsSingleValue): value = IsSingleValue; break;
                        case nameof(this.IsValued): value = IsValued; break;
                        default:
                            break;
                    }

                    result.Add(option.GetXElement(value));
                }
            }

            return result;
        }

        internal static IReadOnlyList<ColumnItem> GetXColumns()
        {
            ScopeType scope = ScopeType.ModelAttribute;
            IAttributeItem attributeNames;
            List<ColumnItem> result = new List<ColumnItem>()
            {
                new ColumnItem() {ColumnName = nameof(attributeNames.AttributeId),          DataType = typeof(Guid),    AllowDBNull = false, Scope = scope},
                new ColumnItem() {ColumnName = nameof(attributeNames.AttributeTitle),       DataType = typeof(String),  AllowDBNull = false, Scope = scope},
                new ColumnItem() {ColumnName = nameof(attributeNames.AttributeDescription), DataType = typeof(String),  AllowDBNull = true,  Scope = scope},

              //new ColumnItem() {ColumnName = nameof(attributeNames.IsCompositeType),      DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},
                new ColumnItem() {ColumnName = nameof(attributeNames.IsSimpleType),         DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},

                new ColumnItem() {ColumnName = nameof(attributeNames.IsDerived),            DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},
              //new ColumnItem() {ColumnName = nameof(attributeNames.IsIntegral),           DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},

                new ColumnItem() {ColumnName = nameof(attributeNames.IsKey),                DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},
                //new ColumnItem() {ColumnName = nameof(attributeNames.IsNonKey),             DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},

              //new ColumnItem() {ColumnName = nameof(attributeNames.IsMultiValue),         DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},
                new ColumnItem() {ColumnName = nameof(attributeNames.IsSingleValue),        DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},

                new ColumnItem() {ColumnName = nameof(attributeNames.IsNullable),           DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},
              //new ColumnItem() {ColumnName = nameof(attributeNames.IsValued),             DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},
            };

            return result;
        }
    }
}
