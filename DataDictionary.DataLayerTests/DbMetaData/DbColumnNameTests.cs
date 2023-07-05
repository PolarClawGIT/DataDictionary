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
    public class DbColumnNameTests
    {
        class UnitTestData : IDbColumnName
        {
            public String? CatalogName { get; init; }
            public String? SchemaName { get; init; }
            public String? ObjectName { get; init; }
            public String? ColumnName { get; init; }
        }

        UnitTestData dataA1 = new UnitTestData() { CatalogName = "A", SchemaName = "A", ObjectName = "A", ColumnName = "A" };
        UnitTestData dataA2 = new UnitTestData() { CatalogName = "a", SchemaName = "a", ObjectName = "a", ColumnName = "a" };
        UnitTestData dataC1 = new UnitTestData() { CatalogName = "A", SchemaName = "A", ObjectName = "A", ColumnName = "C" };
        UnitTestData dataC2 = new UnitTestData() { CatalogName = "C", SchemaName = "C", ObjectName = "C", ColumnName = "C" };
        UnitTestData dataNull = new UnitTestData() { CatalogName = null, SchemaName = null, ObjectName = null, ColumnName = null };
        UnitTestData dataBlank = new UnitTestData() { CatalogName = String.Empty, SchemaName = String.Empty, ObjectName = String.Empty, ColumnName= String.Empty };


        [Test()]
        public void DbColumnNameTest()
        {
            DbColumnName item = new DbColumnName(dataA1);

            Assert.IsTrue(item is DbColumnName, "new DbColumnName(dataA1) is DbColumnName");
            Assert.IsTrue(item is DbTableName, "new DbColumnName(dataA1) is DbObjectName");
            Assert.IsTrue(item is DbSchemaName, "new DbColumnName(dataA1) is DbSchemaName");
            Assert.IsTrue(item is DbCatalogName, "new DbColumnName(dataA1) is DbCatalogName");

            Assert.IsTrue(item is IDbColumnName, "new DbColumnName(dataA1) is IDbColumnName");
            Assert.IsTrue(item is IDbSchemaName, "new DbColumnName(dataA1) is IDbSchemaName");
            Assert.IsTrue(item is IDbCatalogName, "new DbColumnName(dataA1) is IDbCatalogName");
            Assert.IsTrue(item is IDbTableName, "new DbColumnName(dataA1) is IDbObjectName");
        }

        [Test()]
        public void EqualsTest()
        {
            Assert.IsTrue(new DbColumnName(dataA1).Equals(dataA1), "new DbColumnName(dataA1).Equals(dataA1)");
            Assert.IsTrue(new DbColumnName(dataA1).Equals(new DbColumnName(dataA1)), "new DbColumnName(dataA1).Equals(new DbColumnName(dataA1))");

            Assert.IsTrue(new DbColumnName(dataA1) == new DbColumnName(dataA1), "new DbColumnName(dataA1) == new DbColumnName(dataA1)");
            Assert.IsTrue(new DbColumnName(dataA1) == new DbColumnName(dataA2), "new DbColumnName(dataA1) == new DbColumnName(dataA2)");
            Assert.IsTrue(new DbColumnName(dataA1) == dataA1, "new DbColumnName(dataA1) == dataA1");

            Assert.IsFalse(new DbColumnName(dataA1) == new DbColumnName(dataC1), "new DbColumnName(dataA1) != new DbColumnName(dataC)");
            Assert.IsFalse(new DbColumnName(dataNull) == new DbColumnName(dataNull), "new DbColumnName(dataNull) != new DbColumnName(dataNull)");
            Assert.IsFalse(new DbColumnName(dataBlank) == new DbColumnName(dataBlank), "new DbColumnName(dataBlank) != new DbColumnName(dataBlank)");
        }

        [Test()]
        public void CompareToTest()
        {
            Assert.AreEqual(0, new DbColumnName(dataA1).CompareTo(new DbColumnName(dataA1)), "new DbColumnName(dataA1).CompareTo(new DbColumnName(dataA1))");
            Assert.AreEqual(0, new DbColumnName(dataA1).CompareTo(new DbColumnName(dataA2)), "new DbColumnName(dataA1).CompareTo(new DbColumnName(dataA2))");

            Assert.Greater(0, new DbColumnName(dataA1).CompareTo(new DbColumnName(dataC1)), "new DbColumnName(dataA1).CompareTo(new DbColumnName(dataC))");
            Assert.Less(0, new DbColumnName(dataC1).CompareTo(new DbColumnName(dataA1)), "new DbColumnName(dataC).CompareTo(new DbColumnName(dataA1))");

            Assert.Greater(0, new DbColumnName(dataC1).CompareTo(new DbColumnName(dataC2)), "new DbColumnName(dataA1).CompareTo(new DbColumnName(dataC))");
            Assert.Less(0, new DbColumnName(dataC2).CompareTo(new DbColumnName(dataC1)), "new DbColumnName(dataC).CompareTo(new DbColumnName(dataA1))");

            Assert.IsTrue(new DbColumnName(dataA1) < new DbColumnName(dataC1), "new DbColumnName(dataA1) < new DbColumnName(dataC)");
            Assert.IsTrue(new DbColumnName(dataC1) > new DbColumnName(dataA1), "new DbColumnName(dataC) > new DbColumnName(dataA1)");

            Assert.IsTrue(new DbColumnName(dataNull) < new DbColumnName(dataA1), "new DbColumnName(dataNull) < new DbColumnName(dataA1)");
            Assert.IsTrue(new DbColumnName(dataBlank) < new DbColumnName(dataA1), "new DbColumnName(dataBlank) < new DbColumnName(dataA1)");
        }

        [Test()]
        public void GetHashCodeTest()
        {
            Assert.DoesNotThrow(() => new DbColumnName(dataA1).GetHashCode(), "dataA1.GetHashCode()");
            Assert.DoesNotThrow(() => new DbColumnName(dataC1).GetHashCode(), "dataC.GetHashCode()");
            Assert.DoesNotThrow(() => new DbColumnName(dataNull).GetHashCode(), "dataNull.GetHashCode()");
            Assert.DoesNotThrow(() => new DbColumnName(dataBlank).GetHashCode(), "dataBlank.GetHashCode()");
        }

        [Test()]
        public void ToStringTest()
        {
            Assert.DoesNotThrow(() => new DbColumnName(dataA1).ToString(), "dataA1.ToString()");
            Assert.DoesNotThrow(() => new DbColumnName(dataC1).ToString(), "dataC.ToString()");
            Assert.DoesNotThrow(() => new DbColumnName(dataNull).ToString(), "dataNull.ToString()");
            Assert.DoesNotThrow(() => new DbColumnName(dataBlank).ToString(), "dataBlank.ToString()");
        }

        [Test()]
        public void SortTest()
        {
            List<DbColumnName> items = new List<DbColumnName>()
            { new DbColumnName(dataC2), new DbColumnName(dataA2), new DbColumnName(dataC1), new DbColumnName(dataA1) };

            items.Sort();

            Assert.AreEqual(items[0], dataA1);
            Assert.AreEqual(items[1], dataA2);
            Assert.AreEqual(items[2], dataC1);
            Assert.AreEqual(items[3], dataC2);
        }
    }
}