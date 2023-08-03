using NUnit.Framework;
using Toolbox.BindingTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Toolbox.BindingTable.Tests
{
    [TestFixture()]
    public class BindingViewTests
    {
        class RowData : INotifyPropertyChanged
        {
            String data = String.Empty;
            public String Data
            {
                get { return data; }
                set { data = value; OnPropertyChanged(nameof(this.Data)); }
            }

            Boolean isTarget = true;
            public Boolean IsTarget
            {
                get { return isTarget; }
                set { isTarget = value; OnPropertyChanged(nameof(this.IsTarget)); }
            }

            public event PropertyChangedEventHandler? PropertyChanged;
            public virtual void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged is PropertyChangedEventHandler handler)
                { handler(this, new PropertyChangedEventArgs(propertyName)); }
            }
        }

        BindingList<RowData> unitTestData = new BindingList<RowData>()
        {
            new RowData(){ Data = "Base Row 1", IsTarget = true },
            new RowData(){ Data = "Base Row 2", IsTarget = false },
            new RowData(){ Data = "Base Row 3", IsTarget = true },
            new RowData(){ Data = "Base Row 4", IsTarget = false },
        };

        [Test()]
        public void BindingViewTest_Constructor()
        {
            Int32 expectedCount = unitTestData.Count(w => w.IsTarget);
            BindingView<RowData> data = new BindingView<RowData>(unitTestData, w => w.IsTarget);
            Assert.IsTrue(data.Count == expectedCount);

            foreach (RowData item in data)
            { Assert.IsTrue(unitTestData.Contains(item)); }
        }

        [Test()]
        public void BindingViewTest_Add()
        {
            BindingView<RowData> data = new BindingView<RowData>(unitTestData, w => w.IsTarget);

            RowData newItem = new RowData() { Data = "Add Row 1", IsTarget = false };
            data.Add(newItem);

            Assert.IsTrue(unitTestData.Contains(newItem));
        }

        [Test()]
        public void BindingViewTest_Remove()
        {
            BindingView<RowData> data = new BindingView<RowData>(unitTestData, w => w.IsTarget);

            RowData removeItem = unitTestData.First( w=> w.IsTarget);
            data.Remove(removeItem);

            Assert.IsFalse(data.Contains(removeItem));
            Assert.IsFalse(unitTestData.Contains(removeItem));
        }

        [Test()]
        public void BindingViewTest_Clear()
        {
            BindingView<RowData> data = new BindingView<RowData>(unitTestData, w => w.IsTarget);

            data.Clear();

            Assert.IsTrue(unitTestData.Count(w => w.IsTarget) == 0);
            Assert.IsTrue(data.Count() == 0);
        }
    }
}