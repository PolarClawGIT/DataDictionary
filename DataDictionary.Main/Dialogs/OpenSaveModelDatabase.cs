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
using DataDictionary.Main.Forms;
using System.Reflection;
using Toolbox.Mediator;

namespace DataDictionary.Main.Dialogs
{
    partial class OpenSaveModelDatabase : ApplicationFormBase
    {
        class FormData
        {
            public ModelItem? Model { get; set; }
            public BindingTable<ModelItem> ModelItems = new BindingTable<ModelItem>();
        }

        FormData data = new FormData();


        public OpenSaveModelDatabase() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DbOpenSave;
        }


        private void LoadModelFromDatabase_Load(object sender, EventArgs e)
        {
            this.DoWork(Program.Data.LoadModelList(data.ModelItems), onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                serverNameData.DataBindings.Add(new Binding(nameof(serverNameData.Text), Program.Data, nameof(Program.Data.ServerName)));
                databaseNameData.DataBindings.Add(new Binding(nameof(databaseNameData.Text), Program.Data, nameof(Program.Data.DatabaseName)));

                modelList.AutoGenerateColumns = false;
                modelList.DataSource = data.ModelItems;
                modelList.ClearSelection();
            }
        }

        void BindData()
        {
            modelTitleData.DataBindings.Add(new Binding(nameof(modelTitleData.Text), data.Model, nameof(data.Model.ModelTitle)));
            modelDescriptionData.DataBindings.Add(new Binding(nameof(modelDescriptionData.Text), data.Model, nameof(data.Model.ModelDescription)));
            modelObsoleteData.DataBindings.Add(new Binding(nameof(modelObsoleteData.Checked), data.Model, nameof(data.Model.Obsolete), true, DataSourceUpdateMode.OnValidation, false));

        }

        void UnBindData()
        {
            modelTitleData.DataBindings.Clear();
            modelDescriptionData.DataBindings.Clear();
            modelObsoleteData.DataBindings.Clear();
        }

        private void loadCommand_Click(object sender, EventArgs e)
        {
            if (modelList.SelectedRows.Count > 0 && modelList.SelectedRows[0].DataBoundItem is ModelItem item)
            { data.Model = item; }

            if (data.Model is not null)
            {
                SendMessage(new DbDataBatchStarting());
                this.DoWork(Program.Data.LoadModel(new ModelKey(data.Model)), onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                SendMessage(new DbDataBatchCompleted());
                this.Close();
            }
        }

        private void deleteCommand_Click(object sender, EventArgs e)
        {
            if (data.Model is not null && data.Model.Obsolete == true)
            { this.DoWork(Program.Data.DeleteModel(data.Model), onCompleting); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                if (modelList.Rows.Cast<DataGridViewRow>().FirstOrDefault(w => w.DataBoundItem is ModelItem item && item == data.Model) is DataGridViewRow row)
                {
                    modelList.Rows.Remove(row);
                    modelTitleData.DataBindings.Clear();
                    data.Model = null;
                }
            }
        }

        private void saveCommand_Click(object sender, EventArgs e)
        {
            SendMessage(new DbDataBatchStarting());
            UnBindData();

            this.DoWork(Program.Data.SaveModel(), onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is null)
                { this.DoWork(Program.Data.SaveModel(), onCompletingLoad); }
                else { SendMessage(new DbDataBatchCompleted()); }
            }

            void onCompletingLoad(RunWorkerCompletedEventArgs args)
            {
                SendMessage(new DbDataBatchCompleted());
                BindData();
            }
        }

        private void modelList_SelectionChanged(object sender, EventArgs e)
        {
            if (modelList.SelectedRows.Count > 0 && modelList.SelectedRows[0].DataBoundItem is ModelItem item)
            {
                UnBindData();
                data.Model = item;
                BindData();

                if (data.Model is ModelItem && new ModelKey(data.Model) == new ModelKey(Program.Data.Model))
                {
                    saveCommand.Enabled = true;
                    loadCommand.Enabled = true;
                    modelTitleData.ReadOnly = false;
                    modelDescriptionData.ReadOnly = false;
                    modelObsoleteData.Enabled = true;
                }
                else
                {
                    saveCommand.Enabled = false;
                    loadCommand.Enabled = true;
                    modelTitleData.ReadOnly = true;
                    modelDescriptionData.ReadOnly = true;
                    modelObsoleteData.Enabled = false;
                }

                if (data.Model.Obsolete == true)
                { deleteCommand.Enabled = true; }
                else { deleteCommand.Enabled = false; }
            }
            else
            { // ClearSelection was called
                UnBindData();
                data.Model = Program.Data.Model;
                BindData();

                saveCommand.Enabled = true;
                loadCommand.Enabled = false;
                deleteCommand.Enabled = false;
                modelTitleData.ReadOnly = false;
                modelDescriptionData.ReadOnly = false;
                modelObsoleteData.Enabled = true;
            }
        }

        #region IColleague

        #endregion


    }
}
