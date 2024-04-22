using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbLayer = DataDictionary.DataLayer.LibraryData.Source;

namespace DataDictionary.BusinessLayer.Library
{
    /// <inheritdoc/>
    public interface ILibrarySourceKey : DbLayer.ILibrarySourceKey
    { }

    /// <inheritdoc/>
    public class LibrarySourceKey : DbLayer.LibrarySourceKey, ILibrarySourceKey
    {
        /// <inheritdoc cref="DbLayer.LibrarySourceKey.LibrarySourceKey(DbLayer.ILibrarySourceKey)"/>
        public LibrarySourceKey(ILibrarySourceKey source) : base(source)
        { }
    }
}
