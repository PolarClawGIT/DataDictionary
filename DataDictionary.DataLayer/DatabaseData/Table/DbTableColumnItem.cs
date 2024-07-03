// Ignore Spelling: Nullable

using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData.Table
{
    /// <summary>
    /// Interface for the Database Table Column
    /// </summary>
    public interface IDbTableColumnItem : IDbTableColumnKeyName, IDbTableColumnKey, IDbCatalogKey, IDbDomainReferenceKey, IDbColumn, IDbTableType, IScopeKey
    {
        /// <summary>
        /// Is the Column Nullable
        /// </summary>
        Boolean? IsNullable { get; }

        /// <summary>
        /// Column Default value
        /// </summary>
        String? ColumnDefault { get; }

        /// <summary>
        /// Is the Column an Identity
        /// </summary>
        Boolean? IsIdentity { get; }

        /// <summary>
        /// Is the Column Hidden
        /// </summary>
        Boolean? IsHidden { get; }

        /// <summary>
        /// Is the Column Computed
        /// </summary>
        Boolean? IsComputed { get; }

        /// <summary>
        /// If the Column is Computed, the Computed Definition
        /// </summary>
        String? ComputedDefinition { get; }

        /// <summary>
        /// Type of Always Generated Column. Used by System Version.
        /// </summary>
        String? GeneratedAlwayType { get; }
    }

    /// <summary>
    /// Implementation of the Database Table Column
    /// </summary>
    [Serializable]
    public class DbTableColumnItem : BindingTableRow, IDbTableColumnItem, INotifyPropertyChanged, IDbExtendedProperty, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>(nameof(CatalogId)); } }

        /// <inheritdoc/>
        public Guid? ColumnId { get { return GetValue<Guid>(nameof(ColumnId)); } }
        
        /// <inheritdoc/>
        public string? DatabaseName { get { return GetValue(nameof(DatabaseName)); } }

        /// <inheritdoc/>
        public string? SchemaName { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public string? TableName { get { return GetValue(nameof(TableName)); } }

        /// <inheritdoc/>
        public string? ColumnName { get { return GetValue(nameof(ColumnName)); } }

        /// <inheritdoc/>
        public int? OrdinalPosition { get { return GetValue<int>(nameof(OrdinalPosition)); } }

        /// <inheritdoc/>
        public bool? IsNullable { get { return GetValue<bool>(nameof(IsNullable), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public string? DataType { get { return GetValue(nameof(DataType)); } }

        /// <inheritdoc/>
        public string? ColumnDefault { get { return GetValue(nameof(ColumnDefault)); } }

        /// <inheritdoc/>
        public int? CharacterMaximumLength { get { return GetValue<int>(nameof(CharacterMaximumLength)); } }

        /// <inheritdoc/>
        public int? CharacterOctetLength { get { return GetValue<int>(nameof(CharacterOctetLength)); } }

        /// <inheritdoc/>
        public byte? NumericPrecision { get { return GetValue<byte>(nameof(NumericPrecision)); } }

        /// <inheritdoc/>
        public short? NumericPrecisionRadix { get { return GetValue<short>(nameof(NumericPrecisionRadix)); } }

        /// <inheritdoc/>
        public int? NumericScale { get { return GetValue<int>(nameof(NumericScale)); } }

        /// <inheritdoc/>
        public short? DateTimePrecision { get { return GetValue<short>(nameof(DateTimePrecision)); } }

        /// <inheritdoc/>
        public string? CharacterSetCatalog { get { return GetValue(nameof(CharacterSetCatalog)); } }

        /// <inheritdoc/>
        public string? CharacterSetSchema { get { return GetValue(nameof(CharacterSetSchema)); } }

        /// <inheritdoc/>
        public string? CharacterSetName { get { return GetValue(nameof(CharacterSetName)); } }

        /// <inheritdoc/>
        public string? CollationCatalog { get { return GetValue(nameof(CollationCatalog)); } }

        /// <inheritdoc/>
        public string? CollationSchema { get { return GetValue(nameof(CollationSchema)); } }

        /// <inheritdoc/>
        public string? CollationName { get { return GetValue(nameof(CollationName)); } }

        /// <inheritdoc/>
        public string? DomainCatalog { get { return GetValue(nameof(DomainCatalog)); } }

        /// <inheritdoc/>
        public string? DomainSchema { get { return GetValue(nameof(DomainSchema)); } }

        /// <inheritdoc/>
        public string? DomainName { get { return GetValue(nameof(DomainName)); } }

        /// <inheritdoc/>
        public bool? IsIdentity { get { return GetValue<bool>(nameof(IsIdentity), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public bool? IsHidden { get { return GetValue<bool>(nameof(IsHidden), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public bool? IsComputed { get { return GetValue<bool>(nameof(IsComputed), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public string? ComputedDefinition { get { return GetValue(nameof(ComputedDefinition)); } }

        /// <inheritdoc/>
        public string? GeneratedAlwayType { get { return GetValue(nameof(GeneratedAlwayType)); } }

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

        /// <inheritdoc/>
        public ScopeType Scope
        {
            get
            {
                switch (TableType)
                {
                    case DbTableType.Table: return ScopeType.DatabaseTableColumn;
                    case DbTableType.TemporalTable: return ScopeType.DatabaseTableColumn;
                    case DbTableType.HistoryTable: return ScopeType.DatabaseTableColumn;
                    case DbTableType.View: return ScopeType.DatabaseViewColumn;
                    default: return ScopeType.Null;
                }
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
        public DbTableColumnItem() : base() { }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <inheritdoc/>
        public virtual Command PropertyCommand(IConnection connection)
        {
            if (this.Scope.ToDbLevel() is IDbLevelElementKey scopeKey)
            {
                return new DbExtendedPropertyGetCommand(connection)
                {
                    CatalogId = CatalogId,
                    Level0Name = SchemaName,
                    Level0Type = scopeKey.CatalogScope.ToString(),
                    Level1Name = TableName,
                    Level1Type = scopeKey.ObjectScope.ToString(),
                    Level2Name = ColumnName,
                    Level2Type = scopeKey.ElementScope.ToString(),
                }.GetCommand();
            }
            else
            {
                Exception ex = new InvalidOperationException("Could not determine LevelType");
                ex.Data.Add(nameof(DatabaseName), DatabaseName);
                ex.Data.Add(nameof(SchemaName), SchemaName);
                ex.Data.Add(nameof(TableName), TableName);
                ex.Data.Add(nameof(ColumnName), ColumnName);
                throw ex;
            }
        }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for the Database Table Column
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DbTableColumnItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new DbTableColumnKeyName(this).ToString(); }
    }
}
