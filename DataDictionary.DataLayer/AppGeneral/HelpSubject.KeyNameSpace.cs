using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppGeneral
{
    /// <summary>
    /// Unique Key for Help Documents, by NameSpace
    /// </summary>
    public interface IHelpSubjectKeyNameSpace : IKey
    {
        /// <summary>
        /// Key to reference a Help Document by Name Space
        /// </summary>
        String? NameSpace { get; }
    }

    /// <summary>
    /// Unique Key for Help Documents, by NameSpace
    /// </summary>
    public class HelpSubjectKeyNameSpace : IHelpSubjectKeyNameSpace,
        IKeyComparable<IHelpSubjectKeyNameSpace>,
        IKeyComparable<HelpSubjectKeyNameSpace>
    {
        /// <inheritdoc/>
        public String NameSpace { get; init; } = string.Empty;

        /// <summary>
        /// Create a Help Key by NameSpace that implement the Unique Key
        /// </summary>
        /// <param name="source"></param>
        public HelpSubjectKeyNameSpace(IHelpSubjectKeyNameSpace source) : base()
        {
            if (source.NameSpace is string) { NameSpace = source.NameSpace; }
            else { NameSpace = string.Empty; }
        }

        /// <summary>
        /// Create a Help Key from a Object. Uses the Objects Full Name.
        /// </summary>
        /// <param name="source"></param>
        public HelpSubjectKeyNameSpace(object source) : base()
        {
            if (source.GetType().FullName is string value) { NameSpace = value; }
            else { NameSpace = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(HelpSubjectKeyNameSpace? other)
        {
            return
                other is IHelpSubjectKeyNameSpace &&
                !string.IsNullOrEmpty(NameSpace) &&
                !string.IsNullOrEmpty(other.NameSpace) &&
                NameSpace.Equals(other.NameSpace, KeyExtension.CompareString);
        }


        /// <inheritdoc/>
        public virtual Boolean Equals(IHelpSubjectKeyNameSpace? other)
        { return other is IHelpSubjectKeyNameSpace value && Equals(new HelpSubjectKeyNameSpace(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IHelpSubjectKeyNameSpace value && Equals(new HelpSubjectKeyNameSpace(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(HelpSubjectKeyNameSpace? other)
        {
            if (other is HelpSubjectKeyNameSpace value)
            { return string.Compare(NameSpace, value.NameSpace, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IHelpSubjectKeyNameSpace? other)
        {
            if (other is IHelpSubjectKeyNameSpace value)
            { return CompareTo(new HelpSubjectKeyNameSpace(value)); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        {
            if (obj is IHelpSubjectKeyNameSpace value)
            { return CompareTo(new HelpSubjectKeyNameSpace(value)); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public static Boolean operator ==(HelpSubjectKeyNameSpace left, HelpSubjectKeyNameSpace right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(HelpSubjectKeyNameSpace left, HelpSubjectKeyNameSpace right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(HelpSubjectKeyNameSpace left, HelpSubjectKeyNameSpace right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(HelpSubjectKeyNameSpace left, HelpSubjectKeyNameSpace right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(HelpSubjectKeyNameSpace left, HelpSubjectKeyNameSpace right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(HelpSubjectKeyNameSpace left, HelpSubjectKeyNameSpace right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return NameSpace.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (NameSpace is string) { return NameSpace; }
            else { return string.Empty; }
        }


    }
}
