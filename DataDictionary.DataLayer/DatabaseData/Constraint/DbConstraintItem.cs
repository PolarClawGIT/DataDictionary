using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
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
    /// Interface for the Database Constraint Item.
    /// </summary>
    public interface IDbConstraintItem : IDbConstraintKey, IDbCatalogKey, IDbElementScope, IDbTableKey, IBindingTableRow
    {
        /// <summary>
        /// Type of the Database Constraint.
        /// </summary>
        string? ConstraintType { get; }
    }

    /// <summary>
    /// Implementation for the Database Constraint Item.
    /// </summary>
    [Serializable]
    public class DbConstraintItem : BindingTableRow, IDbConstraintItem, INotifyPropertyChanged, IDbExtendedProperty, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }

        /// <inheritdoc/>
        public string? CatalogName { get { return GetValue("CatalogName"); } }

        /// <inheritdoc/>
        public string? SchemaName { get { return GetValue("SchemaName"); } }

        /// <inheritdoc/>
        public string? ConstraintName { get { return GetValue("ConstraintName"); } }

        /// <inheritdoc/>
        public string? TableName { get { return GetValue("TableName"); } }

        /// <inheritdoc/>
        public string? ConstraintType { get { return GetValue("ConstraintType"); } }

        /// <inheritdoc/>
        public DbElementScope ElementScope { get; } = DbElementScope.Constraint;

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(string)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(string)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(string)){ AllowDBNull = false},
            new DataColumn("ConstraintName", typeof(string)){ AllowDBNull = false},
            new DataColumn("TableName", typeof(string)){ AllowDBNull = true},
            new DataColumn("ConstraintType", typeof(string)){ AllowDBNull = false},
        };

        /// <summary>
        /// Constructor for the Database Constraint Item
        /// </summary>
        public DbConstraintItem() : base() { }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <inheritdoc/>
        public virtual Command PropertyCommand(IConnection connection)
        {
            return new DbExtendedPropertyGetCommand(connection)
            {
                Level0Name = SchemaName, 
                Level0Type = "SCHEMA",
                Level1Name = TableName,
                Level1Type = "TABLE",
                Level2Name = ConstraintName,
                Level2Type = "CONSTRAINT"
            }.GetCommand();
        }

        #region ISerializable
        /// <summary>
        /// Serialization constructor for Database Constraint Item.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DbConstraintItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new DbConstraintKey(this).ToString(); }
    }
}
