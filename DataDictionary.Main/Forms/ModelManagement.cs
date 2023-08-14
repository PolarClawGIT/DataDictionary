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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataDictionary.Main.Forms
{
    partial class ModelManagement : ApplicationFormBase
    {

        class FormData
        {
            public ModelItem? Model { get; set; }
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
            if (data.Models.FirstOrDefault(w => new ModelKey(w) == new ModelKey(Program.Data.Model)) is ModelItem modelExists)
            { data.Model = modelExists; }
            else
            {
                data.Models.Add(Program.Data.Model);
                data.Model = Program.Data.Model;
            }

            serverNameData.DataBindings.Add(new Binding(nameof(serverNameData.Text), Program.Data, nameof(Program.Data.ServerName)));
            databaseNameData.DataBindings.Add(new Binding(nameof(databaseNameData.Text), Program.Data, nameof(Program.Data.DatabaseName)));

            modelTitleData.DataBindings.Add(new Binding(nameof(modelTitleData.Text), data.Model, nameof(data.Model.ModelTitle)));
            modelDescriptionData.DataBindings.Add(new Binding(nameof(modelDescriptionData.Text), data.Model, nameof(data.Model.ModelDescription)));

            modelListData.AutoGenerateColumns = false;
            modelListData.DataSource = data.Models;

            // Because the DataGridView always selects a Row, even if it is just the first row.
            if (modelListData.Rows.Cast<DataGridViewRow>().FirstOrDefault(w => w.DataBoundItem is ModelItem item && new ModelKey(item) == new ModelKey(data.Model)) is DataGridViewRow currentRow)
            {
                modelListData.ClearSelection();
                currentRow.Selected = true;
            }
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
            if (modelListData.SelectedRows.Count > 0 && modelListData.SelectedRows[0].DataBoundItem is ModelItem selectedModel)
            {
                modelTitleData.DataBindings.Clear();
                modelDescriptionData.DataBindings.Clear();

                data.Model = selectedModel;

                if (new ModelKey(data.Model) == new ModelKey(Program.Data.Model))
                { saveCommand.Enabled = true; }
                else
                { saveCommand.Enabled = false; }

                if (data.Model.SysStart is null)
                { loadCommand.Enabled = false; }
                else
                { loadCommand.Enabled = true; }

                if (data.Model.Obsolete == true && new ModelKey(data.Model) != new ModelKey(Program.Data.Model))
                { deleteCommand.Enabled = true; }
                else { deleteCommand.Enabled = false; }

                modelTitleData.DataBindings.Add(new Binding(nameof(modelTitleData.Text), data.Model, nameof(data.Model.ModelTitle)));
                modelDescriptionData.DataBindings.Add(new Binding(nameof(modelDescriptionData.Text), data.Model, nameof(data.Model.ModelDescription)));
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
            if (data.Model is IModelItem model)
            {
                SendMessage(new DbDataBatchStarting());
                UnBindData();
                Program.Data.Clear();
                this.DoWork(Program.Data.LoadModel(new ModelKey(model)), onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                SendMessage(new DbDataBatchCompleted());
                this.Close();
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

        private void modelListData_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            //TODO: Update in place? Obsolete?
        }
    }
}
