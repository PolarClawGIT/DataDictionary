using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.Threading;
using DataDictionary.BusinessLayer.AppModel;

namespace DataDictionary.Main.Forms.Model
{
    partial class ModelManager : ApplicationData
    {
        ModelSynchronize models = new ModelSynchronize(BusinessData);

        public ModelManager()
        {
            InitializeComponent();
            newModelCommand.Image = ScopeType.Model.GetImage(CommandType.Add);

            SetIcon(ScopeType.Model);
            SetCommand(ScopeType.Model,
                CommandType.OpenDatabase,
                CommandType.SaveDatabase,
                CommandType.DeleteDatabase);
        }

        private void ModelManager_Load(object sender, EventArgs e)
        {
            if (Settings.Default.IsOnLineMode)
            {
                IsLocked(true);
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                work.Add(factory.OpenConnection());
                work.AddRange(models.GetModels(factory));
                DoWork(work, onCompleting);
            }
            else { BindData(); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                models.Refresh();
                BindData();
                IsLocked(false);
            }

            void BindData()
            {
                ModelSynchronizeValue modelNames;
                modelBinding.DataSource = models;
                Func<String, String> FormatName = (name) => { return String.Format("{0}.{1}", nameof(modelNames.Source), name); };

                modelNavigation.AutoGenerateColumns = false;
                modelNavigation.DataSource = modelBinding;

                modelTitleData.DataBindings.Add(new Binding(nameof(modelTitleData.Text), modelBinding, FormatName(nameof(modelNames.Source.ModelTitle))));
                modelDescriptionData.DataBindings.Add(new Binding(nameof(modelDescriptionData.Text), modelBinding, FormatName(nameof(modelNames.Source.ModelDescription)), false, DataSourceUpdateMode.OnPropertyChanged));
            }
        }


        protected override void DeleteFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);
            modelNavigation.EndEdit();

            if (modelBinding.Current is ModelSynchronizeValue value && value.Source is IModelValue item)
            {
                IsLocked(true);
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                ModelIndex key = new ModelIndex(item);

                work.Add(factory.OpenConnection());
                work.AddRange(models.DeleteFromDb(factory, key));
                work.AddRange(models.GetModels(factory));
                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                models.Refresh();
                IsLocked(false);
            }
        }

        protected override void OpenFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);
            modelNavigation.EndEdit();

            if (modelBinding.Current is ModelSynchronizeValue value && value.Source is IModelValue item)
            {
                IsLocked(true);
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                ModelIndex key = new ModelIndex(item);

                work.Add(factory.OpenConnection());
                work.AddRange(models.OpenFromDb(factory, key));

                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                models.Refresh();
                SendMessage(new RefreshNavigation());
                IsLocked(false);
            }
        }

        protected override void SaveToDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);
            modelNavigation.EndEdit();


            if (modelBinding.Current is ModelSynchronizeValue value && value.Source is IModelValue item)
            {
                IsLocked(true);
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                ModelIndex key = new ModelIndex(item);

                work.Add(factory.OpenConnection());

                if (GetInModel())
                {
                    work.AddRange(models.SaveToDb(factory, key));
                    work.AddRange(models.GetModels(factory));
                }

                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                models.Refresh();
                IsLocked(false);
            }

        }

        private Boolean GetInModel()
        {
            if (modelBinding.Current is ModelSynchronizeValue item)
            { return item.InModel == true; }
            else { return false; }
        }

        private Boolean GetInDatabase()
        {
            if (modelBinding.Current is ModelSynchronizeValue item)
            { return item.InDatabase == true; }
            else { return false; }
        }

        private void modelBinding_CurrentChanged(object sender, EventArgs e)
        {
            CommandButtons[CommandType.OpenDatabase].IsEnabled = GetInDatabase() && !GetInModel();
            CommandButtons[CommandType.SaveDatabase].IsEnabled = GetInModel();
            CommandButtons[CommandType.DeleteDatabase].IsEnabled = GetInDatabase();
        }

        private void newModelCommand_Click(object sender, EventArgs e)
        {
            IsLocked(true);
            DoWork(BusinessData.Delete(), onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                models.Refresh();
                IsLocked(false);
            }
        }
    }
}
