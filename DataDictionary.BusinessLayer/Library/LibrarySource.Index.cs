using DbLayer = DataDictionary.DataLayer.LibraryData.Source;

namespace DataDictionary.BusinessLayer.Library
{
    /// <inheritdoc/>
    public interface ILibrarySourceIndex : DbLayer.ILibrarySourceKey
    { }

    /// <inheritdoc/>
    public class LibrarySourceIndex : DbLayer.LibrarySourceKey, ILibrarySourceIndex
    {
        /// <inheritdoc cref="DbLayer.LibrarySourceKey.LibrarySourceKey(DbLayer.ILibrarySourceKey)"/>
        public LibrarySourceIndex(ILibrarySourceIndex source) : base(source)
        { }
    }
}
