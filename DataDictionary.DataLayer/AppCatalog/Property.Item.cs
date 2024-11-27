using System.Data;
using Toolbox.BindingTable;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.AppCatalog
{


    /// <summary>
    /// Interface for MS SQL ExtendedProperty as stored in the Application Database.
    /// </summary>
    public interface IPropertyItem : ICatalogKeyName, ICatalogKey,
        IPropertyParameter, ITemporalItem
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
        /// Object Type returned sys.fn_listextendedproperty 
        /// </summary>
        String? ObjectType { get; }

        /// <summary>
        /// Object Name returned by sys.fn_listextendedproperty
        /// </summary>
        String? ObjectName { get; }

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
        public Guid? CatalogId { get { return GetValue<Guid>(nameof(CatalogId)); } }

        /// <inheritdoc/>
        public string? DatabaseName { get { return GetValue(nameof(DatabaseName)); } }

        /// <inheritdoc/>
        public string? Level0Type { get { return GetValue(nameof(Level0Type)); } }

        /// <inheritdoc/>
        public string? Level0Name { get { return GetValue(nameof(Level0Name)); } }

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
        public string? Level1Type { get { return GetValue(nameof(Level1Type)); } }

        /// <inheritdoc/>
        public string? Level1Name { get { return GetValue(nameof(Level1Name)); } }

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
        public string? Level2Type { get { return GetValue(nameof(Level2Type)); } }

        /// <inheritdoc/>
        public string? Level2Name { get { return GetValue(nameof(Level2Name)); } }

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

        /// <summary>
        /// Alias of ObjectType
        /// </summary>
        protected String? ObjType { get { return GetValue(nameof(ObjType)); } }

        /// <inheritdoc/>
        public string? ObjectType { get { return ObjType; } }

        /// <summary>
        /// Alias of ObjectName
        /// </summary>
        protected String? ObjName { get { return GetValue(nameof(ObjName)); } }

        /// <inheritdoc/>
        public string? ObjectName { get { return ObjName; } }

        /// <inheritdoc/>
        public string? PropertyName { get { return GetValue(nameof(PropertyName)); } }

        /// <inheritdoc/>
        public string? PropertyValue { get { return GetValue(nameof(PropertyValue)); } }

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
        { }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(CatalogId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(string)){ AllowDBNull = false},
            // Parameter Data
            new DataColumn(nameof(Level0Type), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(Level0Name), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(Level1Type), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(Level1Name), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(Level2Type), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(Level2Name), typeof(string)){ AllowDBNull = true},
            // Results Data
            new DataColumn(nameof(ObjType), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(ObjName), typeof(string)){ AllowDBNull = false},
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

    #region Enum ExtendedProperty translation

    /// <summary>
    /// Helper class that translates the Enums listed above to string values returned by the Database.
    /// Source: https://learn.microsoft.com/en-us/sql/relational-databases/system-functions/sys-fn-listextendedproperty-transact-sql?view=sql-server-ver16
    /// </summary>
    static class ExtendedPropertyExtension
    {

    }
    #endregion
}
