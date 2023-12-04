using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DomainData.Alias;

namespace DataDictionary.DataLayer.DatabaseData.Domain
{
    /// <summary>
    /// Interface for the Database Domain (Type) Key
    /// </summary>
    public interface IDbDomainKeyName : IKey, IDbSchemaKeyName
    {
        /// <summary>
        /// Name of the Database Domain (Type)
        /// </summary>
        String? DomainName { get; }
    }

    /// <summary>
    /// Implementation for IDbDomainKeyName
    /// </summary>
    public static class DbDomainKeyNameExtension
    {
        /// <summary>
        /// Gets the Alias Name for the Database Domain.
        /// </summary>
        /// <returns></returns>
        public static String ToAliasName(this IDbDomainKeyName source)
        { return AliasExtension.FormatName(source.DatabaseName, source.SchemaName, source.DomainName); }
    }

    /// <summary>
    /// Implementation of the Database DomainKey
    /// </summary>
    public class DbDomainKeyName : DbSchemaKeyName, IDbDomainKeyName, IKeyComparable<IDbDomainKeyName>
    {
        /// <inheritdoc/>
        public String DomainName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Database Domain Key
        /// </summary>
        /// <param name="source"></param>
        public DbDomainKeyName(IDbDomainKeyName source) : base(source)
        {
            if (source.DomainName is string) { DomainName = source.DomainName; }
            else { DomainName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbDomainKeyName? other)
        {
            return
                other is IDbSchemaKeyName &&
                new DbSchemaKeyName(this).Equals(other) &&
                !string.IsNullOrEmpty(DomainName) &&
                !string.IsNullOrEmpty(other.DomainName) &&
                DomainName.Equals(other.DomainName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbDomainKeyName value && Equals(new DbDomainKeyName(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDbDomainKeyName? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(DomainName, other.DomainName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbDomainKeyName value) { return CompareTo(new DbDomainKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbDomainKeyName left, DbDomainKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbDomainKeyName left, DbDomainKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbDomainKeyName left, DbDomainKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbDomainKeyName left, DbDomainKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbDomainKeyName left, DbDomainKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbDomainKeyName left, DbDomainKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }


        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), DomainName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return this.ToAliasName(); }

    }
}
