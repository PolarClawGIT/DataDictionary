using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Enumerations
{
    /// <summary>
    /// ScopeEnumeration with Images and Icons.
    /// Used to hold Navigation Icons and Images.
    /// </summary>
    class NavigationEnumeration : Enumeration<ScopeType, NavigationEnumeration>
    {
        // This class could not be placed in base ScopeEnumeration because framework agnostic.
        // This version is Windows WinForms specific.
        // The System.Drawing.Icon and System.Drawing.Image does not exist in all frameworks.

        /// <summary>
        /// Parent Scope
        /// </summary>
        public ScopeType? Parent { get; init; } = null;

        /// <summary>
        /// Icon used for the ScopeType
        /// </summary>
        public Icon WindowIcon { get; init; } = Resources.Icon_UnknownMember;
        static readonly Icon defaultIcon = Resources.Icon_UnknownMember;

        /// <summary>
        /// List of Images for the Scope Type
        /// </summary>
        public IReadOnlyDictionary<CommandImageType, Image> Images
        { get { return images; } }
        Dictionary<CommandImageType, Image> images { get; init; } = new Dictionary<CommandImageType, Image>();
        static readonly Image defaultImage = Resources.UnknownMember;

        /// <summary>
        /// Grouping behavior.
        /// When True, items with the same ScopeType will be group together.
        /// When False, items will not be group together and appear as individual entries.
        /// This effects navigation components.
        /// </summary>
        public Boolean GroupBy { get; init; } = true;

        /// <summary>
        /// Constructor for the Window Form Scope Enumeration.
        /// </summary>
        /// <param name="scope"></param>
        NavigationEnumeration(ScopeType scope) : base()
        {
            ScopeEnumeration source = ScopeEnumeration.Cast(scope);
            DisplayName = source.DisplayName;
            Name = source.Name;
            Value = source.Value;
            Parent = source.Parent;
        }

        /// <summary>
        /// Constructor for the Window Form Scope Enumeration.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="windowIcon"></param>
        NavigationEnumeration(ScopeType scope, Icon windowIcon) : this(scope)
        { this.WindowIcon = windowIcon; }

        /// <summary>
        /// Constructor for the Window Form Scope Enumeration.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="defaultImage"></param>
        NavigationEnumeration(ScopeType scope, Image defaultImage) : this(scope)
        { this.images.Add(CommandImageType.Default, defaultImage); }

        /// <summary>
        /// Constructor for the Window Form Scope Enumeration.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="images"></param>
        NavigationEnumeration(ScopeType scope, params (CommandImageType scope, Image image)[] images) : this(scope)
        {
            foreach ((CommandImageType scope, Image image) item in images)
            { this.images.Add(item.scope, item.image); }
        }

        /// <summary>
        /// Constructor for the Window Form Scope Enumeration.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="windowIcon"></param>
        /// <param name="defaultImage"></param>
        NavigationEnumeration(ScopeType scope, Icon windowIcon, Image defaultImage) : this(scope, windowIcon)
        { this.images.Add(CommandImageType.Default, defaultImage); }

        /// <summary>
        /// Constructor for the Window Form Scope Enumeration.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="windowIcon"></param>
        /// <param name="images"></param>
        NavigationEnumeration(ScopeType scope, Icon windowIcon, params (CommandImageType scope, Image image)[] images) : this(scope, windowIcon)
        {
            foreach ((CommandImageType scope, Image image) item in images)
            { this.images.Add(item.scope, item.image); }
        }

        /// <summary>
        /// Constructor for the Window Form Scope Enumeration static data.
        /// </summary>
        static NavigationEnumeration()
        {
            List<NavigationEnumeration> data = new List<NavigationEnumeration>()
            {
                new NavigationEnumeration(ScopeType.Null),

                new NavigationEnumeration(ScopeType.Application,                Resources.Icon_SoftwareDefinitionModel, Resources.SoftwareDefinitionModel),
                new NavigationEnumeration(ScopeType.ApplicationHelp,            Resources.Icon_HelpTableOfContent,
                    new(CommandImageType.Default, Resources.StatusHelp),
                    new(CommandImageType.Open, Resources.OpenStatusHelp),
                    new(CommandImageType.Add, Resources.NewStatusHelp),
                    new(CommandImageType.Delete, Resources.DeleteStatusHelp),
                    new(CommandImageType.Import, Resources.ImportStatusHelp)),
                new NavigationEnumeration(ScopeType.ApplicationHelpPage,       Resources.Icon_HelpIndexFile,
                    new(CommandImageType.Default, Resources.StatusHelp),
                    new(CommandImageType.Delete, Resources.DeleteStatusHelp)),
                new NavigationEnumeration(ScopeType.ApplicationHelpGroup,       Resources.HelpIndexFile),
                new NavigationEnumeration(ScopeType.ApplicationOption,          Resources.Icon_Settings, Resources.Settings),

                new NavigationEnumeration(ScopeType.Library,                    Resources.Icon_Library,
                    new(CommandImageType.Default, Resources.Library),
                    new(CommandImageType.Add, Resources.NewLibrary),
                    new(CommandImageType.Delete, Resources.DeleteLibrary)) {GroupBy = false},
                new NavigationEnumeration(ScopeType.LibraryTypeEvent,           Resources.Icon_Event, Resources.Event) {GroupBy = false},
                new NavigationEnumeration(ScopeType.LibraryTypeField,           Resources.Icon_Field, Resources.Field) {GroupBy = false},
                new NavigationEnumeration(ScopeType.LibraryTypeMethod,          Resources.Icon_Method, Resources.Method) {GroupBy = false},
                new NavigationEnumeration(ScopeType.LibraryNameSpace,           Resources.Icon_Namespace, Resources.Namespace) {GroupBy = false},
                new NavigationEnumeration(ScopeType.LibraryTypeProperty,        Resources.Icon_Property, Resources.Property) {GroupBy = false},
                new NavigationEnumeration(ScopeType.LibraryTypeParameter,       Resources.Icon_Parameter, Resources.Parameter) { GroupBy = false},
                new NavigationEnumeration(ScopeType.LibraryType,                Resources.Icon_Class, Resources.Class) {GroupBy = false},

                new NavigationEnumeration(ScopeType.Database,                   Resources.Icon_Database,
                    new(CommandImageType.Default, Resources.Database),
                    new(CommandImageType.Add, Resources.NewDatabase),
                    new(CommandImageType.Delete, Resources.DeleteDatabase),
                    new(CommandImageType.Export, Resources.ExportData)) {GroupBy = false},
                new NavigationEnumeration(ScopeType.DatabaseSchema,             Resources.Icon_Schema, Resources.Schema) {GroupBy = false} ,
                new NavigationEnumeration(ScopeType.DatabaseFunction,           Resources.Icon_ScalarFunction, Resources.ScalarFunction) ,
                new NavigationEnumeration(ScopeType.DatabaseProcedure,          Resources.Icon_Procedure, Resources.Procedure) ,
                new NavigationEnumeration(ScopeType.DatabaseTable,              Resources.Icon_Table,
                    new(CommandImageType.Default, Resources.Table),
                    new(CommandImageType.Export, Resources.ExportData)),
                new NavigationEnumeration(ScopeType.DatabaseDomain,             Resources.Icon_DomainType, Resources.DomainType) ,
                new NavigationEnumeration(ScopeType.DatabaseView,               Resources.Icon_View,
                    new(CommandImageType.Default, Resources.View),
                    new(CommandImageType.Export, Resources.ExportData)),
                new NavigationEnumeration(ScopeType.DatabaseViewColumn,         Resources.Icon_Column,
                    new(CommandImageType.Default, Resources.Column),
                    new(CommandImageType.Export, Resources.ExportData)),
                new NavigationEnumeration(ScopeType.DatabaseTableColumn,        Resources.Icon_Column,
                    new(CommandImageType.Default, Resources.Column),
                    new(CommandImageType.Export, Resources.ExportData)),
                new NavigationEnumeration(ScopeType.DatabaseTableConstraint,    Resources.Icon_Key, Resources.Key) ,
                new NavigationEnumeration(ScopeType.DatabaseProcedureParameter, Resources.Icon_Parameter, Resources.Parameter) ,
                new NavigationEnumeration(ScopeType.DatabaseFunctionParameter,  Resources.Icon_Parameter, Resources.Parameter) ,
                new NavigationEnumeration(ScopeType.DatabaseDependency,         Resources.Icon_Dependancy, Resources.Dependancy) ,
                new NavigationEnumeration(ScopeType.DatabaseProperty, Resources.Icon_ExtendedProperty, Resources.ExtendedProperty) ,

                new NavigationEnumeration(ScopeType.Model,                      Resources.Icon_SoftwareDefinitionModel, Resources.SoftwareDefinitionModel) { GroupBy = false},
                new NavigationEnumeration(ScopeType.ModelNameSpace,             Resources.Icon_Namespace, Resources.Namespace) { GroupBy = false},
                new NavigationEnumeration(ScopeType.ModelSubjectArea,           Resources.Icon_Diagram, Resources.Diagram) { GroupBy = false},
                new NavigationEnumeration(ScopeType.ModelDefinition,            Resources.Icon_RichTextBox, Resources.RichTextBox) ,
                new NavigationEnumeration(ScopeType.ModelProperty,              Resources.Icon_Property, Resources.Property) ,
                new NavigationEnumeration(ScopeType.ModelAttribute,             Resources.Icon_Attribute,
                    new(CommandImageType.Default, Resources.Attribute),
                    new(CommandImageType.Add, Resources.NewAttribute),
                    new(CommandImageType.Select, Resources.SelectAttribute),
                    new(CommandImageType.Delete, Resources.DeleteAttribute)) { GroupBy = false},
                new NavigationEnumeration(ScopeType.ModelAttributeAlias,        Resources.Icon_Synonym,
                    new(CommandImageType.Default, Resources.Synonym),
                    new(CommandImageType.Select, Resources.SelectSynonym),
                    new(CommandImageType.Add, Resources.NewSynonym)),
                new NavigationEnumeration(ScopeType.ModelAttributeProperty,     Resources.Icon_Property, Resources.Property) ,
                new NavigationEnumeration(ScopeType.ModelAttributeDefinition,   Resources.Icon_RichTextBox, Resources.RichTextBox) ,
                new NavigationEnumeration(ScopeType.ModelEntity,                Resources.Icon_Entities,
                    new(CommandImageType.Default, Resources.Entity),
                    new(CommandImageType.Add, Resources.NewEntity),
                    new(CommandImageType.Select, Resources.SelectEntity),
                    new(CommandImageType.Delete, Resources.DeleteEntity)) { GroupBy = false},
                new NavigationEnumeration(ScopeType.ModelEntityAlias,           Resources.Icon_Synonym,
                    new(CommandImageType.Default, Resources.Synonym),
                    new(CommandImageType.Select, Resources.SelectSynonym),
                    new(CommandImageType.Add, Resources.NewSynonym)),
                new NavigationEnumeration(ScopeType.ModelEntityProperty,        Resources.Icon_Property, Resources.Property) ,
                new NavigationEnumeration(ScopeType.ModelEntityAttribute,       Resources.Icon_Attribute, Resources.Attribute) ,
                new NavigationEnumeration(ScopeType.ModelEntityDefinition,      Resources.Icon_RichTextBox, Resources.RichTextBox) ,

                new NavigationEnumeration(ScopeType.Scripting,                  Resources.Icon_XmlFile, Resources.XmlFile) ,
                new NavigationEnumeration(ScopeType.ScriptingTemplate,          Resources.Icon_XSLTransform, Resources.XSLTransform) ,
                new NavigationEnumeration(ScopeType.ScriptingTemplateNode,      Resources.Icon_XMLSchema, Resources.XMLSchema) ,
                new NavigationEnumeration(ScopeType.ScriptingTemplateAttribute, Resources.Icon_XMLElement, Resources.XMLElement) ,
                new NavigationEnumeration(ScopeType.ScriptingTemplatePath,      Resources.Icon_XPath,
                    new(CommandImageType.Default, Resources.XPath),
                    new(CommandImageType.Select, Resources.SelectXPath),
                    new(CommandImageType.Add, Resources.NewXPath)),
                //Resources.XPath) ,
                new NavigationEnumeration(ScopeType.ScriptingTemplateDocument,  Resources.Icon_XSLTransform, Resources.XmlFile) ,

                new NavigationEnumeration(ScopeType.Scripting,                  Resources.Icon_XmlFile, Resources.XmlFile) ,

                new NavigationEnumeration(ScopeType.Security,                   Resources.Icon_User, Resources.User),
                new NavigationEnumeration(ScopeType.SecurityPrincipal,          Resources.Icon_User,   
                    new(CommandImageType.Default, Resources.User),
                    new(CommandImageType.Add, Resources.NewUser),
                    new(CommandImageType.Delete, Resources.DeleteUser)),
                new NavigationEnumeration(ScopeType.SecurityRole,               Resources.Icon_ApplicationRole,
                    new(CommandImageType.Default, Resources.ApplicationRole),
                    new(CommandImageType.Add, Resources.NewApplicationRole),
                    new(CommandImageType.Delete, Resources.DeleteApplicationRole)),
                new NavigationEnumeration(ScopeType.SecuritySecurable,          Resources.Icon_Permission, Resources.Permission),
                
            };

            BuildDictionary(data);
        }

        /// <summary>
        /// Returns the Image list for all items using the default/Normal image.
        /// </summary>
        /// <returns></returns>
        public static ImageList AsImageList()
        {
            ImageList result = new ImageList();

            foreach (NavigationEnumeration item in Members.Values)
            { result.Images.Add(item.Name, item.GetImage(CommandImageType.Default)); }

            return result;
        }

        /// <summary>
        /// Given the Scope, return the Icon object for it or the default Icon.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public static Icon GetIcon(ScopeType scope)
        {
            if (Members.ContainsKey(scope))
            { return Members[scope].WindowIcon; }
            else { return defaultIcon; }
        }

        /// <summary>
        /// Given the Scope and Image, return the Image object or default Image.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Image GetImage(ScopeType scope, CommandImageType image)
        {
            if (Members.ContainsKey(scope))
            { return Members[scope].GetImage(image); }
            else { return defaultImage; }
        }

        /// <summary>
        /// Given the Scope and Image, return the default Image.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public static Image GetImage(ScopeType scope)
        {
            if (Members.ContainsKey(scope))
            { return Members[scope].GetImage(); }
            else { return defaultImage; }
        }

        /// <summary>
        /// Image, return the Image object or default Image.
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Image GetImage(CommandImageType image)
        {
            if (Images.ContainsKey(image))
            { return Images[image]; }
            else { return GetImage(); }
        }

        /// <summary>
        /// Image, return the default Image.
        /// </summary>
        /// <returns></returns>
        public Image GetImage()
        {
            if (Images.ContainsKey(CommandImageType.Default))
            { return Images[CommandImageType.Default]; }
            else { return defaultImage; }
        }
    }
}
