using DataDictionary.Resource;

namespace DataDictionary.DataLayer.LibraryData
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
    public class LibrarySourceKey : ILibrarySourceKey,
        IKeyEquality<ILibrarySourceKey>, IKeyEquality<LibrarySourceKey>
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
        public Boolean Equals(LibrarySourceKey? other)
        { return other is LibrarySourceKey key && EqualityComparer<Guid?>.Default.Equals(LibraryId, key.LibraryId); }

        /// <inheritdoc/>
        public Boolean Equals(ILibrarySourceKey? other)
        { return other is ILibrarySourceKey value && Equals(new LibrarySourceKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ILibrarySourceKey value && Equals(new LibrarySourceKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(LibrarySourceKey left, LibrarySourceKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(LibrarySourceKey left, LibrarySourceKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(LibraryId); }
        #endregion
    }
}
