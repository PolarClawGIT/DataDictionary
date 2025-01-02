using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Catalog DomainItem.
    /// </summary>
    public interface IDomainItem : IDomain, IDomainKey, ICatalogKey,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for the Catalog DomainItem.
    /// </summary>
    [Serializable]
    public class DomainItem : BindingTableRow, IDomainItem, ISerializable,
        IInfomationSchemaItem<IDomain, DomainItem>
    {
        /// <inheritdoc/>
        public Guid? CatalogId
        {
            get { return GetValue<Guid>(nameof(CatalogId)); }
            init { SetValue<Guid>(nameof(CatalogId), value); }
        }

        /// <inheritdoc/>
        public Guid? DomainId
        {
            get { return GetValue<Guid>(nameof(DomainId)); }
            private init { SetValue<Guid>(nameof(DomainId), value); }
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
        public String? DomainName
        {
            get { return GetValue(nameof(DomainName)); }
            init { SetValue(nameof(DomainName), value); }
        }

        /// <inheritdoc/>
        public String? DataType
        {
            get { return GetValue(nameof(DataType)); }
            set { SetValue(nameof(DataType), value); }
        }

        /// <inheritdoc/>
        public String? DomainDefault
        {
            get { return GetValue(nameof(DomainDefault)); }
            set { SetValue(nameof(DomainDefault), value); }
        }

        /// <inheritdoc/>
        public Int32? CharacterMaximumLength
        {
            get { return GetValue<int>(nameof(CharacterMaximumLength)); }
            set { SetValue(nameof(CharacterMaximumLength), value); }
        }

        /// <inheritdoc/>
        public Int32? CharacterOctetLength
        {
            get { return GetValue<int>(nameof(CharacterOctetLength)); }
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
        public ITemporal Temporal { get; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(CatalogId), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DomainId), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(DomainName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(DataType), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DomainDefault), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterMaximumLength), typeof(Int32)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterOctetLength), typeof(Int32)){ AllowDBNull = true},
            new DataColumn(nameof(NumericPrecision), typeof(Byte)){ AllowDBNull = true},
            new DataColumn(nameof(NumericPrecisionRadix), typeof(Int16)){ AllowDBNull = true},
            new DataColumn(nameof(NumericScale), typeof(Int32)){ AllowDBNull = true},
            new DataColumn(nameof(DateTimePrecision), typeof(Int16)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetCatalog), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetSchema), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CollationCatalog), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CollationSchema), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CollationName), typeof(String)){ AllowDBNull = true},
            .. TemporalItem.columnDefinitions,
        ];

        /// <summary>
        /// Constructor for the Catalog DomainItem
        /// </summary>
        public DomainItem() : base()
        {
            DomainId = Guid.NewGuid();

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <inheritdoc/>
        public static TResult Create<TResult>(ICatalogKey catalog, IDomain source)
            where TResult : DomainItem, new()
        {
            TResult newValue = new TResult()
            {
                CatalogId = catalog.CatalogId,
                DatabaseName = source.DatabaseName,
                SchemaName = source.SchemaName,
                DomainName = source.DomainName,
            };

            newValue.Update(source);
            return newValue;
        }

        /// <inheritdoc/>
        public virtual void Update(IDomain source)
        {
            CharacterMaximumLength = source.CharacterMaximumLength;
            CharacterOctetLength = source.CharacterMaximumLength;
            CharacterSetCatalog = source.CharacterSetCatalog;
            CharacterSetName = source.CharacterSetName;
            CharacterSetSchema = source.CharacterSetSchema;
            CollationCatalog = source.CharacterSetCatalog;
            CollationName = source.CollationName;
            CollationSchema = source.CollationSchema;
            DataType = source.DataType;
            DateTimePrecision = source.DateTimePrecision;
            DomainDefault = source.DomainDefault;
            NumericPrecision = source.NumericPrecision;
            NumericPrecisionRadix = source.NumericPrecisionRadix;
            NumericScale = source.NumericScale;
        }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for the Domain Item.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DomainItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        public override String ToString()
        { return new DomainKeyName(this).ToString(); }


    }
}
