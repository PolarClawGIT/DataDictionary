namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Extensions on DbLevelCatalog Enum. 
    /// </summary>
    public static class DbLevelCatalogExtension
    {
        /// <summary>
        /// Gets the Details for the DbLevelCatalogType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumeration<DbLevelCatalogType> GetEnumeration(this DbLevelCatalogType value)
        { return DbLevelCatalogEnumeration.GetValue(value); }

        /// <summary>
        /// Try to parse the String into a DbLevelCatalogType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out DbLevelCatalogType result)
        {
            if (DbLevelCatalogEnumeration.TryParse(value, null, out DbLevelCatalogEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = DbLevelCatalogType.Null; return false; }
        }

        public static DbLevelCatalogType GetDbLevel(String? value)
        { return DbLevelCatalogEnumeration.Parse(value ?? String.Empty, null).Value; }

        public static String GetName(this DbLevelCatalogType value)
        { return DbLevelCatalogEnumeration.GetValue(value).Name; }
    }
}
