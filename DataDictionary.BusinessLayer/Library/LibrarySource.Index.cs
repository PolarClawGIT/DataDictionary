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

        /// <summary>
        /// Convert LibrarySourceIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(LibrarySourceIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.LibraryId ?? Guid.Empty }; }
    }
}
