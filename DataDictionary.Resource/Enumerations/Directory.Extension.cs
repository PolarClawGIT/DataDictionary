namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Extensions on TemplateDirectory Enum. 
    /// </summary>
    public static class DirectoryExtension
    {
        /// <summary>
        /// Gets the Details for the TemplateDirectoryType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IDirectoryEnumeration GetEnumeration(this DirectoryType value)
        { return DirectoryEnumeration.GetValue(value); }

        /// <summary>
        /// Try to parse the String into a TemplateDirectoryType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out DirectoryType result)
        {
            if (DirectoryEnumeration.TryParse(value, null, out DirectoryEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = DirectoryType.Null; return false; }
        }
    }
}
