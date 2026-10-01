using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Document Data File Key
    /// </summary>
    public interface IDocumentNameKey : IKey
    {
        /// <summary>
        /// The name of the Schema File (XML).
        /// </summary>
        String DataFileName { get; }
    }

    /// <summary>
    /// Implementation for the unique Name of a Data File.
    /// </summary>
    public class DocumentNameKey : IDocumentNameKey,
        IKeyComparable<IDocumentNameKey>, IKeyComparable<DocumentNameKey>
    {
        /// <inheritdoc/>
        public String DataFileName { get; init; } = string.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return !String.IsNullOrEmpty(DataFileName); } }

        /// <summary>
        /// Constructor for the Data File Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public DocumentNameKey(IDocumentNameKey source) : base()
        {
            if (source.DataFileName is string) { DataFileName = source.DataFileName; }
            else { DataFileName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(DocumentNameKey? other)
        {
            return
                other is DocumentNameKey &&
                !string.IsNullOrEmpty(DataFileName) &&
                !string.IsNullOrEmpty(other.DataFileName) &&
                DataFileName.Equals(other.DataFileName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IDocumentNameKey? other)
        { return other is IDocumentNameKey value && Equals(new DocumentNameKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDocumentNameKey value && Equals(new DocumentNameKey(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(DocumentNameKey? other)
        {
            if (other is DocumentNameKey value)
            { return string.Compare(DataFileName, value.DataFileName, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IDocumentNameKey? other)
        { if (other is IDocumentNameKey value) { return CompareTo(new DocumentNameKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IDocumentNameKey value) { return CompareTo(new DocumentNameKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(DocumentNameKey left, DocumentNameKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DocumentNameKey left, DocumentNameKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(DocumentNameKey left, DocumentNameKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(DocumentNameKey left, DocumentNameKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DocumentNameKey left, DocumentNameKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(DocumentNameKey left, DocumentNameKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return DataFileName.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (DataFileName is string) { return DataFileName; }
            else { return string.Empty; }
        }
    }
}
