using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class Template
    {
        partial class FormBinding
        {
            /// <summary>
            /// Returns the Current Template data.
            /// </summary>
            public Func<ITemplateData> GetData { get; private set; } = () => BusinessData.Templates;

            /// <summary>
            /// How to invoke the WorkerQueue.
            /// </summary>
            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            public FormBinding() : base()
            { }

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

                templateValues = new BindingView<TemplateValue>(GetData(), w => key.Equals(w));
                objectValues = new BindingView<TemplateObjectValue>(GetData().Objects, w => key.Equals(w));
                schemaValues = new BindingView<SchemaDefinitionValue>(GetData().Schemata, w => key.Equals(w));
                transformValues = new BindingView<TransformValue>(GetData().Transforms, w => key.Equals(w));

                DocumentCompare compare = new DocumentCompare();
                documentValues = new BindingList<DocumentValue>();
                documentValues.AddRange(
                    GetData().SchemaDocuments.Select(s => new DocumentValue(s)).
                    Union(GetData().TransformDocuments.Select(s => new DocumentValue(s)), compare));

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
                work.Add(new WorkItem() { DoWork = () => { GetData = () => BusinessData.Templates; } });
                work.AddRange(GetData().Delete(template));
                work.AddRange(GetData().Load(factory, template));

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
                work.Add(new WorkItem() { DoWork = () => { GetData = () => ITemplateData.Create(); } });
                work.AddRange(GetData().Load(factory, template, temporal));

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
                work.AddRange(GetData().Save(factory, template));
                work.Add(new WorkItem() { DoWork = () => { GetData = () => BusinessData.Templates; } });
                work.AddRange(GetData().Delete(template));
                work.AddRange(GetData().Load(factory, template));

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(template);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public ITemporalData GetTemporal(TemplateIndex template)
            { return GetData().GetTemporal(template); }

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
