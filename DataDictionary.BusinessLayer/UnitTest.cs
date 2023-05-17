using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.DbContext;

namespace DataDictionary.BusinessLayer
{
    public class UnitTest
    {
        public static Context AppContext { get; set; } = null!;
        public UnitTest() { }

        public void TestConnection()
        {
            using (var connection = AppContext.CreateConnection())
            {
                connection.Open();
            }

        }
    }
}
