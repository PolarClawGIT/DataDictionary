using NUnit.Framework;
using DataDictionary.DataLayer.DbMetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData.Tests
{
    [TestFixture()]
    public class DbCatalogItemExtensionTests
    {
        class UnitTestData : DbCatalogItem
        {
            public String? catalogName;
            public override String? CatalogName { get { return catalogName; } }
        }

        class UnitTestName : IDbCatalogName
        {
            public String? CatalogName { get; init; }
        }

        UnitTestData dataA1 = new UnitTestData() { catalogName = "A" };
        UnitTestData dataC1 = new UnitTestData() { catalogName = "C" };
        UnitTestData dataNull = new UnitTestData() { catalogName = null };
        UnitTestData dataBlank = new UnitTestData() { catalogName = String.Empty };

        [Test()]
        public void GetCatalogTest()
        {
            List<DbCatalogItem> UnitTestList = new List<DbCatalogItem>() { dataA1, dataC1 };
            UnitTestName nameC1 = new UnitTestName() { CatalogName = dataC1.CatalogName };

            Assert.IsTrue(ReferenceEquals(dataC1, UnitTestList.GetCatalog(dataC1)), "ReferenceEquals(dataC1, UnitTestList.GetCatalog(dataC1)");
            Assert.IsTrue(ReferenceEquals(dataC1, dataC1.GetCatalog(UnitTestList)), "ReferenceEquals(dataC1, dataC1.GetCatalog(UnitTestList)");

            Assert.IsTrue(ReferenceEquals(dataC1, UnitTestList.GetCatalog(nameC1)), "ReferenceEquals(dataC1, UnitTestList.GetCatalog(nameC1)");
            Assert.IsTrue(ReferenceEquals(dataC1, nameC1.GetCatalog(UnitTestList)), "ReferenceEquals(dataC1, nameC1.GetCatalog(UnitTestList)");
        }
    }
}