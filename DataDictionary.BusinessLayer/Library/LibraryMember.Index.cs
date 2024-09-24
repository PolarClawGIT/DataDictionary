using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.LibraryData;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Library
{
    /// <inheritdoc/>
    public interface ILibraryMemberIndex : ILibraryMemberKey
    { }

    /// <inheritdoc/>
    public class LibraryMemberIndex : LibraryMemberKey, ILibraryMemberIndex,
        IKeyEquality<ILibraryMemberIndex>, IKeyEquality<LibraryMemberIndex>,
        IComparable<ILibraryMemberIndex>
    {
        /// <inheritdoc cref="LibraryMemberKey(ILibraryMemberKey)"/>
        public LibraryMemberIndex(ILibraryMemberIndex source) : base(source)
        { }

        /// <inheritdoc cref="LibraryMemberKey(ILibraryMemberKeyParent)"/>
        public LibraryMemberIndex(LibraryMemberIndexParent source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ILibraryMemberIndex? other)
        { return other is ILibraryMemberKey key && Equals(new LibraryMemberKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(LibraryMemberIndex? other)
        { return other is ILibraryMemberKey key && Equals(new LibraryMemberKey(key)); }

        /// <summary>
        /// Convert LibraryMemberIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(LibraryMemberIndex source)
        { return new DataIndex() { SystemId = source.MemberId ?? Guid.Empty }; }

        /// <inheritdoc/>
        public Int32 CompareTo(ILibraryMemberIndex? other)
        {
            if (other is LibraryMemberIndex && other.MemberId is Guid otherItem && MemberId is Guid item)
            { return item.CompareTo(otherItem); }
            else { return -1; }
        }
    }

}
