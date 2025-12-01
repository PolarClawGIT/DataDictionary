using DataDictionary.BusinessLayer.AppCatalog;

namespace DataDictionary.BusinessLayer
{
    partial class BusinessLayerData
    {
        /// <summary>
        /// Wrapper for the Catalog (database) Data
        /// </summary>
        public ICatalog CatalogModel { get { return catalogValue; } }
        private readonly Catalog catalogValue;

    }
}
