using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Constraint Column
    /// </summary>
    public interface IConstraintColumnItem : IConstraintColumn, ICatalogKey, IConstraintColumnKey,
        ITemporalItem
    { }

    /// <summary>
    /// Implantation for the Database Constraint Column
    /// </summary>
    [Serializable]
    public class ConstraintColumnItem : BindingTableRow, IConstraintColumnItem, ISerializable,
        IInfomationSchemaItem<IConstraintColumn, ConstraintColumnItem>
    {
        /// <inheritdoc/>
        public Guid? CatalogId
        {
            get { return GetValue<Guid>(nameof(CatalogId)); }
            init { SetValue<Guid>(nameof(CatalogId), value); }
        }

        /// <inheritdoc/>
        public Guid? ConstraintColumnId
        {
            get { return GetValue<Guid>(nameof(ConstraintColumnId)); }
            private init { SetValue(nameof(ConstraintColumnId), value); }
        }

        /// <inheritdoc/>
        public String? DatabaseName
        {
            get { return GetValue(nameof(DatabaseName)); }
            init { SetValue(nameof(DatabaseName), value); }
        }

        /// <inheritdoc/>
        public String? SchemaName
        {
            get { return GetValue(nameof(SchemaName)); }
            init { SetValue(nameof(SchemaName), value); }
        }

        /// <inheritdoc/>
        public String? ConstraintName
        {
            get { return GetValue(nameof(ConstraintName)); }
            init { SetValue(nameof(ConstraintName), value); }
        }

        /// <inheritdoc/>
        public String? TableName
        {
            get { return GetValue(nameof(TableName)); }
            init { SetValue(nameof(TableName), value); }
        }

        /// <inheritdoc/>
        public String? ColumnName
        {
            get { return GetValue(nameof(ColumnName)); }
            init { SetValue(nameof(ColumnName), value); }
        }

        /// <inheritdoc/>
        public Int32? OrdinalPosition
        {
            get { return GetValue<Int32>(nameof(OrdinalPosition)); }
            set { SetValue(nameof(OrdinalPosition), value); }
        }

        /// <inheritdoc/>
        public String? ReferencedSchemaName
        {
            get { return GetValue(nameof(ReferencedSchemaName)); }
            set { SetValue(nameof(ReferencedSchemaName), value); }
        }

        /// <inheritdoc/>
        public String? ReferencedTableName
        {
            get { return GetValue(nameof(ReferencedTableName)); }
            set { SetValue(nameof(ReferencedTableName), value); }
        }

        /// <inheritdoc/>
        public String? ReferencedColumnName
        {
            get { return GetValue(nameof(ReferencedColumnName)); }
            set { SetValue(nameof(ReferencedColumnName), value); }
        }

        /// <inheritdoc/>
        public String? CreatedBy { get { return GetValue(nameof(CreatedBy)); } }

        /// <inheritdoc/>
        public DateTime? CreatedOn
        {
            get
            {
                DateTime? value = GetValue<DateTime>(nameof(CreatedOn));
                if (value is DateTime baseDate)
                { return TimeZoneInfo.ConvertTimeFromUtc(baseDate, TimeZoneInfo.Local); }
                else { return null; }
            }
        }

        /// <inheritdoc/>
        public String? RemovedBy { get { return GetValue(nameof(RemovedBy)); } }

        /// <inheritdoc/>
        public DateTime? RemovedOn
        {
            get
            {
                DateTime? value = GetValue<DateTime>(nameof(RemovedOn));
                if (value is DateTime baseDate)
                { return TimeZoneInfo.ConvertTimeFromUtc(baseDate, TimeZoneInfo.Local); }
                else { return null; }
            }
        }

        /// <inheritdoc/>
        public Boolean? IsInserted
        { get { return GetValue<bool>(nameof(IsInserted), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsUpdated
        { get { return GetValue<bool>(nameof(IsUpdated), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsDeleted
        { get { return GetValue<bool>(nameof(IsDeleted), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsCurrent
        { get { return GetValue<bool>(nameof(IsCurrent), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public DbModificationType Modification
        {
            get
            {
                if (IsDeleted == true) { return DbModificationType.Deleted; }
                else if (IsInserted == true) { return DbModificationType.Inserted; }
                else if (IsUpdated == true) { return DbModificationType.Updated; }
                else { return DbModificationType.Null; }
            }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(CatalogId), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ConstraintColumnId), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(TableName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(ConstraintName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(ColumnName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(OrdinalPosition), typeof(Int32)){ AllowDBNull = true},
            new DataColumn(nameof(ReferencedSchemaName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ReferencedTableName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ReferencedColumnName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CreatedOn), typeof(DateTime)){ AllowDBNull = true},
            new DataColumn(nameof(CreatedBy), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(RemovedOn), typeof(DateTime)){ AllowDBNull = true},
            new DataColumn(nameof(RemovedBy), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(IsInserted), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsUpdated), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsDeleted), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsCurrent), typeof(Boolean)){ AllowDBNull = true},
        };

        /// <summary>
        /// Constructor for the Database Constraint Column
        /// </summary>
        public ConstraintColumnItem() : base()
        { ConstraintColumnId = Guid.NewGuid(); }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <inheritdoc/>
        public static TResult Create<TResult>(ICatalogKey catalog, IConstraintColumn source)
            where TResult : ConstraintColumnItem, new()
        {
            TResult newValue = new TResult()
            {
                CatalogId = catalog.CatalogId,
                DatabaseName = source.DatabaseName,
                SchemaName = source.SchemaName,
                ConstraintName = source.ConstraintName,
                TableName = source.TableName,
                ColumnName = source.ColumnName,
            };

            newValue.Update(source);
            return newValue;
        }

        /// <inheritdoc/>
        public virtual void Update(IConstraintColumn source)
        {
            OrdinalPosition = source.OrdinalPosition;
            ReferencedSchemaName = source.ReferencedSchemaName;
            ReferencedTableName = source.ReferencedTableName;
            ReferencedColumnName = source.ReferencedColumnName;
        }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for the Database Constraint Column
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected ConstraintColumnItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

    }

}
