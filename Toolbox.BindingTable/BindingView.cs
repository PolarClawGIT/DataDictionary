using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.BindingTable
{
    public class BindingView<TRow> : BindingList<TRow>
        where TRow : class, INotifyPropertyChanged
    {
        BindingList<TRow> baseData;

        public BindingView(BindingList<TRow> baseData, Func<TRow, Boolean> filter) : base()
        {
            this.baseData = baseData;

            foreach (TRow item in baseData.Where(filter).ToList())
            { base.InsertItem(base.Count, item); }

            this.AllowEdit = true;
            this.AllowNew = true;
            this.AllowRemove = true;
        }

        protected override object? AddNewCore()
        {
            Object? newValue = baseData.AddNew();
            return newValue;
        }

        protected override void ClearItems()
        {
            foreach (TRow item in this.ToList())
            { baseData.Remove(item); }

            base.ClearItems();
        }

        protected override void InsertItem(int index, TRow item)
        {
            baseData.Insert(baseData.Count(), item);
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            Int32 baseIndex = baseData.IndexOf(this[index]);
            if (baseIndex >= 0) { baseData.RemoveAt(baseIndex); }
            base.RemoveItem(index);
        }
    }
}
