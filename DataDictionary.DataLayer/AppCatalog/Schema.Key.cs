using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Catalog Schema Key.
    /// </summary>
    public interface ISchemaKey : IKey
    {
        /// <summary>
        /// Application ID for the Schema.
        /// </summary>
        Guid? SchemaId { get; }
    }

    /// <summary>
    /// Implementation for the Catalog Schema Key.
    /// </summary>
    public class SchemaKey : ISchemaKey,
        IKeyEquality<ISchemaKey>, IKeyEquality<SchemaKey>
    {
        /// <inheritdoc/>
        public Guid? SchemaId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return SchemaId.HasValue && SchemaId != Guid.Empty; } }

        /// <summary>
        /// Constructor for the Schema Key.
        /// </summary>
        /// <param name="source"></param>
        public SchemaKey(ISchemaKey source) : base()
        {
            if (source.SchemaId is Guid value) { SchemaId = value; }
            else { SchemaId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(SchemaKey? other)
        {
            return other is SchemaKey key
                && SchemaId.HasValue && SchemaId != Guid.Empty
                && key.SchemaId.HasValue && key.SchemaId != Guid.Empty
                && EqualityComparer<Guid?>.Default.Equals(SchemaId, other.SchemaId);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(ISchemaKey? other)
        { return other is ISchemaKey value && Equals(new SchemaKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is ISchemaKey value && Equals(new SchemaKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(SchemaKey left, SchemaKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(SchemaKey left, SchemaKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(SchemaId); }
        #endregion
    }
}
