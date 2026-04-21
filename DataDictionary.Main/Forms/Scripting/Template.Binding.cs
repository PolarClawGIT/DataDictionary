using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

            public required BindingSource ObjectBinding { private get; init; }
            BindingView<TemplateObjectValue> objectValues =
                new BindingView<TemplateObjectValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource SchemaBinding { private get; init; }
            BindingView<SchemaDefinitionValue> schemaValues =
                new BindingView<SchemaDefinitionValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource TransformBinding { private get; init; }
            BindingView<TransformValue> transformValues =
                new BindingView<TransformValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource DocumentBinding { private get; init; }
            BindingList<DocumentValue> documentValues = new BindingList<DocumentValue>();


            public FormBinding() : base()
            { }

            public Boolean TryGetValue([NotNullWhen(true)] out TemplateValue? result)
            {
                if (TemplateBinding.Position >= 0
                    && TemplateBinding.Current is TemplateValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryAddValue([NotNullWhen(true)] out TemplateValue? result)
            {
                if (BusinessData.Authorization.IsScriptAdmin
                    || BusinessData.Authorization.IsScriptOwner)
                {
                    TemplateValue value = new TemplateValue();
                    data.Add(value);
                    result = value; return true;
                }
                else { result = null;  return false; }
            }

            public void Load(ITemplateIndex template)
            {
                TemplateIndex key = new TemplateIndex(template);
                TemplateBinding.RaiseListChangedEvents = false;
                ObjectBinding.RaiseListChangedEvents = false;
                SchemaBinding.RaiseListChangedEvents = false;
                TransformBinding.RaiseListChangedEvents = false;
                DocumentBinding.RaiseListChangedEvents = false;

                templateValues.RaiseListChangedEvents = false;
                objectValues.RaiseListChangedEvents = false;
                schemaValues.RaiseListChangedEvents = false;
                transformValues.RaiseListChangedEvents = false;
                documentValues.RaiseListChangedEvents = false;

                templateValues = new BindingView<TemplateValue>(data, w => key.Equals(w));
                objectValues = new BindingView<TemplateObjectValue>(data.Objects, w => key.Equals(w));
                schemaValues = new BindingView<SchemaDefinitionValue>(data.Schemata, w => key.Equals(w));
                transformValues = new BindingView<TransformValue>(data.Transforms, w => key.Equals(w));

                // TODO: This is simplified and does not handle add/remove from either source.
                DocumentCompare bindingCompare = new DocumentCompare();
                documentValues.AddRange(
                    data.SchemaDocuments.Select(s => new DocumentValue(s)).
                    Union(data.TransformDocuments.Select(s => new DocumentValue(s)), bindingCompare));

                if (templateValues.Count > 0)
                {
                    TemplateBinding.DataSource = templateValues;
                    ObjectBinding.DataSource = objectValues;
                    SchemaBinding.DataSource = schemaValues;
                    TransformBinding.DataSource = transformValues;
                    DocumentBinding.DataSource = documentValues;

                    TemplateBinding.RaiseListChangedEvents = true;
                    ObjectBinding.RaiseListChangedEvents = true;
                    SchemaBinding.RaiseListChangedEvents = true;
                    TransformBinding.RaiseListChangedEvents = true;
                    DocumentBinding.RaiseListChangedEvents = true;

                    templateValues.RaiseListChangedEvents = true;
                    objectValues.RaiseListChangedEvents = true;
                    schemaValues.RaiseListChangedEvents = true;
                    transformValues.RaiseListChangedEvents = true;
                    documentValues.RaiseListChangedEvents = true;
                }

                TemplateBinding.ResetBindings(false);
                ObjectBinding.ResetBindings(false);
                SchemaBinding.ResetBindings(false);
                TransformBinding.ResetBindings(false);
                DocumentBinding.ResetBindings(false);
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
        }
    }
}
