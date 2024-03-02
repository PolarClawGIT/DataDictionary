using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.BindingTable
{
    /// <summary>
    /// Wrapper class around a BindingList where a sub-set of the base BindingList is to be used.
    /// </summary>
    /// <typeparam name="TRow"></typeparam>
    /// <remarks>
    /// This class is intended to perform adds, updates, and deletes against a base BindingList.
    /// However, it is filtered to a sub-set of the base.
    /// The rows are pointers to the rows in the base BindingList. Updates are being applied against the same object.
    /// Insert and Deletes are rigged to Insert and Update to both the base and the local list.
    /// </remarks>
    public class BindingView<TRow> : BindingList<TRow>
        where TRow : class, INotifyPropertyChanged
    {
        IBindingList<TRow> baseData;

        //public Action<TRow>? OnInsert { get; init; }

        public BindingView(IBindingList<TRow> baseData, Func<TRow, Boolean> filter) : base()
        {
            this.baseData = baseData;

            foreach (TRow item in baseData.Where(filter).ToList())
            { base.InsertItem(base.Count, item); }

            this.AllowEdit = true;
            this.AllowNew = true;
            this.AllowRemove = true;
        }


        TRow? addNewCoreItem = null; // Track the extra row created by DataGridView.
        Boolean isAddNewCore = false; // Tracks if AddNewCore is being executed. We are dealing with a fake DataGridView Row.

        /// <summary>
        /// Adds a new item to the end of the collection.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// This is normally called by DataGridView to create a new row that is not yet part of the DataGridView.
        /// The addNewCoreItem item tracks this row that is pending. 
        /// The isAddingNewCore tracks if the AddNewCore is doing the work. 
        /// The flow of this method is not easy to track. 
        /// It is not called by the BindingList.Add.
        /// InsertItem is called as part of the base call.
        /// </remarks>
        protected override object? AddNewCore()
        {
            isAddNewCore = true;
            Object? newValue = base.AddNewCore();

            if (newValue is TRow value) { addNewCoreItem = value; }

            isAddNewCore = false;
            return newValue;
        }

        /// <summary>
        /// Discards a pending new item.
        /// </summary>
        /// <param name="itemIndex"></param>
        /// <remarks>
        /// This is called if the DataGridView does not commit the new item.
        /// Example: closing the Form or pressing Escape on the DataGridView.
        /// The fake DataGridView row is removed from the data.
        /// It is not called by the BindingList.Add.
        /// </remarks>
        public override void CancelNew(int itemIndex)
        {
            if (itemIndex >= 0 && this[itemIndex] == addNewCoreItem)
            { addNewCoreItem = null; }

            base.CancelNew(itemIndex);
        }

        /// <summary>
        /// Commits a pending new item to the collection.
        /// </summary>
        /// <param name="itemIndex"></param>
        /// <remarks>
        /// This gets called several times during the process of the DataGridView working with a new row.
        /// The first time is as part of the AddNewCore. At this point, the row is not real. Don't do anything to the row.
        /// The second time it is when it is committing the insert. At this point, the row is real and needs to be part of the DataTable
        /// The method can be called several more times before Adding or Canceling a new Row. I don't know what causes this or how to handle these calls.
        /// It is not called by the BindingList.Add.
        /// </remarks>
        public override void EndNew(int itemIndex)
        {
            if (itemIndex >= 0 && this[itemIndex] == addNewCoreItem && !isAddNewCore)
            {
                baseData.Insert(baseData.Count(), addNewCoreItem);
                addNewCoreItem = null;
            }

            base.EndNew(itemIndex);
        }

        protected override void ClearItems()
        {
            foreach (TRow item in this.ToList())
            { baseData.Remove(item); }

            base.ClearItems();
        }

        protected override void InsertItem(int index, TRow item)
        {
            if (!isAddNewCore)
            { baseData.Insert(baseData.Count(), item); }

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
