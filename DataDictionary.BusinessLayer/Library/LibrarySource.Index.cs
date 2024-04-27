using DataDictionary.DataLayer.LibraryData.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Library
{
    /// <inheritdoc/>
    public interface ILibrarySourceIndex : ILibrarySourceKey
    { }

    /// <inheritdoc/>
    public class LibrarySourceIndex : LibrarySourceKey, ILibrarySourceIndex
    {
        /// <inheritdoc cref="LibrarySourceKey.LibrarySourceKey(ILibrarySourceKey)"/>
        public LibrarySourceIndex(ILibrarySourceIndex source) : base(source)
        { }
    }
}
