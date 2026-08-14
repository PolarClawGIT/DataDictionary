using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls.ComboBoxList;
using DataDictionary.Main.Dialogs;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaDocument : ApplicationData
    {
        TemplateIndex templateIndex = new TemplateIndex();
        SchemaDefinitionIndex schemaIndex = new SchemaDefinitionIndex();
        DocumentIndex documentIndex = new DocumentIndex();
        //TemporalIndex? temporalIndex = null;
        FormBinding formBinding;

        public override Boolean IsOpenItem(object? item)
        { return item is IDocumentIndex key && documentIndex.Equals(key); }

        public SchemaDocument()
        {
            InitializeComponent();

            formBinding = new FormBinding(bindingTemplate, bindingSchema, bindingDocument);
            SetIcon(ScopeType.ScriptingDocument);

            SetCommand(ButtonType.Delete);
        }

        public SchemaDocument(IDocumentIndex document, Func<ITemplateData> getData) : this()
        { documentIndex = new DocumentIndex(document); }

        public SchemaDocument(ISchemaDefinitionIndex schema, Func<ITemplateData> getData) : this()
        { schemaIndex = new SchemaDefinitionIndex(schema); }


        private void SchemaDocument_Load(object sender, EventArgs e)
        {
            if (documentIndex.HasValue)
            {
                formBinding.LoadValue(documentIndex);
            }
            else if (schemaIndex.HasValue)
            {
                formBinding.LoadValue(schemaIndex, out documentIndex);
            }
            else
            {   // This should never occur.
                Exception ex = new InvalidOperationException("SchemaDefinition not found");
                ex.Data.Add(nameof(schemaIndex), schemaIndex);
                throw ex;
            }

            if (formBinding.SchemaData.TryGetValue(out SchemaDefinitionValue? _))
            { DoBinding(); }
            else { IsLocked(true); }

            void DoBinding()
            {
                formBinding.TemplateData.AddBinding(templateTitleData, e => e.TemplateTitle);
                formBinding.SchemaData.AddBinding(schemaTitleData, e => e.SchemaTitle);

                ScopeNameList.Load(objectScopeData);
                formBinding.DocumentData.AddBinding(objectScopeData, e => e.ObjectScope);
                formBinding.DocumentData.AddBinding(objectNameData, e => e.ObjectName);
                formBinding.DocumentData.AddBinding(documentFileData, e => e.FileName);
                //documentFileData  FileName
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

        private void ObjectNameData_SelectCommand(object sender, EventArgs e)
        {
            if (bindingDocument is not null
                && ParentForm is not null
                && formBinding.DocumentData.TryGetValue(out SchemaDocumentValue? value))
            {
                using (SelectionDialog dialog = new SelectionDialog(ParentForm))
                {
                    dialog.MultiSelect = false;
                    dialog.FilterScopes.AddRange(
                             ScopeType.ModelAttribute, ScopeType.ModelEntity, ScopeType.ModelProcess);
                    // TODO: Support for Alias, ScopeType.ModelAttributeAlias, ScopeType.ModelEntityAlias, ScopeType.ModelProcessAlias

                    dialog.BuildData(new List<PathIndex>() { new PathIndex(value.ObjectName) });

                    if (dialog.ShowDialog(this) is DialogResult.OK)
                    {
                        foreach (INamedScopeValue item in dialog.SelectedByNamedScope())
                        {
                            value.ObjectName = item.Path.MemberFullPath;
                            value.ObjectScope = item.Scope;
                        }
                    }
                }
            }
        }

        private void DocumentFileData_SelectCommand(object sender, EventArgs e)
        {

        }
    }
}
