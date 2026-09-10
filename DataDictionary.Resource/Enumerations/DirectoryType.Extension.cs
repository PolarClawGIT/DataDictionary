namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Extensions on Directory Enum. 
    /// </summary>
    public static class DirectoryExtension
    {
        /// <summary>
        /// Gets the Details for the DirectoryType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IDirectoryEnumeration GetEnumeration(this DirectoryType value)
        { return DirectoryEnumeration.GetValue(value); }

        /// <summary>
        /// Try to parse the String into a DirectoryType enum.
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

        /// <summary>
        /// Gets the Name of the DirectoryType Enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String GetName(this DirectoryType value)
        {
            if (DirectoryEnumeration.TryGetValue(value, out DirectoryEnumeration? enumeration))
            { return enumeration.Name; }
            else { return String.Empty; }
        }

        /// <summary>
        /// Gets the SpecialFolder of the DirectoryType Enum. Used as the RootFolder.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Environment.SpecialFolder GetFolder(this DirectoryType value)
        {
            if (DirectoryEnumeration.TryGetValue(value, out DirectoryEnumeration? enumeration))
            { return enumeration.SpecialFolder; }
            else { return Environment.SpecialFolder.MyDocuments; }
        }
    }
}
