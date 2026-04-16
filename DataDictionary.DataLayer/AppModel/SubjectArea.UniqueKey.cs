using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Model Subject Area Unique Key
    /// </summary>
    public interface ISubjectAreaUniqueKey : IKey
    {
        /// <summary>
        /// Title of the Subject Area
        /// </summary>
        String? SubjectAreaTitle { get; }

    }

    /// <summary>
    /// Implementation for Model Subject Area Unique Key
    /// </summary>
    public class SubjectAreaUniqueKey : ISubjectAreaUniqueKey,
        IKeyComparable<ISubjectAreaUniqueKey>, IKeyComparable<SubjectAreaUniqueKey>
    {
        /// <inheritdoc/>
        public String SubjectAreaTitle { get; init; } = string.Empty;


        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return !String.IsNullOrEmpty(SubjectAreaTitle); } }

        /// <summary>
        /// Constrictor for Model Subject Area Unique Key
        /// </summary>
        /// <param name="source"></param>
        public SubjectAreaUniqueKey(ISubjectAreaUniqueKey source) : base()
        {
            if (source.SubjectAreaTitle is String) { SubjectAreaTitle = source.SubjectAreaTitle; }
            else { SubjectAreaTitle = String.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(SubjectAreaUniqueKey? other)
        {
            return
                other is SubjectAreaUniqueKey &&
                !String.IsNullOrEmpty(SubjectAreaTitle) &&
                !String.IsNullOrEmpty(other.SubjectAreaTitle) &&
                SubjectAreaTitle.Equals(other.SubjectAreaTitle, KeyExtension.CompareString);

        }

        /// <inheritdoc/>
        public virtual Boolean Equals(ISubjectAreaUniqueKey? other)
        { return other is ISubjectAreaUniqueKey value && Equals(new SubjectAreaUniqueKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ISubjectAreaUniqueKey value && Equals(new SubjectAreaUniqueKey(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(SubjectAreaUniqueKey? other)
        {
            if (other is SubjectAreaUniqueKey value)
            { return string.Compare(SubjectAreaTitle, value.SubjectAreaTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(ISubjectAreaUniqueKey? other)
        { if (other is ISubjectAreaUniqueKey value) { return CompareTo(new SubjectAreaUniqueKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is ISubjectAreaUniqueKey value) { return CompareTo(new SubjectAreaUniqueKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(SubjectAreaUniqueKey left, SubjectAreaUniqueKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(SubjectAreaUniqueKey left, SubjectAreaUniqueKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(SubjectAreaUniqueKey left, SubjectAreaUniqueKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(SubjectAreaUniqueKey left, SubjectAreaUniqueKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(SubjectAreaUniqueKey left, SubjectAreaUniqueKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(SubjectAreaUniqueKey left, SubjectAreaUniqueKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return SubjectAreaTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return SubjectAreaTitle; }


    }
}
