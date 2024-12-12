using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Catalog Schema Item
    /// </summary>
    public interface ISchemaItem : ISchema, ISchemaKey, ICatalogKey,
        IDbIsSystem, ITemporalItem
    { }

    /// <summary>
    /// Implementation for the Catalog Schema Item
    /// </summary>
    [Serializable]
    public class SchemaItem : BindingTableRow, ISchemaItem, INotifyPropertyChanged, ISerializable,
        IInfomationSchemaItem<ISchema, SchemaItem>
    {
        /// <inheritdoc/>
        public Guid? CatalogId
        {
            get { return GetValue<Guid>(nameof(CatalogId)); }
            init { SetValue(nameof(CatalogId), value); }
        }

        /// <inheritdoc/>
        public Guid? SchemaId
        {
            get { return GetValue<Guid>(nameof(SchemaId)); }
            private init { SetValue<Guid>(nameof(SchemaId), value); }
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
        public Boolean IsSystem
        {
            get
            {
                return Schema.IsSystem
                    .Split(',')
                    .Any(w => w.Trim().Equals(SchemaName, KeyExtension.CompareString));
            }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(CatalogId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(SchemaId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(String)){ AllowDBNull = false},
            .. TemporalItem.columnDefinitions,
        ];

        /// <summary>
        /// Constructor for the SchemaItem
        /// </summary>
        public SchemaItem() : base()
        { SchemaId = Guid.NewGuid();

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for the SchemaItem
        /// </summary>
        /// <param name="catalog"></param>
        public SchemaItem(ICatalogKey catalog) : this()
        { CatalogId = catalog.CatalogId; }

        /// <inheritdoc/>
        public static TResult Create<TResult>(ICatalogKey catalog, ISchema source)
            where TResult : SchemaItem, new()
        {
            return new TResult()
            {
                CatalogId = catalog.CatalogId,
                DatabaseName = source.DatabaseName,
                SchemaName = source.SchemaName
            };
        }

        /// <inheritdoc/>
        public virtual void Update(ISchema source)
        {
            // Nothing to actually do. All values are Keys.
        }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for the Database Schema Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected SchemaItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        { return new SchemaKeyName(this).ToString(); }
    }
}
