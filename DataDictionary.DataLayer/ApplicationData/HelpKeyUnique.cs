using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData
{
    /// <summary>
    /// Unique Key for Help Documents, by NameSpace
    /// </summary>
    public interface IHelpKeyUnique
    {
        /// <summary>
        /// Key to reference a Help Document by Name Space
        /// </summary>
        String? NameSpace { get; }
    }

    /// <summary>
    /// Unique Key for Help Documents, by NameSpace
    /// </summary>
    public class HelpKeyUnique : IHelpKeyUnique, IEquatable<HelpKeyUnique>, IComparable<HelpKeyUnique>, IComparable
    {
        /// <inheritdoc/>
        public String NameSpace { get; init; } = String.Empty;

        /// <summary>
        /// Create a Help Key by NameSpace that implement the Unique Key
        /// </summary>
        /// <param name="source"></param>
        public HelpKeyUnique(IHelpKeyUnique source) : base()
        {
            if (source.NameSpace is String) { NameSpace = source.NameSpace; }
            else { NameSpace = String.Empty; }
        }

        /// <summary>
        /// Create a Help Key from a Object. Uses the Objects Full Name.
        /// </summary>
        /// <param name="source"></param>
        public HelpKeyUnique(Object source) : base()
        {
            if (source.GetType().FullName is String value) { NameSpace = value; }
            else { NameSpace = String.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(HelpKeyUnique? other)
        {
            return (
                other is HelpKeyUnique &&
                !String.IsNullOrEmpty(NameSpace) &&
                !String.IsNullOrEmpty(other.NameSpace) &&
                NameSpace.Equals(other.NameSpace, ModelFactory.CompareString));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IHelpKeyUnique value && this.Equals(new HelpKeyUnique(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(HelpKeyUnique? other)
        {
            if (other is HelpKeyUnique value)
            { return String.Compare(NameSpace, value.NameSpace, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IHelpKeyUnique value) { return this.CompareTo(new HelpKeyUnique(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(HelpKeyUnique left, HelpKeyUnique right)
        { return left.Equals(right); }

        public static bool operator !=(HelpKeyUnique left, HelpKeyUnique right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(HelpKeyUnique left, HelpKeyUnique right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(HelpKeyUnique left, HelpKeyUnique right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(HelpKeyUnique left, HelpKeyUnique right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(HelpKeyUnique left, HelpKeyUnique right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(NameSpace); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (NameSpace is String) { return NameSpace; }
            else { return String.Empty; }
        }
    }
}
