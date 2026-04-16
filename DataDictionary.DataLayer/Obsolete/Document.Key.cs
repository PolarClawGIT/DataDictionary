using DataDictionary.Resource;

namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>
    /// Interface for the Primary Key for the Scripting Document.
    /// </summary>
    [Obsolete]
    public interface IDocumentKey : IKey
    {
        /// <summary>
        /// Document Id of the Scripting Document.
        /// </summary>
        Guid? DocumentId { get; }
    }

    /// <summary>
    /// Implementation for the Primary Key for the Scripting Document.
    /// </summary>
    [Obsolete]
    public class DocumentKey : IDocumentKey,
        IKeyEquality<IDocumentKey>, IKeyEquality<DocumentKey>
    {
        /// <inheritdoc/>
        public Guid? DocumentId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return DocumentId.HasValue && DocumentId != Guid.Empty; } }

        /// <summary>
        /// Constructor for the Blank/Empty Document Key
        /// </summary>
        /// <remarks>Empty Key is never equal to anything.</remarks>
        public DocumentKey() : base()
        { }

        /// <summary>
        /// Constructor for the Primary Key of the Scripting Document.
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
        { return other is DocumentKey key && key.DocumentId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(DocumentId, other.DocumentId); }

        /// <inheritdoc/>
        public Boolean Equals(IDocumentKey? other)
        { return other is IDocumentKey value && Equals(new DocumentKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDocumentKey value && Equals(new DocumentKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(DocumentKey left, DocumentKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DocumentKey left, DocumentKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (DocumentId is Guid) { return DocumentId.GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
