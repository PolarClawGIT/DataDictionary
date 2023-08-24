using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public enum DbObjectScope
    {
        NULL,
        Aggregate,
        Default,
        Function,
        LogicalFileName,
        Procedure,
        Queue,
        Rule,
        Synonym,
        Table,
        Type,
        View,
        XmlSchemaCollection,
    }

    public interface IDbObjectScope
    {
        public DbObjectScope ObjectScope { get; }
    }

    public class DbObjectScopeKey: DbCatalogScopeKey, IDbObjectScope
    {
        public DbObjectScope ObjectScope { get; init; } = DbObjectScope.NULL;
    }
}
