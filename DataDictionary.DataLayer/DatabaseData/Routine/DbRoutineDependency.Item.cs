using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Reference;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DatabaseData.Routine
{
    /// <summary>
    /// Interface for a Routine Dependency Item
    /// </summary>
    /// <see href="https://learn.microsoft.com/en-us/sql/relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql?view=sql-server-ver16"/>
    public interface IDbRoutineDependencyItem : IDbColumnReferenceKey, IDbCatalogKey, IDbRoutineDependencyKeyName, IDbRoutineDependencyKey
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
    public class DbRoutineDependencyItem : BindingTableRow, IDbRoutineDependencyItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>(nameof(CatalogId)); } }

        /// <inheritdoc/>
        public Guid? DependencyId { get { return GetValue<Guid>(nameof(DependencyId)); } }

        /// <inheritdoc/>
        public string? DatabaseName { get { return GetValue(nameof(DatabaseName)); } }

        /// <inheritdoc/>
        public string? SchemaName { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public string? RoutineName { get { return GetValue(nameof(RoutineName)); } }

        /// <inheritdoc/>
        public string? ReferenceSchemaName { get { return GetValue(nameof(ReferenceSchemaName)); } }

        /// <inheritdoc/>
        public string? ReferenceObjectName { get { return GetValue(nameof(ReferenceObjectName)); } }

        /// <inheritdoc/>
        public string? ReferenceObjectType { get { return GetValue(nameof(ReferenceObjectType)); } }

        /// <inheritdoc/>
        public string? ReferenceColumnName { get { return GetValue(nameof(ReferenceColumnName)); } }

        /// <inheritdoc/>
        public bool? IsCallerDependent { get { return GetValue<bool>(nameof(IsCallerDependent), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public bool? IsAmbiguous { get { return GetValue<bool>(nameof(IsAmbiguous), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public bool? IsSelected { get { return GetValue<bool>(nameof(IsSelected), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public bool? IsUpdated { get { return GetValue<bool>(nameof(IsUpdated), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public bool? IsSelectAll { get { return GetValue<bool>(nameof(IsSelectAll), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public bool? IsAllColumnsFound { get { return GetValue<bool>(nameof(IsAllColumnsFound), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public bool? IsInsertAll { get { return GetValue<bool>(nameof(IsInsertAll), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public bool? IsIncomplete { get { return GetValue<bool>(nameof(IsIncomplete), BindingItemParsers.BooleanTryParse); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(CatalogId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DependencyId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(RoutineName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(ReferenceSchemaName), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(ReferenceObjectName), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(ReferenceObjectType), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(ReferenceColumnName), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(IsCallerDependent), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsAmbiguous), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsSelected), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsUpdated), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsSelectAll), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsAllColumnsFound), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsInsertAll), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsIncomplete), typeof(bool)){ AllowDBNull = true},
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
