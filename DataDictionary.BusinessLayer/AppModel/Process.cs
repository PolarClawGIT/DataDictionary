// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using System.ComponentModel;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface representing Model Process data
    /// </summary>
    public interface IProcess :
        ILoadData<IProcessIndex>, ISaveData<IProcessIndex>, IDeleteData<IProcessIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>,
        IBindListChanged,
        IGetTemporal<IModelIndex>, IGetTemporal<IProcessIndex>
    {
        /// <summary>
        /// List of Entities within the Model.
        /// </summary>
        IProcessData Processes { get; }

        /// <summary>
        /// List of Aliases for the Entities within the Model.
        /// </summary>
        IProcessAliasData Aliases { get; }

        /// <summary>
        /// List of Properties for the Entities within the Model.
        /// </summary>
        IProcessPropertyData Properties { get; }

        /// <summary>
        /// List of Definitions for the Entities within the Model.
        /// </summary>
        IProcessDefinitionData Definitions { get; }

        /// <summary>
        /// List of Arguments for the Entities within the Model.
        /// </summary>
        IProcessArgumentData Arguments { get; }

        /// <summary>
        /// List of Subject Areas for the Entities within the Model.
        /// </summary>
        IProcessSubjectAreaData SubjectArea { get; }

        /// <summary>
        /// Finds the Process that match the Alias Index.
        /// </summary>
        /// <param name="aliasIndex"></param>
        /// <returns></returns>
        IEnumerable<IProcessValue> FindProcess(IAliasIndex aliasIndex);

        /// <summary>
        /// Returns an empty IProcess.
        /// </summary>
        /// <returns></returns>
        public static IProcess Create()
        { return new Process(); }
    }

    partial class Process : IProcess, IDataTableFile
    {
        /// <inheritdoc/>
        public IProcessData Processes { get { return ProcessValues; } }
        private readonly ProcessData ProcessValues;

        /// <inheritdoc/>
        public IProcessAliasData Aliases { get { return aliasValues; } }
        private readonly ProcessAliasData aliasValues;

        /// <inheritdoc/>
        public IProcessDefinitionData Definitions { get { return definitionValues; } }
        private readonly ProcessDefinitionData definitionValues;

        /// <inheritdoc/>
        public IProcessPropertyData Properties { get { return propertyValues; } }
        private readonly ProcessPropertyData propertyValues;

        /// <inheritdoc/>
        public IProcessArgumentData Arguments { get { return ArgumentValues; } }
        private readonly ProcessArgumentData ArgumentValues;

        // TODO: Need to get a combined object ProcessArgument & Argument. How?

        /// <inheritdoc/>
        public IProcessSubjectAreaData SubjectArea { get { return subjectAreaValues; } }
        private readonly ProcessSubjectAreaData subjectAreaValues;

        public Process() : base()
        {
            ProcessValues = new ProcessData();
            aliasValues = new ProcessAliasData();
            definitionValues = new ProcessDefinitionData();
            propertyValues = new ProcessPropertyData();
            ArgumentValues = new ProcessArgumentData();
            subjectAreaValues = new ProcessSubjectAreaData();

            ProcessValues.ListChanged += OnListChanged;
            aliasValues.ListChanged += OnListChanged;
            definitionValues.ListChanged += OnListChanged;
            propertyValues.ListChanged += OnListChanged;
            ArgumentValues.ListChanged += OnListChanged;
            subjectAreaValues.ListChanged += OnListChanged;

            void OnListChanged(Object? sender, ListChangedEventArgs e)
            {
                if (ListChanged is ListChangedEventHandler handler)
                { handler(sender, e); }
            }
        }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(ProcessValues.Load(factory, dataKey));
            work.AddRange(aliasValues.Load(factory, dataKey));
            work.AddRange(propertyValues.Load(factory, dataKey));
            work.AddRange(definitionValues.Load(factory, dataKey));
            work.AddRange(ArgumentValues.Load(factory, dataKey));
            work.AddRange(subjectAreaValues.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(ProcessValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(aliasValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(propertyValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(definitionValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(ArgumentValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(subjectAreaValues.Load(factory, dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IProcessIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(ProcessValues.Load(factory, dataKey));
            work.AddRange(aliasValues.Load(factory, dataKey));
            work.AddRange(propertyValues.Load(factory, dataKey));
            work.AddRange(definitionValues.Load(factory, dataKey));
            work.AddRange(ArgumentValues.Load(factory, dataKey));
            work.AddRange(subjectAreaValues.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IProcessIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(ProcessValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(aliasValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(propertyValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(definitionValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(ArgumentValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(subjectAreaValues.Load(factory, dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IProcessIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(ProcessValues.Save(factory, dataKey));
            work.AddRange(aliasValues.Save(factory, dataKey));
            work.AddRange(propertyValues.Save(factory, dataKey));
            work.AddRange(definitionValues.Save(factory, dataKey));
            work.AddRange(ArgumentValues.Save(factory, dataKey));
            work.AddRange(subjectAreaValues.Save(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(ProcessValues.Save(factory, dataKey));
            work.AddRange(aliasValues.Save(factory, dataKey));
            work.AddRange(propertyValues.Save(factory, dataKey));
            work.AddRange(definitionValues.Save(factory, dataKey));
            work.AddRange(ArgumentValues.Save(factory, dataKey));
            work.AddRange(subjectAreaValues.Save(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(ProcessValues.Delete());
            work.AddRange(aliasValues.Delete());
            work.AddRange(propertyValues.Delete());
            work.AddRange(definitionValues.Delete());
            work.AddRange(ArgumentValues.Delete());
            work.AddRange(subjectAreaValues.Delete());

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public IReadOnlyList<WorkItem> Delete(IProcessIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(ProcessValues.Delete(dataKey));
            work.AddRange(aliasValues.Delete(dataKey));
            work.AddRange(propertyValues.Delete(dataKey));
            work.AddRange(definitionValues.Delete(dataKey));
            work.AddRange(ArgumentValues.Delete(dataKey));
            work.AddRange(subjectAreaValues.Delete(dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();
            result.Add(ProcessValues.ToDataTable());
            result.Add(aliasValues.ToDataTable());
            result.Add(propertyValues.ToDataTable());
            result.Add(definitionValues.ToDataTable());
            result.Add(ArgumentValues.ToDataTable());
            result.Add(subjectAreaValues.ToDataTable());
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public void Import(System.Data.DataSet source)
        {
            ProcessValues.Load(source);
            aliasValues.Load(source);
            propertyValues.Load(source);
            definitionValues.Load(source);
            ArgumentValues.Load(source);
            subjectAreaValues.Load(source);
        }

        /// <inheritdoc/>
        public IEnumerable<IProcessValue> FindProcess(IAliasIndex aliasIndex)
        {
            AliasIndex key = new AliasIndex(aliasIndex);
            return
                ProcessValues.Join(
                    Aliases.Where(w => key.Equals(w)),
                    Process => new ProcessIndex(Process),
                    alias => new ProcessIndex(alias),
                    (Process, alias) => Process).
                ToList();
        }


        public void Remove(IProcessIndex dataKey)
        {
            ProcessValues.Remove(dataKey);
            aliasValues.Remove(dataKey);
            propertyValues.Remove(dataKey);
            definitionValues.Remove(dataKey);
            ArgumentValues.Remove(dataKey);
            subjectAreaValues.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Remove(IModelIndex dataKey)
        {
            ProcessValues.Remove(dataKey);
            aliasValues.Remove(dataKey);
            propertyValues.Remove(dataKey);
            definitionValues.Remove(dataKey);
            ArgumentValues.Remove(dataKey);
            subjectAreaValues.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            ProcessValues.Clear();
            aliasValues.Clear();
            propertyValues.Clear();
            definitionValues.Clear();
            ArgumentValues.Clear();
            subjectAreaValues.Clear();
        }

        #region IBindListChanged
        /// <inheritdoc/>
        public event ListChangedEventHandler? ListChanged;

        /// <inheritdoc/>
        public Boolean RaiseListChangedEvents
        {
            get
            {
                return ProcessValues.RaiseListChangedEvents
                    && aliasValues.RaiseListChangedEvents
                    && propertyValues.RaiseListChangedEvents
                    && definitionValues.RaiseListChangedEvents
                    && ArgumentValues.RaiseListChangedEvents
                    && subjectAreaValues.RaiseListChangedEvents;
            }
            set
            {
                ProcessValues.RaiseListChangedEvents = value;
                aliasValues.RaiseListChangedEvents = value;
                propertyValues.RaiseListChangedEvents = value;
                definitionValues.RaiseListChangedEvents = value;
                ArgumentValues.RaiseListChangedEvents = value;
                subjectAreaValues.RaiseListChangedEvents = value;
            }
        }

        /// <inheritdoc/>
        public void ResetBindings()
        {
            ProcessValues.ResetBindings();
            aliasValues.ResetBindings();
            propertyValues.ResetBindings();
            definitionValues.ResetBindings();
            ArgumentValues.ResetBindings();
            subjectAreaValues.ResetBindings();
        }
        #endregion



        public ITemporalData GetTemporal(IModelIndex key)
        { return ProcessValues.GetTemporal(key); }

        public ITemporalData GetTemporal(IProcessIndex key)
        { return ProcessValues.GetTemporal(key); }
    }
}
