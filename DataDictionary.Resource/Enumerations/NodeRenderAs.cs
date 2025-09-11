namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Interface for Scripting NodeValueAs
    /// </summary>
    public interface INodeRenderAs
    {
        /// <summary>
        /// How the Value of the Node is to be rendered.
        /// </summary>
        NodeRenderAsType NodeRenderAs { get; }
    }
}
