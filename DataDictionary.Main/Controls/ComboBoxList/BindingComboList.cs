using DataDictionary.Resource;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    /// <summary>
    /// BindingList that supports ComboBox functionality.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class BindingComboList<T> : BindingList<T>
    {
        public Boolean TryGetSelected(ComboBox control, [NotNullWhen(true)] out T? result)
        {
            if (control.SelectedItem is T value)
            { result = value; return true; }
            else { result = default; return false; }
        }

        public Boolean TryGetSelected(ComboBoxData control, [NotNullWhen(true)] out T? result)
        {
            if (control.SelectedItem is T value)
            { result = value; return true; }
            else { result = default; return false; }
        }

        public Boolean TryGetSelected(DataGridViewComboBoxColumn control, [NotNullWhen(true)] out T? result)
        {
            if (control.DataGridView is DataGridView grid
                && grid.CurrentCell is DataGridViewCell cell
                && cell.Value is T value)
            { result = value; return true; }
            else { result = default; return false; }
        }

        public Boolean TrySetSelected(ComboBox control, Func<T, Boolean> selector)
        {
            if (this.FirstOrDefault(selector) is T value)
            { control.SelectedItem = value; return true; }
            else { return false; }
        }

        public Boolean TrySetSelected(ComboBoxData control, Func<T, Boolean> selector)
        {
            if (this.FirstOrDefault(selector) is T value)
            { control.SelectedItem = value; return true; }
            else { return false; }
        }

        public Boolean TrySetSelected(DataGridViewComboBoxColumn control, Func<T, Boolean> selector)
        {
            if (this.FirstOrDefault(selector) is T value
                && control.DataGridView is DataGridView grid
                && grid.CurrentCell is DataGridViewCell cell)
            { cell.Value = value; return true; }
            else { return false; }
        }

        public void BindTo(ComboBox control,
            Func<String> valueMember,
            Func<String> displayMember)
        {
            control.ValueMember = valueMember();
            control.DisplayMember = displayMember();
            control.DataSource = this;
        }

        public void BindTo(ComboBoxData control,
            Func<String> valueMember,
            Func<String> displayMember)
        {
            control.ValueMember = valueMember();
            control.DisplayMember = displayMember();
            control.DataSource = this;
        }

        public void BindTo(DataGridViewComboBoxColumn control,
            Func<String> valueMember,
            Func<String> displayMember)
        {
            control.ValueMember = valueMember();
            control.DisplayMember = displayMember();
            control.DataSource = this;
        }

        public void BuildList<TSource>(
            IBindingList<TSource> source,
            Func<TSource, T> constructor,
            Action<TSource, T>? onItemChanged,
            Func<TSource, Boolean>? filterBy,
            Func<T, Object>? orderBy,
            Func<T, T, Boolean>? areEquel,
            Func<T>? emptyValue)
            where TSource : IBindingPropertyChanged
        {
            filterBy = filterBy ?? (f => 1 == 1);
            orderBy = orderBy ?? (o => 1);
            areEquel = areEquel ?? ((a, b) => false);
            onItemChanged = onItemChanged ?? ((t, s) => { });
            Func<IReadOnlyList<T>> getList = () => source.
                    Where(w => filterBy(w)).
                    Select(s => constructor(s)).
                    OrderBy(orderBy).
                    ToList().
                    AsReadOnly();

            Boolean isRaised = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            T? emptyItem = default;
            Clear();

            if (emptyValue is not null)
            {
                emptyItem = emptyValue();
                Add(emptyItem);
            }

            this.AddRange(getList());

            source.ListChanged += ListChanged;
            if (isRaised) { RaiseListChangedEvents = true; }
            ResetBindings();

            void ListChanged(Object? sender, ListChangedEventArgs e)
            {
                switch (e.ListChangedType)
                {
                    case ListChangedType.Reset:
                        isRaised = RaiseListChangedEvents;
                        RaiseListChangedEvents = false;
                        this.RemoveRange(this.
                            Where(w => !(emptyItem is not null && areEquel(w, emptyItem))));

                        this.AddRange(getList());

                        if (isRaised) { RaiseListChangedEvents = true; }
                        ResetBindings();
                        break;
                    case ListChangedType.ItemAdded:
                        if (filterBy(source[e.NewIndex]))
                        {
                            Add(constructor(source[e.NewIndex]));
                            SortBy(orderBy);
                        }
                        break;
                    case ListChangedType.ItemDeleted:
                        List<T> disired = source.
                            Where(w => filterBy(w)).
                            Select(s => constructor(s)).
                            ToList();

                        List<T> current = this.
                            Where(w => !(emptyItem is not null && areEquel(w, emptyItem))).
                            ToList();

                        this.RemoveRange(current.Except(disired));
                        break;
                    case ListChangedType.ItemChanged:
                        if (filterBy(source[e.NewIndex]))
                        {
                            T itemChanged = constructor(source[e.NewIndex]);

                            foreach (T item in this.Where(w => areEquel(w, itemChanged)).ToList())
                            { onItemChanged(source[e.NewIndex], item); }

                            SortBy(orderBy);
                        }
                        break;
                    case ListChangedType.ItemMoved:
                    case ListChangedType.PropertyDescriptorAdded:
                    case ListChangedType.PropertyDescriptorDeleted:
                    case ListChangedType.PropertyDescriptorChanged:
                    default:
                        Exception ex = new NotSupportedException("ListChangedType is not supported");
                        ex.Data.Add(nameof(e.ListChangedType), e.ListChangedType);
                        ex.Data.Add(nameof(e.NewIndex), e.NewIndex);
                        ex.Data.Add(nameof(e.OldIndex), e.OldIndex);
                        ex.Data.Add(nameof(e.PropertyDescriptor), e.PropertyDescriptor);
                        throw ex;
                }
            }
        }

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
        void SortBy(Func<T, Object> keySelector)
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
    }

}
