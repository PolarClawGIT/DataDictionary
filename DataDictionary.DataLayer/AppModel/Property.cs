namespace DataDictionary.DataLayer.AppModel;

/// <summary>
/// Interface for the Model Property
/// </summary>
public interface IProperty : IPropertyKey
{
    /// <summary>
    /// Property Value. Type and Format is dependent on PeropertyId.
    /// </summary>
    public String? PropertyValue { get; }
}