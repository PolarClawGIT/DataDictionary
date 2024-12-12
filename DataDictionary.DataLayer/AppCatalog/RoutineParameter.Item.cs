using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Routine Parameter
    /// </summary>
    public interface IRoutineParameterItem : IRoutineParameter,
        IRoutineParameterKey, ICatalogKey,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for the Database Routine Parameter
    /// </summary>
    [Serializable]
    public class RoutineParameterItem : BindingTableRow, IRoutineParameterItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId
        {
            get { return GetValue<Guid>(nameof(CatalogId)); }
            init { SetValue<Guid>(nameof(CatalogId), value); }
        }

        /// <inheritdoc/>
        public Guid? RoutineParameterId
        {
            get { return GetValue<Guid>(nameof(RoutineParameterId)); }
            private init { SetValue<Guid>(nameof(RoutineParameterId), value); }
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
        public String? ParameterName
        {
            get { return GetValue(nameof(ParameterName)); }
            init { SetValue(nameof(ParameterName), value); }
        }

        /// <inheritdoc/>
        public Int32? OrdinalPosition
        {
            get { return GetValue<Int32>(nameof(OrdinalPosition)); }
            set { SetValue(nameof(OrdinalPosition), value); }
        }

        /// <inheritdoc/>
        public String? DataType
        {
            get { return GetValue(nameof(DataType)); }
            set { SetValue(nameof(DataType), value); }
        }

        /// <inheritdoc/>
        public Int32? CharacterMaximumLength
        {
            get { return GetValue<Int32>(nameof(CharacterMaximumLength)); }
            set { SetValue(nameof(CharacterMaximumLength), value); }
        }

        /// <inheritdoc/>
        public Int32? CharacterOctetLength
        {
            get { return GetValue<Int32>(nameof(CharacterOctetLength)); }
            set { SetValue(nameof(CharacterOctetLength), value); }
        }

        /// <inheritdoc/>
        public Byte? NumericPrecision
        {
            get { return GetValue<Byte>(nameof(NumericPrecision)); }
            set { SetValue(nameof(NumericPrecision), value); }
        }

        /// <inheritdoc/>
        public Int16? NumericPrecisionRadix
        {
            get { return GetValue<Int16>(nameof(NumericPrecisionRadix)); }
            set { SetValue(nameof(NumericPrecisionRadix), value); }
        }

        /// <inheritdoc/>
        public Int32? NumericScale
        {
            get { return GetValue<Int32>(nameof(NumericScale)); }
            set { SetValue(nameof(NumericScale), value); }
        }

        /// <inheritdoc/>
        public Int16? DateTimePrecision
        {
            get { return GetValue<Int16>(nameof(DateTimePrecision)); }
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
        public DbRoutineType RoutineType
        {
            get
            {
                String? value = GetValue(nameof(RoutineType));
                if (DbRoutineEnumeration.TryParse(value, null, out DbRoutineEnumeration? result))
                { return result.Value; }
                else { return DbRoutineType.Null; }
            }
            init
            { SetValue(nameof(RoutineType), DbRoutineEnumeration.Cast(value).Name); }
        }

        /// <inheritdoc/>
        String? IRoutineType.RoutineType { get { return GetValue(nameof(RoutineType)); } }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(CatalogId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(RoutineParameterId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(RoutineName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(RoutineType), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(ParameterName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(OrdinalPosition), typeof(int)){ AllowDBNull = true},
            new DataColumn(nameof(DataType), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterMaximumLength), typeof(int)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterOctetLength), typeof(int)){ AllowDBNull = true},
            new DataColumn(nameof(NumericPrecision), typeof(byte)){ AllowDBNull = true},
            new DataColumn(nameof(NumericPrecisionRadix), typeof(short)){ AllowDBNull = true},
            new DataColumn(nameof(NumericScale), typeof(int)){ AllowDBNull = true},
            new DataColumn(nameof(DateTimePrecision), typeof(short)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetCatalog), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetSchema), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetName), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CollationCatalog), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CollationSchema), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CollationName), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DomainCatalog), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DomainSchema), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DomainName), typeof(string)){ AllowDBNull = true},
            .. TemporalItem.columnDefinitions,
        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <summary>
        /// Constructor for the Database Routine Parameter
        /// </summary>
        public RoutineParameterItem() : base()
        {
            RoutineParameterId = Guid.NewGuid();

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <inheritdoc/>
        public static TResult Create<TResult>(ICatalogKey catalog, IRoutineParameter source)
            where TResult : RoutineParameterItem, new()
        {
            DbRoutineType routineType = DbRoutineType.Null;
            if (DbRoutineEnumeration.TryParse(source.RoutineType, null, out DbRoutineEnumeration? result))
            { routineType = result.Value; }

            TResult newValue = new TResult()
            {
                CatalogId = catalog.CatalogId,
                DatabaseName = source.DatabaseName,
                SchemaName = source.SchemaName,
                RoutineName = source.RoutineName,
                ParameterName = source.ParameterName,
                RoutineType = routineType
            };

            newValue.Update(source);
            return newValue;
        }

        /// <inheritdoc/>
        public void Update(IRoutineParameter source)
        {
            OrdinalPosition = source.OrdinalPosition;
            DataType = source.DataType;
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
        }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for the Database Routine Parameter
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected RoutineParameterItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        { return new RoutineParameterKeyName(this).ToString(); }
    }
}
