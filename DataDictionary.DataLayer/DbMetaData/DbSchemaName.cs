using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbSchemaName : IDbCatalogName
    {
        String? SchemaName { get; }
    }

    public record DbSchemaName : IDbSchemaName
    {
        public string? SchemaName { get; init; }
        public string? CatalogName { get; init; }

        public DbSchemaName(IDbSchemaName source) : base()
        {
            CatalogName = source.CatalogName;
            SchemaName = source.SchemaName;
        }
    }
}
