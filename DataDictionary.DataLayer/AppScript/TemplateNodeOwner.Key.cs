using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Node Owner Key for the Scripting Template Node.
    /// </summary>
    public interface ITemplateNodeOwnerKey: IKey
    {
        /// <summary>
        /// Template Owner Node Id of the Scripting.
        /// </summary>
        Guid? NodeOwnerId { get; }
    }

    /// <summary>
    /// Implementation for the Node Owner Key for the Scripting Template Node.
    /// </summary>
    public class TemplateNodeOwnerKey : ITemplateNodeOwnerKey,
        IKeyEquality<ITemplateNodeOwnerKey>, IKeyEquality<TemplateNodeOwnerKey>
    {
        /// <inheritdoc/>
        public Guid? NodeOwnerId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Node Owner Key of the Scripting Template Node.
        /// </summary>
        public TemplateNodeOwnerKey() : base()
        { }

        /// <summary>
        /// Constructor for the Node Owner Key of the Scripting Template Node.
        /// </summary>
        /// <param name="source"></param>
        public TemplateNodeOwnerKey(ITemplateNodeOwnerKey source) : this()
        {
            if (source.NodeOwnerId is Guid) { NodeOwnerId = source.NodeOwnerId; }
            else { NodeOwnerId = Guid.Empty; }
        }

        /// <summary>
        /// Constructor for the Node Owner Key of the Scripting Template Node.
        /// </summary>
        /// <param name="source"></param>
        public TemplateNodeOwnerKey(ITemplateNodeKey source): this()
        {
            if (source.NodeId is Guid) { NodeOwnerId = source.NodeId; }
            else { NodeOwnerId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(TemplateNodeOwnerKey? other)
        { return other is TemplateNodeOwnerKey key && key.NodeOwnerId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(NodeOwnerId, other.NodeOwnerId); }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateNodeOwnerKey? other)
        { return other is ITemplateNodeOwnerKey value && Equals(new TemplateNodeOwnerKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateNodeKey? other)
        { return other is ITemplateNodeKey key && key.NodeId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(NodeOwnerId, other.NodeId); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ITemplateNodeOwnerKey value && Equals(new TemplateNodeOwnerKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(TemplateNodeOwnerKey left, TemplateNodeOwnerKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(TemplateNodeOwnerKey left, TemplateNodeOwnerKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (NodeOwnerId is Guid) { return NodeOwnerId.GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
