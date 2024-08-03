// Place holder for file nesting purposes.
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Interface for Level0 MS Extended Property Type.
/// </summary>
public interface IDbLevelCatalogType
{
    /// <summary>
    /// Level0 MS Extended Property Type.
    /// </summary>
    DbLevelCatalogType CatalogScope { get; }
}

public static class DbLevelCatalog
{
    public static DbLevelCatalogType GetDbLevel(String? value)
    { return DbLevelCatalogEnumeration.Parse(value ?? String.Empty, null).Value; }

    public static String GetName(this DbLevelCatalogType value)
    { return DbLevelCatalogEnumeration.Cast(value).Name; }
}
