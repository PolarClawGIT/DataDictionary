using DataDictionary.BusinessLayer.AppLibrary;
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
        public ILibrary LibraryModel { get { return libraryValues; } }
        private readonly LibraryModel libraryValues;
    }
}
