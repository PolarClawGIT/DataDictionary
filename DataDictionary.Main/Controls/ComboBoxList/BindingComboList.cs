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
        class Comparer : IEqualityComparer<T>
        {   // This does the same thing as EqualityComparer<T>.Create
            // The issue is that ToList and similar uses the IEqualityComparer before it is constructed by Create.
            // When this happens, a "not supported" exception is thrown.
            // By making a physical implementation, instead of LINQ statement, this is not an issue.
            Func<T, T, Boolean>? funcEqual = (a, b) => false;

            public Comparer(Func<T, T, Boolean>? areEqual) : base()
            { funcEqual = areEqual; }

            public Boolean Equals(T? x, T? y)
            {   // Note: Most Indexes/Keys consider the empty object (String.Empty, GUID.Empty, ...)
                // to be not equal to anything. The EmptyItem in the list must be handled differently.
                if (x is null || y is null || funcEqual is null) { return false; }
                else { return funcEqual(x, y); }
            }

            public Int32 GetHashCode([DisallowNull] T obj)
            { return obj.GetHashCode(); }
        }

        /// <summary>
        /// Method executed before the ListChange event is applied. Used to Save the Selected Item.
        /// </summary>
        Action OnChanging = () => { };

        /// <summary>
        /// Method executed after the ListChange event is applied. Used to Restore the Selected Item
        /// </summary>
        Action OnChanged = () => { };

        /// <summary>
        /// Binds the ComboBox control
        /// </summary>
        /// <param name="control"></param>
        /// <param name="valueMember"></param>
        /// <param name="displayMember"></param>
        public void BindTo(ComboBox control,
            Func<String> valueMember,
            Func<String> displayMember)
        {
            control.ValueMember = valueMember();
            control.DisplayMember = displayMember();
            control.DataSource = this;

            T? currentValue = default;
            OnChanging = onListChanging;
            OnChanged = onListChanged;

            void onListChanging()
            {
                if (control.SelectedItem is T value)
                { currentValue = value; }
                else { currentValue = default; }
            }

            void onListChanged()
            {
                if (currentValue is T value
                    && control.Items.Contains(value))
                { control.SelectedItem = value; }
                else
                { control.SelectedIndex = 0; }
            }
        }

        /// <summary>
        /// Binds the ComboBoxData control
        /// </summary>
        /// <param name="control"></param>
        /// <param name="valueMember"></param>
        /// <param name="displayMember"></param>
        public void BindTo(ComboBoxData control,
            Func<String> valueMember,
            Func<String> displayMember)
        {
            control.ValueMember = valueMember();
            control.DisplayMember = displayMember();
            control.DataSource = this;

            T? currentValue = default;
            OnChanging = onListChanging;
            OnChanged = onListChanged;

            void onListChanging()
            {
                if (control.SelectedItem is T value)
                { currentValue = value; }
                else { currentValue = default; }
            }

            void onListChanged()
            {
                if (currentValue is T value
                    && control.Items.Contains(value))
                { control.SelectedItem = value; }
                else
                { control.SelectedIndex = 0; }
            }
        }

        /// <summary>
        /// Binds the DataGridViewComboBoxColumn control
        /// </summary>
        /// <param name="control"></param>
        /// <param name="valueMember"></param>
        /// <param name="displayMember"></param>
        public void BindTo(DataGridViewComboBoxColumn control,
            Func<String> valueMember,
            Func<String> displayMember)
        {   // Not verified.
            control.ValueMember = valueMember();
            control.DisplayMember = displayMember();
            control.DataSource = this;

            T? currentValue = default;
            OnChanging = onListChanging;
            OnChanged = onListChanged;

            // TODO: DataGridViewComboBoxColumn does not give access to the underlining ComboBox.
            // It may not be necessary to do anything as the internal ComboBox is built on the fly upon cell entry.

            void onListChanging()
            {
                Exception ex = new NotSupportedException();
                if(control.DataGridView is not null)
                { ex.Data.Add(nameof(control.DataGridView), control.DataGridView.Name); }
                
                ex.Data.Add(nameof(control.Name), control.Name);
                ex.Data.Add(nameof(control.DataPropertyName), control.DataPropertyName);
                throw ex;

                //if(control.DataGridView is DataGridView view
                //    && view.CurrentCell is DataGridViewCell cell
                //    && cell.Value is T value) 
                //{ currentValue = value; }
                //else { currentValue = default; }
            }

            void onListChanged()
            {
                Exception ex = new NotSupportedException();
                if (control.DataGridView is not null)
                { ex.Data.Add(nameof(control.DataGridView), control.DataGridView.Name); }

                ex.Data.Add(nameof(control.Name), control.Name);
                ex.Data.Add(nameof(control.DataPropertyName), control.DataPropertyName);
                throw ex;

                //if (currentValue is T value
                //    && control.Items.Contains(value)
                //    && control.DataGridView is DataGridView view
                //    && view.CurrentCell is DataGridViewCell cell)
                //{ cell.Value = value; }
                //else
                //{ }
            }
        }

        /// <summary>
        /// Builds the List for the ComboBox. The list is assigned on the BindTo call.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="constructor">Call to create an item from the Source. This should be a constructor call.</param>
        /// <param name="areEqual">Call used to determine if items are equal. Use the Index class for the items.</param>
        /// <param name="emptyValue">Call to create the Empty Item. Null = no empty item added.</param>
        /// <param name="onItemChanged">Action to be performed if a change in the item (such as a title change) occurs.</param>
        /// <param name="filterBy">Filters the Source before adding it to the items.</param>
        /// <param name="orderBy">Orders the Source before adding it to the items.</param>
        public void BuildList<TSource>(
            IBindingList<TSource> source,
            Func<TSource, T> constructor,
            Func<T, T, Boolean> areEqual,
            Func<T>? emptyValue = null,
            Action<TSource, T>? onItemChanged = null,
            Func<TSource, Boolean>? filterBy = null,
            Func<T, Object>? orderBy = null)
            where TSource : IBindingPropertyChanged
        {
            filterBy = filterBy ?? (f => 1 == 1);
            orderBy = orderBy ?? (o => 1);
            areEqual = areEqual ?? ((a, b) => false);
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
                OnChanging();

                switch (e.ListChangedType)
                {
                    case ListChangedType.Reset:
                        isRaised = RaiseListChangedEvents;
                        RaiseListChangedEvents = false;
                        this.RemoveRange(this.
                            Where(w => !(emptyItem is not null && areEqual(w, emptyItem))));

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
                        // Need to compare what is currently in the source and what is currently in the list in the BindingComboList
                        // The ones that are missing in the source should be removed from the BindingComboList.
                        List<T> desired = source.
                            Where(w => filterBy(w)).
                            Select(s => constructor(s)).
                            ToList();

                        List<T> current = this.
                            // Don't compare the Empty Item.
                            Where(w => !(emptyItem is not null && Object.ReferenceEquals(w, emptyItem))). 
                            ToList();

                        this.RemoveRange(current.Except(desired, new Comparer(areEqual)));
                        break;
                    case ListChangedType.ItemChanged:
                        if (filterBy(source[e.NewIndex]))
                        {
                            T itemChanged = constructor(source[e.NewIndex]);

                            foreach (T item in this.Where(w => areEqual(w, itemChanged)).ToList())
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

                OnChanged();
            }
        }

        /// <summary>
        /// Switches the order of two items in the list and calls the Moved ChangedList event.
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

    static class BindingComboListExtension
    {
        /// <summary>
        /// Try to Get the Selected Item from the Control.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryGetSelected<T>(this ComboBoxData control, [NotNullWhen(true)] out T? result)
        {
            if (control.SelectedItem is T value)
            { result = value; return true; }
            else { result = default(T); return false; }
        }

        /// <summary>
        /// Try to Set the Selected Item of the Control to the first value that matches.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Boolean TrySetSelected<T>(this ComboBoxData control, Func<T, Boolean> predicate)
        {
            if (control.DataSource is BindingComboList<T> values &&
                values.FirstOrDefault(w => predicate(w)) is T value)
            { control.SelectedItem = value; return true; }
            else { return false; }
        }

        /// <summary>
        /// Tries to Get the list of Items of the Control.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryGetList<T>(this ComboBoxData control, [NotNullWhen(true)] out IEnumerable<T>? result)
        {
            if (control.DataSource is BindingComboList<T> values)
            { result = values; return true; }
            else { result = null; return false; }
        }

        /// <summary>
        /// Try to Get the first Value that matches from the list of Items of the Control.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="predicate"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryGetValue<T>(this ComboBoxData control, Func<T, Boolean> predicate, [NotNullWhen(true)] out T? result)
        {
            if (control.DataSource is BindingComboList<T> values &&
                values.FirstOrDefault(w => w is not null && predicate(w)) is T value)
            { result = value; return true; }
            else { result = default(T); return false; }
        }

        /// <summary>
        /// Try to Get the Selected Item from the Control.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryGetSelected<T>(this ComboBox control, [NotNullWhen(true)] out T? result)
        {
            if (control.SelectedItem is T value)
            { result = value; return true; }
            else { result = default(T); return false; }
        }

        /// <summary>
        /// Try to Set the Selected Item of the Control to the first value that matches.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Boolean TrySetSelected<T>(this ComboBox control, Func<T, Boolean> predicate)
        {
            if (control.DataSource is BindingComboList<T> values &&
                values.FirstOrDefault(w => predicate(w)) is T value)
            { control.SelectedItem = value; return true; }
            else { return false; }
        }

        /// <summary>
        /// Tries to Get the list of Items of the Control.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryGetList<T>(this ComboBox control, [NotNullWhen(true)] out IEnumerable<T>? result)
        {
            if (control.DataSource is BindingComboList<T> values)
            { result = values; return true; }
            else { result = null; return false; }
        }

        /// <summary>
        /// Try to Get the first Value that matches from the list of Items of the Control.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="predicate"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryGetValue<T>(this ComboBox control, Func<T, Boolean> predicate, [NotNullWhen(true)] out T? result)
        {
            if (control.DataSource is BindingComboList<T> values &&
                values.FirstOrDefault(w => w is not null && predicate(w)) is T value)
            { result = value; return true; }
            else { result = default(T); return false; }
        }

        /// <summary>
        /// Trys to Get the Selected Item from the Control.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryGetSelected<T>(this DataGridViewComboBoxColumn control, [NotNullWhen(true)] out T? result)
        {
            if (control.DataGridView is DataGridView grid
                && grid.CurrentCell is DataGridViewCell cell
                && cell.Value is T value)
            { result = value; return true; }
            else { result = default(T); return false; }
        }

        /// <summary>
        /// Try to Set the Selected Item of the Control to the first value that matches.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Boolean TrySetSelected<T>(this DataGridViewComboBoxColumn control, Func<T, Boolean> predicate)
        {
            if (control.DataSource is BindingComboList<T> values
                && values.FirstOrDefault(w => predicate(w)) is T value
                && control.DataGridView is DataGridView grid
                && grid.CurrentCell is DataGridViewCell cell)
            { cell.Value = value; return true; }
            else { return false; }
        }

        /// <summary>
        /// Try to Get the list of Items of the Control.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryGetList<T>(this DataGridViewComboBoxColumn control, [NotNullWhen(true)] out IEnumerable<T>? result)
        {
            if (control.DataSource is BindingComboList<T> values)
            { result = values; return true; }
            else { result = null; return false; }
        }

        /// <summary>
        /// Try to Get the first Value that matches from the list of Items of the Control.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="predicate"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryGetValue<T>(this DataGridViewComboBoxColumn control, Func<T, Boolean> predicate, [NotNullWhen(true)] out T? result)
        {
            if (control.DataSource is BindingComboList<T> values &&
                values.FirstOrDefault(w => w is not null && predicate(w)) is T value)
            { result = value; return true; }
            else { result = default(T); return false; }
        }
    }


}
