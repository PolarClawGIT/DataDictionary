using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>
    /// Interface for the unique Name of a Document.
    /// </summary>
    [Obsolete]
    public interface IDocumentKeyName : IKey
    {
        /// <summary>
        /// Title of the Scripting Document
        /// </summary>
        String? DocumentTitle { get; }
    }

    /// <summary>
    /// Implementation for the unique Name of a Document.
    /// </summary>
    [Obsolete]
    public class DocumentKeyName : IDocumentKeyName,
        IKeyComparable<IDocumentKeyName>, IKeyComparable<DocumentKeyName>
    {
        /// <inheritdoc/>
        public String DocumentTitle { get; init; } = string.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return !String.IsNullOrEmpty(DocumentTitle); } }

        /// <summary>
        /// Constructor for the Document Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public DocumentKeyName(IDocumentKeyName source) : base()
        {
            if (source.DocumentTitle is string) { DocumentTitle = source.DocumentTitle; }
            else { DocumentTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(DocumentKeyName? other)
        {
            return
                other is DocumentKeyName &&
                !string.IsNullOrEmpty(DocumentTitle) &&
                !string.IsNullOrEmpty(other.DocumentTitle) &&
                DocumentTitle.Equals(other.DocumentTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IDocumentKeyName? other)
        { return other is IDocumentKeyName value && Equals(new DocumentKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDocumentKeyName value && Equals(new DocumentKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(DocumentKeyName? other)
        {
            if (other is DocumentKeyName value)
            { return string.Compare(DocumentTitle, value.DocumentTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IDocumentKeyName? other)
        { if (other is IDocumentKeyName value) { return CompareTo(new DocumentKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IDocumentKeyName value) { return CompareTo(new DocumentKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(DocumentKeyName left, DocumentKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DocumentKeyName left, DocumentKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(DocumentKeyName left, DocumentKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(DocumentKeyName left, DocumentKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DocumentKeyName left, DocumentKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(DocumentKeyName left, DocumentKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return DocumentTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (DocumentTitle is string) { return DocumentTitle; }
            else { return string.Empty; }
        }
    }
}
