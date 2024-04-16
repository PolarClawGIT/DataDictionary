using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ICatalogKey : DataLayer.DatabaseData.Catalog.IDbCatalogKey
    { }

    /// <inheritdoc/>
    public class CatalogKey : DataLayer.DatabaseData.Catalog.DbCatalogKey
    {
        /// <summary>
        /// Constructor for the Catalog Key.
        /// </summary>
        /// <param name="source"></param>
        public CatalogKey(ICatalogKey source) : base(source) { }
    }
}
