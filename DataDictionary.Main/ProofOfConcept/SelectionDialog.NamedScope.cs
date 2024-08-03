using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Dialogs;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.ProofOfConcept
{
    class NamedScopeSelection : SelectionDialog
    {
        protected class NamedScopeItem
        {
            public required INamedScopeValue NamedScope { get; init; }
            public required INamedScopeSourceValue Source { get; init; }
        }

        Dictionary<NamedScopeIndex, NamedScopeItem> dataSource;

        Dictionary<ListViewItem, NamedScopeIndex> listViewItems = new Dictionary<ListViewItem, NamedScopeIndex>();

        public NamedScopeSelection() : base()
        {
            dataSource = BusinessData.NamedScope.ScopeKeys(ScopeType.Null). // Return all
                 Select(s => new
                 {
                     Index = new NamedScopeIndex(s),
                     Value = BusinessData.NamedScope.GetValue(s),
                     Data = BusinessData.NamedScope.GetData(s),
                 }).ToDictionary(
                    key => key.Index,
                    data => new NamedScopeItem()
                    { Source = data.Data, NamedScope = data.Value });

            Initialize();
        }

        public NamedScopeSelection(params ScopeType[] scopes) : base()
        {
            dataSource = BusinessData.NamedScope.ScopeKeys(scopes).
                 Select(s => new
                 {
                     Index = new NamedScopeIndex(s),
                     Value = BusinessData.NamedScope.GetValue(s),
                     Data = BusinessData.NamedScope.GetData(s),
                 }).ToDictionary(
                    key => key.Index,
                    data => new NamedScopeItem() { Source = data.Data, NamedScope = data.Value });

            if (scopes.Length == 1)
            {
                ImageEnumeration value = ImageEnumeration.Cast(scopes[0]);
                Icon = value.WindowIcon;
                Text = value.DisplayName;
            }

            Initialize();
        }

        protected virtual void Initialize()
        {
            var groups = dataSource.GroupBy(g => g.Value.NamedScope.Scope);

            foreach (var group in groups)
            {
                ListViewGroup? newGroup = null;
                if (groups.Count() > 1)
                { newGroup = new ListViewGroup(ImageEnumeration.Cast(group.Key).DisplayName); }

                foreach (var groupValue in group)
                {
                    //TODO: Option to not include duplicates?
                    //TODO: Organize by Scope or Path
                    //TODO: Filter by Path
                    //TODO: Re-merge to main Dialog

                    ListViewItem newItem = new ListViewItem(groupValue.Value.NamedScope.Title);

                    if (newGroup is not null) { newItem.Group = newGroup; }
                    newItem.ImageKey = ImageEnumeration.Cast(groupValue.Value.NamedScope.Scope).Name;

                    listViewItems.Add(newItem, groupValue.Key);
                    selectionData.Items.Add(newItem);
                }
            }

            selectionData.ItemChecked += SelectionData_ItemChecked;
            selectionData.ItemSelectionChanged += SelectionData_ItemSelectionChanged;
        }

        public void CheckByValue<TValue>(IEnumerable<TValue> values)
        {
            CheckByIndex(dataSource.
                Where(w => w.Value.Source is TValue).
                Select(s => s.Value.Source.Index));
        }

        public void CheckByIndex(IEnumerable<DataLayerIndex> indexes)
        {
            foreach (DataLayerIndex item in indexes)
            {
                IEnumerable<NamedScopeIndex> selectedKeys = dataSource.
                    Where(w => item.Equals(w.Value.Source.Index)).
                    Select(s => s.Key);

                IEnumerable<ListViewItem> selectedListViews = listViewItems.
                    Where(w => selectedKeys.Any(a => a.Equals(w.Value))).
                    Select(s => s.Key).
                    Distinct();

                foreach (ListViewItem listView in selectedListViews)
                { if (!listView.Checked) { listView.Checked = true; } }
            }
        }

        public IEnumerable<INamedScopeSourceValue> IsCheckedBySource()
        {
            return listViewItems.
                Where(w => w.Key.Checked).
                Select(s => dataSource[s.Value].Source).
                Distinct().
                ToList();
        }

        public IEnumerable<INamedScopeValue> IsCheckedByNamedScope()
        {
            return listViewItems.
                Where(w => w.Key.Checked).
                Select(s => dataSource[s.Value].NamedScope).
                Distinct().
                ToList();
        }


        public IEnumerable<DataLayerIndex> IsCheckedByIndex()
        {
            return listViewItems.
                Where(w => w.Key.Checked).
                Select(s => dataSource[s.Value].Source.Index).
                Distinct().
                ToList();
        }

        private void SelectionData_ItemSelectionChanged(Object? sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.Item is not null
                && e.IsSelected
                && listViewItems.ContainsKey(e.Item)
                && dataSource.ContainsKey(listViewItems[e.Item]))
            {
                NamedScopeIndex selectedIndex = listViewItems[e.Item];
                NamedScopeItem selectedValue = dataSource[selectedIndex];

                titleData.Text = selectedValue.NamedScope.Title;
                scopeData.Text = ImageEnumeration.Cast(selectedValue.NamedScope.Scope).DisplayName;
                pathData.Text = selectedValue.NamedScope.Path.MemberFullPath;
            }
        }

        private void SelectionData_ItemChecked(Object? sender, ItemCheckedEventArgs e)
        {
            if (listViewItems.ContainsKey(e.Item))
            {
                //TODO: Option to auto-check duplicates?

                //var selectedKeys = listViewItems.Where(w => dataSource.
                //    Where(w => dataSource[listViewItems[e.Item]].Source.Index.Equals(w.Value.Source.Index)).
                //    Select(s => s.Key).
                //    Any(a => a.Equals(w.Value))).
                //    Select(s => s.Key).
                //    Where(w => selectionData.Items.Cast<ListViewItem>().Contains(w)).
                //    ToList();

                //foreach (var item in selectedKeys)
                //{
                //    if (e.Item.Checked && !item.Checked) { item.Checked = true; }
                //    else if (!e.Item.Checked && item.Checked) { item.Checked = false; }
                //}
            }
        }
    }
}
