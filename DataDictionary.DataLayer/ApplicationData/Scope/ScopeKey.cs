﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData.Scope
{
    /// <summary>
    /// Interface for the ScopeKey
    /// </summary>
    public interface IScopeKey : IKey
    {
        /// <summary>
        /// Primary Key ID for the Scope
        /// </summary>
        ScopeType Scope { get; }
    }

    /// <summary>
    /// Implementation of the ScopeKey 
    /// </summary>
    /// <remarks>
    /// This is a wrapper around the ScopeType enum.
    /// Gives me better control and more consistent syntax then implementation with extension methods.
    /// </remarks>
    public class ScopeKey : IScopeKey, IKeyComparable<IScopeKey>, IEquatable<ScopeType>, IParsable<ScopeKey>
    {
        /// <inheritdoc/>
        public ScopeType Scope { get; init; } = ScopeType.Null;

        /// <summary>
        /// Basic Constructor for the Scope Key.
        /// </summary>
        protected ScopeKey() : base() { }

        /// <summary>
        /// Constructor for the Scope Key.
        /// </summary>
        /// <param name="source"></param>
        public ScopeKey(ScopeType source) : this()
        { Scope = source; } 

        /// <summary>
        /// Constructor for the Scope Key.
        /// </summary>
        /// <param name="source"></param>
        public ScopeKey(IScopeKey source) : this()
        { if (source is IScopeKey) { Scope = source.Scope; } }

        /// <summary>
        /// Constructor for the Scope Key.
        /// </summary>
        /// <param name="source"></param>
        public ScopeKey(IScopeKeyName source) : this()
        {
            if (ScopeKey.TryParse(source.ScopeName, out ScopeKey? result))
            { Scope = result.Scope; }
        }

        /// <summary>
        /// Converts a ScopeKey into a ScopeType.
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator ScopeKey(ScopeType source) { return new ScopeKey(source); }

        // This cannot be done because it creates an ambiguous reference.
        //public static implicit operator ScopeType (ScopeKey source) { return source.ScopeId; }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(IScopeKey? other)
        {
            return other is IScopeKey
                && this.Scope != ScopeType.Null
                && other.Scope != ScopeType.Null
                && this.Scope.Equals(other.Scope);
        }

        /// <inheritdoc/>
        public bool Equals(ScopeType other)
        {
            return this.Scope != ScopeType.Null &&
                    other != ScopeType.Null &&
                    this.Scope == other;
        }

        /// <inheritdoc/>
        public virtual int CompareTo(IScopeKey? other)
        {
            if (other is ScopeKey value)
            { return string.Compare(this.ToString(), value.ToString(), true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IScopeKey value) { return CompareTo(new ScopeKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IScopeKey value && this.Equals(new ScopeKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(ScopeKey left, ScopeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ScopeKey left, ScopeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(ScopeKey left, ScopeKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(ScopeKey left, ScopeKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(ScopeKey left, ScopeKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(ScopeKey left, ScopeKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return (Scope).GetHashCode(); }
        #endregion

        #region IParsable
        /// <summary>
        /// This is the list that translates the ScopeType Enum to what the Database uses as ScopeName.
        /// The Int32 of ScopeType does not need to match what is in the Database.
        /// The Text must match what is in the database.
        /// Each Level within the structure is delimited by a Period.
        /// </summary>
        static Dictionary<ScopeType, String> parseName = new Dictionary<ScopeType, string>()
        {
            {ScopeType.Null,                      "N/A" },

            {ScopeType.Library,                   "Library" },
            {ScopeType.LibraryEvent,              "Library.Event" },
            {ScopeType.LibraryField,              "Library.Field" },
            {ScopeType.LibraryMethod,             "Library.Method" },
            {ScopeType.LibraryNameSpace,          "Library.NameSpace" },
            {ScopeType.LibraryProperty,           "Library.Property" },
            {ScopeType.LibraryParameter,          "Library.Parameter" },
            {ScopeType.LibraryType,               "Library.Type" },

            {ScopeType.Database,                  "Database" },
            {ScopeType.DatabaseSchema,            "Database.Schema" },
            {ScopeType.DatabaseFunction,          "Database.Schema.Function" },
            {ScopeType.DatabaseProcedure,         "Database.Schema.Procedure" },
            {ScopeType.DatabaseTable,             "Database.Schema.Table" },
            {ScopeType.DatabaseDomain,            "Database.Schema.Type" },
            {ScopeType.DatabaseView,              "Database.Schema.View" },
            {ScopeType.DatabaseViewColumn,        "Database.Schema.View.Column" },
            {ScopeType.DatabaseTableColumn,       "Database.Schema.Table.Column" },
            {ScopeType.DatabaseTableConstraint,   "Database.Schema.Table.Constraint" },
            {ScopeType.DatabaseProcedureParameter,"Database.Schema.Procedure.Parameter" },
            {ScopeType.DatabaseFunctionParameter, "Database.Schema.Function.Parameter" },

            {ScopeType.Model,                     "Model" },
            {ScopeType.ModelAttribute,            "Model.Attribute" },
            {ScopeType.ModelEntity,               "Model.Entity" },
            {ScopeType.ModelSubjectArea,          "Model.SubjectArea" },
            {ScopeType.ModelNameSpace,            "Model.NameSpace" },
        };

        /// <inheritdoc/>
        public static ScopeKey Parse(String source, IFormatProvider? provider)
        {
            if (String.IsNullOrEmpty(source))
            { throw new ArgumentNullException(nameof(source)); }

            if (ScopeKey.TryParse(source, provider, out ScopeKey? result))
            { return result; }
            else
            {
                Exception ex = new FormatException();
                ex.Data.Add(nameof(source), source);
                throw ex;
            }
        }

        /// <inheritdoc/>
        public static bool TryParse([NotNullWhen(true)] String? source, IFormatProvider? provider, [MaybeNullWhen(false)] out ScopeKey result)
        {
            if (parseName.FirstOrDefault(w => w.Value.Equals(source, KeyExtension.CompareString)) is KeyValuePair<ScopeType, String> dbItem && dbItem.Key != ScopeType.Null)
            { result = new ScopeKey() { Scope = dbItem.Key }; return true; }
            else { result = null; return false; }
        }

        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="result">When this method returns, contains the result of successfully parsing</param>
        /// <returns>true if s was successfully parsed; otherwise, false.</returns>
        public static bool TryParse([NotNullWhen(true)] String? source, [MaybeNullWhen(false)] out ScopeKey result)
        {
            if (parseName.FirstOrDefault(w => w.Value.Equals(source, KeyExtension.CompareString)) is KeyValuePair<ScopeType, String> dbItem && dbItem.Key != ScopeType.Null)
            { result = new ScopeKey() { Scope = dbItem.Key }; return true; }
            else { result = null; return false; }
        }
        #endregion

        /// <summary>
        /// Returns the ScopeName.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (parseName.ContainsKey(Scope)) { return parseName[Scope]; }
            else { return Scope.ToString(); }
        }

    }
}
