using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppGeneral
{
    /// <summary>
    /// Interface for the Help Subject name key.
    /// </summary>
    public interface IHelpKeyName : IKey
    {
        /// <summary>
        /// Title/Subject of the Help Document.
        /// </summary>
        String? HelpSubject { get; }
    }

    /// <summary>
    /// Implementation of the Help Subject name key.
    /// </summary>
    public class HelpKeyName : IHelpKeyName,
        IKeyComparable<IHelpKeyName>, IKeyComparable<HelpKeyName>
    {
        /// <inheritdoc/>
        public String HelpSubject { get; init; } = String.Empty;

        /// <summary>
        /// Constructor for the Help Subject name key.
        /// </summary>
        /// <param name="source"></param>
        public HelpKeyName(IHelpKeyName source) : base()
        {
            if (source.HelpSubject is string) { HelpSubject = source.HelpSubject; }
            else { HelpSubject = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(HelpKeyName? other)
        {
            return
                other is HelpKeyName &&
                !string.IsNullOrEmpty(HelpSubject) &&
                !string.IsNullOrEmpty(other.HelpSubject) &&
                HelpSubject.Equals(other.HelpSubject, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IHelpKeyName? other)
        { return other is IHelpKeyName value && Equals(new HelpKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IHelpKeyName value && Equals(new HelpKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(HelpKeyName? other)
        {
            if (other is HelpKeyName value)
            { return string.Compare(HelpSubject, value.HelpSubject, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IHelpKeyName? other)
        { if (other is IHelpKeyName value) { return CompareTo(new HelpKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IHelpKeyName value) { return CompareTo(new HelpKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(HelpKeyName left, HelpKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(HelpKeyName left, HelpKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(HelpKeyName left, HelpKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(HelpKeyName left, HelpKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(HelpKeyName left, HelpKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(HelpKeyName left, HelpKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HelpSubject.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (HelpSubject is String) { return HelpSubject; }
            else { return String.Empty; }
        }
    }
}
