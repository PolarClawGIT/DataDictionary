using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateManager : ApplicationData
    {
        FormBinding formBinding;

        public TemplateManager()
        {
            InitializeComponent();

            SetIcon(ScopeType.Scripting);
            SetCommand(
                ScopeType.Scripting,
                CommandType.Delete,
                CommandType.OpenDatabase,
                CommandType.SaveDatabase,
                CommandType.DeleteDatabase
                );
            newTemplate.Image = ScopeType.ScriptingTemplate.GetImage(CommandType.Add);
            newDataSource.Image = ScopeType.ScriptingData.GetImage(CommandType.Add);

            AddCommands(templateCommands, ToolStripItemDisplayStyle.Image, CommandType.Add);

            formBinding = new FormBinding()
            {
                ManagerBinding = bindingManager,
                DoWork = base.DoWork,
                OnRefresh = () => { SendMessage(new RefreshNavigation()); }
            };
        }

        private void TemplateManager_Load(object sender, EventArgs e)
        {
            CommandButtons[CommandType.Delete].IsEnabled = false;

            CommandButtons[CommandType.OpenDatabase].IsEnabled = false;
            CommandButtons[CommandType.SaveDatabase].IsEnabled = false;
            CommandButtons[CommandType.DeleteDatabase].IsEnabled = false;

            formBinding.Load(doBinding);

            void doBinding(RunWorkerCompletedEventArgs args)
            {
                managerData.AutoGenerateColumns = false;
                managerData.DataSource = bindingManager;

                titleData.DataBindings.Add(new Binding(nameof(titleData.Text), bindingManager, nameof(BindingValue.Title)));
                descriptionData.DataBindings.Add(new Binding(nameof(descriptionData.Text), bindingManager, nameof(BindingValue.Description)));

                // Security
                SetAuthorization(formBinding.GetAuthorization);
                newTemplate.Enabled = formBinding.GetAuthorization(CommandType.Add);
                newDataSource.Enabled = formBinding.GetAuthorization(CommandType.Add);
            }
        }

        private void NewTemplate_Click(object sender, EventArgs e)
        { Activate(() => new Template(null)); }

        private void NewDataSource_Click(object sender, EventArgs e)
        { Activate(() => new DataSource(null)); }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);

            if (formBinding.TryGetValue(out BindingValue? binding))
            { formBinding.Load(binding, onComplete); }

            void onComplete(RunWorkerCompletedEventArgs args)
            {
                SendMessage(new RefreshNavigation());
                RefreshButtons();
            }
        }


        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);

            if (formBinding.TryGetValue(out BindingValue? binding))
            { formBinding.Save(binding, onComplete); }

            void onComplete(RunWorkerCompletedEventArgs args)
            {
                SendMessage(new RefreshNavigation());
                RefreshButtons();
            }
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            if (formBinding.TryGetValue(out BindingValue? binding))
            {
                formBinding.Remove(binding);
                SendMessage(new RefreshNavigation());
                RefreshButtons();
            }

        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);

            if (formBinding.TryGetValue(out BindingValue? binding))
            { formBinding.Delete(binding, onComplete); }

            void onComplete(RunWorkerCompletedEventArgs args)
            {
                SendMessage(new RefreshNavigation());
                RefreshButtons();
            }
        }

        private void BindingManager_CurrentChanged(object sender, EventArgs e)
        { RefreshButtons(); }

        void RefreshButtons()
        {
            if (formBinding.TryGetValue(out BindingValue? current))
            {
                CommandButtons[CommandType.Delete].IsEnabled = current.InModel;

                CommandButtons[CommandType.OpenDatabase].IsEnabled = current.InDatabase && !current.InModel;
                CommandButtons[CommandType.SaveDatabase].IsEnabled = current.InModel;
                CommandButtons[CommandType.DeleteDatabase].IsEnabled = current.InDatabase;
            }
        }
    }
}
