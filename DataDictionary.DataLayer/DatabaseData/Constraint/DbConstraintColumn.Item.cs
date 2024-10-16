﻿using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Table;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DatabaseData.Constraint
{
    /// <summary>
    /// Interface for the Database Constraint Column
    /// </summary>
    public interface IDbConstraintColumnItem : IDbConstraintKeyName, IDbCatalogKey, IDbColumnPosition, IDbTableColumnKeyName, IDbConstraintColumnKeyReferenced
    { }

    /// <summary>
    /// Implantation for the Database Constraint Column
    /// </summary>
    [Serializable]
    public class DbConstraintColumnItem : BindingTableRow, IDbConstraintColumnItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>(nameof(CatalogId)); } }

        /// <inheritdoc/>
        public Guid? ConstraintColumnId { get { return GetValue<Guid>(nameof(ConstraintColumnId)); } }
        
        /// <inheritdoc/>
        public string? DatabaseName { get { return GetValue(nameof(DatabaseName)); } }

        /// <inheritdoc/>
        public string? SchemaName { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public string? ConstraintName { get { return GetValue(nameof(ConstraintName)); } }

        /// <inheritdoc/>
        public string? TableName { get { return GetValue(nameof(TableName)); } }

        /// <inheritdoc/>
        public string? ColumnName { get { return GetValue(nameof(ColumnName)); } }

        /// <inheritdoc/>
        public int? OrdinalPosition { get { return GetValue<int>(nameof(OrdinalPosition)); } }

        /// <inheritdoc/>
        public string? ReferencedSchemaName { get { return GetValue(nameof(ReferencedSchemaName)); } }

        /// <inheritdoc/>
        public string? ReferencedTableName { get { return GetValue(nameof(ReferencedTableName)); } }

        /// <inheritdoc/>
        public string? ReferencedColumnName { get { return GetValue(nameof(ReferencedColumnName)); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(CatalogId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(ConstraintColumnId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(TableName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(ConstraintName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(ColumnName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(OrdinalPosition), typeof(int)){ AllowDBNull = true},
            new DataColumn(nameof(ReferencedSchemaName), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(ReferencedTableName), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(ReferencedColumnName), typeof(string)){ AllowDBNull = true},
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
