using System.ComponentModel;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    /// <summary>
    /// BindingList that supports ComboBox functionality.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class BindingComboList<T> : BindingList<T>
    {
        /// <summary>
        /// Switchs the order of two items in the list and calls the Moved ChangedList event.
        /// </summary>
        /// <param name="oldIndex"></param>
        /// <param name="newIndex"></param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If old or new index is out of range.
        /// </exception>
        public void Move(Int32 oldIndex, Int32 newIndex)
        {
            if (oldIndex > Count || oldIndex < 0 || newIndex > Count || newIndex < 0 || oldIndex == newIndex)
            {
                Exception ex = new ArgumentOutOfRangeException();
                ex.Data.Add(nameof(oldIndex), oldIndex);
                ex.Data.Add(nameof(newIndex), newIndex);
                ex.Data.Add(nameof(Count), Count);
                throw ex;
            }

            Boolean raiseChanged = RaiseListChangedEvents;

            T item = this[oldIndex];
            RemoveAt(oldIndex);

            if (oldIndex >= newIndex)
            { InsertItem(newIndex, item); }
            else { InsertItem(newIndex - 1, item); }

            if (raiseChanged)
            { RaiseListChangedEvents = raiseChanged; }

            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemMoved, newIndex, oldIndex));
        }

        /// <summary>
        /// Sorts the List using the passed function.
        /// Calls Move for each item that changed order.
        /// </summary>
        /// <param name="keySelector">Linq.Order by Key Selector.</param>
        /// <remarks>This allows for simple ordering using Linq.OrderBy.</remarks>
        public void SortBy(Func<T, Object> keySelector)
        {
            List<T> target = this.OrderBy(keySelector).ToList();

            foreach (T item in target)
            {
                Int32 oldIndex = this.IndexOf(item);
                Int32 newIndex = target.IndexOf(item);

                if (oldIndex != newIndex)
                { Move(oldIndex, newIndex); }
            }
        }

        /// <summary>
        /// Reorders the list into the order of the values passed.
        /// </summary>
        /// <param name="values">The list of values sorted in the disired order.</param>
        /// <remarks>This allows for complex order.</remarks>
        public void OrderAs(IEnumerable<T> values)
        {
            List<T> target = values.ToList();

            foreach (var item in values)
            {
                Int32 oldIndex = this.IndexOf(item);
                Int32 newIndex = target.IndexOf(item);

                if (oldIndex != newIndex && oldIndex >= 0 && newIndex >=0)
                { Move(oldIndex, newIndex); }
            }
        }
    }
}
