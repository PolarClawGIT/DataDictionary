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
        FormBinding formBinding;

        public ModelManager()
        {
            InitializeComponent();
            newModelCommand.Image = ScopeType.Model.GetImage(ButtonType.Add);

            SetIcon(ScopeType.Model);

            SetCommand(
                ButtonType.OpenDatabase,
                ButtonType.SaveDatabase,
                ButtonType.DeleteDatabase);

            formBinding = new FormBinding()
            {
                ManagerBinding = modelBinding,
                DoWork = base.DoWork,
                OnRefresh = () => { SendMessage(new RefreshNavigation()); }
            };
        }

        private void ModelManager_Load(object sender, EventArgs e)
        {
            formBinding.Load(doBinding);

            void doBinding(RunWorkerCompletedEventArgs args)
            {
                modelNavigation.AutoGenerateColumns = false;
                modelNavigation.DataSource = modelBinding;

                modelTitleData.DataBindings.Add(new Binding(nameof(modelTitleData.Text), modelBinding, nameof(BindingValue.ModelTitle)));
                modelDescriptionData.DataBindings.Add(new Binding(nameof(modelDescriptionData.Text), modelBinding, nameof(BindingValue.ModelDescription), false, DataSourceUpdateMode.OnPropertyChanged));
            }
        }


        protected override void DeleteFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);
            modelNavigation.EndEdit();

            if (formBinding.TryGetValue(out BindingValue? binding))
            { formBinding.Delete(binding, onComplete); }

            void onComplete(RunWorkerCompletedEventArgs args)
            { SendMessage(new RefreshNavigation()); }
        }

        protected override void OpenFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);
            modelNavigation.EndEdit();

            if (formBinding.TryGetValue(out BindingValue? binding))
            { formBinding.Load(binding, onComplete); }

            void onComplete(RunWorkerCompletedEventArgs args)
            { SendMessage(new RefreshNavigation()); }
        }

        protected override void SaveToDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);
            modelNavigation.EndEdit();

            if (formBinding.TryGetValue(out BindingValue? binding))
            { formBinding.Save(binding, onComplete); }

            void onComplete(RunWorkerCompletedEventArgs args)
            { SendMessage(new RefreshNavigation()); }

        }

        private void NewModelCommand_Click(object sender, EventArgs e)
        { formBinding.Create(); }

        private void ModelBinding_CurrentItemChanged(object sender, EventArgs e)
        {
            if (formBinding.TryGetValue(out BindingValue? binding))
            {
                CommandButtons[ButtonType.Delete].IsEnabled = binding.InModel;

                CommandButtons[ButtonType.OpenDatabase].IsEnabled = binding.InDatabase && !binding.InModel;
                CommandButtons[ButtonType.SaveDatabase].IsEnabled = binding.InModel;
                CommandButtons[ButtonType.DeleteDatabase].IsEnabled = binding.InDatabase;
            }
        }
    }
}
