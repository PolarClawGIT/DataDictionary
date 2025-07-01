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
        /// <summary>  
        /// The stored procedure for retrieving a property.  
        /// </summary>  
        public const string GetProcedure = "[AppCatalog].[procGetProperty]";

        /// <summary>  
        /// The parameter name for the property ID in stored procedures.  
        /// </summary>  
        public const string PropertyId = "@PropertyId";

        /// <summary>  
        /// The stored procedure for setting a property.  
        /// </summary>  
        public const string SetProcedure = "[AppCatalog].[procSetProperty]";

        /// <summary>  
        /// The table type used for properties.  
        /// </summary>  
        public const string TableType = "[AppCatalog].[udttProperty]";
    }
}