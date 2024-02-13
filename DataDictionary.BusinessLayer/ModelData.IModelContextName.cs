using DataDictionary.BusinessLayer.ContextName;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Interface component for the Model Namespace
    /// </summary>
    /// <remarks>When combined with the Extension class, this approximates multi-inheritance.</remarks>
    public interface IModelContextName
    {
        /// <summary>
        /// List of available Context Names (NameSpaces)
        /// </summary>
        ContextNameDictionary ContextName { get; }
    }


}
