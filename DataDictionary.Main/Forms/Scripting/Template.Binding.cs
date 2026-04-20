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
            BindingView<TemplateValue> templateValues =
                new BindingView<TemplateValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public FormBinding() : base()
            { }

            /// <summary>
            /// Try/Get current Value of the Templates
            /// </summary>
            /// <param name="result"></param>
            /// <returns></returns>
            public Boolean TryGetValue([NotNullWhen(true)] out TemplateValue? result)
            {
                if (TemplateBinding.Position >= 0
                    && TemplateBinding.Current is TemplateValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryAddValue(out TemplateValue result)
            {
                TemplateValue value = new TemplateValue();
                data.Add(value);
                result = value; return true;
            }

            public void Load(ITemplateIndex template)
            {
                TemplateIndex key = new TemplateIndex(template);
                TemplateBinding.RaiseListChangedEvents = false;
                templateValues.RaiseListChangedEvents = false;

                templateValues = new BindingView<TemplateValue>(data, w => key.Equals(w));

                if(templateValues.Count > 0)
                {
                    TemplateBinding.DataSource = templateValues;

                    TemplateBinding.RaiseListChangedEvents = true;

                    templateValues.RaiseListChangedEvents = true;
                }

                TemplateBinding.ResetBindings(false);
                TemplateBinding.MoveFirst();
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
