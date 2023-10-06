﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataDictionary.DataLayer.DatabaseData {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class DbScript {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DbScript() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DataDictionary.DataLayer.DatabaseData.DbScript", typeof(DbScript).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select	Convert(UniqueIdentifier,Null) As [CatalogId],
        ///	Db_Name() As [CatalogTitle],
        ///	Convert(NvarChar,Null) As [CatalogDescription],
        ///	Db_Name() As [CatalogName],
        ///	@Server As [SourceServerName],
        ///	Db_Name() As [SourceDatabaseName],
        ///	GetDate() As [SourceDate],
        ///	Convert(DateTime2, Null) As [SysStart].
        /// </summary>
        internal static string DbCatalogItem {
            get {
                return ResourceManager.GetString("DbCatalogItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select	Convert(UniqueIdentifier,Null) As [CatalogId],
        ///	I.[TABLE_CATALOG] As [CatalogName],
        ///	I.[TABLE_SCHEMA] As [SchemaName],
        ///	I.[TABLE_NAME] As [TableName],
        ///	T.[TABLE_TYPE] As [TableType],
        ///	I.[COLUMN_NAME] As [ColumnName],
        ///	I.[ORDINAL_POSITION] As [OrdinalPosition],
        ///	I.[COLUMN_DEFAULT] As [ColumnDefault],
        ///	iif(I.[IS_NULLABLE] In (&apos;YES&apos;,&apos;TRUE&apos;,&apos;1&apos;),1,0) As [IsNullable],
        ///	I.[DATA_TYPE] As [DataType],
        ///	P.[definition] As [ComputedDefinition],
        ///	I.[CHARACTER_MAXIMUM_LENGTH] As [CharacterMaximumLength] [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DbColumnItem {
            get {
                return ResourceManager.GetString("DbColumnItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select	Convert(UniqueIdentifier,Null) As [CatalogId],
        ///	T.[CONSTRAINT_CATALOG] As [CatalogName],
        ///	T.[CONSTRAINT_SCHEMA] As [SchemaName],
        ///	T.[CONSTRAINT_NAME] As [ConstraintName],
        ///	C.[TABLE_NAME] As [TableName],
        ///	C.[COLUMN_NAME] As [ColumnName],
        ///	C.[ORDINAL_POSITION] As [OrdinalPosition],
        ///	F.[TABLE_SCHEMA] As [ReferenceSchemaName],
        ///	F.[TABLE_NAME] As [ReferenceTableName],
        ///	F.[COLUMN_NAME] As [ReferenceColumnName]
        ///From	[INFORMATION_SCHEMA].[TABLE_CONSTRAINTS] T
        ///	Inner Join [INFORMATION_SCHEMA].[KEY_ [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DbConstraintColumnItem {
            get {
                return ResourceManager.GetString("DbConstraintColumnItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select	Convert(UniqueIdentifier,Null) As [CatalogId],
        ///	[CONSTRAINT_CATALOG] As [CatalogName],
        ///	[CONSTRAINT_SCHEMA] As [SchemaName],
        ///	[CONSTRAINT_NAME] As [ConstraintName],
        ///	[TABLE_NAME] As [TableName],
        ///	[CONSTRAINT_TYPE] As [ConstraintType]
        ///From	[INFORMATION_SCHEMA].[TABLE_CONSTRAINTS].
        /// </summary>
        internal static string DbConstraintItem {
            get {
                return ResourceManager.GetString("DbConstraintItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select	Convert(UniqueIdentifier,Null) As [CatalogId],
        ///	[DOMAIN_CATALOG] As [CatalogName],
        ///	[DOMAIN_SCHEMA] As [SchemaName],
        ///	[DOMAIN_NAME] As [DomainName],
        ///	[DATA_TYPE] As [DataType],
        ///	[DOMAIN_DEFAULT] As [DomainDefault],
        ///	[CHARACTER_MAXIMUM_LENGTH] As [CharacterMaximumLength],
        ///	[CHARACTER_OCTET_LENGTH] As [CharacterOctetLength],
        ///	[NUMERIC_PRECISION] As [NumericPrecision],
        ///	[NUMERIC_PRECISION_RADIX] As [NumericPrecisionRadix],
        ///	[NUMERIC_SCALE] As [NumericScale],
        ///	[DATETIME_PRECISION] As [DateTime [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DbDomainItem {
            get {
                return ResourceManager.GetString("DbDomainItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select	Convert(UniqueIdentifier,Null) As [CatalogId],
        ///	Db_Name() [CatalogName],
        ///	@Level0Type [Level0Type],
        ///	@Level0Name [Level0Name],
        ///	@Level1Type [Level1Type],
        ///	@Level1Name [Level1Name],
        ///	@Level2Type [Level2Type],
        ///	@Level2Name [Level2Name],
        ///	[objtype] As [ObjType],
        ///	[objname] As [ObjName],
        ///	[name] As [PropertyName],
        ///	Convert(NVarChar(Max),[value]) As [PropertyValue]
        ///FROM [fn_listextendedproperty] (
        ///	@PropertyName,
        ///	@Level0Type,
        ///	@Level0Name,
        ///	@Level1Type,
        ///	@Level1Name,
        ///	@Level2Type,
        ///	@Le [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DbExtendedPropertyItem {
            get {
                return ResourceManager.GetString("DbExtendedPropertyItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Begin Try;
        ///Select	Convert(UniqueIdentifier,Null) As [CatalogId],
        ///	IsNull(R.[referenced_database_name],DB_Name()) As [CatalogName],
        ///	Object_Schema_Name(Object_id(@ObjectName)) As [SchemaName],
        ///	Object_Name(Object_id(@ObjectName)) As [RoutineName],
        ///	IsNull(R.[referenced_schema_name],&apos;dbo&apos;) As [ReferenceSchemaName],
        ///	R.[referenced_entity_name] As [ReferenceObjectName],
        ///	R.[referenced_class_desc] As [ReferenceObjectType],
        ///	R.referenced_minor_name As [ReferenceColumnName],
        ///	R.[is_caller_dependent] As [I [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DbRoutineDependencyItem {
            get {
                return ResourceManager.GetString("DbRoutineDependencyItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select	Convert(UniqueIdentifier,Null) As [CatalogId],
        ///	[ROUTINE_CATALOG] As [CatalogName], 
        ///	[ROUTINE_SCHEMA] As [SchemaName],
        ///	[ROUTINE_NAME] As [RoutineName],
        ///	[ROUTINE_TYPE] As [RoutineType]
        ///From	[INFORMATION_SCHEMA].[ROUTINES].
        /// </summary>
        internal static string DbRoutineItem {
            get {
                return ResourceManager.GetString("DbRoutineItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select	Convert(UniqueIdentifier,Null) As [CatalogId],
        ///	P.[SPECIFIC_CATALOG] As [CatalogName],
        ///	P.[SPECIFIC_SCHEMA] As [SchemaName],
        ///	P.[SPECIFIC_NAME] As [RoutineName],
        ///	R.[ROUTINE_TYPE] As [RoutineType],
        ///	IIF(R.[ROUTINE_TYPE] IN (&apos;FUNCTION&apos;) AND P.[ORDINAL_POSITION] = 0,&apos;RETURN&apos;,P.[PARAMETER_NAME]) As [ParameterName],
        ///	P.[ORDINAL_POSITION] As [OrdinalPosition],
        ///	P.[DATA_TYPE] As [DataType],
        ///	P.[CHARACTER_MAXIMUM_LENGTH] As [CharacterMaximumLength],
        ///	P.[CHARACTER_OCTET_LENGTH] As [CharacterOctetLen [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DbRoutineParameterItem {
            get {
                return ResourceManager.GetString("DbRoutineParameterItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select	Convert(UniqueIdentifier,Null) As [CatalogId],
        ///	[CATALOG_NAME] As [CatalogName],
        ///	[SCHEMA_NAME] As [SchemaName]
        ///From	[INFORMATION_SCHEMA].[SCHEMATA].
        /// </summary>
        internal static string DbSchemaItem {
            get {
                return ResourceManager.GetString("DbSchemaItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select	Convert(UniqueIdentifier,Null) As [CatalogId],
        ///	I.[TABLE_CATALOG] As [CatalogName],
        ///	I.[TABLE_SCHEMA] As [SchemaName],
        ///	I.[TABLE_NAME] As [TableName],
        ///	Case
        ///	When H.[object_id] is Not Null Then &apos;HISTORY TABLE&apos;
        ///	When T.[history_table_id] is Not Null Then &apos;TEMPORAL TABLE&apos;
        ///	Else I.[TABLE_TYPE]
        ///	End As [TableType]
        ///From	[INFORMATION_SCHEMA].[TABLES] I
        ///	Left Join [sys].[Tables] T
        ///	On	I.[TABLE_SCHEMA] = Object_Schema_Name(T.[object_id]) And
        ///		I.[TABLE_NAME] = Object_Name(T.[object_id])
        ///	Left Jo [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DbTableItem {
            get {
                return ResourceManager.GetString("DbTableItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .
        /// </summary>
        internal static string String1 {
            get {
                return ResourceManager.GetString("String1", resourceCulture);
            }
        }
    }
}
