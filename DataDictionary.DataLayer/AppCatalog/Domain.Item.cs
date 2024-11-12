using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Domain Item.
    /// </summary>
    public interface IDbDomainItem : IDomainKeyName, IDomainKey, ICatalogKey,
        IDomain, ITemporalItem
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
        public String? DatabaseName { get { return GetValue(nameof(DatabaseName)); } }

        /// <inheritdoc/>
        public String? SchemaName { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public String? DomainName { get { return GetValue(nameof(DomainName)); } }

        /// <inheritdoc/>
        public String? DataType { get { return GetValue(nameof(DataType)); } }

        /// <inheritdoc/>
        public String? DomainDefault { get { return GetValue(nameof(DomainDefault)); } }

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
        public String? CreatedBy { get { return GetValue(nameof(CreatedBy)); } }

        /// <inheritdoc/>
        public DateTime? CreatedOn
        {
            get
            {
                DateTime? value = GetValue<DateTime>(nameof(CreatedOn));
                if (value is DateTime baseDate)
                { return TimeZoneInfo.ConvertTimeFromUtc(baseDate, TimeZoneInfo.Local); }
                else { return null; }
            }
        }

        /// <inheritdoc/>
        public Boolean? IsInserted
        { get { return GetValue<bool>(nameof(IsInserted), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsUpdated
        { get { return GetValue<bool>(nameof(IsUpdated), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsDeleted
        { get { return GetValue<bool>(nameof(IsDeleted), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsCurrent
        { get { return GetValue<bool>(nameof(IsCurrent), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public DbModificationType Modification
        {
            get
            {
                if (IsDeleted == true) { return DbModificationType.Deleted; }
                else if (IsInserted == true) { return DbModificationType.Inserted; }
                else if (IsUpdated == true) { return DbModificationType.Updated; }
                else { return DbModificationType.Null; }
            }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(CatalogId), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DomainId), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(DomainName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(DataType), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DomainDefault), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterMaximumLength), typeof(Int32)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterOctetLength), typeof(Int32)){ AllowDBNull = true},
            new DataColumn(nameof(NumericPrecision), typeof(Byte)){ AllowDBNull = true},
            new DataColumn(nameof(NumericPrecisionRadix), typeof(Int16)){ AllowDBNull = true},
            new DataColumn(nameof(NumericScale), typeof(Int32)){ AllowDBNull = true},
            new DataColumn(nameof(DateTimePrecision), typeof(Int16)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetCatalog), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetSchema), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CollationCatalog), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CollationSchema), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CollationName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CreatedBy), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CreatedOn), typeof(DateTime)){ AllowDBNull = true},
            new DataColumn(nameof(IsInserted), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsUpdated), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsDeleted), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsCurrent), typeof(Boolean)){ AllowDBNull = true},

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
            DbLevelObjectKey scopeKey = new DbLevelObjectKey()
            { 
                CatalogScope = DbLevelCatalogType.Schema, 
                ObjectScope = DbLevelObjectType.Type
            };

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
        public override String ToString()
        { return new DomainKeyName(this).ToString(); }
    }
}
