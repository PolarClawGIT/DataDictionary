using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.DataLayer.DomainData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Table
{
    /// <summary>
    /// Interface for the Database Table Key
    /// </summary>
    public interface IDbTableKeyName : IKey, IDbSchemaKeyName, IToAliasName
    {
        /// <summary>
        /// Name of the Database Table (or View)
        /// </summary>
        String? TableName { get; }
    }

    /// <summary>
    /// Implementation for IDbRoutineKeyName
    /// </summary>
    public static class DbTableKeyNameExtension
    {
        /// <summary>
        /// Gets the Alias Name for the Database Table.
        /// </summary>
        /// <returns></returns>
        public static String ToAliasName(this IDbTableKeyName source)
        { return AliasExtension.FormatName(source.DatabaseName, source.SchemaName, source.TableName); }

   }

    /// <summary>
    /// Implementation for the Database Table Key
    /// </summary>
    public class DbTableKeyName : DbSchemaKeyName, IDbTableKeyName, IKeyComparable<IDbTableKeyName>
    {
        /// <inheritdoc/>
        public String TableName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for a blank Database Table Key
        /// </summary>
        protected internal DbTableKeyName() : base() { }

        /// <summary>
        /// Constructor for the Database Table Key
        /// </summary>
        /// <param name="source"></param>
        public DbTableKeyName(IDbTableKeyName source) : base(source)
        { if (source.TableName is string) { TableName = source.TableName; } }

        /// <summary>
        /// Try to Create a Database Table Key from the Alias.
        /// </summary>
        /// <param name="source">A four part Alias name with a Scope of a Table or View.</param>
        /// <returns>A Table Key or Null if a key could not be constructed.</returns>
        public static DbTableKeyName? TryCreate(IDomainAliasItem source)
        {
            if (source.AliasName is null) { return null; }

            List<String> parsed = AliasExtension.NameParts(source.AliasName);
            if (parsed.Count != 3) { return null; }

            if (new ScopeKey(source).ScopeId is ScopeType.DatabaseSchemaTableColumn or ScopeType.DatabaseSchemaViewColumn)
            {
                return new DbTableKeyName()
                {
                    DatabaseName = parsed[0],
                    SchemaName = parsed[1],
                    TableName = parsed[2]
                };
            }
            else { return null; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbTableKeyName? other)
        {
            return 
                other is IDbSchemaKeyName &&
                new DbSchemaKeyName(this).Equals(other) &&
                !string.IsNullOrEmpty(TableName) &&
                !string.IsNullOrEmpty(other.TableName) &&
                TableName.Equals(other.TableName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbTableKeyName value && Equals(new DbTableKeyName(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDbTableKeyName? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(TableName, other.TableName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbTableKeyName value) { return CompareTo(new DbTableKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbTableKeyName left, DbTableKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbTableKeyName left, DbTableKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbTableKeyName left, DbTableKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbTableKeyName left, DbTableKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbTableKeyName left, DbTableKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbTableKeyName left, DbTableKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), TableName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return this.ToAliasName(); }
    }
}
