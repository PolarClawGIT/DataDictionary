using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Interface for a class that has a Path and a Scope.
    /// </summary>
    public interface IPathIndex: IScopeType
    {
        // Todo: Need to setup Alias and Template Objects to be comparable to this.
        // In the end, the goal is to be able to compare any given IPathIndex to a target Path/Scope.
        // This should also get rid of the specialized logic for Alias and Template Objects.

        /// <summary>
        /// The NamedPath for the Value
        /// </summary>
        PathItem Path { get; }
    }
}
