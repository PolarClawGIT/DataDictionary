using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Dialogs;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
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

            aliasAddCommand.Image = ScopeType.ModelAlias.GetNavigation().Images[CommandType.Add];
            aliasSelectCommand.Image = ScopeType.ModelAlias.GetNavigation().Images[CommandType.Select];
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
            aliasNameData.DataBindings.Add(
                new Binding(nameof(TextBox.Text),
                dataBinding, 
                nameof(IAliasSubType.AliasPath))
                    .WithParse<PathIndex, String>(
                        (p) => p.MemberFullPath,
                        (s) => new PathIndex(PathIndex.Parse(s))));

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
        { }


        private void AliasNameData_Validated(object sender, EventArgs e)
        { }

        private void AliasSelectCommand_Click(object sender, EventArgs e)
        {
            if (dataBinding is not null
                && ParentForm is not null)
            {
                using (SelectionDialog dialog = new SelectionDialog(ParentForm))
                {
                    dialog.FilterScopes.AddRange(filterScope);
                    dialog.BuildData(Aliases.SelectMany(s => BusinessData.NamedScope.PathKeys(s.AliasPath)));

                    if (dialog.ShowDialog(this) is DialogResult.OK)
                    {
                        foreach (INamedScopeValue item in dialog.SelectedByNamedScope())
                        {
                            if (!Aliases.Any(w => w.AliasScope.Equals(item.Scope) && w.AliasPath.Equals(item.Path))
                                && dataBinding.AddNew() is IAliasSubType value)
                            {
                                value.AliasScope = item.Scope;
                                value.AliasPath = item.Path;

                                dataBinding.Position = Aliases.ToList().IndexOf(value);
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
