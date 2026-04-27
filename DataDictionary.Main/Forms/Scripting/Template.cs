using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls.ComboBoxList;
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

        public override Boolean IsOpenItem(object? item)
        { return item is ITemplateIndex key && templateIndex.Equals(key); }

        public Template() : base()
        {
            InitializeComponent();

            formBinding = new FormBinding()
            {
                DoWork = base.DoWork,
                TemplateBinding = bindingTemplate,
                ObjectBinding = bindingObject,
                SchemaBinding = bindingSchema,
                TransformBinding = bindingTransform,
                DocumentBinding = bindingDocument
            };

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
                    if (formBinding.TryAddValue(out TemplateValue? value))
                    {
                        templateIndex = new TemplateIndex(value);
                        formBinding.Load(templateIndex);
                        SendMessage(new RefreshNavigation());
                    }
                }

                if (formBinding.TryGetValue(out TemplateValue? _))
                { DoBinding(); }
                else { IsLocked(true); }
            }
            else
            { formBinding.Load(templateIndex, temporalIndex, onCompleting); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is null)
                {
                    if (formBinding.TryGetValue(out TemplateValue? _))
                    { DoBinding(); }
                    else { IsLocked(true); }
                }
            }

            void DoBinding()
            {
                templateTitleData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.TemplateTitle)));
                templateDescriptionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.TemplateDescription)));

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
                SetAuthorization(formBinding.GetAuthorization);
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
                getTemplates: formBinding.GetTemplates,
                getObjects: formBinding.GetObjects));
        }

        private void AddSchemaCommand_Click(object sender, EventArgs e)
        {
            Activate(() => new Forms.Scripting.SchemaDefinition(
                template: templateIndex,
                schema: null,
                getTemplates: formBinding.GetTemplates,
                getObjects: formBinding.GetObjects,
                getSchemata: formBinding.GetSchemata,
                getDocuments: formBinding.GetSchemaDocuments,
                getNodes: formBinding.GetSchemaNodes,
                getOwners: formBinding.GetSchemaNodeOwners,
                tryAddSchema: formBinding.TryAddValue));
        }

        private void OpenSchemaCommand_Click(object sender, EventArgs e)
        {
            if(formBinding.TryGetValue(out SchemaDefinitionValue? value))
            {
                Activate(() => new Forms.Scripting.SchemaDefinition(
                    template: templateIndex,
                    schema: value,
                    getTemplates: formBinding.GetTemplates,
                    getObjects: formBinding.GetObjects,
                    getSchemata: formBinding.GetSchemata,
                    getDocuments: formBinding.GetSchemaDocuments,
                    getNodes: formBinding.GetSchemaNodes,
                    getOwners: formBinding.GetSchemaNodeOwners,
                    tryAddSchema: formBinding.TryAddValue));
            }
        }

        private void ExecuteSchemaCommand_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddTransformCommand_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OpenTransformCommand_Click(object sender, EventArgs e)
        {
            // TODO: Added data
            Activate(static () => new Forms.Scripting.Transform());
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
