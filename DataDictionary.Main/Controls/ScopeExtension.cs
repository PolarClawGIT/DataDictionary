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
        static Dictionary<ScopeType, (Image image,Icon icon)> images = new Dictionary<ScopeType, (Image,Icon)>()
        {
            {ScopeType.Null,                             (Resources.UnknownMember, Resources.Icon_UnknownMember) },
            {ScopeType.Library,                          (Resources.Library, Resources.Icon_Library) },
            {ScopeType.LibraryTypeEvent,                 (Resources.Event, Resources.Icon_UnknownMember) },
            {ScopeType.LibraryTypeField,                 (Resources.Field, Resources.Icon_Field) },
            {ScopeType.LibraryTypeMethod,                (Resources.Method, Resources.Icon_Method) },
            {ScopeType.LibraryNameSpace,                 (Resources.Namespace, Resources.Icon_Namespace) },
            {ScopeType.LibraryTypeProperty,              (Resources.Property, Resources.Icon_Property) },
            {ScopeType.LibraryMethodParameter,           (Resources.Parameter, Resources.Icon_Parameter) },
            {ScopeType.LibraryType,                      (Resources.Class, Resources.Icon_Class) },

            {ScopeType.Database,                         (Resources.Database, Resources.Icon_Database) },
            {ScopeType.DatabaseSchema,                   (Resources.Schema, Resources.Icon_Schema) },
            {ScopeType.DatabaseFunction,                 (Resources.ScalarFunction, Resources.Icon_ScalarFunction) },
            {ScopeType.DatabaseProcedure,                (Resources.Procedure, Resources.Icon_Procedure) },
            {ScopeType.DatabaseTable,                    (Resources.Table, Resources.Icon_Table) },
            {ScopeType.DatabaseDomain,                   (Resources.DomainType, Resources.Icon_DomainType) },
            {ScopeType.DatabaseView,                     (Resources.View, Resources.Icon_View) },
            {ScopeType.DatabaseViewColumn,               (Resources.Column, Resources.Icon_Column) },
            {ScopeType.DatabaseTableColumn,              (Resources.Column, Resources.Icon_Column) },
            {ScopeType.DatabaseTableConstraint,          (Resources.Key, Resources.Icon_Key)},
            {ScopeType.DatabaseProcedureParameter,       (Resources.Parameter, Resources.Icon_Parameter) },
            {ScopeType.DatabaseFunctionParameter,        (Resources.Parameter, Resources.Icon_Parameter) },

            {ScopeType.Model,                            (Resources.SoftwareDefinitionModel, Resources.Icon_SoftwareDefinitionModel) },
            {ScopeType.ModelNameSpace,                   (Resources.Namespace, Resources.Icon_Namespace) },
            {ScopeType.ModelSubjectArea,                 (Resources.Diagram, Resources.Icon_Diagram) },

            {ScopeType.ModelAttribute,                   (Resources.Attribute, Resources.Icon_Attribute) },
            {ScopeType.ModelAttributeAlias,              (Resources.Synonym, Resources.Icon_Attribute) },
            {ScopeType.ModelAttributeProperty,           (Resources.Property, Resources.Icon_Attribute) },

            {ScopeType.ModelEntity,                      (Resources.Entity, Resources.Icon_Entities) },
            {ScopeType.ModelEntityAlias,                 (Resources.Synonym, Resources.Icon_Entities) },
            {ScopeType.ModelEntityProperty,              (Resources.Property, Resources.Icon_Entities) },
            {ScopeType.ModelEntityAttribute,             (Resources.Attribute, Resources.Icon_Attribute) },

            {ScopeType.Scripting,                        (Resources.XmlFile, Resources.Icon_XmlFile) },
            {ScopeType.ScriptingSchema,                  (Resources.XMLSchema, Resources.Icon_XMLSchema) },
            {ScopeType.ScriptingSchemaElement,           (Resources.XMLElement, Resources.Icon_XMLElement) },
            {ScopeType.ScriptingTransform,               (Resources.XSLTransform, Resources.Icon_XSLTransform) },
            //TODO: Need better image/icon for XML Data Selection
            {ScopeType.ScriptingSelection,               (Resources.XMLSchema, Resources.Icon_XMLSchema) },
            {ScopeType.ScriptingSelectionInstance,       (Resources.XMLElement, Resources.Icon_XMLElement) },
        };

        public static Image ToImage(this ScopeType scope)
        {
            if (images.ContainsKey(scope))
            { return images[scope].image; }
            else
            { return images[ScopeType.Null].image; }
        }

        public static Icon ToIcon(this ScopeType scope)
        {
            if (images.ContainsKey(scope))
            { return images[scope].icon; }
            else
            { return images[ScopeType.Null].icon; }
        }

        public static ImageList ToImageList()
        {
            ImageList result = new ImageList();

            foreach (KeyValuePair<ScopeType, (Image image, Icon icon)> item in images)
            { result.Images.Add(item.Key.ToName(), item.Value.image); }

            return result;
        }
    }
}
