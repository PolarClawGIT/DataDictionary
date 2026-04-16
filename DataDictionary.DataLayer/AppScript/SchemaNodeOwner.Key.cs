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
    public class SchemaNodeOwnerKey : SchemaNodeKey,
        ISchemaNodeOwnerKey, IKeyEquality<SchemaNodeOwnerKey>, IKeyEquality<ISchemaNodeOwnerKey>
    {
        /// <inheritdoc/>
        public Guid? NodeOwnerId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public override Boolean HasValue { get { return base.HasValue && NodeOwnerId.HasValue && NodeOwnerId != Guid.Empty; } }

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
        public SchemaNodeOwnerKey(ISchemaNodeKey source) : base(source)
        { }

        /// <summary>
        /// Constructor for the SchemaNode Key
        /// </summary>
        /// <param name="source"></param>
        public SchemaNodeOwnerKey(ISchemaNodeOwnerKey source) : base(source)
        {
            if (source.NodeOwnerId is Guid) { NodeId = source.NodeOwnerId; }
            else { NodeOwnerId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(SchemaNodeOwnerKey? other)
        {
            return other is SchemaNodeOwnerKey key
                && base.Equals(key)
                && NodeOwnerId.HasValue && NodeOwnerId != Guid.Empty
                && key.NodeOwnerId.HasValue && key.NodeOwnerId != Guid.Empty
                && EqualityComparer<Guid?>.Default.Equals(NodeOwnerId, other.NodeOwnerId);
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
