namespace DataDictionary.DataLayer.ScriptingData.Template
{
    /// <summary>
    /// List of supported Script Element Data As types.
    /// </summary>
    public enum ElementDataAsType
    {
        /// <summary>
        /// Unknown Element Data Type
        /// </summary>
        Null,

        /// <summary>
        /// Script the Element Data as plain text (as is)
        /// </summary>
        Text,

        /// <summary>
        /// Script the Element Data with a CData tag.
        /// </summary>
        CData,

        /// <summary>
        /// Script the Element Data as an XML fragment
        /// </summary>
        XML
    }

    /// <summary>
    /// Interface for Scripting ScriptAs Key.
    /// </summary>
    public interface IElementDataAsType : IKey
    {
        /// <summary>
        /// How the Element Data is to be formated.
        /// </summary>
        ElementDataAsType DataAs { get; }
    }
}
