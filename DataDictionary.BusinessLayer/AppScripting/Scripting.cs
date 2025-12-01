using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Interface representing Scripting
    /// </summary>
    public interface IScripting :
        ITemplate, IDataSource, // Allows the this interface to act like it is each of the child wrappers.
        ILoadData<AppModel.IModelIndex>, ISaveData<AppModel.IModelIndex>,
        IBindListChanged
    {
        // These are re-named properties
        /// <inheritdoc cref="ITemplate.Nodes"/>
        ITemplateNodeData TemplateNodes { get; }

        /// <inheritdoc cref="ITemplate.NodeOwners"/>
        ITemplateNodeOwnerData TemplateNodeOwners { get; }

        /// <inheritdoc cref="IXElementBuilderList"/>
        IXElementBuilderList XBuilders { get; }
    }

    class Scripting : IScripting, IDataTableFile
    {
        private readonly DataSource dataSourceValue = new DataSource();
        private readonly Template templateValue = new Template();

        /// <inheritdoc cref="IDataSource.DataSources"/>
        public IDataSourceData DataSources { get { return dataSourceValue.DataSources; } }

        /// <inheritdoc cref="IDataSource.DataObjects"/>
        public IDataObjectData DataObjects { get { return dataSourceValue.DataObjects; } }

        /// <inheritdoc/>
        public ITemplateData Templates { get { return templateValue.Templates; } }

        /// <inheritdoc/>
        public ITemplateNodeData TemplateNodes { get { return templateValue.Nodes; } }

        /// <inheritdoc/>
        public ITemplateNodeOwnerData TemplateNodeOwners { get { return templateValue.NodeOwners; } }

        /// <inheritdoc/>
        public ITemplateInputData TemplateSources { get { return templateValue.TemplateSources; } }

        //ITemplateData ITemplate.Templates { get { return templateValue.Templates; } } // Not Needed
        //ITemplateInputData ITemplate.TemplateSources { get { return templateValue.TemplateSources; } } // Not Needed
        ITemplateNodeData ITemplate.Nodes { get { return templateValue.Nodes; } }
        ITemplateNodeOwnerData ITemplate.NodeOwners { get { return templateValue.NodeOwners; } }

        /// <inheritdoc/>
        IXElementBuilderList IScripting.XBuilders { get { return Builders; } }

        /// <inheritdoc cref="IXElementBuilderList"/>
        public XElementBuilderList Builders { get; } = new XElementBuilderList();

        public Scripting() : base()
        {
            dataSourceValue.ListChanged += OnListChanged;
            templateValue.ListChanged += OnListChanged;

            void OnListChanged(Object? sender, ListChangedEventArgs e)
            {
                if (ListChanged is ListChangedEventHandler handler)
                { handler(sender, e); }
            }
        }

        /// <inheritdoc/>
        public void Clear()
        {
            dataSourceValue.Clear();
            templateValue.Clear();
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(IModelIndex model)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(dataSourceValue.Delete(model));
            work.AddRange(templateValue.Delete(model));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(dataSourceValue.Delete());
            work.AddRange(templateValue.Delete());
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex model)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(dataSourceValue.Load(factory, model));
            work.AddRange(templateValue.Load(factory, model));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex model, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(dataSourceValue.Load(factory, model, asOfUtcDate));
            work.AddRange(templateValue.Load(factory, model, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        public void Remove(IModelIndex model)
        {
            dataSourceValue.Remove(model);
            templateValue.Remove(model);
        }

        #region IBindListChanged
        /// <inheritdoc/>
        public event ListChangedEventHandler? ListChanged;

        /// <inheritdoc/>
        public Boolean RaiseListChangedEvents
        {
            get
            {
                return dataSourceValue.RaiseListChangedEvents
                    && templateValue.RaiseListChangedEvents;
            }
            set
            {
                dataSourceValue.RaiseListChangedEvents = value;
                templateValue.RaiseListChangedEvents = value;
            }
        }

        /// <inheritdoc/>
        public void ResetBindings()
        {
            dataSourceValue.ResetBindings();
            templateValue.ResetBindings();
        }
        #endregion

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex model)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(dataSourceValue.Save(factory, model));
            work.AddRange(templateValue.Save(factory, model));
            return work;
        }

        public IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            List<WorkItem> work = new List<WorkItem>();

            if (dataSourceValue.DataSources.Count > 0
                || templateValue.Templates.Count > 0)
            {
                // Root Node
                NameSpaceSource root = new NameSpaceSource(ScopeType.Scripting);
                work.Add(new WorkItem() { DoWork = () => { addNamedScope(null, new NamedScopeValue(root)); } });

                // Children
                work.AddRange(dataSourceValue.LoadNamedScope(addNamedScope, (a) => { return root; }));
                work.AddRange(templateValue.LoadNamedScope(addNamedScope, (a) => { return root; }));
            }
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<DataTable> Export()
        {
            List<DataTable> work = new List<DataTable>();
            dataSourceValue.Export();
            templateValue.Export();
            return work;
        }

        /// <inheritdoc/>
        public void Import(DataSet source)
        {
            dataSourceValue.Import(source);
            templateValue.Import(source);
        }

        /// <inheritdoc/>
        public IDataSource GetDataSource()
        { return dataSourceValue; }

        /// <inheritdoc/>
        public ITemplate GetTemplate()
        { return templateValue; }

        IReadOnlyList<WorkItem> ISaveData<ITemplateIndex>.Save(IDatabaseWork factory, ITemplateIndex template)
        { return templateValue.Save(factory, template); }

        IReadOnlyList<WorkItem> ILoadData<ITemplateIndex>.Load(IDatabaseWork factory, ITemplateIndex template)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValue.Load(factory, template));
            work.AddRange(dataSourceValue.Load(factory, template));
            return work;
        }

        IReadOnlyList<WorkItem> ILoadData<ITemplateIndex>.Load(IDatabaseWork factory, ITemplateIndex template, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValue.Load(factory, template, asOfUtcDate));
            work.AddRange(dataSourceValue.Load(factory, template, asOfUtcDate));
            return work;
        }

        IReadOnlyList<WorkItem> IDeleteData<ITemplateIndex>.Delete(ITemplateIndex template)
        { return templateValue.Delete(template); }

        ITemporalData IGetTemporal<ITemplateIndex>.GetTemporal(ITemplateIndex template)
        { return templateValue.GetTemporal(template); }

        IReadOnlyList<WorkItem> ISaveData<IDataSourceIndex>.Save(IDatabaseWork factory, IDataSourceIndex dataSource)
        { return dataSourceValue.Save(factory, dataSource); }

        IReadOnlyList<WorkItem> ILoadData<IDataSourceIndex>.Load(IDatabaseWork factory, IDataSourceIndex dataSource)
        { return dataSourceValue.Load(factory, dataSource); }

        IReadOnlyList<WorkItem> ILoadData<IDataSourceIndex>.Load(IDatabaseWork factory, IDataSourceIndex dataSource, ITemporalIndex asOfUtcDate)
        { return dataSourceValue.Load(factory, dataSource, asOfUtcDate); }

        IReadOnlyList<WorkItem> IDeleteData<IDataSourceIndex>.Delete(IDataSourceIndex dataSource)
        { return dataSourceValue.Delete(dataSource); }


        ITemporalData IGetTemporal<IDataSourceIndex>.GetTemporal(IDataSourceIndex dataSource)
        { return dataSourceValue.GetTemporal(dataSource); }

        ITemporalData IGetTemporal<IModelIndex>.GetTemporal(IModelIndex key)
        { throw new InvalidOperationException("Use GetTemporal on ITemplate or IDataSource instead."); }

        /// <inheritdoc/>
        public void Remove(ITemplateNodeIndex dataKey)
        { templateValue.Remove(dataKey); }

        /// <inheritdoc/>
        public void Remove(ITemplateIndex dataKey)
        { templateValue.Remove(dataKey); }

        /// <inheritdoc/>
        public void Remove(IDataSourceIndex dataKey)
        { dataSourceValue.Remove(dataKey); }
    }
}
