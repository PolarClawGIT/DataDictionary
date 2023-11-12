using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Alias
{
    /// <summary>
    /// Interface for the Domain Alias Unique Key
    /// </summary>
    public interface IDomainAliasUniqueKey : IKey
    {
        /// <summary>
        /// Fully Qualified Alias Name.
        /// Value is to be formated with each element qualified by square brackets and separated by period.
        /// </summary>
        /// <remarks>
        /// This is to match DB Qualified Name Syntax.
        /// MS SQL allows for periods and other symbols in the name, but not square brackets.
        /// C# and Vb.Net element/member names do not allow square brackets
        /// </remarks>
        String? AliasName { get; }

    }

    /// <summary>
    /// Implementation for the Domain Alias Unique Key
    /// </summary>
    public class DomainAliasUniqueKey : IDomainAliasUniqueKey, IKeyComparable<IDomainAliasUniqueKey>
    {
        /// <inheritdoc/>
        public String AliasName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Attribute Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public DomainAliasUniqueKey(IDomainAliasUniqueKey source) : base()
        {
            if (source.AliasName is string) { AliasName = source.AliasName; }
            else { AliasName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IDomainAliasUniqueKey? other)
        {
            return
                other is IDomainAliasUniqueKey &&
                !string.IsNullOrEmpty(AliasName) &&
                !string.IsNullOrEmpty(other.AliasName) &&
                AliasName.Equals(other.AliasName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDomainAliasUniqueKey value && Equals(new DomainAliasUniqueKey(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IDomainAliasUniqueKey? other)
        {
            if (other is DomainAliasUniqueKey value)
            { return string.Compare(AliasName, value.AliasName, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IDomainAliasUniqueKey value) { return CompareTo(new DomainAliasUniqueKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DomainAliasUniqueKey left, DomainAliasUniqueKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainAliasUniqueKey left, DomainAliasUniqueKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DomainAliasUniqueKey left, DomainAliasUniqueKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DomainAliasUniqueKey left, DomainAliasUniqueKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DomainAliasUniqueKey left, DomainAliasUniqueKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DomainAliasUniqueKey left, DomainAliasUniqueKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return AliasName.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (AliasName is string) { return AliasName; }
            else { return string.Empty; }
        }
    }
}
