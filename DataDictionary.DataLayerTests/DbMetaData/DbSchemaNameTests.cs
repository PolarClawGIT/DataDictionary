using DataDictionary.DataLayer.DbMetaData;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData.Tests
{
    [TestFixture()]
    public class DbSchemaNameTests
    {
        class UnitTestData : IDbSchemaName
        {
            public String? CatalogName { get; init; }
            public String? SchemaName { get; init; }
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
            DbSchemaName item = new DbSchemaName(dataA1);

            Assert.IsTrue(item is DbSchemaName, "new DbSchemaName(dataA1) is DbSchemaName");
            Assert.IsTrue(item is DbCatalogName, "new DbSchemaName(dataA1) is DbCatalogName");
            Assert.IsTrue(item is IDbSchemaName, "new DbSchemaName(dataA1) is IDbSchemaName");
            Assert.IsTrue(item is IDbCatalogName, "new DbSchemaName(dataA1) is IDbCatalogName");
        }

        [Test()]
        public void EqualsTest()
        {
            Assert.IsTrue(new DbSchemaName(dataA1).Equals(dataA1), "new DbSchemaName(dataA1).Equals(dataA1)");
            Assert.IsTrue(new DbSchemaName(dataA1).Equals(new DbSchemaName(dataA1)), "new DbSchemaName(dataA1).Equals(new DbSchemaName(dataA1))");

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
            Assert.AreEqual(0, new DbSchemaName(dataA1).CompareTo(new DbSchemaName(dataA2)), "new DbSchemaName(dataA1).CompareTo(new DbSchemaName(dataA2))");

            Assert.Greater(0, new DbSchemaName(dataA1).CompareTo(new DbSchemaName(dataC1)), "new DbSchemaName(dataA1).CompareTo(new DbSchemaName(dataC1))");
            Assert.Less(0, new DbSchemaName(dataC1).CompareTo(new DbSchemaName(dataA1)), "new DbSchemaName(dataC).CompareTo(new DbSchemaName(dataA1))");

            Assert.Greater(0, new DbSchemaName(dataC1).CompareTo(new DbSchemaName(dataC2)), "new DbSchemaName(dataC1).CompareTo(new DbSchemaName(dataC1))");
            Assert.Less(0, new DbSchemaName(dataC2).CompareTo(new DbSchemaName(dataC1)), "new DbSchemaName(dataC2).CompareTo(new DbSchemaName(dataC1))");

            Assert.IsTrue(new DbSchemaName(dataA1) < new DbSchemaName(dataC1), "new DbSchemaName(dataA1) < new DbSchemaName(dataC)");
            Assert.IsTrue(new DbSchemaName(dataC1) > new DbSchemaName(dataA1), "new DbSchemaName(dataC) > new DbSchemaName(dataA1)");

            Assert.IsTrue(new DbSchemaName(dataNull) < new DbSchemaName(dataA1), "new DbSchemaName(dataNull) < new DbSchemaName(dataA1)");
            Assert.IsTrue(new DbSchemaName(dataBlank) < new DbSchemaName(dataA1), "new DbSchemaName(dataBlank) < new DbSchemaName(dataA1)");
        }

        [Test()]
        public void GetHashCodeTest()
        {
            Assert.DoesNotThrow(() => new DbSchemaName(dataA1).GetHashCode(), "dataA1.GetHashCode()");
            Assert.DoesNotThrow(() => new DbSchemaName(dataC1).GetHashCode(), "dataC.GetHashCode()");
            Assert.DoesNotThrow(() => new DbSchemaName(dataNull).GetHashCode(), "dataNull.GetHashCode()");
            Assert.DoesNotThrow(() => new DbSchemaName(dataBlank).GetHashCode(), "dataBlank.GetHashCode()");
        }

        [Test()]
        public void ToStringTest()
        {
            Assert.DoesNotThrow(() => new DbSchemaName(dataA1).ToString(), "dataA1.ToString()");
            Assert.DoesNotThrow(() => new DbSchemaName(dataC1).ToString(), "dataC.ToString()");
            Assert.DoesNotThrow(() => new DbSchemaName(dataNull).ToString(), "dataNull.ToString()");
            Assert.DoesNotThrow(() => new DbSchemaName(dataBlank).ToString(), "dataBlank.ToString()");
        }

        [Test()]
        public void SortTest()
        {
            List<DbSchemaName> items = new List<DbSchemaName>()
            { new DbSchemaName(dataC2), new DbSchemaName(dataA2), new DbSchemaName(dataC1), new DbSchemaName(dataA1) };

            items.Sort();

            Assert.AreEqual(items[0], dataA1);
            Assert.AreEqual(items[1], dataA2);
            Assert.AreEqual(items[2], dataC1);
            Assert.AreEqual(items[3], dataC2);
        }

    }
}
