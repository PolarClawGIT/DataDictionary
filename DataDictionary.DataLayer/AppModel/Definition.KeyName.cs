using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the unique Name of a Definition.
    /// </summary>
    public interface IDefinitionKeyName : IKey
    {
        /// <summary>
        /// Title of the Domain Definition (aka Name of the Definition)
        /// </summary>
        String? DefinitionTitle { get; }
    }

    /// <summary>
    /// Implementation for the unique Name of a Definition.
    /// </summary>
    public class DefinitionKeyName : IDefinitionKeyName,
        IKeyComparable<IDefinitionKeyName>, IKeyComparable<DefinitionKeyName>
    {
        /// <inheritdoc/>
        public String DefinitionTitle { get; init; } = string.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return !String.IsNullOrEmpty(DefinitionTitle); } }

        /// <summary>
        /// Constructor for the Definition Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public DefinitionKeyName(IDefinitionKeyName source) : base()
        {
            if (source.DefinitionTitle is string) { DefinitionTitle = source.DefinitionTitle; }
            else { DefinitionTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(DefinitionKeyName? other)
        {
            return
                other is DefinitionKeyName &&
                !string.IsNullOrEmpty(DefinitionTitle) &&
                !string.IsNullOrEmpty(other.DefinitionTitle) &&
                DefinitionTitle.Equals(other.DefinitionTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IDefinitionKeyName? other)
        { return other is IDefinitionKeyName value && Equals(new DefinitionKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDefinitionKeyName value && Equals(new DefinitionKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(DefinitionKeyName? other)
        {
            if (other is DefinitionKeyName value)
            { return string.Compare(DefinitionTitle, value.DefinitionTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IDefinitionKeyName? other)
        { if (other is IDefinitionKeyName value) { return CompareTo(new DefinitionKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IDefinitionKeyName value) { return CompareTo(new DefinitionKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(DefinitionKeyName left, DefinitionKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DefinitionKeyName left, DefinitionKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(DefinitionKeyName left, DefinitionKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(DefinitionKeyName left, DefinitionKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(DefinitionKeyName left, DefinitionKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(DefinitionKeyName left, DefinitionKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return DefinitionTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (DefinitionTitle is string) { return DefinitionTitle; }
            else { return string.Empty; }
        }




    }
}
