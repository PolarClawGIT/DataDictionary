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
    partial class Alias : UserControl
    {
        BindingSource? dataBinding; // Pointer to the BindingSource.
        List<ScopeType> filterScope = new List<ScopeType>();

        public Alias()
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

            aliasesData.AutoGenerateColumns = false;
            aliasesData.DataSource = binding;

            ScopeNameList.Load(aliaseScopeColumn);
            ScopeNameList.Load(aliasScopeData);

            aliasScopeData.DataBindings.Add(new Binding(nameof(aliasScopeData.SelectedValue), dataBinding, nameof(IAliasSubType.AliasScope), false, DataSourceUpdateMode.OnPropertyChanged) { DataSourceNullValue = ScopeNameList.NullValue });
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
                    Boolean inModel = BusinessData.NamedScope.PathKeys(current.AliasPath).Count > 0;
                    isAliasInModelData.Checked = inModel;
                    aliasNameData.ReadOnly = inModel;
                    aliasScopeData.ReadOnly = inModel;
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
                value.AliasPath = new PathIndex(PathIndex.Parse(aliasNameData.Text).ToArray());
            }
        }

        private void AliasSelectCommand_Click(object sender, EventArgs e)
        {
            if (dataBinding is not null
                && ParentForm is not null
                && dataBinding.List is IList<IAliasSubType> alias)
            {
                using (SelectionDialog dialog = new SelectionDialog(ParentForm))
                {
                    dialog.FilterScopes.AddRange(filterScope);
                    dialog.BuildData(alias.SelectMany(s => BusinessData.NamedScope.PathKeys(s.AliasPath)));

                    if (dialog.ShowDialog(this) is DialogResult.OK)
                    {
                        foreach (INamedScopeValue item in dialog.SelectedByNamedScope())
                        {
                            if (!alias.Any(w => w.AliasScope.Equals(item.Scope) && w.AliasPath.Equals(item.Path))
                                && dataBinding.AddNew() is IAliasSubType value)
                            {
                                value.AliasScope = item.Scope;
                                value.AliasPath = item.Path;
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
