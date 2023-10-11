using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Reference;
using DataDictionary.DataLayer.DatabaseData.Table;
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

namespace DataDictionary.DataLayer.DatabaseData.Constraint
{
    /// <summary>
    /// Interface for the Database Constraint Column
    /// </summary>
    public interface IDbConstraintColumnItem : IDbConstraintKey, IDbCatalogKey, IDbColumnPosition, IDbTableColumnKey, IDbColumnReferenceKey, IDataItem
    { }

    /// <summary>
    /// Implantation for the Database Constraint Column
    /// </summary>
    [Serializable]
    public class DbConstraintColumnItem : BindingTableRow, IDbConstraintColumnItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }

        /// <inheritdoc/>
        public string? DatabaseName { get { return GetValue("DatabaseName"); } }

        /// <inheritdoc/>
        public string? SchemaName { get { return GetValue("SchemaName"); } }

        /// <inheritdoc/>
        public string? ConstraintName { get { return GetValue("ConstraintName"); } }

        /// <inheritdoc/>
        public string? TableName { get { return GetValue("TableName"); } }

        /// <inheritdoc/>
        public string? ColumnName { get { return GetValue("ColumnName"); } }

        /// <inheritdoc/>
        public int? OrdinalPosition { get { return GetValue<int>("OrdinalPosition"); } }

        /// <inheritdoc/>
        public string? ReferenceSchemaName { get { return GetValue("ReferenceSchemaName"); } }

        /// <inheritdoc/>
        public string? ReferenceObjectName { get { return GetValue("ReferenceTableName"); } }

        /// <inheritdoc/>
        public string? ReferenceColumnName { get { return GetValue("ReferenceColumnName"); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(string)){ AllowDBNull = true},
            new DataColumn("DatabaseName", typeof(string)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(string)){ AllowDBNull = false},
            new DataColumn("ConstraintName", typeof(string)){ AllowDBNull = false},
            new DataColumn("TableName", typeof(string)){ AllowDBNull = false},
            new DataColumn("ColumnName", typeof(string)){ AllowDBNull = false},
            new DataColumn("OrdinalPosition", typeof(int)){ AllowDBNull = true},
            new DataColumn("ReferenceSchemaName", typeof(string)){ AllowDBNull = true},
            new DataColumn("ReferenceTableName", typeof(string)){ AllowDBNull = true},
            new DataColumn("ReferenceColumnName", typeof(string)){ AllowDBNull = true},
        };

        /// <summary>
        /// Constructor for the Database Constraint Column
        /// </summary>
        public DbConstraintColumnItem() : base() { }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for the Database Constraint Column
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DbConstraintColumnItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

    }

}
