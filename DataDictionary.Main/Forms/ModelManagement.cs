using DataDictionary.BusinessLayer.WorkFlows;
using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.BindingTable;
using Toolbox.DbContext;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms
{
    partial class ModelManagement : ApplicationFormBase
    {

        class FormData
        {
            public ModelItem Model { get; set; } = new ModelItem();
            public BindingTable<ModelItem> Models { get; } = new BindingTable<ModelItem>();
        }

        FormData data = new FormData();

        public ModelManagement() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DbOpenSave;
        }


        private void ModelManagement_Load(object sender, EventArgs e)
        {
            this.DoWork(Program.Data.LoadModelList(data.Models), onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs result)
            { BindData(); }
        }

        void BindData()
        {
            modelListData.ClearSelection();
            if (modelListData.Rows.Cast<DataGridViewRow>().FirstOrDefault(w => w.DataBoundItem is ModelItem item && item.ModelId == Program.Data.Model.ModelId) is DataGridViewRow currentItem)
            { currentItem.Selected = true; }
            else { data.Model = Program.Data.Model; }

            serverNameData.DataBindings.Add(new Binding(nameof(serverNameData.Text), Program.Data, nameof(Program.Data.ServerName)));
            databaseNameData.DataBindings.Add(new Binding(nameof(databaseNameData.Text), Program.Data, nameof(Program.Data.DatabaseName)));

            modelTitleData.DataBindings.Add(new Binding(nameof(modelTitleData.Text), data.Model, nameof(data.Model.ModelTitle)));
            modelDescriptionData.DataBindings.Add(new Binding(nameof(modelDescriptionData.Text), data.Model, nameof(data.Model.ModelDescription)));

            modelListData.AutoGenerateColumns = false;
            modelListData.DataSource = data.Models;

            if (data.Models.Count > 0)
            { loadCommand.Enabled = true; }
            else { loadCommand.Enabled = false; }

            if (data.Model.Obsolete == true && data.Model.ModelId != Program.Data.Model.ModelId)
            { deleteCommand.Enabled = true; }
            else { deleteCommand.Enabled = false; }

            if (data.Model.ModelId == Program.Data.Model.ModelId)
            { saveCommand.Enabled = true; }
            else { saveCommand.Enabled = false; }
        }

        void UnBindData()
        {
            serverNameData.DataBindings.Clear();
            databaseNameData.DataBindings.Clear();
            modelTitleData.DataBindings.Clear();
            modelDescriptionData.DataBindings.Clear();
            modelListData.DataSource = null;
        }

        private void modelListData_SelectionChanged(object sender, EventArgs e)
        {
            if (modelListData.SelectedRows.Count > 0)
            {
                if (modelListData.SelectedRows[0].DataBoundItem is ModelItem newItem)
                {
                    modelTitleData.DataBindings.Clear();
                    modelDescriptionData.DataBindings.Clear();

                    data.Model = newItem;
                    if (data.Model.ModelId == Program.Data.Model.ModelId)
                    { saveCommand.Enabled = true; }
                    else { saveCommand.Enabled = false; }

                    if (data.Model.Obsolete == true && data.Model.ModelId != Program.Data.Model.ModelId)
                    { deleteCommand.Enabled = true; }
                    else { deleteCommand.Enabled = false; }

                    modelTitleData.DataBindings.Add(new Binding(nameof(modelTitleData.Text), data.Model, nameof(data.Model.ModelTitle)));
                    modelDescriptionData.DataBindings.Add(new Binding(nameof(modelDescriptionData.Text), data.Model, nameof(data.Model.ModelDescription)));
                }
            }
        }

        private void saveCommand_Click(object sender, EventArgs e)
        {
            SendMessage(new DbDataBatchStarting());
            UnBindData();
            data.Models.Clear();

            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(Program.Data.SaveModel());
            work.AddRange(Program.Data.LoadModelList(data.Models));
            this.DoWork(work, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                BindData();
                SendMessage(new DbDataBatchCompleted());
            }
        }

        private void loadCommand_Click(object sender, EventArgs e)
        {
            SendMessage(new DbDataBatchStarting());
            UnBindData();
            Program.Data.Clear();
            this.DoWork(Program.Data.LoadModel(data.Model), onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                BindData();
                SendMessage(new DbDataBatchCompleted());
            }
        }

        private void deleteCommand_Click(object sender, EventArgs e)
        {
            UnBindData();
            data.Models.Clear();
            data.Model = new ModelItem();

            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(Program.Data.DeleteModel(data.Model));
            work.AddRange(Program.Data.LoadModelList(data.Models));
            this.DoWork(work, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { BindData(); }
        }

        private void ModelManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DoWork(Program.Data.SaveModelList(data.Models), onCompleting);

            // Do not think there is anything to be done. 
            void onCompleting(RunWorkerCompletedEventArgs args) { }
        }


    }
}
