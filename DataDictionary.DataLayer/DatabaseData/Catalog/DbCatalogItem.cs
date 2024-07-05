using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DatabaseData.Catalog
{
    /// <summary>
    /// Interface for the Database Catalog Item.
    /// </summary>
    public interface IDbCatalogItem : IDbCatalogKeyName, IDbCatalogKey, IDbIsSystem, IScopeType
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
        public Guid? CatalogId { get { return GetValue<Guid>(nameof(CatalogId)); } protected set { SetValue(nameof(CatalogId), value); } }

        /// <inheritdoc/>
        public string? DatabaseName { get { return GetValue(nameof(SourceDatabaseName)); }  }

        /// <inheritdoc/>
        public string? CatalogTitle { get { return GetValue(nameof(CatalogTitle)); } set { SetValue(nameof(CatalogTitle), value); } }

        /// <inheritdoc/>
        public string? CatalogDescription { get { return GetValue(nameof(CatalogDescription)); } set { SetValue(nameof(CatalogDescription), value); } }

        /// <inheritdoc/>
        public string? SourceServerName { get { return GetValue(nameof(SourceServerName)); } protected set { SetValue(nameof(SourceServerName), value); } }

        /// <inheritdoc/>
        public string? SourceDatabaseName { get { return GetValue(nameof(SourceDatabaseName)); } protected set { SetValue(nameof(SourceDatabaseName), value); } }

        /// <inheritdoc/>
        public DateTime? SourceDate { get { return GetValue<DateTime>(nameof(SourceDate)); } protected set { SetValue(nameof(SourceDate), value); } }

        /// <inheritdoc/>
        public bool IsSystem { get { return DatabaseName is "tempdb" or "master" or "msdb" or "model"; } }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.Database;

        /// <summary>
        /// Constructor for DbCatalogItem.
        /// </summary>
        public DbCatalogItem() : base()
        { CatalogId = Guid.NewGuid(); }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(CatalogId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(CatalogTitle), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CatalogDescription), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(SourceServerName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SourceDatabaseName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SourceDate), typeof(DateTime)){ AllowDBNull = true}
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
