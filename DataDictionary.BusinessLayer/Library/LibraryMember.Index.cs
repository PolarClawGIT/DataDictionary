<<<<<<< HEAD
﻿using DbLayer = DataDictionary.DataLayer.LibraryData.Member;
=======
﻿using DataDictionary.DataLayer.LibraryData.Member;
>>>>>>> RenameIndexValue

namespace DataDictionary.BusinessLayer.Library
{
    /// <inheritdoc/>
<<<<<<< HEAD
    public interface ILibraryMemberIndex : DbLayer.ILibraryMemberKey
    { }

    /// <inheritdoc/>
    public class LibraryMemberIndex : DbLayer.LibraryMemberKey, ILibraryMemberIndex
    {
        /// <inheritdoc cref="DbLayer.LibraryMemberKey.LibraryMemberKey(DbLayer.ILibraryMemberKey)"/>
=======
    public interface ILibraryMemberIndex : ILibraryMemberKey
    { }

    /// <inheritdoc/>
    public class LibraryMemberIndex : LibraryMemberKey, ILibraryMemberIndex
    {
        /// <inheritdoc cref="LibraryMemberKey(ILibraryMemberKey)"/>
>>>>>>> RenameIndexValue
        public LibraryMemberIndex(ILibraryMemberIndex source) : base(source)
        { }
    }

    /// <inheritdoc/>
<<<<<<< HEAD
    public interface ILibraryMemberIndexParent : DbLayer.ILibraryMemberKeyParent
    { }

    /// <inheritdoc/>
    public class LibraryMemberIndexParent : DbLayer.LibraryMemberKeyParent
    {
        /// <inheritdoc cref="DbLayer.LibraryMemberKeyParent.LibraryMemberKeyParent(DbLayer.ILibraryMemberKeyParent)"/>
=======
    public interface ILibraryMemberIndexParent : ILibraryMemberKeyParent
    { }

    /// <inheritdoc/>
    public class LibraryMemberIndexParent : LibraryMemberKeyParent
    {
        /// <inheritdoc cref="LibraryMemberKeyParent(ILibraryMemberKeyParent)"/>
>>>>>>> RenameIndexValue
        public LibraryMemberIndexParent (ILibraryMemberIndexParent source): base (source)
        { }
    }
}
