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
        /// Provides mechanism to inject a Description.
        /// </summary>
        public Func<INamedScopeSourceValue, String> GetDescription { get; init; } = (value) => String.Empty;

        public Type? FilterValueType { get; init; } = null;
        public BindingList<ScopeType> FilterScopes { get; } =
            new BindingList<ScopeType>()
            { AllowEdit = false, AllowNew = true, AllowRemove = false };
        public BindingList<NamedScopePath> FilterPaths { get; } =
            new BindingList<NamedScopePath>()
            { AllowEdit = false, AllowNew = true, AllowRemove = false };
        public BindingList<DataLayerIndex> Selected { get; } =
            new BindingList<DataLayerIndex>()
            { AllowEdit = false, AllowNew = true, AllowRemove = false };
        SelectionDialogData formData = new SelectionDialogData();

        public SelectionDialog() : base()
        {
            InitializeComponent();
            selectionData.SmallImageList = ImageEnumeration.AsImageList();
            bindingSource.DataSource = formData;
            bindingSource.Position = 0;

            formData.PropertyChanged += FormData_PropertyChanged;
            FilterScopes.ListChanged += FilterScopes_ListChanged;
            FilterPaths.ListChanged += FilterPaths_ListChanged;
            Selected.ListChanged += Selected_ListChanged;

            // Data Bindings
            //TODO: The radio button are not binding. Throws an error. May need to handle this manually?
            groupByScope.DataBindings.Add(new Binding(nameof(groupByScope.Checked), formData, nameof(formData.GroupByScope), true, DataSourceUpdateMode.OnPropertyChanged, false));
            groupByPath.DataBindings.Add(new Binding(nameof(groupByPath.Checked), formData, nameof(formData.GroupByPath), true, DataSourceUpdateMode.OnPropertyChanged, false));

            //TODO: Combo box list are not binding.
            formData.BindScopes(bindingSource, filterScope);
            formData.BindPaths(bindingSource, filterPath);

            titleData.DataBindings.Add(new Binding(nameof(titleData.Text), bindingSource, nameof(SelectionDialogValue.Title)));
            scopeData.DataBindings.Add(new Binding(nameof(scopeData.Text), bindingSource, nameof(SelectionDialogValue.ScopeName)));
            pathData.DataBindings.Add(new Binding(nameof(pathData.Text), bindingSource, nameof(SelectionDialogValue.PathName)));
            descriptionData.DataBindings.Add(new Binding(nameof(descriptionData.Text), bindingSource, nameof(SelectionDialogValue.Description)));
        }

        private void FormData_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Selected_ListChanged(Object? sender, ListChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void FilterPaths_ListChanged(Object? sender, ListChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void FilterScopes_ListChanged(Object? sender, ListChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void SelectionData_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (bindingSource.DataSource is IList<SelectionDialogValue> data)
            {
                if (data.FirstOrDefault(w => w.ListView.Equals(e.Item)) is SelectionDialogValue value)
                { bindingSource.Position = data.IndexOf(value); }
            }
        }

        private void GroupByScope_CheckedChanged(object sender, EventArgs e)
        {
            formData.GroupByScope = groupByScope.Checked;
        }

        private void GroupByPath_CheckedChanged(object sender, EventArgs e)
        {
            formData.GroupByPath = groupByPath.Checked;
        }
    }
}
