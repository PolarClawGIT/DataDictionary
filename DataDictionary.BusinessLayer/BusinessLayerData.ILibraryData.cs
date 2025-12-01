using DataDictionary.BusinessLayer.AppLibrary;

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
