namespace DataDictionary.DataLayer.AppModel;

/// <summary>
/// Interface for the Model Process
/// </summary>
public interface IProcess : IProcessKeyName, IProcessSubjectAreaName
{
    /// <summary>
    /// Description of the Domain Process
    /// </summary>
    String? ProcessDescription { get; set; }
}