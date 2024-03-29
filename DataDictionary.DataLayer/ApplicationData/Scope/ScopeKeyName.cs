using DataDictionary.DataLayer.DatabaseData;
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
    [Obsolete("This goes away once the name is removed from the Database")]
    public interface IScopeKeyName : IKey
    {
        /// <summary>
        /// The Database Scope Name. This describes the Scope/Level of an Object within database.
        /// </summary>
        string? ScopeName { get; }
    }

    /// <summary>
    /// Implementation for the Unique Scope Key
    /// </summary>
    [Obsolete("This goes away once the name is removed from the Database")]
    public class ScopeKeyName : IScopeKeyName, IKeyComparable<IScopeKeyName>
    {
        /// <inheritdoc/>
        public String ScopeName { get; init; } = string.Empty;

        /// <summary>
        /// Internal Constructor needed by ScopeType
        /// </summary>
        internal ScopeKeyName() : base() { }

        /// <summary>
        /// Constructor for the Scope Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public ScopeKeyName(IScopeKeyName source) : this()
        {
            if (source.ScopeName is string) { ScopeName = source.ScopeName; }
            else { ScopeName = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Scope Unique Key
        /// </summary>
        /// <param name="source"></param>
        public ScopeKeyName(IScopeKey source) : this()
        {
            ScopeKey key = new ScopeKey(source);
            ScopeName = key.ToString();
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IScopeKeyName? other)
        {
            return
                other is IScopeKeyName &&
                !string.IsNullOrEmpty(ScopeName) &&
                !string.IsNullOrEmpty(other.ScopeName) &&
                ScopeName.Equals(other.ScopeName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IScopeKeyName value && Equals(new ScopeKeyName(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IScopeKeyName? other)
        {
            if (other is IScopeKeyName value)
            { return string.Compare(ScopeName, value.ScopeName, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IScopeKeyName value) { return CompareTo(new ScopeKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(ScopeKeyName left, ScopeKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ScopeKeyName left, ScopeKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(ScopeKeyName left, ScopeKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(ScopeKeyName left, ScopeKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(ScopeKeyName left, ScopeKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(ScopeKeyName left, ScopeKeyName right)
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
