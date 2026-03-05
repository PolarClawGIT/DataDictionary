namespace DataDictionary.DataLayer.AppCatalog
{
    //TODO: This will take a lot of re-work. Need to get it to follow the pattern of Information Schema approach.

    /// <summary>
    /// Base Catalog Property interface (data elements only)
    /// </summary>
    public interface IProperty: ICatalogKeyName, IPropertyKeyName
    {
        /// <summary>
        /// Level 0 (Catalog) Type parameter
        /// </summary>
        string? Level0Type { get; }

        /// <summary>
        /// Level 1 (Object) Type parameter
        /// </summary>
        string? Level1Type { get; }

        /// <summary>
        /// Level 2 (Element) Type parameter
        /// </summary>
        string? Level2Type { get; }

        /// <summary>
        /// Value of the Property.
        /// </summary>
        string? PropertyValue { get; }
    }

    /// <summary>  
    /// Static class containing constants related to the Properties database operations.
    /// </summary>  
    static class Property
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(Property));

        /// <summary>  
        /// The stored procedure for retrieving a property.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetProperty]");

        /// <summary>  
        /// The parameter name for the property ID in stored procedures.  
        /// </summary>  
        public readonly static String PropertyId = "@PropertyId";

        /// <summary>  
        /// The stored procedure for setting a property.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetProperty]");

        /// <summary>  
        /// The table type used for properties.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttProperty]");
    }
}