using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Interface representing Scripting
    /// </summary>
    public interface IScripting :
        ILoadData<AppModel.IModelIndex>, ISaveData<AppModel.IModelIndex>,
        IBindListChanged
    {
        /// <inheritdoc cref="IDataSource.DataSources"/>
        IDataSourceData DataSources { get; }

        /// <inheritdoc cref="IDataSource.DataObjects"/>
        IDataObjectData DataObjects { get; }

        /// <inheritdoc cref="ITemplate.Templates"/>
        ITemplateData Templates { get; }

        /// <inheritdoc cref="ITemplate.Elements"/>
        ITemplateElementData TemplateElements { get; }

        /// <inheritdoc cref="ITemplate.Attributes"/>
        ITemplateAttributeData TemplateAttributes { get; }

        /// <inheritdoc cref="ITemplate.AttributeOwners"/>
        ITemplateNodeOwnerData TemplateAttributeOwners { get; }

        /// <inheritdoc cref="ITemplate.TemplateSources"/>
        ITemplateInputData TemplateSources { get; }

        /// <summary>
        /// Gets the IDataSource wrapper instance.
        /// </summary>
        /// <returns></returns>
        IDataSource GetDataSource();

        /// <summary>
        /// Gets the ITemplate wrapper instance.
        /// </summary>
        /// <returns></returns>
        ITemplate GetTemplate();
    }

    class Scripting : IScripting, IDataTableFile
    {
        private readonly DataSource dataSourceValue = new DataSource();
        private readonly Template templateValue = new Template();

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
        public IDataSourceData DataSources { get { return dataSourceValue.DataSources; } }

        /// <inheritdoc/>
        public IDataObjectData DataObjects { get { return dataSourceValue.DataObjects; } }

        /// <inheritdoc/>
        public ITemplateData Templates { get { return templateValue.Templates; } }

        /// <inheritdoc/>
        public ITemplateElementData TemplateElements { get { return templateValue.Elements; } }

        /// <inheritdoc/>
        public ITemplateAttributeData TemplateAttributes { get { return templateValue.Attributes; } }

        /// <inheritdoc/>
        public ITemplateNodeOwnerData TemplateAttributeOwners { get { return templateValue.AttributeOwners; } }

        /// <inheritdoc/>
        public ITemplateInputData TemplateSources { get { return templateValue.TemplateSources; } }

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

        /// <inheritdoc/>
        public void ResetBindings()
        {
            dataSourceValue.ResetBindings();
            templateValue.ResetBindings();
        }

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

            // TODO: Create parent and pass to methods.

            work.AddRange(dataSourceValue.LoadNamedScope(addNamedScope));
            work.AddRange(templateValue.LoadNamedScope(addNamedScope));
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
    }
}
