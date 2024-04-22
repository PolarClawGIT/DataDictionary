using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbLayer = DataDictionary.DataLayer.LibraryData.Member;

namespace DataDictionary.BusinessLayer.Library
{
    /// <inheritdoc/>
    public interface ILibraryMemberKey : DbLayer.ILibraryMemberKey
    { }

    /// <inheritdoc/>
    public class LibraryMemberKey : DbLayer.LibraryMemberKey, ILibraryMemberKey
    {
        /// <inheritdoc cref="DbLayer.LibraryMemberKey.LibraryMemberKey(DbLayer.ILibraryMemberKey)"/>
        public LibraryMemberKey(ILibraryMemberKey source) : base(source)
        { }
    }
}
