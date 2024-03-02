using DataDictionary.BusinessLayer.NameSpace;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Interface component for the Model Namespace
    /// </summary>
    /// <remarks>When combined with the Extension class, this approximates multi-inheritance.</remarks>
    public interface IModelNamespace
    {
        /// <summary>
        /// List of available Model Alias Items from Catalogs and Libraries.
        /// </summary>
        ModelNameSpaceDictionary ModelNamespace { get; }
    }


}
