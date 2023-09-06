﻿using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.Main.Forms;
using DataDictionary.Main.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Dialogs
{
    partial class Experiment : ApplicationBase
    {
        DefinitionKey? currentKey;

        public Experiment() : base()
        {
            InitializeComponent();
        }

        private void Experiment_Load(object sender, EventArgs e)
        {
            bindingSource.DataSource = Program.Data.Definitions;

            BindData();
        }

        private void bindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            DefinitionItem newItem = new DefinitionItem();

            if (bindingSource.Current is null || definitionNavigation.CurrentRow is null)
            {
                newItem.DefinitionTitle = definitionTitleData.Text;
                newItem.DefinitionDescription = definitionDescriptionData.Text;
            }

            e.NewObject = newItem;

            if (definitionNavigation.Focused)
            { definitionTitleData.Focus(); }
        }

        void BindData()
        {
            bindingSource.ResetBindings(false);

            if (Program.Data.Definitions.FirstOrDefault(w => currentKey is not null && currentKey.Equals(w)) is DefinitionItem priorItem)
            { bindingSource.Position = Program.Data.Definitions.IndexOf(priorItem); }

            definitionNavigation.AutoGenerateColumns = false;
            definitionNavigation.DataSource = bindingSource;

            DefinitionItem item = new DefinitionItem();
            definitionTitleData.DataBindings.Add(new Binding(nameof(definitionTitleData.Text), bindingSource, nameof(item.DefinitionTitle)));
            definitionDescriptionData.DataBindings.Add(new Binding(nameof(definitionDescriptionData.Text), bindingSource, nameof(item.DefinitionDescription)));
            obsoleteData.DataBindings.Add(new Binding(nameof(obsoleteData.Checked), bindingSource, nameof(item.Obsolete), true, DataSourceUpdateMode.OnValidation, false));
        }

        void UnBindData()
        {
            if (bindingSource.Current is DefinitionItem item)
            { currentKey = new DefinitionKey(item); }

            definitionNavigation.DataSource = null;
            definitionTitleData.DataBindings.Clear();
            definitionDescriptionData.DataBindings.Clear();
            obsoleteData.DataBindings.Clear();
        }

        #region IColleague
        protected override void HandleMessage(DbApplicationBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbApplicationBatchCompleted message)
        { BindData(); }

        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }
        #endregion

        private void ControlValidated(object sender, EventArgs e)
        {

            if (bindingSource.Current is null || definitionNavigation.CurrentRow is null)
            { bindingSource.AddNew(); }
        }

        private void definitionNavigation_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            definitionNavigation.ClearSelection();
            definitionNavigation.Rows[e.RowIndex].Selected = true;
        }

        private void definitionNavigation_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {

        }
    }
}
