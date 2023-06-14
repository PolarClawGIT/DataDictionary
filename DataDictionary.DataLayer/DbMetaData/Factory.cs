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
        static Dictionary<Type, Func<IConnection, IDataReader>> dataReaders = new Dictionary<Type, Func<IConnection, IDataReader>>()
        {
            {typeof(DbCatalogItem), DbCatalogItem.GetDataReader },
            {typeof(DbTableItem), DbTableItem.GetDataReader },
            {typeof(DbColumnItem), DbColumnItem.GetDataReader },
            {typeof(DbSchemaItem), DbSchemaItem.GetDataReader },
        };

        public static IDataReader GetDataReader<TRow>(IConnection connection)
            where TRow : BindingTableRow
        {
            if(connection is null) { throw new ArgumentNullException(nameof(connection)); }

            if (dataReaders.ContainsKey(typeof(TRow))) { return dataReaders[typeof(TRow)](connection); }
            else
            {
                Exception ex = new NotImplementedException("Datatype is not defined to the Factory method");
                ex.Data.Add("DataType", typeof(TRow).Name);
                throw ex;
            }
        }

        public static void Load<TRow>(this IBindingTable<TRow> data, IConnection connection)
            where TRow : BindingTableRow
        {
            if (connection is null) { throw new ArgumentNullException(nameof(connection)); }
            data.Load(GetDataReader<TRow>(connection));
        }

    }
}
