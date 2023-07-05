﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataDictionary.DataLayer.DbMetaData {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DataDictionary.DataLayer.DbMetaData.DbScript", typeof(DbScript).Assembly);
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
        ///	Db_Name() As [CatalogName],
        ///	@Server As [SourceServerName].
        /// </summary>
        internal static string DbCatalogItem {
            get {
                return ResourceManager.GetString("DbCatalogItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select	Convert(UniqueIdentifier,Null) As [CatalogId],
        ///	I.[TABLE_CATALOG],
        ///	I.[TABLE_SCHEMA],
        ///	I.[TABLE_NAME],
        ///	I.[COLUMN_NAME],
        ///	I.[ORDINAL_POSITION],
        ///	I.[COLUMN_DEFAULT],
        ///	I.[IS_NULLABLE],
        ///	I.[DATA_TYPE],
        ///	I.[CHARACTER_MAXIMUM_LENGTH],
        ///	I.[CHARACTER_OCTET_LENGTH],
        ///	I.[NUMERIC_PRECISION],
        ///	I.[NUMERIC_PRECISION_RADIX],
        ///	I.[NUMERIC_SCALE],
        ///	I.[DATETIME_PRECISION],
        ///	I.[CHARACTER_SET_CATALOG],
        ///	I.[CHARACTER_SET_SCHEMA],
        ///	I.[CHARACTER_SET_NAME],
        ///	I.[COLLATION_CATALOG],
        ///	I.[COLLATION_SCHEMA],
        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DbColumnItem {
            get {
                return ResourceManager.GetString("DbColumnItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT Db_Name() [CatalogName], @Level0Type [Level0Type], @Level0Name [Level0Name], @Level1Type [Level1Type], @Level1Name [Level1Name], @Level2Type [Level2Type], @Level2Name [Level2Name], [objtype], [objname], [name], [value] FROM [fn_listextendedproperty](@PropertyName, @Level0Type, @Level0Name, @Level1Type, @Level1Name, @Level2Type, @Level2Name).
        /// </summary>
        internal static string DbExtendedPropertyItem {
            get {
                return ResourceManager.GetString("DbExtendedPropertyItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select	Convert(UniqueIdentifier,Null) As [CatalogId],
        ///	[CATALOG_NAME],
        ///	[SCHEMA_NAME]
        ///From	[INFORMATION_SCHEMA].[SCHEMATA].
        /// </summary>
        internal static string DbSchemaItem {
            get {
                return ResourceManager.GetString("DbSchemaItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select	Convert(UniqueIdentifier,Null) As [CatalogId],
        ///	I.[TABLE_CATALOG],
        ///	I.[TABLE_SCHEMA],
        ///	I.[TABLE_NAME],
        ///	Case
        ///	When H.[object_id] is Not Null Then &apos;HISTORY TABLE&apos;
        ///	When T.[history_table_id] is Not Null Then &apos;TEMPORAL TABLE&apos;
        ///	Else I.[TABLE_TYPE]
        ///	End As [TABLE_TYPE]
        ///From	[INFORMATION_SCHEMA].[TABLES] I
        ///	Left Join [sys].[Tables] T
        ///	On	I.[TABLE_SCHEMA] = Object_Schema_Name(T.[object_id]) And
        ///		I.[TABLE_NAME] = Object_Name(T.[object_id])
        ///	Left Join [sys].[Tables] H
        ///	On	I.[TABLE_SCHEMA] = Obj [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DbTableItem {
            get {
                return ResourceManager.GetString("DbTableItem", resourceCulture);
            }
        }
    }
}
