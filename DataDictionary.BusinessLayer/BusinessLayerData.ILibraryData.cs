using DataDictionary.BusinessLayer.LibraryData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    partial class BusinessLayerData
    {
        /// <summary>
        /// Wrapper for Library Data (Library Source and Member)
        /// </summary>
        public ILibraryData LibraryData { get { return library; } }
        private readonly LibraryData.LibraryData library = new LibraryData.LibraryData();
    }
}
