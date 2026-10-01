using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting TemplateObject Name Key
    /// </summary>
    public interface ITemplateObjectKey: IKey
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
    public class TemplateObjectKey : ITemplateObjectKey,
            IKeyComparable<ITemplateObjectKey>, IKeyComparable<TemplateObjectKey>
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
        protected TemplateObjectKey() : base() { }

        /// <summary>
        /// Constructor for Scripting TemplateObject Name Key
        /// </summary>
        public TemplateObjectKey(ITemplateObjectKey source) : this()
        {
            ObjectScope = source.ObjectScope;
            ObjectPath = source.ObjectPath??String.Empty;
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(TemplateObjectKey? other)
        {
            return
                other is TemplateObjectKey &&
                !String.IsNullOrEmpty(ObjectPath) &&
                !String.IsNullOrEmpty(other.ObjectPath) &&
                ObjectScope.Equals(other.ObjectScope) &&
                ObjectPath.Equals(other.ObjectPath, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateObjectKey? other)
        { return other is ITemplateObjectKey value && Equals(new TemplateObjectKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ITemplateObjectKey value && Equals(new TemplateObjectKey(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(TemplateObjectKey? other)
        {
            if (other is null) { return 1; }
            else { return String.Compare(ObjectPath, other.ObjectPath, true); }
        }

        /// <inheritdoc/>
        public Int32 CompareTo(ITemplateObjectKey? other)
        { if (other is ITemplateObjectKey value) { return CompareTo(new TemplateObjectKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public Int32 CompareTo(object? obj)
        { if (obj is ITemplateObjectKey value) { return CompareTo(new TemplateObjectKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(TemplateObjectKey left, TemplateObjectKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(TemplateObjectKey left, TemplateObjectKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(TemplateObjectKey left, TemplateObjectKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(TemplateObjectKey left, TemplateObjectKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(TemplateObjectKey left, TemplateObjectKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(TemplateObjectKey left, TemplateObjectKey right)
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
