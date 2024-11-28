using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Table Column
    /// </summary>
    public interface ITableColumnItem : ITableColumn, ITableColumnKey, ICatalogKey, IDbTableType
    { }

    /// <summary>
    /// Implementation of the Database Table Column
    /// </summary>
    [Serializable]
    public class TableColumnItem : BindingTableRow, ITableColumnItem, INotifyPropertyChanged, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>(nameof(CatalogId)); } }

        /// <inheritdoc/>
        public Guid? ColumnId { get { return GetValue<Guid>(nameof(ColumnId)); } }

        /// <inheritdoc/>
        public String? DatabaseName { get { return GetValue(nameof(DatabaseName)); } }

        /// <inheritdoc/>
        public String? SchemaName { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public String? TableName { get { return GetValue(nameof(TableName)); } }

        /// <inheritdoc/>
        String? ITableColumn.TableType { get { return GetValue(nameof(TableType)); } }

        /// <inheritdoc/>
        public String? ColumnName { get { return GetValue(nameof(ColumnName)); } }

        /// <inheritdoc/>
        public Int32? OrdinalPosition { get { return GetValue<int>(nameof(OrdinalPosition)); } }

        /// <inheritdoc/>
        public Boolean? IsNullable { get { return GetValue<bool>(nameof(IsNullable), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public String? DataType { get { return GetValue(nameof(DataType)); } }

        /// <inheritdoc/>
        public String? ColumnDefault { get { return GetValue(nameof(ColumnDefault)); } }

        /// <inheritdoc/>
        public Int32? CharacterMaximumLength { get { return GetValue<int>(nameof(CharacterMaximumLength)); } }

        /// <inheritdoc/>
        public Int32? CharacterOctetLength { get { return GetValue<int>(nameof(CharacterOctetLength)); } }

        /// <inheritdoc/>
        public Byte? NumericPrecision { get { return GetValue<byte>(nameof(NumericPrecision)); } }

        /// <inheritdoc/>
        public Int16? NumericPrecisionRadix { get { return GetValue<short>(nameof(NumericPrecisionRadix)); } }

        /// <inheritdoc/>
        public Int32? NumericScale { get { return GetValue<int>(nameof(NumericScale)); } }

        /// <inheritdoc/>
        public Int16? DateTimePrecision { get { return GetValue<short>(nameof(DateTimePrecision)); } }

        /// <inheritdoc/>
        public String? CharacterSetCatalog { get { return GetValue(nameof(CharacterSetCatalog)); } }

        /// <inheritdoc/>
        public String? CharacterSetSchema { get { return GetValue(nameof(CharacterSetSchema)); } }

        /// <inheritdoc/>
        public String? CharacterSetName { get { return GetValue(nameof(CharacterSetName)); } }

        /// <inheritdoc/>
        public String? CollationCatalog { get { return GetValue(nameof(CollationCatalog)); } }

        /// <inheritdoc/>
        public String? CollationSchema { get { return GetValue(nameof(CollationSchema)); } }

        /// <inheritdoc/>
        public String? CollationName { get { return GetValue(nameof(CollationName)); } }

        /// <inheritdoc/>
        public String? DomainCatalog { get { return GetValue(nameof(DomainCatalog)); } }

        /// <inheritdoc/>
        public String? DomainSchema { get { return GetValue(nameof(DomainSchema)); } }

        /// <inheritdoc/>
        public String? DomainName { get { return GetValue(nameof(DomainName)); } }

        /// <inheritdoc/>
        public Boolean? IsIdentity { get { return GetValue<bool>(nameof(IsIdentity), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsHidden { get { return GetValue<bool>(nameof(IsHidden), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsComputed { get { return GetValue<bool>(nameof(IsComputed), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public String? ComputedDefinition { get { return GetValue(nameof(ComputedDefinition)); } }

        /// <inheritdoc/>
        public String? GeneratedAlwayType { get { return GetValue(nameof(GeneratedAlwayType)); } }

        /// <inheritdoc/>
        public DbTableType TableType
        {
            get
            {
                String? value = GetValue(nameof(TableType));
                if (DbTableEnumeration.TryParse(value, null, out DbTableEnumeration? result))
                { return result.Value; }
                else { return DbTableType.Null; }
            }
        }


        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(CatalogId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(ColumnId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(TableName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(TableType), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(ColumnName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(OrdinalPosition), typeof(int)){ AllowDBNull = false},
            new DataColumn(nameof(IsNullable), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(DataType), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(ColumnDefault), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterMaximumLength), typeof(int)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterOctetLength), typeof(int)){ AllowDBNull = true},
            new DataColumn(nameof(NumericPrecision), typeof(byte)){ AllowDBNull = true},
            new DataColumn(nameof(NumericPrecisionRadix), typeof(short)){ AllowDBNull = true},
            new DataColumn(nameof(NumericScale), typeof(int)){ AllowDBNull = true},
            new DataColumn(nameof(DateTimePrecision), typeof(short)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetCatalog), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetSchema), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetName), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CollationCatalog), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CollationSchema), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CollationName), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DomainCatalog), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DomainSchema), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DomainName), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(IsIdentity), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsHidden), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsComputed), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(ComputedDefinition), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(GeneratedAlwayType), typeof(string)){ AllowDBNull = true},
        };

        /// <summary>
        /// Constructor for the Database Table Column
        /// </summary>
        public TableColumnItem() : base() { }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for the Database Table Column
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected TableColumnItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new TableColumnKeyName(this).ToString(); }
    }
}
