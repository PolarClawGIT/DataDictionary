using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ICatalogIndex : IDbCatalogKey
    { }

    /// <inheritdoc/>
    public class CatalogIndex : DbCatalogKey, ICatalogIndex
    {
        /// <inheritdoc cref="DbCatalogKey(IDbCatalogKey)"/>
        public CatalogIndex(ICatalogIndex source) : base(source) { }
    }

    /// <inheritdoc/>
    public interface ICatalogIndexName : IDbCatalogKeyName
    { }

    /// <inheritdoc/>
    public class CatalogIndexName : DbCatalogKeyName, ICatalogIndexName
    {
        /// <inheritdoc cref="DbCatalogKeyName(IDbCatalogKeyName)"/>
        public CatalogIndexName(ICatalogIndexName source) : base(source) { }
    }
}
