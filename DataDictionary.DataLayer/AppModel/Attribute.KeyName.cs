using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the unique Name of a Attribute.
    /// </summary>
    public interface IAttributeKeyName : IKey
    {
        /// <summary>
        /// Title of the Model Attribute (aka Name of the Attribute)
        /// </summary>
        String? AttributeTitle { get; }
    }

    /// <summary>
    /// Implementation for the unique Name of a Attribute.
    /// </summary>
    public class AttributeKeyName : IAttributeKeyName,
        IKeyComparable<IAttributeKeyName>, IKeyComparable<AttributeKeyName>
    {
        /// <inheritdoc/>
        public String AttributeTitle { get; init; } = string.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return !String.IsNullOrEmpty(AttributeTitle); } }

        /// <summary>
        /// Constructor for the Attribute Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public AttributeKeyName(IAttributeKeyName source) : base()
        {
            if (source.AttributeTitle is string) { AttributeTitle = source.AttributeTitle; }
            else { AttributeTitle = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Attribute Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public AttributeKeyName(ITableColumnKeyName source) : base()
        {
            if (source.ColumnName is string) { AttributeTitle = source.ColumnName; }
            else { AttributeTitle = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Attribute Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public AttributeKeyName(IRoutineParameterKeyName source) : base()
        {
            if (source.ParameterName is string) { AttributeTitle = source.ParameterName.Replace("@", ""); }
            else { AttributeTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(AttributeKeyName? other)
        {
            return
                other is AttributeKeyName &&
                !string.IsNullOrEmpty(AttributeTitle) &&
                !string.IsNullOrEmpty(other.AttributeTitle) &&
                AttributeTitle.Equals(other.AttributeTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IAttributeKeyName? other)
        { return other is IAttributeKeyName value && Equals(new AttributeKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IAttributeKeyName value && Equals(new AttributeKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(AttributeKeyName? other)
        {
            if (other is AttributeKeyName value)
            { return string.Compare(AttributeTitle, value.AttributeTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IAttributeKeyName? other)
        { if (other is IAttributeKeyName value) { return CompareTo(new AttributeKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IAttributeKeyName value) { return CompareTo(new AttributeKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(AttributeKeyName left, AttributeKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(AttributeKeyName left, AttributeKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(AttributeKeyName left, AttributeKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(AttributeKeyName left, AttributeKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(AttributeKeyName left, AttributeKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(AttributeKeyName left, AttributeKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return AttributeTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (AttributeTitle is string) { return AttributeTitle; }
            else { return string.Empty; }
        }
    }
}
