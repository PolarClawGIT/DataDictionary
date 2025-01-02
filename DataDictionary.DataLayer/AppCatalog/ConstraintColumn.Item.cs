using DataDictionary.Resource;
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
        public ITemporal Temporal { get; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
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
            .. TemporalItem.columnDefinitions,
        ];

        /// <summary>
        /// Constructor for the Database Constraint Column
        /// </summary>
        public ConstraintColumnItem() : base()
        {
            ConstraintColumnId = Guid.NewGuid();

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

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
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }
        #endregion

    }

}
