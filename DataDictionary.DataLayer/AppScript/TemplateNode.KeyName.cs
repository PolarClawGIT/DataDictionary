using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppScript
{

    /// <summary>
    /// Interface for the unique Name of a TemplateNode.
    /// </summary>
    public interface ITemplateNodeKeyName : IKey
    {
        /// <summary>
        /// Title of the Scripting TemplateNode
        /// </summary>
        String? NodeName { get; }
    }

    /// <summary>
    /// Implementation for the unique Name of a TemplateNode.
    /// </summary>
    public class TemplateNodeKeyName : ITemplateNodeKeyName,
        IKeyComparable<ITemplateNodeKeyName>, IKeyComparable<TemplateNodeKeyName>
    {
        /// <inheritdoc/>
        public String NodeName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the TemplateNode Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public TemplateNodeKeyName(ITemplateNodeKeyName source) : base()
        {
            if (source.NodeName is string) { NodeName = source.NodeName; }
            else { NodeName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(TemplateNodeKeyName? other)
        {
            return
                other is TemplateNodeKeyName &&
                !string.IsNullOrEmpty(NodeName) &&
                !string.IsNullOrEmpty(other.NodeName) &&
                NodeName.Equals(other.NodeName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(ITemplateNodeKeyName? other)
        { return other is ITemplateNodeKeyName value && Equals(new TemplateNodeKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ITemplateNodeKeyName value && Equals(new TemplateNodeKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(TemplateNodeKeyName? other)
        {
            if (other is TemplateNodeKeyName value)
            { return string.Compare(NodeName, value.NodeName, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(ITemplateNodeKeyName? other)
        { if (other is ITemplateNodeKeyName value) { return CompareTo(new TemplateNodeKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is ITemplateNodeKeyName value) { return CompareTo(new TemplateNodeKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(TemplateNodeKeyName left, TemplateNodeKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(TemplateNodeKeyName left, TemplateNodeKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(TemplateNodeKeyName left, TemplateNodeKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(TemplateNodeKeyName left, TemplateNodeKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(TemplateNodeKeyName left, TemplateNodeKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(TemplateNodeKeyName left, TemplateNodeKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return NodeName.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (NodeName is string) { return NodeName; }
            else { return string.Empty; }
        }
    }
}
