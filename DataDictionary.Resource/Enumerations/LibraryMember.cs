namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Interface for Database MemberType Key.
/// </summary>
public interface ILibraryMemberType
{
    /// <summary>
    /// Type of Member (NameSpace, Type, Property, Field, ...)
    /// </summary>
    LibraryMemberType MemberType { get; }
}
