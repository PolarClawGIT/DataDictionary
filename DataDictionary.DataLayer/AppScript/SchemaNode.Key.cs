using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting SchemaNode Key
    /// </summary>
    public interface ISchemaNodeKey : IKey
    {
        /// <summary>
        /// Node ID for the Scripting SchemaNode.
        /// </summary>
        Guid? NodeId { get; }
    }

    /// <summary>
    /// Implementation for the Scripting SchemaNode Key
    /// </summary>
    public class SchemaNodeKey : ISchemaNodeKey,
        IKeyEquality<SchemaNodeKey>,
        IKeyEquality<ISchemaNodeKey>
    {
        /// <inheritdoc/>
        public Guid? NodeId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Blank/Empty SchemaNode Key
        /// </summary>
        /// <remarks>Empty Key is never equal to anything.</remarks>
        public SchemaNodeKey()
        { }

        /// <summary>
        /// Constructor for the SchemaNode Key
        /// </summary>
        /// <param name="source"></param>
        public SchemaNodeKey(ISchemaNodeKey source) : base()
        {
            if (source.NodeId is Guid) { NodeId = source.NodeId; }
            else { NodeId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(SchemaNodeKey? other)
        { return other is SchemaNodeKey key && key.NodeId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(NodeId, key.NodeId); }

        /// <inheritdoc/>
        public Boolean Equals(ISchemaNodeKey? other)
        { return other is ISchemaNodeKey key && Equals(new SchemaNodeKey(key)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ISchemaNodeKey key && Equals(new SchemaNodeKey(key)); }

        /// <inheritdoc/>
        public static Boolean operator ==(SchemaNodeKey left, SchemaNodeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(SchemaNodeKey left, SchemaNodeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(NodeId); }


        #endregion
    }
}
