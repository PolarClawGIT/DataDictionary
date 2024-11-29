using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for Database Column Item
    /// </summary>
    public interface ITableItem : ITable, ITableKey, ICatalogKey,
        IDbIsSystem, IDbTableType, ITemporalItem
    { }

    /// <summary>
    /// Implementation of Database Column Item
    /// </summary>
    [Serializable]
    public class TableItem : BindingTableRow, ITableItem, INotifyPropertyChanged, ISerializable,
        IInfomationSchemaItem<ITable, TableItem>
    {
        /// <inheritdoc/>
        public Guid? CatalogId
        {
            get { return GetValue<Guid>(nameof(CatalogId)); }
            init { SetValue<Guid>(nameof(CatalogId), value); }
        }

        /// <inheritdoc/>
        public Guid? TableId
        {
            get { return GetValue<Guid>(nameof(TableId)); }
            private init { SetValue<Guid>(nameof(TableId), value); }
        }

        /// <inheritdoc/>
        public String? DatabaseName
        {
            get { return GetValue(nameof(DatabaseName)); }
            init { SetValue(nameof(DatabaseName), value); }
        }

        /// <inheritdoc/>
        public String? SchemaName
        {
            get { return GetValue(nameof(SchemaName)); }
            init { SetValue(nameof(SchemaName), value); }
        }

        /// <inheritdoc/>
        public String? TableName
        {
            get { return GetValue(nameof(TableName)); }
            init { SetValue(nameof(TableName), value); }
        }

        /// <inheritdoc/>
        public Boolean IsSystem { get { return TableName is "__RefactorLog" or "sysdiagrams"; } }

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
            init
            { SetValue(nameof(TableType), DbTableEnumeration.Cast(value).Name); }
        }

        /// <inheritdoc/>
        String? ITable.TableType { get { return GetValue(nameof(ITable.TableType)); } }

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
            new DataColumn(nameof(CatalogId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(TableId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(TableName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(TableType), typeof(string)){ AllowDBNull = false},
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
        /// Constructor for Database Column 
        /// </summary>
        public TableItem() : base()
        { TableId = Guid.NewGuid(); }

        /// <inheritdoc/>
        public static TResult Create<TResult>(ICatalogKey catalog, ITable source)
            where TResult : TableItem, new()
        {
            DbTableType tableType = DbTableType.Null;
            if (DbTableEnumeration.TryParse(source.TableType, null, out DbTableEnumeration? result))
            { tableType = result.Value; }

            return new TResult()
            {
                CatalogId = catalog.CatalogId,
                DatabaseName = source.DatabaseName,
                SchemaName = source.SchemaName,
                TableName = source.TableName,
                TableType = tableType,
            };
        }

        /// <inheritdoc/>
        public virtual void Update(ITable source)
        {
            // Nothing to actually do. All values are fixed.
        }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Database Column 
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected TableItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new TableKeyName(this).ToString(); }


    }
}
