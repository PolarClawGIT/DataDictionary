using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Enumerations
{
    partial class ScopeIcon
    {
        static Dictionary<ScopeType, Icon> scopeIconMap = new Dictionary<ScopeType, Icon>()
        {
            { ScopeType.Null,                       Resources.Icon_SoftwareDefinitionModel },
            { ScopeType.Application,                Resources.Icon_SoftwareDefinitionModel },
            { ScopeType.ApplicationHelp,            Resources.Icon_Help },
            { ScopeType.ApplicationHelpPage,        Resources.Icon_HelpIndexFile },
            { ScopeType.ApplicationHelpGroup,       Resources.Icon_HelpTableOfContent },
            { ScopeType.ApplicationHelpForm,        Resources.Icon_HelpApplication },
            { ScopeType.ApplicationOption,          Resources.Icon_Settings },
            { ScopeType.ApplicationDocument,        Resources.Icon_Document },
            { ScopeType.ApplicationTimeLine,        Resources.Icon_TimeLine },
            { ScopeType.ApplicationLog,             Resources.Icon_Log },
            { ScopeType.ApplicationConnection,      Resources.Icon_ServerDatabase },

            { ScopeType.Library,                    Resources.Icon_Library },
            { ScopeType.LibraryType,                Resources.Icon_Class },
            { ScopeType.LibraryTypeEvent,           Resources.Icon_Event },
            { ScopeType.LibraryTypeField,           Resources.Icon_Field },
            { ScopeType.LibraryTypeMethod,          Resources.Icon_Method },
            { ScopeType.LibraryNameSpace,           Resources.Icon_Namespace },
            { ScopeType.LibraryTypeProperty,        Resources.Icon_Property },
            { ScopeType.LibraryTypeParameter,       Resources.Icon_Parameter },

            { ScopeType.Database,                   Resources.Icon_Database },
            { ScopeType.DatabaseSchema,             Resources.Icon_Schema },
            { ScopeType.DatabaseFunction,           Resources.Icon_ScalarFunction },
            { ScopeType.DatabaseProcedure,          Resources.Icon_Procedure },
            { ScopeType.DatabaseTable,              Resources.Icon_Table },
            { ScopeType.DatabaseDomain,             Resources.Icon_DomainType },
            { ScopeType.DatabaseView,               Resources.Icon_View },
            { ScopeType.DatabaseViewColumn,         Resources.Icon_Column },
            { ScopeType.DatabaseTableColumn,        Resources.Icon_Column },
            { ScopeType.DatabaseConstraint,         Resources.Icon_TableRule },
            { ScopeType.DatabaseConstraintKey,      Resources.Icon_TableKey },
            { ScopeType.DatabaseConstraintCheck,    Resources.Icon_ColumnRule },

            { ScopeType.DatabaseProcedureParameter, Resources.Icon_Parameter },
            { ScopeType.DatabaseFunctionParameter,  Resources.Icon_Parameter },
            { ScopeType.DatabaseFunctionColumn,     Resources.Icon_FunctionColumn },

            { ScopeType.DatabaseReference,          Resources.Icon_Dependancy },
            { ScopeType.DatabaseProperty,           Resources.Icon_ExtendedProperty },

            { ScopeType.Model,                      Resources.Icon_SoftwareDefinitionModel },
            { ScopeType.ModelNameSpace,             Resources.Icon_Namespace },
            { ScopeType.ModelSubjectArea,           Resources.Icon_Diagram },
            { ScopeType.ModelDefinition,            Resources.Icon_RichTextBox },
            { ScopeType.ModelProperty,              Resources.Icon_Property },
            { ScopeType.ModelAlias,                 Resources.Icon_Synonym },
            { ScopeType.ModelAttribute,             Resources.Icon_Attribute },
            { ScopeType.ModelAttributeAlias,        Resources.Icon_Synonym },
            { ScopeType.ModelAttributeProperty,     Resources.Icon_Property },
            { ScopeType.ModelAttributeDefinition,   Resources.Icon_RichTextBox },
            { ScopeType.ModelAttributeSubjectArea,  Resources.Icon_Diagram },

            { ScopeType.ModelEntity,                Resources.Icon_Entities },
            { ScopeType.ModelEntityAlias,           Resources.Icon_Synonym },
            { ScopeType.ModelEntityProperty,        Resources.Icon_Property },
            { ScopeType.ModelEntityAttribute,       Resources.Icon_Attribute },
            { ScopeType.ModelEntityDefinition,      Resources.Icon_RichTextBox },
            { ScopeType.ModelEntitySubjectArea,     Resources.Icon_Diagram },

            { ScopeType.ModelProcess,               Resources.Icon_Process },
            { ScopeType.ModelProcessAlias,          Resources.Icon_Synonym },
            { ScopeType.ModelProcessProperty,       Resources.Icon_Property },
            { ScopeType.ModelProcessArgument,       Resources.Icon_Parameter },

            { ScopeType.ModelProcessDefinition,     Resources.Icon_RichTextBox },
            { ScopeType.ModelProcessSubjectArea,    Resources.Icon_Diagram },

            { ScopeType.Scripting,                  Resources.Icon_Script },
            //{ ScopeType.ScriptingTemplate,          Resources.Icon_XMLSchema },
            { ScopeType.ScriptingData,              Resources.Icon_XPath },
            { ScopeType.ScriptingDataObject,        Resources.Icon_XMLDescendant },
            //{ ScopeType.ScriptingDocument,          Resources.Icon_XMLFile },
            { ScopeType.ScriptingTemplateNode,      Resources.Icon_Tag },
            { ScopeType.ScriptingTemplateNodeOwner, Resources.Icon_XMLElement },
            { ScopeType.ScriptingTemplateData,      Resources.Icon_XPath },

            {ScopeType.ScriptingTemplate,           Resources.Icon_Template},
            {ScopeType.ScriptingNode,               Resources.Icon_XMLElement},
            {ScopeType.ScriptingNodeOwner,          Resources.Icon_XMLDescendant},
            {ScopeType.ScriptingObject,             Resources.Icon_Tag},
            {ScopeType.ScriptingSchema,             Resources.Icon_XMLSchema},
            {ScopeType.ScriptingTransform,          Resources.Icon_XMLTransformation},
            {ScopeType.ScriptingDocument,           Resources.Icon_XMLFile},


            { ScopeType.Security,                   Resources.Icon_Lock },
            { ScopeType.SecurityPrincipal,          Resources.Icon_User },
            { ScopeType.SecurityRole,               Resources.Icon_ApplicationRole },
            { ScopeType.SecuritySecurable,          Resources.Icon_Permission },
            };
    }
}
