using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Dialogs;
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

namespace DataDictionary.Main.Forms.Model.Controls
{
    partial class AliasData : UserControl
    {
        BindingSource? dataBinding; // Pointer to the BindingSource.
        List<ScopeType> filterScope = new List<ScopeType>();

        /// <summary>
        /// List of Alias in the BindingSource.
        /// </summary>
        public IEnumerable<IAliasSubType> Aliases
        {
            get
            {
                if (dataBinding is null) { return new List<IAliasSubType>(); }
                else { return dataBinding.List.OfType<IAliasSubType>(); }
            }
        }

        public AliasData()
        {
            InitializeComponent();

            aliasAddCommand.Image = NavigationEnumeration.GetImage(ScopeType.ModelAlias, CommandImageType.Add);
            aliasSelectCommand.Image = NavigationEnumeration.GetImage(ScopeType.ModelAlias, CommandImageType.Select);
        }

        public void BindTo(BindingSource binding,
            Func<IAliasSubType> onNewAlias,
            params ScopeType[] scopes)
        {
            dataBinding = binding;

            aliasGrid.AutoGenerateColumns = false;
            aliasGrid.DataSource = binding;

            ScopeNameList.Load(aliaseScopeColumn);
            ScopeNameList.Load(aliasScopeData);

            aliasScopeData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedValue), dataBinding, nameof(IAliasSubType.AliasScope), false, DataSourceUpdateMode.OnPropertyChanged) { DataSourceNullValue = ScopeNameList.NullValue });
            // Binding does not work on complex types such as a Class. Must be done manually.
            //aliasNameData.DataBindings.Add(new Binding(nameof(aliasNameData.Text), dataBinding, nameof(IAliasSubType.AliasPath), false, DataSourceUpdateMode.OnPropertyChanged));

            foreach (ScopeType item in scopes)
            { filterScope.Add(item); }

            dataBinding.AddingNew += DataBinding_AddingNew;
            dataBinding.CurrentChanged += DataBinding_CurrentChanged;

            void DataBinding_AddingNew(Object? sender, AddingNewEventArgs e)
            { e.NewObject = onNewAlias(); }

            void DataBinding_CurrentChanged(Object? sender, EventArgs e)
            {
                if (dataBinding.Current is IAliasSubType current)
                {
                    aliasNameData.Text = current.AliasName.MemberFullPath;
                    Boolean inModel = BusinessData.NamedScope.PathKeys(current.AliasName).Count > 0;
                    isAliasInModelData.Checked = inModel;
                    aliasNameData.ReadOnly = inModel;
                    aliasScopeData.ReadOnly = inModel;
                }
                else
                {
                    aliasNameData.Text = String.Empty;
                    isAliasInModelData.Checked = false;
                    aliasNameData.ReadOnly = true;
                    aliasScopeData.ReadOnly = true;
                }
            }
        }

        private void AliasNameData_Validating(object sender, CancelEventArgs e)
        {
            PathIndex path = new PathIndex(PathIndex.Parse(aliasNameData.Text).ToArray());
            aliasNameData.Text = path.MemberFullPath;
        }


        private void AliasNameData_Validated(object sender, EventArgs e)
        {
            if (dataBinding is not null
                && dataBinding.Position >= 0
                && dataBinding.Current is IAliasSubType value)
            {
                value.AliasName = new PathIndex(PathIndex.Parse(aliasNameData.Text).ToArray());
            }
        }

        private void AliasSelectCommand_Click(object sender, EventArgs e)
        {
            if (dataBinding is not null
                && ParentForm is not null)
            {
                using (SelectionDialog dialog = new SelectionDialog(ParentForm))
                {
                    dialog.FilterScopes.AddRange(filterScope);
                    dialog.BuildData(Aliases.SelectMany(s => BusinessData.NamedScope.PathKeys(s.AliasName)));

                    if (dialog.ShowDialog(this) is DialogResult.OK)
                    {
                        foreach (INamedScopeValue item in dialog.SelectedByNamedScope())
                        {
                            if (!Aliases.Any(w => w.AliasScope.Equals(item.Scope) && w.AliasName.Equals(item.Path))
                                && dataBinding.AddNew() is IAliasSubType value)
                            {
                                value.AliasScope = item.Scope;
                                value.AliasName = item.Path;

                                dataBinding.Position = Aliases.ToList().IndexOf(value);
                                aliasNameData.Text = value.AliasName.MemberFullPath;
                            }
                        }
                    }
                }
            }
        }

        private void AliasAddCommand_Click(object sender, EventArgs e)
        {
            if (dataBinding is not null)
            { dataBinding.AddNew(); }
        }

    }
}
