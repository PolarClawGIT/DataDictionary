namespace DataDictionary.DataLayer.AppModel;

/// <summary>
/// Interface for the Model Entity
/// </summary>
public interface IEntity: IEntityKeyName, IEntitySubjectAreaName
{
    /// <summary>
    /// Description of the Domain Entity
    /// </summary>
    String? EntityDescription { get; set; }
}
