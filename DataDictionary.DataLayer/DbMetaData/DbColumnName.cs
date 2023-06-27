using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbColumnName : IDbTableName
    {
        public String? ColumnName { get; }
    }

    public record DbColumnName : IDbColumnName
    {
        public String? CatalogName { get; init; }
        public String? SchemaName { get; init; }
        public String? ColumnName { get; init; }
        public String? TableName { get; init; }

        public DbColumnName(IDbColumnName source) : base()
        {
            CatalogName = source.CatalogName;
            SchemaName = source.SchemaName;
            TableName = source.TableName;
            ColumnName = source.ColumnName;
        }
    }
}
