using Microsoft.VisualStudio.TestTools.UnitTesting;
using Toolbox.BindingTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace Toolbox.BindingTable.Tests
{
    [TestClass()]
    public class BindingTableTests
    {
        class TestItem : BindingTableRow
        {
            public Guid? TestId { get { return GetValue<Guid>("TestId"); } protected set { SetValue("TestId", value); } }
            public string? TestTitle { get { return GetValue("TestTitle"); } set { SetValue("TestTitle", value); } }
            public string? TestDescription { get { return GetValue("TestDescription"); } set { SetValue("TestDescription", value); } }
            public bool? IsFilter { get => GetValue<Boolean>("IsFilter", BindingItemParsers.BooleanTryParse); set { SetValue<Boolean>("IsFilter", value); } }

            public TestItem() : base()
            { TestId = Guid.NewGuid(); }

            static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
            {
                new DataColumn("TestId", typeof(Guid)){ AllowDBNull = false},
                new DataColumn("TestTitle", typeof(string)){ AllowDBNull = true},
                new DataColumn("TestDescription", typeof(string)){ AllowDBNull = true},
                new DataColumn("IsFilter", typeof(Boolean)){ AllowDBNull = true},
            };

            public override IReadOnlyList<DataColumn> ColumnDefinitions()
            { return columnDefinitions; }
        }

        [TestMethod()]
        public void BindingTable_CTOR()
        {
            BindingTable<TestItem> source = new BindingTable<TestItem>() {
                new TestItem(){ TestTitle="Test 1", TestDescription = "Test 1", IsFilter = false },
                new TestItem(){ TestTitle="Test 2", TestDescription = "Test 2", IsFilter = true },
                new TestItem(){ TestTitle="Test 3", TestDescription = "Test 3", IsFilter = false },
                new TestItem(){ TestTitle="Test 4", TestDescription = "Test 4", IsFilter = true },
            };

            Assert.IsTrue(source.Count == 4);
        }

        [TestMethod()]
        public void BindingTable_CTOR_fromSource()
        {
            BindingTable<TestItem> source = new BindingTable<TestItem>() { 
                new TestItem(){ TestTitle="Test 1", TestDescription = "Test 1", IsFilter = false },
                new TestItem(){ TestTitle="Test 2", TestDescription = "Test 2", IsFilter = true },
                new TestItem(){ TestTitle="Test 3", TestDescription = "Test 3", IsFilter = false },
                new TestItem(){ TestTitle="Test 4", TestDescription = "Test 4", IsFilter = true },
            };

            BindingTable<TestItem> result = new BindingTable<TestItem>(source.Where(w => w.IsFilter == true));

            source.Clear(); // Get rid of source to show they are disconnected

            Assert.IsTrue(source.Count == 0);
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.All(w => w.IsFilter == true));

        }
    }
}