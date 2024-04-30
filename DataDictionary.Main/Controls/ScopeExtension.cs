using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Controls
{
    static class ScopeExtension
    {
        /// <summary>
        /// This associates a Image to a Model Scope. ScopeType must be defined. ScopeKey defines the text.
        /// </summary>
        /// <see cref="DataDictionary.DataLayer.ApplicationData.Scope.ScopeType"/>
        /// <see cref="DataDictionary.DataLayer.ApplicationData.Scope.ScopeKey"/>
        static Dictionary<ScopeType, ScopeSetting> images = new Dictionary<ScopeType, ScopeSetting>()
        {
            {ScopeType.Null,                             new ScopeSetting(Resources.UnknownMember, Resources.Icon_UnknownMember) },

            {ScopeType.Library,                          new ScopeSetting(Resources.Library, Resources.Icon_Library) {GroupByScope = false} },
            {ScopeType.LibraryTypeEvent,                 new ScopeSetting(Resources.Event, Resources.Icon_UnknownMember) {GroupByScope = false}},
            {ScopeType.LibraryTypeField,                 new ScopeSetting(Resources.Field, Resources.Icon_Field) {GroupByScope = false}},
            {ScopeType.LibraryTypeMethod,                new ScopeSetting(Resources.Method, Resources.Icon_Method) {GroupByScope = false}},
            {ScopeType.LibraryNameSpace,                 new ScopeSetting(Resources.Namespace, Resources.Icon_Namespace) {GroupByScope = false}},
            {ScopeType.LibraryTypeProperty,              new ScopeSetting(Resources.Property, Resources.Icon_Property) {GroupByScope = false}},
            {ScopeType.LibraryMethodParameter,           new ScopeSetting(Resources.Parameter, Resources.Icon_Parameter) { GroupByScope = false}},
            {ScopeType.LibraryType,                      new ScopeSetting(Resources.Class, Resources.Icon_Class) {GroupByScope = false}},

            {ScopeType.Database,                         new ScopeSetting(Resources.Database, Resources.Icon_Database) {GroupByScope = false} },
            {ScopeType.DatabaseSchema,                   new ScopeSetting(Resources.Schema, Resources.Icon_Schema) {GroupByScope = false} },
            {ScopeType.DatabaseFunction,                 new ScopeSetting(Resources.ScalarFunction, Resources.Icon_ScalarFunction) },
            {ScopeType.DatabaseProcedure,                new ScopeSetting(Resources.Procedure, Resources.Icon_Procedure) },
            {ScopeType.DatabaseTable,                    new ScopeSetting(Resources.Table, Resources.Icon_Table) },
            {ScopeType.DatabaseDomain,                   new ScopeSetting(Resources.DomainType, Resources.Icon_DomainType) },
            {ScopeType.DatabaseView,                     new ScopeSetting(Resources.View, Resources.Icon_View) },
            {ScopeType.DatabaseViewColumn,               new ScopeSetting(Resources.Column, Resources.Icon_Column) },
            {ScopeType.DatabaseTableColumn,              new ScopeSetting(Resources.Column, Resources.Icon_Column) },
            {ScopeType.DatabaseTableConstraint,          new ScopeSetting(Resources.Key, Resources.Icon_Key)},
            {ScopeType.DatabaseProcedureParameter,       new ScopeSetting(Resources.Parameter, Resources.Icon_Parameter) },
            {ScopeType.DatabaseFunctionParameter,        new ScopeSetting(Resources.Parameter, Resources.Icon_Parameter) },

            {ScopeType.Model,                            new ScopeSetting(Resources.SoftwareDefinitionModel, Resources.Icon_SoftwareDefinitionModel) { GroupByScope = false}},
            {ScopeType.ModelNameSpace,                   new ScopeSetting(Resources.Namespace, Resources.Icon_Namespace) },
            {ScopeType.ModelSubjectArea,                 new ScopeSetting(Resources.Diagram, Resources.Icon_Diagram) },

            {ScopeType.ModelAttribute,                   new ScopeSetting(Resources.Attribute, Resources.Icon_Attribute) },
            {ScopeType.ModelAttributeAlias,              new ScopeSetting(Resources.Synonym, Resources.Icon_Attribute) },
            {ScopeType.ModelAttributeProperty,           new ScopeSetting(Resources.Property, Resources.Icon_Attribute) },

            {ScopeType.ModelEntity,                      new ScopeSetting(Resources.Entity, Resources.Icon_Entities) },
            {ScopeType.ModelEntityAlias,                 new ScopeSetting(Resources.Synonym, Resources.Icon_Entities) },
            {ScopeType.ModelEntityProperty,              new ScopeSetting(Resources.Property, Resources.Icon_Entities) },
            {ScopeType.ModelEntityAttribute,             new ScopeSetting(Resources.Attribute, Resources.Icon_Attribute) },

            {ScopeType.Scripting,                        new ScopeSetting(Resources.XmlFile, Resources.Icon_XmlFile) },
            {ScopeType.ScriptingSchema,                  new ScopeSetting(Resources.XMLSchema, Resources.Icon_XMLSchema) },
            {ScopeType.ScriptingSchemaElement,           new ScopeSetting(Resources.XMLElement, Resources.Icon_XMLElement) },
            {ScopeType.ScriptingTransform,               new ScopeSetting(Resources.XSLTransform, Resources.Icon_XSLTransform) },
            //TODO: Need better image/icon for XML Data Selection
            {ScopeType.ScriptingSelection,               new ScopeSetting(Resources.XMLSchema, Resources.Icon_XMLSchema) },
            {ScopeType.ScriptingSelectionInstance,       new ScopeSetting(Resources.XMLElement, Resources.Icon_XMLElement) },
        };

        public static Image ToImage(this ScopeType scope)
        {
            if (images.ContainsKey(scope))
            { return images[scope].NavigationImage; }
            else
            { return images[ScopeType.Null].NavigationImage; }
        }

        public static ScopeSetting Setting(this ScopeType scope)
        {
            if (images.ContainsKey(scope))
            { return images[scope]; }
            else
            { return images[ScopeType.Null]; }
        }

        public static Icon ToIcon(this ScopeType scope)
        {
            if (images.ContainsKey(scope))
            { return images[scope].WindowIcon; }
            else
            { return images[ScopeType.Null].WindowIcon; }
        }

        public static ImageList ToImageList()
        {
            ImageList result = new ImageList();

            foreach (KeyValuePair<ScopeType, ScopeSetting> item in images)
            { result.Images.Add(item.Key.ToName(), item.Value.NavigationImage); }

            return result;
        }
    }

    /// <summary>
    /// Represents the setting (icon, images, ...) for a Scope 
    /// </summary>
    struct ScopeSetting
    {
        public Image NavigationImage { get; init; }
        public Icon WindowIcon { get; init; }
        public Boolean GroupByScope { get; init; } = true;

        public ScopeSetting(Image image, Icon icon)
        {
            NavigationImage = image;
            WindowIcon = icon;
        }
    }
}
