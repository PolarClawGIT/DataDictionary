using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Enumerations
{
    interface INavigationValue : IScopeEnumeration
    {
        /// <summary>
        /// Grouping behavior.
        /// When True, items with the same ScopeType will be group together.
        /// When False, items will not be group together and appear as individual entries.
        /// This effects navigation components.
        /// </summary>
        Boolean GroupBy { get; }
    }

    static partial class NavigationExtension
    {
        partial class NavigationValue
        {
            /// <summary>
            /// Constructor for the Window Form Scope Enumeration static data.
            /// </summary>
            static NavigationValue()
            {
                List<NavigationValue> data = new List<NavigationValue>() {
                new NavigationValue(ScopeType.Null),
                new NavigationValue(ScopeType.Application),
                new NavigationValue(ScopeType.ApplicationHelp),
                new NavigationValue(ScopeType.ApplicationHelpPage),
                new NavigationValue(ScopeType.ApplicationHelpGroup),
                new NavigationValue(ScopeType.ApplicationHelpForm),
                new NavigationValue(ScopeType.ApplicationOption),
                new NavigationValue(ScopeType.ApplicationDocument),
                new NavigationValue(ScopeType.ApplicationTimeLine),
                new NavigationValue(ScopeType.ApplicationLog),
                new NavigationValue(ScopeType.ApplicationConnection),

                new NavigationValue(ScopeType.Library) {GroupBy = false},
                new NavigationValue(ScopeType.LibraryType) {GroupBy = false},
                new NavigationValue(ScopeType.LibraryTypeEvent) {GroupBy = false},
                new NavigationValue(ScopeType.LibraryTypeField) {GroupBy = false},
                new NavigationValue(ScopeType.LibraryTypeMethod) {GroupBy = false},
                new NavigationValue(ScopeType.LibraryNameSpace) {GroupBy = false},
                new NavigationValue(ScopeType.LibraryTypeProperty) {GroupBy = false},
                new NavigationValue(ScopeType.LibraryTypeParameter) { GroupBy = false},

                new NavigationValue(ScopeType.Database) {GroupBy = false},
                new NavigationValue(ScopeType.DatabaseSchema) {GroupBy = false},
                new NavigationValue(ScopeType.DatabaseFunction),
                new NavigationValue(ScopeType.DatabaseProcedure),
                new NavigationValue(ScopeType.DatabaseTable),
                new NavigationValue(ScopeType.DatabaseDomain),
                new NavigationValue(ScopeType.DatabaseView),
                new NavigationValue(ScopeType.DatabaseViewColumn),
                new NavigationValue(ScopeType.DatabaseTableColumn),
                new NavigationValue(ScopeType.DatabaseConstraint),
                new NavigationValue(ScopeType.DatabaseConstraintKey),
                new NavigationValue(ScopeType.DatabaseConstraintCheck),

                new NavigationValue(ScopeType.DatabaseProcedureParameter),
                new NavigationValue(ScopeType.DatabaseFunctionParameter),
                new NavigationValue(ScopeType.DatabaseFunctionColumn),

                new NavigationValue(ScopeType.DatabaseReference),
                new NavigationValue(ScopeType.DatabaseProperty),

                new NavigationValue(ScopeType.Model) { GroupBy = false},
                new NavigationValue(ScopeType.ModelNameSpace) { GroupBy = false},
                new NavigationValue(ScopeType.ModelSubjectArea) { GroupBy = false},
                new NavigationValue(ScopeType.ModelDefinition),
                new NavigationValue(ScopeType.ModelProperty),
                new NavigationValue(ScopeType.ModelAlias),
                new NavigationValue(ScopeType.ModelAttribute) { GroupBy = false},
                new NavigationValue(ScopeType.ModelAttributeAlias),
                new NavigationValue(ScopeType.ModelAttributeProperty) ,
                new NavigationValue(ScopeType.ModelAttributeDefinition) ,
                new NavigationValue(ScopeType.ModelAttributeSubjectArea) ,

                new NavigationValue(ScopeType.ModelEntity) { GroupBy = false},
                new NavigationValue(ScopeType.ModelEntityAlias),
                new NavigationValue(ScopeType.ModelEntityProperty),
                new NavigationValue(ScopeType.ModelEntityAttribute),
                new NavigationValue(ScopeType.ModelEntityDefinition),
                new NavigationValue(ScopeType.ModelEntitySubjectArea),

                new NavigationValue(ScopeType.ModelProcess) { GroupBy = false},
                new NavigationValue(ScopeType.ModelProcessAlias),
                new NavigationValue(ScopeType.ModelProcessProperty),
                new NavigationValue(ScopeType.ModelProcessArgument),

                new NavigationValue(ScopeType.ModelProcessDefinition),
                new NavigationValue(ScopeType.ModelProcessSubjectArea),

                new NavigationValue(ScopeType.Scripting),
                new NavigationValue(ScopeType.ScriptingData),
                new NavigationValue(ScopeType.ScriptingDataObject),
                new NavigationValue(ScopeType.ScriptingTemplateNode),
                new NavigationValue(ScopeType.ScriptingTemplateNodeOwner),
                new NavigationValue(ScopeType.ScriptingTemplateData),

                new NavigationValue(ScopeType.ScriptingTemplate),
                new NavigationValue(ScopeType.ScriptingNode),
                new NavigationValue(ScopeType.ScriptingNodeOwner),
                new NavigationValue(ScopeType.ScriptingObject),
                new NavigationValue(ScopeType.ScriptingSchema),
                new NavigationValue(ScopeType.ScriptingTransform),
                new NavigationValue(ScopeType.ScriptingDocument),

                new NavigationValue(ScopeType.Security),
                new NavigationValue(ScopeType.SecurityPrincipal),
                new NavigationValue(ScopeType.SecurityRole),
                new NavigationValue(ScopeType.SecuritySecurable),
                };

                BuildDictionary(data);
            }
        }
    }
}
