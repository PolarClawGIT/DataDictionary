using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class ScriptingTemplate
    {
        //TODO: Think this form needs to be rebuilt completely.
        class FormBinding
        {
            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            public required BindingSource BindingTemplate { private get; init; }
            public BindingView<ScriptingTemplateValue> Templates { get; private set; } =
                new BindingView<ScriptingTemplateValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingNode { private get; init; }
            public BindingView<ScriptingNodeValue> TemplateNodes { get; private set; } =
                new BindingView<ScriptingNodeValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingPath { private get; init; }
            public BindingView<ScriptingPathValue> TemplatePaths { get; private set; } =
                new BindingView<ScriptingPathValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingAttributes { private get; init; }
            public BindingView<ScriptingAttributeValue> TemplateAttributes { get; private set; } =
                new BindingView<ScriptingAttributeValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingDocument { private get; init; }
            public BindingView<XDocumentValue> TemplateDocuments { get; private set; } =
                new BindingView<XDocumentValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            ScriptingTemplateIndex templateIndex = new ScriptingTemplateIndex();
            //IScriptingEngine scriptingData = BusinessData.ScriptingEngine;
            TemporalIndex? temporalIndex = null;

            public IEnumerable<ScriptingNodeIndexName> Properties
            {
                get
                {
                    return new List<ScriptingNodeIndexName>();
                    //return scriptingData.Properties;
                }
            }

            public FormBinding()
            { }

            public void Init()
            {
                //Templates = new BindingView<ScriptingTemplateValue>(scriptingData.Templates);
                //TemplateNodes = new BindingView<ScriptingNodeValue>(scriptingData.TemplateNodes);
                //TemplatePaths = new BindingView<ScriptingPathValue>(scriptingData.TemplatePaths);
                //TemplateAttributes = new BindingView<ScriptingAttributeValue>(scriptingData.TemplateAttributes);
                //TemplateDocuments = new BindingView<XDocumentValue>(scriptingData.TemplateDocuments);

                BindingTemplate.DataSource = Templates;
                BindingNode.DataSource = TemplateNodes;
                BindingPath.DataSource = TemplatePaths;
                BindingAttributes.DataSource = TemplateAttributes;
                BindingDocument.DataSource = TemplateDocuments;
            }

            public void SetPosition(IScriptingTemplateIndex template)
            {
                templateIndex = new ScriptingTemplateIndex(template);

                BindingTemplate.RaiseListChangedEvents = false;
                BindingNode.RaiseListChangedEvents = false;
                BindingPath.RaiseListChangedEvents = false;
                BindingAttributes.RaiseListChangedEvents = false;
                BindingDocument.RaiseListChangedEvents = false;

                Templates.RaiseListChangedEvents = false;
                TemplateNodes.RaiseListChangedEvents = false;
                TemplatePaths.RaiseListChangedEvents = false;
                TemplateAttributes.RaiseListChangedEvents = false;
                TemplateDocuments.RaiseListChangedEvents = false;

                //Templates = new BindingView<ScriptingTemplateValue>(scriptingData.Templates, w => templateIndex.Equals(w));
                //TemplateNodes = new BindingView<ScriptingNodeValue>(scriptingData.TemplateNodes, w => templateIndex.Equals(w));
                //TemplatePaths = new BindingView<ScriptingPathValue>(scriptingData.TemplatePaths, w => templateIndex.Equals(w));
                //TemplateAttributes = new BindingView<ScriptingAttributeValue>(scriptingData.TemplateAttributes, w => templateIndex.Equals(w));
                //TemplateDocuments = new BindingView<XDocumentValue>(scriptingData.TemplateDocuments, w => templateIndex.Equals(w));

                if (Templates.Count > 0)
                {
                    BindingTemplate.DataSource = Templates;
                    BindingNode.DataSource = TemplateNodes;
                    BindingPath.DataSource = TemplatePaths;
                    BindingAttributes.DataSource = TemplateAttributes;
                    BindingDocument.DataSource = TemplateDocuments;

                    BindingTemplate.RaiseListChangedEvents = true;
                    BindingNode.RaiseListChangedEvents = true;
                    BindingPath.RaiseListChangedEvents = true;
                    BindingAttributes.RaiseListChangedEvents = true;
                    BindingDocument.RaiseListChangedEvents = true;

                    Templates.RaiseListChangedEvents = true;
                    TemplateNodes.RaiseListChangedEvents = true;
                    TemplatePaths.RaiseListChangedEvents = true;
                    TemplateAttributes.RaiseListChangedEvents = true;
                    TemplateDocuments.RaiseListChangedEvents = true;
                }

                Templates.ResetBindings();
                TemplateNodes.ResetBindings();
                TemplatePaths.ResetBindings();
                TemplateAttributes.ResetBindings();
                TemplateDocuments.ResetBindings();

                BindingTemplate.MoveFirst(); // Needs to be done last.
            }

            public Boolean TryGetValue([NotNullWhen(true)] out ScriptingTemplateValue? result)
            {
                if (BindingTemplate.Position >= 0
                    && BindingTemplate.Current is ScriptingTemplateValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryGetValue([NotNullWhen(true)] out ScriptingNodeValue? result)
            {
                if (BindingNode.Position >= 0
                    && BindingNode.Current is ScriptingNodeValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryGetValue(IScriptingNodeIndexName index, [NotNullWhen(true)] out ScriptingNodeValue? result)
            {
                ScriptingNodeIndexName key = new ScriptingNodeIndexName(index);
                if (TemplateNodes.FirstOrDefault(w => key.Equals(w)) is ScriptingNodeValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryGetValue(IScriptingNodeIndex index, [NotNullWhen(true)] out ScriptingNodeValue? result)
            {
                ScriptingNodeIndex key = new ScriptingNodeIndex(index);
                if (TemplateNodes.FirstOrDefault(w => key.Equals(w)) is ScriptingNodeValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public IScriptingTemplateValue NewValue()
            {
                ScriptingTemplateValue newValue = new ScriptingTemplateValue();
                //scriptingData.Templates.Add(newValue);
                SetPosition(newValue);

                return newValue;
            }

            public IScriptingNodeValue NewNode()
            {
                if (TryGetValue(out ScriptingTemplateValue? template))
                { return new ScriptingNodeValue(template); }
                else { throw new InvalidOperationException("Current ScriptingTemplateValue not defined"); }
            }

            public IScriptingPathValue NewPath()
            {
                if (TryGetValue(out ScriptingTemplateValue? template))
                { return new ScriptingPathValue(template); }
                else { throw new InvalidOperationException("Current ScriptingTemplateValue not defined"); }
            }



            public void RemoveValue()
            {
                if (TryGetValue(out ScriptingTemplateValue? value))
                {
                    //scriptingData.RaiseListChangedEvents = false;
                    //scriptingData.Remove(value);
                    SetPosition(value);
                }
            }

            public void Load(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());

                if (temporalIndex is null)
                {
                    //scriptingData = BusinessData.ScriptingEngine;
                    //work.AddRange(scriptingData.Delete());
                    //work.AddRange(scriptingData.Load(factory, BusinessData.Model.ModelIndex));
                }
                else
                {
                    //attributeData = IAttribute.Create(BusinessData.Model.Properties, BusinessData.Model.Definitions);
                    //work.AddRange(attributeData.Load(factory, attributeIndex, temporalIndex));
                }

                DoWork(work, StartBinding);

                void StartBinding(RunWorkerCompletedEventArgs args)
                { if (onComplete is not null) { onComplete(args); } }
            }

            public void Save(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                //work.AddRange(attributeData.Save(factory, attributeIndex));

                DoWork(work, onComplete);
            }

            public Boolean GetAuthorization(CommandImageType command)
            {
                Boolean isGrant = false;
                SecurableIndex securable = BusinessData.Model.ModelIndex;
                isGrant = BusinessData.Authorization.IsGrant(securable);

                switch (command)
                {
                    case CommandImageType.Default: return true;
                    case CommandImageType.Delete: return BusinessData.Authorization.IsScriptAdmin || isGrant;
                    case CommandImageType.OpenDatabase: return BusinessData.Authorization.IsScriptAdmin || isGrant;
                    case CommandImageType.SaveDatabase: return BusinessData.Authorization.IsScriptAdmin || isGrant;
                    case CommandImageType.DeleteDatabase: return BusinessData.Authorization.IsScriptAdmin || isGrant;
                    case CommandImageType.HistoryDatabase: return BusinessData.Authorization.IsScriptAdmin || isGrant;
                    default: return false;
                }
            }

            public Boolean GetLocked()
            {
                if (TryGetValue(out ScriptingTemplateValue? value))
                { return value.RowState() is DataRowState.Detached or DataRowState.Deleted; }
                else { return true; }
            }

        }
    }
}
