using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class Template
    {
        class FormBinding
        {
            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            public required BindingSource TemplateBinding { private get; init; }
            BindingView<TemplateValue> Templates =
                new BindingView<TemplateValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public ITemplateIndex? TemplateIndex
            {
                get { return templateIndex; }
                set
                {
                    if (value is ITemplateIndex)
                    {
                        templateIndex = new TemplateIndex(value);
                        TemplateBinding.RaiseListChangedEvents = false;
                        Templates = new BindingView<TemplateValue>(BusinessData.ScriptingTemplate.Templates, w => templateIndex.Equals(w));
                        TemplateBinding.DataSource = Templates;
                        TemplateBinding.RaiseListChangedEvents = true;
                        TemplateBinding.ResetBindings(false);
                    }
                    else { throw new ArgumentNullException(nameof(TemplateIndex)); }
                }
            }
            TemplateIndex templateIndex = new TemplateIndex();

            public ITemporalIndex? TemporalIndex
            {
                get { return temporalIndex; }
                set
                {
                    if (value is ITemporalIndex)
                    { temporalIndex = new TemporalIndex(value); }
                    else { temporalIndex = null; }
                }

            }
            TemporalIndex? temporalIndex = null;

            public FormBinding() : base()
            {
                Templates = new BindingView<TemplateValue>(BusinessData.ScriptingTemplate.Templates, w => templateIndex.Equals(w));
            }


            public void Load(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {

            }

            public TemplateValue NewValue()
            {
                TemplateValue result = new TemplateValue();
                Templates.Add(result);

                return result;
            }

            public Boolean TryGetValue([NotNullWhen(true)] out TemplateValue? result)
            {
                if (TemplateBinding.Position >= 0
                    && TemplateBinding.Current is TemplateValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }
        }

    }
}
