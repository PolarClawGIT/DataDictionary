using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;

namespace DataDictionary.DataLayer.DatabaseData.Tests
{
    [TestFixture()]
    public class DbColumnNameTests
    {
        class UnitTestData : IDbTableColumnKey
        {
            public String? CatalogName { get; init; }
            public String? SchemaName { get; init; }
            public String? TableName { get; init; }
            public String? ColumnName { get; init; }
        }

        UnitTestData dataA1 = new UnitTestData() { CatalogName = "A", SchemaName = "A", TableName = "A", ColumnName = "A" };
        UnitTestData dataA2 = new UnitTestData() { CatalogName = "a", SchemaName = "a", TableName = "a", ColumnName = "a" };
        UnitTestData dataC1 = new UnitTestData() { CatalogName = "A", SchemaName = "A", TableName = "A", ColumnName = "C" };
        UnitTestData dataC2 = new UnitTestData() { CatalogName = "C", SchemaName = "C", TableName = "C", ColumnName = "C" };
        UnitTestData dataNull = new UnitTestData() { CatalogName = null, SchemaName = null, TableName = null, ColumnName = null };
        UnitTestData dataBlank = new UnitTestData() { CatalogName = String.Empty, SchemaName = String.Empty, TableName = String.Empty, ColumnName= String.Empty };


        [Test()]
        public void DbColumnNameTest()
        {
            DbTableColumnKey item = new DbTableColumnKey(dataA1);

            Assert.IsTrue(item is DbTableColumnKey, "new DbColumnName(dataA1) is DbColumnName");
            Assert.IsTrue(item is DbTableKey, "new DbColumnName(dataA1) is DbObjectName");
            Assert.IsTrue(item is DbSchemaKey, "new DbColumnName(dataA1) is DbSchemaName");
            Assert.IsTrue(item is DbCatalogKeyUnique, "new DbColumnName(dataA1) is DbCatalogName");

            Assert.IsTrue(item is IDbTableColumnKey, "new DbColumnName(dataA1) is IDbColumnName");
            Assert.IsTrue(item is IDbSchemaKey, "new DbColumnName(dataA1) is IDbSchemaName");
            Assert.IsTrue(item is IDbCatalogKeyUnique, "new DbColumnName(dataA1) is IDbCatalogName");
            Assert.IsTrue(item is IDbTableKey, "new DbColumnName(dataA1) is IDbObjectName");
        }

        [Test()]
        public void EqualsTest()
        {
            Assert.IsTrue(new DbTableColumnKey(dataA1).Equals(dataA1), "new DbColumnName(dataA1).Equals(dataA1)");
            Assert.IsTrue(new DbTableColumnKey(dataA1).Equals(new DbTableColumnKey(dataA1)), "new DbColumnName(dataA1).Equals(new DbColumnName(dataA1))");

            Assert.IsTrue(new DbTableColumnKey(dataA1) == new DbTableColumnKey(dataA1), "new DbColumnName(dataA1) == new DbColumnName(dataA1)");
            Assert.IsTrue(new DbTableColumnKey(dataA1) == new DbTableColumnKey(dataA2), "new DbColumnName(dataA1) == new DbColumnName(dataA2)");
            //Assert.IsTrue(new DbTableColumnKeyPrimary(dataA1) == dataA1, "new DbColumnName(dataA1) == dataA1");

            Assert.IsFalse(new DbTableColumnKey(dataA1) == new DbTableColumnKey(dataC1), "new DbColumnName(dataA1) != new DbColumnName(dataC)");
            Assert.IsFalse(new DbTableColumnKey(dataNull) == new DbTableColumnKey(dataNull), "new DbColumnName(dataNull) != new DbColumnName(dataNull)");
            Assert.IsFalse(new DbTableColumnKey(dataBlank) == new DbTableColumnKey(dataBlank), "new DbColumnName(dataBlank) != new DbColumnName(dataBlank)");
        }

        [Test()]
        public void CompareToTest()
        {
            Assert.AreEqual(0, new DbTableColumnKey(dataA1).CompareTo(new DbTableColumnKey(dataA1)), "new DbColumnName(dataA1).CompareTo(new DbColumnName(dataA1))");
            Assert.AreEqual(0, new DbTableColumnKey(dataA1).CompareTo(new DbTableColumnKey(dataA2)), "new DbColumnName(dataA1).CompareTo(new DbColumnName(dataA2))");

            Assert.Greater(0, new DbTableColumnKey(dataA1).CompareTo(new DbTableColumnKey(dataC1)), "new DbColumnName(dataA1).CompareTo(new DbColumnName(dataC))");
            Assert.Less(0, new DbTableColumnKey(dataC1).CompareTo(new DbTableColumnKey(dataA1)), "new DbColumnName(dataC).CompareTo(new DbColumnName(dataA1))");

            Assert.Greater(0, new DbTableColumnKey(dataC1).CompareTo(new DbTableColumnKey(dataC2)), "new DbColumnName(dataA1).CompareTo(new DbColumnName(dataC))");
            Assert.Less(0, new DbTableColumnKey(dataC2).CompareTo(new DbTableColumnKey(dataC1)), "new DbColumnName(dataC).CompareTo(new DbColumnName(dataA1))");

            Assert.IsTrue(new DbTableColumnKey(dataA1) < new DbTableColumnKey(dataC1), "new DbColumnName(dataA1) < new DbColumnName(dataC)");
            Assert.IsTrue(new DbTableColumnKey(dataC1) > new DbTableColumnKey(dataA1), "new DbColumnName(dataC) > new DbColumnName(dataA1)");

            Assert.IsTrue(new DbTableColumnKey(dataNull) < new DbTableColumnKey(dataA1), "new DbColumnName(dataNull) < new DbColumnName(dataA1)");
            Assert.IsTrue(new DbTableColumnKey(dataBlank) < new DbTableColumnKey(dataA1), "new DbColumnName(dataBlank) < new DbColumnName(dataA1)");
        }

        [Test()]
        public void GetHashCodeTest()
        {
            Assert.DoesNotThrow(() => new DbTableColumnKey(dataA1).GetHashCode(), "dataA1.GetHashCode()");
            Assert.DoesNotThrow(() => new DbTableColumnKey(dataC1).GetHashCode(), "dataC.GetHashCode()");
            Assert.DoesNotThrow(() => new DbTableColumnKey(dataNull).GetHashCode(), "dataNull.GetHashCode()");
            Assert.DoesNotThrow(() => new DbTableColumnKey(dataBlank).GetHashCode(), "dataBlank.GetHashCode()");
        }

        [Test()]
        public void ToStringTest()
        {
            Assert.DoesNotThrow(() => new DbTableColumnKey(dataA1).ToString(), "dataA1.ToString()");
            Assert.DoesNotThrow(() => new DbTableColumnKey(dataC1).ToString(), "dataC.ToString()");
            Assert.DoesNotThrow(() => new DbTableColumnKey(dataNull).ToString(), "dataNull.ToString()");
            Assert.DoesNotThrow(() => new DbTableColumnKey(dataBlank).ToString(), "dataBlank.ToString()");
        }

        [Test()]
        public void SortTest()
        {
            List<DbTableColumnKey> items = new List<DbTableColumnKey>()
            { new DbTableColumnKey(dataC2), new DbTableColumnKey(dataA2), new DbTableColumnKey(dataC1), new DbTableColumnKey(dataA1) };

            items.Sort();

            Assert.AreEqual(items[0], dataA1);
            Assert.AreEqual(items[1], dataA2);
            Assert.AreEqual(items[2], dataC1);
            Assert.AreEqual(items[3], dataC2);
        }
    }
}