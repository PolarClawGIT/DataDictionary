using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateManager : ApplicationData
    {
        FormBinding formBinding;

        public TemplateManager()
        {
            InitializeComponent();

            SetIcon(ScopeType.ScriptingTemplate);

            SetCommand(ScopeType.ScriptingTemplate,
                Enumerations.CommandType.Add,
                Enumerations.CommandType.Delete,
                Enumerations.CommandType.OpenDatabase,
                Enumerations.CommandType.SaveDatabase,
                Enumerations.CommandType.DeleteDatabase);

            formBinding = new FormBinding()
            {
                ManagerBinding = bindingTemplate,
                DoWork = base.DoWork,
                OnRefresh = () => { SendMessage(new RefreshNavigation()); }
            };
        }

        private void TemplateManager_Load(object sender, EventArgs e)
        {
            formBinding.Load(doBinding);

            void doBinding(RunWorkerCompletedEventArgs args)
            {
                templateNavigation.AutoGenerateColumns = false;
                templateNavigation.DataSource = bindingTemplate;

                templateTitleData.DataBindings.Add(new Binding(nameof(templateTitleData.Text), bindingTemplate, nameof(BindingValue.TemplateTitle)));
                templateDescriptionData.DataBindings.Add(new Binding(nameof(templateDescriptionData.Text), bindingTemplate, nameof(BindingValue.TemplateDescription)));
            }
        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);
            Activate(static () => new Forms.Scripting.Template());
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            if(formBinding.TryGetValue(out BindingValue? value))
            { formBinding.Remove(value); }
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);
        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);
        }

        protected override void HistoryCommand_Click(Object sender, EventArgs e)
        {
            base.HistoryCommand_Click(sender, e);
        }

        private void BindingTemplate_CurrentItemChanged(object sender, EventArgs e)
        {
            if (formBinding.TryGetValue(out BindingValue? binding))
            {
                CommandButtons[CommandType.Delete].IsEnabled = binding.InModel;

                CommandButtons[CommandType.OpenDatabase].IsEnabled = binding.InDatabase && !binding.InModel;
                CommandButtons[CommandType.SaveDatabase].IsEnabled = binding.InModel;
                CommandButtons[CommandType.DeleteDatabase].IsEnabled = binding.InDatabase;
            }
        }
    }
}
