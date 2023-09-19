using DataDictionary.DataLayer.ApplicationData.Model;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData.Catalog
{
    /// <summary>
    /// Interface for the Database Catalog Item.
    /// </summary>
    public interface IDbCatalogItem : IDbCatalogKeyUnique, IDbCatalogKey, IDbIsSystem, IBindingTableRow
    {
        /// <summary>
        /// The SQL Server that the database was extracted from.
        /// </summary>
        string? SourceServerName { get; }
    }

    /// <summary>
    /// Implementation for Database Catalog Item.
    /// </summary>
    [Serializable]
    public class DbCatalogItem : BindingTableRow, IDbCatalogItem, INotifyPropertyChanged, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } protected set { SetValue("CatalogId", value); } }

        /// <inheritdoc/>
        public string? CatalogName { get { return GetValue("CatalogName"); } }

        /// <inheritdoc/>
        public string? SourceServerName { get { return GetValue("SourceServerName"); } }

        /// <inheritdoc/>
        public bool IsSystem { get { return CatalogName is "tempdb" or "master" or "msdb" or "model"; } }

        /// <summary>
        /// Constructor for DbCatalogItem.
        /// </summary>
        public DbCatalogItem() : base()
        { CatalogId = Guid.NewGuid(); }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(string)){ AllowDBNull = false},
            new DataColumn("SourceServerName", typeof(string)){ AllowDBNull = false},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
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
        { return new DbCatalogKeyUnique(this).ToString(); }
    }

}
