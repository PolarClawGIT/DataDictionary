namespace DataDictionary.DataLayer.AppModel;

/// <summary>
/// Interface for the Domain Property
/// </summary>
public interface IDomainProperty : IPropertyKey
{
    /// <summary>
    /// Property Value. Type and Format is dependent on PeropertyId.
    /// </summary>
    public String? PropertyValue { get; }
}