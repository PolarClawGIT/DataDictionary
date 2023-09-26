using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Reference;
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

namespace DataDictionary.DataLayer.DatabaseData.Routine
{
    /// <summary>
    /// Interface for a Routine Dependency Item
    /// </summary>
    /// <see href="https://learn.microsoft.com/en-us/sql/relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql?view=sql-server-ver16"/>
    public interface IDbRoutineDependencyItem : IDbColumnReferenceKey, IDbCatalogKey
    {
        /// <summary>
        /// The Object Type of the Reference
        /// </summary>
        String? ReferenceObjectType { get; }

        /// <summary>
        /// Is the Reference Item Dependent on the Caller
        /// </summary>
        Boolean? IsCallerDependent { get; }

        /// <summary>
        /// Is the Reference Item Ambiguous
        /// </summary>
        Boolean? IsAmbiguous { get; }

        /// <summary>
        /// Is the Reference Item Selected
        /// </summary>
        Boolean? IsSelected { get; }

        /// <summary>
        /// Is the Reference Item Updated
        /// </summary>
        Boolean? IsUpdated { get; }

        /// <summary>
        /// Is the Reference Item all columns Selected
        /// </summary>
        Boolean? IsSelectAll { get; }

        /// <summary>
        /// Is the Reference Item Found All Columns
        /// </summary>
        Boolean? IsAllColumnsFound { get; }

        /// <summary>
        /// Is the Reference Item is Insert All
        /// </summary>
        Boolean? IsInsertAll { get; }

        /// <summary>
        /// Is the Reference Item is Complete
        /// </summary>
        Boolean? IsIncomplete { get; }
    }

    /// <summary>
    /// Implementation of the Database Routine Dependencies Item
    /// </summary>
    [Serializable]
    public class DbRoutineDependencyItem : BindingTableRow, IDbRoutineDependencyItem, INotifyPropertyChanged, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }

        /// <inheritdoc/>
        public string? CatalogName { get { return GetValue("CatalogName"); } }

        /// <inheritdoc/>
        public string? SchemaName { get { return GetValue("SchemaName"); } }

        /// <inheritdoc/>
        public string? RoutineName { get { return GetValue("RoutineName"); } }

        /// <inheritdoc/>
        public string? ReferenceSchemaName { get { return GetValue("ReferenceSchemaName"); } }

        /// <inheritdoc/>
        public string? ReferenceObjectName { get { return GetValue("ReferenceObjectName"); } }

        /// <inheritdoc/>
        public string? ReferenceObjectType { get { return GetValue("ReferenceObjectType"); } }

        /// <inheritdoc/>
        public string? ReferenceColumnName { get { return GetValue("ReferenceColumnName"); } }

        /// <inheritdoc/>
        public bool? IsCallerDependent { get { return GetValue<bool>("IsCallerDependent", BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public bool? IsAmbiguous { get { return GetValue<bool>("IsAmbiguous", BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public bool? IsSelected { get { return GetValue<bool>("IsSelected", BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public bool? IsUpdated { get { return GetValue<bool>("IsUpdated", BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public bool? IsSelectAll { get { return GetValue<bool>("IsSelectAll", BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public bool? IsAllColumnsFound { get { return GetValue<bool>("IsAllColumnsFound", BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public bool? IsInsertAll { get { return GetValue<bool>("IsInsertAll", BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public bool? IsIncomplete { get { return GetValue<bool>("IsIncomplete", BindingItemParsers.BooleanTryParse); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(string)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(string)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(string)){ AllowDBNull = false},
            new DataColumn("RoutineName", typeof(string)){ AllowDBNull = false},
            new DataColumn("ReferenceSchemaName", typeof(string)){ AllowDBNull = true},
            new DataColumn("ReferenceObjectName", typeof(string)){ AllowDBNull = true},
            new DataColumn("ReferenceObjectType", typeof(string)){ AllowDBNull = true},
            new DataColumn("ReferenceColumnName", typeof(string)){ AllowDBNull = true},
            new DataColumn("IsCallerDependent", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsAmbiguous", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsSelected", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsUpdated", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsSelectAll", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsAllColumnsFound", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsInsertAll", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsIncomplete", typeof(bool)){ AllowDBNull = true},
        };

        /// <summary>
        /// Constructor for the Database Routine Dependency Item
        /// </summary>
        public DbRoutineDependencyItem() : base() { }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for the Database Routine Dependency Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DbRoutineDependencyItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion
    }


}
