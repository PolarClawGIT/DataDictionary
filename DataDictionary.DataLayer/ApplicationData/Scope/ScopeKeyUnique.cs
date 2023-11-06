using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData.Scope
{
    /// <summary>
    /// Interface for the Unique Scope Key
    /// </summary>
    public interface IScopeKeyUnique :IKey
    {
        /// <summary>
        /// Name of the Scoped (period delimited full name, like a NameSpace)
        /// </summary>
        String? ScopeName { get; }
    }

    /// <summary>
    /// Implementation for the Unique Scope Key
    /// </summary>
    public class ScopeKeyUnique: IScopeKeyUnique, IKeyComparable<IScopeKeyUnique>
    {
        /// <inheritdoc/>
        public String ScopeName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Catalog Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public ScopeKeyUnique(IScopeKeyUnique source) : base()
        {
            if (source.ScopeName is string) { ScopeName = source.ScopeName; }
            else { ScopeName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IScopeKeyUnique? other)
        {
            return
                other is IScopeKeyUnique &&
                !string.IsNullOrEmpty(ScopeName) &&
                !string.IsNullOrEmpty(other.ScopeName) &&
                ScopeName.Equals(other.ScopeName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IScopeKeyUnique value && Equals(new ScopeKeyUnique(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IScopeKeyUnique? other)
        {
            if (other is IScopeKeyUnique value)
            { return string.Compare(ScopeName, value.ScopeName, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IScopeKeyUnique value) { return CompareTo(new ScopeKeyUnique(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(ScopeKeyUnique left, ScopeKeyUnique right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ScopeKeyUnique left, ScopeKeyUnique right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(ScopeKeyUnique left, ScopeKeyUnique right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(ScopeKeyUnique left, ScopeKeyUnique right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(ScopeKeyUnique left, ScopeKeyUnique right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(ScopeKeyUnique left, ScopeKeyUnique right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return ScopeName.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (ScopeName is string) { return ScopeName; }
            else { return string.Empty; }
        }
    }
}
