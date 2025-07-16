using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class ScriptingTemplate
    {
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

            IScriptingEngine scriptingData = BusinessData.ScriptingEngine;
            TemporalIndex? temporalIndex = null;

            public IEnumerable<ScriptingNodeIndexName> Properties { get { return scriptingData.Properties; } }

            public FormBinding()
            { }

            public void Init()
            {
                Templates = new BindingView<ScriptingTemplateValue>(scriptingData.Templates);
                TemplateNodes = new BindingView<ScriptingNodeValue>(scriptingData.TemplateNodes);
                TemplatePaths = new BindingView<ScriptingPathValue>(scriptingData.TemplatePaths);
                TemplateAttributes = new BindingView<ScriptingAttributeValue>(scriptingData.TemplateAttributes);

                BindingTemplate.DataSource = Templates;
                BindingNode.DataSource = TemplateNodes;
                BindingPath.DataSource = TemplatePaths;
            }

            public void Load(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());

                if (temporalIndex is null)
                {
                    scriptingData = BusinessData.ScriptingEngine;
                    work.AddRange(scriptingData.Delete());
                    work.AddRange(scriptingData.Load(factory, BusinessData.Model.ModelIndex));
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

        }
    }
}
