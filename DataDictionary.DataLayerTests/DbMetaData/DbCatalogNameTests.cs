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
    public class DbCatalogNameTests
    {
        class UnitTestData : IDbCatalogKeyUnique
        { public String? CatalogName { get; init; } }

        UnitTestData dataA1 = new UnitTestData() { CatalogName = "A" };
        UnitTestData dataA2 = new UnitTestData() { CatalogName = "a" };
        UnitTestData dataB1 = new UnitTestData() { CatalogName = "B" };
        UnitTestData dataC1 = new UnitTestData() { CatalogName = "C" };
        UnitTestData dataNull = new UnitTestData() { CatalogName = null };
        UnitTestData dataBlank = new UnitTestData() { CatalogName = String.Empty };

        [Test()]
        public void DbCatalogNameTest()
        {
            DbCatalogKeyUnique item = new DbCatalogKeyUnique(dataA1);

            Assert.IsTrue(item is DbCatalogKeyUnique, "new DbCatalogName(dataA1) is DbCatalogName");
            Assert.IsTrue(item is IDbCatalogKeyUnique, "new DbCatalogName(dataA1) is IDbCatalogName");
        }

        [Test()]
        public void EqualsTest()
        {
            //Assert.IsTrue(new DbCatalogKeyUnique(dataA1).Equals(dataA1), "new DbCatalogName(dataA1).Equals(dataA1)");
            Assert.IsTrue(new DbCatalogKeyUnique(dataA1).Equals(new DbCatalogKeyUnique(dataA2)), "new DbCatalogName(dataA1).Equals(new DbCatalogName(dataA2))");

            Assert.IsTrue(new DbCatalogKeyUnique(dataA1) == new DbCatalogKeyUnique(dataA1), "new DbCatalogName(dataA1) == new DbCatalogName(dataA1)");
            Assert.IsTrue(new DbCatalogKeyUnique(dataA1) == new DbCatalogKeyUnique(dataA2), "new DbCatalogName(dataA1) == new DbCatalogName(dataA2)");
            //Assert.IsTrue(new DbCatalogKeyUnique(dataA1) == dataA1, "new DbCatalogName(dataA1) == dataA1");

            Assert.IsFalse(new DbCatalogKeyUnique(dataA1) == new DbCatalogKeyUnique(dataC1), "new DbCatalogName(dataA1) != new DbCatalogName(dataC)");
            Assert.IsFalse(new DbCatalogKeyUnique(dataNull) == new DbCatalogKeyUnique(dataNull), "new DbCatalogName(dataNull) != new DbCatalogName(dataNull)");
            Assert.IsFalse(new DbCatalogKeyUnique(dataBlank) == new DbCatalogKeyUnique(dataBlank), "new DbCatalogName(dataBlank) != new DbCatalogName(dataBlank)");
        }

        [Test()]
        public void CompareToTest()
        {
            Assert.AreEqual(0, new DbCatalogKeyUnique(dataA1).CompareTo(new DbCatalogKeyUnique(dataA1)), "new DbCatalogName(dataA1).CompareTo(new DbCatalogName(dataA1))");
            Assert.AreEqual(0, new DbCatalogKeyUnique(dataA1).CompareTo(new DbCatalogKeyUnique(dataA2)), "new DbCatalogName(dataA1).CompareTo(new DbCatalogName(dataA2))");
            Assert.Greater(0, new DbCatalogKeyUnique(dataA1).CompareTo(new DbCatalogKeyUnique(dataC1)), "new DbCatalogName(dataA1).CompareTo(new DbCatalogName(dataC))");
            Assert.Less(0, new DbCatalogKeyUnique(dataC1).CompareTo(new DbCatalogKeyUnique(dataA1)), "new DbCatalogName(dataC).CompareTo(new DbCatalogName(dataA1))");

            Assert.IsTrue(new DbCatalogKeyUnique(dataA1) < new DbCatalogKeyUnique(dataC1), "new DbCatalogName(dataA1) < new DbCatalogName(dataC)");
            Assert.IsTrue(new DbCatalogKeyUnique(dataC1) > new DbCatalogKeyUnique(dataA1), "new DbCatalogName(dataC) > new DbCatalogName(dataA1)");

            Assert.IsTrue(new DbCatalogKeyUnique(dataNull) < new DbCatalogKeyUnique(dataA1), "new DbCatalogName(dataNull) < new DbCatalogName(dataA1)");
            Assert.IsTrue(new DbCatalogKeyUnique(dataBlank) < new DbCatalogKeyUnique(dataA1), "new DbCatalogName(dataBlank) < new DbCatalogName(dataA1)");
        }

        [Test()]
        public void GetHashCodeTest()
        {
            Assert.DoesNotThrow(() => new DbCatalogKeyUnique(dataA1).GetHashCode(), "dataA1.GetHashCode()");
            Assert.DoesNotThrow(() => new DbCatalogKeyUnique(dataC1).GetHashCode(), "dataC.GetHashCode()");
            Assert.DoesNotThrow(() => new DbCatalogKeyUnique(dataNull).GetHashCode(), "dataNull.GetHashCode()");
            Assert.DoesNotThrow(() => new DbCatalogKeyUnique(dataBlank).GetHashCode(), "dataBlank.GetHashCode()");
        }

        [Test()]
        public void ToStringTest()
        {
            Assert.DoesNotThrow(() => new DbCatalogKeyUnique(dataA1).ToString(), "dataA1.ToString()");
            Assert.DoesNotThrow(() => new DbCatalogKeyUnique(dataC1).ToString(), "dataC.ToString()");
            Assert.DoesNotThrow(() => new DbCatalogKeyUnique(dataNull).ToString(), "dataNull.ToString()");
            Assert.DoesNotThrow(() => new DbCatalogKeyUnique(dataBlank).ToString(), "dataBlank.ToString()");
        }

        [Test()]
        public void SortTest()
        {
            List<DbCatalogKeyUnique> items = new List<DbCatalogKeyUnique>();

            var d1 = new DbCatalogKeyUnique(dataA2);
            var d2 = new DbCatalogKeyUnique(dataC1);
            var d3 = new DbCatalogKeyUnique(dataNull);
            var d4 = new DbCatalogKeyUnique(dataB1);

            items.Add(d1);
            items.Add(d2);
            items.Add(d3);
            items.Add(d4);

            items.Sort();

            Assert.AreSame(items[0], d3);
            Assert.AreSame(items[1], d1);
            Assert.AreSame(items[2], d4);
            Assert.AreSame(items[3], d2);
        }
    }
}