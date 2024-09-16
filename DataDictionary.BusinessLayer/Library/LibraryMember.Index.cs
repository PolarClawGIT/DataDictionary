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
        /// Convert LibraryMemberIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(LibraryMemberIndex source)
        { return new DataLayerIndex() { DataLayerId = source.MemberId ?? Guid.Empty }; }

        /// <inheritdoc/>
        public Int32 CompareTo(ILibraryMemberIndex? other)
        {
            if (other is LibraryMemberIndex && other.MemberId is Guid otherItem && MemberId is Guid item)
            { return item.CompareTo(otherItem); }
            else { return -1; }
        }
    }

    /// <inheritdoc/>
    public interface ILibraryMemberIndexParent : ILibraryMemberKeyParent
    { }

    /// <inheritdoc/>
    public class LibraryMemberIndexParent : LibraryMemberKeyParent, ILibraryMemberIndexParent,
        IKeyEquality<ILibraryMemberIndexParent>, IKeyEquality<LibraryMemberIndexParent>
    {
        /// <inheritdoc cref="LibraryMemberKeyParent(ILibraryMemberKeyParent)"/>
        public LibraryMemberIndexParent(ILibraryMemberIndexParent source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ILibraryMemberIndexParent? other)
        { return other is ILibraryMemberKeyParent key && Equals(new LibraryMemberKeyParent(key)); }

        /// <inheritdoc/>
        public Boolean Equals(LibraryMemberIndexParent? other)
        { return other is ILibraryMemberKeyParent key && Equals(new LibraryMemberKeyParent(key)); }

        /// <summary>
        /// Convert LibraryMemberIndexParent to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(LibraryMemberIndexParent source)
        { return new DataLayerIndex() { DataLayerId = source.MemberParentId ?? Guid.Empty }; }
    }
}
