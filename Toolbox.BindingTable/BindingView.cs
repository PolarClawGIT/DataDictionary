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
        where TRow : class, IBindingPropertyChanged
    {
        Func<Int32> BaseCount { get; set; }
        Action<Int32, TRow> BaseInsert { get; set; }
        Func<TRow, Int32> BaseIndexOf { get; set; }
        Func<TRow, Boolean> BaseRemove { get; set; }
        Action<Int32> BaseRemoveAt { get; set; }

        //IList<TRow> SourceData;
        Func<TRow, Boolean> FilterBy { get; set; }
        Func<TRow, Object> OrderBy { get; set; }

        List<TRow> directAdd = new List<TRow>(); // Contains a list of items added directly to the BindingView so they are not filterd out.

        /// <summary>
        /// Constructor for a BindingView.
        /// </summary>
        /// <param name="baseData"></param>
        /// <param name="filter">default is no items</param>
        /// <param name="orderBy">default is no order</param>
        public BindingView(IList<TRow> baseData, Func<TRow, Boolean>? filter = null, Func<TRow, Object>? orderBy = null) : base()
        {
            BaseCount = () => baseData.Count;
            BaseInsert = baseData.Insert;
            BaseIndexOf = baseData.IndexOf;
            BaseRemove = baseData.Remove;
            BaseRemoveAt = baseData.RemoveAt;

            FilterBy = filter ?? (f => 1 == 2);
            OrderBy = orderBy ?? (o => 1);

            foreach (TRow item in baseData.Where(FilterBy).OrderBy(OrderBy).ToList())
            { base.InsertItem(base.Count, item); }

            this.AllowEdit = true;
            this.AllowNew = true;
            this.AllowRemove = true;

            // Special handing for IBindingList
            if (baseData is IBindingList bindingList)
            { bindingList.ListChanged += BindingList_ListChanged; }

        }

        /// <summary>
        /// Handle BindingList changes to reflect changes into this object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BindingList_ListChanged(Object? sender, ListChangedEventArgs e)
        {
            if (sender is IList<TRow> data)
            {
                List<TRow> targetState = data.
                    Where(FilterBy).
                    OrderBy(OrderBy).
                    Union(directAdd).
                    ToList();
                List<TRow> toInsert = targetState.
                    Except(this, ReferenceEqualityComparer.Instance).
                    OfType<TRow>().
                    ToList();
                List<TRow> toDelete = this.
                    Except(targetState, ReferenceEqualityComparer.Instance).
                    OfType<TRow>().
                    ToList();

                if (e.ListChangedType is ListChangedType.ItemAdded or ListChangedType.Reset)
                {
                    foreach (var item in toInsert)
                    { base.InsertItem(base.Count, item); }
                }

                if (e.ListChangedType is ListChangedType.ItemDeleted or ListChangedType.Reset)
                {
                    foreach (var item in toDelete)
                    { base.RemoveItem(this.IndexOf(item)); }
                }
            }
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
                BaseInsert(BaseCount(), addNewCoreItem);
                addNewCoreItem = null;
            }

            base.EndNew(itemIndex);
        }

        protected override void ClearItems()
        {
            foreach (TRow item in this.ToList())
            { BaseRemove(item); }

            base.ClearItems();
        }

        protected override void InsertItem(int index, TRow item)
        {
            if (!directAdd.Contains(item))
            { directAdd.Add(item); }

            base.InsertItem(base.Count, item);

            if (!isAddNewCore)
            { BaseInsert(BaseCount(), item); } // Causes ListChange event to occur on base.
        }

        protected override void RemoveItem(int index)
        {
            if (directAdd.Contains(this[index]))
            { directAdd.Remove(this[index]); }

            Int32 baseIndex = BaseIndexOf(this[index]);
            base.RemoveItem(index);
            
            if (baseIndex >= 0) { BaseRemoveAt(baseIndex); } // Causes ListChange event to occur on base.
        }

        /// <summary>
        /// Calls ResetBindings then forces a ListChangedType.Reset.
        /// </summary>
        public void ResetList()
        {
            // For some reason, ResetBindings does not call ListChangedType.Reset under all conditions.
            // The documentation and source code found says otherwise.
            // This could cause a double call to ListChangedType.Reset.
            // The only guess I got has to do with multi-threading not raising the event as expected.
            ResetBindings();

            if(RaiseListChangedEvents)
            { OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1)); }
        }

    }
}
