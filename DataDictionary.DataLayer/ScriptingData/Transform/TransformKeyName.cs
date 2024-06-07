using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Transform
{
    /// <summary>
    /// Interface for the Scripting Transform Name
    /// </summary>
    [Obsolete("To be removed", true)]
    public interface ITransformKeyName : IKey
    {
        /// <summary>
        /// Title of the Scripting Transform.
        /// </summary>
        String? TransformTitle { get; }
    }

    /// <summary>
    /// Implementation for Scripting Transform Name
    /// </summary>
    [Obsolete("To be removed", true)]
    public class TransformKeyName : ITransformKeyName, IKeyComparable<ITransformKeyName>
    {
        /// <inheritdoc/>
        public String TransformTitle { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for a Scripting Transform Name
        /// </summary>
        protected internal TransformKeyName() : base() { }

        /// <summary>
        /// Constructor for the Scripting Transform Name
        /// </summary>
        /// <param name="source"></param>
        public TransformKeyName(ITransformKeyName source) : base()
        {
            if (source.TransformTitle is string) { TransformTitle = source.TransformTitle; }
            else { TransformTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(ITransformKeyName? other)
        {
            return
                other is ITransformKeyName &&
                !string.IsNullOrEmpty(TransformTitle) &&
                !string.IsNullOrEmpty(other.TransformTitle) &&
                TransformTitle.Equals(other.TransformTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is ITransformKeyName value && Equals(new TransformKeyName(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(ITransformKeyName? other)
        {
            if (other is TransformKeyName value)
            { return string.Compare(TransformTitle, value.TransformTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is ITransformKeyName value) { return CompareTo(new TransformKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(TransformKeyName left, TransformKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(TransformKeyName left, TransformKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(TransformKeyName left, TransformKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(TransformKeyName left, TransformKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(TransformKeyName left, TransformKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(TransformKeyName left, TransformKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return TransformTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return TransformTitle; }
    }
}
