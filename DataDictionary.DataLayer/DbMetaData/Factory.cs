using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DbMetaData
{
    public static class Factory
    {
        /* Could not get this to work
        static Dictionary<Type, Func<Func<IDataReader>, IBindingList>> creators = new Dictionary<Type, Func<Func<IDataReader>, IBindingList>>()
        {
            {typeof(DbCatalogItem), DbCatalogItem.Create },
            {typeof(DbSchemaItem), DbSchemaItem.Create },
            {typeof(DbTableItem), DbTableItem.Create },
            {typeof(DbColumnItem), DbColumnItem.Create },
        };*/

        public static IBindingTable<T> Create<T>(Func<IDataReader> reader)
            where T : BindingTableRow
        {
            if (typeof(T) == typeof(DbCatalogItem)) { return (IBindingTable<T>)DbCatalogItem.Create(reader); }
            else if (typeof(T) == typeof(DbSchemaItem)) { return (IBindingTable<T>)DbSchemaItem.Create(reader); }
            else if (typeof(T) == typeof(DbTableItem)) { return (IBindingTable<T>)DbTableItem.Create(reader); }
            else if (typeof(T) == typeof(DbColumnItem)) { return (IBindingTable<T>)DbColumnItem.Create(reader); }
            else { throw new NotImplementedException(); }
        }
    }
}
