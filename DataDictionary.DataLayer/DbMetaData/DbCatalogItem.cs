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
    public class DbCatalogItem : BindingTableRow
    {
        public String? CatalogName { get { return GetValue("database_name"); } }
        public Nullable<Int32> DatabaseId { get { return GetValue<Int32>("DbId"); } }
        public Nullable<DateTime> CreateDate { get { return GetValue<DateTime>("create_date"); } }
        public Boolean IsSystem { get { return CatalogName is "TempDb" or "Master" or "MSDB"; } }

        internal static IBindingTable<DbCatalogItem> Create(Func<IDataReader> dataReader)
        {
            if (dataReader is null) { throw new ArgumentNullException(nameof(dataReader)); }

            BindingTable<DbCatalogItem> data = new BindingTable<DbCatalogItem>();
            data.Load(dataReader());
            return data;
        }
    }
}
