using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.DbWorkItem;
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

            ITemplate templateData = BusinessData.ScriptingTemplate;

            public required BindingSource TemplateBinding { private get; init; }
            BindingView<TemplateValue> Templates =
                new BindingView<TemplateValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public FormBinding() : base()
            { }

            public void Load(TemplateIndex template)
            {
                TemplateBinding.RaiseListChangedEvents = false;
                Templates = new BindingView<TemplateValue>(templateData.Templates, w => template.Equals(w));
                TemplateBinding.DataSource = Templates;
                TemplateBinding.RaiseListChangedEvents = true;
                TemplateBinding.ResetBindings(false);
            }

            public void Load(TemplateIndex template, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();
                TemplateBinding.RaiseListChangedEvents = false;

                work.Add(factory.OpenConnection());
                work.AddRange(templateData.Delete(template));
                work.AddRange(templateData.Load(factory, template));

                DoWork(work,completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Templates = new BindingView<TemplateValue>(templateData.Templates, w => template.Equals(w));
                    TemplateBinding.DataSource = Templates;
                    TemplateBinding.RaiseListChangedEvents = true;
                    TemplateBinding.ResetBindings(false);

                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public void Load(TemplateIndex template, TemporalIndex temporal, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();
                TemplateBinding.RaiseListChangedEvents = false;

                work.Add(factory.OpenConnection());
                work.Add(new WorkItem() { DoWork = () => { templateData = ITemplate.Create(); } });
                work.AddRange(templateData.Load(factory, template, temporal));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Templates = new BindingView<TemplateValue>(templateData.Templates, w => template.Equals(w));
                    TemplateBinding.DataSource = Templates;
                    TemplateBinding.RaiseListChangedEvents = true;
                    TemplateBinding.ResetBindings(false);

                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public TemplateValue NewValue()
            {
                TemplateValue result = new TemplateValue();
                templateData.Templates.Add(result);

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
