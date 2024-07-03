// Place holder for file nesting purposes.
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Interface for DomainPropertyType
/// </summary>
public interface IDomainPropertyType
{
    /// <summary>
    /// Domain Property Type for the Item.
    /// </summary>
    DomainPropertyType PropertyType { get; }
}

public static class DomainPropertyExtension
{
    /// <summary>
    /// Gets the Data from DomainPropertyEnumeration
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static DomainPropertyEnumeration Data(this DomainPropertyType source)
    { return DomainPropertyEnumeration.AsDictionary[source]; }
}