using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
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

            ProcessIndex processIndex = new ProcessIndex();
            TemporalIndex? temporalIndex = null;
            IProcess processData = BusinessData.Model.Processes;

            public FormBinding()
            { }

            public void Init()
            {
                // Note: C# 13 (Nov 2025?) adds "field". Allows code to be applied to "Init".

                Process = new BindingView<ProcessValue>(processData.Values, w => processIndex.Equals(w));
                Properties = new BindingView<ProcessPropertyValue>(processData.Properties, w => processIndex.Equals(w));
                Aliases = new BindingView<ProcessAliasValue>(processData.Aliases, w => processIndex.Equals(w));
                SubjectAreas = new BindingView<ProcessSubjectAreaValue>(processData.SubjectArea, w => processIndex.Equals(w));
                Definitions = new BindingView<ProcessDefinitionValue>(processData.Definitions, w => processIndex.Equals(w));
                Arguments = new BindingView<ProcessArgumentValue>(processData.Arguments, w => processIndex.Equals(w));

                BindingProcess.DataSource = Process;
                BindingProperty.DataSource = Properties;
                BindingAlias.DataSource = Aliases;
                BindingSubjectArea.DataSource = SubjectAreas;
                BindingDefinition.DataSource = Definitions;
                BindingArgument.DataSource = Arguments;
            }

            public IProcessIndex? NewValue()
            {
                ProcessValue newValue = new ProcessValue();
                processData.Values.Add(newValue);
                SetPosition(newValue);

                return newValue;
            }

            public void SetPosition(IProcessIndex process)
            {
                processIndex = new ProcessIndex(process);

                BindingProcess.RaiseListChangedEvents = false;
                BindingProperty.RaiseListChangedEvents = false;
                BindingAlias.RaiseListChangedEvents = false;
                BindingSubjectArea.RaiseListChangedEvents = false;
                BindingDefinition.RaiseListChangedEvents = false;
                BindingArgument.RaiseListChangedEvents = false;

                Process.RaiseListChangedEvents = false;
                Properties.RaiseListChangedEvents = false;
                Aliases.RaiseListChangedEvents = false;
                SubjectAreas.RaiseListChangedEvents = false;
                Definitions.RaiseListChangedEvents = false;

                Process = new BindingView<ProcessValue>(processData.Values, w => processIndex.Equals(w));
                Properties = new BindingView<ProcessPropertyValue>(processData.Properties, w => processIndex.Equals(w));
                Aliases = new BindingView<ProcessAliasValue>(processData.Aliases, w => processIndex.Equals(w));
                SubjectAreas = new BindingView<ProcessSubjectAreaValue>(processData.SubjectArea, w => processIndex.Equals(w));
                Definitions = new BindingView<ProcessDefinitionValue>(processData.Definitions, w => processIndex.Equals(w));
                Arguments = new BindingView<ProcessArgumentValue>(processData.Arguments, w => processIndex.Equals(w));

                if (Process.Count > 0)
                {
                    BindingProcess.DataSource = Process;
                    BindingProperty.DataSource = Properties;
                    BindingAlias.DataSource = Aliases;
                    BindingSubjectArea.DataSource = SubjectAreas;
                    BindingDefinition.DataSource = Definitions;
                    BindingArgument.DataSource = Arguments;

                    BindingProcess.RaiseListChangedEvents = true;
                    BindingProperty.RaiseListChangedEvents = true;
                    BindingAlias.RaiseListChangedEvents = true;
                    BindingSubjectArea.RaiseListChangedEvents = true;
                    BindingDefinition.RaiseListChangedEvents = true;
                    BindingArgument.RaiseListChangedEvents = true;

                    Process.RaiseListChangedEvents = true;
                    Properties.RaiseListChangedEvents = true;
                    Aliases.RaiseListChangedEvents = true;
                    SubjectAreas.RaiseListChangedEvents = true;
                    Definitions.RaiseListChangedEvents = true;
                    Arguments.RaiseListChangedEvents = true;
                }

                Process.ResetBindings();
                Properties.ResetBindings();
                Aliases.ResetBindings();
                SubjectAreas.ResetBindings();
                Definitions.ResetBindings();
                Arguments.ResetBindings();

                BindingProcess.MoveFirst(); // For some reason this must be done last or it does not work.
            }

            public Boolean TryGetValue([NotNullWhen(true)] out ProcessValue? result)
            {
                if (BindingProcess.Position >= 0
                    && BindingProcess.Current is ProcessValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryGetArgument([NotNullWhen(true)] out ProcessArgumentValue? result)
            {
                if (BindingArgument.Position >= 0
                    && BindingArgument.Current is ProcessArgumentValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public IPropertySubType NewProperty()
            {
                if (TryGetValue(out ProcessValue? value))
                { return new ProcessPropertyValue(value); }
                else { throw new InvalidOperationException("Current ProcessValue not defined"); }
            }

            public IDefinitionSubType NewDefinition()
            {
                if (TryGetValue(out ProcessValue? value))
                { return new ProcessDefinitionValue(value); }
                else { throw new InvalidOperationException("Current ProcessValue not defined"); }
            }

            public IProcessArgumentValue NewArgument()
            {
                if (TryGetValue(out ProcessValue? value))
                { return new ProcessArgumentValue(value); }
                else { throw new InvalidOperationException("Current ProcessValue not defined"); }
            }

            public void AddSubjectArea(ISubjectAreaIndex index)
            {
                if (TryGetValue(out ProcessValue? value))
                { SubjectAreas.Add(new ProcessSubjectAreaValue(value, index)); }
            }

            public void RemoveSubjectArea(ISubjectAreaValue value)
            {
                SubjectAreaIndex key = new SubjectAreaIndex(value);

                while (SubjectAreas.FirstOrDefault(w => key.Equals(w)) is ProcessSubjectAreaValue item)
                { SubjectAreas.Remove(item); }
            }

            public Boolean? GetLocked()
            {
                if (TryGetValue(out ProcessValue? value))
                {
                    return value.RowState() is DataRowState.Detached
                        or DataRowState.Deleted;
                }
                else return true;
            }

            public Boolean GetAuthorization(CommandImageType command)
            {
                Boolean isGrant = false;
                SecurableIndex securable = BusinessData.Model.ModelIndex;
                isGrant = BusinessData.Authorization.IsGrant(securable);

                switch (command)
                {
                    case CommandImageType.Default: return true;
                    case CommandImageType.Delete: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    case CommandImageType.OpenDatabase: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    case CommandImageType.SaveDatabase: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    case CommandImageType.DeleteDatabase: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    case CommandImageType.HistoryDatabase: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    default: return false;
                }
            }

            public Boolean GetIsOpen(IProcessValue value)
            {
                ProcessIndex key = new ProcessIndex(value);
                return TryGetValue(out ProcessValue? current) && key.Equals(current);
            }

            public void Load(Action<RunWorkerCompletedEventArgs> onCompleting)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                StopBinding();
                work.Add(factory.OpenConnection());

                if (temporalIndex is null)
                {
                    work.AddRange(processData.Delete(processIndex));
                    work.AddRange(processData.Load(factory, processIndex));
                }
                else
                {
                    processData = IProcess.Create();
                    work.AddRange(processData.Load(factory, processIndex, temporalIndex));
                }

                DoWork(work, StartBinding);

                void StopBinding()
                {
                    BindingProcess.SuspendBinding();
                    BindingProperty.SuspendBinding();
                    BindingAlias.SuspendBinding();
                    BindingSubjectArea.SuspendBinding();
                    BindingDefinition.SuspendBinding();
                    BindingArgument.SuspendBinding();
                }

                void StartBinding(RunWorkerCompletedEventArgs args)
                {
                    SetPosition(processIndex);
                    BindingProcess.ResumeBinding();
                    BindingProperty.ResumeBinding();
                    BindingAlias.ResumeBinding();
                    BindingSubjectArea.ResumeBinding();
                    BindingDefinition.ResumeBinding();
                    BindingArgument.ResumeBinding();

                    if (onCompleting is not null) { onCompleting(args); }
                }
            }

            public void SetPosition(IProcessIndex process, ITemporalIndex temporal)
            {
                SetPosition(process);
                temporalIndex = new TemporalIndex(temporal);
            }

            public IAliasSubType NewAlias ()
            {
                if (TryGetValue(out ProcessValue? value))
                { return new ProcessAliasValue(value); }
                else { throw new InvalidOperationException("Current AttributeValue not defined"); }
            }
        }
    }
}
