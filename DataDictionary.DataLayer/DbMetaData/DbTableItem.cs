using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DbMetaData
{
    public class DbTableItem : BindingTableRow
    {
        public String? CatalogName { get { return GetValue("TABLE_CATALOG"); } }
        public String? SchemaName { get { return GetValue("TABLE_SCHEMA"); } }
        public String? TableName { get { return GetValue("TABLE_NAME"); } }
        public String? TableType { get { return GetValue("TABLE_TYPE"); } }
        public Boolean IsSystem { get { return TableName is "__RefactorLog" or "sysdiagrams"; } }

        public static BindingTable<DbTableItem> Create(Func<IDataReader> dataReader)
        {
            if(dataReader is null) { throw new ArgumentNullException(nameof(dataReader)); }

            BindingTable<DbTableItem> result = new BindingTable<DbTableItem>();
            result.Load(dataReader());

            return result;
        }

    }
}
