namespace DataDictionary.DataLayer.LibraryData.Member
{
    /// <summary>
    /// Scope Type used by the Library.
    /// </summary>
    public interface ILibraryScopeType
    {
        /// <summary>
        /// Type of Member, such as the name of the Class, Enum, Method, Property, ...
        /// </summary>
        string? MemberType { get; }
    }
}