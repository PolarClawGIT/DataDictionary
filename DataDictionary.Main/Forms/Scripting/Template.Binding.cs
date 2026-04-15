using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class Template
    {
        class FormBinding
        {
            ITemplateData data = BusinessData.Templates;

            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            public required BindingSource TemplateBinding { private get; init; }
            BindingView<TemplateValue> templates =
                new BindingView<TemplateValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public FormBinding() : base()
            { }

            public Boolean TryGetValue(ITemplateIndex template, [NotNullWhen(true)] out TemplateValue? result)
            {
                TemplateIndex key = new TemplateIndex(template);
                if (templates.FirstOrDefault(w => key.Equals(w)) is TemplateValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public void AddValue(out TemplateValue result)
            {
                TemplateValue value = new TemplateValue();
                templates.Add(value);
                result = value;
            }

            public void Load(ITemplateIndex template)
            {
                TemplateIndex key = new TemplateIndex(template);
                TemplateBinding.RaiseListChangedEvents = false;

                templates = new BindingView<TemplateValue>(data, w => key.Equals(w));

                TemplateBinding.RaiseListChangedEvents = true;

                TemplateBinding.ResetBindings(false);
            }

            public void Load(ITemplateIndex template, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.Add(new WorkItem() { DoWork = () => { data = BusinessData.Templates; } });
                work.AddRange(data.Delete(template));
                work.AddRange(data.Load(factory, template));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(template);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public void Load(ITemplateIndex template, TemporalIndex temporal, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.Add(new WorkItem() { DoWork = () => { data = ITemplateData.Create(); } });
                work.AddRange(data.Load(factory, template, temporal));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(template);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public void Save(TemplateIndex template, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                DoWork(work, completing);

                work.Add(factory.OpenConnection());
                work.AddRange(data.Save(factory, template));
                work.Add(new WorkItem() { DoWork = () => { data = BusinessData.Templates; } });
                work.AddRange(data.Delete(template));
                work.AddRange(data.Load(factory, template));

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(template);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public ITemporalData GetTemporal(TemplateIndex template)
            { return data.GetTemporal(template); }

            public Boolean GetAuthorization(Enumerations.ButtonType command)
            {
                return false;
            }

            public Boolean GetLocked()
            {
                return true;
            }
        }
    }
}
