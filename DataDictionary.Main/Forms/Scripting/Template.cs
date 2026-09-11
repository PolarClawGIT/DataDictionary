using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls.ComboBoxList;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class Template : ApplicationData
    {
        TemplateIndex templateIndex = new TemplateIndex();
        TemporalIndex? temporalIndex = null;
        FormBinding formBinding;

        public override Boolean IsOpenItem(object? item)
        { return item is ITemplateIndex key && templateIndex.Equals(key); }

        public Template() : base()
        {
            InitializeComponent();

            formBinding = new FormBinding(
                templateBinding: bindingTemplate,
                schemaBinding: bindingSchema,
                transformBinding: bindingTransform,
                documentBinding: bindingDocument)
            { DoWork = base.DoWork };

            SetRowState(
                bindingTemplate,
                bindingObject,
                bindingSchema,
                bindingTransform,
                bindingDocument);
            SetTitle(bindingTemplate);
            SetIcon(bindingTemplate);

            SetCommand(
                ButtonType.Delete,
                ButtonType.OpenDatabase,
                ButtonType.SaveDatabase,
                ButtonType.DeleteDatabase);

            schemaOpenCommand.Image = ScopeType.ScriptingSchema.GetImage(ButtonType.Open);
            schemaNewCommand.Image = ScopeType.ScriptingSchema.GetImage(ButtonType.Add);
            schemaDeleteCommand.Image = ScopeType.ScriptingSchema.GetImage(ButtonType.Delete);
            schemaBuildCommand.Image = ScopeType.ScriptingSchema.GetImage(ButtonType.Export);

            transformOpenCommand.Image = ScopeType.ScriptingTransform.GetImage(ButtonType.Open);
            transformNewCommand.Image = ScopeType.ScriptingTransform.GetImage(ButtonType.Add);
            transformBuildCommand.Image = ScopeType.ScriptingTransform.GetImage(ButtonType.Export);

            documentOpenCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Open);
        }

        public Template(ITemplateIndex? template) : this()
        {
            if (template is ITemplateIndex key)
            { templateIndex = new TemplateIndex(key); }
        }

        public Template(ITemplateIndex template, ITemporalIndex temporal) : this(template)
        { temporalIndex = new TemporalIndex(temporal); }

        private void Template_Load(object sender, EventArgs e)
        {
            if (temporalIndex is null)
            {
                if (templateIndex.HasValue)
                { formBinding.LoadValue(templateIndex); }
                else
                {
                    TemplateValue value = new TemplateValue();
                    formBinding.TemplateData.Add(value);
                    templateIndex = new TemplateIndex(value);
                    formBinding.LoadValue(templateIndex);
                    SendMessage(new RefreshNavigation());
                }

                if (formBinding.TemplateData.TryGetCurrent(out TemplateValue? _))
                { DoBinding(); }
                else { IsLocked(true); }
            }
            else
            { formBinding.LoadData(templateIndex, temporalIndex, onCompleting); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is null)
                {
                    formBinding.LoadValue(templateIndex);
                    CommandButtons[ButtonType.Delete].Enabled = false;
                    CommandButtons[ButtonType.DeleteDatabase].Enabled = false;

                    if (formBinding.TemplateData.TryGetCurrent(out TemplateValue? _))
                    { DoBinding(); }
                    else { IsLocked(true); }
                }
            }

            void DoBinding()
            {
                formBinding.TemplateData.AddBinding(templateTitleData, e => e.TemplateTitle);
                formBinding.TemplateData.AddBinding(templateDescriptionData, e => e.TemplateDescription);

                schemaData.AutoGenerateColumns = false;
                schemaData.DataSource = bindingSchema;

                transformsData.AutoGenerateColumns = false;
                transformsData.DataSource = bindingTransform;

                documentData.AutoGenerateColumns = false;
                documentData.DataSource = bindingTransform;

                // Security
                IsLocked(formBinding.GetLocked());
                SetAuthorization(formBinding.Authorize);
            }
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            formBinding.RemoveValue();
            IsLocked(true);
            SendMessage(new RefreshRow<TemplateIndex>(templateIndex));
            SendMessage(new RefreshNavigation());
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);

            if (temporalIndex is null)
            { formBinding.LoadData(templateIndex, complete); }
            else { formBinding.LoadData(templateIndex, temporalIndex, complete); }

            void complete(RunWorkerCompletedEventArgs args)
            {
                IsLocked(formBinding.GetLocked());
                SendMessage(new RefreshRow<TemplateIndex>(templateIndex));
                SendMessage(new RefreshNavigation());
            }
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);

            formBinding.SaveData(templateIndex, complete);

            void complete(RunWorkerCompletedEventArgs args)
            {
                IsLocked(formBinding.GetLocked());
                SendMessage(new RefreshRow<TemplateIndex>(templateIndex));
            }
        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);

            formBinding.DeleteData(templateIndex, complete);

            void complete(RunWorkerCompletedEventArgs args)
            { IsLocked(formBinding.GetLocked()); }
        }

        private void SchemaAddCommand_Click(object sender, EventArgs e)
        {
            Activate(() => new Forms.Scripting.SchemaDefinition(
                template: templateIndex,
                schema: null,
                getData: formBinding.GetData));
        }

        private void SchemaOpenCommand_Click(object sender, EventArgs e)
        {
            if (formBinding.SchemaData.TryGetCurrent(out SchemaDefinitionValue? value))
            {
                Activate(() => new Forms.Scripting.SchemaDefinition(
                    template: templateIndex,
                    schema: value,
                    getData: formBinding.GetData));
            }
        }

        private void SchemaDeleteCommand_Click(object sender, EventArgs e)
        {
            if (formBinding.SchemaData.TryGetCurrent(out SchemaDefinitionValue? value))
            { formBinding.Remove(value); }           
        }

        private void SchemaBuildCommand_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TransformNewCommand_Click(object sender, EventArgs e)
        {
            Activate(() => new Forms.Scripting.Transform(
                template: templateIndex,
                transform: null,
                getData: formBinding.GetData));
        }

        private void TransformOpenCommand_Click(object sender, EventArgs e)
        {
            if (formBinding.TransformData.TryGetCurrent(out TransformValue? value))
            {
                Activate(() => new Forms.Scripting.Transform(
                    template: templateIndex,
                    transform: value,
                    getData: formBinding.GetData));
            }
        }

        private void TransformBuildCommand_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DocumentOpenCommand_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


    }
}
