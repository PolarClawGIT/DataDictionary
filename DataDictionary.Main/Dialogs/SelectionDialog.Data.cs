using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Dialogs
{
    class SelectionDialogData : BindingList<SelectionDialogValue>,
        IBindingList<SelectionDialogValue>,
        IBindingPropertyChanged
    {
        /// <summary>
        /// The currently selected Scope
        /// </summary>
        public ScopeType SelectedScope
        {
            get { return scopeValue; }
            set
            {
                scopeValue = value;
                IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, nameof(SelectedScope));
            }
        }
        public ScopeType ScopeNull { get { return scopeNull; } }
        private static ScopeType scopeNull = ScopeType.Null;
        private ScopeType scopeValue = scopeNull;
        private BindingList<ScopeType> filterScopes;

        /// <summary>
        /// The currently selected Path
        /// </summary>
        public NamedScopePath SelectedPath
        {
            get { return pathValue; }
            set
            {
                pathValue = value;
                IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, nameof(SelectedPath));
            }
        }
        public NamedScopePath PathNull { get { return pathNull; } }
        private static NamedScopePath pathNull = new NamedScopePath();
        private NamedScopePath pathValue = pathNull;
        private BindingList<NamedScopePath> filterPaths;

        public Func<INamedScopeSourceValue, String> GetDescription { get; init; } = (value) => String.Empty;

        /// <summary>
        /// Group By Scope is Selected
        /// </summary>
        public Boolean GroupByScope
        {
            get { return isGroupByScope; }
            set
            {
                isGroupByScope = value;
                IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, nameof(GroupByScope));
                IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, nameof(GroupByPath));
            }
        }

        /// <summary>
        /// Group by Path is Selected
        /// </summary>
        public Boolean GroupByPath
        {
            get { return !isGroupByScope; }
            set
            {
                isGroupByScope = !value;
                IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, nameof(GroupByScope));
                IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, nameof(GroupByPath));
            }
        }
        private Boolean isGroupByScope = true;

        public SelectionDialogData(BindingList<ScopeType> scopes, BindingList<NamedScopePath> paths)
        {
            this.filterScopes = scopes;
            this.filterPaths = paths;

            foreach (NamedScopeIndex rootKey in BusinessData.NamedScope.RootKeys())
            { this.AddRange(Create(rootKey)); }

            IEnumerable<SelectionDialogValue> Create(NamedScopeIndex key)
            {
                List<SelectionDialogValue> result = new List<SelectionDialogValue>();
                result.Add(new SelectionDialogValue(key) { GetDescription = GetDescription });

                foreach (NamedScopeIndex childKey in BusinessData.NamedScope.ChildrenKeys(key))
                { result.AddRange(Create(childKey)); }

                return result;
            }
        }

        public void BindScopes(ComboBox control)
        {
            //Issue: Binding does not work with Enums
            //Binding bind = new Binding(nameof(control.SelectedValue), binding, nameof(this.SelectedScope));
            //bind.DataSourceNullValue = scopeNull;
            //bind.NullValue = scopeNull;
            //control.DataBindings.Add(bind);

            BuildList();

            control.SelectionChangeCommitted += Control_SelectionChangeCommitted;
            filterScopes.ListChanged += Filter_ListChanged;
            filterPaths.ListChanged += Filter_ListChanged;

            void Control_SelectionChangeCommitted(Object? sender, EventArgs e)
            {
                if (control.SelectedValue is ScopeType value)
                { SelectedScope = value; }

                OnFilterChanged();
            }

            void Filter_ListChanged(Object? sender, ListChangedEventArgs e)
            { BuildList(); }

            void BuildList()
            {
                control.DataSource = null;

                Dictionary<ScopeType, String> data = new Dictionary<ScopeType, String>();
                data.Add(scopeNull, "(any)");

                this.Where(w => 
                        (filterScopes.Count == 0 || filterScopes.Contains(w.Scope))
                        && (filterPaths.Count == 0 || filterPaths.Contains(w.Path))).
                    Select(s => new { s.Scope, s.ScopeName }).
                    DistinctBy(d => d.Scope).
                    ToList().
                    ForEach(a => data.Add(a.Scope, a.ScopeName));

                control.ValueMember = nameof(SelectionDialogValue.Scope);
                control.DisplayMember = nameof(SelectionDialogValue.ScopeName);
                control.DataSource = data.Select(s => new { Scope = s.Key, ScopeName = s.Value }).ToList();

                if (data.Count() == 1)
                { SelectedScope = data.First().Key; }
                else if (data.Count() == 2)
                { SelectedScope = data.Last().Key; }
                control.SelectedValue = SelectedScope;
            }
        }

        public void BindPaths(ComboBox control)
        {

            //Issue: Binding does not work with complex types such as Classes
            //Binding bind = new Binding(nameof(control.SelectedValue), binding, nameof(this.SelectedPath));
            //bind.DataSourceNullValue = pathNull;
            //bind.NullValue = pathNull;
            //control.DataBindings.Add(bind);
            BuildList();

            control.SelectionChangeCommitted += Control_SelectionChangeCommitted;
            filterScopes.ListChanged += Filter_ListChanged;
            filterPaths.ListChanged += Filter_ListChanged;

            void Control_SelectionChangeCommitted(Object? sender, EventArgs e)
            {
                if (control.SelectedValue is NamedScopePath value)
                { SelectedPath = value; }

                OnFilterChanged();
            }

            void Filter_ListChanged(Object? sender, ListChangedEventArgs e)
            { BuildList(); }

            void BuildList()
            {
                control.DataSource = null;
                Dictionary<NamedScopePath, String> data = new Dictionary<NamedScopePath, String>();
                data.Add(pathNull, "(any)");

                this.Where(w =>
                        (filterScopes.Count == 0 || filterScopes.Contains(w.Scope))
                        && (filterPaths.Count == 0 || filterPaths.Contains(w.Path))).
                    Select(s => new { s.Path, s.PathName }).
                    DistinctBy(d => d.Path).
                    ToList().
                    ForEach(a => data.Add(a.Path, a.PathName));

                control.ValueMember = nameof(SelectionDialogValue.Path);
                control.DisplayMember = nameof(SelectionDialogValue.PathName);
                control.DataSource = data.Select(s => new { Path = s.Key, PathName = s.Value }).ToList();

                if (data.Count() == 1)
                { SelectedPath = data.First().Key; }
                else if (data.Count() == 2)
                { SelectedPath = data.Last().Key; }
                control.SelectedValue = SelectedPath;
            }
        }

        public void BindGroupBy(RadioButton byScope)
        {
            //Issue: Binding does not work on Value types, like boolean
            //groupByScope.DataBindings.Add(new Binding(nameof(groupByScope.Checked), formData, nameof(formData.GroupByScope), true, DataSourceUpdateMode.OnPropertyChanged, false));
            //groupByPath.DataBindings.Add(new Binding(nameof(groupByPath.Checked), formData, nameof(formData.GroupByPath), true, DataSourceUpdateMode.OnPropertyChanged, false));
            byScope.Checked = isGroupByScope;

            byScope.CheckedChanged += ByScope_CheckedChanged;

            void ByScope_CheckedChanged(Object? sender, EventArgs e)
            { isGroupByScope = byScope.Checked; OnFilterChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public event EventHandler? FilterChanged;
        protected void OnFilterChanged()
        {
            if (FilterChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
    }
}
