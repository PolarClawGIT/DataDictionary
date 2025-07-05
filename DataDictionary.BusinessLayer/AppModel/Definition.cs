using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>  
    /// Delegate for attempting to retrieve a definition value based on a definition index.  
    /// </summary>  
    /// <param name="definitionIndex">The index of the definition to retrieve.</param>  
    /// <param name="definitionValue">The retrieved definition value, if found; otherwise, null.</param>  
    /// <returns>True if the definition value was successfully retrieved; otherwise, false.</returns>  
    public delegate Boolean TryGetDefinition(IDefinitionIndex definitionIndex, [NotNullWhen(true)] out IDefinitionValue? definitionValue);
}
