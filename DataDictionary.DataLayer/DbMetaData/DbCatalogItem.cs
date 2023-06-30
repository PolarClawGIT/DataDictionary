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
    public interface IDbCatalogItem : IDbCatalogName, IDbIsSystem
    {
        Nullable<Int32> DatabaseId { get; }
        Nullable<DateTime> CreateDate { get; }
    }

    public class DbCatalogItem : BindingTableRow, IDbCatalogItem, INotifyPropertyChanged
    {
        public String? CatalogName { get { return GetValue("database_name"); } }
        public Nullable<Int32> DatabaseId { get { return GetValue<Int32>("DbId"); } }
        public Nullable<DateTime> CreateDate { get { return GetValue<DateTime>("create_date"); } }
        public Boolean IsSystem { get { return CatalogName is "tempdb" or "master" or "msdb" or "model"; } }


        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("database_name", typeof(String)){ AllowDBNull = false},
            new DataColumn("DbId", typeof(Int32)) {AllowDBNull = false},
            new DataColumn("create_date", typeof(DateTime)) {AllowDBNull = false},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions { get; } = columnDefinitions;

        public static IDataReader GetDataReader(IConnection connection)
        { return connection.GetReader(Schema.Collection.Databases); }

        public static IDataReader GetDataReader(IConnection connection, String catalog)
        { return connection.GetReader(Schema.Collection.Databases, catalog); }
    }
}
