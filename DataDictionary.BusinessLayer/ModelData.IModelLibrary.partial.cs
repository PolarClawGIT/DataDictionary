using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    partial class ModelData_Old : IModelLibrary
    {
        /// <inheritdoc/>
        public LibrarySourceCollection LibrarySources { get; } = new LibrarySourceCollection();

        /// <inheritdoc/>
        public LibraryMemberCollection LibraryMembers { get; } = new LibraryMemberCollection();
    }
}
