using NUnit.Framework;
using Toolbox.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.DbContext.Tests
{
    [TestFixture()]
    public class ContextTests
    {


        [Test()]
        public void ContextTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CreateConnectionTest()
        {
            
            Context context = new Context() { ServerName = "(LocalDb)\\MSSQLLocalDb", DatabaseName = "master" };
            using (IConnection connection = context.CreateConnection())
            {
                connection.Open();
                connection.Rollback();
            }
        }


        [Test()]
        public void CreateConnectionTest_ConnectionPool()
        {
            // Connection Pool
            Context context = new Context() { ServerName = "(LocalDb)\\MSSQLLocalDb", DatabaseName = "master" };
            using (IConnection connection = context.CreateConnection())
            {
                connection.Open();
                connection.Rollback();
            }

            using (IConnection connection = context.CreateConnection())
            {
                connection.Open();
                connection.Rollback();
            }
        }

        [Test()]
        public void CreateConnectionTests_ApplicationRole()
        {
            // Fails because the password is not correct
            Context context = new Context() { ServerName = "(LocalDb)\\MSSQLLocalDb", DatabaseName = "DataDictionary", ApplicationRole = "DataDictionaryApp" };
            using (IConnection connection = context.CreateConnection())
            {
                connection.Open();
                connection.Rollback();
            }

            using (IConnection connection = context.CreateConnection())
            {
                connection.Open();
                connection.Rollback();
            }

        }

        [Test()]
        public void ToStringTest()
        {
            Assert.Fail();
        }
    }
}