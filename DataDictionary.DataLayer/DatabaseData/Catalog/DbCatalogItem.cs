using DataDictionary.DataLayer.ApplicationData.Scope;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DatabaseData.Catalog
{
    /// <summary>
    /// Interface for the Database Catalog Item.
    /// </summary>
    public interface IDbCatalogItem : IDbCatalogKeyName, IDbCatalogKey, IDbIsSystem, IScopeKey
    {
        /// <summary>
        /// Title given to the Catalog. Default is the Database Name.
        /// </summary>
        string? CatalogTitle { get; }

        /// <summary>
        /// Description given to the Catalog.
        /// </summary>
        string? CatalogDescription { get; }

        /// <summary>
        /// The SQL Server that the database was extracted from.
        /// </summary>
        string? SourceServerName { get; }

        /// <summary>
        /// The SQL Server Database that was extracted.
        /// </summary>
        string? SourceDatabaseName { get; }

        /// <summary>
        /// The Date that the database was extracted.
        /// </summary>
        DateTime? SourceDate { get; }
    }

    /// <summary>
    /// Implementation for Database Catalog Item.
    /// </summary>
    [Serializable]
    public class DbCatalogItem : BindingTableRow, IDbCatalogItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } protected set { SetValue("CatalogId", value); } }

        /// <inheritdoc/>
        public string? DatabaseName { get { return GetValue("SourceDatabaseName"); }  }

        /// <inheritdoc/>
        public string? CatalogTitle { get { return GetValue("CatalogTitle"); } set { SetValue("CatalogTitle", value); } }

        /// <inheritdoc/>
        public string? CatalogDescription { get { return GetValue("CatalogDescription"); } set { SetValue("CatalogDescription", value); } }

        /// <inheritdoc/>
        public string? SourceServerName { get { return GetValue("SourceServerName"); } protected set { SetValue("SourceServerName", value); } }

        /// <inheritdoc/>
        public string? SourceDatabaseName { get { return GetValue("SourceDatabaseName"); } protected set { SetValue("SourceDatabaseName", value); } }

        /// <inheritdoc/>
        public DateTime? SourceDate { get { return GetValue<DateTime>("SourceDate"); } protected set { SetValue("SourceDate", value); } }

        /// <inheritdoc/>
        public bool IsSystem { get { return DatabaseName is "tempdb" or "master" or "msdb" or "model"; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.Database; } }

        /// <summary>
        /// Constructor for DbCatalogItem.
        /// </summary>
        public DbCatalogItem() : base()
        { CatalogId = Guid.NewGuid(); }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("CatalogTitle", typeof(string)){ AllowDBNull = true},
            new DataColumn("CatalogDescription", typeof(string)){ AllowDBNull = true},
            new DataColumn("SourceServerName", typeof(string)){ AllowDBNull = false},
            new DataColumn("SourceDatabaseName", typeof(string)){ AllowDBNull = false},
            new DataColumn("SourceDate", typeof(DateTime)){ AllowDBNull = true}
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for DbCatalogItem.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DbCatalogItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new DbCatalogKeyName(this).ToString(); }
    }

}
