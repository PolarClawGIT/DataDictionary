namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Interface for Scripting ScriptAs Key.
    /// </summary>
    public interface IScriptAsType
    {
        /// <summary>
        /// Type of Script that is Generated
        /// </summary>
        ScriptAsType ScriptAs { get; }
    }

    /// <summary>
    /// List of supported Script types.
    /// </summary>
    public enum ScriptAsType // TemplateScriptAsEnumeration
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