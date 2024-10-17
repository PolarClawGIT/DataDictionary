using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Interface for the unique Login of a Security Object.
    /// </summary>
    public interface IObjectKeyName : IKey
    {
        /// <summary>
        /// Login of the Security Object
        /// </summary>
        String? ObjectTitle { get; }
    }

    /// <summary>
    /// Implementation of the unique Login of a Security Object.
    /// </summary>
    public class ObjectKeyName : IObjectKeyName,
        IKeyComparable<IObjectKeyName>, IKeyComparable<ObjectKeyName>
    {
        /// <inheritdoc/>
        public String ObjectTitle { get; init; } = String.Empty;

        /// <summary>
        /// Constructor for the Security Object Name Key.
        /// </summary>
        /// <param name="source"></param>
        public ObjectKeyName(IObjectKeyName source) : base()
        {
            if (source.ObjectTitle is string) { ObjectTitle = source.ObjectTitle; }
            else { ObjectTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(ObjectKeyName? other)
        {
            return
                other is ObjectKeyName &&
                !string.IsNullOrEmpty(ObjectTitle) &&
                !string.IsNullOrEmpty(other.ObjectTitle) &&
                ObjectTitle.Equals(other.ObjectTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IObjectKeyName? other)
        { return other is IObjectKeyName value && Equals(new ObjectKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IObjectKeyName value && Equals(new ObjectKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(ObjectKeyName? other)
        {
            if (other is ObjectKeyName value)
            { return string.Compare(ObjectTitle, value.ObjectTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IObjectKeyName? other)
        { if (other is IObjectKeyName value) { return CompareTo(new ObjectKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IObjectKeyName value) { return CompareTo(new ObjectKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(ObjectKeyName left, ObjectKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ObjectKeyName left, ObjectKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(ObjectKeyName left, ObjectKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(ObjectKeyName left, ObjectKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(ObjectKeyName left, ObjectKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(ObjectKeyName left, ObjectKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return ObjectTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (ObjectTitle is String) { return ObjectTitle; }
            else { return String.Empty; }
        }
    }
}
