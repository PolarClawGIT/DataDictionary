﻿using DataDictionary.DataLayer.DatabaseData;
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
    /// <remarks>
    /// This list matches to the database view [App_DataDictionary].[ModelScope]
    /// </remarks>
    public enum ScopeType
    {
        /// <summary>
        /// Represents the undefined Scope.
        /// </summary>
        Null,

        /// <summary>
        /// .Net Library
        /// </summary>
        Library,

        /// <summary>
        /// .Net Library Event
        /// </summary>
        LibraryEvent,

        /// <summary>
        /// .Net Library Field
        /// </summary>
        LibraryField,

        /// <summary>
        /// .Net Library Method
        /// </summary>
        LibraryMethod,

        /// <summary>
        /// .Net Library NameSpace
        /// </summary>
        LibraryNameSpace,

        /// <summary>
        /// .Net Library Property
        /// </summary>
        LibraryProperty,

        /// <summary>
        /// .Net Library Parameter,
        /// </summary>
        LibraryParameter,

        /// <summary>
        /// .Net Library Type (Class, Enum, ...)
        /// </summary>
        LibraryType,

        /// <summary>
        /// SQL Database
        /// </summary>
        Database,

        /// <summary>
        /// SQL Schema
        /// </summary>
        DatabaseSchema,

        /// <summary>
        /// SQL Function
        /// </summary>
        DatabaseSchemaFunction,

        /// <summary>
        /// SQL Procedure
        /// </summary>
        DatabaseSchemaProcedure,

        /// <summary>
        /// SQL Table
        /// </summary>
        DatabaseSchemaTable,

        /// <summary>
        /// SQL Type/Domain
        /// </summary>
        DatabaseSchemaType,

        /// <summary>
        /// SQL View
        /// </summary>
        DatabaseSchemaView,

        /// <summary>
        /// SQL View Column
        /// </summary>
        DatabaseSchemaViewColumn,

        //DatabaseSchemaViewIndex,

        /// <summary>
        /// SQL Table Column
        /// </summary>
        DatabaseSchemaTableColumn,

        /// <summary>
        /// SQL Table Constraint
        /// </summary>
        DatabaseSchemaTableConstraint,

        //DatabaseSchemaTableIndex,

        /// <summary>
        /// SQL Procedure Parameter
        /// </summary>
        DatabaseSchemaProcedureParameter,

        /// <summary>
        /// SQL Function Parameter
        /// </summary>
        DatabaseSchemaFunctionParameter,
    }

    /// <summary>
    /// Support Extension for the Scope Type
    /// </summary>
    public static class ScopeTypeExtension
    {
        static Dictionary<ScopeType, String> scopeTypeToLibraryScope = new Dictionary<ScopeType, String>()
        {
            {ScopeType.Library,"Library" },
            {ScopeType.LibraryEvent,"Library.Event" },
            {ScopeType.LibraryField,"Library.Field" },
            {ScopeType.LibraryMethod,"Library.Method" },
            {ScopeType.LibraryNameSpace,"Library.NameSpace" },
            {ScopeType.LibraryProperty,"Library.Property" },
            {ScopeType.LibraryParameter,"Library.Parameter" },
            {ScopeType.LibraryType,"Library.Type" },
        };

        static Dictionary<ScopeType, String> scopeTypeToDatabaseScope = new Dictionary<ScopeType, String>()
        {
            {ScopeType.Database,"Database" },
            {ScopeType.DatabaseSchema,"Database.Schema" },
            {ScopeType.DatabaseSchemaFunction,"Database.Schema.Function" },
            {ScopeType.DatabaseSchemaProcedure,"Database.Schema.Procedure" },
            {ScopeType.DatabaseSchemaTable,"Database.Schema.Table" },
            {ScopeType.DatabaseSchemaType,"Database.Schema.Type" },
            {ScopeType.DatabaseSchemaView,"Database.Schema.View" },
            {ScopeType.DatabaseSchemaViewColumn,"Database.Schema.View.Column" },
            {ScopeType.DatabaseSchemaTableColumn,"Database.Schema.Table.Column" },
            {ScopeType.DatabaseSchemaTableConstraint,"Database.Schema.Table.Constraint" },
            {ScopeType.DatabaseSchemaProcedureParameter,"Database.Schema.Procedure.Parameter" },
            {ScopeType.DatabaseSchemaFunctionParameter,"Database.Schema.Function.Parameter" },
        };

        /// <summary>
        /// Translates a Database Scope to a ScopeType
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static ScopeType ToScopeType(String? value)
        {
            if (scopeTypeToDatabaseScope.FirstOrDefault(w => w.Value.Equals(value, KeyExtension.CompareString)) is KeyValuePair<ScopeType, String> dbItem && dbItem.Key != ScopeType.Null)
            { return dbItem.Key; }
            else if (scopeTypeToLibraryScope.FirstOrDefault(w => w.Value.Equals(value, KeyExtension.CompareString)) is KeyValuePair<ScopeType, String> libraryItem && libraryItem.Key != ScopeType.Null)
            { return libraryItem.Key; }
            else { return ScopeType.Null; }
        }

        /// <summary>
        /// Translates a Application Scope to a ScopeType
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ScopeType ToScopeType(this IScopeKeyName value)
        {
            if (scopeTypeToDatabaseScope.FirstOrDefault(w => w.Value.Equals(value.ScopeName, KeyExtension.CompareString)) is KeyValuePair<ScopeType, String> dbScope && dbScope.Key != ScopeType.Null)
            { return dbScope.Key; }
            else if (scopeTypeToDatabaseScope.FirstOrDefault(w => w.Value.Equals(value.ScopeName, KeyExtension.CompareString)) is KeyValuePair<ScopeType, String> libraryScope && libraryScope.Key != ScopeType.Null)
            { return libraryScope.Key; }
            else { return ScopeType.Null; }
        }

        /// <summary>
        /// Translates the ScopeType to a ScopeName (String).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String ToScopeName(this ScopeType value)
        {
            if (scopeTypeToLibraryScope.ContainsKey(value))
            { return scopeTypeToLibraryScope[value]; }
            else if (scopeTypeToDatabaseScope.ContainsKey(value))
            { return scopeTypeToDatabaseScope[value]; }
            else if (value == ScopeType.Null) { return String.Empty; }
            else
            {
                Exception ex = new ArgumentOutOfRangeException(nameof(value));
                ex.Data.Add(nameof(value), value.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// Coverts the Member Type to a Scope Type for a Library Member.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static ScopeType ToScopeType(this ILibraryMemberItem item)
        {
            if (scopeTypeToLibraryScope.FirstOrDefault(w => w.Value.Equals(item.ScopeName, KeyExtension.CompareString)) is KeyValuePair<ScopeType, String> memberScope && memberScope.Key != ScopeType.Null)
            { return memberScope.Key; }
            else
            { return ScopeType.Null; }
        }
    }
}