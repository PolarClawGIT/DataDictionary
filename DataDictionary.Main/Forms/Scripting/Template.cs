using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class Template : ApplicationData
    {
        TemplateIndex templateIndex = new TemplateIndex();
        TemporalIndex? temporalIndex = null;
        FormBinding formBinding;
        Boolean isNew = false;

        public override Boolean IsOpenItem(object? item)
        { return item is ITemplateIndex key && templateIndex.Equals(key); }

        public Template() : base()
        {
            InitializeComponent();

            formBinding = new FormBinding()
            {
                DoWork = base.DoWork,
                TemplateBinding = bindingTemplate
            };

            SetTitle(bindingTemplate);
            SetRowState(
                bindingTemplate);
            //SetIcon(ScopeType.ScriptingTemplate);

            SetCommand(ScopeType.ScriptingTemplate,
                Enumerations.ButtonType.Delete,
                Enumerations.ButtonType.OpenDatabase,
                Enumerations.ButtonType.SaveDatabase,
                Enumerations.ButtonType.DeleteDatabase,
                Enumerations.ButtonType.HistoryDatabase);

            openObjectCommand.Image = ScopeType.ScriptingObject.GetImage(ButtonType.Open);
            openSchemaCommand.Image = ScopeType.ScriptingSchema.GetImage(ButtonType.Open);
            openTransformCommand.Image = ScopeType.ScriptingTransform.GetImage(ButtonType.Open);
            addObjectCommand.Image = ScopeType.ScriptingObject.GetImage(ButtonType.Add);
            addSchemaCommand.Image = ScopeType.ScriptingSchema.GetImage(ButtonType.Add);
            addTransformCommand.Image = ScopeType.ScriptingTransform.GetImage(ButtonType.Add);

            executeSchemaCommand.Image = ScopeType.ScriptingSchema.GetImage(ButtonType.Export);
            executeTransformCommand.Image = ScopeType.ScriptingTransform.GetImage(ButtonType.Export);

            openDocumentCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Open);
        }

        public Template(ITemplateIndex? template) : this()
        {
            if (template is ITemplateIndex key)
            { templateIndex = new TemplateIndex(key); }
        }

        public Template(ITemplateIndex template, ITemporalIndex temporal) : this(template)
        { temporalIndex = new TemporalIndex(); }

        private void Template_Load(object sender, EventArgs e)
        {
            //TODO: The Message RefreshNavigation is not occurring as expected across the application.
            // This cannot be called in the New because the event has not been hooked up.
            // Order of Events:
            // - New is called
            // - Add MDI child called, after New is completed.
            // - Messages hooked up
            // Idea- the Form Load event will need to handle the Message.
            // To do that, it needs to detect when the New row needs to be added.
            // So that formBinding.AddValue(out TemplateValue value) can be called.
            // This will need to be repeated in ALL forms.
            // The new Index attribute HasValue can do this.

            if (temporalIndex is null)
            {
                if (templateIndex.HasValue)
                { formBinding.Load(templateIndex); }
                else
                {
                    if (formBinding.TryAddValue(out TemplateValue value))
                    {
                        templateIndex = new TemplateIndex(value);
                        formBinding.Load(templateIndex);
                        SendMessage(new RefreshNavigation());
                    }
                }

                if (formBinding.TryGetValue(out TemplateValue? _))
                { DoBinding(); }
            }
            else
            { formBinding.Load(templateIndex, temporalIndex, onCompleting); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is null)
                {
                    if (bindingTemplate.Count > 0)
                    { DoBinding(); }
                }
            }

            void DoBinding()
            {
                templateTitleData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.TemplateTitle)));
                templateDescriptionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.TemplateDescription)));

            }
        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);
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

        private void AddObjectCommand_Click(object sender, EventArgs e)
        {

        }

        private void OpenObjectCommand_Click(object sender, EventArgs e)
        {
            // TODO: Added data
            Activate(static () => new Forms.Scripting.TemplateObject());
        }

        private void AddSchemaCommand_Click(object sender, EventArgs e)
        {

        }

        private void OpenSchemaCommand_Click(object sender, EventArgs e)
        {
            // TODO: Added data
            Activate(static () => new Forms.Scripting.SchemaDefinition());
        }

        private void ExecuteSchemaCommand_Click(object sender, EventArgs e)
        {

        }

        private void AddTransformCommand_Click(object sender, EventArgs e)
        {

        }

        private void OpenTransformCommand_Click(object sender, EventArgs e)
        {
            // TODO: Added data
            Activate(static () => new Forms.Scripting.Transform());
        }

        private void ExecuteTransformCommand_Click(object sender, EventArgs e)
        {

        }

        private void OpenDocumentCommand_Click(object sender, EventArgs e)
        {
            // TODO: Added data
            Activate(static () => new Forms.Scripting.Document());
        }
    }
}
