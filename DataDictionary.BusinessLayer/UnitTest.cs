using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.DbContext;

namespace DataDictionary.BusinessLayer
{
    public class UnitTest
    {
        public UnitTest() { }

        public void TestConnection()
        {
            using (IConnection connection = BusinessContext.Instance.DbContext.CreateConnection())
            { connection.Open(); }

        }

        public void TestGetSchema()
        {
            using (IConnection connection = BusinessContext.Instance.DbContext.CreateConnection())
            {
                var x1 = DataLayer.DbMetaData.DbCatalogItem.Create(() => connection.GetSchema(Schema.Collection.Databases));
                var x2 = DataLayer.DbMetaData.DbTableItem.Create(() => connection.GetSchema(Schema.Collection.Tables));
                var x3 = DataLayer.DbMetaData.DbSchemaItem.Create(() => connection.GetSchema(Schema.Collection.Schemas));
                var x4 = DataLayer.DbMetaData.DbColumnItem.Create(() => connection.GetSchema(Schema.Collection.Columns));

            }
        }
    }
}
