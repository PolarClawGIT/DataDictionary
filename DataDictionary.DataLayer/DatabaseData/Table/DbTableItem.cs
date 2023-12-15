using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DomainData;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData.Table
{
    /// <summary>
    /// Interface for Database Column Item
    /// </summary>
    public interface IDbTableItem : IDbTableKeyName, IDbTableKey, IDbCatalogKey, IDbIsSystem, IDbScopeType, IDataItem
    {
        /// <summary>
        /// Type of Table Object (Table, View, ...)
        /// </summary>
        String? TableType { get; }
    }

    /// <summary>
    /// Implementation of Database Column Item
    /// </summary>
    [Serializable]
    public class DbTableItem : BindingTableRow, IDbTableItem, INotifyPropertyChanged, IDbExtendedProperty, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }

        /// <inheritdoc/>
        public Guid? TableId { get { return GetValue<Guid>("TableId"); } }
        
        /// <inheritdoc/>
        public string? DatabaseName { get { return GetValue("DatabaseName"); } }

        /// <inheritdoc/>
        public string? SchemaName { get { return GetValue("SchemaName"); } }

        /// <inheritdoc/>
        public string? TableName { get { return GetValue("TableName"); } }

        /// <inheritdoc/>
        public string? ScopeName { get { return GetValue("ScopeName"); } }

        /// <inheritdoc/>
        public string? TableType { get { return GetValue("TableType"); } }

        /// <inheritdoc/>
        public bool IsSystem { get { return TableName is "__RefactorLog" or "sysdiagrams"; } }

        /// <inheritdoc/>
        //public DbObjectScope ObjectScope
        //{
        //    get
        //    {
        //        if (Enum.TryParse(TableType, true, out DbObjectScope value))
        //        { return value; }
        //        else if (TableType is "BASE TABLE" or "HISTORY TABLE" or "TEMPORAL TABLE") { return DbObjectScope.Table; }
        //        else { return DbObjectScope.NULL; }
        //    }
        //}

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(string)){ AllowDBNull = true},
            new DataColumn("TableId", typeof(string)){ AllowDBNull = true},
            new DataColumn("DatabaseName", typeof(string)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(string)){ AllowDBNull = false},
            new DataColumn("TableName", typeof(string)){ AllowDBNull = false},
            new DataColumn("ScopeName", typeof(string)){ AllowDBNull = false},
            new DataColumn("TableType", typeof(string)){ AllowDBNull = false},
        };

        /// <summary>
        /// Constructor for Database Column 
        /// </summary>
        public DbTableItem() : base() { }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        /// <inheritdoc/>
        public virtual Command PropertyCommand(IConnection connection)
        {
            return new DbExtendedPropertyGetCommand(connection)
            {
                CatalogId = CatalogId,
                Level0Name = SchemaName,
                Level0Type = "SCHEMA",
                Level1Name = TableName,
                Level1Type = "TABLE"
            }.
            GetCommand();
        }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Database Column 
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DbTableItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new DbTableKeyName(this).ToString(); }
    }
}
