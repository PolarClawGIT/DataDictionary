using DataDictionary.DataLayer.DatabaseData;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Tests
{
    [TestFixture()]
    public class DbSchemaNameTests
    {
        class UnitTestData : IDbSchemaKey
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
            DbSchemaKey item = new DbSchemaKey(dataA1);

            Assert.IsTrue(item is DbSchemaKey, "new DbSchemaName(dataA1) is DbSchemaName");
            Assert.IsTrue(item is DbCatalogKeyUnique, "new DbSchemaName(dataA1) is DbCatalogName");
            Assert.IsTrue(item is IDbSchemaKey, "new DbSchemaName(dataA1) is IDbSchemaName");
            Assert.IsTrue(item is IDbCatalogKeyUnique, "new DbSchemaName(dataA1) is IDbCatalogName");
        }

        [Test()]
        public void EqualsTest()
        {
            Assert.IsTrue(new DbSchemaKey(dataA1).Equals(dataA1), "new DbSchemaName(dataA1).Equals(dataA1)");
            Assert.IsTrue(new DbSchemaKey(dataA1).Equals(new DbSchemaKey(dataA1)), "new DbSchemaName(dataA1).Equals(new DbSchemaName(dataA1))");

            Assert.IsTrue(new DbSchemaKey(dataA1) == new DbSchemaKey(dataA1), "new DbSchemaName(dataA1) == new DbSchemaName(dataA1)");
            Assert.IsTrue(new DbSchemaKey(dataA1) == new DbSchemaKey(dataA2), "new DbSchemaName(dataA1) == new DbSchemaName(dataA2)");
            //Assert.IsTrue(new DbSchemaKeyPrimary(dataA1) == dataA1, "new DbSchemaName(dataA1) == dataA1");

            Assert.IsFalse(new DbSchemaKey(dataA1) == new DbSchemaKey(dataC1), "new DbSchemaName(dataA1) != new DbSchemaName(dataC)");
            Assert.IsFalse(new DbSchemaKey(dataNull) == new DbSchemaKey(dataNull), "new DbSchemaName(dataNull) != new DbSchemaName(dataNull)");
            Assert.IsFalse(new DbSchemaKey(dataBlank) == new DbSchemaKey(dataBlank), "new DbSchemaName(dataBlank) != new DbSchemaName(dataBlank)");
        }

        [Test()]
        public void CompareToTest()
        {
            Assert.AreEqual(0, new DbSchemaKey(dataA1).CompareTo(new DbSchemaKey(dataA1)), "new DbSchemaName(dataA1).CompareTo(new DbSchemaName(dataA1))");
            Assert.AreEqual(0, new DbSchemaKey(dataA1).CompareTo(new DbSchemaKey(dataA2)), "new DbSchemaName(dataA1).CompareTo(new DbSchemaName(dataA2))");

            Assert.Greater(0, new DbSchemaKey(dataA1).CompareTo(new DbSchemaKey(dataC1)), "new DbSchemaName(dataA1).CompareTo(new DbSchemaName(dataC1))");
            Assert.Less(0, new DbSchemaKey(dataC1).CompareTo(new DbSchemaKey(dataA1)), "new DbSchemaName(dataC).CompareTo(new DbSchemaName(dataA1))");

            Assert.Greater(0, new DbSchemaKey(dataC1).CompareTo(new DbSchemaKey(dataC2)), "new DbSchemaName(dataC1).CompareTo(new DbSchemaName(dataC1))");
            Assert.Less(0, new DbSchemaKey(dataC2).CompareTo(new DbSchemaKey(dataC1)), "new DbSchemaName(dataC2).CompareTo(new DbSchemaName(dataC1))");

            Assert.IsTrue(new DbSchemaKey(dataA1) < new DbSchemaKey(dataC1), "new DbSchemaName(dataA1) < new DbSchemaName(dataC)");
            Assert.IsTrue(new DbSchemaKey(dataC1) > new DbSchemaKey(dataA1), "new DbSchemaName(dataC) > new DbSchemaName(dataA1)");

            Assert.IsTrue(new DbSchemaKey(dataNull) < new DbSchemaKey(dataA1), "new DbSchemaName(dataNull) < new DbSchemaName(dataA1)");
            Assert.IsTrue(new DbSchemaKey(dataBlank) < new DbSchemaKey(dataA1), "new DbSchemaName(dataBlank) < new DbSchemaName(dataA1)");
        }

        [Test()]
        public void GetHashCodeTest()
        {
            Assert.DoesNotThrow(() => new DbSchemaKey(dataA1).GetHashCode(), "dataA1.GetHashCode()");
            Assert.DoesNotThrow(() => new DbSchemaKey(dataC1).GetHashCode(), "dataC.GetHashCode()");
            Assert.DoesNotThrow(() => new DbSchemaKey(dataNull).GetHashCode(), "dataNull.GetHashCode()");
            Assert.DoesNotThrow(() => new DbSchemaKey(dataBlank).GetHashCode(), "dataBlank.GetHashCode()");
        }

        [Test()]
        public void ToStringTest()
        {
            Assert.DoesNotThrow(() => new DbSchemaKey(dataA1).ToString(), "dataA1.ToString()");
            Assert.DoesNotThrow(() => new DbSchemaKey(dataC1).ToString(), "dataC.ToString()");
            Assert.DoesNotThrow(() => new DbSchemaKey(dataNull).ToString(), "dataNull.ToString()");
            Assert.DoesNotThrow(() => new DbSchemaKey(dataBlank).ToString(), "dataBlank.ToString()");
        }

        [Test()]
        public void SortTest()
        {
            List<DbSchemaKey> items = new List<DbSchemaKey>()
            { new DbSchemaKey(dataC2), new DbSchemaKey(dataA2), new DbSchemaKey(dataC1), new DbSchemaKey(dataA1) };

            items.Sort();

            Assert.AreEqual(items[0], dataA1);
            Assert.AreEqual(items[1], dataA2);
            Assert.AreEqual(items[2], dataC1);
            Assert.AreEqual(items[3], dataC2);
        }

    }
}
