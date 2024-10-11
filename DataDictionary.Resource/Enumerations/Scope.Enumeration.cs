namespace DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Enumeration support class for Scope Type.
/// </summary>
public class ScopeEnumeration : Enumeration<ScopeType, ScopeEnumeration>
{
    /// <summary>
    /// Parent Scope
    /// </summary>
    public ScopeType? Parent { get; init; } = null;

    /// <summary>
    /// Internal Constructor for Scope Type Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    protected ScopeEnumeration(ScopeType value, String name) : base(value, name) { }

    /// <summary>
    /// Internal Constructor for Scope Type Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    protected ScopeEnumeration(ScopeType value, ScopeType parent, String name) : this(value, name)
    { Parent = parent; }

    /// <summary>
    /// Static constructor, loads data.
    /// </summary>
    static ScopeEnumeration()
    {
        List<ScopeEnumeration> data = new List<ScopeEnumeration>()
        {
            new ScopeEnumeration(ScopeType.Null, String.Empty) { DisplayName = "not defined" },

            new ScopeEnumeration(ScopeType.Application,                "Application"),
            new ScopeEnumeration(ScopeType.ApplicationHelp,            ScopeType.Application,    "Application.Help"),
            new ScopeEnumeration(ScopeType.ApplicationHelpPage,        ScopeType.ApplicationHelp,"Application.Help.Page"),
            new ScopeEnumeration(ScopeType.ApplicationHelpGroup,       ScopeType.ApplicationHelp,"Application.Help.Group"),
            new ScopeEnumeration(ScopeType.ApplicationOption,          ScopeType.Application,    "Application.Option"),
            
            new ScopeEnumeration(ScopeType.Library,                    "Library"),
            new ScopeEnumeration(ScopeType.LibraryNameSpace,           ScopeType.Library,     "Library.NameSpace"),
            new ScopeEnumeration(ScopeType.LibraryType,                ScopeType.Library,     "Library.NameSpace.Type"),
            new ScopeEnumeration(ScopeType.LibraryTypeEvent,           ScopeType.LibraryType, "Library.NameSpace.Type.Event"),
            new ScopeEnumeration(ScopeType.LibraryTypeField,           ScopeType.LibraryType, "Library.NameSpace.Type.Field"),
            new ScopeEnumeration(ScopeType.LibraryTypeMethod,          ScopeType.LibraryType, "Library.NameSpace.Type.Method"),
            new ScopeEnumeration(ScopeType.LibraryTypeProperty,        ScopeType.LibraryType, "Library.NameSpace.Type.Property"),
            new ScopeEnumeration(ScopeType.LibraryTypeParameter,       ScopeType.LibraryType, "Library.NameSpace.Type.Parameter"),
            new ScopeEnumeration(ScopeType.Database,                   "Database"),
            new ScopeEnumeration(ScopeType.DatabaseSchema,             ScopeType.Database,          "Database.Schema"),
            new ScopeEnumeration(ScopeType.DatabaseFunction,           ScopeType.DatabaseSchema,    "Database.Schema.Function"),
            new ScopeEnumeration(ScopeType.DatabaseProcedure,          ScopeType.DatabaseSchema,    "Database.Schema.Procedure"),
            new ScopeEnumeration(ScopeType.DatabaseTable,              ScopeType.DatabaseSchema,    "Database.Schema.Table"),
            new ScopeEnumeration(ScopeType.DatabaseDomain,             ScopeType.DatabaseSchema,    "Database.Schema.Type"),
            new ScopeEnumeration(ScopeType.DatabaseView,               ScopeType.DatabaseSchema,    "Database.Schema.View"),
            new ScopeEnumeration(ScopeType.DatabaseViewColumn,         ScopeType.DatabaseSchema,    "Database.Schema.View.Column"),
            new ScopeEnumeration(ScopeType.DatabaseTableColumn,        ScopeType.DatabaseSchema,    "Database.Schema.Table.Column"),
            new ScopeEnumeration(ScopeType.DatabaseTableConstraint,    ScopeType.DatabaseSchema,    "Database.Schema.Table.Constraint"),
            new ScopeEnumeration(ScopeType.DatabaseProcedureParameter, ScopeType.DatabaseProcedure, "Database.Schema.Procedure.Parameter"),
            new ScopeEnumeration(ScopeType.DatabaseFunctionParameter,  ScopeType.DatabaseFunction,  "Database.Schema.Function.Parameter"),
            new ScopeEnumeration(ScopeType.DatabaseDependency,         ScopeType.DatabaseSchema,    "Database.Schema.Function.Parameter"),
            new ScopeEnumeration(ScopeType.DatabaseExtendedProperties, ScopeType.Database,          "Database.ExtendedProperties"),

            new ScopeEnumeration(ScopeType.Model,                      "Model"),
            new ScopeEnumeration(ScopeType.ModelProperty,              ScopeType.Model,          "Model.Property"),
            new ScopeEnumeration(ScopeType.ModelDefinition,            ScopeType.Model,          "Model.Definition"),
            new ScopeEnumeration(ScopeType.ModelAttribute,             ScopeType.Model,          "Model.Attribute"),
            new ScopeEnumeration(ScopeType.ModelAttributeAlias,        ScopeType.ModelAttribute, "Model.Attribute.Alias"),
            new ScopeEnumeration(ScopeType.ModelAttributeProperty,     ScopeType.ModelAttribute, "Model.Attribute.Property"),
            new ScopeEnumeration(ScopeType.ModelAttributeDefinition,   ScopeType.ModelAttribute, "Model.Attribute.Definition"),
            new ScopeEnumeration(ScopeType.ModelEntity,                ScopeType.Model,          "Model.Entity"),
            new ScopeEnumeration(ScopeType.ModelEntityAlias,           ScopeType.ModelEntity,    "Model.Entity.Alias"),
            new ScopeEnumeration(ScopeType.ModelEntityProperty,        ScopeType.ModelEntity,    "Model.Entity.Property"),
            new ScopeEnumeration(ScopeType.ModelEntityDefinition,      ScopeType.ModelEntity,    "Model.Entity.Definition"),
            new ScopeEnumeration(ScopeType.ModelEntityAttribute,       ScopeType.ModelEntity,    "Model.Entity.Attribute"),
            new ScopeEnumeration(ScopeType.ModelSubjectArea,           ScopeType.Model,          "Model.SubjectArea"),
            new ScopeEnumeration(ScopeType.ModelNameSpace,             ScopeType.Model,          "Model.NameSpace"),

            new ScopeEnumeration(ScopeType.Security,                  "Security"),
            new ScopeEnumeration(ScopeType.SecurityPrinciple,          ScopeType.Security,         "Security.Principle"),
            new ScopeEnumeration(ScopeType.SecurityRole,               ScopeType.Security,         "Security.Role"),
            new ScopeEnumeration(ScopeType.SecurityMembership,         ScopeType.Security,         "Security.Membership"),

            new ScopeEnumeration(ScopeType.Scripting,                  "Scripting"),
            new ScopeEnumeration(ScopeType.ScriptingTemplate,          ScopeType.Scripting,         "Scripting.Template"),
            new ScopeEnumeration(ScopeType.ScriptingTemplatePath,      ScopeType.ScriptingTemplate, "Scripting.Template.Path"),
            new ScopeEnumeration(ScopeType.ScriptingTemplateNode,      ScopeType.ScriptingTemplate, "Scripting.Template.Node"),
            new ScopeEnumeration(ScopeType.ScriptingTemplateAttribute, ScopeType.ScriptingTemplate, "Scripting.Template.Attribute"),
            new ScopeEnumeration(ScopeType.ScriptingTemplateDocument,  ScopeType.ScriptingTemplate, "Scripting.Template.Document"),

        };

        BuildDictionary(data);
    }
}
