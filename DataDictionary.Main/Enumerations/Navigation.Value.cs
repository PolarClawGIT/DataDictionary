using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Enumerations
{
    static partial class NavigationExtention
    {
        partial class Enumeration
        {
            /// <summary>
            /// Constructor for the Window Form Scope Enumeration static data.
            /// </summary>
            static Enumeration()
            {
                List<Enumeration> data = new List<Enumeration>()
            {
                new Enumeration(ScopeType.Null),

                new Enumeration(ScopeType.Application,               Resources.Icon_SoftwareDefinitionModel),
                new Enumeration(ScopeType.ApplicationHelp,           Resources.Icon_Help,
                    new(CommandType.Open,    Resources.Icon_HelpOffset16.MergeImage(Resources.OpenItem)),
                    new(CommandType.Add,     Resources.Icon_HelpOffset16.MergeImage(Resources.NewItem)),
                    new(CommandType.Delete,  Resources.Icon_HelpOffset16.MergeImage(Resources.DeleteItem)),
                    new(CommandType.Import,  Resources.Icon_HelpOffset16.MergeImage(Resources.ImportItem))),
                new Enumeration(ScopeType.ApplicationHelpPage,       Resources.Icon_HelpIndexFile),
                new Enumeration(ScopeType.ApplicationHelpGroup,      Resources.Icon_HelpTableOfContent),
                new Enumeration(ScopeType.ApplicationHelpForm,       Resources.HelpApplication), // TODO: Make this an Icon from base SVG file
                new Enumeration(ScopeType.ApplicationOption,         Resources.Icon_Settings),

                new Enumeration(ScopeType.Library,                    Resources.Icon_Library) {GroupBy = false},
                new Enumeration(ScopeType.LibraryTypeEvent,           Resources.Icon_Event) {GroupBy = false},
                new Enumeration(ScopeType.LibraryTypeField,           Resources.Icon_Field) {GroupBy = false},
                new Enumeration(ScopeType.LibraryTypeMethod,          Resources.Icon_Method) {GroupBy = false},
                new Enumeration(ScopeType.LibraryNameSpace,           Resources.Icon_Namespace) {GroupBy = false},
                new Enumeration(ScopeType.LibraryTypeProperty,        Resources.Icon_Property) {GroupBy = false},
                new Enumeration(ScopeType.LibraryTypeParameter,       Resources.Icon_Parameter) { GroupBy = false},
                new Enumeration(ScopeType.LibraryType,                Resources.Icon_Class) {GroupBy = false},

                new Enumeration(ScopeType.Database,                   Resources.Icon_Database,
                    new(CommandType.OpenDatabase,   ImageHelper.MergeImage(Resources.Table, Resources.OpenItem)),
                    new(CommandType.SaveDatabase,   ImageHelper.MergeImage(Resources.Table, Resources.SaveItem)),
                    new(CommandType.DeleteDatabase, ImageHelper.MergeImage(Resources.Table, Resources.DeleteItem)))
                    {GroupBy = false},
                new Enumeration(ScopeType.DatabaseSchema,             Resources.Icon_Schema) {GroupBy = false},
                new Enumeration(ScopeType.DatabaseFunction,           Resources.Icon_ScalarFunction),
                new Enumeration(ScopeType.DatabaseProcedure,          Resources.Icon_Procedure),
                new Enumeration(ScopeType.DatabaseTable,              Resources.Icon_Table),
                new Enumeration(ScopeType.DatabaseDomain,             Resources.Icon_DomainType),
                new Enumeration(ScopeType.DatabaseView,               Resources.Icon_View),
                new Enumeration(ScopeType.DatabaseViewColumn,         Resources.Icon_Column),
                new Enumeration(ScopeType.DatabaseTableColumn,        Resources.Icon_Column),
                new Enumeration(ScopeType.DatabaseConstraint,         Resources.Icon_Column),
                new Enumeration(ScopeType.DatabaseConstraintColumn,   Resources.Icon_Key),

                new Enumeration(ScopeType.DatabaseProcedureParameter, Resources.Icon_Parameter),
                new Enumeration(ScopeType.DatabaseFunctionParameter,  Resources.Icon_Parameter),
                new Enumeration(ScopeType.DatabaseFunctionColumn,     Resources.Icon_Column),

                new Enumeration(ScopeType.DatabaseReference,          Resources.Icon_Dependancy),
                new Enumeration(ScopeType.DatabaseProperty,           Resources.Icon_ExtendedProperty),

                new Enumeration(ScopeType.Model,                      Resources.Icon_SoftwareDefinitionModel) { GroupBy = false},
                new Enumeration(ScopeType.ModelNameSpace,             Resources.Icon_Namespace) { GroupBy = false},
                new Enumeration(ScopeType.ModelSubjectArea,           Resources.Icon_Diagram) { GroupBy = false},
                new Enumeration(ScopeType.ModelDefinition,            Resources.Icon_RichTextBox),
                new Enumeration(ScopeType.ModelProperty,              Resources.Icon_Property),
                new Enumeration(ScopeType.ModelAlias,                 Resources.Icon_Synonym),
                new Enumeration(ScopeType.ModelAttribute,             Resources.Icon_Attribute) { GroupBy = false},
                new Enumeration(ScopeType.ModelAttributeAlias,        Resources.Icon_Synonym),
                new Enumeration(ScopeType.ModelAttributeProperty,     Resources.Icon_Property) ,
                new Enumeration(ScopeType.ModelAttributeDefinition,   Resources.Icon_RichTextBox) ,
                new Enumeration(ScopeType.ModelAttributeSubjectArea,  Resources.Icon_Diagram) ,

                new Enumeration(ScopeType.ModelEntity,                Resources.Icon_Entities) { GroupBy = false},
                new Enumeration(ScopeType.ModelEntityAlias,           Resources.Icon_Synonym),
                new Enumeration(ScopeType.ModelEntityProperty,        Resources.Icon_Property),
                new Enumeration(ScopeType.ModelEntityAttribute,       Resources.Icon_Attribute),
                new Enumeration(ScopeType.ModelEntityDefinition,      Resources.Icon_RichTextBox),
                new Enumeration(ScopeType.ModelEntitySubjectArea,     Resources.Icon_Diagram),

                new Enumeration(ScopeType.ModelProcess,               Resources.Icon_Process) { GroupBy = false},
                new Enumeration(ScopeType.ModelProcessAlias,          Resources.Icon_Synonym),
                new Enumeration(ScopeType.ModelProcessProperty,       Resources.Icon_Property),
                new Enumeration(ScopeType.ModelProcessArgument,       Resources.Icon_Parameter),

                new Enumeration(ScopeType.ModelProcessDefinition,     Resources.Icon_RichTextBox),
                new Enumeration(ScopeType.ModelProcessSubjectArea,    Resources.Icon_Diagram),

                new Enumeration(ScopeType.Scripting,                  Resources.Icon_XSLTransform) { GroupBy = false},
                new Enumeration(ScopeType.ScriptingTemplate,          Resources.Icon_XMLSchema),
                new Enumeration(ScopeType.ScriptingData,              Resources.Icon_XPath),
                new Enumeration(ScopeType.ScriptingDataObject,        Resources.Icon_XMLDescendant),
                new Enumeration(ScopeType.ScriptingTemplateNode,      Resources.Icon_Tag),
                new Enumeration(ScopeType.ScriptingTemplateAttribute, Resources.Icon_XMLAttribute),
                new Enumeration(ScopeType.ScriptingTemplateElement,   Resources.Icon_XMLElement),
                new Enumeration(ScopeType.ScriptingTemplateDocument,  Resources.Icon_XSLTransform),
                new Enumeration(ScopeType.ScriptingTemplateNodeOwner, Resources.Icon_XMLElement),
                new Enumeration(ScopeType.ScriptingTemplateData,      Resources.Icon_XPath),

                new Enumeration(ScopeType.Security,                   Resources.Icon_User),
                new Enumeration(ScopeType.SecurityPrincipal,          Resources.Icon_User),
                new Enumeration(ScopeType.SecurityRole,               Resources.Icon_ApplicationRole),
                new Enumeration(ScopeType.SecuritySecurable,          Resources.Icon_Permission),

            };

                BuildDictionary(data);
            }
        }
    }
}
