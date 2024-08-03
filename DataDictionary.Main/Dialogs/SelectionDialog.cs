using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Dialogs
{
    partial class SelectionDialog : Form
    {
        /// <summary>
        /// Filters the list by Scopes.
        /// </summary>
        public BindingList<ScopeType> FilterScopes { get; } =
            new BindingList<ScopeType>()
            { AllowEdit = false, AllowNew = true, AllowRemove = true };

        /// <summary>
        /// Filters the List by Paths
        /// </summary>
        public BindingList<NamedScopePath> FilterPaths { get; } =
            new BindingList<NamedScopePath>()
            { AllowEdit = false, AllowNew = true, AllowRemove = true };

        /// <summary>
        /// The Selected items.
        /// </summary>
        public BindingList<NamedScopeIndex> Selected { get; } =
            new BindingList<NamedScopeIndex>()
            { AllowEdit = false, AllowNew = true, AllowRemove = true };

        SelectionDialogData formData;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="getDescription">Function that returns the Description</param>
        public SelectionDialog(Func<INamedScopeSourceValue, String> getDescription) : base()
        {
            formData = new SelectionDialogData(FilterScopes, FilterPaths);
            formData.BuildList(getDescription); // Could not create getDescription as a Property and pass it to the data class.

            InitializeComponent();
            selectionData.SmallImageList = ImageEnumeration.AsImageList();
            bindingSource.DataSource = formData;
            bindingSource.Position = 0;

            //formData.PropertyChanged += FormData_PropertyChanged;
            formData.FilterChanged += FormData_FilterChanged;
            FilterScopes.ListChanged += FilterScopes_ListChanged;
            FilterPaths.ListChanged += FilterPaths_ListChanged;
            Selected.ListChanged += Selected_ListChanged;

            // Data Bindings
            //Issue: Could not get binding to Radio Buttons to work. Manual Binding is used.
            formData.BindGroupBy(groupByScope);

            //Issue: Could not get binding to Combo Boxes to work as desired. Manual Binding is used.
            formData.BindScopes(filterScope);
            formData.BindPaths(filterPath);

            titleData.DataBindings.Add(new Binding(nameof(titleData.Text), bindingSource, nameof(SelectionDialogValue.Title)));
            scopeData.DataBindings.Add(new Binding(nameof(scopeData.Text), bindingSource, nameof(SelectionDialogValue.ScopeName)));
            pathData.DataBindings.Add(new Binding(nameof(pathData.Text), bindingSource, nameof(SelectionDialogValue.PathName)));
            descriptionData.DataBindings.Add(new Binding(nameof(descriptionData.Text), bindingSource, nameof(SelectionDialogValue.Description)));

            LoadListView();
        }

        /// <summary>
        /// Shows the dialog initially position over the form that called it.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public DialogResult ShowDialog(Form source)
        {
            if (source.IsMdiChild
                && source.MdiParent is Form parent
                && parent.Controls.Cast<Control>().FirstOrDefault(w => w is MdiClient) is Control mdiControl)
            {
                Point mdiTopLeft = mdiControl.Location;
                Point formOffset = source.Location; // The Forms location is offset from the MDI container

                // Get the sum of the controls that are on the top and left edges of the MDI Container
                // Note: Menu Strips and some other controls are above or to the left of the MDI container.
                // This places them outside of the MDI container.
                // We are interested in controls that take up space within the MDI container.
                // ToolStrips are within the MDI, but other controls may need to be accounted for.
                Int32 topSum = parent.Controls.
                    Cast<Control>().
                    Where(w => w.Dock == DockStyle.Top && w is ToolStrip).
                    Sum(s => s.Bottom);
                Int32 leftSum = parent.Controls.
                    Cast<Control>().
                    Where(w => w.Dock == DockStyle.Left && w is ToolStrip).
                    Sum(s => s.Right);

                var topLeft = Point.Add(Point.Add(mdiControl.Location, new Size(leftSum, topSum)), new Size(source.Location));

                this.StartPosition = FormStartPosition.Manual;
                this.Location = topLeft;
            }


            return base.ShowDialog(source);
        }

        public void SelectByValue<TValue>(IEnumerable<TValue> values)
            where TValue : INamedScopeSourceValue
        {
            IEnumerable<DataLayerIndex> indexes = values.Select(s => s.Index);

            SelectByIndex(indexes);
        }

        public void SelectByIndex(IEnumerable<DataLayerIndex> indexes)
        {
            foreach (NamedScopeIndex item in formData.
                Where(w => indexes.Contains(w.Source.Index)).
                Select(s => s.Index))
            { if (!Selected.Contains(item)) { Selected.Add(item); } }
        }

        public IEnumerable<TValue> SelectedByValue<TValue>()
            where TValue : INamedScopeSourceValue
        { return formData.Where(w => Selected.Contains(w.Index)).Select(s => s.Source).OfType<TValue>().Distinct(); }

        protected void LoadListView()
        {
            selectionData.Columns.Clear();
            selectionData.Groups.Clear();
            selectionData.Items.Clear();

            selectionData.Columns.Add(new ColumnHeader()
            {
                Text = nameof(SelectionDialogValue.Title),
                Width = selectionData.Width - SystemInformation.VerticalScrollBarWidth - 5
            });

            IEnumerable<SelectionDialogValue> filtered = formData.Where(
                w => (FilterScopes.Count() == 0 || FilterScopes.Contains(w.Scope))
                && (FilterPaths.Count() == 0 || FilterPaths.Contains(w.Path))
                && (formData.SelectedPath == formData.PathNull || formData.SelectedPath == w.Path)
                && (formData.SelectedScope == formData.ScopeNull || formData.SelectedScope == w.Scope)
                );

            var grouping = filtered.
                Select(s => new { Group = formData.GroupByScope ? s.ScopeName : s.PathName, Data = s }).
                OrderBy(o => o.Group, StringComparer.OrdinalIgnoreCase).
                GroupBy(g => g.Group, StringComparer.OrdinalIgnoreCase);

            foreach (var viewGroup in grouping)
            {
                ListViewGroup? newGroup = null;
                if (grouping.Count() > 1)
                {
                    newGroup = new ListViewGroup(viewGroup.Key);
                    selectionData.Groups.Add(newGroup);
                }

                foreach (var item in viewGroup.OrderBy(o => o.Data.Title))
                {
                    ListViewItem newItem = item.Data.ListView;
                    newItem.Group = newGroup;

                    selectionData.Items.Add(newItem);
                }
            }
        }

        private void FormData_FilterChanged(Object? sender, EventArgs e)
        { LoadListView(); }

        private void Selected_ListChanged(Object? sender, ListChangedEventArgs e)
        {
            foreach (SelectionDialogValue item in formData)
            {
                // Causes SelectionData_ItemCheck to trigger.
                if (Selected.Contains(item.Index) && !item.ListView.Checked)
                { item.ListView.Checked = true; }
                else if (!Selected.Contains(item.Index) && item.ListView.Checked)
                { item.ListView.Checked = false; }
                // Everything else does not change the state. Avoids infinite Loop.
            }
        }

        private void FilterPaths_ListChanged(Object? sender, ListChangedEventArgs e)
        { LoadListView(); }

        private void FilterScopes_ListChanged(Object? sender, ListChangedEventArgs e)
        { LoadListView(); }

        private void SelectionData_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (bindingSource.DataSource is IList<SelectionDialogValue> data)
            {
                if (data.FirstOrDefault(w => w.ListView.Equals(e.Item)) is SelectionDialogValue value)
                { bindingSource.Position = data.IndexOf(value); }
            }
        }

        private void SelectionDialog_SizeChanged(object sender, EventArgs e)
        {
            if (selectionData.Columns.Count > 0) // Only expect one column.
            { selectionData.Columns[0].Width = selectionData.Width - SystemInformation.VerticalScrollBarWidth - 5; }
        }

        private void SelectionData_ItemChecked(object sender, ItemCheckedEventArgs e)
        { } //This triggers on Initialization/Visible/Show in addition to ItemCheck

        private void SelectionData_ItemCheck(object sender, ItemCheckEventArgs e)
        {   // This triggers when the checked box is changed. Visible/Show does not trigger this event.
            if (formData.FirstOrDefault(w => w.ListView == selectionData.Items[e.Index]) is SelectionDialogValue value)
            {
                NamedScopeIndex key = value.Index;

                // Causes Selected_ListChanged to trigger
                if (e.NewValue is CheckState.Checked && !Selected.Contains(key))
                { Selected.Add(key); }
                else if (e.NewValue is CheckState.Unchecked && Selected.Contains(key))
                { Selected.Remove(key); }
                // Everything else does not change state. Avoids infinite Loop.
            }
        }
    }
}
