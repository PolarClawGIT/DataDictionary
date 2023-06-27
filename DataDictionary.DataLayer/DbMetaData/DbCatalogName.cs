using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbCatalogName
    {
        String? CatalogName { get; }
    }

    public record DbCatalogName : IDbCatalogName
    {
        public String? CatalogName { get; init; }

        public DbCatalogName(IDbCatalogName source) : base()
        { CatalogName = source.CatalogName; }
    }
}
