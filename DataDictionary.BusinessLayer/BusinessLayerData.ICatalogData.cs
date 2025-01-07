using DataDictionary.BusinessLayer.AppCatalog;
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
        /// Wrapper for the Catalog (database) Data
        /// </summary>
        public ICatalog CatalogModel { get { return catalogValue; } }
        private readonly Catalog catalogValue;

    }
}
