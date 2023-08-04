using DataDictionary.DataLayer.DbMetaData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DbGetSchema
{
    public class GetSchemaDatabase : BindingTableRow, IDbCatalogKeyUnique
    {
        public virtual String? CatalogName { get { return GetValue("database_name"); } }
        public static InformationSchema.Collection Schema { get { return InformationSchema.Collection.Databases; } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("database_name", typeof(String)){ AllowDBNull = true},
            new DataColumn("dbid", typeof(Int16)){ AllowDBNull = false},
            new DataColumn("create_date", typeof(DateTime)){ AllowDBNull = false},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }
    }
}
