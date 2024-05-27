using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms.Scripting.ComboBoxList;
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

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SelectionManager : ApplicationData
    {
        public Boolean IsOpenItem(object? item)
        { return bindingSelection.Current is ISelectionValue current && ReferenceEquals(current, item); }

        public SelectionManager() : base()
        {
            InitializeComponent();
            toolStrip.TransferItems(selectionToolStrip, 0);
        }

        public SelectionManager(ISelectionValue? selectionItem) : this()
        {
            if (selectionItem is null)
            {
                selectionItem = new SelectionValue();
                BusinessData.ScriptingEngine.Selections.Add(selectionItem);
            }

            SelectionIndex key = new SelectionIndex(selectionItem);
            bindingSelection.DataSource = new BindingView<SelectionValue>(BusinessData.ScriptingEngine.Selections, w => key.Equals(w));
            bindingSelection.Position = 0;

            Setup(bindingSelection);

            if (bindingSelection.Current is ISelectionValue current)
            {
                bindingSelectionItem.DataSource = new BindingView<SelectionPathValue>(BusinessData.ScriptingEngine.SelectionPaths, w => key.Equals(w));
            }
        }

        private void SelectionManager_Load(Object sender, EventArgs e)
        {
            ISelectionValue nameOfValue;
            this.DataBindings.Add(new Binding(nameof(this.Text), bindingSelection, nameof(nameOfValue.SelectionTitle), false, DataSourceUpdateMode.OnPropertyChanged));

            selectionTitleData.DataBindings.Add(new Binding(nameof(selectionTitleData.Text), bindingSelection, nameof(nameOfValue.SelectionTitle), false, DataSourceUpdateMode.OnPropertyChanged));
            selectionDescriptionData.DataBindings.Add(new Binding(nameof(selectionDescriptionData.Text), bindingSelection, nameof(nameOfValue.SelectionDescription), false, DataSourceUpdateMode.OnPropertyChanged));

            TransformNameMember.Load(transformData, BusinessData.ScriptingEngine.Transforms);
            transformData.DataBindings.Add(new Binding(nameof(transformData.SelectedValue), bindingSelection, nameof(nameOfValue.TransformId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty));

            DefinitionNameMember.Load(schemaData, BusinessData.ScriptingEngine.Schemta);
            schemaData.DataBindings.Add(new Binding(nameof(schemaData.SelectedValue), bindingSelection, nameof(nameOfValue.SchemaId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty));

            selectionItemData.AutoGenerateColumns = false;
            selectionItemData.DataSource = bindingSelectionItem;

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingSelection.Current is not ISelectionValue);
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);
        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);
        }

        private void NamedScopeData_OnApply(Object sender, EventArgs e)
        {
            if (bindingSelectionItem.DataSource is IList<ISelectionPathValue> aliases
                && aliases.FirstOrDefault(
                w => w.Scope == namedScopeData.Scope
                && new NamedScopePath(w.SelectionPath) == namedScopeData.ScopePath)
                is ISelectionPathValue value)
            { bindingSelectionItem.Position = aliases.IndexOf(value); }
            else { bindingSelectionItem.AddNew(); }
        }

        private void BindingSelectionItem_AddingNew(Object sender, AddingNewEventArgs e)
        {
            if (bindingSelection.Current is SelectionValue current)
            {
                SelectionPathValue newItem = new SelectionPathValue(current);
                newItem.SelectionPath = namedScopeData.ScopePath.MemberFullPath;
                newItem.Scope = namedScopeData.Scope;
                e.NewObject = newItem;
            }
        }

        private void BindingSelectionItem_CurrentChanged(Object sender, EventArgs e)
        {
            if (bindingSelectionItem.Current is ISelectionPathValue current)
            {
                NamedScopePath path = new NamedScopePath(current.SelectionPath);

                namedScopeData.ScopePath = path;
                namedScopeData.Scope = current.Scope;
            }
        }

        private void BindingSelection_AddingNew(Object sender, AddingNewEventArgs e)
        { }

        private void RemoveSelectionCommand_Click(Object sender, EventArgs e)
        {
            if (bindingSelection.Current is ISelectionValue current)
            { BusinessData.ScriptingEngine.Selections.Remove(current); }
        }
    }
}
