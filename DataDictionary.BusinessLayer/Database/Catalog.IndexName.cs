using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ICatalogIndexName : IDbCatalogKeyName
    { }

    /// <inheritdoc/>
    public class CatalogIndexName : DbCatalogKeyName, ICatalogIndexName,
        IKeyEquality<ICatalogIndexName>, IKeyEquality<CatalogIndexName>
    {
        /// <inheritdoc cref="DbCatalogKeyName(IDbCatalogKeyName)"/>
        public CatalogIndexName(ICatalogIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ICatalogIndexName? other)
        { return other is IDbCatalogKeyName value && Equals(new DbCatalogKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(CatalogIndexName? other)
        { return other is IDbCatalogKeyName value && Equals(new DbCatalogKeyName(value)); }
    }
}
