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
            DataRepository data = new DataRepository();
            data.Load();
        }
    }
}
