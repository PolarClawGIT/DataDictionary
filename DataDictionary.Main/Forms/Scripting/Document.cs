using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls.ComboBoxList;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class Document : ApplicationData, IApplicationDataForm
    {
        FormBinding formBinding;
        DocumentIndex documentIndex = new DocumentIndex();
        TemporalIndex? temporalIndex = null;

        public Boolean IsOpenItem(object? item)
        { return documentIndex.Equals(item); }

        public Document()
        {
            InitializeComponent();

            inputOpenCommand.Image = ScopeType.ScriptingDocument.GetImage(CommandType.Open);
            inputSaveCommand.Image = ScopeType.ScriptingDocument.GetImage(CommandType.Save);

            openTransformCommand.Image = ScopeType.ScriptingTemplate.GetImage(CommandType.Open);
            saveTransformCommand.Image = ScopeType.ScriptingTemplate.GetImage(CommandType.Save);
            getTransformCommand.Image = ScopeType.ScriptingTemplate.GetImage(CommandType.Import);

            saveResultCommand.Image = ScopeType.ApplicationDocument.GetImage(CommandType.Save);
            refreshResultCommand.Image = ScopeType.ApplicationDocument.GetImage(CommandType.Refresh);

            SetIcon(ScopeType.ScriptingDocument);

            formBinding = new FormBinding()
            {
                DocumentBinding = bindingDocument,
                DoWork = base.DoWork
            };

            SetIcon(ScopeType.ScriptingData);
            SetTitle(bindingDocument);
            SetRowState(bindingDocument);

            SetCommand(ScopeType.ScriptingDocument,
                CommandType.Delete,
                CommandType.OpenDatabase,
                CommandType.SaveDatabase,
                CommandType.DeleteDatabase,
                CommandType.HistoryDatabase);

        }

        public Document(IDocumentIndex? document) : this()
        {
            if (document is IDocumentIndex)
            { documentIndex = new DocumentIndex(document); }
            else { documentIndex = new DocumentIndex(formBinding.NewValue()); }
        }

        public Document(DocumentIndex document, ITemporalIndex temporal) : this(document)
        { temporalIndex = new TemporalIndex(); }

        private void Document_Load(object sender, EventArgs e)
        {
            if (temporalIndex is null)
            {
                formBinding.Load(documentIndex);
                DoBinding();
            }
            else
            { formBinding.Load(documentIndex, temporalIndex, onCompleting); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is null)
                {
                    DoBinding();
                    SendMessage(new RefreshNavigation());
                }
            }

            void DoBinding()
            {
                documentTitleData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, nameof(IDocumentValue.DocumentTitle)));
                
                DirectoryTypeList.Load(rootFolderData);
                rootFolderData.DataBindings.Add(new Binding(
                    nameof(ComboBox.SelectedValue),
                    bindingDocument, nameof(IDocumentValue.RootFolder),
                    true, DataSourceUpdateMode.OnValidation)
                { DataSourceNullValue = DirectoryType.Null });



            }
        }
    }
}
