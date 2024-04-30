using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Table;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData.Constraint
{
    /// <summary>
    /// Interface for the Database Constraint Item.
    /// </summary>
    public interface IDbConstraintItem : IDbConstraintKeyName, IDbConstraintKey, IDbCatalogKey,  IDbTableKeyName, IScopeKey
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
    public class DbConstraintItem : BindingTableRow, IDbConstraintItem, IDbExtendedProperty, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }

        /// <inheritdoc/>
        public Guid? ConstraintId { get { return GetValue<Guid>("ConstraintId"); } }

        /// <inheritdoc/>
        public string? DatabaseName { get { return GetValue("DatabaseName"); } }

        /// <inheritdoc/>
        public string? SchemaName { get { return GetValue("SchemaName"); } }

        /// <inheritdoc/>
        public string? ConstraintName { get { return GetValue("ConstraintName"); } }

        /// <inheritdoc/>
        public string? TableName { get { return GetValue("TableName"); } }

        /// <inheritdoc/>
        public string? ConstraintType { get { return GetValue("ConstraintType"); } }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.DatabaseTableConstraint;

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(string)){ AllowDBNull = true},
            new DataColumn("ConstraintId", typeof(string)){ AllowDBNull = true},
            new DataColumn("DatabaseName", typeof(string)){ AllowDBNull = false},
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
            {
                if (this.Scope.ToDbLevel() is IDbLevelElementKey scopeKey)
                {
                    return new DbExtendedPropertyGetCommand(connection)
                    {
                        CatalogId = CatalogId,
                        Level0Name = SchemaName,
                        Level0Type = scopeKey.CatalogScope.ToString(),
                        Level1Name = TableName,
                        Level1Type = scopeKey.ObjectScope.ToString(),
                        Level2Name = ConstraintName,
                        Level2Type = scopeKey.ElementScope.ToString(),
                    }.GetCommand();
                }
                else
                {
                    Exception ex = new InvalidOperationException("Could not determine LevelType");
                    ex.Data.Add(nameof(DatabaseName), DatabaseName);
                    ex.Data.Add(nameof(SchemaName), SchemaName);
                    ex.Data.Add(nameof(TableName), TableName);
                    ex.Data.Add(nameof(ConstraintName), ConstraintName);
                    throw ex;
                }
            }
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
        { return new DbConstraintKeyName(this).ToString(); }
    }
}
