using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.DataLayer.DomainData.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Table
{
    /// <summary>
    /// Interface for the Database Table Column Key
    /// </summary>
    public interface IDbTableColumnKeyName : IKey, IDbTableKeyName, IToAliasName
    {
        /// <summary>
        /// Name of the Database Column
        /// </summary>
        String? ColumnName { get; }
    }

    /// <summary>
    /// Implementation for IDbTableColumnKeyName
    /// </summary>
    public static class DbTableColumnKeyNameExtension
    {
        /// <summary>
        /// Gets the Alias Name for the Database Table Column.
        /// </summary>
        /// <returns></returns>
        public static String ToAliasName(this IDbTableColumnKeyName source)
        { return AliasExtension.FormatName(source.DatabaseName, source.SchemaName, source.TableName, source.ColumnName); }
    }

    /// <summary>
    /// Implementation of the Database Table Column Key
    /// </summary>
    public class DbTableColumnKeyName : DbTableKeyName, IDbTableColumnKeyName, IKeyComparable<IDbTableColumnKeyName>
    {
        /// <inheritdoc/>
        public string ColumnName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for a blank Database Column Key
        /// </summary>
        protected internal DbTableColumnKeyName() : base() { }

        /// <summary>
        /// Constructor for the Database Column Key
        /// </summary>
        /// <param name="source"></param>
        public DbTableColumnKeyName(IDbTableColumnKeyName source) : base(source)
        { if (source.ColumnName is string) { ColumnName = source.ColumnName; } }

        /// <summary>
        /// Try to Create a Database Column Key from the Alias.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">A four part Alias name with a Scope of a Table/View Column.</param>
        /// <returns>A Column Key or Null if a key could not be constructed.</returns>
        public static new DbTableColumnKeyName? TryCreate<T>(T source)
            where T : IAliasKeyName, IScopeKey
        {
            if (source.AliasName is null) { return null; }

            List<String> parsed = AliasExtension.NameParts(source.AliasName);
            if (parsed.Count != 4) { return null; }

            if (new ScopeKey(source).Scope is ScopeType.DatabaseTableColumn or ScopeType.DatabaseViewColumn)
            {
                return new DbTableColumnKeyName()
                {
                    DatabaseName = parsed[0],
                    SchemaName = parsed[1],
                    TableName = parsed[2],
                    ColumnName = parsed[3]
                };
            }
            else { return null; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbTableColumnKeyName? other)
        {
            return
                other is IDbSchemaKeyName &&
                new DbTableKeyName(this).Equals(other) &&
                !string.IsNullOrEmpty(ColumnName) &&
                !string.IsNullOrEmpty(other.ColumnName) &&
                ColumnName.Equals(other.ColumnName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbTableColumnKeyName value && Equals(new DbTableColumnKeyName(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDbTableColumnKeyName? other)
        {
            if (other is null) { return 1; }
            else if (new DbTableKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(ColumnName, other.ColumnName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbTableColumnKeyName value) { return CompareTo(new DbTableColumnKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbTableColumnKeyName left, DbTableColumnKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbTableColumnKeyName left, DbTableColumnKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbTableColumnKeyName left, DbTableColumnKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbTableColumnKeyName left, DbTableColumnKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbTableColumnKeyName left, DbTableColumnKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbTableColumnKeyName left, DbTableColumnKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ColumnName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return this.ToAliasName(); }

    }
}
