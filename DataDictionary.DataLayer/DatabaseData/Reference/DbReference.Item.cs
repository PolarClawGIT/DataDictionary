using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DatabaseData.Reference
{
    /// <summary>
    /// Interface for a Database Reference Item
    /// </summary>
    /// <see href="https://learn.microsoft.com/en-us/sql/relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql?view=sql-server-ver16"/>
    public interface IDbReferenceItem : IDbReferenceKey, IDbCatalogKey, IDbReferenceKeyName, IDbReferencedKeyColumn
    {
        /// <summary>
        /// Database Object Type
        /// </summary>
        DbObjectType ObjectType { get; }

        /// <summary>
        /// Referenced Database Object Type
        /// </summary>
        DbObjectType ReferencedType { get; }

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
    /// Implementation for a Database Reference Item
    /// </summary>
    [Serializable]
    public class DbReferenceItem : BindingTableRow, IDbReferenceItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId => throw new NotImplementedException();

        /// <inheritdoc/>
        public Guid? ReferenceId => throw new NotImplementedException();

        /// <inheritdoc/>
        public String? DatabaseName { get { return GetValue(nameof(DatabaseName)); } }

        /// <inheritdoc/>
        public String? SchemaName { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public String? ObjectName { get { return GetValue(nameof(ObjectName)); } }

        /// <inheritdoc/>
        public DbObjectType ObjectType
        {
            get
            {
                String? value = GetValue(nameof(ObjectType));
                if (DbObjectEnumeration.TryParse(value, null, out DbObjectEnumeration? result))
                { return result.Value; }
                else { return DbObjectType.Null; }
            }
        }

        /// <inheritdoc/>
        public String? ReferencedDatabaseName { get { return GetValue(nameof(ReferencedDatabaseName)); } }

        /// <inheritdoc/>
        public String? ReferencedSchemaName { get { return GetValue(nameof(ReferencedSchemaName)); } }

        /// <inheritdoc/>
        public String? ReferencedObjectName { get { return GetValue(nameof(ReferencedObjectName)); } }

        /// <inheritdoc/>
        public String? ReferencedColumnName { get { return GetValue(nameof(ReferencedColumnName)); } }

        /// <inheritdoc/>
        public DbObjectType ReferencedType
        {
            get
            {
                String? value = GetValue(nameof(ObjectType));
                if (DbObjectEnumeration.TryParse(value, null, out DbObjectEnumeration? result))
                { return result.Value; }
                else { return DbObjectType.Null; }
            }
        }

        /// <inheritdoc/>
        public Boolean? IsCallerDependent { get { return GetValue<Boolean>(nameof(IsCallerDependent), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsAmbiguous { get { return GetValue<Boolean>(nameof(IsAmbiguous), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsSelected { get { return GetValue<Boolean>(nameof(IsSelected), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsUpdated { get { return GetValue<Boolean>(nameof(IsUpdated), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsSelectAll { get { return GetValue<Boolean>(nameof(IsSelectAll), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsAllColumnsFound { get { return GetValue<Boolean>(nameof(IsAllColumnsFound), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsInsertAll { get { return GetValue<Boolean>(nameof(IsInsertAll), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsIncomplete { get { return GetValue<Boolean>(nameof(IsIncomplete), BindingItemParsers.BooleanTryParse); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(CatalogId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(ReferenceId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(ObjectName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(ObjectType), typeof(String)){ AllowDBNull = false},

            new DataColumn(nameof(ReferencedDatabaseName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ReferencedSchemaName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ReferencedObjectName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ReferencedColumnName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ReferencedType), typeof(String)){ AllowDBNull = true},

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
        /// Constructor for the Database Reference Item
        /// </summary>
        public DbReferenceItem() : base() { }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for the Database Reference Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DbReferenceItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion
    }
}
