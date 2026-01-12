using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class Document
    {
        class FormBinding
        {
            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            IDocument data = BusinessData.Scripting.Documents;

            public required BindingSource DocumentBinding { private get; init; }
            BindingView<DocumentValue> documents =
                new BindingView<DocumentValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public DocumentValue NewValue()
            {
                DocumentValue result = new DocumentValue();
                data.Add(result);

                return result;
            }

            public void Load(DocumentIndex document)
            {
                DocumentBinding.RaiseListChangedEvents = false;

                documents = new BindingView<DocumentValue>(data, w => document.Equals(w));
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

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(document);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

        }
    }
}
