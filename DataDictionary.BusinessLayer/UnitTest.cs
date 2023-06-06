using DataDictionary.DataLayer.DbMetaData;
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
                var x1 = Factory.Create<DbCatalogItem>(() => connection.GetSchema(Schema.Collection.Databases));
                var x2 = Factory.Create<DbSchemaItem>(() => connection.GetSchema(Schema.Collection.Schemas));
                var x3 = Factory.Create<DbTableItem>(() => connection.GetSchema(Schema.Collection.Tables));
                var x4 = Factory.Create<DbColumnItem>(() => connection.GetSchema(Schema.Collection.Columns));

            }
        }
    }
}
