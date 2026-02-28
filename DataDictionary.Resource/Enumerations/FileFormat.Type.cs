using System;
using System.Collections.Generic;
using System.Text;

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
        /// Mark Down: *.MD
        /// </summary>
        Markdown,

        /// <summary>
        /// Mermaid Mark Down: *.mmd, *.mermaid
        /// </summary>
        Mermaid,
    }
}
