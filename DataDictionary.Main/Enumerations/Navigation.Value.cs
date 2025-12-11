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
                List<Enumeration> data = new List<Enumeration>() {
                new Enumeration(ScopeType.Null),
                new Enumeration(ScopeType.Application),
                new Enumeration(ScopeType.ApplicationHelp),
                new Enumeration(ScopeType.ApplicationHelpPage),
                new Enumeration(ScopeType.ApplicationHelpGroup),
                new Enumeration(ScopeType.ApplicationHelpForm),
                new Enumeration(ScopeType.ApplicationOption),
                new Enumeration(ScopeType.ApplicationDocument),
                new Enumeration(ScopeType.ApplicationTimeLine),
                new Enumeration(ScopeType.ApplicationLog),
                new Enumeration(ScopeType.ApplicationConnection),

                new Enumeration(ScopeType.Library) {GroupBy = false},
                new Enumeration(ScopeType.LibraryType) {GroupBy = false},
                new Enumeration(ScopeType.LibraryTypeEvent) {GroupBy = false},
                new Enumeration(ScopeType.LibraryTypeField) {GroupBy = false},
                new Enumeration(ScopeType.LibraryTypeMethod) {GroupBy = false},
                new Enumeration(ScopeType.LibraryNameSpace) {GroupBy = false},
                new Enumeration(ScopeType.LibraryTypeProperty) {GroupBy = false},
                new Enumeration(ScopeType.LibraryTypeParameter) { GroupBy = false},

                new Enumeration(ScopeType.Database) {GroupBy = false},
                new Enumeration(ScopeType.DatabaseSchema) {GroupBy = false},
                new Enumeration(ScopeType.DatabaseFunction),
                new Enumeration(ScopeType.DatabaseProcedure),
                new Enumeration(ScopeType.DatabaseTable),
                new Enumeration(ScopeType.DatabaseDomain),
                new Enumeration(ScopeType.DatabaseView),
                new Enumeration(ScopeType.DatabaseViewColumn),
                new Enumeration(ScopeType.DatabaseTableColumn),
                new Enumeration(ScopeType.DatabaseConstraint),
                new Enumeration(ScopeType.DatabaseConstraintKey),
                new Enumeration(ScopeType.DatabaseConstraintCheck),

                new Enumeration(ScopeType.DatabaseProcedureParameter),
                new Enumeration(ScopeType.DatabaseFunctionParameter),
                new Enumeration(ScopeType.DatabaseFunctionColumn),

                new Enumeration(ScopeType.DatabaseReference),
                new Enumeration(ScopeType.DatabaseProperty),

                new Enumeration(ScopeType.Model) { GroupBy = false},
                new Enumeration(ScopeType.ModelNameSpace) { GroupBy = false},
                new Enumeration(ScopeType.ModelSubjectArea) { GroupBy = false},
                new Enumeration(ScopeType.ModelDefinition),
                new Enumeration(ScopeType.ModelProperty),
                new Enumeration(ScopeType.ModelAlias),
                new Enumeration(ScopeType.ModelAttribute) { GroupBy = false},
                new Enumeration(ScopeType.ModelAttributeAlias),
                new Enumeration(ScopeType.ModelAttributeProperty) ,
                new Enumeration(ScopeType.ModelAttributeDefinition) ,
                new Enumeration(ScopeType.ModelAttributeSubjectArea) ,

                new Enumeration(ScopeType.ModelEntity) { GroupBy = false},
                new Enumeration(ScopeType.ModelEntityAlias),
                new Enumeration(ScopeType.ModelEntityProperty),
                new Enumeration(ScopeType.ModelEntityAttribute),
                new Enumeration(ScopeType.ModelEntityDefinition),
                new Enumeration(ScopeType.ModelEntitySubjectArea),

                new Enumeration(ScopeType.ModelProcess) { GroupBy = false},
                new Enumeration(ScopeType.ModelProcessAlias),
                new Enumeration(ScopeType.ModelProcessProperty),
                new Enumeration(ScopeType.ModelProcessArgument),

                new Enumeration(ScopeType.ModelProcessDefinition),
                new Enumeration(ScopeType.ModelProcessSubjectArea),

                new Enumeration(ScopeType.Scripting) { GroupBy = false},
                new Enumeration(ScopeType.ScriptingTemplate),
                new Enumeration(ScopeType.ScriptingData),
                new Enumeration(ScopeType.ScriptingDataObject),
                new Enumeration(ScopeType.ScriptingTemplateNode),
                new Enumeration(ScopeType.ScriptingTemplateDocument),
                new Enumeration(ScopeType.ScriptingTemplateNodeOwner),
                new Enumeration(ScopeType.ScriptingTemplateData),

                new Enumeration(ScopeType.Security),
                new Enumeration(ScopeType.SecurityPrincipal),
                new Enumeration(ScopeType.SecurityRole),
                new Enumeration(ScopeType.SecuritySecurable),
                };

                BuildDictionary(data);
            }
        }
    }
}
