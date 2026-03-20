using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting SchemaDefinition Key
    /// </summary>
    public interface ISchemaDefinitionKey : IKey
    {
        /// <summary>
        /// SchemaDefinition ID for the Scripting SchemaDefinition.
        /// </summary>
        Guid? SchemaId { get; }
    }

    /// <summary>
    /// Implementation for the Scripting SchemaDefinition Key
    /// </summary>
    public class SchemaDefinitionKey : ISchemaDefinitionKey,
        IKeyEquality<SchemaDefinitionKey>,
        IKeyEquality<ISchemaDefinitionKey>
    {
        /// <inheritdoc/>
        public Guid? SchemaId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Blank/Empty SchemaDefinition Key
        /// </summary>
        /// <remarks>Empty Key is never equal to anything.</remarks>
        public SchemaDefinitionKey()
        { }

        /// <summary>
        /// Constructor for the SchemaDefinition Key
        /// </summary>
        /// <param name="source"></param>
        public SchemaDefinitionKey(ISchemaDefinitionKey source) : base()
        {
            if (source.SchemaId is Guid) { SchemaId = source.SchemaId; }
            else { SchemaId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(SchemaDefinitionKey? other)
        { return other is SchemaDefinitionKey key && key.SchemaId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(SchemaId, key.SchemaId); }

        /// <inheritdoc/>
        public Boolean Equals(ISchemaDefinitionKey? other)
        { return other is ISchemaDefinitionKey key && Equals(new SchemaDefinitionKey(key)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ISchemaDefinitionKey key && Equals(new SchemaDefinitionKey(key)); }

        /// <inheritdoc/>
        public static Boolean operator ==(SchemaDefinitionKey left, SchemaDefinitionKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(SchemaDefinitionKey left, SchemaDefinitionKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(SchemaId); }


        #endregion
    }
}
