using DbLayer = DataDictionary.DataLayer.LibraryData.Member;

namespace DataDictionary.BusinessLayer.Library
{
    /// <inheritdoc/>
    public interface ILibraryMemberIndex : DbLayer.ILibraryMemberKey
    { }

    /// <inheritdoc/>
    public class LibraryMemberIndex : DbLayer.LibraryMemberKey, ILibraryMemberIndex
    {
        /// <inheritdoc cref="DbLayer.LibraryMemberKey.LibraryMemberKey(DbLayer.ILibraryMemberKey)"/>
        public LibraryMemberIndex(ILibraryMemberIndex source) : base(source)
        { }
    }

    /// <inheritdoc/>
    public interface ILibraryMemberIndexParent : DbLayer.ILibraryMemberKeyParent
    { }

    /// <inheritdoc/>
    public class LibraryMemberIndexParent : DbLayer.LibraryMemberKeyParent
    {
        /// <inheritdoc cref="DbLayer.LibraryMemberKeyParent.LibraryMemberKeyParent(DbLayer.ILibraryMemberKeyParent)"/>
        public LibraryMemberIndexParent (ILibraryMemberIndexParent source): base (source)
        { }
    }
}
