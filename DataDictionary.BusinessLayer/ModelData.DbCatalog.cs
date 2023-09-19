using DataDictionary.DataLayer.DatabaseData.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer
{
    partial class ModelData
    {
        /// <summary>
        /// Returns the Catalog that matches the key passed.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public DbCatalogItem? GetCatalog(IDbCatalogKeyUnique item)
        { return DbCatalogs.FirstOrDefault(w => new DbCatalogKeyUnique(item) == new DbCatalogKeyUnique(w)); }
    }
}
