﻿using DataDictionary.DataLayer.LibraryData.Member;

namespace DataDictionary.BusinessLayer.Library
{
    /// <inheritdoc/>
    public interface ILibraryMemberIndex : ILibraryMemberKey
    { }

    /// <inheritdoc/>
    public class LibraryMemberIndex : LibraryMemberKey, ILibraryMemberIndex, IComparable<ILibraryMemberIndex>
    {
        /// <inheritdoc cref="LibraryMemberKey(ILibraryMemberKey)"/>
        public LibraryMemberIndex(ILibraryMemberIndex source) : base(source)
        { }

        /// <inheritdoc cref="LibraryMemberKey(ILibraryMemberKeyParent)"/>
        public LibraryMemberIndex(LibraryMemberIndexParent source) : base(source)
        { }

        /// <summary>
        /// Convert LibraryMemberIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(LibraryMemberIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.MemberId ?? Guid.Empty }; }

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
    public class LibraryMemberIndexParent : LibraryMemberKeyParent
    {
        /// <inheritdoc cref="LibraryMemberKeyParent(ILibraryMemberKeyParent)"/>
        public LibraryMemberIndexParent(ILibraryMemberIndexParent source) : base(source)
        { }

        /// <summary>
        /// Convert LibraryMemberIndexParent to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(LibraryMemberIndexParent source)
        { return new DataLayerIndex() { BusinessLayerId = source.MemberParentId ?? Guid.Empty }; }
    }
}
