using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.Main.Forms;
using DataDictionary.Main.Properties;
using DataDictionary.BusinessLayer.WorkFlows;
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
using Toolbox.DbContext;
using DataDictionary.Main.Messages;

namespace DataDictionary.Main.Dialogs
{
    partial class SaveModelToDatabase : ApplicationFormBase
    {
        class FormData
        {
            public ModelItem Model { get; } = Program.Data.Model;
            public String ServerName { get; } = Program.Data.ServerName;
            public String DatabaseName { get; } = Program.Data.DatabaseName;
        }

        FormData data = new FormData();


        public SaveModelToDatabase()
        {
            InitializeComponent();
            this.Icon = Resources.DbOpenSave;
        }

        private void SaveModelToDatabase_Load(object sender, EventArgs e)
        {
            serverNameData.DataBindings.Add(new Binding(nameof(serverNameData.Text), data, nameof(data.ServerName)));
            databaseNameData.DataBindings.Add(new Binding(nameof(databaseNameData.Text), data, nameof(data.DatabaseName)));

            modelTitleData.DataBindings.Add(new Binding(nameof(modelTitleData.Text), data.Model, nameof(data.Model.ModelTitle)));
            modelDescriptionData.DataBindings.Add(new Binding(nameof(modelDescriptionData.Text), data.Model, nameof(data.Model.ModelDescription)));
            modelObsoleteData.DataBindings.Add(new Binding(nameof(modelObsoleteData.Checked), data.Model, nameof(data.Model.Obsolete)));
            modelSysStartData.DataBindings.Add(new Binding(nameof(modelSysStartData.Text), data.Model, nameof(data.Model.SysStart)));
        }

        private void saveCommand_Click(object sender, EventArgs e)
        {
            SendMessage(new DbDataBatchStarting());
            this.DoWork(Program.Data.SaveModel(), onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                SendMessage(new DbDataBatchCompleted());
                this.Close();
            }
        }
    }
}
