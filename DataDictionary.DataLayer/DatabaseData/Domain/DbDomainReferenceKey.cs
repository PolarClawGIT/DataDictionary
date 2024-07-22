using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Domain
{
    /// <summary>
    /// Interface for the Database Domain Reference Key
    /// </summary>
    public interface IDbDomainReferenceKey : IKey
    {
        /// <summary>
        /// Database Catalog Name for the Domain.
        /// </summary>
        string? DomainCatalog { get; }

        /// <summary>
        /// Database Schema Name for the Domain
        /// </summary>
        string? DomainSchema { get; }

        /// <summary>
        /// Database Domain Name.
        /// </summary>
        string? DomainName { get; }
    }

    /// <summary>
    /// Implementation of the Database Domain Reference Key
    /// </summary>
    public class DbDomainReferenceKey : IDbDomainReferenceKey, IEquatable<IDbDomainReferenceKey>
    {
        /// <inheritdoc/>
        public string DomainCatalog { get; init; }

        /// <inheritdoc/>
        public string DomainSchema { get; init; }

        /// <inheritdoc/>
        public string DomainName { get; init; }

        /// <summary>
        /// Constructor for the Database Domain Reference Key
        /// </summary>
        /// <param name="source"></param>
        public DbDomainReferenceKey(IDbDomainReferenceKey source) : base()
        {
            if (source.DomainCatalog is string) { DomainCatalog = source.DomainCatalog; }
            else { DomainCatalog = string.Empty; }

            if (source.DomainSchema is string) { DomainSchema = source.DomainSchema; }
            else { DomainSchema = string.Empty; }

            if (source.DomainName is string) { DomainName = source.DomainName; }
            else { DomainName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbDomainReferenceKey? other)
        {
            return 
                other is IDbDomainReferenceKey &&
                !string.IsNullOrEmpty(DomainCatalog) &&
                !string.IsNullOrEmpty(other.DomainCatalog) &&
                !string.IsNullOrEmpty(DomainSchema) &&
                !string.IsNullOrEmpty(other.DomainSchema) &&
                !string.IsNullOrEmpty(DomainName) &&
                !string.IsNullOrEmpty(other.DomainName) &&
                DomainCatalog.Equals(other.DomainCatalog, KeyExtension.CompareString) &&
                DomainSchema.Equals(other.DomainSchema, KeyExtension.CompareString) &&
                DomainName.Equals(other.DomainName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbDomainReferenceKey value && Equals(new DbDomainReferenceKey(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDbDomainReferenceKey? other)
        {
            if (other is null) { return 1; }
            else
            {
                if (string.Compare(DomainCatalog,
                                   other.DomainCatalog,
                                   true) is int catalogValue && catalogValue != 0)
                { return catalogValue; }
                else if (string.Compare(DomainSchema,
                                   other.DomainSchema,
                                   true) is int schemaValue && schemaValue != 0)
                { return schemaValue; }
                else { return string.Compare(DomainName, other.DomainName, true); }
            }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IDbDomainReferenceKey value) { return CompareTo(new DbDomainReferenceKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbDomainReferenceKey left, DbDomainReferenceKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbDomainReferenceKey left, DbDomainReferenceKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbDomainReferenceKey left, DbDomainReferenceKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbDomainReferenceKey left, DbDomainReferenceKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbDomainReferenceKey left, DbDomainReferenceKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbDomainReferenceKey left, DbDomainReferenceKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), DomainName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (DomainCatalog is string && DomainSchema is string && DomainName is string)
            { return string.Format("{0}.{1}.{2}", DomainCatalog, DomainSchema, DomainName); }
            else { return string.Empty; }
        }


    }
}
