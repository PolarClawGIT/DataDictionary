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
                CommandType.Open);
            newTemplate.Image = ScopeType.ScriptingTemplate.GetNavigation().Images[CommandType.Add];
            newDataSource.Image = ScopeType.ScriptingData.GetNavigation().Images[CommandType.Add];

            AddCommands(templateCommands);

            formBinding = new FormBinding()
            {
                ManagerBinding = bindingManager,
                DoWork = base.DoWork,
                OnRefresh = () => { SendMessage(new RefreshNavigation()); }
            };
        }

        private void TemplateManager_Load(object sender, EventArgs e)
        {
            formBinding.Load(doBinding);

            void doBinding(RunWorkerCompletedEventArgs args)
            {
                managerData.AutoGenerateColumns = false;
                managerData.DataSource = bindingManager;

                titleData.DataBindings.Add(new Binding(nameof(titleData.Text), bindingManager, nameof(BindingValue.Title)));
                descriptionData.DataBindings.Add(new Binding(nameof(descriptionData.Text), bindingManager, nameof(BindingValue.Description)));
            }
        }

        private void NewTemplate_Click(object sender, EventArgs e)
        { Activate(() => new Template(null)); }

        private void NewDataSource_Click(object sender, EventArgs e)
        { Activate(() => new DataSource(null)); }

        protected override void OpenCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenCommand_Click(sender, e);

            if (formBinding.TryGetValue(out BindingValue? value))
            {
                if (value.TryGetIndex(out DataSourceIndex? dataSource))
                { Activate(() => new DataSource(dataSource)); }
                else if (value.TryGetIndex(out TemplateIndex? template))
                { Activate(() => new Template(template)); }
            }
        }
    }
}
