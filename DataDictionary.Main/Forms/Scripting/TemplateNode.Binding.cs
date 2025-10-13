using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateNode
    {
        class FormBinding
        {
            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }
            //public required Action OnRefresh { get; init; }

            public required BindingSource TemplateBinding { private get; init; }
            BindingView<TemplateValue> templates =
                new BindingView<TemplateValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource TemplateNodeBinding { private get; init; }
            BindingView<TemplateNodeValue> templateNodes =
                new BindingView<TemplateNodeValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            ITemplate data = BusinessData.Scripting;

            public FormBinding()
            { }

            public void Load(TemplateIndex template)
            {
                TemplateBinding.RaiseListChangedEvents = false;
                TemplateNodeBinding.RaiseListChangedEvents = false;

                templates = new BindingView<TemplateValue>(data.Templates, w => template.Equals(w));
                templateNodes = new BindingView<TemplateNodeValue>(data.Nodes, w => template.Equals(w));

                TemplateBinding.DataSource = templates;
                TemplateNodeBinding.DataSource = templateNodes;

                TemplateBinding.RaiseListChangedEvents = true;
                TemplateNodeBinding.RaiseListChangedEvents = true;
                TemplateBinding.ResetBindings(false);
                TemplateNodeBinding.ResetBindings(false);
            }

            public void Load(TemplateIndex template, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.Add(new WorkItem() { DoWork = () => { data = BusinessData.Scripting; } });
                work.AddRange(data.Delete(template));
                work.AddRange(data.Load(factory, template));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(template);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public void Load(TemplateIndex template, TemporalIndex temporal, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.Add(new WorkItem() { DoWork = () => { data = ITemplate.Create(); } });
                work.AddRange(data.Load(factory, template, temporal));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(template);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public Boolean SetPosition(ITemplateNodeIndex node)
            {
                TemplateNodeIndex key = new TemplateNodeIndex(node);

                if (templateNodes.FirstOrDefault(w => key.Equals(w)) is TemplateNodeValue value)
                { TemplateNodeBinding.Position = templateNodes.IndexOf(value); return true; }
                else { return false; }
            }

            public Boolean TryGetValue([NotNullWhen(true)] out ITemplateNodeValue? result)
            {
                if (TemplateNodeBinding.Position >= 0
                    && TemplateNodeBinding.Current is ITemplateNodeValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

        }
    }
}
