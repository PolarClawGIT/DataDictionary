using DataDictionary.DataLayer.DatabaseData.Schema;
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
    public interface IDbTableKey : IKey, IDbSchemaKey
    {
        /// <summary>
        /// Name of the Database Table (or View)
        /// </summary>
        String? TableName { get; }
    }

    /// <summary>
    /// Implementation for the Database Table Key
    /// </summary>
    public class DbTableKey : DbSchemaKey, IDbTableKey, IKeyComparable<IDbTableKey>
    {
        /// <inheritdoc/>
        public String TableName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Database Table Key
        /// </summary>
        /// <param name="source"></param>
        public DbTableKey(IDbTableKey source) : base(source)
        { if (source.TableName is string) { TableName = source.TableName; } }

        /// <summary>
        /// Constructor for the Database Table Key
        /// </summary>
        /// <param name="source"></param>
        public DbTableKey(IDomainEntityAliasKey source) : base(source)
        { if (source.ObjectName is string) { TableName = source.ObjectName; } }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbTableKey? other)
        {
            return 
                other is IDbSchemaKey &&
                new DbSchemaKey(this).Equals(other) &&
                !string.IsNullOrEmpty(TableName) &&
                !string.IsNullOrEmpty(other.TableName) &&
                TableName.Equals(other.TableName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbTableKey value && Equals(new DbTableKey(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDbTableKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaKey(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(TableName, other.TableName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbTableKey value) { return CompareTo(new DbTableKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbTableKey left, DbTableKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbTableKey left, DbTableKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbTableKey left, DbTableKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbTableKey left, DbTableKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbTableKey left, DbTableKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbTableKey left, DbTableKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(DatabaseName, SchemaName, TableName); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (TableName is string)
            { return string.Format("{0}.{1}", base.ToString(), TableName); }
            else { return string.Empty; }
        }

    }
}
