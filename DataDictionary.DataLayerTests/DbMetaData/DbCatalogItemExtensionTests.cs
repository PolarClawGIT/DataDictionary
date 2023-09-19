using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDictionary.DataLayer.DatabaseData.Catalog;

namespace DataDictionary.DataLayer.DatabaseData.Tests
{
    [TestFixture()]
    public class DbCatalogItemExtensionTests
    {
        class UnitTestData : DbCatalogItem
        {
            public String? catalogName;
            public new String? CatalogName { get { return catalogName; } }
        }

        class UnitTestName : IDbCatalogKeyUnique
        {
            public String? CatalogName { get; init; }
        }

        UnitTestData dataA1 = new UnitTestData() { catalogName = "A" };
        UnitTestData dataC1 = new UnitTestData() { catalogName = "C" };
        UnitTestData dataNull = new UnitTestData() { catalogName = null };
        UnitTestData dataBlank = new UnitTestData() { catalogName = String.Empty };
    }
}