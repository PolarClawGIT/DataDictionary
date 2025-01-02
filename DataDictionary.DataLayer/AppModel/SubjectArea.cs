namespace DataDictionary.DataLayer.AppModel;

/// <summary>
/// Base Model SubjectArea Interface (data elements only)
/// </summary>
public interface ISubjectArea
{
    /// <summary>
    /// Description of the Subject Area
    /// </summary>
    String? SubjectAreaDescription { get; }

    /// <summary>
    /// NameSpace used for the Subject Area
    /// </summary>
    String? SubjectName { get; }
}