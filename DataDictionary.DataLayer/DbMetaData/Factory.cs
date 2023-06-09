using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DbMetaData
{
    public static class Factory
    {
        static Dictionary<Type, Func<IConnection, IBindingList>> creators = new Dictionary<Type, Func<IConnection, IBindingList>>()
        {
            {typeof(DbCatalogItem), DbCatalogItem.Create },
            {typeof(DbTableItem), DbTableItem.Create },
            {typeof(DbColumnItem), DbColumnItem.Create },
            {typeof(DbSchemaItem), DbSchemaItem.Create },
        };

        public static IBindingTable<T> Create<T>(IConnection connection)
            where T : BindingTableRow
        {
            if (creators.ContainsKey(typeof(T))) { return (IBindingTable<T>)creators[typeof(T)](connection); }
            else
            {
                Exception ex = new NotImplementedException("Datatype is not defined to the Factory method");
                ex.Data.Add("DataType", typeof(T).Name);
                throw ex;
            }
        }
    }
}
