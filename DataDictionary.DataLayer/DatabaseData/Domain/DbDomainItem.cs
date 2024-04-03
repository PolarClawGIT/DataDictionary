using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
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
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }

        /// <inheritdoc/>
        public Guid? DomainId { get { return GetValue<Guid>("DomainId"); } }

        /// <inheritdoc/>
        public string? DatabaseName { get { return GetValue("DatabaseName"); } }

        /// <inheritdoc/>
        public string? SchemaName { get { return GetValue("SchemaName"); } }

        /// <inheritdoc/>
        public string? DomainName { get { return GetValue("DomainName"); } }

        /// <inheritdoc/>
        public string? DataType { get { return GetValue("DataType"); } }

        /// <inheritdoc/>
        public string? DomainDefault { get { return GetValue("DomainDefault"); } }

        /// <inheritdoc/>
        public int? CharacterMaximumLength { get { return GetValue<int>("CharacterMaximumLength"); } }

        /// <inheritdoc/>
        public int? CharacterOctetLength { get { return GetValue<int>("CharacterOctetLength"); } }

        /// <inheritdoc/>
        public byte? NumericPrecision { get { return GetValue<byte>("NumericPrecision"); } }

        /// <inheritdoc/>
        public short? NumericPrecisionRadix { get { return GetValue<short>("NumericPrecisionRadix"); } }

        /// <inheritdoc/>
        public int? NumericScale { get { return GetValue<int>("NumericScale"); } }

        /// <inheritdoc/>
        public short? DateTimePrecision { get { return GetValue<short>("DateTimePrecision"); } }

        /// <inheritdoc/>
        public string? CharacterSetCatalog { get { return GetValue("CharacterSetCatalog"); } }

        /// <inheritdoc/>
        public string? CharacterSetSchema { get { return GetValue("CharacterSetSchema"); } }

        /// <inheritdoc/>
        public string? CharacterSetName { get { return GetValue("CharacterSetName"); } }

        /// <inheritdoc/>
        public string? CollationCatalog { get { return GetValue("CollationCatalog"); } }

        /// <inheritdoc/>
        public string? CollationSchema { get { return GetValue("CollationSchema"); } }

        /// <inheritdoc/>
        public string? CollationName { get { return GetValue("CollationName"); } }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.DatabaseDomain;

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(string)){ AllowDBNull = true},
            new DataColumn("DomainId", typeof(string)){ AllowDBNull = true},
            new DataColumn("DatabaseName", typeof(string)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(string)){ AllowDBNull = false},
            new DataColumn("DomainName", typeof(string)){ AllowDBNull = false},
            new DataColumn("DataType", typeof(string)){ AllowDBNull = true},
            new DataColumn("DomainDefault", typeof(string)){ AllowDBNull = true},
            new DataColumn("CharacterMaximumLength", typeof(int)){ AllowDBNull = true},
            new DataColumn("CharacterOctetLength", typeof(int)){ AllowDBNull = true},
            new DataColumn("NumericPrecision", typeof(byte)){ AllowDBNull = true},
            new DataColumn("NumericPrecisionRadix", typeof(short)){ AllowDBNull = true},
            new DataColumn("NumericScale", typeof(int)){ AllowDBNull = true},
            new DataColumn("DateTimePrecision", typeof(short)){ AllowDBNull = true},
            new DataColumn("CharacterSetCatalog", typeof(string)){ AllowDBNull = true},
            new DataColumn("CharacterSetSchema", typeof(string)){ AllowDBNull = true},
            new DataColumn("CharacterSetName", typeof(string)){ AllowDBNull = true},
            new DataColumn("CollationCatalog", typeof(string)){ AllowDBNull = true},
            new DataColumn("CollationSchema", typeof(string)){ AllowDBNull = true},
            new DataColumn("CollationName", typeof(string)){ AllowDBNull = true},
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
