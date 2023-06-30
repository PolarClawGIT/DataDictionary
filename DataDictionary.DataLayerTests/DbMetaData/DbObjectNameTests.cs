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
    public class DbObjectNameTests
    {
        class UnitTestData : IDbObjectName
        {
            public String? CatalogName { get; init; }
            public String? SchemaName { get; init; }
            public String? ObjectName { get; init; }
        }

        UnitTestData dataA1 = new UnitTestData() { CatalogName = "A", SchemaName = "A", ObjectName = "A" };
        UnitTestData dataA2 = new UnitTestData() { CatalogName = "a", SchemaName = "a", ObjectName = "a" };
        UnitTestData dataC1 = new UnitTestData() { CatalogName = "A", SchemaName = "A", ObjectName = "C" };
        UnitTestData dataC2 = new UnitTestData() { CatalogName = "C", SchemaName = "C", ObjectName = "C" };
        UnitTestData dataNull = new UnitTestData() { CatalogName = null, SchemaName = null, ObjectName = null };
        UnitTestData dataBlank = new UnitTestData() { CatalogName = String.Empty, SchemaName = String.Empty, ObjectName = String.Empty };

        [Test()]
        public void DbObjectNameTest()
        {
            DbObjectName item = new DbObjectName(dataA1);

            Assert.IsTrue(item is DbObjectName, "new DbObjectName(dataA1) is DbObjectName");
            Assert.IsTrue(item is DbSchemaName, "new DbObjectName(dataA1) is DbSchemaName");
            Assert.IsTrue(item is DbCatalogName, "new DbObjectName(dataA1) is DbCatalogName");

            Assert.IsTrue(item is IDbSchemaName, "new DbObjectName(dataA1) is IDbSchemaName");
            Assert.IsTrue(item is IDbCatalogName, "new DbObjectName(dataA1) is IDbCatalogName");
            Assert.IsTrue(item is IDbObjectName, "new DbObjectName(dataA1) is IDbObjectName");
        }

        [Test()]
        public void EqualsTest()
        {
            Assert.IsTrue(new DbObjectName(dataA1).Equals(dataA1), "new DbObjectName(dataA1).Equals(dataA1)");
            Assert.IsTrue(new DbObjectName(dataA1).Equals(new DbObjectName(dataA1)), "new DbObjectName(dataA1).Equals(new DbObjectName(dataA1))");

            Assert.IsTrue(new DbObjectName(dataA1) == new DbObjectName(dataA1), "new DbObjectName(dataA1) == new DbObjectName(dataA1)");
            Assert.IsTrue(new DbObjectName(dataA1) == new DbObjectName(dataA2), "new DbObjectName(dataA1) == new DbObjectName(dataA2)");
            Assert.IsTrue(new DbObjectName(dataA1) == dataA1, "new DbObjectName(dataA1) == dataA1");

            Assert.IsFalse(new DbObjectName(dataA1) == new DbObjectName(dataC1), "new DbObjectName(dataA1) != new DbObjectName(dataC)");
            Assert.IsFalse(new DbObjectName(dataNull) == new DbObjectName(dataNull), "new DbObjectName(dataNull) != new DbObjectName(dataNull)");
            Assert.IsFalse(new DbObjectName(dataBlank) == new DbObjectName(dataBlank), "new DbObjectName(dataBlank) != new DbObjectName(dataBlank)");
        }

        [Test()]
        public void CompareToTest()
        {
            Assert.AreEqual(0, new DbObjectName(dataA1).CompareTo(new DbObjectName(dataA1)), "new DbObjectName(dataA1).CompareTo(new DbObjectName(dataA1))");
            Assert.AreEqual(0, new DbObjectName(dataA1).CompareTo(new DbObjectName(dataA2)), "new DbObjectName(dataA1).CompareTo(new DbObjectName(dataA2))");

            Assert.Greater(0, new DbObjectName(dataA1).CompareTo(new DbObjectName(dataC1)), "new DbObjectName(dataA1).CompareTo(new DbObjectName(dataC1))");
            Assert.Less(0, new DbObjectName(dataC1).CompareTo(new DbObjectName(dataA1)), "new DbObjectName(dataC).CompareTo(new DbObjectName(dataA1))");

            Assert.Greater(0, new DbObjectName(dataC1).CompareTo(new DbObjectName(dataC2)), "new DbObjectName(dataA1).CompareTo(new DbObjectName(dataC2))");
            Assert.Less(0, new DbObjectName(dataC2).CompareTo(new DbObjectName(dataC1)), "new DbObjectName(dataC).CompareTo(new DbObjectName(dataA1))");

            Assert.IsTrue(new DbObjectName(dataA1) < new DbObjectName(dataC1), "new DbObjectName(dataA1) < new DbObjectName(dataC)");
            Assert.IsTrue(new DbObjectName(dataC1) > new DbObjectName(dataA1), "new DbObjectName(dataC) > new DbObjectName(dataA1)");

            Assert.IsTrue(new DbObjectName(dataNull) < new DbObjectName(dataA1), "new DbObjectName(dataNull) < new DbObjectName(dataA1)");
            Assert.IsTrue(new DbObjectName(dataBlank) < new DbObjectName(dataA1), "new DbObjectName(dataBlank) < new DbObjectName(dataA1)");
        }

        [Test()]
        public void GetHashCodeTest()
        {
            Assert.DoesNotThrow(() => new DbObjectName(dataA1).GetHashCode(), "dataA1.GetHashCode()");
            Assert.DoesNotThrow(() => new DbObjectName(dataC1).GetHashCode(), "dataC.GetHashCode()");
            Assert.DoesNotThrow(() => new DbObjectName(dataNull).GetHashCode(), "dataNull.GetHashCode()");
            Assert.DoesNotThrow(() => new DbObjectName(dataBlank).GetHashCode(), "dataBlank.GetHashCode()");
        }

        [Test()]
        public void ToStringTest()
        {
            Assert.DoesNotThrow(() => new DbObjectName(dataA1).ToString(), "dataA1.ToString()");
            Assert.DoesNotThrow(() => new DbObjectName(dataC1).ToString(), "dataC.ToString()");
            Assert.DoesNotThrow(() => new DbObjectName(dataNull).ToString(), "dataNull.ToString()");
            Assert.DoesNotThrow(() => new DbObjectName(dataBlank).ToString(), "dataBlank.ToString()");
        }

        [Test()]
        public void SortTest()
        {
            List<DbObjectName> items = new List<DbObjectName>()
            { new DbObjectName(dataC2), new DbObjectName(dataA2), new DbObjectName(dataC1), new DbObjectName(dataA1) };

            items.Sort();

            Assert.AreEqual(items[0], dataA1);
            Assert.AreEqual(items[1], dataA2);
            Assert.AreEqual(items[2], dataC1);
            Assert.AreEqual(items[3], dataC2);
        }
    }
}