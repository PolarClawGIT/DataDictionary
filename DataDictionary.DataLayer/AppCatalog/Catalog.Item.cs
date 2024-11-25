using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Catalog Item.
    /// </summary>
    public interface ICatalogItem : ICatalogKeyName, ICatalogKey,
        IDbIsSystem, ITemporalItem
    {
        /// <summary>
        /// Title given to the Catalog. Default is the Database Name.
        /// </summary>
        String? CatalogTitle { get; }

        /// <summary>
        /// Description given to the Catalog.
        /// </summary>
        String? CatalogDescription { get; }

        /// <summary>
        /// The SQL Server that the database was extracted from.
        /// </summary>
        String? ServerName { get; }

        /// <summary>
        /// The Date that the database was extracted.
        /// </summary>
        DateTime? SourceDate { get; }
    }

    /// <summary>
    /// Implementation for Database Catalog Item.
    /// </summary>
    [Serializable]
    public class CatalogItem : BindingTableRow, ICatalogItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>(nameof(CatalogId)); } protected set { SetValue(nameof(CatalogId), value); } }

        /// <inheritdoc/>
        public String? CatalogTitle { get { return GetValue(nameof(CatalogTitle)); } set { SetValue(nameof(CatalogTitle), value); } }

        /// <inheritdoc/>
        public String? CatalogDescription { get { return GetValue(nameof(CatalogDescription)); } set { SetValue(nameof(CatalogDescription), value); } }

        /// <inheritdoc/>
        public String? ServerName { get { return GetValue(nameof(ServerName)); } internal set { SetValue(nameof(ServerName), value); } }

        /// <inheritdoc/>
        public String? DatabaseName { get { return GetValue(nameof(DatabaseName)); } internal set { SetValue(nameof(DatabaseName), value); } }

        /// <inheritdoc/>
        public DateTime? SourceDate { get { return GetValue<DateTime>(nameof(SourceDate)); } internal set { SetValue(nameof(SourceDate), value); } }

        /// <inheritdoc/>
        public bool IsSystem { get { return DatabaseName is "tempdb" or "master" or "msdb" or "model"; } }

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
        { get { return GetValue<Boolean>(nameof(IsInserted), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsUpdated
        { get { return GetValue<Boolean>(nameof(IsUpdated), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsDeleted
        { get { return GetValue<Boolean>(nameof(IsDeleted), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsCurrent
        { get { return GetValue<Boolean>(nameof(IsCurrent), BindingItemParsers.BooleanTryParse); } }

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

        /// <summary>
        /// Constructor for CatalogItem.
        /// </summary>
        public CatalogItem() : base()
        { CatalogId = Guid.NewGuid(); }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(CatalogId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(CatalogTitle), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CatalogDescription), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ServerName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(DatabaseName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(SourceDate), typeof(DateTime)){ AllowDBNull = true},
            new DataColumn(nameof(CreatedBy), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CreatedOn), typeof(DateTime)){ AllowDBNull = true},
            new DataColumn(nameof(RemovedBy), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(RemovedOn), typeof(DateTime)){ AllowDBNull = true},
            new DataColumn(nameof(IsInserted), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsUpdated), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsDeleted), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsCurrent), typeof(Boolean)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for CatalogItem.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected CatalogItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new CatalogKeyName(this).ToString(); }
    }

}
