using DataDictionary.DataLayer.ApplicationData.Scope;

namespace DataDictionary.DataLayer.LibraryData
{
    /// <summary>
    /// Interface Scope Type used by the Library.
    /// </summary>
    public interface ILibraryScopeType: IToScopeType
    {
        /// <summary>
        /// Type of Member, such as the name of the Class, Enum, Method, Property, ...
        /// </summary>
        string? MemberType { get; }
    }

    /// <summary>
    /// Implementation Scope Type used by the Library.
    /// </summary>
    public static class LibraryScopeType
    {
        /// <summary>
        /// Returns the Scope Type for the Library Scope.
        /// </summary>
        /// <returns></returns>
        public static ScopeType ToScopeType(this ILibraryScopeType source)
        { return ScopeTypeExtension.ToScopeType(source.MemberType); }
    }
}