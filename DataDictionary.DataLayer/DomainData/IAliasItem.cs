using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.DomainData
{
    /// <summary>
    /// Interface common to Alias Items.
    /// </summary>
    public interface IAliasItem
    {
        /// <summary>
        /// Application Scope of the Alias.
        /// </summary>
        ScopeType AliasScope { get; }

        /// <summary>
        /// Name of the Alias.
        /// </summary>
        String? AliasName { get; }
    }
}
