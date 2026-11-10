using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppScript
{

    /// <summary>
    /// Interface for the unique Name of a Transform.
    /// </summary>
    public interface ITransformKeyName : IKey
    {
        /// <summary>
        /// Title of the Scripting Transform (aka Name of the Transform)
        /// </summary>
        String? TransformTitle { get; }
    }

    /// <summary>
    /// Implementation for the unique Name of a Transform.
    /// </summary>
    public class TransformKeyName : ITransformKeyName,
        IKeyComparable<ITransformKeyName>, IKeyComparable<TransformKeyName>
    {
        /// <inheritdoc/>
        public String TransformTitle { get; init; } = string.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return !String.IsNullOrEmpty(TransformTitle); } }

        /// <summary>
        /// Constructor for the Transform Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public TransformKeyName(ITransformKeyName source) : base()
        {
            if (source.TransformTitle is string) { TransformTitle = source.TransformTitle; }
            else { TransformTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(TransformKeyName? other)
        {
            return
                other is TransformKeyName &&
                !string.IsNullOrEmpty(TransformTitle) &&
                !string.IsNullOrEmpty(other.TransformTitle) &&
                TransformTitle.Equals(other.TransformTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(ITransformKeyName? other)
        { return other is ITransformKeyName value && Equals(new TransformKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ITransformKeyName value && Equals(new TransformKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(TransformKeyName? other)
        {
            if (other is TransformKeyName value)
            { return string.Compare(TransformTitle, value.TransformTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(ITransformKeyName? other)
        { if (other is ITransformKeyName value) { return CompareTo(new TransformKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is ITransformKeyName value) { return CompareTo(new TransformKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(TransformKeyName left, TransformKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(TransformKeyName left, TransformKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(TransformKeyName left, TransformKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(TransformKeyName left, TransformKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(TransformKeyName left, TransformKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(TransformKeyName left, TransformKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return TransformTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (TransformTitle is string) { return TransformTitle; }
            else { return string.Empty; }
        }
    }
}
