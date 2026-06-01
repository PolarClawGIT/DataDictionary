using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class ScriptingManager : ApplicationData
    {
        FormBinding formBinding;

        public ScriptingManager()
        {
            InitializeComponent();

            SetIcon(ScopeType.Scripting);

            SetTitle("Scripting Manager");

            SetCommand(
                ButtonType.Add,
                ButtonType.Open,
                ButtonType.Delete,
                ButtonType.OpenDatabase,
                ButtonType.SaveDatabase,
                ButtonType.DeleteDatabase,
                ButtonType.HistoryDatabase);

            formBinding = new FormBinding(bindingTemplate)
            { DoWork = base.DoWork, };
        }

        private void TemplateManager_Load(object sender, EventArgs e)
        {
            formBinding.LoadData(doBinding);

            void doBinding(RunWorkerCompletedEventArgs args)
            {
                templateNavigation.AutoGenerateColumns = false;
                templateNavigation.DataSource = bindingTemplate;

                templateTitleData.DataBindings.Add(new Binding(nameof(templateTitleData.Text), bindingTemplate, nameof(BindingValue.TemplateTitle)));
                templateDescriptionData.DataBindings.Add(new Binding(nameof(templateDescriptionData.Text), bindingTemplate, nameof(BindingValue.TemplateDescription)));

                // Security
                SetButtons();
            }
        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);
            Activate(() => new Forms.Scripting.Template(null), (f) => f.IsOpenItem(null));
        }

        protected override void OpenCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenCommand_Click(sender, e);
            if (formBinding.TryGetValue(out BindingValue? value)) 
            {
                TemplateIndex key = new TemplateIndex(value);
                Activate(() => new Forms.Scripting.Template(key), (f) => f.IsOpenItem(key));
            }

        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            formBinding.RemoveValue();
            SendMessage(new RefreshNavigation());
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);

            if (formBinding.TryGetValue(out BindingValue? value))
            { formBinding.LoadData(new TemplateIndex(value), onComplete); }

            void onComplete(RunWorkerCompletedEventArgs args)
            {
                SetButtons();
                SendMessage(new RefreshNavigation());
            }
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);

            if (formBinding.TryGetValue(out BindingValue? value))
            { formBinding.SaveData(new TemplateIndex(value), onComplete); }

            void onComplete(RunWorkerCompletedEventArgs args)
            {
                SetButtons();
                SendMessage(new RefreshNavigation());
            }
        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);

            if (formBinding.TryGetValue(out BindingValue? value))
            { formBinding.DeleteData(new TemplateIndex(value), onComplete); }

            void onComplete(RunWorkerCompletedEventArgs args)
            {
                SetButtons();
                SendMessage(new RefreshNavigation());
            }
        }

        protected override void HistoryCommand_Click(Object sender, EventArgs e)
        {
            base.HistoryCommand_Click(sender, e);

            Activate(() => new ApplicationWide.HistoryView(formBinding.GetTemporal())
            {
                OpenForm = (temporal) =>
                {
                    if (temporal.TryGetValue(out TemplateValue? template))
                    { return new Template(template, new TemporalIndex(temporal)); }
                    else { throw new InvalidOperationException("Could not convert TemporalValue back to HelpSubjectValue"); }
                }
            });
        }

        private void BindingTemplate_CurrentChanged(object sender, EventArgs e)
        { SetButtons(); }

        void SetButtons()
        {
            if (formBinding.TryGetValue(out BindingValue? binding))
            {
                CommandButtons[ButtonType.Delete].IsEnabled = binding.InModel;
                CommandButtons[ButtonType.Open].IsEnabled = binding.InModel;

                CommandButtons[ButtonType.OpenDatabase].IsEnabled = binding.InDatabase && !binding.InModel;
                CommandButtons[ButtonType.SaveDatabase].IsEnabled = binding.InModel;
                CommandButtons[ButtonType.DeleteDatabase].IsEnabled = binding.InDatabase;
            }
            else
            {
                CommandButtons[ButtonType.Delete].IsEnabled = false;
                CommandButtons[ButtonType.Open].IsEnabled = false;

                CommandButtons[ButtonType.OpenDatabase].IsEnabled = false;
                CommandButtons[ButtonType.SaveDatabase].IsEnabled = false;
                CommandButtons[ButtonType.DeleteDatabase].IsEnabled = false;
            }

            SetAuthorization(formBinding.Authorize);
        }
    }
}
