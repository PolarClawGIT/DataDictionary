using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppGeneral
{
    /// <summary>
    /// Interface for the Help Subject name key.
    /// </summary>
    public interface IHelpSubjectKeyName : IKey
    {
        /// <summary>
        /// Title/Subject of the Help Document.
        /// </summary>
        String? HelpSubject { get; }
    }

    /// <summary>
    /// Implementation of the Help Subject name key.
    /// </summary>
    public class HelpSubjectKeyName : IHelpSubjectKeyName,
        IKeyComparable<IHelpSubjectKeyName>, IKeyComparable<HelpSubjectKeyName>
    {
        /// <inheritdoc/>
        public String HelpSubject { get; init; } = String.Empty;

        /// <summary>
        /// Constructor for the Help Subject name key.
        /// </summary>
        /// <param name="source"></param>
        public HelpSubjectKeyName(IHelpSubjectKeyName source) : base()
        {
            if (source.HelpSubject is string) { HelpSubject = source.HelpSubject; }
            else { HelpSubject = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(HelpSubjectKeyName? other)
        {
            return
                other is HelpSubjectKeyName &&
                !string.IsNullOrEmpty(HelpSubject) &&
                !string.IsNullOrEmpty(other.HelpSubject) &&
                HelpSubject.Equals(other.HelpSubject, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IHelpSubjectKeyName? other)
        { return other is IHelpSubjectKeyName value && Equals(new HelpSubjectKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IHelpSubjectKeyName value && Equals(new HelpSubjectKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(HelpSubjectKeyName? other)
        {
            if (other is HelpSubjectKeyName value)
            { return string.Compare(HelpSubject, value.HelpSubject, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IHelpSubjectKeyName? other)
        { if (other is IHelpSubjectKeyName value) { return CompareTo(new HelpSubjectKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IHelpSubjectKeyName value) { return CompareTo(new HelpSubjectKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(HelpSubjectKeyName left, HelpSubjectKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(HelpSubjectKeyName left, HelpSubjectKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(HelpSubjectKeyName left, HelpSubjectKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(HelpSubjectKeyName left, HelpSubjectKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(HelpSubjectKeyName left, HelpSubjectKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(HelpSubjectKeyName left, HelpSubjectKeyName right)
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
