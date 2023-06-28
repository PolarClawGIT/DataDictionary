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
    public class DbCatalogNameTests
    {
        class UnitTestData : IDbCatalogName
        { public String? CatalogName { get; init; } }

        UnitTestData dataA1 = new UnitTestData() { CatalogName = "A" };
        UnitTestData dataA2 = new UnitTestData() { CatalogName = "a" };
        UnitTestData dataC1 = new UnitTestData() { CatalogName = "C" };
        UnitTestData dataNull = new UnitTestData() { CatalogName = null };
        UnitTestData dataBlank = new UnitTestData() { CatalogName = String.Empty };

        [Test()]
        public void DbCatalogNameTest()
        {
            Assert.IsTrue(new DbCatalogName(dataA1) is DbCatalogName, "new DbCatalogName(dataA1) is DbCatalogName");
            Assert.IsTrue(new DbCatalogName(dataA1) is IDbCatalogName, "new DbCatalogName(dataA1) is IDbCatalogName");
            Assert.IsTrue(dataA1.ToCatalogName() is IDbCatalogName, "dataA1.ToCatalogName() is IDbCatalogName");
        }

        [Test()]
        public void EqualsTest()
        {
            Assert.IsTrue(new DbCatalogName(dataA1).Equals(dataA1), "new DbCatalogName(dataA1).Equals(dataA1)");
            Assert.IsTrue(new DbCatalogName(dataA1).Equals(new DbCatalogName(dataA2)), "new DbCatalogName(dataA1).Equals(new DbCatalogName(dataA2))");

            Assert.IsTrue(new DbCatalogName(dataA1) == new DbCatalogName(dataA1), "new DbCatalogName(dataA1) == new DbCatalogName(dataA1)");
            Assert.IsTrue(new DbCatalogName(dataA1) == new DbCatalogName(dataA2), "new DbCatalogName(dataA1) == new DbCatalogName(dataA2)");
            Assert.IsTrue(new DbCatalogName(dataA1) == dataA1, "new DbCatalogName(dataA1) == dataA1");

            Assert.IsFalse(new DbCatalogName(dataA1) == new DbCatalogName(dataC1), "new DbCatalogName(dataA1) != new DbCatalogName(dataC)");
            Assert.IsFalse(new DbCatalogName(dataNull) == new DbCatalogName(dataNull), "new DbCatalogName(dataNull) != new DbCatalogName(dataNull)");
            Assert.IsFalse(new DbCatalogName(dataBlank) == new DbCatalogName(dataBlank), "new DbCatalogName(dataBlank) != new DbCatalogName(dataBlank)");
        }

        [Test()]
        public void CompareToTest()
        {
            Assert.AreEqual(0, new DbCatalogName(dataA1).CompareTo(new DbCatalogName(dataA1)), "new DbCatalogName(dataA1).CompareTo(new DbCatalogName(dataA1))");
            Assert.AreEqual(0, new DbCatalogName(dataA1).CompareTo(new DbCatalogName(dataA2)), "new DbCatalogName(dataA1).CompareTo(new DbCatalogName(dataA2))");
            Assert.Greater(0, new DbCatalogName(dataA1).CompareTo(new DbCatalogName(dataC1)), "new DbCatalogName(dataA1).CompareTo(new DbCatalogName(dataC))");
            Assert.Less(0, new DbCatalogName(dataC1).CompareTo(new DbCatalogName(dataA1)), "new DbCatalogName(dataC).CompareTo(new DbCatalogName(dataA1))");

            Assert.IsTrue(new DbCatalogName(dataA1) < new DbCatalogName(dataC1), "new DbCatalogName(dataA1) < new DbCatalogName(dataC)");
            Assert.IsTrue(new DbCatalogName(dataC1) > new DbCatalogName(dataA1), "new DbCatalogName(dataC) > new DbCatalogName(dataA1)");

            Assert.IsTrue(new DbCatalogName(dataNull) < new DbCatalogName(dataA1), "new DbCatalogName(dataNull) < new DbCatalogName(dataA1)");
            Assert.IsTrue(new DbCatalogName(dataBlank) < new DbCatalogName(dataA1), "new DbCatalogName(dataBlank) < new DbCatalogName(dataA1)");
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
            { new DbCatalogName(dataA2), new DbCatalogName(dataC1), new DbCatalogName(dataA1) };

            catalogNames.Sort();

            Assert.AreEqual(catalogNames[0], dataA1);
            Assert.AreEqual(catalogNames[1], dataA2);
            Assert.AreEqual(catalogNames[2], dataC1);
        }
    }
}