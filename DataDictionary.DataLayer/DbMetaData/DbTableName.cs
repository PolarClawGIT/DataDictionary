using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbTableName : IDbSchemaName
    {
        public String? TableName { get; }
    }

    public record DbTableName : IDbTableName
    {
        public String? CatalogName { get; init; }
        public String? SchemaName { get; init; }
        public String? TableName { get; init; }

        public DbTableName(IDbTableName source) : base()
        {
            CatalogName = source.CatalogName;
            SchemaName = source.SchemaName;
            TableName = source.TableName;
        }
    }
}
