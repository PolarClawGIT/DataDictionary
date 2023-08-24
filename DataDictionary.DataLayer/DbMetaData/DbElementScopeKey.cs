using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public enum DbElementScope
    {
        NULL,
        Default,
        Column,
        Constraint,
        EventNotification,
        Index,
        Parameter,
        Trigger,
    }

    public interface IDbElementScope
    {
        public DbElementScope ElementScope { get; }
    }

    public class DbElementScopeKey: DbObjectScopeKey, IDbElementScope
    {
        public DbElementScope ElementScope { get; init; } = DbElementScope.NULL;
    }
}
