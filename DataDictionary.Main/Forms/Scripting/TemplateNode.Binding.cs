using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using System.ComponentModel;
using System.Data;
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

            public Boolean TryGetValue([NotNullWhen(true)] out TemplateNodeValue? result)
            {
                if (TemplateNodeBinding.Position >= 0
                    && TemplateNodeBinding.Current is TemplateNodeValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public TemplateNodeValue NewValue(TemplateIndex template)
            {
                TemplateNodeValue newValue = new TemplateNodeValue(template);
                templateNodes.Add(newValue);
                SetPosition(newValue);
                return newValue;
            }

            public void RemoveValue()
            {
                if (TryGetValue(out TemplateNodeValue? value))
                { templateNodes.Remove(value); }
            }

            public Boolean GetAuthorization(Enumerations.CommandType command)
            {
                Boolean isGrant = false;
                Boolean isNode = TryGetValue(out _);
                SecurableIndex securable = templateIndex;
                isGrant = BusinessData.Authorization.IsScriptAdmin
                    || BusinessData.Authorization.IsScriptOwner
                    || BusinessData.Authorization.IsGrant(securable);

                switch (command)
                {
                    case Enumerations.CommandType.Default: return true;
                    case Enumerations.CommandType.Add: return isGrant;
                    case Enumerations.CommandType.Delete: return isGrant && isNode;
                    case Enumerations.CommandType.OpenDatabase: return isGrant && isNode;
                    case Enumerations.CommandType.SaveDatabase: return isGrant && isNode;
                    case Enumerations.CommandType.DeleteDatabase: return isGrant && isNode;
                    case Enumerations.CommandType.HistoryDatabase: return isGrant && isNode;
                    default: return false;
                }
            }

            public Boolean GetLocked()
            {
                if (TryGetValue(out TemplateNodeValue? value))
                {
                    return value.RowState() is DataRowState.Detached
                        or DataRowState.Deleted;
                }
                else return true;
            }
        }
    }
}
