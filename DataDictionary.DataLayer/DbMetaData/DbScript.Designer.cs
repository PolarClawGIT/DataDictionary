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
        ///   Looks up a localized string similar to SELECT Db_Name() [CatalogName], @Level0Type [Level0Type], @Level0Name [Level0Name], @Level1Type [Level1Type], @Level1Name [Level1Name], @Level2Type [Level2Type], @Level2Name [Level2Name], [objtype], [objname], [name], [value] FROM [fn_listextendedproperty](@PropertyName, @Level0Type, @Level0Name, @Level1Type, @Level1Name, @Level2Type, @Level2Name).
        /// </summary>
        internal static string DbExtendedPropertyItem {
            get {
                return ResourceManager.GetString("DbExtendedPropertyItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select Db_Name() As [SCHEMA_CATALOG], [name] As [SCHEMA_NAME] From [Sys].[Schemas].
        /// </summary>
        internal static string DbSchemaItem {
            get {
                return ResourceManager.GetString("DbSchemaItem", resourceCulture);
            }
        }
    }
}