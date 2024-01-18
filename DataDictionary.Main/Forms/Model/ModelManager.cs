using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Messages;
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
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Model
{
    partial class ModelManager : ApplicationBase, IApplicationDataBind
    {
        ModelCollection dbData = new ModelCollection();
        ModelManagerCollection bindingData = new ModelManagerCollection();

        Boolean inModelList
        {
            get
            {
                if (modelBinding.Current is ModelManagerItem item)
                {
                    ModelKey key = new ModelKey(item);
                    return (Program.Data.Models.FirstOrDefault(w => key.Equals(w)) is ModelItem);
                }
                else { return false; }
            }
        }

        Boolean inDatabaseList
        {
            get
            {
                if (modelBinding.Current is ModelManagerItem item)
                {
                    ModelKey key = new ModelKey(item);
                    return (dbData.FirstOrDefault(w => key.Equals(w)) is ModelItem);
                }
                else { return false; }
            }
        }

        public ModelManager()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_SoftwareDefinitionModel;

            newItemCommand.Enabled = true;
            newItemCommand.Click += NewItemCommand_Click;
            newItemCommand.Image = Resources.NewDictionary;
            newItemCommand.ToolTipText = "Create a empty Model";

            openFromDatabaseCommand.Click += OpenFromDatabaseCommand_Click;
            deleteFromDatabaseCommand.Click += DeleteFromDatabaseCommand_Click;
            saveToDatabaseCommand.Click += SaveToDatabaseCommand_Click;
        }

        private void ModelManager_Load(object sender, EventArgs e)
        {
            if (Settings.Default.IsOnLineMode)
            {
                List<WorkItem> work = new List<WorkItem>();
                DatabaseWork factory = new DatabaseWork();
                work.Add(factory.OpenConnection());
                work.AddRange(LoadLocalData(factory));
                this.DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { this.BindData(); }
        }

        public bool BindDataCore()
        {
            bindingData.Build(Program.Data.Models, dbData);

            modelBinding.DataSource = bindingData;

            modelNavigation.AutoGenerateColumns = false;
            modelNavigation.DataSource = modelBinding;

            ModelManagerItem? nameOfValues;
            modelTitleData.DataBindings.Add(new Binding(nameof(modelTitleData.Text), modelBinding, nameof(nameOfValues.ModelTitle)));
            modelDescriptionData.DataBindings.Add(new Binding(nameof(modelDescriptionData.Text), modelBinding, nameof(nameOfValues.ModelDescription)));

            return true;
        }

        public void UnbindDataCore()
        {
            modelTitleData.DataBindings.Clear();
            modelDescriptionData.DataBindings.Clear();

            modelNavigation.DataSource = null;
            modelBinding.DataSource = null;
        }

        private void NewItemCommand_Click(object? sender, EventArgs e)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(Program.Data.NewModel());

            DoLocalWork(work);
        }


        private void DeleteFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            modelNavigation.EndEdit();

            if (modelBinding.Current is ModelManagerItem item)
            {
                List<WorkItem> work = new List<WorkItem>();
                DatabaseWork factory = new DatabaseWork();
                ModelKey key = new ModelKey(item);
                
                work.Add(factory.OpenConnection());

                if (inModelList) { work.AddRange(Program.Data.DeleteModel(factory, key)); }
                else { work.AddRange(dbData.DeleteModel(factory, key)); }

                work.AddRange(LoadLocalData(factory));
                DoLocalWork(work);
            }
        }

        private void OpenFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            modelNavigation.EndEdit();

            if (modelBinding.Current is ModelManagerItem item)
            {
                List<WorkItem> work = new List<WorkItem>();
                DatabaseWork factory = new DatabaseWork();

                ModelKey key = new ModelKey(item);
                work.Add(factory.OpenConnection());
                work.AddRange(Program.Data.LoadModel(factory, key));

                DoLocalWork(work);
            }
        }

        private void SaveToDatabaseCommand_Click(object? sender, EventArgs e)
        {
            modelNavigation.EndEdit();

            if (modelBinding.Current is ModelManagerItem item)
            {
                List<WorkItem> work = new List<WorkItem>();
                DatabaseWork factory = new DatabaseWork();

                ModelKey key = new ModelKey(item);
                work.Add(factory.OpenConnection());
                work.AddRange(Program.Data.SaveModel(factory,Program.Data.ModelKey));
                work.AddRange(LoadLocalData(factory));

                DoLocalWork(work);
            }
        }

        private void DoLocalWork(List<WorkItem> work)
        {
            SendMessage(new Messages.DoUnbindData());

            this.DoWork(work, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { SendMessage(new Messages.DoBindData()); }
        }

        private IReadOnlyList<WorkItem> LoadLocalData(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { WorkName = "Clear local data", DoWork = dbData.Clear });
            work.AddRange(dbData.LoadModel(factory));

            return work;
        }

        protected override void HandleMessage(OnlineStatusChanged message)
        {
            base.HandleMessage(message);
            openFromDatabaseCommand.Enabled = Settings.Default.IsOnLineMode && inDatabaseList && !inModelList;
            deleteFromDatabaseCommand.Enabled = Settings.Default.IsOnLineMode && inDatabaseList;
            saveToDatabaseCommand.Enabled = Settings.Default.IsOnLineMode && inModelList;
        }

        private void modelBinding_CurrentChanged(object sender, EventArgs e)
        {
            openFromDatabaseCommand.Enabled = Settings.Default.IsOnLineMode && inDatabaseList && !inModelList;
            deleteFromDatabaseCommand.Enabled = Settings.Default.IsOnLineMode && inDatabaseList;
            saveToDatabaseCommand.Enabled = Settings.Default.IsOnLineMode && inModelList;
        }

        private void BindingComplete(object sender, BindingCompleteEventArgs e)
        { if (sender is BindingSource binding) { binding.BindComplete(sender, e); } }


    }
}
