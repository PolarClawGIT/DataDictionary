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
        protected DbCatalogItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new DbCatalogKeyUnique(this).ToString(); }
    }

    /// <summary>
    /// Extension on the Database Catalog Item.
    /// </summary>
    public static class DbCatalogItemExtension
    {// TODO: Consider moving these to the List

        /// <summary>
        /// Returns the Catalog that matches the key passed.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static DbCatalogItem? GetCatalog(this IEnumerable<DbCatalogItem> source, IDbCatalogKeyUnique item)
        { return source.FirstOrDefault(w => new DbCatalogKeyUnique(item) == new DbCatalogKeyUnique(w)); }

        /// <summary>
        /// Returns the Catalog that matches the key.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DbCatalogItem? GetCatalog(this IDbCatalogKeyUnique item, IEnumerable<DbCatalogItem> source)
        {
            DbCatalogKeyUnique itemName = new DbCatalogKeyUnique(item);
            { return source.FirstOrDefault(w => new DbCatalogKeyUnique(item) == new DbCatalogKeyUnique(w)); }
        }

        /// <summary>
        /// Returns the Catalog that matches the Server/Database names passed.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DbCatalogItem? GetCatalog(this IEnumerable<DbCatalogItem> source, (string ServerName, string DatabaseName) parameters)
        {
            return source.FirstOrDefault(
                w => parameters.DatabaseName.Equals(w.CatalogName, KeyExtension.CompareString) &&
                parameters.ServerName.Equals(w.SourceServerName, KeyExtension.CompareString));
        }
    }
}
