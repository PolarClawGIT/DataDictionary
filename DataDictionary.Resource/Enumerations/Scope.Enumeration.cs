namespace DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Enumeration support class for Scope Type.
/// </summary>
public class ScopeEnumeration : Enumeration<ScopeType, ScopeEnumeration>
{
    /// <summary>
    /// Internal Constructor for Scope Type Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    protected ScopeEnumeration() : base()
    { }

    static ScopeEnumeration()
    {
        List<ScopeEnumeration> data = new List<ScopeEnumeration>()
        {
            new ScopeEnumeration() { Value = ScopeType.Null, Name = String.Empty, DisplayName = "not defined" },

            new ScopeEnumeration() { Value = ScopeType.Library,                    Name = "Library"                                 , DisplayName = "Library" },
            new ScopeEnumeration() { Value = ScopeType.LibraryNameSpace,           Name = "Library.NameSpace"                       , DisplayName = "Library.NameSpace" },
            new ScopeEnumeration() { Value = ScopeType.LibraryType,                Name = "Library.NameSpace.Type"                  , DisplayName = "Library.NameSpace.Type" },
            new ScopeEnumeration() { Value = ScopeType.LibraryTypeEvent,           Name = "Library.NameSpace.Type.Event"            , DisplayName = "Library.NameSpace.Type.Event" },
            new ScopeEnumeration() { Value = ScopeType.LibraryTypeField,           Name = "Library.NameSpace.Type.Field"            , DisplayName = "Library.NameSpace.Type.Field" },
            new ScopeEnumeration() { Value = ScopeType.LibraryTypeMethod,          Name = "Library.NameSpace.Type.Method"           , DisplayName = "Library.NameSpace.Type.Method" },
            new ScopeEnumeration() { Value = ScopeType.LibraryTypeProperty,        Name = "Library.NameSpace.Type.Property"         , DisplayName = "Library.NameSpace.Type.Property" },
            new ScopeEnumeration() { Value = ScopeType.LibraryMethodParameter,     Name = "Library.NameSpace.Type.Method.Parameter" , DisplayName = "Library.NameSpace.Type.Method.Parameter" },
            new ScopeEnumeration() { Value = ScopeType.LibraryPropertyParameter,   Name = "Library.NameSpace.Type.Property.Parameter", DisplayName = "Library.NameSpace.Type.Method.Parameter" },
            new ScopeEnumeration() { Value = ScopeType.Database,                   Name = "Database"                                , DisplayName = "Database" },
            new ScopeEnumeration() { Value = ScopeType.DatabaseSchema,             Name = "Database.Schema"                         , DisplayName = "Database.Schema" },
            new ScopeEnumeration() { Value = ScopeType.DatabaseFunction,           Name = "Database.Schema.Function"                , DisplayName = "Database.Schema.Function" },
            new ScopeEnumeration() { Value = ScopeType.DatabaseProcedure,          Name = "Database.Schema.Procedure"               , DisplayName = "Database.Schema.Procedure" },
            new ScopeEnumeration() { Value = ScopeType.DatabaseTable,              Name = "Database.Schema.Table"                   , DisplayName = "Database.Schema.Table"},
            new ScopeEnumeration() { Value = ScopeType.DatabaseDomain,             Name = "Database.Schema.Type"                    , DisplayName = "Database.Schema.Type" },
            new ScopeEnumeration() { Value = ScopeType.DatabaseView,               Name = "Database.Schema.View"                    , DisplayName = "Database.Schema.View" },
            new ScopeEnumeration() { Value = ScopeType.DatabaseViewColumn,         Name = "Database.Schema.View.Column"             , DisplayName = "Database.Schema.View.Column" },
            new ScopeEnumeration() { Value = ScopeType.DatabaseTableColumn,        Name = "Database.Schema.Table.Column"            , DisplayName = "Database.Schema.Table.Column" },
            new ScopeEnumeration() { Value = ScopeType.DatabaseTableConstraint,    Name = "Database.Schema.Table.Constraint"        , DisplayName = "Database.Schema.Table.Constraint" },
            new ScopeEnumeration() { Value = ScopeType.DatabaseProcedureParameter, Name = "Database.Schema.Procedure.Parameter"     , DisplayName = "Database.Schema.Procedure.Parameter" },
            new ScopeEnumeration() { Value = ScopeType.DatabaseFunctionParameter,  Name = "Database.Schema.Function.Parameter"      , DisplayName = "Database.Schema.Function.Parameter" },
            new ScopeEnumeration() { Value = ScopeType.Model,                      Name = "Model"                                   , DisplayName = "Model" },
            new ScopeEnumeration() { Value = ScopeType.ModelProperty,              Name = "Model.Property"                          , DisplayName = "Model.Property" },
            new ScopeEnumeration() { Value = ScopeType.ModelDefinition,            Name = "Model.Definition"                        , DisplayName = "Model.Definition" },
            new ScopeEnumeration() { Value = ScopeType.ModelAttribute,             Name = "Model.Attribute"                         , DisplayName = "Model.Attribute" },
            new ScopeEnumeration() { Value = ScopeType.ModelAttributeAlias,        Name = "Model.Attribute.Alias"                   , DisplayName = "Model.Attribute.Alias" },
            new ScopeEnumeration() { Value = ScopeType.ModelAttributeProperty,     Name = "Model.Attribute.Property"                , DisplayName = "Model.Attribute.Property" },
            new ScopeEnumeration() { Value = ScopeType.ModelAttributeDefinition,   Name = "Model.Attribute.Definition"              , DisplayName = "Model.Attribute.Definition" },
            new ScopeEnumeration() { Value = ScopeType.ModelEntity,                Name = "Model.Entity"                            , DisplayName = "Model.Entity" },
            new ScopeEnumeration() { Value = ScopeType.ModelEntityAlias,           Name = "Model.Entity.Alias"                      , DisplayName = "Model.Entity.Alias" },
            new ScopeEnumeration() { Value = ScopeType.ModelEntityProperty,        Name = "Model.Entity.Property"                   , DisplayName = "Model.Entity.Property" },
            new ScopeEnumeration() { Value = ScopeType.ModelEntityDefinition,      Name = "Model.Entity.Definition"                 , DisplayName = "Model.Entity.Definition" },
            new ScopeEnumeration() { Value = ScopeType.ModelEntityAttribute,       Name = "Model.Entity.Attribute"                  , DisplayName = "Model.Entity.Attribute" },
            new ScopeEnumeration() { Value = ScopeType.ModelSubjectArea,           Name = "Model.SubjectArea"                       , DisplayName = "Model.SubjectArea" },
            new ScopeEnumeration() { Value = ScopeType.ModelNameSpace,             Name = "Model.NameSpace"                         , DisplayName = "Model.NameSpace" },
            new ScopeEnumeration() { Value = ScopeType.Scripting,                  Name = "Scripting"                               , DisplayName = "Scripting" },
            new ScopeEnumeration() { Value = ScopeType.ScriptingSchema,            Name = "Scripting.Schema"                        , DisplayName = "Scripting.Schema" },
            new ScopeEnumeration() { Value = ScopeType.ScriptingSchemaElement,     Name = "Scripting.Schema.Element"                , DisplayName = "Scripting.Schema.Element" },
            new ScopeEnumeration() { Value = ScopeType.ScriptingTransform,         Name = "Scripting.Transform"                     , DisplayName = "Scripting.Transform" },
            new ScopeEnumeration() { Value = ScopeType.ScriptingSelection,         Name = "Scripting.Selection"                     , DisplayName = "Scripting.Selection" },
            new ScopeEnumeration() { Value = ScopeType.ScriptingSelectionPath,     Name = "Scripting.Selection.Path"                , DisplayName = "Scripting.Selection.Path" },
            new ScopeEnumeration() { Value = ScopeType.ScriptingTemplate,          Name = "Scripting.Template"                      , DisplayName = "Scripting.Template" },
            new ScopeEnumeration() { Value = ScopeType.ScriptingTemplatePath,      Name = "Scripting.Template.Path"                 , DisplayName = "Scripting.Template.Path" },
            new ScopeEnumeration() { Value = ScopeType.ScriptingTemplateNode,      Name = "Scripting.Template.Node"                 , DisplayName = "Scripting.Template.Node" },
            new ScopeEnumeration() { Value = ScopeType.ScriptingTemplateAttribute, Name = "Scripting.Template.Attribute"            , DisplayName = "Scripting.Template.Attribute" },
            new ScopeEnumeration() { Value = ScopeType.ScriptingTemplateDocument,  Name = "Scripting.Template.Document"             , DisplayName = "Scripting.Template.Document" },

        };

        BuildDictionary(data);
    }
}
