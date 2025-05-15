using DataDictionary.BusinessLayer.AppModel;
using System.ComponentModel;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Model
{
    partial class Process
    {
        class FormBinding
        {
            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            public required BindingSource BindingProcess { private get; init; }
            public BindingView<ProcessValue> Process { get; private set; } =
                new BindingView<ProcessValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingAlias { private get; init; }
            public BindingView<ProcessAliasValue> Aliases { get; private set; } =
                new BindingView<ProcessAliasValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingSubjectArea { private get; init; }
            public BindingView<ProcessSubjectAreaValue> SubjectAreas { get; private set; } =
                new BindingView<ProcessSubjectAreaValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingProperty { private get; init; }
            public BindingView<ProcessPropertyValue> Properties { get; private set; } =
                new BindingView<ProcessPropertyValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingDefinition { private get; init; }
            public BindingView<ProcessDefinitionValue> Definitions { get; private set; } =
                new BindingView<ProcessDefinitionValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingArgument { private get; init; }
            public BindingView<ProcessArgumentValue> Arguments { get; private set; } =
                new BindingView<ProcessArgumentValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public void Init()
            {
                // Note: C# 13 adds "field".


                //throw new NotImplementedException();
            }
        }
    }
}
