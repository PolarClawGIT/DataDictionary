using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for a Database Reference Item
    /// </summary>
    /// <see href="https://learn.microsoft.com/en-us/sql/relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql?view=sql-server-ver16"/>
    public interface IReferenceItem : IReferenceKey, ICatalogKey, IReference,
        ITemporalItem
    {
        /// <inheritdoc cref="IReference.ObjectType"/>
        new DbObjectType ObjectType { get; }

        /// <inheritdoc cref="IReference.ReferencedType"/>
        new DbObjectType ReferencedType { get; }
    }

    /// <summary>
    /// Implementation for a Database Reference Item
    /// </summary>
    [Serializable]
    public class ReferenceItem : BindingTableRow, IReferenceItem, ISerializable,
        IInfomationSchemaItem<IReference, ReferenceItem>
    {
        /// <inheritdoc/>
        public Guid? CatalogId
        {
            get { return GetValue<Guid>(nameof(CatalogId)); }
            init { SetValue(nameof(CatalogId), value); }
        }

        /// <inheritdoc/>
        public Guid? ReferenceId
        {
            get { return GetValue<Guid>(nameof(ReferenceId)); }
            private init { SetValue(nameof(ReferenceId), value); }
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
        public String? ObjectName
        {
            get { return GetValue(nameof(ObjectName)); }
            init { SetValue(nameof(ObjectName), value); }
        }

        /// <inheritdoc/>
        String? IReference.ObjectType { get { return GetValue(nameof(ObjectType)); } }

        /// <inheritdoc/>
        public DbObjectType ObjectType
        {
            get
            {
                String? value = GetValue(nameof(ObjectType));
                if (value.TryParse(out DbObjectType result))
                { return result; }
                else { return DbObjectType.Null; }
            }
            init
            { SetValue(nameof(ObjectType), value.GetEnumeration().Name); }
        }

        /// <inheritdoc/>
        public String? ReferencedDatabaseName
        {
            get { return GetValue(nameof(ReferencedDatabaseName)); }
            init { SetValue(nameof(ReferencedDatabaseName), value); }
        }

        /// <inheritdoc/>
        public String? ReferencedSchemaName
        {
            get { return GetValue(nameof(ReferencedSchemaName)); }
            init { SetValue(nameof(ReferencedSchemaName), value); }
        }

        /// <inheritdoc/>
        public String? ReferencedObjectName
        {
            get { return GetValue(nameof(ReferencedObjectName)); }
            init { SetValue(nameof(ReferencedObjectName), value); }
        }

        /// <inheritdoc/>
        public String? ReferencedColumnName
        {
            get { return GetValue(nameof(ReferencedColumnName)); }
            init { SetValue(nameof(ReferencedColumnName), value); }
        }

        /// <inheritdoc/>
        String? IReference.ReferencedType { get { return GetValue(nameof(ReferencedType)); } }

        /// <inheritdoc/>
        public DbObjectType ReferencedType
        {
            get
            {
                String? value = GetValue(nameof(ReferencedType));
                if (value.TryParse(out DbObjectType result))
                { return result; }
                else { return DbObjectType.Null; }
            }
            init { SetValue(nameof(ReferencedType), value.GetEnumeration().Name); }
        }

        /// <inheritdoc/>
        public Boolean? IsCallerDependent
        {
            get { return GetValue<Boolean>(nameof(IsCallerDependent), BindingItemParsers.BooleanTryParse); }
            set { SetValue(nameof(IsCallerDependent), value); }
        }

        /// <inheritdoc/>
        public Boolean? IsAmbiguous
        {
            get { return GetValue<Boolean>(nameof(IsAmbiguous), BindingItemParsers.BooleanTryParse); }
            set { SetValue(nameof(IsAmbiguous), value); }
        }

        /// <inheritdoc/>
        public Boolean? IsSelected
        {
            get { return GetValue<Boolean>(nameof(IsSelected), BindingItemParsers.BooleanTryParse); }
            set { SetValue(nameof(IsSelected), value); }
        }

        /// <inheritdoc/>
        public Boolean? IsModified
        {
            get { return GetValue<Boolean>(nameof(IsModified), BindingItemParsers.BooleanTryParse); }
            set { SetValue(nameof(IsModified), value); }
        }

        /// <inheritdoc/>
        public Boolean? IsSelectAll
        {
            get { return GetValue<Boolean>(nameof(IsSelectAll), BindingItemParsers.BooleanTryParse); }
            set { SetValue(nameof(IsSelectAll), value); }
        }

        /// <inheritdoc/>
        public Boolean? IsAllColumnsFound
        {
            get { return GetValue<Boolean>(nameof(IsAllColumnsFound), BindingItemParsers.BooleanTryParse); }
            set { SetValue(nameof(IsAllColumnsFound), value); }
        }

        /// <inheritdoc/>
        public Boolean? IsInsertAll
        {
            get { return GetValue<Boolean>(nameof(IsInsertAll), BindingItemParsers.BooleanTryParse); }
            set { SetValue(nameof(IsInsertAll), value); }
        }

        /// <inheritdoc/>
        public Boolean? IsIncomplete
        {
            get { return GetValue<Boolean>(nameof(IsIncomplete), BindingItemParsers.BooleanTryParse); }
            set { SetValue(nameof(IsIncomplete), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
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
            new DataColumn(nameof(IsModified), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsSelectAll), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsAllColumnsFound), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsInsertAll), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsIncomplete), typeof(bool)){ AllowDBNull = true},
            .. TemporalItem.columnDefinitions,
        ];

        /// <summary>
        /// Constructor for the Database Reference Item
        /// </summary>
        public ReferenceItem() : base()
        { ReferenceId = Guid.NewGuid();

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
        public void Update(IReference source)
        {
            IsCallerDependent = source.IsCallerDependent;
            IsAmbiguous = source.IsAmbiguous;
            IsSelected = source.IsSelected;
            IsModified = source.IsModified;
            IsSelectAll = source.IsSelectAll;
            IsAllColumnsFound = source.IsAllColumnsFound;
            IsInsertAll = source.IsInsertAll;
            IsIncomplete = source.IsIncomplete;
        }

        /// <inheritdoc/>
        public static TResult Create<TResult>(ICatalogKey catalog, IReference source) 
            where TResult : ReferenceItem, new()
        {
            DbObjectType objectType = DbObjectType.Null;
            if (source.ObjectType.TryParse(out DbObjectType objectValue))
            { objectType = objectValue; }

            DbObjectType referencedType = DbObjectType.Null;
            if (source.ReferencedType.TryParse(out DbObjectType refrencedValue))
            { referencedType = refrencedValue; }

            TResult newValue = new TResult()
            {
                CatalogId = catalog.CatalogId,
                DatabaseName = source.DatabaseName,
                SchemaName = source.SchemaName,

                ObjectName = source.ObjectName,
                ObjectType = objectType,

                ReferencedDatabaseName = source.ReferencedDatabaseName,
                ReferencedSchemaName = source.ReferencedSchemaName,
                ReferencedObjectName = source.ReferencedObjectName,
                ReferencedColumnName = source.ReferencedColumnName,
                ReferencedType = referencedType,
            };

            newValue.Update(source);
            return newValue;

            throw new NotImplementedException();
        }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for the Database Reference Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected ReferenceItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
