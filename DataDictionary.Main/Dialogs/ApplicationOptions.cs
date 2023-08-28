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

namespace DataDictionary.Main.Dialogs
{
    partial class ApplicationOptions : ApplicationFormBase
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
            this.DoWork(Program.Data.SaveApplicationData(), OnComplete);

            void OnComplete(RunWorkerCompletedEventArgs args)
            { } // Nothing to do at this point
        }

        private void commandLoadFromDatabase_Click(object sender, EventArgs e)
        {
            this.DoWork(Program.Data.LoadApplicationData(), OnComplete);

            void OnComplete(RunWorkerCompletedEventArgs args)
            { } // Nothing to do at this point
        }

        private void commandSaveToFile_Click(object sender, EventArgs e)
        {

            this.DoWork(Program.Data.SaveApplicationData(new FileInfo(Settings.Default.AppDataFile)), OnComplete);

            void OnComplete(RunWorkerCompletedEventArgs args)
            { } // Nothing to do at this point
        }

        private void commandLoadFromFile_Click(object sender, EventArgs e)
        {
            this.DoWork(Program.Data.LoadApplicationData(new FileInfo(Settings.Default.AppDataFile)), OnComplete);

            void OnComplete(RunWorkerCompletedEventArgs args)
            { } // Nothing to do at this point
        }

        private void defaultModeOnLine_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.IsOnLineMode = defaultModeOnLine.Checked;
            Settings.Default.Save();
        }
    }
}
