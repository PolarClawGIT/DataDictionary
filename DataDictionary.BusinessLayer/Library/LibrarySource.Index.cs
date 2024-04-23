using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
