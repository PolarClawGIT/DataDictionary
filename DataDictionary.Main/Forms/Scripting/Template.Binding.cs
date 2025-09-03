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

            ITemplate data = BusinessData.ScriptingTemplate;

            public required BindingSource TemplateBinding { private get; init; }
            BindingView<TemplateValue> Templates =
                new BindingView<TemplateValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource DataSourceBinding { private get; init; }
            BindingView<TemplateInputValue> DataSources =
                new BindingView<TemplateInputValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public FormBinding() : base()
            { }

            public void Load(TemplateIndex template)
            {
                TemplateBinding.RaiseListChangedEvents = false;
                DataSourceBinding.RaiseListChangedEvents = false;

                Templates = new BindingView<TemplateValue>(data.Templates, w => template.Equals(w));
                DataSources = new BindingView<TemplateInputValue>(data.TemplateSources, w => template.Equals(w));

                TemplateBinding.DataSource = Templates;
                DataSourceBinding.DataSource = DataSources;

                TemplateBinding.RaiseListChangedEvents = true;
                DataSourceBinding.RaiseListChangedEvents = true;

                TemplateBinding.ResetBindings(false);
                DataSourceBinding.ResetBindings(false);

            }

            public void Load(TemplateIndex template, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();
                TemplateBinding.RaiseListChangedEvents = false;

                work.Add(factory.OpenConnection());
                work.Add(new WorkItem() { DoWork = () => { data = BusinessData.ScriptingTemplate; } });
                work.AddRange(data.Delete(template));
                work.AddRange(data.Load(factory, template));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Templates = new BindingView<TemplateValue>(data.Templates, w => template.Equals(w));
                    DataSources = new BindingView<TemplateInputValue>(data.TemplateSources, w => template.Equals(w));
                    TemplateBinding.DataSource = Templates;
                    DataSourceBinding.DataSource = DataSources;
                    TemplateBinding.RaiseListChangedEvents = true;
                    DataSourceBinding.RaiseListChangedEvents = true;
                    TemplateBinding.ResetBindings(false);
                    DataSourceBinding.ResetBindings(false);

                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public void Load(TemplateIndex template, TemporalIndex temporal, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();
                TemplateBinding.RaiseListChangedEvents = false;

                work.Add(factory.OpenConnection());
                work.Add(new WorkItem() { DoWork = () => { data = ITemplate.Create(); } });
                work.AddRange(data.Load(factory, template, temporal));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Templates = new BindingView<TemplateValue>(data.Templates, w => template.Equals(w));
                    DataSources = new BindingView<TemplateInputValue>(data.TemplateSources, w => template.Equals(w));
                    TemplateBinding.DataSource = Templates;
                    DataSourceBinding.DataSource = DataSources;
                    TemplateBinding.RaiseListChangedEvents = true;
                    DataSourceBinding.RaiseListChangedEvents = true;
                    TemplateBinding.ResetBindings(false);
                    DataSourceBinding.ResetBindings(false);

                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public TemplateValue NewValue()
            {
                TemplateValue result = new TemplateValue();
                data.Templates.Add(result);

                return result;
            }

            public TemplateInputValue NewDataSource()
            {
                if (TryGetValue(out TemplateValue? value))
                { return new TemplateInputValue(value); }
                else { throw new InvalidOperationException("Current TemplateValue not defined"); }
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
