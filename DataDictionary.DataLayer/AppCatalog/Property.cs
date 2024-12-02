namespace DataDictionary.DataLayer.AppCatalog;

//TODO: This will take a lot of re-work. Need to get it to follow the pattern of Information Schema approach.

/// <summary>
/// Base Catalog Property interface (data elements only)
/// </summary>
public interface IProperty: ICatalogKeyName, IPropertyKeyName
{
    /// <summary>
    /// Level 0 (Catalog) Type parameter
    /// </summary>
    string? Level0Type { get; }

    /// <summary>
    /// Level 1 (Object) Type parameter
    /// </summary>
    string? Level1Type { get; }

    /// <summary>
    /// Level 2 (Element) Type parameter
    /// </summary>
    string? Level2Type { get; }

    /// <summary>
    /// Value of the Property.
    /// </summary>
    string? PropertyValue { get; }
}