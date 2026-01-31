using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class Document
    {
        class FormBinding : IDocumentValue
        {
            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            IDocument data = BusinessData.Scripting.Documents;
            DocumentValue value = new DocumentValue();

            BindingView<DocumentValue> documents =
                new BindingView<DocumentValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource DocumentBinding { private get; init; }

            #region IDocumentValue
            /// <inheritdoc/>
            public String RootPath => value.RootPath;

            /// <inheritdoc/>
            public DocumentFile InputValue => value.InputValue;

            /// <inheritdoc/>
            public DocumentFile TransformValue => value.TransformValue;

            /// <inheritdoc/>
            public DocumentFile OutputValue => value.OutputValue;

            /// <inheritdoc/>
            public String? TransformScript => value.TransformScript;

            /// <inheritdoc/>
            public DirectoryType RootFolder => value.RootFolder;

            /// <inheritdoc/>
            public String? InputDirectory => value.InputDirectory;

            /// <inheritdoc/>
            public String? InputFile => value.InputFile;

            /// <inheritdoc/>
            public String? OutputDirectory => value.OutputDirectory;

            /// <inheritdoc/>
            public String? OutputFile => value.OutputFile;

            /// <inheritdoc/>
            public String? DocumentTitle => value.DocumentTitle;

            /// <inheritdoc/>
            public Guid? DocumentId => value.DocumentId;

            /// <inheritdoc/>
            public Guid? TemplateId => value.TemplateId;

            /// <inheritdoc/>
            public DataLayer.ITemporal Temporal => value.Temporal;

            /// <inheritdoc/>
            public DataIndex Index => ((IDocumentValue)value).Index;

            /// <inheritdoc/>
            public String Title => ((IDocumentValue)value).Title;

            /// <inheritdoc/>
            public ScopeType Scope => value.Scope;

            /// <inheritdoc/>
            public event PropertyChangedEventHandler? PropertyChanged
            {
                add { ((INotifyPropertyChanged)this.value).PropertyChanged += value; }
                remove { ((INotifyPropertyChanged)this.value).PropertyChanged -= value; }
            }
            #endregion

            public DocumentValue NewValue()
            {
                DocumentValue result = new DocumentValue();
                data.Add(result);

                value = result;
                return result;
            }

            public void Load(DocumentIndex document)
            {
                DocumentBinding.RaiseListChangedEvents = false;

                documents = new BindingView<DocumentValue>(data, w => document.Equals(w));
                if (documents.FirstOrDefault(w => document.Equals(w)) is DocumentValue documentValue)
                { value = documentValue; }

                DocumentBinding.DataSource = documents;
                DocumentBinding.RaiseListChangedEvents = true;
                DocumentBinding.ResetBindings(false);
            }

            public void Load(DocumentIndex document, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.Add(new WorkItem() { DoWork = () => { data = BusinessData.Scripting.Documents; } });
                work.AddRange(data.Delete(document));
                work.AddRange(data.Load(factory, document));
                work.AddRange(data.OpenFiles(document));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(document);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public void Load(DocumentIndex document, TemporalIndex temporal, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.Add(new WorkItem() { DoWork = () => { data = IDocument.Create(); } });
                work.AddRange(data.Load(factory, document, temporal));
                work.AddRange(data.OpenFiles(document));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(document);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public void OpenFiles(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                List<WorkItem> work = new List<WorkItem>();
                work.AddRange(data.OpenFiles(value));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                { if (onComplete is not null) { onComplete(args); } }
            }

            public void SaveFiles(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                List<WorkItem> work = new List<WorkItem>();
                work.AddRange(data.SaveFiles(value));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                { if (onComplete is not null) { onComplete(args); } }
                
            }

            public void Save(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(data.Save(factory, value));
                work.AddRange(data.SaveFiles(value));
                work.Add(new WorkItem() { DoWork = () => { data = BusinessData.Scripting.Documents; } });
                work.AddRange(data.Delete(value));
                work.AddRange(data.Load(factory, value));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(new DocumentIndex(value));
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public Boolean TryTransform([NotNullWhen(false)] out Exception? exception)
            { return value.TryTransform(out exception); }


            public void Remove()
            { data.Remove(value); }

            public ITemporalData GetTemporal()
            { return data.GetTemporal(value); }

            public Boolean TryTransform(DocumentIndex document, [NotNullWhen(false)] out Exception? exception)
            {
                if (value.TryTransform(out Exception? transformException))
                { exception = null; return true; }
                else { exception = transformException; return false; }
            }


            public Boolean GetAuthorization(DocumentIndex document, Enumerations.CommandType command)
            {
                Boolean isGrant = false;

                SecurableIndex? documentKey = null;
                SecurableIndex? templateKey = null;

                documentKey = new DocumentIndex(value);
                templateKey = new TemplateIndex(value);

                isGrant = BusinessData.Authorization.IsScriptAdmin
                    || BusinessData.Authorization.IsScriptOwner
                    || BusinessData.Authorization.IsGrant(documentKey)
                    || BusinessData.Authorization.IsGrant(templateKey);

                switch (command)
                {
                    case Enumerations.CommandType.Default: return true;
                    case Enumerations.CommandType.Add: return isGrant;
                    case Enumerations.CommandType.Delete: return isGrant;
                    case Enumerations.CommandType.OpenDatabase: return isGrant;
                    case Enumerations.CommandType.SaveDatabase: return isGrant;
                    case Enumerations.CommandType.DeleteDatabase: return isGrant;
                    case Enumerations.CommandType.HistoryDatabase: return isGrant;
                    default: return false;
                }
            }

            public Boolean GetLocked()
            {
                return value.RowState() is DataRowState.Detached
                                        or DataRowState.Deleted;
            }


        }

        public void Open(FileDialog dialog, DocumentFile file, Action<RunWorkerCompletedEventArgs> onComplete)
        {
            dialog.InitialDirectory = file.FilePath;
            dialog.FileName = file.FileName;

            if (dialog.ShowDialog() is DialogResult.OK)
            {
                file.FilePath = Path.GetDirectoryName(dialog.FileName) ?? String.Empty;
                file.FileName = Path.GetFileName(dialog.FileName);

                DoWork(file.Open(), onCompleted);
            }

            void onCompleted(RunWorkerCompletedEventArgs args)
            { onComplete(args); }
        }

        public void Save(FileDialog dialog, DocumentFile file, Action<RunWorkerCompletedEventArgs> onComplete)
        {
            dialog.InitialDirectory = file.FilePath;
            dialog.FileName = file.FileName;

            if (dialog.ShowDialog() is DialogResult.OK)
            {
                file.FilePath = Path.GetDirectoryName(dialog.FileName) ?? String.Empty;
                file.FileName = Path.GetFileName(dialog.FileName);

                DoWork(file.Save(), onCompleted);
            }

            void onCompleted(RunWorkerCompletedEventArgs args)
            { onComplete(args); }
        }



    }
}
