using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDictionary.DataLayer.DatabaseData.Schema;

namespace DataDictionary.DataLayer.DatabaseData.Tests
{
    [TestFixture()]
    public class DbSchemaItemExtensionTests
    {
        class UnitTestData : IDbSchemaKey
        {
            public String? CatalogName { get; init; }
            public String? SchemaName { get; init; }
        }

        UnitTestData dataA1 = new UnitTestData() { CatalogName = "A", SchemaName = "A" };
        UnitTestData dataC1 = new UnitTestData() { CatalogName = "A", SchemaName = "C" };
        UnitTestData dataC2 = new UnitTestData() { CatalogName = "C", SchemaName = "C" };
        UnitTestData dataNull = new UnitTestData() { CatalogName = null, SchemaName = null };
        UnitTestData dataBlank = new UnitTestData() { CatalogName = String.Empty, SchemaName = String.Empty };


    }
}