using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using DataDictionary.DataLayer.DatabaseData.Catalog;

namespace DataDictionary.DataLayer.DatabaseData.Schema
{
    /// <summary>
    /// Interface for the Database Schema Key
    /// </summary>
    public interface IDbSchemaKey : IKey, IDbCatalogKeyUnique
    {
        /// <summary>
        /// Name of the Database Schema
        /// </summary>
        String? SchemaName { get; }
    }

    /// <summary>
    /// Implementation of the Database Schema Key
    /// </summary>
    public class DbSchemaKey : DbCatalogKeyUnique, IDbSchemaKey, IKeyComparable<IDbSchemaKey>
    {
        /// <inheritdoc/>
        public string SchemaName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Database Scheme Key
        /// </summary>
        /// <param name="source"></param>
        public DbSchemaKey(IDbSchemaKey source) : base(source)
        {
            if (source.SchemaName is string) { SchemaName = source.SchemaName; }
            else { SchemaName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbSchemaKey? other)
        {
            return 
                other is IDbSchemaKey &&
                new DbCatalogKeyUnique(this).Equals(other) &&
                !string.IsNullOrEmpty(SchemaName) &&
                !string.IsNullOrEmpty(other.SchemaName) &&
                SchemaName.Equals(other.SchemaName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbSchemaKey value && Equals(new DbSchemaKey(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDbSchemaKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbCatalogKeyUnique(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(SchemaName, other.SchemaName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbSchemaKey value) { return CompareTo(new DbSchemaKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbSchemaKey left, DbSchemaKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbSchemaKey left, DbSchemaKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbSchemaKey left, DbSchemaKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbSchemaKey left, DbSchemaKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbSchemaKey left, DbSchemaKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbSchemaKey left, DbSchemaKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(CatalogName, SchemaName); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (SchemaName is string)
            { return string.Format("{0}.{1}", base.ToString(), SchemaName); }
            else { return string.Empty; }
        }
    }
}
