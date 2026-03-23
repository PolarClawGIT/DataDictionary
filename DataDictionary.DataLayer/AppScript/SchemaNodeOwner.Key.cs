using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting SchemaNodeOwner Key
    /// </summary>
    public interface ISchemaNodeOwnerKey : ISchemaNodeKey
    {
        /// <summary>
        /// Node Owner ID for the Scripting SchemaNode.
        /// </summary>
        Guid? NodeOwnerId { get; }
    }

    /// <summary>
    /// Implementation for the Scripting SchemaNodeOwner Key
    /// </summary>
    public class SchemaNodeOwnerKey : ISchemaNodeOwnerKey,
        IKeyEquality<SchemaNodeOwnerKey>,
        IKeyEquality<ISchemaNodeOwnerKey>
    {
        /// <inheritdoc/>
        public Guid? NodeId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public Guid? NodeOwnerId { get; set; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Blank/Empty SchemaNode Key
        /// </summary>
        /// <remarks>Empty Key is never equal to anything.</remarks>
        public SchemaNodeOwnerKey()
        { }

        /// <summary>
        /// Constructor for the SchemaNode Key
        /// </summary>
        /// <param name="source"></param>
        public SchemaNodeOwnerKey(ISchemaNodeKey source) : base()
        {
            if (source.NodeId is Guid) { NodeId = source.NodeId; }
            else { NodeId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(SchemaNodeOwnerKey? other)
        {
            return other is SchemaNodeOwnerKey key &&
                key.NodeId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(NodeId, key.NodeId) &&
                key.NodeOwnerId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(NodeOwnerId, key.NodeOwnerId);
        }

        /// <inheritdoc/>
        public Boolean Equals(ISchemaNodeOwnerKey? other)
        { return other is ISchemaNodeOwnerKey key && Equals(new SchemaNodeOwnerKey(key)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ISchemaNodeOwnerKey key && Equals(new SchemaNodeOwnerKey(key)); }

        /// <inheritdoc/>
        public static Boolean operator ==(SchemaNodeOwnerKey left, SchemaNodeOwnerKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(SchemaNodeOwnerKey left, SchemaNodeOwnerKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(NodeId, NodeOwnerId); }


        #endregion
    }
}
