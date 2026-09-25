namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// File Format (extensions) Support
    /// </summary>
    public enum FileFormatType
    {
        /// <summary>
        /// Other, Unspecified or unidentified: *.*
        /// </summary>
        Other,

        /// <summary>
        /// Plain Text: *.txt
        /// </summary>
        PlainText,

        /// <summary>
        /// XML Data: *.XML
        /// </summary>
        XMLData,

        /// <summary>
        /// XSL Transform: *.XSLT; *.XSL
        /// </summary>
        XSLTransform,

        /// <summary>
        /// T4 Template: *.TT; *.TTInclude
        /// </summary>
        T4Template,

        /// <summary>
        /// JavaScript Object Notation: *.JSON
        /// </summary>
        JavaScript,

        /// <summary>
        /// Extensible Application Markup Language: XAML
        /// </summary>
        XAML,

        /// <summary>
        /// SQL Script: *.SQL
        /// </summary>
        SQLScript,

        /// <summary>
        /// C# Code: *.CS
        /// </summary>
        CSharp,

        /// <summary>
        /// VB.Net Code: *.VB
        /// </summary>
        VisualBasic,

        /// <summary>
        /// Mermaid Mark Down: *.mmd, *.mermaid
        /// </summary>
        Mermaid,

        /// <summary>
        /// Mark Down: *.MD
        /// </summary>
        Markdown,
    }
}