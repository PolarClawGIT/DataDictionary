namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// List of Scope Types that are supported by the application.
    /// A Scope is used to define a NameSpace and what type of object that NameSpace represents.
    /// Database NameSpaces are represented by the fully qualified object name.
    /// Library NameSpaces are defined by the namespace or type and the element of the namespace or type.
    /// </summary>
    public enum ScopeType
    {
        /// <summary>
        /// Represents the undefined Scope.
        /// </summary>
        Null,

        /// <summary>
        /// Overall Application
        /// </summary>
        Application,

        /// <summary>
        /// Application Help
        /// </summary>
        ApplicationHelp,

        /// <summary>
        /// Application Help Page
        /// </summary>
        ApplicationHelpPage,

        /// <summary>
        /// Application Help Page of a Form
        /// </summary>
        ApplicationHelpForm,

        /// <summary>
        /// Application Help Group
        /// </summary>
        ApplicationHelpGroup,

        /// <summary>
        /// Application Options
        /// </summary>
        ApplicationOption,

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
        /// Subject Areas of the Attribute
        /// </summary>
        ModelAttributeSubjectArea,

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
        /// Subject Areas of the Entity
        /// </summary>
        ModelEntitySubjectArea,

        /// <summary>
        /// Application Model Attribute of an Entity
        /// </summary>
        ModelEntityAttribute,

        /// <summary>
        /// Application Model Process
        /// </summary>
        ModelProcess,

        /// <summary>
        /// Application Model Process Alias
        /// </summary>
        ModelProcessAlias,

        /// <summary>
        /// Application Model Process Property
        /// </summary>
        ModelProcessProperty,

        /// <summary>
        /// Application Model Process Definition
        /// </summary>
        ModelProcessDefinition,

        /// <summary>
        /// Subject Areas of the Process
        /// </summary>
        ModelProcessSubjectArea,

        /// <summary>
        /// Application Model Argument of an Process
        /// </summary>
        ModelProcessArgument,

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
        /// .Net Library Method/Property Parameter
        /// </summary>
        LibraryTypeParameter,

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
        DatabaseConstraint,

        /// <summary>
        /// SQL Table Constraint Column
        /// </summary>
        DatabaseConstraintColumn,

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
        /// SQL Table Value Function Column
        /// </summary>
        DatabaseFunctionColumn,

        /// <summary>
        /// SQL Reference between Objects
        /// </summary>
        DatabaseReference,

        /// <summary>
        /// SQL Database Extended Properties
        /// </summary>
        DatabaseProperty,

        /// <summary>
        /// Security Objects
        /// </summary>
        Security,

        /// <summary>
        /// Security Principal
        /// </summary>
        SecurityPrincipal,

        /// <summary>
        /// Security Role
        /// </summary>
        SecurityRole,

        /// <summary>
        /// Security Securable (Security Object)
        /// </summary>
        SecuritySecurable,

        /// <summary>
        /// Scripting Engine
        /// </summary>
        Scripting,

        /// <summary>
        /// Scripting Template
        /// </summary>
        ScriptingTemplate,

        /// <summary>
        /// Scripting Template Path
        /// </summary>
        ScriptingTemplatePath,

        /// <summary>
        /// Scripting Template Node
        /// </summary>
        ScriptingTemplateNode,

        /// <summary>
        /// Scripting Template Node Attribute
        /// </summary>
        ScriptingTemplateAttribute,

        /// <summary>
        /// Scripting Template Document
        /// </summary>
        ScriptingTemplateDocument,

    }

}
