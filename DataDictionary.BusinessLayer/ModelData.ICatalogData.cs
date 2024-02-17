using DataDictionary.BusinessLayer.CatalogData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    partial class ModelData
    {
        /// <summary>
        /// List of Database Catalogs within the Model.
        /// </summary>
        public ICatalogData DbCatalogs { get; } = new CatalogData.CatalogData();

    }
}
