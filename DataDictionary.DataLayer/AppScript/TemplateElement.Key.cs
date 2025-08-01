using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Primary Key for the Scripting Template Element.
    /// </summary>
    public interface ITemplateElementKey : IKey
    {
        /// <summary>
        /// Template Node Id of the Scripting Template Node.
        /// </summary>
        Guid? NodeId { get; }
    }

    /// <summary>
    /// Implementation for the Primary Key for the Scripting Template Element.
    /// </summary>
    public class TemplateElementKey : ITemplateElementKey,
        IKeyEquality<ITemplateElementKey>, IKeyEquality<TemplateElementKey>
    {
        /// <inheritdoc/>
        public Guid? NodeId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Primary Key of the Scripting Template Node.
        /// </summary>
        /// <param name="source"></param>
        public TemplateElementKey(ITemplateElementKey source) : base()
        {
            if (source.NodeId is Guid) { NodeId = source.NodeId; }
            else { NodeId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(TemplateElementKey? other)
        { return other is TemplateElementKey key && key.NodeId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(NodeId, other.NodeId); }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateElementKey? other)
        { return other is ITemplateElementKey value && Equals(new TemplateElementKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ITemplateElementKey value && Equals(new TemplateElementKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(TemplateElementKey left, TemplateElementKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(TemplateElementKey left, TemplateElementKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (NodeId is Guid) { return NodeId.GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
