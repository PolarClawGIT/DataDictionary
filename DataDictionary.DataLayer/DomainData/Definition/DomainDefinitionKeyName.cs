using DataDictionary.Resource;

namespace DataDictionary.DataLayer.DomainData.Definition
{
    /// <summary>
    /// Interface for the unique Name of a Definition.
    /// </summary>
    public interface IDomainDefinitionKeyName : IKey
    {
        /// <summary>
        /// Title of the Domain Definition (aka Name of the Definition)
        /// </summary>
        String? DefinitionTitle { get; }
    }

    /// <summary>
    /// Implementation for the unique Name of a Definition.
    /// </summary>
    public class DomainDefinitionKeyName : IDomainDefinitionKeyName, IKeyComparable<IDomainDefinitionKeyName>
    {
        /// <inheritdoc/>
        public String DefinitionTitle { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Definition Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public DomainDefinitionKeyName(IDomainDefinitionKeyName source) : base()
        {
            if (source.DefinitionTitle is string) { DefinitionTitle = source.DefinitionTitle; }
            else { DefinitionTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IDomainDefinitionKeyName? other)
        {
            return
                other is IDomainDefinitionKeyName &&
                !string.IsNullOrEmpty(DefinitionTitle) &&
                !string.IsNullOrEmpty(other.DefinitionTitle) &&
                DefinitionTitle.Equals(other.DefinitionTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDomainDefinitionKeyName value && Equals(new DomainDefinitionKeyName(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IDomainDefinitionKeyName? other)
        {
            if (other is DomainDefinitionKeyName value)
            { return string.Compare(DefinitionTitle, value.DefinitionTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IDomainDefinitionKeyName value) { return CompareTo(new DomainDefinitionKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DomainDefinitionKeyName left, DomainDefinitionKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainDefinitionKeyName left, DomainDefinitionKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DomainDefinitionKeyName left, DomainDefinitionKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DomainDefinitionKeyName left, DomainDefinitionKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DomainDefinitionKeyName left, DomainDefinitionKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DomainDefinitionKeyName left, DomainDefinitionKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return DefinitionTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (DefinitionTitle is string) { return DefinitionTitle; }
            else { return string.Empty; }
        }

    }
}
