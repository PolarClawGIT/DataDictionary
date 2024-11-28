using System.Data;
using Toolbox.BindingTable;
using DataDictionary.Resource.Enumerations;

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
    public class PropertyItem : BindingTableRow, IPropertyItem
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
            private set { SetValue<Guid>(nameof(PropertyId), value); }
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
                if (DbLevelCatalogEnumeration.TryParse(value, null, out DbLevelCatalogEnumeration? result))
                { return result.Value; }
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
                if (DbLevelObjectEnumeration.TryParse(value, null, out DbLevelObjectEnumeration? result))
                { return result.Value; }
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
                if (DbLevelElementEnumeration.TryParse(value, null, out DbLevelElementEnumeration? result))
                { return result.Value; }
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


        /// <summary>
        /// Constructor for PropertyItem.
        /// </summary>
        public PropertyItem() : base()
        { PropertyId = Guid.NewGuid(); }

        /// <summary>
        /// Generic constructor of a PropertyItem
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="catalog"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        internal static TResult Create<TResult>(ICatalogKey catalog, IProperty source)
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

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
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
            // Temporal Data
            new DataColumn(nameof(CreatedBy), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CreatedOn), typeof(DateTime)){ AllowDBNull = true},
            new DataColumn(nameof(RemovedBy), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(RemovedOn), typeof(DateTime)){ AllowDBNull = true},
            new DataColumn(nameof(IsInserted), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsUpdated), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsDeleted), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsCurrent), typeof(Boolean)){ AllowDBNull = true},

        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }
    }
}
