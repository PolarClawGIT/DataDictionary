using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.Main.Enumerations
{
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
    class ScopeWinFormEnumeration : ScopeEnumeration, IEnumeration<ScopeType, ScopeWinFormEnumeration>
    {
        // This class could not be placed in base ScopeEnumeration because framework agnostic.
        // This version is Windows WinForms specific.
        // The System.Drawing.Icon and System.Drawing.Image does not exist in all frameworks.

        /// <summary>
        /// Icon used for the ScopeType
        /// </summary>
        public Icon WindowIcon { get; init; } = Resources.Icon_UnknownMember;

        Dictionary<ScopeImage, Image> images { get; init; } = new Dictionary<ScopeImage, Image>();
        Image nullImage = Resources.UnknownMember;

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
                    else { result.Add(item, nullImage); }
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

        static readonly List<ScopeWinFormEnumeration> windowValues = new List<ScopeWinFormEnumeration>()
        {
            new ScopeWinFormEnumeration(ScopeType.Null,                       Resources.Icon_UnknownMember, Resources.UnknownMember),
            new ScopeWinFormEnumeration(ScopeType.Library,                    Resources.Icon_Library, Resources.Library) {GroupBy = false},
            new ScopeWinFormEnumeration(ScopeType.LibraryTypeEvent,           Resources.Icon_Event, Resources.Event) {GroupBy = false},
            new ScopeWinFormEnumeration(ScopeType.LibraryTypeField,           Resources.Icon_Field, Resources.Field) {GroupBy = false},
            new ScopeWinFormEnumeration(ScopeType.LibraryTypeMethod,          Resources.Icon_Method, Resources.Method) {GroupBy = false},
            new ScopeWinFormEnumeration(ScopeType.LibraryNameSpace,           Resources.Icon_Namespace, Resources.Namespace) {GroupBy = false},
            new ScopeWinFormEnumeration(ScopeType.LibraryTypeProperty,        Resources.Icon_Property, Resources.Property) {GroupBy = false},
            new ScopeWinFormEnumeration(ScopeType.LibraryMethodParameter,     Resources.Icon_Parameter, Resources.Parameter) { GroupBy = false},
            new ScopeWinFormEnumeration(ScopeType.LibraryType,                Resources.Icon_Class, Resources.Class) {GroupBy = false},
            new ScopeWinFormEnumeration(ScopeType.Database,                   Resources.Icon_Database, Resources.Database) {GroupBy = false} ,
            new ScopeWinFormEnumeration(ScopeType.DatabaseSchema,             Resources.Icon_Schema, Resources.Schema) {GroupBy = false} ,
            new ScopeWinFormEnumeration(ScopeType.DatabaseFunction,           Resources.Icon_ScalarFunction, Resources.ScalarFunction) ,
            new ScopeWinFormEnumeration(ScopeType.DatabaseProcedure,          Resources.Icon_Procedure, Resources.Procedure) ,
            new ScopeWinFormEnumeration(ScopeType.DatabaseTable,              Resources.Icon_Table, Resources.Table) ,
            new ScopeWinFormEnumeration(ScopeType.DatabaseDomain,             Resources.Icon_DomainType, Resources.DomainType) ,
            new ScopeWinFormEnumeration(ScopeType.DatabaseView,               Resources.Icon_View, Resources.View) ,
            new ScopeWinFormEnumeration(ScopeType.DatabaseViewColumn,         Resources.Icon_Column, Resources.Column) ,
            new ScopeWinFormEnumeration(ScopeType.DatabaseTableColumn,        Resources.Icon_Column, Resources.Column) ,
            new ScopeWinFormEnumeration(ScopeType.DatabaseTableConstraint,    Resources.Icon_Key, Resources.Key) ,
            new ScopeWinFormEnumeration(ScopeType.DatabaseProcedureParameter, Resources.Icon_Parameter, Resources.Parameter) ,
            new ScopeWinFormEnumeration(ScopeType.DatabaseFunctionParameter,  Resources.Icon_Parameter, Resources.Parameter) ,
            new ScopeWinFormEnumeration(ScopeType.Model,                      Resources.Icon_SoftwareDefinitionModel, Resources.SoftwareDefinitionModel) { GroupBy = false},
            new ScopeWinFormEnumeration(ScopeType.ModelNameSpace,             Resources.Icon_Namespace, Resources.Namespace) { GroupBy = false},
            new ScopeWinFormEnumeration(ScopeType.ModelSubjectArea,           Resources.Icon_Diagram, Resources.Diagram) { GroupBy = false},
            new ScopeWinFormEnumeration(ScopeType.ModelDefinition,            Resources.Icon_RichTextBox, Resources.RichTextBox) ,
            new ScopeWinFormEnumeration(ScopeType.ModelProperty,              Resources.Icon_Property, Resources.Property) ,
            new ScopeWinFormEnumeration(ScopeType.ModelAttribute,             Resources.Icon_Attribute, Resources.Attribute) ,
            new ScopeWinFormEnumeration(ScopeType.ModelAttributeAlias,        Resources.Icon_Synonym, Resources.Synonym) ,
            new ScopeWinFormEnumeration(ScopeType.ModelAttributeProperty,     Resources.Icon_Property, Resources.Property) ,
            new ScopeWinFormEnumeration(ScopeType.ModelAttributeDefinition,   Resources.Icon_RichTextBox, Resources.RichTextBox) ,
            new ScopeWinFormEnumeration(ScopeType.ModelEntity,                Resources.Icon_Entities, Resources.Entity) ,
            new ScopeWinFormEnumeration(ScopeType.ModelEntityAlias,           Resources.Icon_Synonym, Resources.Synonym) ,
            new ScopeWinFormEnumeration(ScopeType.ModelEntityProperty,        Resources.Icon_Property, Resources.Property) ,
            new ScopeWinFormEnumeration(ScopeType.ModelEntityAttribute,       Resources.Icon_Attribute, Resources.Attribute) ,
            new ScopeWinFormEnumeration(ScopeType.ModelEntityDefinition,      Resources.Icon_RichTextBox, Resources.RichTextBox) ,
            new ScopeWinFormEnumeration(ScopeType.Scripting,                  Resources.Icon_XmlFile, Resources.XmlFile) ,
            new ScopeWinFormEnumeration(ScopeType.ScriptingSchema,            Resources.Icon_XMLSchema, Resources.XMLSchema) ,
            new ScopeWinFormEnumeration(ScopeType.ScriptingSchemaElement,     Resources.Icon_XMLElement, Resources.XMLElement) ,
            new ScopeWinFormEnumeration(ScopeType.ScriptingTransform,         Resources.Icon_XSLTransform, Resources.XSLTransform) ,
            new ScopeWinFormEnumeration(ScopeType.ScriptingSelection,         Resources.Icon_XPath, Resources.XPath) ,
            new ScopeWinFormEnumeration(ScopeType.ScriptingSelectionPath,     Resources.Icon_XPath, Resources.XPath) ,
            new ScopeWinFormEnumeration(ScopeType.ScriptingTemplate,          Resources.Icon_XSLTransform, Resources.XSLTransform) ,
            new ScopeWinFormEnumeration(ScopeType.ScriptingTemplateNode,      Resources.Icon_XMLSchema, Resources.XMLSchema) ,
            new ScopeWinFormEnumeration(ScopeType.ScriptingTemplateAttribute, Resources.Icon_XMLElement, Resources.XMLElement) ,
            new ScopeWinFormEnumeration(ScopeType.ScriptingTemplatePath,      Resources.Icon_XPath, Resources.XPath) ,
            new ScopeWinFormEnumeration(ScopeType.ScriptingTemplateDocument,  Resources.Icon_XSLTransform, Resources.XmlFile) ,
        };

        ScopeWinFormEnumeration(ScopeType scope) : base()
        {
            Resource.Enumerations.ScopeEnumeration source = Resource.Enumerations.ScopeEnumeration.Cast(scope);
            this.DisplayName = source.DisplayName;
            this.Name = source.Name;
            this.Value = source.Value;
        }

        ScopeWinFormEnumeration(ScopeType scope, Icon windowIcon) : this(scope)
        { this.WindowIcon = windowIcon; }

        ScopeWinFormEnumeration(ScopeType scope, Icon windowIcon, Image defaultImage) : this(scope, windowIcon)
        { this.images = new Dictionary<ScopeImage, Image>() { { ScopeImage.Normal, defaultImage } }; }

        ScopeWinFormEnumeration(ScopeType scope, Icon windowIcon, params (ScopeImage scope, Image image)[] images) : this(scope, windowIcon)
        {
            this.images = new Dictionary<ScopeImage, Image>();
            foreach ((ScopeImage scope, Image image) item in images)
            {
                this.images.Add(item.scope, item.image);
            }
        }

        //TODO: Need to initialize the dictionary once. Need to apply same approach to all other Enumerations.
        static Dictionary<ScopeType, ScopeWinFormEnumeration> createDictionary()
        { // Order of methods matters. Statics are created top-down.
            Dictionary<ScopeType, ScopeWinFormEnumeration> result = new Dictionary<ScopeType, ScopeWinFormEnumeration>();
            foreach (ScopeType item in Enum.GetValues<ScopeType>())
            {
                if (windowValues.FirstOrDefault(w => w.Value == item) is ScopeWinFormEnumeration value)
                { result.Add(item, value); }
                else { result.Add(item, new ScopeWinFormEnumeration(item)); }
            }
            return result;
        }

        /// <summary>
        /// Returns the Image list for all items.
        /// </summary>
        /// <returns></returns>
        public static ImageList AsImageList()
        {
            ImageList result = new ImageList();

            foreach (ScopeWinFormEnumeration item in windowValues)
            { result.Images.Add(item.Name, item.Images[ScopeImage.Normal]); }

            return result;
        }

        /// <inheritdoc />
        public static new IReadOnlyDictionary<ScopeType, ScopeWinFormEnumeration> AsDictionary { get; } = createDictionary();

        /// <inheritdoc cref="IEnumeration{TEnum, TSelf}.Cast(TEnum)" />
        public static new ScopeWinFormEnumeration Cast(ScopeType source)
        { return IEnumeration<ScopeType, ScopeWinFormEnumeration>.Cast(source); }

        static ScopeWinFormEnumeration IEnumeration<ScopeType, ScopeWinFormEnumeration>.Parse(String s, IFormatProvider? provider)
        { return IEnumeration<ScopeType, ScopeWinFormEnumeration>.Parse(s); }

        public static Boolean TryParse([NotNullWhen(true)] String? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ScopeWinFormEnumeration result)
        { return IEnumeration<ScopeType, ScopeWinFormEnumeration>.TryParse(s, out result); }

        public Boolean Equals(ScopeWinFormEnumeration? other)
        { return other is ScopeWinFormEnumeration && Value.Equals(other.Value); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is ScopeWinFormEnumeration value && Equals(other); }

        /// <inheritdoc/>
        public static Boolean operator ==(ScopeWinFormEnumeration left, ScopeWinFormEnumeration right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ScopeWinFormEnumeration left, ScopeWinFormEnumeration right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(Value); }

        public override String ToString()
        { return Name; }
    }
}
