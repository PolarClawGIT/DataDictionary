using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Routine Column
    /// </summary>
    public interface IRoutineColumnItem : IRoutineColumn, IRoutineColumnKey, ICatalogKey,
        ITemporalItem, IDbType
    { }

    /// <summary>
    /// Implementation of the Database Routine Column
    /// </summary>
    [Serializable]
    public class RoutineColumnItem : BindingTableRow, IRoutineColumnItem, INotifyPropertyChanged, ISerializable,
        IInfomationSchemaItem<IRoutineColumn, RoutineColumnItem>
    {
        /// <inheritdoc/>
        public Guid? CatalogId
        {
            get { return GetValue<Guid>(nameof(CatalogId)); }
            init { SetValue<Guid>(nameof(CatalogId), value); }
        }

        /// <inheritdoc/>
        public Guid? RoutineColumnId
        {
            get { return GetValue<Guid>(nameof(RoutineColumnId)); }
            private init { SetValue<Guid>(nameof(RoutineColumnId), value); }
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
        public String? RoutineName
        {
            get { return GetValue(nameof(RoutineName)); }
            init { SetValue(nameof(RoutineName), value); }
        }

        /// <inheritdoc/>
        public DbRoutineType RoutineType
        {
            get
            {
                String? value = GetValue(nameof(RoutineType));
                if (value.TryParse(out DbRoutineType result))
                { return result; }
                else { return DbRoutineType.Null; }
            }
            init
            { SetValue(nameof(RoutineType), value.GetEnumeration().Name); }
        }

        /// <inheritdoc/>
        String? IRoutineType.RoutineType { get { return GetValue(nameof(RoutineType)); } }

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
        public Boolean? IsNullable
        {
            get { return GetValue<Boolean>(nameof(IsNullable), BindingItemParsers.BooleanTryParse); }
            set { SetValue(nameof(IsNullable), value); }
        }

        /// <inheritdoc/>
        public String? DataType
        {
            get { return GetValue(nameof(DataType)); }
            set { SetValue(nameof(DataType), value); }
        }

        /// <inheritdoc/>
        public DbType? DatabaseType
        {
            get
            {
                var value = GetValue(nameof(DataType));
                if (value.TryParse(out DbType? item))
                { return item.Value; }
                else { return null; }
            }
        }


        /// <inheritdoc/>
        public String? ColumnDefault
        {
            get { return GetValue(nameof(ColumnDefault)); }
            set { SetValue(nameof(ColumnDefault), value); }
        }

        /// <inheritdoc/>
        public Int16? CharacterMaximumLength
        {
            get { return GetValue<Int16>(nameof(CharacterMaximumLength)); }
            set { SetValue(nameof(CharacterMaximumLength), value); }
        }

        /// <inheritdoc/>
        public Int16? CharacterOctetLength
        {
            get { return GetValue<Int16>(nameof(CharacterOctetLength)); }
            set { SetValue(nameof(CharacterOctetLength), value); }
        }

        /// <inheritdoc/>
        public Byte? NumericPrecision
        {
            get { return GetValue<Byte>(nameof(NumericPrecision)); }
            set { SetValue(nameof(NumericPrecision), value); }
        }

        /// <inheritdoc/>
        public Byte? NumericPrecisionRadix
        {
            get { return GetValue<Byte>(nameof(NumericPrecisionRadix)); }
            set { SetValue(nameof(NumericPrecisionRadix), value); }
        }

        /// <inheritdoc/>
        public Byte? NumericScale
        {
            get { return GetValue<Byte>(nameof(NumericScale)); }
            set { SetValue(nameof(NumericScale), value); }
        }

        /// <inheritdoc/>
        public Byte? DateTimePrecision
        {
            get { return GetValue<Byte>(nameof(DateTimePrecision)); }
            set { SetValue(nameof(DateTimePrecision), value); }
        }

        /// <inheritdoc/>
        public String? CharacterSetCatalog
        {
            get { return GetValue(nameof(CharacterSetCatalog)); }
            set { SetValue(nameof(CharacterSetCatalog), value); }
        }

        /// <inheritdoc/>
        public String? CharacterSetSchema
        {
            get { return GetValue(nameof(CharacterSetSchema)); }
            set { SetValue(nameof(CharacterSetSchema), value); }
        }

        /// <inheritdoc/>
        public String? CharacterSetName
        {
            get { return GetValue(nameof(CharacterSetName)); }
            set { SetValue(nameof(CharacterSetName), value); }
        }

        /// <inheritdoc/>
        public String? CollationCatalog
        {
            get { return GetValue(nameof(CollationCatalog)); }
            set { SetValue(nameof(CollationCatalog), value); }
        }

        /// <inheritdoc/>
        public String? CollationSchema
        {
            get { return GetValue(nameof(CollationSchema)); }
            set { SetValue(nameof(CollationSchema), value); }
        }

        /// <inheritdoc/>
        public String? CollationName
        {
            get { return GetValue(nameof(CollationName)); }
            set { SetValue(nameof(CollationName), value); }
        }

        /// <inheritdoc/>
        public String? DomainCatalog
        {
            get { return GetValue(nameof(DomainCatalog)); }
            set { SetValue(nameof(DomainCatalog), value); }
        }

        /// <inheritdoc/>
        public String? DomainSchema
        {
            get { return GetValue(nameof(DomainSchema)); }
            set { SetValue(nameof(DomainSchema), value); }
        }

        /// <inheritdoc/>
        public String? DomainName
        {
            get { return GetValue(nameof(DomainName)); }
            set { SetValue(nameof(DomainName), value); }
        }

        /// <inheritdoc/>
        public Boolean? IsIdentity
        {
            get { return GetValue<Boolean>(nameof(IsIdentity), BindingItemParsers.BooleanTryParse); }
            set { SetValue(nameof(IsIdentity), value); }
        }

        /// <inheritdoc/>
        public Boolean? IsHidden
        {
            get { return GetValue<Boolean>(nameof(IsHidden), BindingItemParsers.BooleanTryParse); }
            set { SetValue(nameof(IsHidden), value); }
        }

        /// <inheritdoc/>
        public Boolean? IsComputed
        {
            get { return GetValue<Boolean>(nameof(IsComputed), BindingItemParsers.BooleanTryParse); }
            set { SetValue(nameof(IsComputed), value); }
        }

        /// <inheritdoc/>
        public String? ComputedDefinition
        {
            get { return GetValue(nameof(ComputedDefinition)); }
            set { SetValue(nameof(ComputedDefinition), value); }
        }

        /// <inheritdoc/>
        public String? GeneratedAlwayType
        {
            get { return GetValue(nameof(GeneratedAlwayType)); }
            set { SetValue(nameof(GeneratedAlwayType), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(CatalogId), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(RoutineColumnId), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(RoutineName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(RoutineType), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(ColumnName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(OrdinalPosition), typeof(Int32)){ AllowDBNull = false},
            new DataColumn(nameof(IsNullable), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(DataType), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ColumnDefault), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterMaximumLength), typeof(Int16)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterOctetLength), typeof(Int16)){ AllowDBNull = true},
            new DataColumn(nameof(NumericPrecision), typeof(Byte)){ AllowDBNull = true},
            new DataColumn(nameof(NumericPrecisionRadix), typeof(Byte)){ AllowDBNull = true},
            new DataColumn(nameof(NumericScale), typeof(Byte)){ AllowDBNull = true},
            new DataColumn(nameof(DateTimePrecision), typeof(Byte)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetCatalog), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetSchema), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CollationCatalog), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CollationSchema), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CollationName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DomainCatalog), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DomainSchema), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DomainName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(IsIdentity), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsComputed), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(ComputedDefinition), typeof(String)){ AllowDBNull = true},
            .. TemporalItem.columnDefinitions,
        ];

        /// <summary>
        /// Constructor for the Database Table Column
        /// </summary>
        public RoutineColumnItem() : base()
        {
            RoutineColumnId = Guid.NewGuid();

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <inheritdoc/>
        public static TResult Create<TResult>(ICatalogKey catalog, IRoutineColumn source)
            where TResult : RoutineColumnItem, new()
        {
            DbRoutineType routineType = DbRoutineType.Null;
            if (source.RoutineType.TryParse(out DbRoutineType result))
            { routineType = result; }

            TResult newValue = new TResult()
            {
                CatalogId = catalog.CatalogId,
                DatabaseName = source.DatabaseName,
                SchemaName = source.SchemaName,
                RoutineName = source.RoutineName,
                RoutineType = routineType,
                ColumnName = source.ColumnName,

            };

            newValue.Update(source);
            return newValue;
        }

        /// <inheritdoc/>
        public virtual void Update(IRoutineColumn source)
        {
            OrdinalPosition = source.OrdinalPosition;
            IsNullable = source.IsNullable;
            DataType = source.DataType;
            ColumnDefault = source.ColumnDefault;
            CharacterMaximumLength = source.CharacterMaximumLength;
            CharacterOctetLength = source.CharacterOctetLength;
            NumericPrecision = source.NumericPrecision;
            NumericPrecisionRadix = source.NumericPrecisionRadix;
            NumericScale = source.NumericScale;
            DateTimePrecision = source.DateTimePrecision;
            CharacterSetCatalog = source.CharacterSetCatalog;
            CharacterSetSchema = source.CharacterSetSchema;
            CharacterSetName = source.CharacterSetName;
            CollationCatalog = source.CollationCatalog;
            CollationSchema = source.CollationSchema;
            CollationName = source.CollationName;
            DomainCatalog = source.DomainCatalog;
            DomainSchema = source.DomainSchema;
            DomainName = source.DomainName;
            IsIdentity = source.IsIdentity;
            IsComputed = source.IsComputed;
            ComputedDefinition = source.ComputedDefinition;
        }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for the Database Table Column
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected RoutineColumnItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        { return new RoutineColumnKeyName(this).ToString(); }
    }
}
