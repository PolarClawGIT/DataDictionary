namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Interface for a FileFormat Type Enumeration.
    /// </summary>
    public interface IFileFormatEnumeration : IEnumeration<FileFormatType>
    {
        /// <summary>
        /// List of Extension for the File Format
        /// </summary>
        IEnumerable<String> Extensions { get; }
    }

    /// <summary>
    /// Enumeration support class for Directory Enum.
    /// </summary>
    class FileFormatEnumeration : Enumeration<FileFormatType, FileFormatEnumeration>,
        IFileFormatEnumeration
    {
        public required IEnumerable<String> Extensions { get; init; }


        /// <summary>
        /// Internal Constructor for Directory Enumeration
        /// </summary>
        /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
        FileFormatEnumeration(FileFormatType value, String name) : base(value, name) { }

        FileFormatEnumeration(FileFormatType value) : base(value) { }

        static FileFormatEnumeration()
        {
            List<FileFormatEnumeration> data = new List<FileFormatEnumeration>()
            {
            new FileFormatEnumeration(FileFormatType.Other,         String.Empty){ DisplayName = "Other", Extensions = new List<String>() {String.Empty }},
            new FileFormatEnumeration(FileFormatType.PlainText)    { DisplayName = "Plain Text", Extensions = new List<String>() {"TXT" }},
            new FileFormatEnumeration(FileFormatType.XMLData)      { DisplayName = "XML data", Extensions = new List<String>() {"XML" }},
            new FileFormatEnumeration(FileFormatType.XSLTransform) { DisplayName = "XSL Transform", Extensions = new List<String>() { "XSLT", "XSL" }},
            new FileFormatEnumeration(FileFormatType.T4Template)   { DisplayName = "T4 Template", Extensions = new List<String>() { "TT", "TTInclude" }},
            new FileFormatEnumeration(FileFormatType.XAML)         { DisplayName = "Application Markup", Extensions = new List<String>() { "XAML" }},
            new FileFormatEnumeration(FileFormatType.JavaScript)   { DisplayName = "JavaScript", Extensions = new List<String>() { "JSON" }},
            new FileFormatEnumeration(FileFormatType.SQLScript)    { DisplayName = "SQL Script", Extensions = new List<String>() {"SQL" }},
            new FileFormatEnumeration(FileFormatType.CSharp)       { DisplayName = "C# Code", Extensions = new List<String>() {"CS" }},
            new FileFormatEnumeration(FileFormatType.VisualBasic)  { DisplayName = "VB.Net Code", Extensions = new List<String>() {"VB" }},
            new FileFormatEnumeration(FileFormatType.Mermaid)      { DisplayName = "Mermaid Diagram", Extensions = new List<String>() {"MMD", "MERMAID" }},
            new FileFormatEnumeration(FileFormatType.Markdown)     { DisplayName = "Markdown", Extensions = new List<String>() {"MD" }},
            };

            BuildDictionary(data);
        }
    }
}
