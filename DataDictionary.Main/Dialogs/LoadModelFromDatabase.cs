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
    partial class LoadModelFromDatabase : ApplicationFormBase
    {
        class FormData
        {
            public ModelItem? ModelItem { get; set; }
            public BindingTable<ModelItem> ModelItems = new BindingTable<ModelItem>();
        }

        FormData data = new FormData();


        public LoadModelFromDatabase() : base()
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
            }
        }


        private void loadCommand_Click(object sender, EventArgs e)
        {
            if (data.ModelItem is not null)
            {
                SendMessage(new DbDataBatchStarting());
                this.DoWork(Program.Data.LoadModel(new ModelKey(data.ModelItem)), onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                SendMessage(new DbDataBatchCompleted());
                this.Close();
            }
        }

        private void deleteCommand_Click(object sender, EventArgs e)
        {
            if (data.ModelItem is not null && data.ModelItem.Obsolete == true)
            { this.DoWork(Program.Data.DeleteModel(data.ModelItem), onCompleting); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                if (modelList.Rows.Cast<DataGridViewRow>().FirstOrDefault(w => w.DataBoundItem is ModelItem item && item == data.ModelItem) is DataGridViewRow row)
                {
                    modelList.Rows.Remove(row);
                    modelTitleData.DataBindings.Clear();
                    data.ModelItem = null;
                }
            }
        }


        private void modelList_SelectionChanged(object sender, EventArgs e)
        {
            if (modelList.SelectedRows.Count > 0 && modelList.SelectedRows[0].DataBoundItem is ModelItem item)
            {
                data.ModelItem = item;
                modelTitleData.DataBindings.Clear();
                modelTitleData.DataBindings.Add(new Binding(nameof(modelTitleData.Text), data.ModelItem, nameof(data.ModelItem.ModelTitle)));

                if (data.ModelItem.Obsolete == true)
                { deleteCommand.Enabled = true; }
                else { deleteCommand.Enabled = false; }
            }
        }

        #region IColleague

        #endregion
    }
}
