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
    public class DbObjectNameTests
    {
        class UnitTestData : IDbTableKey
        {
            public String? CatalogName { get; init; }
            public String? SchemaName { get; init; }
            public String? TableName { get; init; }
        }

        UnitTestData dataA1 = new UnitTestData() { CatalogName = "A", SchemaName = "A", TableName = "A" };
        UnitTestData dataA2 = new UnitTestData() { CatalogName = "a", SchemaName = "a", TableName = "a" };
        UnitTestData dataC1 = new UnitTestData() { CatalogName = "A", SchemaName = "A", TableName = "C" };
        UnitTestData dataC2 = new UnitTestData() { CatalogName = "C", SchemaName = "C", TableName = "C" };
        UnitTestData dataNull = new UnitTestData() { CatalogName = null, SchemaName = null, TableName = null };
        UnitTestData dataBlank = new UnitTestData() { CatalogName = String.Empty, SchemaName = String.Empty, TableName = String.Empty };

        [Test()]
        public void DbObjectNameTest()
        {
            DbTableKey item = new DbTableKey(dataA1);

            Assert.IsTrue(item is DbTableKey, "new DbObjectName(dataA1) is DbObjectName");
            Assert.IsTrue(item is DbSchemaKey, "new DbObjectName(dataA1) is DbSchemaName");
            Assert.IsTrue(item is DbCatalogKeyUnique, "new DbObjectName(dataA1) is DbCatalogName");

            Assert.IsTrue(item is IDbSchemaKey, "new DbObjectName(dataA1) is IDbSchemaName");
            Assert.IsTrue(item is IDbCatalogKeyUnique, "new DbObjectName(dataA1) is IDbCatalogName");
            Assert.IsTrue(item is IDbTableKey, "new DbObjectName(dataA1) is IDbObjectName");
        }

        [Test()]
        public void EqualsTest()
        {
            Assert.IsTrue(new DbTableKey(dataA1).Equals(dataA1), "new DbObjectName(dataA1).Equals(dataA1)");
            Assert.IsTrue(new DbTableKey(dataA1).Equals(new DbTableKey(dataA1)), "new DbObjectName(dataA1).Equals(new DbObjectName(dataA1))");

            Assert.IsTrue(new DbTableKey(dataA1) == new DbTableKey(dataA1), "new DbObjectName(dataA1) == new DbObjectName(dataA1)");
            Assert.IsTrue(new DbTableKey(dataA1) == new DbTableKey(dataA2), "new DbObjectName(dataA1) == new DbObjectName(dataA2)");
            //Assert.IsTrue(new DbTableKeyPrimary(dataA1) == dataA1, "new DbObjectName(dataA1) == dataA1");

            Assert.IsFalse(new DbTableKey(dataA1) == new DbTableKey(dataC1), "new DbObjectName(dataA1) != new DbObjectName(dataC)");
            Assert.IsFalse(new DbTableKey(dataNull) == new DbTableKey(dataNull), "new DbObjectName(dataNull) != new DbObjectName(dataNull)");
            Assert.IsFalse(new DbTableKey(dataBlank) == new DbTableKey(dataBlank), "new DbObjectName(dataBlank) != new DbObjectName(dataBlank)");
        }

        [Test()]
        public void CompareToTest()
        {
            Assert.AreEqual(0, new DbTableKey(dataA1).CompareTo(new DbTableKey(dataA1)), "new DbObjectName(dataA1).CompareTo(new DbObjectName(dataA1))");
            Assert.AreEqual(0, new DbTableKey(dataA1).CompareTo(new DbTableKey(dataA2)), "new DbObjectName(dataA1).CompareTo(new DbObjectName(dataA2))");

            Assert.Greater(0, new DbTableKey(dataA1).CompareTo(new DbTableKey(dataC1)), "new DbObjectName(dataA1).CompareTo(new DbObjectName(dataC1))");
            Assert.Less(0, new DbTableKey(dataC1).CompareTo(new DbTableKey(dataA1)), "new DbObjectName(dataC).CompareTo(new DbObjectName(dataA1))");

            Assert.Greater(0, new DbTableKey(dataC1).CompareTo(new DbTableKey(dataC2)), "new DbObjectName(dataA1).CompareTo(new DbObjectName(dataC2))");
            Assert.Less(0, new DbTableKey(dataC2).CompareTo(new DbTableKey(dataC1)), "new DbObjectName(dataC).CompareTo(new DbObjectName(dataA1))");

            Assert.IsTrue(new DbTableKey(dataA1) < new DbTableKey(dataC1), "new DbObjectName(dataA1) < new DbObjectName(dataC)");
            Assert.IsTrue(new DbTableKey(dataC1) > new DbTableKey(dataA1), "new DbObjectName(dataC) > new DbObjectName(dataA1)");

            Assert.IsTrue(new DbTableKey(dataNull) < new DbTableKey(dataA1), "new DbObjectName(dataNull) < new DbObjectName(dataA1)");
            Assert.IsTrue(new DbTableKey(dataBlank) < new DbTableKey(dataA1), "new DbObjectName(dataBlank) < new DbObjectName(dataA1)");
        }

        [Test()]
        public void GetHashCodeTest()
        {
            Assert.DoesNotThrow(() => new DbTableKey(dataA1).GetHashCode(), "dataA1.GetHashCode()");
            Assert.DoesNotThrow(() => new DbTableKey(dataC1).GetHashCode(), "dataC.GetHashCode()");
            Assert.DoesNotThrow(() => new DbTableKey(dataNull).GetHashCode(), "dataNull.GetHashCode()");
            Assert.DoesNotThrow(() => new DbTableKey(dataBlank).GetHashCode(), "dataBlank.GetHashCode()");
        }

        [Test()]
        public void ToStringTest()
        {
            Assert.DoesNotThrow(() => new DbTableKey(dataA1).ToString(), "dataA1.ToString()");
            Assert.DoesNotThrow(() => new DbTableKey(dataC1).ToString(), "dataC.ToString()");
            Assert.DoesNotThrow(() => new DbTableKey(dataNull).ToString(), "dataNull.ToString()");
            Assert.DoesNotThrow(() => new DbTableKey(dataBlank).ToString(), "dataBlank.ToString()");
        }

        [Test()]
        public void SortTest()
        {
            List<DbTableKey> items = new List<DbTableKey>()
            { new DbTableKey(dataC2), new DbTableKey(dataA2), new DbTableKey(dataC1), new DbTableKey(dataA1) };

            items.Sort();

            Assert.AreEqual(items[0], dataA1);
            Assert.AreEqual(items[1], dataA2);
            Assert.AreEqual(items[2], dataC1);
            Assert.AreEqual(items[3], dataC2);
        }
    }
}