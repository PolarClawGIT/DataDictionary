using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.Main.Properties;
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
using DataDictionary.BusinessLayer.WorkFlows;
using DataDictionary.Main.Messages;
using System.Windows.Forms.VisualStyles;
using System.Reflection;
using Toolbox.Mediator;
using Toolbox.Threading;

namespace DataDictionary.Main.Dialogs
{
    partial class OpenSaveModelDatabase : ApplicationBase
    {
        ModelKey currentKey = new ModelKey(Program.Data.Model);

        public OpenSaveModelDatabase() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DbOpenSave;
        }


        private void LoadModelFromDatabase_Load(object sender, EventArgs e)
        {
            // Constant, does not need to be rebound
            serverNameData.DataBindings.Add(new Binding(nameof(serverNameData.Text), Program.Data, nameof(Program.Data.ServerName)));
            databaseNameData.DataBindings.Add(new Binding(nameof(databaseNameData.Text), Program.Data, nameof(Program.Data.DatabaseName)));

            this.DoWork(Program.Data.LoadModelList(), onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                BindData();
                SelectionChanged(modelList, EventArgs.Empty);
            }
        }

        void BindData()
        {
            ModelItem currentModel = Program.Data.Model;

            if (Program.Data.Models.FirstOrDefault(w => new ModelKey(w) == currentKey) is ModelItem item)
            { currentModel = item; }

            modelTitleData.DataBindings.Add(new Binding(nameof(modelTitleData.Text), currentModel, nameof(currentModel.ModelTitle)));
            modelDescriptionData.DataBindings.Add(new Binding(nameof(modelDescriptionData.Text), currentModel, nameof(currentModel.ModelDescription)));
            modelObsoleteData.DataBindings.Add(new Binding(nameof(modelObsoleteData.Checked), currentModel, nameof(currentModel.Obsolete), true, DataSourceUpdateMode.OnValidation, false));

            modelList.SelectionChanged -= SelectionChanged; // Because setting the Data Source causes a Selection Change event to occur

            modelList.AutoGenerateColumns = false;
            modelList.DataSource = Program.Data.Models;
            modelList.ClearSelection();

            if (modelList.FindRow<ModelItem, ModelKey>(currentKey, (item) => new ModelKey(item)).row is DataGridViewRow row)
            { row.Selected = true; }
            else
            { currentKey = Program.Data.ModelKey; }

            modelList.SelectionChanged += SelectionChanged;
        }

        void UnBindData()
        {
            modelTitleData.DataBindings.Clear();
            modelDescriptionData.DataBindings.Clear();
            modelObsoleteData.DataBindings.Clear();
            modelList.DataSource = null;
        }

        private void loadCommand_Click(object sender, EventArgs e)
        {
            SendMessage(new DbDataBatchStarting());
            UnBindData();
            this.DoWork(Program.Data.LoadModel(currentKey), onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                BindData();
                SendMessage(new DbDataBatchCompleted());
                SelectionChanged(modelList, EventArgs.Empty);
            }
        }

        private void deleteCommand_Click(object sender, EventArgs e)
        {
            SendMessage(new DbDataBatchStarting());
            UnBindData();
            this.DoWork(Program.Data.DeleteModel(currentKey), onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                BindData();
                SendMessage(new DbDataBatchCompleted());
                SelectionChanged(modelList, EventArgs.Empty);
            }
        }

        private void saveCommand_Click(object sender, EventArgs e)
        {
            SendMessage(new DbDataBatchStarting());
            UnBindData();
            this.DoWork(Program.Data.SaveModel(), onCompleting);


            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                List<WorkItem> work = new List<WorkItem>();

                if (args.Error is null)
                { this.DoWork(Program.Data.LoadModel(currentKey), onCompletingLoad); }
                else { BindData(); SendMessage(new DbDataBatchCompleted()); }
            }

            void onCompletingLoad(RunWorkerCompletedEventArgs args)
            { BindData(); SendMessage(new DbDataBatchCompleted()); }
        }

        private void SelectionChanged(object? sender, EventArgs e)
        {
            if (modelList.SelectedRows.Count > 0 && modelList.SelectedRows[0].DataBoundItem is ModelItem item)
            {
                UnBindData();
                currentKey = new ModelKey(item);
                BindData();
            }

            if (currentKey == Program.Data.ModelKey)
            {
                saveCommand.Enabled = true;

                modelTitleData.ReadOnly = false;
                modelDescriptionData.ReadOnly = false;

                if (Program.Data.Model.SysStart is null)
                {
                    modelObsoleteData.Enabled = false;
                    loadCommand.Enabled = false;
                }
                else
                {
                    modelObsoleteData.Enabled = true;
                    loadCommand.Enabled = true;
                }
            }
            else
            {
                saveCommand.Enabled = false;
                loadCommand.Enabled = true;
                modelTitleData.ReadOnly = true;
                modelDescriptionData.ReadOnly = true;
                modelObsoleteData.Enabled = false;
            }

            if (currentKey == Program.Data.ModelKey && Program.Data.Model.Obsolete == true)
            { deleteCommand.Enabled = true; }
            else { deleteCommand.Enabled = false; }
        }

        #region IColleague
        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }
        #endregion


    }
}
