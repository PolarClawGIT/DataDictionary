// Ignore Spelling: Nullable
namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Base Catalog TableColumn interface (data elements only)
    /// </summary>
    public interface ITableColumn: ITableColumnKeyName, IDataType, IOrdinalPosition, ITableType, IDomainKeyReference
    {
        /// <summary>
        /// Is the Column Nullable
        /// </summary>
        Boolean? IsNullable { get; }

        /// <summary>
        /// Column Default value
        /// </summary>
        String? ColumnDefault { get; }

        /// <summary>
        /// Is the Column an Identity
        /// </summary>
        Boolean? IsIdentity { get; }

        /// <summary>
        /// Is the Column Hidden
        /// </summary>
        Boolean? IsHidden { get; }

        /// <summary>
        /// Is the Column Computed
        /// </summary>
        Boolean? IsComputed { get; }

        /// <summary>
        /// If the Column is Computed, the Computed Definition
        /// </summary>
        String? ComputedDefinition { get; }

        /// <summary>
        /// Type of Always Generated Column. Used by System Version.
        /// </summary>
        String? GeneratedAlwayType { get; }
    }

    /// <summary>  
    /// Static class containing constants related to the Table Column database operations.
    /// </summary>  
    static class TableColumn
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(TableColumn));

        /// <summary>  
        /// Stored procedure to get table column information.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetTableColumn]");

        /// <summary>  
        /// Stored procedure to set table column information.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetTableColumn]");

        /// <summary>  
        /// User-defined table type for table column information.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttTableColumn]");
    }
}