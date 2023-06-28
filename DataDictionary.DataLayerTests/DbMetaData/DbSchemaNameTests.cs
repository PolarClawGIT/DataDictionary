using DataDictionary.DataLayer.DbMetaData;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayerTests.DbMetaData
{
    [TestFixture()]
    public class DbSchemaNameTests
    {
        class UnitTestData : IDbSchemaName
        {
            public String? CatalogName { get; init; }
            public string? SchemaName { get; init; }
        }

        UnitTestData dataA1 = new UnitTestData() { CatalogName = "A", SchemaName = "A" };
        UnitTestData dataA2 = new UnitTestData() { CatalogName = "a", SchemaName = "a" };
        UnitTestData dataC1 = new UnitTestData() { CatalogName = "A", SchemaName = "C" };
        UnitTestData dataC2 = new UnitTestData() { CatalogName = "C", SchemaName = "C" };
        UnitTestData dataNull = new UnitTestData() { CatalogName = null, SchemaName = null };
        UnitTestData dataBlank = new UnitTestData() { CatalogName = String.Empty, SchemaName = String.Empty };

        [Test()]
        public void DbSchemaNameTest()
        {
            Assert.IsTrue(new DbSchemaName(dataA1) is DbSchemaName, "new DbSchemaName(dataA1) is DbSchemaName");
            Assert.IsTrue(new DbSchemaName(dataA1) is IDbSchemaName, "new DbSchemaName(dataA1) is IDbSchemaName");
            Assert.IsTrue(new DbSchemaName(dataA1) is IDbCatalogName, "new DbSchemaName(dataA1) is IDbCatalogName");
        }

        [Test()]
        public void EqualsTest()
        {
            Assert.IsTrue(new DbSchemaName(dataA1).Equals(dataA1), "new DbSchemaName(dataA1).Equals(dataA1)");
            Assert.IsTrue(new DbSchemaName(dataA1).Equals(new DbSchemaName(dataA1)), "new DbSchemaName(dataA1).Equals(new DbSchemaName(dataA1))");
            Assert.IsTrue(new DbCatalogName(dataA1).Equals(dataA1), "new DbCatalogName(dataA1).Equals(dataA1)");

            Assert.IsTrue(new DbSchemaName(dataA1) == new DbSchemaName(dataA1), "new DbSchemaName(dataA1) == new DbSchemaName(dataA1)");
            Assert.IsTrue(new DbSchemaName(dataA1) == new DbSchemaName(dataA2), "new DbSchemaName(dataA1) == new DbSchemaName(dataA2)");
            Assert.IsTrue(new DbSchemaName(dataA1) == dataA1, "new DbSchemaName(dataA1) == dataA1");

            Assert.IsFalse(new DbSchemaName(dataA1) == new DbSchemaName(dataC1), "new DbSchemaName(dataA1) != new DbSchemaName(dataC)");
            Assert.IsFalse(new DbSchemaName(dataNull) == new DbSchemaName(dataNull), "new DbSchemaName(dataNull) != new DbSchemaName(dataNull)");
            Assert.IsFalse(new DbSchemaName(dataBlank) == new DbSchemaName(dataBlank), "new DbSchemaName(dataBlank) != new DbSchemaName(dataBlank)");
        }

        [Test()]
        public void CompareToTest()
        {
            Assert.AreEqual(0, new DbSchemaName(dataA1).CompareTo(new DbSchemaName(dataA1)), "new DbSchemaName(dataA1).CompareTo(new DbSchemaName(dataA1))");
            Assert.Greater(0, new DbSchemaName(dataA1).CompareTo(new DbSchemaName(dataC1)), "new DbSchemaName(dataA1).CompareTo(new DbSchemaName(dataC1))");
            Assert.Greater(0, new DbSchemaName(dataA1).CompareTo(new DbSchemaName(dataC2)), "new DbSchemaName(dataA1).CompareTo(new DbSchemaName(dataC2))");
            Assert.Less(0, new DbSchemaName(dataC1).CompareTo(new DbSchemaName(dataA1)), "new DbSchemaName(dataC1).CompareTo(new DbSchemaName(dataA1))");
            Assert.Less(0, new DbSchemaName(dataC2).CompareTo(new DbSchemaName(dataA1)), "new DbSchemaName(dataC1).CompareTo(new DbSchemaName(dataA1))");

        }

        [Test()]
        public void GetHashCodeTest()
        {
            Assert.DoesNotThrow(() => dataA1.GetHashCode(), "dataA1.GetHashCode()");
            Assert.DoesNotThrow(() => dataC1.GetHashCode(), "dataC.GetHashCode()");
            Assert.DoesNotThrow(() => dataNull.GetHashCode(), "dataNull.GetHashCode()");
            Assert.DoesNotThrow(() => dataBlank.GetHashCode(), "dataBlank.GetHashCode()");
        }

        [Test()]
        public void ToStringTest()
        {
            Assert.DoesNotThrow(() => dataA1.ToString(), "dataA1.ToString()");
            Assert.DoesNotThrow(() => dataC1.ToString(), "dataC.ToString()");
            Assert.DoesNotThrow(() => dataNull.ToString(), "dataNull.ToString()");
            Assert.DoesNotThrow(() => dataBlank.ToString(), "dataBlank.ToString()");
        }

        [Test()]
        public void SortTest()
        {
            List<DbCatalogName> catalogNames = new List<DbCatalogName>()
            { new DbCatalogName(dataC2), new DbCatalogName(dataA2), new DbCatalogName(dataC1), new DbCatalogName(dataA1) };

            catalogNames.Sort();

            Assert.AreEqual(catalogNames[0], dataA1);
            Assert.AreEqual(catalogNames[1], dataA2);
            Assert.AreEqual(catalogNames[2], dataC1);
            Assert.AreEqual(catalogNames[3], dataC2);
        }

    }
}
