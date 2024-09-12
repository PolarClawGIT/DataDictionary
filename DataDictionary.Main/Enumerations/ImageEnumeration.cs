using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Enumerations
{
    /// <summary>
    /// ScopeEnumeration with Images and Icons
    /// </summary>
    class ImageEnumeration : Enumeration<ScopeType, ImageEnumeration>
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
        ImageEnumeration(ScopeType scope) : base()
        {
            Resource.Enumerations.ScopeEnumeration source = Resource.Enumerations.ScopeEnumeration.Cast(scope);
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
        ImageEnumeration(ScopeType scope, Icon windowIcon) : this(scope)
        { this.WindowIcon = windowIcon; }

        /// <summary>
        /// Constructor for the Window Form Scope Enumeration.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="defaultImage"></param>
        ImageEnumeration(ScopeType scope, Image defaultImage) : this(scope)
        { this.images.Add(CommandImageType.Default, defaultImage); }

        /// <summary>
        /// Constructor for the Window Form Scope Enumeration.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="images"></param>
        ImageEnumeration(ScopeType scope, params (CommandImageType scope, Image image)[] images) : this(scope)
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
        ImageEnumeration(ScopeType scope, Icon windowIcon, Image defaultImage) : this(scope, windowIcon)
        { this.images.Add(CommandImageType.Default, defaultImage); }

        /// <summary>
        /// Constructor for the Window Form Scope Enumeration.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="windowIcon"></param>
        /// <param name="images"></param>
        ImageEnumeration(ScopeType scope, Icon windowIcon, params (CommandImageType scope, Image image)[] images) : this(scope, windowIcon)
        {
            foreach ((CommandImageType scope, Image image) item in images)
            { this.images.Add(item.scope, item.image); }
        }

        /// <summary>
        /// Constructor for the Window Form Scope Enumeration static data.
        /// </summary>
        static ImageEnumeration()
        {
            List<ImageEnumeration> data = new List<ImageEnumeration>()
            {
                new ImageEnumeration(ScopeType.Null),

                new ImageEnumeration(ScopeType.Application,                Resources.Icon_SoftwareDefinitionModel, Resources.SoftwareDefinitionModel),
                new ImageEnumeration(ScopeType.ApplicationHelp,            Resources.Icon_HelpTableOfContent,
                    new(CommandImageType.Default, Resources.StatusHelp),
                    new(CommandImageType.Add, Resources.NewStatusHelp),
                    new(CommandImageType.Delete, Resources.DeleteStatusHelp)),
                new ImageEnumeration(ScopeType.ApplicationHelpPage,        Resources.StatusHelp),
                new ImageEnumeration(ScopeType.ApplicationHelpGroup,       Resources.HelpIndexFile),
                new ImageEnumeration(ScopeType.ApplicationOption,          Resources.Icon_Settings, Resources.Settings),

                new ImageEnumeration(ScopeType.Library,                    Resources.Icon_Library,
                    new(CommandImageType.Default, Resources.Library),
                    new(CommandImageType.Add, Resources.NewLibrary),
                    new(CommandImageType.Delete, Resources.DeleteLibrary)) {GroupBy = false},
                new ImageEnumeration(ScopeType.LibraryTypeEvent,           Resources.Icon_Event, Resources.Event) {GroupBy = false},
                new ImageEnumeration(ScopeType.LibraryTypeField,           Resources.Icon_Field, Resources.Field) {GroupBy = false},
                new ImageEnumeration(ScopeType.LibraryTypeMethod,          Resources.Icon_Method, Resources.Method) {GroupBy = false},
                new ImageEnumeration(ScopeType.LibraryNameSpace,           Resources.Icon_Namespace, Resources.Namespace) {GroupBy = false},
                new ImageEnumeration(ScopeType.LibraryTypeProperty,        Resources.Icon_Property, Resources.Property) {GroupBy = false},
                new ImageEnumeration(ScopeType.LibraryTypeParameter,       Resources.Icon_Parameter, Resources.Parameter) { GroupBy = false},
                new ImageEnumeration(ScopeType.LibraryType,                Resources.Icon_Class, Resources.Class) {GroupBy = false},
                new ImageEnumeration(ScopeType.Database,                   Resources.Icon_Database,
                    new(CommandImageType.Default, Resources.Database),
                    new(CommandImageType.Add, Resources.NewDatabase),
                    new(CommandImageType.Delete, Resources.DeleteDatabase),
                    new(CommandImageType.Export, Resources.ExportData)) {GroupBy = false},
                new ImageEnumeration(ScopeType.DatabaseSchema,             Resources.Icon_Schema, Resources.Schema) {GroupBy = false} ,
                new ImageEnumeration(ScopeType.DatabaseFunction,           Resources.Icon_ScalarFunction, Resources.ScalarFunction) ,
                new ImageEnumeration(ScopeType.DatabaseProcedure,          Resources.Icon_Procedure, Resources.Procedure) ,
                new ImageEnumeration(ScopeType.DatabaseTable,              Resources.Icon_Table,
                    new(CommandImageType.Default, Resources.Table),
                    new(CommandImageType.Export, Resources.ExportData)),
                new ImageEnumeration(ScopeType.DatabaseDomain,             Resources.Icon_DomainType, Resources.DomainType) ,
                new ImageEnumeration(ScopeType.DatabaseView,               Resources.Icon_View,
                    new(CommandImageType.Default, Resources.View),
                    new(CommandImageType.Export, Resources.ExportData)),
                new ImageEnumeration(ScopeType.DatabaseViewColumn,         Resources.Icon_Column,
                    new(CommandImageType.Default, Resources.Column),
                    new(CommandImageType.Export, Resources.ExportData)),
                new ImageEnumeration(ScopeType.DatabaseTableColumn,        Resources.Icon_Column,
                    new(CommandImageType.Default, Resources.Column),
                    new(CommandImageType.Export, Resources.ExportData)),
                new ImageEnumeration(ScopeType.DatabaseTableConstraint,    Resources.Icon_Key, Resources.Key) ,
                new ImageEnumeration(ScopeType.DatabaseProcedureParameter, Resources.Icon_Parameter, Resources.Parameter) ,
                new ImageEnumeration(ScopeType.DatabaseFunctionParameter,  Resources.Icon_Parameter, Resources.Parameter) ,
                new ImageEnumeration(ScopeType.DatabaseDependency,         Resources.Icon_Dependancy, Resources.Dependancy) ,
                new ImageEnumeration(ScopeType.DatabaseExtendedProperties, Resources.Icon_ExtendedProperty, Resources.ExtendedProperty) ,

                new ImageEnumeration(ScopeType.Model,                      Resources.Icon_SoftwareDefinitionModel, Resources.SoftwareDefinitionModel) { GroupBy = false},
                new ImageEnumeration(ScopeType.ModelNameSpace,             Resources.Icon_Namespace, Resources.Namespace) { GroupBy = false},
                new ImageEnumeration(ScopeType.ModelSubjectArea,           Resources.Icon_Diagram, Resources.Diagram) { GroupBy = false},
                new ImageEnumeration(ScopeType.ModelDefinition,            Resources.Icon_RichTextBox, Resources.RichTextBox) ,
                new ImageEnumeration(ScopeType.ModelProperty,              Resources.Icon_Property, Resources.Property) ,
                new ImageEnumeration(ScopeType.ModelAttribute,             Resources.Icon_Attribute,
                    new(CommandImageType.Default, Resources.Attribute),
                    new(CommandImageType.Add, Resources.NewAttribute),
                    new(CommandImageType.Select, Resources.SelectAttribute),
                    new(CommandImageType.Delete, Resources.DeleteAttribute)) { GroupBy = false},
                new ImageEnumeration(ScopeType.ModelAttributeAlias,        Resources.Icon_Synonym,
                    new(CommandImageType.Default, Resources.Synonym),
                    new(CommandImageType.Select, Resources.SelectSynonym),
                    new(CommandImageType.Add, Resources.NewSynonym)),
                new ImageEnumeration(ScopeType.ModelAttributeProperty,     Resources.Icon_Property, Resources.Property) ,
                new ImageEnumeration(ScopeType.ModelAttributeDefinition,   Resources.Icon_RichTextBox, Resources.RichTextBox) ,
                new ImageEnumeration(ScopeType.ModelEntity,                Resources.Icon_Entities,
                    new(CommandImageType.Default, Resources.Entity),
                    new(CommandImageType.Add, Resources.NewEntity),
                    new(CommandImageType.Select, Resources.SelectEntity),
                    new(CommandImageType.Delete, Resources.DeleteEntity)) { GroupBy = false},
                new ImageEnumeration(ScopeType.ModelEntityAlias,           Resources.Icon_Synonym,
                    new(CommandImageType.Default, Resources.Synonym),
                    new(CommandImageType.Select, Resources.SelectSynonym),
                    new(CommandImageType.Add, Resources.NewSynonym)),
                new ImageEnumeration(ScopeType.ModelEntityProperty,        Resources.Icon_Property, Resources.Property) ,
                new ImageEnumeration(ScopeType.ModelEntityAttribute,       Resources.Icon_Attribute, Resources.Attribute) ,
                new ImageEnumeration(ScopeType.ModelEntityDefinition,      Resources.Icon_RichTextBox, Resources.RichTextBox) ,
                new ImageEnumeration(ScopeType.Scripting,                  Resources.Icon_XmlFile, Resources.XmlFile) ,
                new ImageEnumeration(ScopeType.ScriptingTemplate,          Resources.Icon_XSLTransform, Resources.XSLTransform) ,
                new ImageEnumeration(ScopeType.ScriptingTemplateNode,      Resources.Icon_XMLSchema, Resources.XMLSchema) ,
                new ImageEnumeration(ScopeType.ScriptingTemplateAttribute, Resources.Icon_XMLElement, Resources.XMLElement) ,
                new ImageEnumeration(ScopeType.ScriptingTemplatePath,      Resources.Icon_XPath,
                    new(CommandImageType.Default, Resources.XPath),
                    new(CommandImageType.Select, Resources.SelectXPath),
                    new(CommandImageType.Add, Resources.NewXPath)),
                //Resources.XPath) ,
                new ImageEnumeration(ScopeType.ScriptingTemplateDocument,  Resources.Icon_XSLTransform, Resources.XmlFile) ,
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

            foreach (ImageEnumeration item in Members.Values)
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
