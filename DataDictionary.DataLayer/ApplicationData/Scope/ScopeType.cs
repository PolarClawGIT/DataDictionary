using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.DataLayer.LibraryData;
using DataDictionary.DataLayer.LibraryData.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData.Scope
{
    /// <summary>
    /// List of Scope Types that are supported by the application.
    /// A Scope is used to define a NameSpace and what type of object that NameSpace represents.
    /// Database NameSpaces are represented by the fully qualified object name.
    /// Library NameSpaces are defined by the namespace or type and the element of the namespace or type.
    /// </summary>
    /// <see cref="ScopeKey"/>
    public enum ScopeType
    {
        /// <summary>
        /// Represents the undefined Scope.
        /// </summary>
        Null,

        /// <summary>
        /// Application Model
        /// </summary>
        Model,

        /// <summary>
        /// Application Model Subject Area
        /// </summary>
        ModelSubjectArea,

        /// <summary>
        /// Application Model Attribute
        /// </summary>
        ModelAttribute,

        /// <summary>
        /// Application Model Attribute Alias
        /// </summary>
        ModelAttributeAlias,

        /// <summary>
        /// Application Model Attribute Property
        /// </summary>
        ModelAttributeProperty,

        /// <summary>
        /// Application Model Attribute Definition
        /// </summary>
        ModelAttributeDefinition,

        /// <summary>
        /// Application Model Entity
        /// </summary>
        ModelEntity,

        /// <summary>
        /// Application Model Entity Alias
        /// </summary>
        ModelEntityAlias,

        /// <summary>
        /// Application Model Entity Property
        /// </summary>
        ModelEntityProperty,

        /// <summary>
        /// Application Model Entity Definition
        /// </summary>
        ModelEntityDefinition,

        /// <summary>
        /// Application Model Attribute of an Entity
        /// </summary>
        ModelEntityAttribute,

        /// <summary>
        /// NameSpace item for the Model
        /// </summary>
        ModelNameSpace,

        /// <summary>
        /// Property for the Model
        /// </summary>
        ModelProperty,

        /// <summary>
        /// Definition for the Model
        /// </summary>
        ModelDefinition,

        /// <summary>
        /// .Net Library
        /// </summary>
        Library,

        /// <summary>
        /// .Net Library NameSpace
        /// </summary>
        /// <remarks>NameSpace cannot be detected directly. Instead it must be inferred.</remarks>
        LibraryNameSpace,

        /// <summary>
        /// .Net Library Type (Class, Enum, Delegates, ...)
        /// </summary>
        /// <remarks>The exact type cannot be determined using the Document file.</remarks>
        LibraryType,

        /// <summary>
        /// .Net Library Event
        /// </summary>
        LibraryTypeEvent,

        /// <summary>
        /// .Net Library Field
        /// </summary>
        LibraryTypeField,

        /// <summary>
        /// .Net Library Method
        /// </summary>
        LibraryTypeMethod,

        /// <summary>
        /// .Net Library Property
        /// </summary>
        LibraryTypeProperty,

        /// <summary>
        /// .Net Library Method Parameter
        /// </summary>
        LibraryMethodParameter,

        /// <summary>
        /// .Net Library Property Parameter, Indexer
        /// </summary>
        LibraryPropertyParameter,

        /// <summary>
        /// SQL Database
        /// </summary>
        Database,

        /// <summary>
        /// SQL Schema
        /// </summary>
        DatabaseSchema,

        /// <summary>
        /// SQL Table
        /// </summary>
        DatabaseTable,

        /// <summary>
        /// SQL Function
        /// </summary>
        DatabaseFunction,

        /// <summary>
        /// SQL Procedure
        /// </summary>
        DatabaseProcedure,

        /// <summary>
        /// SQL Type/Domain
        /// </summary>
        DatabaseDomain,

        /// <summary>
        /// SQL View
        /// </summary>
        DatabaseView,

        /// <summary>
        /// SQL View Column
        /// </summary>
        DatabaseViewColumn,

        //DatabaseSchemaViewIndex,

        /// <summary>
        /// SQL Table Column
        /// </summary>
        DatabaseTableColumn,

        /// <summary>
        /// SQL Table Constraint
        /// </summary>
        DatabaseTableConstraint,

        //DatabaseSchemaTableIndex,

        /// <summary>
        /// SQL Procedure Parameter
        /// </summary>
        DatabaseProcedureParameter,

        /// <summary>
        /// SQL Function Parameter
        /// </summary>
        DatabaseFunctionParameter,

        /// <summary>
        /// Scripting Engine
        /// </summary>
        Scripting,

        /// <summary>
        /// Scripting Schema
        /// </summary>
        ScriptingSchema,

        /// <summary>
        /// Scripting Schema Element
        /// </summary>
        ScriptingSchemaElement,

        /// <summary>
        /// Scripting Transform
        /// </summary>
        ScriptingTransform,

        /// <summary>
        /// Scripting Selection
        /// </summary>
        ScriptingSelection,

        /// <summary>
        /// Scripting Selection Path
        /// </summary>
        ScriptingSelectionPath,
    }

    /// <summary>
    /// Support Extension for the Scope Type
    /// </summary>
    public static class ScopeTypeExtension
    {
        /// <summary>
        /// Translates the ScopeType to a ScopeName (String).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String ToName(this ScopeType value)
        { return new ScopeKey(value).ToString(); }
    }
}
