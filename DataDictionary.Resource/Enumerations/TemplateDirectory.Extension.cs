namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Extensions on TemplateDirectory Enum. 
    /// </summary>
    public static class TemplateDirectoryExtension
    {
        /// <summary>
        /// Gets the Details for the TemplateDirectoryType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TemplateDirectoryEnumeration GetEnumeration(this TemplateDirectoryType value)
        { return TemplateDirectoryEnumeration.Cast(value); }

        /// <summary>
        /// Try to parse the String into a TemplateDirectoryType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out TemplateDirectoryType result)
        {
            if (TemplateDirectoryEnumeration.TryParse(value, null, out TemplateDirectoryEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = TemplateDirectoryType.Null; return false; }
        }
    }
}
