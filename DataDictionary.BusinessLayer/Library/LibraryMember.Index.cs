using DataDictionary.DataLayer.LibraryData.Member;

namespace DataDictionary.BusinessLayer.Library
{
    /// <inheritdoc/>
    public interface ILibraryMemberIndex : ILibraryMemberKey
    { }

    /// <inheritdoc/>
    public class LibraryMemberIndex : LibraryMemberKey, ILibraryMemberIndex
    {
        /// <inheritdoc cref="LibraryMemberKey(ILibraryMemberKey)"/>
        public LibraryMemberIndex(ILibraryMemberIndex source) : base(source)
        { }
    }

    /// <inheritdoc/>
    public interface ILibraryMemberIndexParent : ILibraryMemberKeyParent
    { }

    /// <inheritdoc/>
    public class LibraryMemberIndexParent : LibraryMemberKeyParent
    {
        /// <inheritdoc cref="LibraryMemberKeyParent(ILibraryMemberKeyParent)"/>
        public LibraryMemberIndexParent (ILibraryMemberIndexParent source): base (source)
        { }
    }
}
