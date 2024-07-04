namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Interface for Scripting ScriptAs Key.
/// </summary>
public interface IScriptAsType
{
    /// <summary>
    /// Type of Script that is Generated
    /// </summary>
    TemplateScriptAsType ScriptAs { get; }
}
