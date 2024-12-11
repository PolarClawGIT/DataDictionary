using DataDictionary.Resource;
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
    public interface ICatalogItem : ICatalog, ICatalogKey,
        IDbIsSystem, ITemporalItem
    { }

    /// <summary>
    /// Implementation for Database Catalog Item.
    /// </summary>
    [Serializable]
    public class CatalogItem : BindingTableRow, ICatalogItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId
        {
            get { return GetValue<Guid>(nameof(CatalogId)); }
            private init { SetValue(nameof(CatalogId), value); }
        }

        /// <inheritdoc/>
        public String? CatalogTitle
        {
            get { return GetValue(nameof(CatalogTitle)); }
            set { SetValue(nameof(CatalogTitle), value); }
        }

        /// <inheritdoc/>
        public String? CatalogDescription
        {
            get { return GetValue(nameof(CatalogDescription)); }
            set { SetValue(nameof(CatalogDescription), value); }
        }

        /// <inheritdoc/>
        public String? ServerName
        {
            get { return GetValue(nameof(ServerName)); }
            set { SetValue(nameof(ServerName), value); }
        }

        /// <inheritdoc/>
        public String? DatabaseName
        {
            get { return GetValue(nameof(DatabaseName)); }
            init { SetValue(nameof(DatabaseName), value); }
        }

        /// <inheritdoc/>
        public DateTime? SourceDate
        {
            get { return GetValue<DateTime>(nameof(SourceDate)); }
            set { SetValue(nameof(SourceDate), value); }
        }

        /// <inheritdoc/>
        public Boolean IsSystem
        {
            get
            {
                return Catalog.IsSystem
                    .Split(',')
                    .Any(w => w.Trim().Equals(DatabaseName, KeyExtension.CompareString));
            }
        }
        #region ITemporalItem
        TemporalItem temporal; // Backing field for Temporal Data.

        /// <inheritdoc/>
        public DateTime? CreatedOn { get { return temporal.CreatedOn; } }

        /// <inheritdoc/>
        public String? CreatedBy { get { return temporal.CreatedBy; } }

        /// <inheritdoc/>
        public DateTime? RemovedOn { get { return temporal.RemovedOn; } }

        /// <inheritdoc/>
        public String? RemovedBy { get { return temporal.RemovedBy; } }

        /// <inheritdoc/>
        public Boolean? IsInserted { get { return temporal.IsInserted; } }

        /// <inheritdoc/>
        public Boolean? IsUpdated { get { return temporal.IsUpdated; } }

        /// <inheritdoc/>
        public Boolean? IsDeleted { get { return temporal.IsDeleted; } }

        /// <inheritdoc/>
        public Boolean? IsCurrent { get { return temporal.IsCurrent; } }

        /// <inheritdoc/>
        public DbModificationType Modification { get { return temporal.Modification; } }
        #endregion

        /// <summary>
        /// Constructor for CatalogItem.
        /// </summary>
        public CatalogItem() : base()
        {
            CatalogId = Guid.NewGuid();

            temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Generic constructor for CatalogItem
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        internal static TResult Create<TResult>(ICatalog source)
            where TResult : CatalogItem, new()
        {
            TResult newValue = new TResult()
            {
                CatalogTitle = source.DatabaseName,
                DatabaseName = source.DatabaseName,
            };

            newValue.Update(source);
            return newValue;
        }

        /// <summary>
        /// Used to Update based on Information Schema values
        /// </summary>
        /// <param name="source"></param>
        public virtual void Update(ICatalog source)
        {
            // Handle LocalDb
            if (source.ServerName is String && source.ServerName.Contains(Catalog.LocalDbContains))
            { ServerName = Catalog.LocalDbName; }
            else { ServerName = source.ServerName; }

            SourceDate = DateTime.Now;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(CatalogId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(CatalogTitle), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CatalogDescription), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ServerName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(DatabaseName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(SourceDate), typeof(DateTime)){ AllowDBNull = true},
            .. TemporalItem.columnDefinitions,
        ];

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
        {
            temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new CatalogKeyName(this).ToString(); }
    }

}
