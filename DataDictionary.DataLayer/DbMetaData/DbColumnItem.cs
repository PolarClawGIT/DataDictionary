using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DbMetaData
{
    public class DbColumnItem : BindingTableRow
    {
        public static BindingTable<DbColumnItem> Create(Func<IDataReader> dataReader)
        {
            if (dataReader is null) { throw new ArgumentNullException(nameof(dataReader)); }

            BindingTable<DbColumnItem> result = new BindingTable<DbColumnItem>();
            result.Load(dataReader());

            return result;
        }
    }
}
