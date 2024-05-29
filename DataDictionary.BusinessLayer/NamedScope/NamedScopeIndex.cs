using DataDictionary.DataLayer;

namespace DataDictionary.BusinessLayer.NamedScope
{
    /// <summary>
    /// Interface for the NameScope Index
    /// </summary>
    public interface INamedScopeIndex : IKey
    {
        /// <summary>
        /// System Id of the Named Scope item.
        /// </summary>
        public Guid NamedScopeId { get; }
    }

    /// <summary>
    /// Implementation for the Named Scope Index
    /// </summary>
    public class NamedScopeIndex : INamedScopeIndex, IKeyComparable<INamedScopeIndex>
    {
        /// <inheritdoc/>
        public Guid NamedScopeId { get; init; } = Guid.Empty;

        internal NamedScopeIndex() : base() { }

        /// <summary>
        /// Constructor for the NameScope Key
        /// </summary>
        /// <param name="source" >A ModelNameSpace</param>
        public NamedScopeIndex(INamedScopeIndex source) : this()
        { NamedScopeId = source.NamedScopeId; }

        /// <summary>
        /// Constructor for the NameScope Key
        /// </summary>
        internal NamedScopeIndex(Guid? source) : this()
        { NamedScopeId = source ?? Guid.Empty; }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(INamedScopeIndex? other)
        {
            return
                other is INamedScopeIndex &&
                NamedScopeId.Equals(other.NamedScopeId);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is INamedScopeIndex value && Equals(new NamedScopeIndex(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(INamedScopeIndex? other)
        {
            if (other is null) { return 1; }
            else { return NamedScopeId.CompareTo(other.NamedScopeId); }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is INamedScopeIndex value) { return CompareTo(new NamedScopeIndex(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(NamedScopeIndex left, NamedScopeIndex right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(NamedScopeIndex left, NamedScopeIndex right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(NamedScopeIndex left, NamedScopeIndex right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(NamedScopeIndex left, NamedScopeIndex right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(NamedScopeIndex left, NamedScopeIndex right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(NamedScopeIndex left, NamedScopeIndex right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return NamedScopeId.GetHashCode(); }
        #endregion

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override String? ToString()
        {
            if (NamedScopeId is Guid value) { return value.ToString(); }
            else { return base.ToString(); }
        }
    }
}
