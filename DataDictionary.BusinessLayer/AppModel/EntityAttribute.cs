using DataDictionary.BusinessLayer.ToolSet;

namespace DataDictionary.BusinessLayer.AppModel;

/// <summary>
/// Delegate for the Function that returns the list of Attributes that match the path provided.
/// </summary>
/// <param name="path"></param>
/// <returns></returns>
delegate IEnumerable<IAttributeValue> FindAttributes(PathIndex path);