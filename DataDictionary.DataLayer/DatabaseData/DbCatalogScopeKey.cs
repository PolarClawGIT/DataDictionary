using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData
{
    public enum DbCatalogScope
    {
        NULL,
        Assembly,
        Contract,
        EventNotification,
        Filegroup,
        MessageType,
        PartitionFunction,
        PartitionScheme,
        RemoteServiceBinding,
        Route,
        Schema,
        Service,
        Trigger,
        Type,
        User,
    }

    public interface IDbCatalogScope
    {
        DbCatalogScope CatalogScope { get; }
    }

    public class DbCatalogScopeKey: IDbCatalogScope
    {
        public DbCatalogScope CatalogScope { get; init; } = DbCatalogScope.NULL;
    }
}
