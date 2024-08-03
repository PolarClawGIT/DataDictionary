namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Interface for Level1 MS Extended Property Type.
/// </summary>
public interface IDbLevelObjectType : IDbLevelCatalogType
{
    /// <summary>
    /// Level1 MS Extended Property Type.
    /// </summary>
    public DbLevelObjectType ObjectScope { get; }
}

public static class DbLevelObject
{
    public static DbLevelObjectType GetDbLevel(String? value)
    { return DbLevelObjectEnumeration.Parse(value ?? String.Empty, null).Value; }

    public static String GetName(this DbLevelObjectType value)
    { return DbLevelObjectEnumeration.Cast(value).Name; }
}
