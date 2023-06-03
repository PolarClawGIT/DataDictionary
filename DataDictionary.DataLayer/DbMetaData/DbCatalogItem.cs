using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DbMetaData
{
    public class  DbCatalogItem : BindingTableRow
    {
        public static BindingTable<DbCatalogItem> Create(Func<IDataReader> dataReader)
        {
            if (dataReader is null) { throw new ArgumentNullException(nameof(dataReader)); }

            BindingTable<DbCatalogItem> result = new BindingTable<DbCatalogItem>();
            result.Load(dataReader());

            return result;
        }
    }
}
