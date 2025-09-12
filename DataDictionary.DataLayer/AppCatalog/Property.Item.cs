using System.Data;
using Toolbox.BindingTable;
using DataDictionary.Resource.Enumerations;
using System.Runtime.Serialization;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for MS SQL ExtendedProperty as stored in the Application Database.
    /// </summary>
    public interface IPropertyItem : ICatalogKey,
        IProperty, ITemporalItem
    {
        /// <summary>
        /// MS SQL ExtendedProperty Level 0 Type
        /// </summary>
        DbLevelCatalogType CatalogScope { get; }

        /// <summary>
        /// MS SQL ExtendedProperty Level 1 Type
        /// </summary>
        DbLevelObjectType ObjectScope { get; }

        /// <summary>
        /// MS SQL ExtendedProperty Level 2 Type
        /// </summary>
        DbLevelElementType ElementScope { get; }

        /// <summary>
        /// Is the MS SQL ExtendedProperty the MS_Description property.
        /// </summary>
        Boolean IsDescription { get; }
    }

    /// <summary>
    /// Implementation of MS SQL ExtendedProperty as stored in the Application Database.
    /// </summary>
    [Serializable]
    public class PropertyItem : BindingTableRow, IPropertyItem, ISerializable,
        IInfomationSchemaItem<IProperty, PropertyItem>
    {
        /// <inheritdoc/>
        public Guid? CatalogId
        {
            get { return GetValue<Guid>(nameof(CatalogId)); }
            init { SetValue<Guid>(nameof(CatalogId), value); }
        }

        /// <inheritdoc/>
        public Guid? PropertyId
        {
            get { return GetValue<Guid>(nameof(PropertyId)); }
            private init { SetValue<Guid>(nameof(PropertyId), value); }
        }

        /// <inheritdoc/>
        public String? DatabaseName
        {
            get { return GetValue(nameof(DatabaseName)); }
            init { SetValue(nameof(DatabaseName), value); }
        }

        /// <inheritdoc/>
        public String? Level0Type
        {
            get { return GetValue(nameof(Level0Type)); }
            init { SetValue(nameof(Level0Type), value); }
        }

        /// <inheritdoc/>
        public String? Level0Name
        {
            get { return GetValue(nameof(Level0Name)); }
            init { SetValue(nameof(Level0Name), value); }
        }

        /// <inheritdoc/>
        public DbLevelCatalogType CatalogScope
        {
            get
            {
                String? value = GetValue(nameof(Level2Type));
                if (value.TryParse(out DbLevelCatalogType result))
                { return result; }
                else { return DbLevelCatalogType.Null; }
            }
        }

        /// <inheritdoc/>
        public String? Level1Type
        {
            get { return GetValue(nameof(Level1Type)); }
            init { SetValue(nameof(Level1Type), value); }
        }

        /// <inheritdoc/>
        public String? Level1Name
        {
            get { return GetValue(nameof(Level1Name)); }
            init { SetValue(nameof(Level1Name), value); }
        }

        /// <inheritdoc/>
        public DbLevelObjectType ObjectScope
        {
            get
            {
                String? value = GetValue(nameof(Level2Type));
                if (value.TryParse(out DbLevelObjectType result))
                { return result; }
                else { return DbLevelObjectType.Null; }
            }
        }

        /// <inheritdoc/>
        public String? Level2Type
        {
            get { return GetValue(nameof(Level2Type)); }
            init { SetValue(nameof(Level2Type), value); }
        }

        /// <inheritdoc/>
        public String? Level2Name
        {
            get { return GetValue(nameof(Level2Name)); }
            init { SetValue(nameof(Level2Name), value); }
        }

        /// <inheritdoc/>
        public DbLevelElementType ElementScope
        {
            get
            {
                String? value = GetValue(nameof(Level2Type));
                if (value.TryParse(out DbLevelElementType result))
                { return result; }
                else { return DbLevelElementType.Null; }
            }
        }

        /// <inheritdoc/>
        public String? PropertyName
        {
            get { return GetValue(nameof(PropertyName)); }
            init { SetValue(nameof(PropertyName), value); }
        }

        /// <inheritdoc/>
        public String? PropertyValue
        {
            get { return GetValue(nameof(PropertyValue)); }
            set { SetValue(nameof(PropertyValue), value); }
        }

        /// <inheritdoc/>
        public Boolean IsDescription
        { get { return String.Equals(PropertyName, "MS_Description", StringComparison.OrdinalIgnoreCase); } }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for PropertyItem.
        /// </summary>
        public PropertyItem() : base()
        {
            PropertyId = Guid.NewGuid();

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <inheritdoc/>
        public static TResult Create<TResult>(ICatalogKey catalog, IProperty source)
            where TResult : PropertyItem, new()
        {
            return new TResult()
            {
                CatalogId = catalog.CatalogId,
                DatabaseName = source.DatabaseName,
                Level0Type = source.Level0Type,
                Level0Name = source.Level0Name,
                Level1Type = source.Level1Type,
                Level1Name = source.Level1Name,
                Level2Type = source.Level2Type,
                Level2Name = source.Level2Name,
                PropertyName = source.PropertyName,
                PropertyValue = source.PropertyValue,
            };
        }

        /// <inheritdoc/>
        public virtual void Update(IProperty source)
        { PropertyValue = source.PropertyValue; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(CatalogId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(PropertyId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(string)){ AllowDBNull = false},
            // Parameter Data
            new DataColumn(nameof(Level0Type), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(Level0Name), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(Level1Type), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(Level1Name), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(Level2Type), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(Level2Name), typeof(string)){ AllowDBNull = true},
            // Results Data
            new DataColumn(nameof(PropertyName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(PropertyValue), typeof(string)){ AllowDBNull = false},
            .. TemporalItem.columnDefinitions,

        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization constructor for Database Constraint Item.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected PropertyItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
