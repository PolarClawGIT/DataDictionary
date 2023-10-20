using DataDictionary.Main.Properties;
using System.ComponentModel;
using DataDictionary.BusinessLayer.WorkFlows;
using DataDictionary.Main.Messages;
using Toolbox.Threading;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms;
using DataDictionary.DataLayer.ApplicationData.Model;

namespace DataDictionary.Main.Dialogs
{
    partial class OpenSaveModelDatabase : ApplicationBase, IApplicationDataBind
    {
        ModelKey currentKey = new ModelKey(Program.Data.Model);
        public Boolean IsOpenItem(Object? item) { return false; }

        public OpenSaveModelDatabase() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_SaveTable;
        }

        private void LoadModelFromDatabase_Load(object sender, EventArgs e)
        {
            // Constant, does not need to be rebound
            serverNameData.DataBindings.Add(new Binding(nameof(serverNameData.Text), Program.Data, nameof(Program.Data.ServerName)));
            databaseNameData.DataBindings.Add(new Binding(nameof(databaseNameData.Text), Program.Data, nameof(Program.Data.DatabaseName)));

            this.DoWork(Program.Data.LoadModelList(), onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                (this as IApplicationDataBind).BindData();
                SelectionChanged(modelList, EventArgs.Empty);
            }
        }

        public bool BindDataCore()
        {
            ModelItem currentModel = Program.Data.Model;

            if (Program.Data.Models.FirstOrDefault(w => new ModelKey(w) == currentKey) is ModelItem item)
            { currentModel = item; }

            modelTitleData.DataBindings.Add(new Binding(nameof(modelTitleData.Text), currentModel, nameof(currentModel.ModelTitle)));
            modelDescriptionData.DataBindings.Add(new Binding(nameof(modelDescriptionData.Text), currentModel, nameof(currentModel.ModelDescription)));

            modelList.SelectionChanged -= SelectionChanged; // Because setting the Data Source causes a Selection Change event to occur

            modelList.AutoGenerateColumns = false;
            modelList.DataSource = Program.Data.Models;
            modelList.ClearSelection();

            if (modelList.FirstOrDefault<ModelItem>(currentKey.Equals) is (DataGridViewRow, ModelItem) value && value.Row is DataGridViewRow row)
            { row.Selected = true; }
            else
            { currentKey = Program.Data.ModelKey; }

            modelList.SelectionChanged += SelectionChanged;
            return true;
        }

        public void UnbindDataCore()
        {
            modelTitleData.DataBindings.Clear();
            modelDescriptionData.DataBindings.Clear();
            modelList.DataSource = null;
        }

        private void loadCommand_Click(object sender, EventArgs e)
        {
            SendMessage(new DoUnbindData());
            this.DoWork(Program.Data.LoadModel(currentKey), onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                SendMessage(new DoBindData());
                SelectionChanged(modelList, EventArgs.Empty);
            }
        }

        private void deleteCommand_Click(object sender, EventArgs e)
        {
            SendMessage(new DoUnbindData());
            this.DoWork(Program.Data.DeleteModel(currentKey), onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                SendMessage(new DoBindData());
                SelectionChanged(modelList, EventArgs.Empty);
            }
        }

        private void saveCommand_Click(object sender, EventArgs e)
        {
            SendMessage(new DoUnbindData());
            this.DoWork(Program.Data.SaveModel(), onCompleting);


            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                List<WorkItem> work = new List<WorkItem>();
                if (args.Error is null)
                { this.DoWork(Program.Data.LoadModel(currentKey), onCompletingLoad); }
                else { SendMessage(new DoBindData()); }
            }

            void onCompletingLoad(RunWorkerCompletedEventArgs args)
            { SendMessage(new DoBindData()); }
        }

        private void SelectionChanged(object? sender, EventArgs e)
        {
            if (modelList.SelectedRows.Count > 0 && modelList.SelectedRows[0].DataBoundItem is ModelItem item)
            {
                (this as IApplicationDataBind).UnbindData();
                currentKey = new ModelKey(item);
                (this as IApplicationDataBind).BindData();
            }

            if (currentKey == Program.Data.ModelKey)
            {
                saveCommand.Enabled = true;

                modelTitleData.ReadOnly = false;
                modelDescriptionData.ReadOnly = false;

                if (Program.Data.Model.RowState() == System.Data.DataRowState.Added)
                {   loadCommand.Enabled = false; }
                else
                {loadCommand.Enabled = true; }
            }
            else
            {
                saveCommand.Enabled = false;
                loadCommand.Enabled = true;
                modelTitleData.ReadOnly = true;
                modelDescriptionData.ReadOnly = true;
            }
        }

    }
}
