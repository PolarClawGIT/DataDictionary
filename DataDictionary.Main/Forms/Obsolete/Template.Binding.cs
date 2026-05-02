//using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.Obsolete;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Obsolete
{
    partial class Template
    {
        class FormBinding
        {
            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            ITemplate data = BusinessData.Scripting;

            //TODO: C# 13 Init fields fixed to mimic other classes.
            //Currently, the ListChange Event is firing before the object is assigned to a property.

            public required BindingSource TemplateBinding { private get; init; }
            BindingView<TemplateValue> templates =
                new BindingView<TemplateValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource NodeBinding { private get; init; }
            BindingView<TemplateNodeValue> templateNodes =
                new BindingView<TemplateNodeValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource NodeOwnerBinding { private get; init; }
            BindingView<TemplateNodeOwnerValue> templateNodeOwners =
                new BindingView<TemplateNodeOwnerValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource DataSourceBinding { private get; init; }
            BindingView<TemplateInputValue> dataSources =
                new BindingView<TemplateInputValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public BusinessLayer.AppScripting.IXElementBuilderList XBuilder { get; } = BusinessData.Scripting.XBuilders;

            public FormBinding() : base()
            { }

            public void Load(TemplateIndex template)
            {
                TemplateBinding.RaiseListChangedEvents = false;
                NodeBinding.RaiseListChangedEvents = false;
                NodeOwnerBinding.RaiseListChangedEvents = false;
                DataSourceBinding.RaiseListChangedEvents = false;

                templates = new BindingView<TemplateValue>(data.Templates, w => template.Equals(w));
                templateNodes = new BindingView<TemplateNodeValue>(data.Nodes, w => template.Equals(w));
                templateNodeOwners = new BindingView<TemplateNodeOwnerValue>(data.NodeOwners, w => template.Equals(w));
                dataSources = new BindingView<TemplateInputValue>(data.TemplateSources, w => template.Equals(w));

                TemplateBinding.DataSource = templates;
                NodeBinding.DataSource = templateNodes;
                NodeOwnerBinding.DataSource = templateNodeOwners;
                DataSourceBinding.DataSource = dataSources;

                TemplateBinding.RaiseListChangedEvents = true;
                NodeBinding.RaiseListChangedEvents = true;
                NodeOwnerBinding.RaiseListChangedEvents = true;
                DataSourceBinding.RaiseListChangedEvents = true;

                TemplateBinding.ResetBindings(false);
                NodeBinding.ResetBindings(false);
                NodeOwnerBinding.ResetBindings(false);
                DataSourceBinding.ResetBindings(false);

                NodeBinding.CurrentChanged += NodeBinding_CurrentChanged;

                void NodeBinding_CurrentChanged(Object? sender, EventArgs e)
                {
                    NodeOwnerBinding.RaiseListChangedEvents = false;

                    if (TryGetValue(out TemplateNodeValue? currentNode))
                    {
                        TemplateNodeIndex nodeKey = new TemplateNodeIndex(currentNode);
                        templateNodeOwners = new BindingView<TemplateNodeOwnerValue>(data.NodeOwners, w => template.Equals(w) && nodeKey.Equals(w));
                        NodeOwnerBinding.DataSource = templateNodeOwners;
                    }

                    NodeOwnerBinding.RaiseListChangedEvents = true;
                    NodeOwnerBinding.ResetBindings(false);
                }
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

            public void Save(TemplateIndex template, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(data.Save(factory, template));
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

            public ITemporalData GetTemporal(TemplateIndex template)
            { return data.GetTemporal(template); }

            public TemplateValue NewValue()
            {
                TemplateValue result = new TemplateValue();
                data.Templates.Add(result);

                return result;
            }

            public TemplateNodeValue NewNodeValue(TemplateIndex template)
            {
                TemplateNodeValue newValue = new TemplateNodeValue(template);
                templateNodes.Add(newValue);

                return newValue;
            }

            public TemplateNodeOwnerValue NewNodeOwner(TemplateNodeIndex ownerNode)
            {
                if (TryGetValue(out TemplateNodeValue? value))
                {
                    TemplateNodeOwnerValue newItem = new TemplateNodeOwnerValue(value, ownerNode);
                    templateNodeOwners.Add(newItem);

                    return newItem;
                }
                else
                { throw new IndexOutOfRangeException(); }
            }

            public void RemoveValue()
            {
                if (TryGetValue(out TemplateValue? value))
                { data.Remove(new TemplateIndex(value)); }
            }

            public void RemoveNodeValue()
            {
                if (TryGetValue(out TemplateNodeValue? value))
                { data.Remove(new TemplateNodeIndex(value)); }
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

            public Boolean TryGetValue([NotNullWhen(true)] out TemplateNodeValue? result)
            {
                if (NodeBinding.Position >= 0
                    && NodeBinding.Current is TemplateNodeValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TrySetPosition(ITemplateNodeIndex node)
            {
                TemplateNodeIndex key = new TemplateNodeIndex(node);

                if (templateNodes.FirstOrDefault(w => key.Equals(w)) is TemplateNodeValue value)
                { NodeBinding.Position = templateNodes.IndexOf(value); return true; }
                else { return false; }
            }

            public Boolean GetAuthorization(Enumerations.ButtonType command)
            {
                Boolean isGrant = false;
                Boolean isNode = TryGetValue(out TemplateValue? _);

                SecurableIndex? templateKey = null;
                if (TryGetValue(out TemplateValue? templateValue))
                { templateKey = new TemplateIndex(templateValue); }

                isGrant = BusinessData.Authorization.IsScriptAdmin
                    || BusinessData.Authorization.IsScriptOwner
                    || BusinessData.Authorization.IsGrant(templateKey);

                switch (command)
                {
                    case Enumerations.ButtonType.Default: return true;
                    case Enumerations.ButtonType.Add: return isGrant;
                    case Enumerations.ButtonType.Delete: return isGrant && isNode;
                    case Enumerations.ButtonType.OpenDatabase: return isGrant && isNode;
                    case Enumerations.ButtonType.SaveDatabase: return isGrant && isNode;
                    case Enumerations.ButtonType.DeleteDatabase: return isGrant && isNode;
                    case Enumerations.ButtonType.HistoryDatabase: return isGrant && isNode;
                    default: return false;
                }
            }

            public Boolean GetLocked()
            {
                if (TryGetValue(out TemplateValue? value))
                {
                    return value.RowState() is DataRowState.Detached
                        or DataRowState.Deleted;
                }
                else return true;
            }

            public void BuildTree(TreeView tree)
            {
                if (TryGetValue(out TemplateValue? template))
                { tree.BuildTree(template, data.Nodes, data.NodeOwners); }
            }
        }

    }
}
