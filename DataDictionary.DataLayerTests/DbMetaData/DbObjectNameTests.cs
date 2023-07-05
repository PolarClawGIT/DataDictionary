﻿using NUnit.Framework;
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
        class UnitTestData : IDbTableName
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
            DbTableName item = new DbTableName(dataA1);

            Assert.IsTrue(item is DbTableName, "new DbObjectName(dataA1) is DbObjectName");
            Assert.IsTrue(item is DbSchemaName, "new DbObjectName(dataA1) is DbSchemaName");
            Assert.IsTrue(item is DbCatalogName, "new DbObjectName(dataA1) is DbCatalogName");

            Assert.IsTrue(item is IDbSchemaName, "new DbObjectName(dataA1) is IDbSchemaName");
            Assert.IsTrue(item is IDbCatalogName, "new DbObjectName(dataA1) is IDbCatalogName");
            Assert.IsTrue(item is IDbTableName, "new DbObjectName(dataA1) is IDbObjectName");
        }

        [Test()]
        public void EqualsTest()
        {
            Assert.IsTrue(new DbTableName(dataA1).Equals(dataA1), "new DbObjectName(dataA1).Equals(dataA1)");
            Assert.IsTrue(new DbTableName(dataA1).Equals(new DbTableName(dataA1)), "new DbObjectName(dataA1).Equals(new DbObjectName(dataA1))");

            Assert.IsTrue(new DbTableName(dataA1) == new DbTableName(dataA1), "new DbObjectName(dataA1) == new DbObjectName(dataA1)");
            Assert.IsTrue(new DbTableName(dataA1) == new DbTableName(dataA2), "new DbObjectName(dataA1) == new DbObjectName(dataA2)");
            Assert.IsTrue(new DbTableName(dataA1) == dataA1, "new DbObjectName(dataA1) == dataA1");

            Assert.IsFalse(new DbTableName(dataA1) == new DbTableName(dataC1), "new DbObjectName(dataA1) != new DbObjectName(dataC)");
            Assert.IsFalse(new DbTableName(dataNull) == new DbTableName(dataNull), "new DbObjectName(dataNull) != new DbObjectName(dataNull)");
            Assert.IsFalse(new DbTableName(dataBlank) == new DbTableName(dataBlank), "new DbObjectName(dataBlank) != new DbObjectName(dataBlank)");
        }

        [Test()]
        public void CompareToTest()
        {
            Assert.AreEqual(0, new DbTableName(dataA1).CompareTo(new DbTableName(dataA1)), "new DbObjectName(dataA1).CompareTo(new DbObjectName(dataA1))");
            Assert.AreEqual(0, new DbTableName(dataA1).CompareTo(new DbTableName(dataA2)), "new DbObjectName(dataA1).CompareTo(new DbObjectName(dataA2))");

            Assert.Greater(0, new DbTableName(dataA1).CompareTo(new DbTableName(dataC1)), "new DbObjectName(dataA1).CompareTo(new DbObjectName(dataC1))");
            Assert.Less(0, new DbTableName(dataC1).CompareTo(new DbTableName(dataA1)), "new DbObjectName(dataC).CompareTo(new DbObjectName(dataA1))");

            Assert.Greater(0, new DbTableName(dataC1).CompareTo(new DbTableName(dataC2)), "new DbObjectName(dataA1).CompareTo(new DbObjectName(dataC2))");
            Assert.Less(0, new DbTableName(dataC2).CompareTo(new DbTableName(dataC1)), "new DbObjectName(dataC).CompareTo(new DbObjectName(dataA1))");

            Assert.IsTrue(new DbTableName(dataA1) < new DbTableName(dataC1), "new DbObjectName(dataA1) < new DbObjectName(dataC)");
            Assert.IsTrue(new DbTableName(dataC1) > new DbTableName(dataA1), "new DbObjectName(dataC) > new DbObjectName(dataA1)");

            Assert.IsTrue(new DbTableName(dataNull) < new DbTableName(dataA1), "new DbObjectName(dataNull) < new DbObjectName(dataA1)");
            Assert.IsTrue(new DbTableName(dataBlank) < new DbTableName(dataA1), "new DbObjectName(dataBlank) < new DbObjectName(dataA1)");
        }

        [Test()]
        public void GetHashCodeTest()
        {
            Assert.DoesNotThrow(() => new DbTableName(dataA1).GetHashCode(), "dataA1.GetHashCode()");
            Assert.DoesNotThrow(() => new DbTableName(dataC1).GetHashCode(), "dataC.GetHashCode()");
            Assert.DoesNotThrow(() => new DbTableName(dataNull).GetHashCode(), "dataNull.GetHashCode()");
            Assert.DoesNotThrow(() => new DbTableName(dataBlank).GetHashCode(), "dataBlank.GetHashCode()");
        }

        [Test()]
        public void ToStringTest()
        {
            Assert.DoesNotThrow(() => new DbTableName(dataA1).ToString(), "dataA1.ToString()");
            Assert.DoesNotThrow(() => new DbTableName(dataC1).ToString(), "dataC.ToString()");
            Assert.DoesNotThrow(() => new DbTableName(dataNull).ToString(), "dataNull.ToString()");
            Assert.DoesNotThrow(() => new DbTableName(dataBlank).ToString(), "dataBlank.ToString()");
        }

        [Test()]
        public void SortTest()
        {
            List<DbTableName> items = new List<DbTableName>()
            { new DbTableName(dataC2), new DbTableName(dataA2), new DbTableName(dataC1), new DbTableName(dataA1) };

            items.Sort();

            Assert.AreEqual(items[0], dataA1);
            Assert.AreEqual(items[1], dataA2);
            Assert.AreEqual(items[2], dataC1);
            Assert.AreEqual(items[3], dataC2);
        }
    }
}