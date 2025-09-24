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

                new Enumeration(ScopeType.Application,                Resources.Icon_SoftwareDefinitionModel, Resources.SoftwareDefinitionModel),
                new Enumeration(ScopeType.ApplicationHelp,            Resources.Icon_HelpTableOfContent,
                    new(CommandType.Default, Resources.StatusHelp),
                    new(CommandType.Open,    ImageHelper.MergeImage(Resources.StatusHelpAlt, Resources.OpenItem)),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.StatusHelpAlt, Resources.NewItem)),
                    new(CommandType.Delete,  ImageHelper.MergeImage(Resources.StatusHelpAlt, Resources.DeleteItem)),
                    new(CommandType.Import,  ImageHelper.MergeImage(Resources.StatusHelpAlt, Resources.ImportItem))),
                new Enumeration(ScopeType.ApplicationHelpPage,       Resources.Icon_HelpIndexFile,
                    new(CommandType.Default, Resources.HelpIndexFile),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.HelpIndexFile, Resources.NewItem)),
                    new(CommandType.Delete,  ImageHelper.MergeImage(Resources.HelpIndexFile, Resources.DeleteItem))),
                new Enumeration(ScopeType.ApplicationHelpGroup,       Resources.HelpTableOfContents),
                new Enumeration(ScopeType.ApplicationHelpForm,        Resources.HelpApplication),
                new Enumeration(ScopeType.ApplicationOption,          Resources.Icon_Settings, Resources.Settings),

                new Enumeration(ScopeType.Library,                    Resources.Icon_Library,
                    new(CommandType.Default, Resources.Library),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.Library, Resources.NewItem)),
                    new(CommandType.Delete,  ImageHelper.MergeImage(Resources.Library, Resources.DeleteItem)))
                    {GroupBy = false},
                new Enumeration(ScopeType.LibraryTypeEvent,           Resources.Icon_Event, Resources.Event) {GroupBy = false},
                new Enumeration(ScopeType.LibraryTypeField,           Resources.Icon_Field, Resources.Field) {GroupBy = false},
                new Enumeration(ScopeType.LibraryTypeMethod,          Resources.Icon_Method, Resources.Method) {GroupBy = false},
                new Enumeration(ScopeType.LibraryNameSpace,           Resources.Icon_Namespace, Resources.Namespace) {GroupBy = false},
                new Enumeration(ScopeType.LibraryTypeProperty,        Resources.Icon_Property, Resources.Property) {GroupBy = false},
                new Enumeration(ScopeType.LibraryTypeParameter,       Resources.Icon_Parameter, Resources.Parameter) { GroupBy = false},
                new Enumeration(ScopeType.LibraryType,                Resources.Icon_Class, Resources.Class) {GroupBy = false},

                new Enumeration(ScopeType.Database,                   Resources.Icon_Database,
                    new(CommandType.Default,        Resources.Database),
                    new(CommandType.Add,            ImageHelper.MergeImage(Resources.Database, Resources.NewItem)),
                    new(CommandType.Open,           ImageHelper.MergeImage(Resources.Database, Resources.OpenItem)),
                    new(CommandType.Save,           ImageHelper.MergeImage(Resources.Database, Resources.SaveItem)),
                    new(CommandType.Delete,         ImageHelper.MergeImage(Resources.Database, Resources.DeleteItem)),
                    new(CommandType.Export,         ImageHelper.MergeImage(Resources.Database, Resources.ExportItem)),
                    new(CommandType.Import,         ImageHelper.MergeImage(Resources.Database, Resources.ImportItem)),
                    new(CommandType.Select,         ImageHelper.MergeImage(Resources.Database, Resources.SelectItem)),
                    new(CommandType.OpenDatabase,   ImageHelper.MergeImage(Resources.Table, Resources.OpenItem)),
                    new(CommandType.SaveDatabase,   ImageHelper.MergeImage(Resources.Table, Resources.SaveItem)),
                    new(CommandType.DeleteDatabase, ImageHelper.MergeImage(Resources.Table, Resources.DeleteItem)))
                    {GroupBy = false},
                new Enumeration(ScopeType.DatabaseSchema,             Resources.Icon_Schema,         Resources.Schema) {GroupBy = false} ,
                new Enumeration(ScopeType.DatabaseFunction,           Resources.Icon_ScalarFunction, Resources.ScalarFunction) ,
                new Enumeration(ScopeType.DatabaseProcedure,          Resources.Icon_Procedure,      Resources.Procedure) ,
                new Enumeration(ScopeType.DatabaseTable,              Resources.Icon_Table,
                    new(CommandType.Default, Resources.Table),
                    new(CommandType.Export,  ImageHelper.MergeImage(Resources.Table, Resources.ExportItem))),
                new Enumeration(ScopeType.DatabaseDomain,             Resources.Icon_DomainType, Resources.DomainType) ,
                new Enumeration(ScopeType.DatabaseView,               Resources.Icon_View,
                    new(CommandType.Default, Resources.View),
                    new(CommandType.Export,  ImageHelper.MergeImage(Resources.View, Resources.ExportItem))),
                new Enumeration(ScopeType.DatabaseViewColumn,         Resources.Icon_Column,
                    new(CommandType.Default, Resources.Column),
                    new(CommandType.Export,  ImageHelper.MergeImage(Resources.Column, Resources.ExportItem))),
                new Enumeration(ScopeType.DatabaseTableColumn,        Resources.Icon_Column,
                    new(CommandType.Default, Resources.Column),
                    new(CommandType.Export,  ImageHelper.MergeImage(Resources.Column, Resources.ExportItem))),
                new Enumeration(ScopeType.DatabaseConstraint,         Resources.Icon_Column, Resources.Column) ,
                new Enumeration(ScopeType.DatabaseConstraintColumn,   Resources.Icon_Key, Resources.Key) ,

                new Enumeration(ScopeType.DatabaseProcedureParameter, Resources.Icon_Parameter, Resources.Parameter) ,
                new Enumeration(ScopeType.DatabaseFunctionParameter,  Resources.Icon_Parameter, Resources.Parameter) ,
                new Enumeration(ScopeType.DatabaseFunctionColumn,     Resources.Icon_Column, Resources.Column) ,

                new Enumeration(ScopeType.DatabaseReference,          Resources.Icon_Dependancy, Resources.Dependancy) ,
                new Enumeration(ScopeType.DatabaseProperty,           Resources.Icon_ExtendedProperty, Resources.ExtendedProperty) ,

                new Enumeration(ScopeType.Model,                      Resources.Icon_SoftwareDefinitionModel,
                    new(CommandType.Default, Resources.SoftwareDefinitionModel),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.SoftwareDefinitionModel, Resources.NewItem)),
                    new(CommandType.Delete,  ImageHelper.MergeImage(Resources.SoftwareDefinitionModel, Resources.DeleteItem)),
                    new(CommandType.Open,    ImageHelper.MergeImage(Resources.SoftwareDefinitionModel, Resources.OpenItem)),
                    new(CommandType.Save,    ImageHelper.MergeImage(Resources.SoftwareDefinitionModel, Resources.SaveItem)),
                    new(CommandType.Export,  ImageHelper.MergeImage(Resources.SoftwareDefinitionModel, Resources.ExportItem)),
                    new(CommandType.Import,  ImageHelper.MergeImage(Resources.SoftwareDefinitionModel, Resources.ImportItem)),
                    new(CommandType.Select,  ImageHelper.MergeImage(Resources.SoftwareDefinitionModel, Resources.SelectItem)))
                    { GroupBy = false},
                new Enumeration(ScopeType.ModelNameSpace,             Resources.Icon_Namespace, Resources.Namespace) { GroupBy = false},
                new Enumeration(ScopeType.ModelSubjectArea,           Resources.Icon_Diagram, Resources.Diagram) { GroupBy = false},
                new Enumeration(ScopeType.ModelDefinition,            Resources.Icon_RichTextBox, Resources.RichTextBox) ,
                new Enumeration(ScopeType.ModelProperty,              Resources.Icon_Property, Resources.Property) ,
                new Enumeration(ScopeType.ModelAlias,                 Resources.Icon_Synonym,
                    new(CommandType.Default, Resources.Synonym),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.Synonym, Resources.NewItem)),
                    new(CommandType.Select,  ImageHelper.MergeImage(Resources.Synonym, Resources.SelectItem))),
                new Enumeration(ScopeType.ModelAttribute,             Resources.Icon_Attribute,
                    new(CommandType.Default, Resources.Attribute),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.Attribute, Resources.NewItem)),
                    new(CommandType.Delete,  ImageHelper.MergeImage(Resources.Attribute, Resources.DeleteItem)),
                    new(CommandType.Select,  ImageHelper.MergeImage(Resources.Attribute, Resources.SelectItem)))
                    { GroupBy = false},
                new Enumeration(ScopeType.ModelAttributeAlias,        Resources.Icon_Synonym,
                    new(CommandType.Default, Resources.Synonym),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.Synonym, Resources.NewItem)),
                    new(CommandType.Select,  ImageHelper.MergeImage(Resources.Synonym, Resources.SelectItem))),
                new Enumeration(ScopeType.ModelAttributeProperty,     Resources.Icon_Property, Resources.Property) ,
                new Enumeration(ScopeType.ModelAttributeDefinition,   Resources.Icon_RichTextBox, Resources.RichTextBox) ,
                new Enumeration(ScopeType.ModelAttributeSubjectArea,  Resources.Icon_Diagram, Resources.Diagram) ,

                new Enumeration(ScopeType.ModelEntity,                Resources.Icon_Entities,
                    new(CommandType.Default, Resources.Entity),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.Entity, Resources.NewItem)),
                    new(CommandType.Delete,  ImageHelper.MergeImage(Resources.Entity, Resources.DeleteItem)),
                    new(CommandType.Select,  ImageHelper.MergeImage(Resources.Entity, Resources.SelectItem)))
                    { GroupBy = false},
                new Enumeration(ScopeType.ModelEntityAlias,           Resources.Icon_Synonym,
                    new(CommandType.Default, Resources.Synonym),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.Synonym, Resources.NewItem)),
                    new(CommandType.Select,  ImageHelper.MergeImage(Resources.Synonym, Resources.SelectItem))),
                new Enumeration(ScopeType.ModelEntityProperty,        Resources.Icon_Property, Resources.Property) ,
                new Enumeration(ScopeType.ModelEntityAttribute,       Resources.Icon_Attribute, Resources.Attribute) ,
                new Enumeration(ScopeType.ModelEntityDefinition,      Resources.Icon_RichTextBox, Resources.RichTextBox) ,
                new Enumeration(ScopeType.ModelEntitySubjectArea,     Resources.Icon_Diagram, Resources.Diagram) ,

                new Enumeration(ScopeType.ModelProcess,               Resources.Icon_Process,
                    new (CommandType.Default, Resources.Process),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.Process, Resources.NewItem)),
                    new(CommandType.Delete,  ImageHelper.MergeImage(Resources.Process, Resources.DeleteItem)),
                    new(CommandType.Select,  ImageHelper.MergeImage(Resources.Process, Resources.SelectItem)))
                    { GroupBy = false},
                new Enumeration(ScopeType.ModelProcessAlias,          Resources.Icon_Synonym,
                    new (CommandType.Default, Resources.Synonym),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.Synonym, Resources.NewItem)),
                    new(CommandType.Select,  ImageHelper.MergeImage(Resources.Synonym, Resources.SelectItem))),
                new Enumeration(ScopeType.ModelProcessProperty,       Resources.Icon_Property, Resources.Property) ,
                new Enumeration(ScopeType.ModelProcessArgument,       Resources.Icon_Parameter,
                    new (CommandType.Default, Resources.Parameter),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.Parameter, Resources.NewItem)),
                    new(CommandType.Delete,  ImageHelper.MergeImage(Resources.Parameter, Resources.DeleteItem)),
                    new(CommandType.Select,  ImageHelper.MergeImage(Resources.Parameter, Resources.SelectItem))),

                new Enumeration(ScopeType.ModelProcessDefinition,     Resources.Icon_RichTextBox, Resources.RichTextBox) ,
                new Enumeration(ScopeType.ModelProcessSubjectArea,    Resources.Icon_Diagram, Resources.Diagram) ,

                new Enumeration(ScopeType.Scripting,                  Resources.Icon_XMLFile,
                    new(CommandType.Default, Resources.XMLFile),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.XMLFile, Resources.NewItem)),
                    new(CommandType.Delete,  ImageHelper.MergeImage(Resources.XMLFile, Resources.DeleteItem)),
                    new(CommandType.Open,    ImageHelper.MergeImage(Resources.XMLFile, Resources.OpenItem)),
                    new(CommandType.Save,    ImageHelper.MergeImage(Resources.XMLFile, Resources.SaveItem)),
                    new(CommandType.Export,  ImageHelper.MergeImage(Resources.XMLFile, Resources.ExportItem)),
                    new(CommandType.Import,  ImageHelper.MergeImage(Resources.XMLFile, Resources.ImportItem)),
                    new(CommandType.Select,  ImageHelper.MergeImage(Resources.XMLFile, Resources.SelectItem)))
                    { GroupBy = false},
                new Enumeration(ScopeType.ScriptingTemplate,          Resources.Icon_XMLSchema,
                    new(CommandType.Default, Resources.XMLSchema),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.XMLSchema, Resources.NewItem)),
                    new(CommandType.Delete,  ImageHelper.MergeImage(Resources.XMLSchema, Resources.DeleteItem)),
                    new(CommandType.Select,  ImageHelper.MergeImage(Resources.XMLSchema, Resources.SelectItem))),
                new Enumeration(ScopeType.ScriptingData,              Resources.Icon_XPath,
                    new(CommandType.Default, Resources.XPath),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.XPath, Resources.NewItem)),
                    new(CommandType.Delete,  ImageHelper.MergeImage(Resources.XPath, Resources.DeleteItem)),
                    new(CommandType.Select,  ImageHelper.MergeImage(Resources.XPath, Resources.SelectItem))),
                new Enumeration(ScopeType.ScriptingDataObject,        Resources.Icon_XMLDescendant,
                    new(CommandType.Default, Resources.XMLDescendant),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.XMLDescendant, Resources.NewItem)),
                    new(CommandType.Delete,  ImageHelper.MergeImage(Resources.XMLDescendant, Resources.DeleteItem)),
                    new(CommandType.Select,  ImageHelper.MergeImage(Resources.XMLDescendant, Resources.SelectItem))),
                new Enumeration(ScopeType.ScriptingTemplateNode,      Resources.Icon_Tag,
                    new(CommandType.Default, Resources.Tag),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.Tag, Resources.NewItem)),
                    new(CommandType.Delete,  ImageHelper.MergeImage(Resources.Tag, Resources.DeleteItem)),
                    new(CommandType.Select,  ImageHelper.MergeImage(Resources.Tag, Resources.SelectItem))),
                new Enumeration(ScopeType.ScriptingTemplateAttribute, Resources.Icon_XMLAttribute,
                    new(CommandType.Default, Resources.XMLAttribute),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.XMLAttribute, Resources.NewItem)),
                    new(CommandType.Delete,  ImageHelper.MergeImage(Resources.XMLAttribute, Resources.DeleteItem)),
                    new(CommandType.Select,  ImageHelper.MergeImage(Resources.XMLAttribute, Resources.SelectItem))),
                new Enumeration(ScopeType.ScriptingTemplateElement,   Resources.Icon_XMLElement,
                    new(CommandType.Default, Resources.XMLElement),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.XMLElement, Resources.NewItem)),
                    new(CommandType.Delete,  ImageHelper.MergeImage(Resources.XMLElement, Resources.DeleteItem)),
                    new(CommandType.Select,  ImageHelper.MergeImage(Resources.XMLElement, Resources.SelectItem))),
                new Enumeration(ScopeType.ScriptingTemplateDocument,  Resources.Icon_XSLTransform, Resources.XSLTransform) ,
                new Enumeration(ScopeType.ScriptingTemplateNodeOwner, Resources.Icon_XMLElement, Resources.XMLElement) ,
                new Enumeration(ScopeType.ScriptingTemplateData,      Resources.Icon_XPath, Resources.XPath) ,

                new Enumeration(ScopeType.Security,                   Resources.Icon_User, Resources.User),
                new Enumeration(ScopeType.SecurityPrincipal,          Resources.Icon_User,
                    new(CommandType.Default, Resources.User),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.User, Resources.NewItem)),
                    new(CommandType.Delete,  ImageHelper.MergeImage(Resources.User, Resources.DeleteItem))),
                new Enumeration(ScopeType.SecurityRole,               Resources.Icon_ApplicationRole,
                    new(CommandType.Default, Resources.ApplicationRole),
                    new(CommandType.Add,     ImageHelper.MergeImage(Resources.ApplicationRole, Resources.NewItem)),
                    new(CommandType.Delete,  ImageHelper.MergeImage(Resources.ApplicationRole, Resources.DeleteItem))),
                new Enumeration(ScopeType.SecuritySecurable,          Resources.Icon_Permission, Resources.Permission),

            };

                BuildDictionary(data);
            }
        }
    }
}
