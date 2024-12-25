namespace DataDictionary.DataLayer.AppModel;

/// <summary>
/// Interface for the Model Entity
/// </summary>
public interface IEntity: IEntityKeyName
{
    /// <summary>
    /// Description of the Domain Entity
    /// </summary>
    String? EntityDescription { get; set; }

    /// <summary>
    /// Name within the Subject Area.
    /// </summary>
    String? EntityName { get; set; }
}
