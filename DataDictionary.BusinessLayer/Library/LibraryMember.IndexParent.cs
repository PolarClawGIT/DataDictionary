using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.LibraryData;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Library
{

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
        /// Convert LibraryMemberIndexParent to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(LibraryMemberIndexParent source)
        { return new DataIndex() { SystemId = source.MemberParentId ?? Guid.Empty }; }
    }
}
