using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData.Domain
{
    /// <summary>
    /// Interface for the Database Domain Item.
    /// </summary>
    public interface IDbDomainItem : IDbDomainKeyName, IDbDomainKey, IDbCatalogKey, IDbDomain, IScopeKey
    {
        /// <summary>
        /// The Default value for the Domain
        /// </summary>
        String? DomainDefault { get; }
    }

    /// <summary>
    /// Implementation for the Database Domain Item.
    /// </summary>
    [Serializable]
    public class DbDomainItem : BindingTableRow, IDbDomainItem, IDbExtendedProperty, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>(nameof(CatalogId)); } }

        /// <inheritdoc/>
        public Guid? DomainId { get { return GetValue<Guid>(nameof(DomainId)); } }

        /// <inheritdoc/>
        public string? DatabaseName { get { return GetValue(nameof(DatabaseName)); } }

        /// <inheritdoc/>
        public string? SchemaName { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public string? DomainName { get { return GetValue(nameof(DomainName)); } }

        /// <inheritdoc/>
        public string? DataType { get { return GetValue(nameof(DataType)); } }

        /// <inheritdoc/>
        public string? DomainDefault { get { return GetValue(nameof(DomainDefault)); } }

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
        public ScopeType Scope { get; } = ScopeType.DatabaseDomain;

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(CatalogId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DomainId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(DomainName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(DataType), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DomainDefault), typeof(string)){ AllowDBNull = true},
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
        };

        /// <summary>
        /// Constructor for the Database Domain Item
        /// </summary>
        public DbDomainItem() : base() { }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <inheritdoc/>
        public virtual Command PropertyCommand(IConnection connection)
        {
            if (this.Scope.ToDbLevel() is IDbLevelObjectKey scopeKey)
            {
                return new DbExtendedPropertyGetCommand(connection)
                {
                    CatalogId = CatalogId,
                    Level0Name = SchemaName,
                    Level0Type = scopeKey.CatalogScope.ToString(),
                    Level1Name = DomainName,
                    Level1Type = scopeKey.ObjectScope.ToString(),
                    Level2Name = String.Empty,
                    Level2Type = String.Empty,
                }.GetCommand();
            }
            else
            {
                Exception ex = new InvalidOperationException("Could not determine LevelType");
                ex.Data.Add(nameof(DatabaseName), DatabaseName);
                ex.Data.Add(nameof(SchemaName), SchemaName);
                ex.Data.Add(nameof(DomainName), DomainName);
                throw ex;
            }
        }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for the Domain Item.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DbDomainItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new DbDomainKeyName(this).ToString(); }
    }
}
