using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Alias
{
    /// <summary>
    /// Interface for the Domain Source Name Key
    /// </summary>
    public interface IDomainAliasSourceKey : IKey
    {
        /// <summary>
        /// Name of the Source of the Alias.
        /// Name of Database or Assembly.
        /// </summary>
        String? SourceName { get; }
    }

    /// <summary>
    /// Implementation for the Domain Source Name Key
    /// </summary>
    public class DomainAliasSourceKey : IDomainAliasSourceKey, IKeyComparable<IDomainAliasSourceKey>
    {
        /// <inheritdoc/>
        public String SourceName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Domain Alias Key
        /// </summary>
        protected DomainAliasSourceKey() : base() { }

        /// <summary>
        /// Constructor for the Domain Alias Key
        /// </summary>
        /// <param name="source"></param>
        public DomainAliasSourceKey(IDomainAliasSourceKey source) : this()
        {
            if (source.SourceName is string) { SourceName = source.SourceName; }
            else { SourceName = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Domain Alias Key
        /// </summary>
        /// <param name="source"></param>
        public DomainAliasSourceKey(IDbCatalogKeyName source) : this()
        {
            if (source.DatabaseName is string) { SourceName = source.DatabaseName; }
            else { SourceName = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Domain Alias Key
        /// </summary>
        /// <param name="source"></param>
        public DomainAliasSourceKey(ILibrarySourceUniqueKey source) : this()
        {
            if (source.AssemblyName is string) { SourceName = source.AssemblyName; }
            else { SourceName = string.Empty; }
        }


        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IDomainAliasSourceKey? other)
        {
            return
                other is IDomainAliasSourceKey &&
                !string.IsNullOrEmpty(SourceName) &&
                !string.IsNullOrEmpty(other.SourceName) &&
                SourceName.Equals(other.SourceName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDomainAliasSourceKey value && Equals(new DomainAliasSourceKey(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IDomainAliasSourceKey? other)
        {
            if (other is null) { return 1; }
            else { return string.Compare(SourceName, other.SourceName, true); }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IDomainAliasSourceKey value) { return CompareTo(new DomainAliasSourceKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DomainAliasSourceKey left, DomainAliasSourceKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainAliasSourceKey left, DomainAliasSourceKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DomainAliasSourceKey left, DomainAliasSourceKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DomainAliasSourceKey left, DomainAliasSourceKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DomainAliasSourceKey left, DomainAliasSourceKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DomainAliasSourceKey left, DomainAliasSourceKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(
            base.GetHashCode(),
            SourceName.GetHashCode(KeyExtension.CompareString));
        }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (SourceName is string) { return SourceName; }
            else { return string.Empty; }
        }
    }
}
