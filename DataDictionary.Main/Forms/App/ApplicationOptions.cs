using DataDictionary.BusinessLayer.WorkFlows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataDictionary.Main.Properties;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Forms;
using Toolbox.Threading;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer;

namespace DataDictionary.Main.App
{
    partial class ApplicationOptions : ApplicationBase
    {
        public ApplicationOptions() : base()
        {
            InitializeComponent();
        }

        private void ApplicationOptions_Load(object sender, EventArgs e)
        {
            defaultModeOnLine.Checked = Settings.Default.IsOnLineMode;
            defaultModeOffLine.Checked = !Settings.Default.IsOnLineMode;
            defaultModeOnLine.DataBindings.Add(new Binding(nameof(defaultModeOnLine.Checked), Settings.Default, nameof(Settings.Default.IsOnLineMode)));
            serverNameData.DataBindings.Add(new Binding(nameof(serverNameData.Text), Settings.Default, nameof(Settings.Default.AppServer)));
            databaseNameData.DataBindings.Add(new Binding(nameof(databaseNameData.Text), Settings.Default, nameof(Settings.Default.AppDatabase)));
            applicationRoleData.DataBindings.Add(new Binding(nameof(applicationRoleData.Text), Settings.Default, nameof(Settings.Default.AppDbRole)));
            applicationFileData.DataBindings.Add(new Binding(nameof(applicationFileData.Text), Settings.Default, nameof(Settings.Default.AppDataFile)));
        }

        private void commandSaveToDatabase_Click(object sender, EventArgs e)
        {
            List<WorkItem> work = new List<WorkItem>();
            DatabaseWork factory = new DatabaseWork();
            work.Add(factory.OpenConnection());
            work.AddRange(Program.Data.SaveApplicationData(factory));
            this.DoWork(work, OnComplete);

            void OnComplete(RunWorkerCompletedEventArgs args)
            { } // Nothing to do at this point
        }

        private void commandLoadFromDatabase_Click(object sender, EventArgs e)
        {
            SendMessage(new DbApplicationBatchStarting());

            List<WorkItem> work = new List<WorkItem>();
            DatabaseWork factory = new DatabaseWork();
            work.Add(factory.OpenConnection());
            work.AddRange(Program.Data.LoadApplicationData(factory));
            this.DoWork(work, OnComplete);

            void OnComplete(RunWorkerCompletedEventArgs args)
            { SendMessage(new DbApplicationBatchCompleted()); } 
        }

        private void commandSaveToFile_Click(object sender, EventArgs e)
        {
            FileInfo appDataFile = new FileInfo(Path.Combine(Application.UserAppDataPath, Settings.Default.AppDataFile));
            this.DoWork(Program.Data.SaveApplicationData(appDataFile), OnComplete);

            void OnComplete(RunWorkerCompletedEventArgs args)
            { } // Nothing to do at this point
        }

        private void commandLoadFromFile_Click(object sender, EventArgs e)
        {
            SendMessage(new DbApplicationBatchStarting());

            FileInfo appDataFile = new FileInfo(Path.Combine(Application.UserAppDataPath, Settings.Default.AppDataFile));
            this.DoWork(Program.Data.LoadApplicationData(appDataFile), OnComplete);

            void OnComplete(RunWorkerCompletedEventArgs args)
            { SendMessage(new DbApplicationBatchCompleted()); }
        }

        private void defaultModeOnLine_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.IsOnLineMode = defaultModeOnLine.Checked;
            Settings.Default.Save();
            SendMessage(new OnlineStatusChanged());
        }
    }
}
