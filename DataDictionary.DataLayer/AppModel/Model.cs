namespace DataDictionary.DataLayer.AppModel;

/// <summary>
/// Base Model Interface (data elements only)
/// </summary>
public interface IModel
{
    /// <summary>
    /// Title for the Model.
    /// </summary>
   String? ModelTitle { get; set; }

    /// <summary>
    /// Description for the Model.
    /// </summary>
    String? ModelDescription { get; set; }
}