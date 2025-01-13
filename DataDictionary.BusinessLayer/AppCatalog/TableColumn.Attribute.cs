using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using System.Data;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <summary>
    /// Class that takes a Table Column and builds an AppModel.Attribute
    /// </summary>
    /// <remarks>Helper Class</remarks>
    public class TableColumnAttribute
    {
        ITableColumnValue sourceColumn; // Source Column

        /// <summary>
        /// The Attribute built from the sourceColumn
        /// </summary>
        public AttributeValue Attribute { get; private set; }

        /// <summary>
        /// List of Aliases for the sourceColumn
        /// </summary>
        public IEnumerable<IAttributeAliasValue> Aliases
        {
            get
            {
                List< AttributeAliasValue > result = new List<AttributeAliasValue>();
                foreach (ITableColumnValue item in GetAlias(sourceColumn))
                {
                    AliasIndex index = new AliasIndex(item);
                    result.Add(new AttributeAliasValue(Attribute, index));
                }
                return result;
            }
        }

        /// <summary>
        /// List of Properties for the sourceColumn
        /// </summary>
        public IEnumerable<IAttributePropertyValue> Properties
        {
            get
            {
                List<AttributePropertyValue> result = new List<AttributePropertyValue>();

                foreach (AppCatalog.IPropertyValue databaseProperty in GetCatalogProperty(sourceColumn))
                {
                    if (GetModelProperty(databaseProperty) is AppModel.IPropertyValue modelProperty)
                    {   result.Add(new AttributePropertyValue(Attribute, modelProperty) { PropertyValue = databaseProperty.PropertyValue }); }

                    if (databaseProperty.IsDescription && String.IsNullOrEmpty(Attribute.AttributeDescription))
                    { Attribute.AttributeDescription = databaseProperty.PropertyValue; }
                }
                return result;
            }
        }

        /// <inheritdoc cref="ITableColumnData.GetAlias(ITableColumnIndexName)"/>
        public required Func<ITableColumnIndexName, IEnumerable<ITableColumnValue>> GetAlias { get; init; }

        /// <inheritdoc cref="AppCatalog.IPropertyData.GetProperty(ITableColumnIndexName)"/>
        public required Func<ITableColumnIndexName, IEnumerable<AppCatalog.IPropertyValue>> GetCatalogProperty { get; init; }

        /// <inheritdoc cref="AppModel.IPropertyData.GetProperty(AppCatalog.IPropertyValue)"/>
        public required Func<AppCatalog.IPropertyValue, AppModel.IPropertyValue?> GetModelProperty { get; init; }

        /// <summary>
        /// Create a new TableColumnAttribute
        /// </summary>
        /// <param name="source"></param>
        public TableColumnAttribute(ITableColumnValue source)
        {
            sourceColumn = source;
            Attribute = new AttributeValue()
            {
                AttributeTitle = source.ColumnName,
                AttributeName = new TableColumnIndexName(source).ToString(),
                DataType = source.DataType
            };

            if (source.DatabaseType is DbType value)
            {
                DbTypeEnumeration item = DbTypeEnumeration.Cast(value);

                if (item.IsAlphaNumeric
                    && source.CharacterMaximumLength.HasValue
                    && source.CharacterMaximumLength.Value > 0)
                { Attribute.DataLength = source.CharacterMaximumLength; }

                if (item.IsNumeric)
                {
                    Attribute.DataPrecision = source.NumericPrecision;

                    if (item.IsFloatingPoint)
                    { Attribute.DataScale = source.NumericScale; }
                }
                if (item.IsDate)
                { Attribute.DataPrecision = source.DateTimePrecision; }

            }
            else
            {
                Attribute.DataLength = source.CharacterMaximumLength;
                Attribute.DataPrecision = source.NumericPrecision;
                Attribute.DataScale = source.NumericScale;
            }

            Attribute.IsNullable = source.IsNullable ?? false;
            Attribute.IsDerived = source.IsComputed ?? false;
        }   
    }
}
