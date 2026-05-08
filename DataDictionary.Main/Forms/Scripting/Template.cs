using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls.ComboBoxList;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;

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
                objectBinding: bindingObject,
                documentBinding: bindingDocument);

            SetRowState(
                bindingTemplate,
                bindingObject,
                bindingSchema,
                bindingTransform,
                bindingDocument);
            SetTitle(bindingTemplate);
            SetIcon(ScopeType.ScriptingTemplate);

            SetCommand(ScopeType.ScriptingTemplate,
                Enumerations.ButtonType.Delete,
                Enumerations.ButtonType.OpenDatabase,
                Enumerations.ButtonType.SaveDatabase,
                Enumerations.ButtonType.DeleteDatabase,
                Enumerations.ButtonType.HistoryDatabase);

            openObjectCommand.Image = ScopeType.ScriptingObject.GetImage(ButtonType.Open);
            openSchemaCommand.Image = ScopeType.ScriptingSchema.GetImage(ButtonType.Open);
            openTransformCommand.Image = ScopeType.ScriptingTransform.GetImage(ButtonType.Open);
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
            if (temporalIndex is null)
            {
                if (templateIndex.HasValue)
                { formBinding.Load(templateIndex); }
                else
                {
                    TemplateValue value = new TemplateValue();
                    formBinding.TemplateData.AddValue(value);
                    templateIndex = new TemplateIndex(value);
                    formBinding.Load(templateIndex);
                    SendMessage(new RefreshNavigation());
                }

                if (formBinding.TemplateData.TryGetValue(out TemplateValue? _))
                { DoBinding(); }
                else { IsLocked(true); }
            }
            //else
            //{ formBinding.Load(templateIndex, temporalIndex, onCompleting); }

            //void onCompleting(RunWorkerCompletedEventArgs args)
            //{
            //    if (args.Error is null)
            //    {
            //        if (formBinding.TemplateData.TryGetValue(out TemplateValue? _))
            //        { DoBinding(); }
            //        else { IsLocked(true); }
            //    }
            //}

            void DoBinding()
            {
                formBinding.TemplateData.AddBinding(templateTitleData, nameof(ITemplateValue.TemplateTitle));
                formBinding.TemplateData.AddBinding(templateDescriptionData, nameof(ITemplateValue.TemplateDescription));

                ScopeNameList.Load(objectScopeColumn);
                objectData.AutoGenerateColumns = false;
                objectData.DataSource = bindingObject;

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

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);
            throw new NotImplementedException();
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);
            throw new NotImplementedException();
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);
            throw new NotImplementedException();
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);
            throw new NotImplementedException();
        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);
            throw new NotImplementedException();
        }

        protected override void HistoryCommand_Click(Object sender, EventArgs e)
        {
            base.HistoryCommand_Click(sender, e);
            throw new NotImplementedException();
        }


        private void OpenObjectCommand_Click(object sender, EventArgs e)
        {
            Activate(() => new Forms.Scripting.TemplateObject(
                template: templateIndex,
                getData: formBinding.GetData));
        }

        private void AddSchemaCommand_Click(object sender, EventArgs e)
        {
            Activate(() => new Forms.Scripting.SchemaDefinition(
                template: templateIndex,
                schema: null,
                getData: formBinding.GetData));
        }

        private void OpenSchemaCommand_Click(object sender, EventArgs e)
        {
            if (formBinding.SchemaData.TryGetValue(out SchemaDefinitionValue? value))
            {
                Activate(() => new Forms.Scripting.SchemaDefinition(
                    template: templateIndex,
                    schema: value,
                    getData: formBinding.GetData));
            }
        }

        private void ExecuteSchemaCommand_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddTransformCommand_Click(object sender, EventArgs e)
        {
            Activate(() => new Forms.Scripting.Transform(
                template: templateIndex,
                transform: null,
                getData: formBinding.GetData));
        }

        private void OpenTransformCommand_Click(object sender, EventArgs e)
        {
            if (formBinding.TransformData.TryGetValue(out TransformValue? value))
            {
                Activate(() => new Forms.Scripting.Transform(
                    template: templateIndex,
                    transform: value,
                    getData: formBinding.GetData));
            }
        }

        private void ExecuteTransformCommand_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OpenDocumentCommand_Click(object sender, EventArgs e)
        {
            // TODO: Added data
            Activate(static () => new Forms.Scripting.Document());
        }
    }
}
