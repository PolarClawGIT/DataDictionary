<<<<<<< HEAD
﻿using DbLayer = DataDictionary.DataLayer.LibraryData.Source;
=======
﻿using DataDictionary.DataLayer.LibraryData.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> RenameIndexValue

namespace DataDictionary.BusinessLayer.Library
{
    /// <inheritdoc/>
<<<<<<< HEAD
    public interface ILibrarySourceIndex : DbLayer.ILibrarySourceKey
    { }

    /// <inheritdoc/>
    public class LibrarySourceIndex : DbLayer.LibrarySourceKey, ILibrarySourceIndex
    {
        /// <inheritdoc cref="DbLayer.LibrarySourceKey.LibrarySourceKey(DbLayer.ILibrarySourceKey)"/>
=======
    public interface ILibrarySourceIndex : ILibrarySourceKey
    { }

    /// <inheritdoc/>
    public class LibrarySourceIndex : LibrarySourceKey, ILibrarySourceIndex
    {
        /// <inheritdoc cref="LibrarySourceKey.LibrarySourceKey(ILibrarySourceKey)"/>
>>>>>>> RenameIndexValue
        public LibrarySourceIndex(ILibrarySourceIndex source) : base(source)
        { }
    }
}
