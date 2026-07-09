using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations;


/// <summary>
/// Enumeration support class for Template Script As type.
/// </summary>
class ScriptAsEnumeration : Enumeration<ScriptAsType, ScriptAsEnumeration>
{
    /// <summary>
    /// Returns the extension used by the Script type.
    /// </summary>
    /// <returns></returns>
    public String Extension { get; init; } = String.Empty;

    /// <summary>
    /// Internal Constructor for Database Routine Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    ScriptAsEnumeration(ScriptAsType value, String name) : base(value, name) { }

    /// <summary>
    /// Internal Constructor for Database Routine Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    ScriptAsEnumeration(ScriptAsType value, String name, String extension) : this(value, name)
    { Extension = extension; }

    static ScriptAsEnumeration()
    {
        List<ScriptAsEnumeration> data = new List<ScriptAsEnumeration>()
        {
            new ScriptAsEnumeration(ScriptAsType.none,   String.Empty) { DisplayName = "not defined"},
            new ScriptAsEnumeration(ScriptAsType.CSharp, "C#",     "cs"),
            new ScriptAsEnumeration(ScriptAsType.VBNet,  "VB.Net", "vb"),
            new ScriptAsEnumeration(ScriptAsType.MsSql,  "Ms SQL", "sql"),
            new ScriptAsEnumeration(ScriptAsType.Text,   "Text",   "txt"),
            new ScriptAsEnumeration(ScriptAsType.XML,    "XML",    "xml"),
        };

        BuildDictionary(data);
    }
}
