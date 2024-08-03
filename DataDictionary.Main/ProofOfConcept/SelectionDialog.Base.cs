using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Dialogs;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.ProofOfConcept
{
    class SelectionDialog<TValue, TResult> : SelectionDialog
            where TValue : class, IScopeType
            where TResult : class, IEquatable<TResult>
    {
        public IEnumerable<TValue> DataSource { get; init; }

        public IList<TResult> Selected = new List<TResult>();

        public Func<TValue, TResult?> GetResult { get; init; } = (value) => null;

        public Func<TValue, String> GetDescription { get; init; } = (value) => String.Empty;

        public Func<TValue, String> GetTitle { get; init; } = (value) =>
        {
            switch (value)
            {
                case INamedScopeValue: return ((INamedScopeValue)value).Title;
                case INamedScopeSourceValue: return ((INamedScopeSourceValue)value).Title;
                default: return String.Empty;
            }
        };

        public Func<TValue, NamedScopePath> GetPath { get; init; } = (value) =>
        {
            switch (value)
            {
                case INamedScopeValue: return ((INamedScopeValue)value).Path;
                case INamedScopeSourceValue: return ((INamedScopeSourceValue)value).Path;
                default: return new NamedScopePath();
            }
        };

        Dictionary<ListViewItem, TValue> listViewItems = new Dictionary<ListViewItem, TValue>();

        public SelectionDialog() : base()
        {
            Initialize();

            var raw = BusinessData.NamedScope.ScopeKeys(ScopeType.Null). // Null gets all
                Where(w => BusinessData.NamedScope.GetData(w) is TValue).
                Select(s =>
                    new
                    {
                        s.NamedScopeId,
                        Value = BusinessData.NamedScope.GetValue(s),
                        Data = BusinessData.NamedScope.GetData(s) as TValue,
                    }).ToList();

            DataSource = raw.Select(s => s.Data).OfType<TValue>();
        }

        public SelectionDialog(params ScopeType[] scopes) : base()
        {
            Initialize();

            var raw = BusinessData.NamedScope.ScopeKeys(scopes).
                Where(w => BusinessData.NamedScope.GetData(w) is TValue).
                Select(s =>
                    new
                    {
                        s.NamedScopeId,
                        NamedScope = BusinessData.NamedScope.GetValue(s),
                        Data = BusinessData.NamedScope.GetData(s) as TValue
                    }).
                ToList();

            DataSource = raw.Select(s => s.Data).OfType<TValue>();
        }

        protected virtual void Initialize()
        {
            Load += SelectionDialog_Load;
            selectionData.ItemChecked += SelectionData_ItemChecked;
            selectionData.ItemSelectionChanged += SelectionData_ItemSelectionChanged;
        }


        protected virtual void SelectionDialog_Load(Object? sender, EventArgs e)
        {
            IEnumerable<IGrouping<ScopeType, TValue>> groups = DataSource.GroupBy(g => g.Scope);

            foreach (IGrouping<ScopeType, TValue> scopeGroup in groups)
            {
                ListViewGroup? newGroup = null;
                if (groups.Count() > 1)
                { newGroup = new ListViewGroup(ImageEnumeration.Cast(scopeGroup.Key).Name); }

                foreach (TValue scopeValue in scopeGroup.Distinct())
                {
                    ListViewItem newItem = new ListViewItem(GetTitle(scopeValue));

                    if (newGroup is not null) { newItem.Group = newGroup; }
                    newItem.ImageKey = ImageEnumeration.Cast(scopeValue.Scope).Name;

                    if (Selected.Any(c => c.Equals(GetResult(scopeValue))))
                    { newItem.Checked = true; }

                    listViewItems.Add(newItem, scopeValue);
                    selectionData.Items.Add(newItem);
                }
            }
        }

        protected virtual void SelectionData_ItemChecked(Object? sender, ItemCheckedEventArgs e)
        {
            if (listViewItems.ContainsKey(e.Item))
            {
                TValue selection = listViewItems[e.Item];

                if (GetResult(selection) is TResult selectedResult)
                {
                    TResult? existingResult = Selected.FirstOrDefault(w => w.Equals(selectedResult));

                    if (e.Item.Checked)
                    { if (existingResult is null) { Selected.Add(selectedResult); } }
                    else
                    { if (existingResult is not null) { Selected.Remove(existingResult); } }
                }

            }
        }

        private void SelectionData_ItemSelectionChanged(Object? sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.Item is not null && e.IsSelected && listViewItems.ContainsKey(e.Item))
            {
                TValue selectedValue = listViewItems[selectionData.SelectedItems[0]];

                titleData.Text = GetTitle(selectedValue);
                descriptionData.Text = GetDescription(selectedValue);
                pathData.Text = GetPath(selectedValue).MemberFullPath;
            }
        }
    }
}
