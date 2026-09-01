using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting TemplateObject Name Key
    /// </summary>
    public interface ITemplateObjectKeyName: IKey
    {
        /// <summary>
        ///  Scope of the Object being referenced.
        /// </summary>
        ScopeType ObjectScope { get; }

        /// <summary>
        /// The Object Name and Path being referenced.
        /// </summary>
        String? ObjectPath { get; }
    }

    /// <summary>
    /// Implementation for the Scripting TemplateObject Name Key
    /// </summary>
    public class TemplateObjectKeyName : ITemplateObjectKeyName,
            IKeyComparable<ITemplateObjectKeyName>, IKeyComparable<TemplateObjectKeyName>
    {
        /// <inheritdoc/>
        public ScopeType ObjectScope { get; init; } = ScopeType.Null;

        /// <inheritdoc/>
        public String ObjectPath { get; init; } = string.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return ObjectScope is ScopeType.Null || !String.IsNullOrEmpty(ObjectPath); } }

        /// <summary>
        /// Constructor for Scripting TemplateObject Name Key
        /// </summary>
        protected TemplateObjectKeyName() : base() { }

        /// <summary>
        /// Constructor for Scripting TemplateObject Name Key
        /// </summary>
        public TemplateObjectKeyName(ITemplateObjectKeyName source) : this()
        {
            ObjectScope = source.ObjectScope;
            ObjectPath = source.ObjectPath??String.Empty;
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(TemplateObjectKeyName? other)
        {
            return
                other is TemplateObjectKeyName &&
                !String.IsNullOrEmpty(ObjectPath) &&
                !String.IsNullOrEmpty(other.ObjectPath) &&
                ObjectScope.Equals(other.ObjectScope) &&
                ObjectPath.Equals(other.ObjectPath, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateObjectKeyName? other)
        { return other is ITemplateObjectKeyName value && Equals(new TemplateObjectKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ITemplateObjectKeyName value && Equals(new TemplateObjectKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(TemplateObjectKeyName? other)
        {
            if (other is null) { return 1; }
            else { return String.Compare(ObjectPath, other.ObjectPath, true); }
        }

        /// <inheritdoc/>
        public Int32 CompareTo(ITemplateObjectKeyName? other)
        { if (other is ITemplateObjectKeyName value) { return CompareTo(new TemplateObjectKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public Int32 CompareTo(object? obj)
        { if (obj is ITemplateObjectKeyName value) { return CompareTo(new TemplateObjectKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(TemplateObjectKeyName left, TemplateObjectKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(TemplateObjectKeyName left, TemplateObjectKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(TemplateObjectKeyName left, TemplateObjectKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(TemplateObjectKeyName left, TemplateObjectKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(TemplateObjectKeyName left, TemplateObjectKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(TemplateObjectKeyName left, TemplateObjectKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ObjectPath.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return ObjectPath; }
    }
}
