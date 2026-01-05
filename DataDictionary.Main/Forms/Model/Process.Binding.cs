using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
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

            public required BindingSource BindingArgument
            { private get; init { field = value; field.DataSource = Arguments; } }

            public BindingView<ProcessArgumentValue> Arguments { get; private set; } =
                new BindingView<ProcessArgumentValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            IProcess data = BusinessData.Model.Process;

            public FormBinding()
            { }

            public ProcessValue NewValue()
            {
                ProcessValue newValue = new ProcessValue();
                data.Processes.Add(newValue);

                return newValue;
            }


            public void Load(ProcessIndex process)
            {
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

                Process = new BindingView<ProcessValue>(data.Processes, w => process.Equals(w));
                Properties = new BindingView<ProcessPropertyValue>(data.Properties, w => process.Equals(w));
                Aliases = new BindingView<ProcessAliasValue>(data.Aliases, w => process.Equals(w));
                SubjectAreas = new BindingView<ProcessSubjectAreaValue>(data.SubjectArea, w => process.Equals(w));
                Definitions = new BindingView<ProcessDefinitionValue>(data.Definitions, w => process.Equals(w));
                Arguments = new BindingView<ProcessArgumentValue>(data.Arguments, w => process.Equals(w));

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


            public void Load(ProcessIndex process, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());

                work.Add(new WorkItem() { DoWork = () => { data = BusinessData.Model.Process; } });
                work.AddRange(data.Delete(process));
                work.AddRange(data.Load(factory, process));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(process);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public void Load(ProcessIndex process, TemporalIndex temporal, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.Add(new WorkItem() { DoWork = () => { data = IProcess.Create(); } });
                work.AddRange(data.Load(factory, process, temporal));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(process);
                    if (onComplete is not null) { onComplete(args); }
                }
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

            public void AddArgument(PathIndex path)
            {
                if (TryGetValue(out ProcessValue? value))
                {
                    ProcessArgumentValue newValue = new ProcessArgumentValue(value);
                    newValue.OrdinalPosition = Arguments.Count + 1;
                    newValue.ArgumentPath = path;
                    newValue.ArgumentKnownAs = path.Member;

                    Arguments.Add(newValue);
                }

                var positions = Arguments.OrderBy(o => o.OrdinalPosition).ThenBy(o => o.ArgumentKnownAs).ToList();
                foreach (ProcessArgumentValue item in Arguments)
                { item.OrdinalPosition = positions.IndexOf(item) + 1; }

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

            public Boolean GetAuthorization(Enumerations.CommandType command)
            {
                Boolean isGrant = false;
                SecurableIndex securable = BusinessData.Model.ModelIndex;
                isGrant = BusinessData.Authorization.IsGrant(securable);

                switch (command)
                {
                    case Enumerations.CommandType.Default: return true;
                    case Enumerations.CommandType.Delete: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    case Enumerations.CommandType.OpenDatabase: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    case Enumerations.CommandType.SaveDatabase: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    case Enumerations.CommandType.DeleteDatabase: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    case Enumerations.CommandType.HistoryDatabase: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    default: return false;
                }
            }

            public Boolean GetIsOpen(IProcessValue value)
            {
                ProcessIndex key = new ProcessIndex(value);
                return TryGetValue(out ProcessValue? current) && key.Equals(current);
            }

            public IAliasSubType NewAlias()
            {
                if (TryGetValue(out ProcessValue? value))
                { return new ProcessAliasValue(value); }
                else { throw new InvalidOperationException("Current AttributeValue not defined"); }
            }

            public void Save(ProcessIndex process, Action<RunWorkerCompletedEventArgs> onCompleting)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(data.Save(factory, process));

                DoWork(work, onCompleting);
            }

            public void Remove(ProcessIndex process)
            { data.Remove(process); }

            public ITemporalData GetTemporal(ProcessIndex process)
            {
                { return data.GetTemporal(process); }
            }
        }
    }
}
