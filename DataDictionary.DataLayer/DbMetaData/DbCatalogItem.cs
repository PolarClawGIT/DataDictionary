using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DbMetaData
{
    public class DbCatalogItem : BindingTableRow
    {
        public String? CatalogName { get { return GetValue("database_name"); } }
        public Nullable<Int32> DatabaseId { get { return GetValue<Int32>("DbId"); } }
        public Nullable<DateTime> CreateDate { get { return GetValue<DateTime>("create_date"); } }
        public Boolean IsSystem { get { return CatalogName is "tempdb" or "master" or "msdb" or "model"; } }

        internal static IDataReader GetDataReader(IConnection connection)
        { return connection.GetSchema(Schema.Collection.Databases); }
    }
}
