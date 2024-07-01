using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData.Table
{
    /// <summary>
    /// Interface for Database Column Item
    /// </summary>
    public interface IDbTableItem : IDbTableKeyName, IDbTableKey, IDbCatalogKey, IDbIsSystem, IDbTableTypeKey, IScopeKey
    { }

    /// <summary>
    /// Implementation of Database Column Item
    /// </summary>
    [Serializable]
    public class DbTableItem : BindingTableRow, IDbTableItem, INotifyPropertyChanged, IDbExtendedProperty, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>(nameof(CatalogId)); } }

        /// <inheritdoc/>
        public Guid? TableId { get { return GetValue<Guid>(nameof(TableId)); } }

        /// <inheritdoc/>
        public String? DatabaseName { get { return GetValue(nameof(DatabaseName)); } }

        /// <inheritdoc/>
        public String? SchemaName { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public String? TableName { get { return GetValue(nameof(TableName)); } }

        /// <inheritdoc/>
        public Boolean IsSystem { get { return TableName is "__RefactorLog" or "sysdiagrams"; } }

        /// <inheritdoc/>
        public DbTableType TableType
        { get { return DbTableTypeKey.Parse(GetValue(nameof(TableType)) ?? String.Empty).TableType; } }

        /// <inheritdoc/>
        public ScopeType Scope
        {
            get
            {
                switch (TableType)
                {
                    case DbTableType.Table: return ScopeType.DatabaseTable;
                    case DbTableType.TemporalTable: return ScopeType.DatabaseTable;
                    case DbTableType.HistoryTable: return ScopeType.DatabaseTable;
                    case DbTableType.View: return ScopeType.DatabaseView;
                    default: return ScopeType.Null;
                }
            }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(CatalogId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(TableId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(TableName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(TableType), typeof(string)){ AllowDBNull = false},
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
            if (this.Scope.ToDbLevel() is IDbLevelObjectKey scopeKey)
            {
                return new DbExtendedPropertyGetCommand(connection)
                {
                    CatalogId = CatalogId,
                    Level0Name = SchemaName,
                    Level0Type = scopeKey.CatalogScope.ToString(),
                    Level1Name = TableName,
                    Level1Type = scopeKey.ObjectScope.ToString(),
                    Level2Name = String.Empty,
                    Level2Type = String.Empty,
                }.GetCommand();
            }
            else
            {
                Exception ex = new InvalidOperationException("Could not determine LevelType");
                ex.Data.Add(nameof(DatabaseName), DatabaseName);
                ex.Data.Add(nameof(SchemaName), SchemaName);
                ex.Data.Add(nameof(TableName), TableName);
                throw ex;
            }
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
