using DataDictionary.BusinessLayer.AppScripting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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

            public DocumentValue NewValue()
            {
                DocumentValue result = new DocumentValue();
                data.Add(result);

                return result;
            }
        }
    }
}
