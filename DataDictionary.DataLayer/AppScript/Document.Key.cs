using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Document Key
    /// </summary>
    public interface IDocumentKey : IKey
    {
        /// <summary>
        /// Document ID for the Scripting Document.
        /// </summary>
        Guid? DocumentId { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Document Key
    /// </summary>
    public class DocumentKey : IDocumentKey,
        IKeyEquality<DocumentKey>,
        IKeyEquality<IDocumentKey>
    {
        /// <inheritdoc/>
        public Guid? DocumentId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Blank/Empty Document Key
        /// </summary>
        /// <remarks>Empty Key is never equal to anything.</remarks>
        public DocumentKey()
        { }

        /// <summary>
        /// Constructor for the Document Key
        /// </summary>
        /// <param name="source"></param>
        public DocumentKey(IDocumentKey source) : base()
        {
            if (source.DocumentId is Guid) { DocumentId = source.DocumentId; }
            else { DocumentId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(DocumentKey? other)
        { return other is DocumentKey key && key.DocumentId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(DocumentId, key.DocumentId); }

        /// <inheritdoc/>
        public Boolean Equals(IDocumentKey? other)
        { return other is IDocumentKey key && Equals(new DocumentKey(key)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDocumentKey key && Equals(new DocumentKey(key)); }

        /// <inheritdoc/>
        public static Boolean operator ==(DocumentKey left, DocumentKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DocumentKey left, DocumentKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(DocumentId); }


        #endregion
    }
}
