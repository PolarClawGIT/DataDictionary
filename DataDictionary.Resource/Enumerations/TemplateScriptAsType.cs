namespace DataDictionary.Resource.Enumerations
{

    /// <summary>
    /// List of supported Script types.
    /// </summary>
    public enum TemplateScriptAsType
    {
        /// <summary>
        /// Not Scripted
        /// </summary>
        none,

        /// <summary>
        /// Microsoft C# code
        /// </summary>
        CSharp,

        /// <summary>
        /// Microsoft Visual Basic code
        /// </summary>
        VBNet,


        /// <summary>
        /// Microsoft SQL script
        /// </summary>
        MsSql,

        /// <summary>
        /// Script as Text
        /// </summary>
        Text,

        /// <summary>
        /// Script As XElement
        /// </summary>
        XML
    }
}
