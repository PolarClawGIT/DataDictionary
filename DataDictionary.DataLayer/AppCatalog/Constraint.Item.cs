using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Constraint Item.
    /// </summary>
    public interface IConstraintItem : IConstraint, IConstraintKey, ICatalogKey,
        IDbConstraintType, ITemporalItem
    {
        /// <inheritdoc cref="IDbConstraintType.ConstraintType"/>
        new DbConstraintType ConstraintType { get; }
    }

    /// <summary>
    /// Implementation for the Database Constraint Item.
    /// </summary>
    [Serializable]
    public class ConstraintItem : BindingTableRow, IConstraintItem, ISerializable,
        IInfomationSchemaItem<IConstraint, ConstraintItem>
    {
        /// <inheritdoc/>
        public Guid? CatalogId
        {
            get { return GetValue<Guid>(nameof(CatalogId)); }
            init { SetValue<Guid>(nameof(CatalogId), value); }
        }

        /// <inheritdoc/>
        public Guid? ConstraintId
        {
            get { return GetValue<Guid>(nameof(ConstraintId)); }
            private init { SetValue(nameof(ConstraintId), value); }
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
        public DbConstraintType ConstraintType
        {
            get
            {
                String? value = GetValue(nameof(ConstraintType));
                if (value.TryParse(out DbConstraintType result))
                { return result; }
                else { return DbConstraintType.Null; }
            }
            init { SetValue(nameof(ConstraintType), value.GetEnumeration().Name); }
        }

        String? IConstraintType.ConstraintType { get { return GetValue(nameof(IConstraintType.ConstraintType)); } }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(CatalogId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(ConstraintId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(TableName), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(ConstraintName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(ConstraintType), typeof(string)){ AllowDBNull = false},
            .. TemporalItem.columnDefinitions,
        ];

        /// <summary>
        /// Constructor for the Database Constraint Item
        /// </summary>
        public ConstraintItem() : base()
        {
            ConstraintId = Guid.NewGuid();

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
        public static TResult Create<TResult>(ICatalogKey catalog, IConstraint source)
            where TResult : ConstraintItem, new()
        {
            DbConstraintType constraintType = DbConstraintType.Null;
            if (source.ConstraintType.TryParse(out DbConstraintType result))
            { constraintType = result; }

            return new TResult()
            {
                CatalogId = catalog.CatalogId,
                DatabaseName = source.DatabaseName,
                SchemaName = source.SchemaName,
                TableName = source.TableName,
                ConstraintName = source.ConstraintName,
                ConstraintType = constraintType,
            };
        }

        /// <inheritdoc/>
        public virtual void Update(IConstraint source)
        {
            // Nothing to actually do. All values are fixed.
        }

        #region ISerializable
        /// <summary>
        /// Serialization constructor for Database Constraint Item.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected ConstraintItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new ConstraintKeyName(this).ToString(); }
    }
}
