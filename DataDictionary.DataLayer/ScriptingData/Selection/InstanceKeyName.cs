using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Selection
{
    /// <summary>
    /// Interface for the Scripting Instance Key
    /// </summary>
    public interface IInstanceKeyName : IKey
    {
        /// <summary>
        /// Name of the Instance.
        /// </summary>
        String? InstanceName { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Instance Key
    /// </summary>
    public class InstanceKeyName : IInstanceKeyName, IKeyComparable<IInstanceKeyName>
    {
        /// <inheritdoc/>
        public String InstanceName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Scripting Instance Key
        /// </summary>
        protected InstanceKeyName() : base() { }

        /// <summary>
        /// Constructor for the Scripting Instance Key
        /// </summary>
        /// <param name="source"></param>
        public InstanceKeyName(IInstanceKeyName source) : this()
        {
            if (source.InstanceName is string) { InstanceName = source.InstanceName; }
            else { InstanceName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IInstanceKeyName? other)
        {
            return
                other is IInstanceKeyName &&
                !string.IsNullOrEmpty(InstanceName) &&
                !string.IsNullOrEmpty(other.InstanceName) &&
                InstanceName.Equals(other.InstanceName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IInstanceKeyName value && Equals(new InstanceKeyName(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IInstanceKeyName? other)
        {
            if (other is null) { return 1; }
            else { return string.Compare(InstanceName, other.InstanceName, true); }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IInstanceKeyName value) { return CompareTo(new InstanceKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(InstanceKeyName left, InstanceKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(InstanceKeyName left, InstanceKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(InstanceKeyName left, InstanceKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(InstanceKeyName left, InstanceKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(InstanceKeyName left, InstanceKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(InstanceKeyName left, InstanceKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return InstanceName.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (InstanceName is string) { return InstanceName; }
            else { return string.Empty; }
        }
    }
}
