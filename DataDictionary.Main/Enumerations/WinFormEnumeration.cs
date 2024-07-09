using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Enumerations
{
    /// <summary>
    /// Images used for Scope ImageList
    /// </summary>
    enum ScopeImage
    {
        Normal,
        New,
        Delete,
        Save,
        Open
    }

    /// <summary>
    /// ScopeEnumeration with Images and Icons
    /// </summary>
    class WinFormEnumeration : Enumeration<ScopeType, WinFormEnumeration>
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

        Dictionary<ScopeImage, Image> images { get; init; } = new Dictionary<ScopeImage, Image>();
        static readonly Image defaultImage = Resources.UnknownMember;

        /// <summary>
        /// List of Images for the Scope Type
        /// </summary>
        public IReadOnlyDictionary<ScopeImage, Image> Images
        {
            get
            {
                Dictionary<ScopeImage, Image> result = new Dictionary<ScopeImage, Image>();

                foreach (ScopeImage item in Enum.GetValues(typeof(ScopeImage)))
                {
                    if (images.TryGetValue(item, out Image? value))
                    { result.Add(item, value); }
                    else { result.Add(item, defaultImage); }
                }
                return result;
            }
        }

        /// <summary>
        /// Grouping behavior.
        /// When True, items with the same ScopeType will be group together.
        /// When False, items will not be group together and appear as individual entries.
        /// This effects navigation components.
        /// </summary>
        public Boolean GroupBy { get; init; } = false;

        /// <summary>
        /// Constructor for the Window Form Scope Enumeration.
        /// </summary>
        /// <param name="scope"></param>
        WinFormEnumeration(ScopeType scope) : base()
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
        WinFormEnumeration(ScopeType scope, Icon windowIcon) : this(scope)
        { this.WindowIcon = windowIcon; }

        /// <summary>
        /// Constructor for the Window Form Scope Enumeration.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="windowIcon"></param>
        /// <param name="defaultImage"></param>
        WinFormEnumeration(ScopeType scope, Icon windowIcon, Image defaultImage) : this(scope, windowIcon)
        { this.images = new Dictionary<ScopeImage, Image>() { { ScopeImage.Normal, defaultImage } }; }

        /// <summary>
        /// Constructor for the Window Form Scope Enumeration.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="windowIcon"></param>
        /// <param name="images"></param>
        WinFormEnumeration(ScopeType scope, Icon windowIcon, params (ScopeImage scope, Image image)[] images) : this(scope, windowIcon)
        {
            this.images = new Dictionary<ScopeImage, Image>();
            foreach ((ScopeImage scope, Image image) item in images)
            {
                this.images.Add(item.scope, item.image);
            }
        }

        /// <summary>
        /// Constructor for the Window Form Scope Enumeration static data.
        /// </summary>
        static WinFormEnumeration()
        {
            List<WinFormEnumeration> data = new List<WinFormEnumeration>()
            {
                new WinFormEnumeration(ScopeType.Null,                       Resources.Icon_UnknownMember, Resources.UnknownMember),
                new WinFormEnumeration(ScopeType.Library,                    Resources.Icon_Library,
                    new(ScopeImage.Normal, Resources.Library),
                    new(ScopeImage.New, Resources.NewLibrary),
                    new(ScopeImage.Delete, Resources.DeleteLibrary)) {GroupBy = false},
                new WinFormEnumeration(ScopeType.LibraryTypeEvent,           Resources.Icon_Event, Resources.Event) {GroupBy = false},
                new WinFormEnumeration(ScopeType.LibraryTypeField,           Resources.Icon_Field, Resources.Field) {GroupBy = false},
                new WinFormEnumeration(ScopeType.LibraryTypeMethod,          Resources.Icon_Method, Resources.Method) {GroupBy = false},
                new WinFormEnumeration(ScopeType.LibraryNameSpace,           Resources.Icon_Namespace, Resources.Namespace) {GroupBy = false},
                new WinFormEnumeration(ScopeType.LibraryTypeProperty,        Resources.Icon_Property, Resources.Property) {GroupBy = false},
                new WinFormEnumeration(ScopeType.LibraryMethodParameter,     Resources.Icon_Parameter, Resources.Parameter) { GroupBy = false},
                new WinFormEnumeration(ScopeType.LibraryPropertyParameter,   Resources.Icon_Parameter, Resources.Parameter) { GroupBy = false},
                new WinFormEnumeration(ScopeType.LibraryType,                Resources.Icon_Class, Resources.Class) {GroupBy = false},
                new WinFormEnumeration(ScopeType.Database,                   Resources.Icon_Database, Resources.Database) {GroupBy = false} ,
                new WinFormEnumeration(ScopeType.DatabaseSchema,             Resources.Icon_Schema, Resources.Schema) {GroupBy = false} ,
                new WinFormEnumeration(ScopeType.DatabaseFunction,           Resources.Icon_ScalarFunction, Resources.ScalarFunction) ,
                new WinFormEnumeration(ScopeType.DatabaseProcedure,          Resources.Icon_Procedure, Resources.Procedure) ,
                new WinFormEnumeration(ScopeType.DatabaseTable,              Resources.Icon_Table, Resources.Table) ,
                new WinFormEnumeration(ScopeType.DatabaseDomain,             Resources.Icon_DomainType, Resources.DomainType) ,
                new WinFormEnumeration(ScopeType.DatabaseView,               Resources.Icon_View, Resources.View) ,
                new WinFormEnumeration(ScopeType.DatabaseViewColumn,         Resources.Icon_Column, Resources.Column) ,
                new WinFormEnumeration(ScopeType.DatabaseTableColumn,        Resources.Icon_Column, Resources.Column) ,
                new WinFormEnumeration(ScopeType.DatabaseTableConstraint,    Resources.Icon_Key, Resources.Key) ,
                new WinFormEnumeration(ScopeType.DatabaseProcedureParameter, Resources.Icon_Parameter, Resources.Parameter) ,
                new WinFormEnumeration(ScopeType.DatabaseFunctionParameter,  Resources.Icon_Parameter, Resources.Parameter) ,
                new WinFormEnumeration(ScopeType.Model,                      Resources.Icon_SoftwareDefinitionModel, Resources.SoftwareDefinitionModel) { GroupBy = false},
                new WinFormEnumeration(ScopeType.ModelNameSpace,             Resources.Icon_Namespace, Resources.Namespace) { GroupBy = false},
                new WinFormEnumeration(ScopeType.ModelSubjectArea,           Resources.Icon_Diagram, Resources.Diagram) { GroupBy = false},
                new WinFormEnumeration(ScopeType.ModelDefinition,            Resources.Icon_RichTextBox, Resources.RichTextBox) ,
                new WinFormEnumeration(ScopeType.ModelProperty,              Resources.Icon_Property, Resources.Property) ,
                new WinFormEnumeration(ScopeType.ModelAttribute,             Resources.Icon_Attribute, Resources.Attribute) ,
                new WinFormEnumeration(ScopeType.ModelAttributeAlias,        Resources.Icon_Synonym, Resources.Synonym) ,
                new WinFormEnumeration(ScopeType.ModelAttributeProperty,     Resources.Icon_Property, Resources.Property) ,
                new WinFormEnumeration(ScopeType.ModelAttributeDefinition,   Resources.Icon_RichTextBox, Resources.RichTextBox) ,
                new WinFormEnumeration(ScopeType.ModelEntity,                Resources.Icon_Entities, Resources.Entity) ,
                new WinFormEnumeration(ScopeType.ModelEntityAlias,           Resources.Icon_Synonym, Resources.Synonym) ,
                new WinFormEnumeration(ScopeType.ModelEntityProperty,        Resources.Icon_Property, Resources.Property) ,
                new WinFormEnumeration(ScopeType.ModelEntityAttribute,       Resources.Icon_Attribute, Resources.Attribute) ,
                new WinFormEnumeration(ScopeType.ModelEntityDefinition,      Resources.Icon_RichTextBox, Resources.RichTextBox) ,
                new WinFormEnumeration(ScopeType.Scripting,                  Resources.Icon_XmlFile, Resources.XmlFile) ,
                new WinFormEnumeration(ScopeType.ScriptingTemplate,          Resources.Icon_XSLTransform, Resources.XSLTransform) ,
                new WinFormEnumeration(ScopeType.ScriptingTemplateNode,      Resources.Icon_XMLSchema, Resources.XMLSchema) ,
                new WinFormEnumeration(ScopeType.ScriptingTemplateAttribute, Resources.Icon_XMLElement, Resources.XMLElement) ,
                new WinFormEnumeration(ScopeType.ScriptingTemplatePath,      Resources.Icon_XPath, Resources.XPath) ,
                new WinFormEnumeration(ScopeType.ScriptingTemplateDocument,  Resources.Icon_XSLTransform, Resources.XmlFile) ,
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

            foreach (WinFormEnumeration item in Values.Values)
            { result.Images.Add(item.Name, item.Images[ScopeImage.Normal]); }

            return result;
        }

        /// <summary>
        /// Given the Scope, return the Icon object for it or the default Icon.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public static Icon GetIcon(ScopeType scope)
        {
            if (Values.ContainsKey(scope))
            { return Values[scope].WindowIcon; }
            else { return defaultIcon; }
        }

        /// <summary>
        /// Given the Scope and Image, return the Image object or default Image.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Image GetImage(ScopeType scope, ScopeImage image)
        {
            if (Values.ContainsKey(scope) && Values[scope].Images.ContainsKey(image))
            { return Values[scope].Images[image]; }
            else if(Values.ContainsKey(scope) && Values[scope].Images.ContainsKey(ScopeImage.Normal))
            { return Values[scope].Images[ScopeImage.Normal]; }
            else { return defaultImage; }
        }
    }
}
