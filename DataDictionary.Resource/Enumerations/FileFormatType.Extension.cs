using System.Text.RegularExpressions;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Extensions on FileFormat Enum. 
    /// </summary>
    public static class FileFormatExtension
    {
        /// <summary>
        /// Gets the Details for the FileFormatType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IFileFormatEnumeration GetEnumeration(this FileFormatType value)
        { return FileFormatEnumeration.GetValue(value); }

        /// <summary>
        /// Try to parse the String into a FileFormatType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out FileFormatType result)
        {
            if (FileFormatEnumeration.TryParse(value, null, out FileFormatEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = FileFormatType.Other; return false; }
        }

        /// <summary>
        /// Gets the Name of the FileFormatType Enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String GetName(this FileFormatType value)
        {
            if (FileFormatEnumeration.TryGetValue(value, out FileFormatEnumeration? enumeration))
            { return enumeration.Name; }
            else { return String.Empty; }
        }

        /// <summary>
        /// Test is a given file name (as string) matches the file extension list of the given FileFormatType.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Boolean IsFileFormat (this FileFormatType value, String? fileName)
        {
            FileFormatEnumeration formats = FileFormatEnumeration.GetValue(value);
            Boolean result = false;

            foreach (var pattern in formats.Extensions)
            {   // Google AI result. Changes common file patterns into a Regular expression and test for match.
                String regexPattern = "^" + Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".") + "$";

                if(fileName is String && Regex.IsMatch(fileName, regexPattern, RegexOptions.IgnoreCase))
                { result = true; }
            }

            return result;
        }
    }
}
