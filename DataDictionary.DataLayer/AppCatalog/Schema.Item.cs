using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Catalog Schema Item
    /// </summary>
    public interface ISchemaItem : ISchemaKeyName, ISchemaKey, ICatalogKey,
        IDbIsSystem, ITemporalItem
    { }

    /// <summary>
    /// Implementation for the Catalog Schema Item
    /// </summary>
    [Serializable]
    public class SchemaItem : BindingTableRow, ISchemaItem, INotifyPropertyChanged, IDbExtendedProperty, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId
        {
            get { return GetValue<Guid>(nameof(CatalogId)); }
            private set { SetValue<Guid>(nameof(CatalogId), value); }
        }

        /// <inheritdoc/>
        public Guid? SchemaId
        {
            get { return GetValue<Guid>(nameof(SchemaId)); }
            private set { SetValue<Guid>(nameof(SchemaId), value); }
        }

        /// <inheritdoc/>
        public String? DatabaseName
        {
            get { return GetValue(nameof(DatabaseName)); }
            private set { SetValue(nameof(DatabaseName), value); }
        }

        /// <inheritdoc/>
        public String? SchemaName { 
            get { return GetValue(nameof(SchemaName)); }
            private set { SetValue(nameof(SchemaName), value); }
        }

        /// <inheritdoc/>
        public Boolean IsSystem
        {
            get
            {
                return SchemaName is "sys" or
                    "db_owner" or
                    "db_accessadmin" or
                    "db_securityadmin" or
                    "db_ddladmin" or
                    "db_backupoperator" or
                    "db_datareader" or
                    "db_datawriter" or
                    "db_denydatareader" or
                    "db_denydatawriter" or
                    "INFORMATION_SCHEMA" or
                    "guest";
            }
        }

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
        public String? RemovedBy { get { return GetValue(nameof(RemovedBy)); } }

        /// <inheritdoc/>
        public DateTime? RemovedOn
        {
            get
            {
                DateTime? value = GetValue<DateTime>(nameof(RemovedOn));
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
            new DataColumn(nameof(CatalogId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(SchemaId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(CreatedBy), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CreatedOn), typeof(DateTime)){ AllowDBNull = true},
            new DataColumn(nameof(RemovedBy), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(RemovedOn), typeof(DateTime)){ AllowDBNull = true},
            new DataColumn(nameof(IsInserted), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsUpdated), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsDeleted), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsCurrent), typeof(Boolean)){ AllowDBNull = true},
        };

        /// <summary>
        /// Constructor for the Database Schema Item
        /// </summary>
        public SchemaItem() : base() { }

        /// <summary>
        /// Constructor for the Database Schema Item using InformationSchema
        /// </summary>
        /// <param name="catalogKey"></param>
        /// <param name="schema"></param>
        protected SchemaItem(ICatalogKey catalogKey, SchemaInformationSchema schema)
        {
            CatalogId = catalogKey.CatalogId;
            DatabaseName = schema.DatabaseName;
            SchemaName = schema.SchemaName;
        }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <inheritdoc/>
        public virtual Command PropertyCommand(IConnection connection)
        {
            DbLevelCatalogKey scopeKey = new DbLevelCatalogKey()
            { CatalogScope = DbLevelCatalogType.Schema };

            return new DbExtendedPropertyGetCommand(connection)
            {
                CatalogId = CatalogId,
                Level0Name = SchemaName,
                Level0Type = scopeKey.CatalogScope.ToString(),
                Level1Name = String.Empty,
                Level1Type = String.Empty,
                Level2Name = String.Empty,
                Level2Type = String.Empty,
            }.GetCommand();
        }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for the Database Schema Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected SchemaItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new SchemaKeyName(this).ToString(); }
    }
}
