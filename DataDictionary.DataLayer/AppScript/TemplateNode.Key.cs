using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Primary Key for the Scripting Template Attribute.
    /// </summary>
    public interface ITemplateNodeKey : IKey
    {
        /// <summary>
        /// Template Node Id of the Scripting.
        /// </summary>
        Guid? NodeId { get; }
    }

    /// <summary>
    /// Implementation for the Primary Key for the Scripting Template Attribute.
    /// </summary>
    public class TemplateNodeKey : ITemplateNodeKey,
        IKeyEquality<ITemplateNodeKey>, IKeyEquality<TemplateNodeKey>
    {
        /// <inheritdoc/>
        public Guid? NodeId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Primary Key of the Scripting Template Node.
        /// </summary>
        public TemplateNodeKey() :base ()
        { }

        /// <summary>
        /// Constructor for the Primary Key of the Scripting Template Node.
        /// </summary>
        /// <param name="source"></param>
        public TemplateNodeKey(ITemplateNodeKey source) : this()
        {
            if (source.NodeId is Guid) { NodeId = source.NodeId; }
            else { NodeId = Guid.Empty; }
        }

        /// <summary>
        /// Constructor for the Primary Key of the Scripting Template Node.
        /// </summary>
        /// <param name="source"></param>
        public TemplateNodeKey(ITemplateNodeOwnerKey source): this()
        {
            if (source.NodeOwnerId is Guid) { NodeId = source.NodeOwnerId; }
            else { NodeId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(TemplateNodeKey? other)
        { return other is TemplateNodeKey key && key.NodeId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(NodeId, other.NodeId); }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateNodeKey? other)
        { return other is ITemplateNodeKey value && Equals(new TemplateNodeKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ITemplateNodeKey value && Equals(new TemplateNodeKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(TemplateNodeKey left, TemplateNodeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(TemplateNodeKey left, TemplateNodeKey right)
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
