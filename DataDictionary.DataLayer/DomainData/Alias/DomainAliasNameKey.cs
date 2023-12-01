using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.LibraryData.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Alias
{
    /// <summary>
    /// Interface for the Domain Alias Key
    /// </summary>
    public interface IDomainAliasNameKey : IKey
    {
        /// <summary>
        /// Name of the Alias.
        /// </summary>
        String? AliasName { get; }
    }

    /// <summary>
    /// Implementation for the Domain Alias Key
    /// </summary>
    public class DomainAliasNameKey : IDomainAliasNameKey, IKeyComparable<IDomainAliasNameKey>
    {
        /// <inheritdoc/>
        public String AliasName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Domain Alias Key
        /// </summary>
        protected DomainAliasNameKey () : base() { }

        /// <summary>
        /// Constructor for the Domain Alias Key
        /// </summary>
        /// <param name="source"></param>
        public DomainAliasNameKey(IDomainAliasNameKey source): this()
        {
            if (source.AliasName is string) { AliasName = source.AliasName; }
            else { AliasName = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Database Alias Key
        /// </summary>
        /// <param name="source"></param>
        public DomainAliasNameKey(IDbCatalogKeyUnique source) : this()
        { AliasName = source.ToAliasName(); }

        /// <summary>
        /// Constructor for the Schema Alias Key
        /// </summary>
        /// <param name="source"></param>
        public DomainAliasNameKey(IDbSchemaKey source) : this()
        { AliasName = source.ToAliasName(); }

        /// <summary>
        /// Constructor for the Table Alias Key
        /// </summary>
        /// <param name="source"></param>
        public DomainAliasNameKey(IDbTableKey source) : this()
        { AliasName = source.ToAliasName(); }

        /// <summary>
        /// Constructor for the Table Column Alias Key
        /// </summary>
        /// <param name="source"></param>
        public DomainAliasNameKey(IDbTableColumnKey source) : this()
        { AliasName = source.ToAliasName(); }

        /// <summary>
        /// Constructor for the Domain Alias Key
        /// </summary>
        /// <param name="source"></param>
        public DomainAliasNameKey(ILibraryMemberAlternateKey source) : this()
        { AliasName = source.ToAliasName(); }

        /// <summary>
        /// Returns the Parent Alias Key, if any
        /// </summary>
        /// <returns></returns>
        public DomainAliasNameKey Parent()
        {
            if (this.AliasName.Contains(".["))
            {
                String reverse = new String(this.AliasName.Reverse().ToArray());
                Int32 index = reverse.IndexOf("[.");
                String value = this.AliasName.Substring(0, this.AliasName.Length - index - 1);

                return new DomainAliasNameKey(this) { AliasName = value };
            }
            else { return new DomainAliasNameKey(); }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IDomainAliasNameKey? other)
        {
            return
                other is IDomainAliasNameKey &&
                !string.IsNullOrEmpty(AliasName) &&
                !string.IsNullOrEmpty(other.AliasName) &&
                AliasName.Equals(other.AliasName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDomainAliasNameKey value && Equals(new DomainAliasNameKey(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IDomainAliasNameKey? other)
        {
            if (other is null) { return 1; }
            else { return string.Compare(AliasName, other.AliasName, true); }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IDomainAliasNameKey value) { return CompareTo(new DomainAliasNameKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DomainAliasNameKey left, DomainAliasNameKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainAliasNameKey left, DomainAliasNameKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DomainAliasNameKey left, DomainAliasNameKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DomainAliasNameKey left, DomainAliasNameKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DomainAliasNameKey left, DomainAliasNameKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DomainAliasNameKey left, DomainAliasNameKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(
            base.GetHashCode(),
            AliasName.GetHashCode(KeyExtension.CompareString));
        }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (AliasName is string) { return AliasName; }
            else { return string.Empty; }
        }
    }
}
