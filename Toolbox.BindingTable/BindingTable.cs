using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Toolbox.BindingTable
{
    /// <summary>
    /// The BindingTable represent a class that wrappers a DataTable and presents it as if it was a Binding List.
    /// </summary>
    /// <typeparam name="TBindingItem">the mapping class between the DataRow and the POCO like object used in code.</typeparam>
    public class BindingTable<TBindingItem> : BindingList<TBindingItem>, IBindingTable<TBindingItem>
        where TBindingItem : BindingTableRow, INotifyPropertyChanged, IBindingTableRow, new()
    {
        /// <summary>
        /// Internal DataTable that hold the values.
        /// </summary>
        protected DataTable dataItems;

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        public BindingTable() : base()
        {
            dataItems = new DataTable();
            dataItems.TableName = typeof(TBindingItem).Name;
            foreach (DataColumn item in new TBindingItem().ColumnDefinitions())
            {
                using (DataColumn column = new DataColumn(item.ColumnName, item.DataType)
                { AllowDBNull = item.AllowDBNull, Caption = item.Caption, DefaultValue = item.DefaultValue, })
                { dataItems.Columns.Add(column); }
            }

            dataItems.Disposed += TableDisposed;

            dataItems.RowChanged += TableRowChanged;
            dataItems.RowChanging += TableRowChanging;
            dataItems.RowDeleting += TableRowDeleting;
            dataItems.RowDeleted += TableRowDeleted;

            dataItems.ColumnChanged += TableColumnChanged;
            dataItems.ColumnChanging += TableColumnChanging;

            dataItems.TableCleared += TableCleared;
            dataItems.TableClearing += TableClearing;
            dataItems.TableNewRow += TableNewRow;

            this.AllowEdit = true;
            this.AllowNew = true;
            this.AllowRemove = true;
        }

        #region DataTable Events
        protected virtual void TableNewRow(object sender, DataTableNewRowEventArgs e)
        { /*throw new NotImplementedException();*/ }

        protected virtual void TableClearing(object sender, DataTableClearEventArgs e)
        { /*throw new NotImplementedException();*/ }

        protected virtual void TableCleared(object sender, DataTableClearEventArgs e)
        { /*throw new NotImplementedException();*/ }

        protected virtual void TableColumnChanging(object sender, DataColumnChangeEventArgs e)
        { /*throw new NotImplementedException();*/ }

        protected virtual void TableColumnChanged(object sender, DataColumnChangeEventArgs e)
        { /*throw new NotImplementedException();*/ }

        protected virtual void TableRowChanging(object sender, DataRowChangeEventArgs e)
        { /*throw new NotImplementedException();*/ }

        protected virtual void TableRowChanged(object sender, DataRowChangeEventArgs e)
        { /*throw new NotImplementedException();*/ }

        protected virtual void TableRowDeleting(object sender, DataRowChangeEventArgs e)
        { /*throw new NotImplementedException();*/ }

        protected virtual void TableRowDeleted(object sender, DataRowChangeEventArgs e)
        { /*throw new NotImplementedException();*/ }

        protected virtual void TableDisposed(object? sender, EventArgs e)
        { /*throw new NotImplementedException();*/ }
        #endregion

        #region DataTable
        /// <inheritdoc cref="DataTable.HasErrors"/>
        public virtual Boolean HasErrors { get { return dataItems.HasErrors; } }

        /// <inheritdoc cref="DataTable.Load(IDataReader)"/>
        public virtual void Load(IDataReader reader)
        { this.Load(reader, LoadOption.PreserveChanges, null); }

        /// <inheritdoc cref="DataTable.Load(IDataReader, LoadOption)"/>
        public virtual void Load(IDataReader reader, LoadOption loadOption)
        { this.Load(reader, loadOption, null); }

        /// <inheritdoc cref="DataTable.Load(IDataReader, LoadOption, FillErrorEventHandler?)"/>
        public virtual void Load(IDataReader reader, LoadOption loadOption, FillErrorEventHandler? errorHandler)
        {
            using (DataTable newData = new DataTable() { TableName = typeof(TBindingItem).Name })
            {
                // Load to a work table
                try
                { newData.Load(reader, loadOption, handleError); }
                catch (Exception ex)
                {
                    ex.Data.Add(nameof(newData.TableName), newData.TableName);
                    throw;
                }

                // Transfer the work table to the data table, building the Binding Rows as we go.
                foreach (DataRow row in newData.Rows)
                {
                    try
                    { dataItems.ImportRow(row); }
                    catch (Exception ex)
                    {
                        ex.Data.Add(nameof(newData.TableName), newData.TableName);

                        foreach (DataColumn column in row.Table.Columns)
                        { ex.Data.Add(String.Format("Data- {0}", column.ColumnName), row[column]); }

                        throw;
                    }

                    DataRow newRow = dataItems.Rows[(dataItems.Rows.Count - 1)]; // new rows is always added at the end
                    TBindingItem newItem = new TBindingItem();
                    newItem.SetRow(newRow);
                    this.Add(newItem);
                }
            }

            void handleError(object sender, FillErrorEventArgs e)
            { // TODO: Figure out how to capture the row that thru the exception. Flag the Row using Rows[x].Errors if handled.
                if (errorHandler != null) { errorHandler(sender, e); }
                else if (e.Errors is not null) { throw e.Errors; } // TODO: Consider throwing only one error instead of one for each row?
            }
        }

        /// <inheritdoc cref="DataTable.AcceptChanges"/>
        public virtual void AcceptChanges()
        {
            foreach (TBindingItem item in this.Where(w => !Object.ReferenceEquals(w, addNewCoreItem)))
            { if (item.GetRow() is DataRow row) { row.AcceptChanges(); } }
        }

        /// <inheritdoc cref="DataTable.RejectChanges"/>
        public virtual void RejectChanges()
        {
            DataTable? deleted = dataItems.GetChanges(DataRowState.Deleted);
            List<TBindingItem> inserted = this.Where(w => w.RowState() == DataRowState.Added).ToList();
            List<TBindingItem> modified = this.Where(w => w.RowState() == DataRowState.Modified).ToList();
            List<TBindingItem> detached = this.Where(w => w.RowState() == DataRowState.Detached).ToList();

            OnListChangedEnabled = false;

            if (deleted is DataTable)
            {
                foreach (DataRow row in deleted.Rows)
                {
                    TBindingItem item = new TBindingItem();
                    row.RejectChanges();
                    item.SetRow(row);
                    this.Add(item);
                }
            }

            foreach (TBindingItem item in inserted)
            {
                this.Remove(item);
                if (item.GetRow() is DataRow row) { row.RejectChanges(); }
            }

            foreach (TBindingItem item in modified)
            { if (item.GetRow() is DataRow row) { row.RejectChanges(); } }

            if (this.isSorted && this.SortPropertyCore is PropertyDescriptor)
            { this.ApplySortCore(this.SortPropertyCore, this.SortDirectionCore); }

            OnListChangedEnabled = true;
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
        #endregion

        #region BindingList
        TBindingItem? addNewCoreItem = null; // Track the extra row created by DataGridView.
        Boolean isAddNewCore = false; // Tracks if AddNewCore is being executed. We are dealing with a fake DataGridView Row.

        /// <remarks>
        /// This is normally called by DataGridView to create a new row that is not yet part of the DataGridView.
        /// The addNewCoreItem item tracks this row that is pending. 
        /// The isAddingNewCore tracks if the AddNewCore is doing the work. 
        /// The flow of this method is not easy to track. 
        /// It is not called by the BindingList.Add.
        /// InsertItem is called as part of the base call.
        /// </remarks>
        protected override Object? AddNewCore()
        {
            isAddNewCore = true;
            Object? item = base.AddNewCore();

            if (item is TBindingItem newItem) { addNewCoreItem = newItem; }
            isAddNewCore = false;

            return item;
        }

        /// <remarks>
        /// This is called if the DataGridView does not commit the new item.
        /// Example: closing the Form or pressing Escape on the DataGridView.
        /// The fake DataGridView row is removed from the data.
        /// It is not called by the BindingList.Add.
        /// </remarks>
        public override void CancelNew(Int32 itemIndex)
        {
            if (itemIndex >= 0 && this[itemIndex] == addNewCoreItem)
            { addNewCoreItem = null; }

            base.CancelNew(itemIndex);
        }

        protected override void ClearItems()
        {
            dataItems.Clear();
            base.ClearItems();
        }

        /// <remarks>
        /// This gets called several times during the process of the DataGridView working with a new row.
        /// The first time is as part of the AddNewCore. At this point, the row is not real. Don't do anything to the row.
        /// The second time it is when it is committing the insert. At this point, the row is real and needs to be part of the DataTable
        /// The method can be called several more times before Adding or Canceling a new Row. I don't know what causes this or how to handle these calls.
        /// It is not called by the BindingList.Add.
        /// </remarks>
        public override void EndNew(Int32 itemIndex)
        {
            if (itemIndex >= 0 && this[itemIndex] == addNewCoreItem && !isAddNewCore)
            { // Item is not being canceled or executing AddNewCore, finish adding the item.
                try
                {
                    dataItems.ImportRow(addNewCoreItem.GetRow());
                    addNewCoreItem.BindingTable = this;
                    addNewCoreItem.SetRow(dataItems.Rows[dataItems.Rows.Count - 1]);
                }
                catch (Exception ex)
                {
                    foreach (DataColumn columnItem in addNewCoreItem.GetRow().Table.Columns)
                    { ex.Data.Add(columnItem.ColumnName, addNewCoreItem.GetRow()[columnItem]); }

                    throw;
                }

                addNewCoreItem = null;
            }

            base.EndNew(itemIndex);
        }

        protected override void InsertItem(Int32 index, TBindingItem item)
        {
            DataRow itemRow = item.GetRow();
            
            try
            {
                if (!!ReferenceEquals(itemRow.Table, dataItems) || !isAddNewCore)
                { // Replace the row data with a copy that belongs to this table
                    dataItems.ImportRow(itemRow);
                    item.BindingTable = this;
                    item.SetRow(dataItems.Rows[dataItems.Rows.Count - 1]);
                }
            }
            catch (Exception ex)
            {
                foreach (DataColumn columnItem in itemRow.Table.Columns)
                { ex.Data.Add(columnItem.ColumnName, itemRow[columnItem]); }

                throw;
            }

            base.InsertItem(index, item);
        }

        protected override void OnAddingNew(AddingNewEventArgs e)
        { base.OnAddingNew(e); }

        protected override Boolean SupportsChangeNotificationCore => true;

        /// <summary>
        /// Used to temporary disable or enable the Change List event.
        /// This allows a set of changes to be made before calling OnListChanged
        /// so that they are treated as a single logical change.
        /// </summary>
        protected virtual Boolean OnListChangedEnabled { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <remarks>
        /// This can raise an Thread exception when a Form has a bound object, usually a DataGridView.
        /// To address this, the Form needs to disconnect from the binding source and re-connect after the operation is complete.
        /// </remarks>
        protected override void OnListChanged(ListChangedEventArgs e)
        { if (OnListChangedEnabled) { base.OnListChanged(e); } }

        protected override void RemoveItem(Int32 index)
        {
            if (this[index].GetRow() is DataRow row && row.RowState != DataRowState.Detached)
            { row.Delete(); }

            base.RemoveItem(index);
        }

        protected override void SetItem(Int32 index, TBindingItem item)
        { base.SetItem(index, item); }

        #region Sorting and Searching
        protected override Boolean SupportsSortingCore => true;
        protected override Boolean SupportsSearchingCore => true;

        /// <summary>
        /// Backing attribute for IsSortedCore property.
        /// </summary>
        Boolean isSorted = false;
        protected override Boolean IsSortedCore { get { return isSorted; } }

        /// <summary>
        /// Backing attribute for SortDirectionCore property.
        /// </summary>
        ListSortDirection sortDirection = ListSortDirection.Ascending;
        protected override ListSortDirection SortDirectionCore { get { return sortDirection; } }

        /// <summary>
        /// Backing attribute for SortPropertyCore property.
        /// </summary>
        PropertyDescriptor? sortProrty = null;
        protected override PropertyDescriptor? SortPropertyCore { get { return sortProrty; } }

        /// <summary>
        /// Sets the behavior of String Comparisons for Sort and Search on Properties
        /// If the Property is a String or is not IComparable then strings are sorted or searched using this setting.
        /// If the Property is not a String and is IComparable then IComparable is used.
        /// </summary>
        public virtual StringComparison CompareString { get; set; } = StringComparison.CurrentCulture;

        /// <summary>
        /// Contains the Comparer used for Sorting and Searching.
        /// The default Comparer uses: SortPropertyCore, SortDirectionCore and StringComparison.
        /// </summary>
        /// <remarks>
        /// Overriding this property allows a different comparer method to be used.
        /// </remarks>
        protected virtual IComparer<TBindingItem> Comparer
        {
            get
            {
                return new DefaultComparer()
                {
                    Property = SortPropertyCore,
                    Direction = SortDirectionCore,
                    StringComparison = CompareString
                };
            }
        }

        /// <summary>
        /// Class Defining the default behavior of the Compare operator used for sorting.
        /// </summary>
        /// <remarks>
        /// Override the Comparer property to use a different Compare class.
        /// This class can be used as a starter point.
        /// </remarks>
        protected class DefaultComparer : Comparer<TBindingItem>
        {
            public PropertyDescriptor? Property { get; init; }
            public required ListSortDirection Direction { get; init; }
            public required StringComparison StringComparison { get; init; }

            public override int Compare(TBindingItem? x, TBindingItem? y)
            {
                if (Property is PropertyDescriptor)
                {
                    if (Property.GetValue(x) is String xString && Property.GetValue(y) is String yString)
                    {
                        if (Direction == ListSortDirection.Descending)
                        { return String.Compare(xString, yString, StringComparison); }
                        else { return String.Compare(yString, xString, StringComparison); }
                    }
                    else if (Property.GetValue(x) is IComparable xValue && Property.GetValue(y) is IComparable yValue)
                    { // Base type is IComparable, use that
                        if (Direction == ListSortDirection.Descending)
                        { return xValue.CompareTo(yValue); }
                        else { return yValue.CompareTo(xValue); }
                    }
                    else if (Property.GetValue(x) is Object xObject && Property.GetValue(y) is Object yObject)
                    { // Base Type is not IComparable, use the String Compare on the ToString
                        if (Direction == ListSortDirection.Descending)
                        { return String.Compare(xObject.ToString(), yObject.ToString(), StringComparison); }
                        else { return String.Compare(yObject.ToString(), xObject.ToString(), StringComparison); }
                    }
                    else { return 0; }
                }
                else { return 0; }
            }
        }

        /// <summary>
        /// Applies the Sort to the data.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="direction"></param>
        /// <remarks>
        /// The items are moved within the collection (add/remove item is not called).
        /// This returns a OnListChange event of ItemMoved.
        /// </remarks>
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            sortDirection = direction;
            sortProrty = prop;

            List<TBindingItem> newOrder = this.Order(Comparer).ToList();

            // Reorder this list without calling insert/remove
            for (int newIndex = 0; newIndex < this.Count; newIndex++)
            {
                Int32 oldIndex = this.IndexOf(newOrder[newIndex]);
                if (oldIndex != newIndex)
                { // Switch the two items
                    this[oldIndex] = this[newIndex];
                    this[newIndex] = newOrder[newIndex];
                    OnListChanged(new ListChangedEventArgs(ListChangedType.ItemMoved, newIndex, oldIndex));
                }
            }

            isSorted = true;
        }

        /// <summary>
        /// Removes the Sort from the data.
        /// </summary>
        /// <remarks>
        /// The order is reset to match the order of the internal Data Table.
        /// The items are moved within the collection (add/remove item is not called).
        /// This returns a OnListChange event of ItemMoved.
        /// </remarks>
        protected override void RemoveSortCore()
        {
            sortProrty = null;

            List<TBindingItem> newOrder = new List<TBindingItem>();

            // Set the order to be the same as the DataTable
            foreach (DataRow item in dataItems.Rows)
            { newOrder.Add(this.First(w => Object.ReferenceEquals(w.GetRow(), item))); }

            // Reorder this list without calling insert/remove
            for (int newIndex = 0; newIndex < this.Count; newIndex++)
            {
                Int32 oldIndex = this.IndexOf(newOrder[newIndex]);
                if (oldIndex != newIndex)
                { // Switch the two items
                    this[oldIndex] = this[newIndex];
                    this[newIndex] = newOrder[newIndex];
                    OnListChanged(new ListChangedEventArgs(ListChangedType.ItemMoved, newIndex, oldIndex));
                }
            }

            isSorted = false;
        }

        /// <summary>
        /// Performs the Find operation on the dataset (overriding the base Find).
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        protected override int FindCore(PropertyDescriptor prop, Object key)
        {
            //TODO: This is probably incomplete and may not work. Check for IEquatable and handle String with StringComparison.
            if (this.FirstOrDefault(w => prop.GetValue(w) == key) is TBindingItem value)
            { return this.IndexOf(value); }
            else { return -1; }
        }
        #endregion
        #endregion

        #region IDisposable
        /// <summary>
        /// Flag used by the Dispose method.
        /// </summary>
        private Boolean disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    dataItems.Disposed -= TableDisposed;

                    dataItems.RowChanged -= TableRowChanged;
                    dataItems.RowChanging -= TableRowChanging;
                    dataItems.RowDeleting -= TableRowDeleting;
                    dataItems.RowDeleted -= TableRowDeleted;

                    dataItems.ColumnChanged -= TableColumnChanged;
                    dataItems.ColumnChanging -= TableColumnChanging;

                    dataItems.TableCleared -= TableCleared;
                    dataItems.TableClearing -= TableClearing;
                    dataItems.TableNewRow -= TableNewRow;

                    dataItems.Clear();
                    dataItems.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public virtual void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region ICloneable
        public virtual object Clone()
        {
            BindingTable<TBindingItem> result = new BindingTable<TBindingItem>();
            result.Load(dataItems.CreateDataReader());
            return result;
        }
        #endregion
    }
}
