namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Interface for Level2 MS Extended Property Type.
/// </summary>
public interface IDbLevelElementType : IDbLevelObjectType
{
    /// <summary>
    /// Level2 MS Extended Property Type.
    /// </summary>
    public DbLevelElementType ElementScope { get; }
}

public static class DbLevelElement
{
    public static DbLevelElementType GetDbLevel(String? value)
    { return DbLevelElementEnumeration.Parse(value ?? String.Empty, null).Value; }

    public static String GetName(this DbLevelElementType value)
    { return DbLevelElementEnumeration.Cast(value).Name; }
}