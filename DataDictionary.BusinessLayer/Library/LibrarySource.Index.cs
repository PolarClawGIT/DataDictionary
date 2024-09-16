using DataDictionary.DataLayer.LibraryData;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Library
{
    /// <inheritdoc/>
    public interface ILibrarySourceIndex : ILibrarySourceKey
    { }

    /// <inheritdoc/>
    public class LibrarySourceIndex : LibrarySourceKey, ILibrarySourceIndex,
        IKeyEquality<ILibrarySourceIndex>, IKeyEquality<LibrarySourceIndex>
    {
        /// <inheritdoc cref="LibrarySourceKey.LibrarySourceKey(ILibrarySourceKey)"/>
        public LibrarySourceIndex(ILibrarySourceIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ILibrarySourceIndex? other)
        { return other is ILibrarySourceKey key && Equals(new LibrarySourceKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(LibrarySourceIndex? other)
        { return other is ILibrarySourceKey key && Equals(new LibrarySourceKey(key)); }

        /// <summary>
        /// Convert LibrarySourceIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(LibrarySourceIndex source)
        { return new DataLayerIndex() { DataLayerId = source.LibraryId ?? Guid.Empty }; }
    }
}
