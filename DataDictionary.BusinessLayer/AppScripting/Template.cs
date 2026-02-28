using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Interface representing Scripting Template data
    /// </summary>
    public interface ITemplate :
        ILoadData<ITemplateIndex>, ISaveData<ITemplateIndex>, IDeleteData<ITemplateIndex>,
        ILoadData<AppModel.IModelIndex>, ISaveData<AppModel.IModelIndex>,
        IGetTemporal<AppModel.IModelIndex>, IGetTemporal<ITemplateIndex>,
        IRemoveData<ITemplateNodeIndex>, IRemoveData<IDataSourceIndex>,
        IBindListChanged
    {
        /// <summary>
        /// List of Scripting Engine Templates.
        /// </summary>
        ITemplateData Templates { get; }

        /// <summary>
        /// List of Scripting Nodes for the Template
        /// </summary>
        ITemplateNodeData Nodes { get; }

        /// <summary>
        /// List of Scripting Node owners for the Template.
        /// </summary>
        ITemplateNodeOwnerData NodeOwners { get; }

        /// <summary>
        /// List of Scripting Data Sources associated with a Templates.
        /// </summary>
        ITemplateInputData TemplateSources { get; }

        /// <summary>
        /// Creates an empty instance of ITemplate
        /// </summary>
        public static ITemplate Create()
        { return new Template(); }
    }

    class Template : ITemplate
    {
        /// <inheritdoc/>
        public ITemplateData Templates { get { return templateValues; } }
        TemplateData templateValues = new TemplateData();

        /// <inheritdoc/>
        public ITemplateNodeData Nodes { get { return templateNodes; } }
        TemplateNodeData templateNodes = new TemplateNodeData();

        /// <inheritdoc/>
        public ITemplateNodeOwnerData NodeOwners { get { return templateNodeOwners; } }
        TemplateNodeOwnerData templateNodeOwners = new TemplateNodeOwnerData();

        /// <inheritdoc/>
        public ITemplateInputData TemplateSources { get { return templateSources; } }
        TemplateInputData templateSources = new TemplateInputData();

        public Template() : base()
        {
            templateValues.ListChanged += OnListChanged;
            templateNodes.ListChanged += OnListChanged;
            templateNodeOwners.ListChanged += OnListChanged;
            templateSources.ListChanged += OnListChanged;

            void OnListChanged(Object? sender, ListChangedEventArgs e)
            {
                if (ListChanged is ListChangedEventHandler handler)
                { handler(sender, e); }
            }
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<DataTable> Export()
        {
            List<DataTable> result = new List<DataTable>();
            if (templateValues.Count > 0)
            {
                result.Add(templateValues.ToDataTable());
                result.Add(templateNodes.ToDataTable());
                result.Add(templateNodeOwners.ToDataTable());
                result.Add(templateSources.ToDataTable());
            }
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public void Import(DataSet source)
        {
            if(templateValues.Load(source, default, true))
            {
                templateNodes.Load(source);
                templateNodeOwners.Load(source);
                templateSources.Load(source);
            }
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, AppModel.IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Load(factory, dataKey));
            work.AddRange(templateNodes.Load(factory, dataKey));
            work.AddRange(templateNodeOwners.Load(factory, dataKey));
            work.AddRange(templateSources.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, AppModel.IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(templateValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(templateNodes.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(templateNodeOwners.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(templateSources.Load(factory, dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Load(factory, dataKey));
            work.AddRange(templateNodes.Load(factory, dataKey));
            work.AddRange(templateNodeOwners.Load(factory, dataKey));
            work.AddRange(templateSources.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(templateValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(templateNodes.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(templateNodeOwners.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(templateSources.Load(factory, dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, AppModel.IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Save(factory, dataKey));
            work.AddRange(templateNodes.Save(factory, dataKey));
            work.AddRange(templateNodeOwners.Save(factory, dataKey));
            work.AddRange(templateSources.Save(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Save(factory, dataKey));
            work.AddRange(templateNodes.Save(factory, dataKey));
            work.AddRange(templateNodeOwners.Save(factory, dataKey));
            work.AddRange(templateSources.Save(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Delete(ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Delete(dataKey));
            work.AddRange(templateValues.Delete(dataKey));
            work.AddRange(templateNodes.Delete(dataKey));
            work.AddRange(templateNodeOwners.Delete(dataKey));
            work.AddRange(templateSources.Delete(dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Delete(AppModel.IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Delete(dataKey));
            work.AddRange(templateValues.Delete(dataKey));
            work.AddRange(templateNodes.Delete(dataKey));
            work.AddRange(templateNodeOwners.Delete(dataKey));
            work.AddRange(templateSources.Delete(dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Delete());
            work.AddRange(templateValues.Delete());
            work.AddRange(templateNodes.Delete());
            work.AddRange(templateNodeOwners.Delete());
            work.AddRange(templateSources.Delete());
            return work;
        }

        /// <inheritdoc/>
        public ITemporalData GetTemporal(ITemplateIndex key)
        { return templateValues.GetTemporal(key); }

        /// <inheritdoc/>
        public ITemporalData GetTemporal(AppModel.IModelIndex key)
        { return templateValues.GetTemporal(key); }

        /// <inheritdoc/>
        public void Remove(AppModel.IModelIndex dataKey)
        {
            templateValues.Remove(dataKey);
            templateNodes.Remove(dataKey);
            templateNodeOwners.Remove(dataKey);
            templateSources.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Remove(ITemplateIndex dataKey)
        {
            templateValues.Remove(dataKey);
            templateNodes.Remove(dataKey);
            templateNodeOwners.Remove(dataKey);
            templateSources.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Remove(IDataSourceIndex dataKey)
        { templateSources.Remove(dataKey); }

        /// <inheritdoc/>
        public void Remove(ITemplateNodeIndex dataKey)
        {
            TemplateNodeIndex key = new TemplateNodeIndex(dataKey);
            TemplateNodeOwnerIndex owner = new TemplateNodeOwnerIndex(key);

            templateNodeOwners.Remove(owner);
            templateNodeOwners.Remove(key);
            templateNodes.Remove(key);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            templateValues.Clear();
            templateNodes.Clear();
            templateNodeOwners.Clear();
            templateSources.Clear();
        }

        public IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope, Func<TemplateValue, INamedScopeSourceValue?>? getParent)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(NameSpaceSource.Load<TemplateData, TemplateValue>(templateValues, addNamedScope, getParent));
            return work;
        }

        #region IBindListChanged
        /// <inheritdoc/>
        public event ListChangedEventHandler? ListChanged;

        /// <inheritdoc/>
        public Boolean RaiseListChangedEvents
        {
            get
            {
                return templateValues.RaiseListChangedEvents
                    && templateNodes.RaiseListChangedEvents
                    && templateNodeOwners.RaiseListChangedEvents
                    && templateSources.RaiseListChangedEvents;
            }
            set
            {
                templateValues.RaiseListChangedEvents = value;
                templateNodes.RaiseListChangedEvents = value;
                templateNodeOwners.RaiseListChangedEvents = value;
                templateSources.RaiseListChangedEvents = value;
            }
        }

        /// <inheritdoc/>
        public void ResetBindings()
        {
            templateValues.ResetBindings();
            templateNodes.ResetBindings();
            templateNodeOwners.ResetBindings();
            templateSources.ResetBindings();
        }
        #endregion
    }
}
