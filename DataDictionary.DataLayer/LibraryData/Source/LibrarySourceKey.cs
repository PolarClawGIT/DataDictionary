using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.LibraryData.Source
{
    /// <summary>
    /// Interface for the Library Source Key
    /// </summary>
    public interface ILibrarySourceKey : IKey
    {
        /// <summary>
        /// Library Id for the Library Source
        /// </summary>
        Guid? LibraryId { get; }
    }

    /// <summary>
    /// Implementation of the Library Source Key
    /// </summary>
    public class LibrarySourceKey : ILibrarySourceKey, IKeyEquality<ILibrarySourceKey>
    {
        /// <inheritdoc/>
        public Guid? LibraryId { get; init; } = Guid.Empty;

        /// <inheritdoc cref="Nullable{T}.HasValue"/>
        public virtual Boolean HasValue { get { return LibraryId.HasValue && LibraryId != Guid.Empty; } }

        /// <summary>
        /// Constructor for the Library Source Key
        /// </summary>
        /// <param name="source"></param>
        public LibrarySourceKey(ILibrarySourceKey source) : base()
        {
            if (source.LibraryId is Guid) { LibraryId = source.LibraryId; }
            else { LibraryId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public bool Equals(ILibrarySourceKey? other)
        { return other is ILibrarySourceKey key && EqualityComparer<Guid?>.Default.Equals(LibraryId, key.LibraryId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is ILibrarySourceKey value && Equals(new LibrarySourceKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(LibrarySourceKey left, LibrarySourceKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(LibrarySourceKey left, LibrarySourceKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(LibraryId); }
        #endregion
    }
}
