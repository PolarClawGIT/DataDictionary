using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ModelData;
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
    partial class ModelManager : ApplicationData, IApplicationDataBind
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
                    return (key.Equals(BusinessData.Model));
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
                    return (dbData.FirstOrDefault(w => key.Equals(w)) is IModelItem);
                }
                else { return false; }
            }
        }

        public ModelManager()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_SoftwareDefinitionModel;
        }

        private void ModelManager_Load(object sender, EventArgs e)
        {
            if (Settings.Default.IsOnLineMode)
            {
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                work.Add(factory.OpenConnection());
                work.Add(new WorkItem() { WorkName = "Clear local data", DoWork = dbData.Clear });
                work.AddRange(factory.CreateLoad(dbData).ToList());
                this.DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { this.BindData(); }
        }

        public bool BindDataCore()
        {
            bindingData.Build(BusinessData.Model, dbData);

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

        protected override void DeleteFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);
            modelNavigation.EndEdit();

            if (modelBinding.Current is ModelManagerItem item)
            {
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                IModelKey key = new ModelKey(item);

                work.Add(factory.OpenConnection());

                if (inModelList)
                {
                    work.AddRange(BusinessData.Remove());
                    work.AddRange(BusinessData.Save(factory, key));
                }
                else
                {
                    work.Add(new WorkItem() { DoWork = () => dbData.Remove(key) });
                    work.Add(factory.CreateSave(dbData, key));
                }

                DoLocalWork(work);
            }
        }

        protected override void OpenFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);
            modelNavigation.EndEdit();

            if (modelBinding.Current is ModelManagerItem item)
            {
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();

                ModelKey key = new ModelKey(item);
                work.Add(factory.OpenConnection());
                work.AddRange(BusinessData.Remove());
                work.AddRange(BusinessData.Load(factory, key));

                work.AddRange(BusinessData.NamedScope.Build());
                DoLocalWork(work);
            }
        }

        protected override void SaveToDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);
            modelNavigation.EndEdit();

            if (modelBinding.Current is ModelManagerItem item)
            {
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();

                ModelKey key = new ModelKey(item);
                work.Add(factory.OpenConnection());
                work.AddRange(BusinessData.Save(factory, key));

                DoLocalWork(work);
            }
        }

        private void DoLocalWork(List<WorkItem> work)
        {
            SendMessage(new Messages.DoUnbindData());

            this.DoWork(work, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { 
                SendMessage(new Messages.DoBindData());
                SendMessage(new Messages.RefreshNavigation());
            }
        }

        private void modelBinding_CurrentChanged(object sender, EventArgs e)
        {
            IsOpenDatabase = inDatabaseList && !inModelList;
            IsSaveDatabase = inModelList;
            IsDeleteDatabase = inDatabaseList;
        }

        private void BindingComplete(object sender, BindingCompleteEventArgs e)
        { if (sender is BindingSource binding) { binding.BindComplete(sender, e); } }

        private void newModelCommand_Click(object sender, EventArgs e)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(BusinessData.Remove());

            DoLocalWork(work);
        }
    }
}
