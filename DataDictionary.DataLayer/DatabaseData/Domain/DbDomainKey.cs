using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDictionary.DataLayer.DatabaseData.Schema;

namespace DataDictionary.DataLayer.DatabaseData.Domain
{
    /// <summary>
    /// Interface for the Database Domain (Type) Key
    /// </summary>
    public interface IDbDomainKey : IKey, IDbSchemaKey
    {
        /// <summary>
        /// Name of the Database Domain (Type)
        /// </summary>
        String? DomainName { get; }
    }

    /// <summary>
    /// Implementation of the Database DomainKey
    /// </summary>
    public class DbDomainKey : DbSchemaKey, IDbDomainKey, IKeyComparable<IDbDomainKey>
    {
        /// <inheritdoc/>
        public String DomainName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Database Domain Key
        /// </summary>
        /// <param name="source"></param>
        public DbDomainKey(IDbDomainKey source) : base(source)
        {
            if (source.DomainName is string) { DomainName = source.DomainName; }
            else { DomainName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbDomainKey? other)
        {
            return 
                other is IDbSchemaKey &&
                new DbSchemaKey(this).Equals(other) &&
                !string.IsNullOrEmpty(DomainName) &&
                !string.IsNullOrEmpty(other.DomainName) &&
                DomainName.Equals(other.DomainName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbDomainKey value && Equals(new DbDomainKey(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDbDomainKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaKey(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(DomainName, other.DomainName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbDomainKey value) { return CompareTo(new DbDomainKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbDomainKey left, DbDomainKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbDomainKey left, DbDomainKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbDomainKey left, DbDomainKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbDomainKey left, DbDomainKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbDomainKey left, DbDomainKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbDomainKey left, DbDomainKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(CatalogName, SchemaName, DomainName); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (DomainName is string)
            { return string.Format("{0}.{1}", base.ToString(), DomainName); }
            else { return string.Empty; }
        }
    }
}
