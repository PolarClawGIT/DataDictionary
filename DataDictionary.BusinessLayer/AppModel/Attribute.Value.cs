using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using System.Data;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IAttributeValue : IAttributeItem, IAttributeIndex, IAttributeIndexName,
        IScopeType, ITemporalValue
    { }

    /// <inheritdoc/>
    public class AttributeValue : AttributeItem, IAttributeValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelAttribute; } }

        /// <inheritdoc/>
        public AttributeValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new AttributeIndex(this),
                GetPath = () =>
                {
                    if (String.IsNullOrWhiteSpace(AttributeName))
                    { return new PathIndex(AttributeTitle); }
                    else { return new PathIndex(new PathIndex(PathIndex.Parse(AttributeName).ToArray())); }
                },
                GetScope = () => Scope,
                GetTitle = () => AttributeTitle ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(AttributeTitle) or nameof(AttributeName),
                IsTitleChanged = (e) => e.PropertyName is nameof(AttributeTitle)
            };
        }

        /// <summary>
        /// Builds an Attribute from a TableColumn
        /// </summary>
        /// <param name="source"></param>
        public AttributeValue(AppCatalog.ITableColumnValue source) : this()
        {
            AttributeTitle = source.ColumnName;
            AttributeName = new AppCatalog.TableColumnIndexName(source).ToString();

            DataType = source.DataType;

            if (source.DatabaseType is DbType value)
            {
                DbTypeEnumeration item = DbTypeEnumeration.Cast(value);

                if (item.IsAlphaNumeric
                    && source.CharacterMaximumLength.HasValue
                    && source.CharacterMaximumLength.Value > 0)
                { DataLength = source.CharacterMaximumLength; }

                if (item.IsNumeric)
                {
                    DataPrecision = source.NumericPrecision;

                    if (item.IsFloatingPoint)
                    { DataScale = source.NumericScale; }
                }
                if (item.IsDate)
                { DataPrecision = source.DateTimePrecision; }

            }
            else
            {
                DataLength = source.CharacterMaximumLength;
                DataPrecision = source.NumericPrecision;
                DataScale = source.NumericScale;
            }

            IsNullable = source.IsNullable ?? false;
            IsDerived = source.IsComputed ?? false;

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
