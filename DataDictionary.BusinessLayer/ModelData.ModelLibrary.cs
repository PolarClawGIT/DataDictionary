using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Interface component for the Model Library
    /// </summary>
    /// <remarks>When combined with the Extension class, this implements multi-inheritance.</remarks>
    public interface IModelLibrary
    {
        /// <summary>
        /// List of Domain Properties for the Entities within the Model.
        /// </summary>
        LibrarySourceCollection LibrarySources { get; }

        /// <summary>
        /// List of Domain Properties for the Entities within the Model.
        /// </summary>
        LibraryMemberCollection LibraryMembers { get; }
    }

    partial class ModelData: IModelLibrary
    {
        /// <inheritdoc/>
        public LibrarySourceCollection LibrarySources { get; } = new LibrarySourceCollection();

        /// <inheritdoc/>
        public LibraryMemberCollection LibraryMembers { get; } = new LibraryMemberCollection();
    }
}
