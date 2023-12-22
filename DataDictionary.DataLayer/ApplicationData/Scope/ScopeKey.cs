using System;
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
        ScopeType ScopeId { get; }
    }

    /// <summary>
    /// Implementation of the ScopeKey 
    /// </summary>
    /// <remarks>
    /// This is a wrapper around the ScopeType enum.
    /// Gives me better control and more consistent syntax then implementation with extension methods.
    /// </remarks>
    public class ScopeKey : IScopeKey, IKeyEquality<IScopeKey>, IEquatable<ScopeType>, IParsable<ScopeKey>
    {
        /// <inheritdoc/>
        public ScopeType ScopeId { get; init; } = ScopeType.Null;

        /// <summary>
        /// Basic Constructor for the Scope Key.
        /// </summary>
        protected ScopeKey() : base() { }

        /// <summary>
        /// Constructor for the Scope Key.
        /// </summary>
        /// <param name="source"></param>
        public ScopeKey(ScopeType source) : this()
        { ScopeId = source; } 

        /// <summary>
        /// Constructor for the Scope Key.
        /// </summary>
        /// <param name="source"></param>
        public ScopeKey(IScopeKey source) : this()
        { if (source is IScopeKey) { ScopeId = source.ScopeId; } }

        /// <summary>
        /// Constructor for the Scope Key.
        /// </summary>
        /// <param name="source"></param>
        public ScopeKey(IScopeKeyName source) : this()
        {
            if (ScopeKey.TryParse(source.ScopeName, out ScopeKey? result))
            { ScopeId = result.ScopeId; }
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
                && this.ScopeId != ScopeType.Null
                && other.ScopeId != ScopeType.Null
                && this.ScopeId.Equals(other.ScopeId);
        }

        /// <inheritdoc/>
        public bool Equals(ScopeType other)
        {
            return this.ScopeId != ScopeType.Null &&
                    other != ScopeType.Null &&
                    this.ScopeId == other;
        }

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
        public override Int32 GetHashCode()
        { return (ScopeId).GetHashCode(); }
        #endregion

        #region IParsable
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
            if (scopeTypeToDatabaseScope.FirstOrDefault(w => w.Value.Equals(source, KeyExtension.CompareString)) is KeyValuePair<ScopeType, String> dbItem && dbItem.Key != ScopeType.Null)
            { result = new ScopeKey() { ScopeId = dbItem.Key }; return true; }
            else if (scopeTypeToLibraryScope.FirstOrDefault(w => w.Value.Equals(source, KeyExtension.CompareString)) is KeyValuePair<ScopeType, String> libraryItem && libraryItem.Key != ScopeType.Null)
            { result = new ScopeKey() { ScopeId = libraryItem.Key }; return true; }
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
            if (scopeTypeToDatabaseScope.FirstOrDefault(w => w.Value.Equals(source, KeyExtension.CompareString)) is KeyValuePair<ScopeType, String> dbItem && dbItem.Key != ScopeType.Null)
            { result = new ScopeKey() { ScopeId = dbItem.Key }; return true; }
            else if (scopeTypeToLibraryScope.FirstOrDefault(w => w.Value.Equals(source, KeyExtension.CompareString)) is KeyValuePair<ScopeType, String> libraryItem && libraryItem.Key != ScopeType.Null)
            { result = new ScopeKey() { ScopeId = libraryItem.Key }; return true; }
            else { result = null; return false; }
        }
        #endregion

        /// <summary>
        /// Returns the ScopeName.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (scopeTypeToLibraryScope.ContainsKey(ScopeId)) { return scopeTypeToLibraryScope[ScopeId]; }
            else if (scopeTypeToDatabaseScope.ContainsKey(ScopeId)) { return scopeTypeToDatabaseScope[ScopeId]; }
            else { return ScopeId.ToString(); }
        }

    }
}
