using DataDictionary.DataLayer.DatabaseData.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ICatalogKey : IDbCatalogKey
    { }

    /// <inheritdoc/>
    public class CatalogKey : DbCatalogKey, ICatalogKey
    {
        /// <inheritdoc cref="DbCatalogKey(IDbCatalogKey)"/>
        public CatalogKey(ICatalogKey source) : base(source) { }
    }
}
