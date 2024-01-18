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
        public void BindingTable_CopyData()
        {
            BindingTable<TestItem> source = new BindingTable<TestItem>() {
                new TestItem(){ TestTitle="Test 1", TestDescription = "Test 1", IsFilter = false },
                new TestItem(){ TestTitle="Test 2", TestDescription = "Test 2", IsFilter = true },
                new TestItem(){ TestTitle="Test 3", TestDescription = "Test 3", IsFilter = false },
                new TestItem(){ TestTitle="Test 4", TestDescription = "Test 4", IsFilter = true },
            };

            BindingTable<TestItem> result = new BindingTable<TestItem>();
            result.AddRange(source.Where(w => w.IsFilter == true));

            source.Clear(); // Get rid of source to show they are disconnected

            Assert.IsTrue(source.Count == 0);
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.All(w => w.IsFilter == true));

        }

        [TestMethod()]
        public void BindingTable_Disconnected()
        {
            BindingTable<TestItem> source = new BindingTable<TestItem>() {
                new TestItem(){ TestTitle="Test 1", TestDescription = "Test 1", IsFilter = false },
                new TestItem(){ TestTitle="Test 2", TestDescription = "Test 2", IsFilter = true },
                new TestItem(){ TestTitle="Test 3", TestDescription = "Test 3", IsFilter = false },
                new TestItem(){ TestTitle="Test 4", TestDescription = "Test 4", IsFilter = true },
            };

            TestItem newItem01 = new TestItem() { TestTitle = "Test 5", TestDescription = "Test 5", IsFilter = true };
            TestItem newItem02 = new TestItem() { TestTitle = "Test 6", TestDescription = "Test 6", IsFilter = true };

            Assert.AreEqual(DataRowState.Detached, newItem01.RowState());

            source.Add(newItem01);
            source.Add(newItem02);

            Assert.AreEqual(DataRowState.Added, newItem01.RowState());

            newItem01.Remove();  // Removed by BindgingTableRow
            source.Remove(newItem02); // Removed by BindingTable 

            Assert.AreEqual(DataRowState.Deleted, newItem01.RowState());
            Assert.AreEqual(DataRowState.Deleted, newItem02.RowState());

            // Value persists after delete, because it is only removed from the BindingTable
            Assert.AreEqual("Test 5", newItem01.TestTitle);

            source.Add(newItem02); // Can be re-added
            Assert.AreEqual(DataRowState.Added, newItem02.RowState());

            source.Clear();
        }

    }
}