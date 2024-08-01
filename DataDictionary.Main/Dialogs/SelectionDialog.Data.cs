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
        private static ScopeType scopeNull = ScopeType.Null;
        private ScopeType scopeValue = scopeNull;

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
        private static NamedScopePath pathNull = new NamedScopePath();
        private NamedScopePath pathValue = pathNull;

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

        public SelectionDialogData()
        {
            foreach (NamedScopeIndex rootKey in BusinessData.NamedScope.RootKeys())
            { this.AddRange(Create(rootKey)); }

            IEnumerable<SelectionDialogValue> Create(NamedScopeIndex key)
            {
                List<SelectionDialogValue> result = new List<SelectionDialogValue>();
                result.Add(new SelectionDialogValue(key));

                foreach (NamedScopeIndex childKey in BusinessData.NamedScope.ChildrenKeys(key))
                { result.AddRange(Create(childKey)); }

                return result;
            }
        }

        public void BindScopes(BindingSource binding, ComboBox control)
        {
            Dictionary<ScopeType, String> data = new Dictionary<ScopeType, String>();
            data.Add(scopeNull, "(any)");

            this.Select(s => new { s.Scope, s.ScopeName }).
                DistinctBy(d => d.Scope).ToList().ForEach(a => data.Add(a.Scope,a.ScopeName));

            control.ValueMember = nameof(SelectionDialogValue.Scope);
            control.DisplayMember = nameof(SelectionDialogValue.ScopeName);
            control.DataSource = data.Select(s => new { Scope = s.Key, ScopeName = s.Value }).ToList();

            Binding bind = new Binding(nameof(control.SelectedValue), binding, nameof(this.SelectedScope));
            bind.DataSourceNullValue = scopeNull;
            bind.NullValue = scopeNull;
            control.DataBindings.Add(bind);
        }

        public void BindPaths(BindingSource binding, ComboBox control)
        {
            Dictionary<NamedScopePath, String> data = new Dictionary<NamedScopePath, String>();
            data.Add(pathNull, "(any)");

            this.Select(s => new { s.Path, s.PathName }).
                DistinctBy(d => d.Path).ToList().ForEach(a => data.Add(a.Path, a.PathName));

            control.ValueMember = nameof(SelectionDialogValue.Path);
            control.DisplayMember = nameof(SelectionDialogValue.PathName);
            control.DataSource = data.Select(s => new { Path = s.Key, PathName = s.Value }).ToList();

            Binding bind = new Binding(nameof(control.SelectedValue), binding, nameof(this.SelectedPath));
            bind.DataSourceNullValue = pathNull;
            bind.NullValue = pathNull;
            control.DataBindings.Add(bind);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
