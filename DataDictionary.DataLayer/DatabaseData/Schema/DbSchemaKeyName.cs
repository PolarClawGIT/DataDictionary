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
    public interface IDbSchemaKeyName : IKey, IDbCatalogKeyName
    {
        /// <summary>
        /// Name of the Database Schema
        /// </summary>
        String? SchemaName { get; }
    }

    /// <summary>
    /// Implementation of the Database Schema Key
    /// </summary>
    public class DbSchemaKeyName : DbCatalogKeyName, IDbSchemaKeyName, IKeyComparable<IDbSchemaKeyName>
    {
        /// <inheritdoc/>
        public string SchemaName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Database Scheme Key
        /// </summary>
        /// <param name="source"></param>
        public DbSchemaKeyName(IDbSchemaKeyName source) : base(source)
        {
            if (source.SchemaName is string) { SchemaName = source.SchemaName; }
            else { SchemaName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbSchemaKeyName? other)
        {
            return 
                other is IDbSchemaKeyName &&
                new DbCatalogKeyName(this).Equals(other) &&
                !string.IsNullOrEmpty(SchemaName) &&
                !string.IsNullOrEmpty(other.SchemaName) &&
                SchemaName.Equals(other.SchemaName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbSchemaKeyName value && Equals(new DbSchemaKeyName(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDbSchemaKeyName? other)
        {
            if (other is null) { return 1; }
            else if (new DbCatalogKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(SchemaName, other.SchemaName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbSchemaKeyName value) { return CompareTo(new DbSchemaKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbSchemaKeyName left, DbSchemaKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbSchemaKeyName left, DbSchemaKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbSchemaKeyName left, DbSchemaKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbSchemaKeyName left, DbSchemaKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbSchemaKeyName left, DbSchemaKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbSchemaKeyName left, DbSchemaKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), SchemaName.GetHashCode(KeyExtension.CompareString)); }
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
