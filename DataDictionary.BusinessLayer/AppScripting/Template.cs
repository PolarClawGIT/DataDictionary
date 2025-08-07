using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
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
        IBindListChanged
    {
        /// <summary>
        /// List of Scripting Engine Templates.
        /// </summary>
        ITemplateData Templates { get; }

        /// <summary>
        /// List of Scripting Elements for the Template
        /// </summary>
        ITemplateElementData Elements { get; }

        /// <summary>
        /// List of Scripting Attributes for the Template
        /// </summary>
        ITemplateAttributeData Attributes { get; }

        /// <summary>
        /// List of Scripting Node/Attribute owners for the Template.
        /// </summary>
        ITemplateNodeOwnerData AttributeOwners { get; }

        /// <summary>
        /// List of Scripting Data Sources asscoated with a Templates.
        /// </summary>
        ITemplateInputData TemplateSources { get; }
    }

    class Template: ITemplate
    {
        /// <inheritdoc/>
        public ITemplateData Templates { get { return templateValues; } }
        TemplateData templateValues = new TemplateData();

        /// <inheritdoc/>
        public ITemplateElementData Elements { get { return templateElements; } }
        TemplateElementData templateElements = new TemplateElementData();

        /// <inheritdoc/>
        public ITemplateAttributeData Attributes { get { return templateAttributes; } }
        TemplateAttributeData templateAttributes = new TemplateAttributeData();

        /// <inheritdoc/>
        public ITemplateNodeOwnerData AttributeOwners { get { return templateNodeOwners; } }
        TemplateNodeOwnerData templateNodeOwners = new TemplateNodeOwnerData();

        /// <inheritdoc/>
        public ITemplateInputData TemplateSources { get { return templateSources; } }
        TemplateInputData templateSources = new TemplateInputData();

        /// <inheritdoc/>
        public Boolean RaiseListChangedEvents
        {
            get
            {
                return templateValues.RaiseListChangedEvents
                    && templateElements.RaiseListChangedEvents
                    && templateAttributes.RaiseListChangedEvents
                    && templateNodeOwners.RaiseListChangedEvents
                    && templateSources.RaiseListChangedEvents;
            }
            set
            {
                templateValues.RaiseListChangedEvents = value;
                templateElements.RaiseListChangedEvents = value;
                templateAttributes.RaiseListChangedEvents = value;
                templateNodeOwners.RaiseListChangedEvents = value;
                templateSources.RaiseListChangedEvents = value;
            }
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<DataTable> Export()
        {
            List<DataTable> work = new List<DataTable>();
            work.Add(templateValues.ToDataTable());
            work.Add(templateElements.ToDataTable());
            work.Add(templateAttributes.ToDataTable());
            work.Add(templateNodeOwners.ToDataTable());
            work.Add(templateSources.ToDataTable());
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public void Import(DataSet source)
        {
            templateValues.Load(GetTable(templateValues.BindingName));
            templateElements.Load(GetTable(templateElements.BindingName));
            templateAttributes.Load(GetTable(templateAttributes.BindingName));
            templateNodeOwners.Load(GetTable(templateNodeOwners.BindingName));
            templateSources.Load(GetTable(templateSources.BindingName));

            DataTableReader GetTable(String tableName)
            {
                if (source.Tables.Contains(tableName) && source.Tables[tableName] is DataTable sourceTable)
                { return sourceTable.CreateDataReader(); }
                else
                {
                    Exception ex = new IndexOutOfRangeException();
                    ex.Data.Add(nameof(tableName), tableName);
                    ex.Data.Add(nameof(source.Tables),
                        String.Join(",", source.Tables.OfType<DataTable>().Select(s => s.TableName)));
                    throw ex;
                }
            }
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, AppModel.IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Load(factory, dataKey));
            work.AddRange(templateElements.Load(factory, dataKey));
            work.AddRange(templateAttributes.Load(factory, dataKey));
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
            work.AddRange(templateElements.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(templateAttributes.Load(factory, dataKey, asOfUtcDate));
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
            work.AddRange(templateElements.Load(factory, dataKey));
            work.AddRange(templateAttributes.Load(factory, dataKey));
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
            work.AddRange(templateElements.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(templateAttributes.Load(factory, dataKey, asOfUtcDate));
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
            work.AddRange(templateElements.Save(factory, dataKey));
            work.AddRange(templateAttributes.Save(factory, dataKey));
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
            work.AddRange(templateElements.Save(factory, dataKey));
            work.AddRange(templateAttributes.Save(factory, dataKey));
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
            work.AddRange(templateElements.Delete(dataKey));
            work.AddRange(templateAttributes.Delete(dataKey));
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
            work.AddRange(templateElements.Delete(dataKey));
            work.AddRange(templateAttributes.Delete(dataKey));
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
            work.AddRange(templateElements.Delete());
            work.AddRange(templateAttributes.Delete());
            work.AddRange(templateNodeOwners.Delete());
            work.AddRange(templateSources.Delete());
            return work;
        }

        /// <inheritdoc/>
        public void Remove(AppModel.IModelIndex dataKey)
        {
            templateValues.Remove(dataKey);
            templateElements.Remove(dataKey);
            templateAttributes.Remove(dataKey);
            templateNodeOwners.Remove(dataKey);
            templateSources.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Remove(ITemplateIndex dataKey)
        {
            templateValues.Remove(dataKey);
            templateElements.Remove(dataKey);
            templateAttributes.Remove(dataKey);
            templateNodeOwners.Remove(dataKey);
            templateSources.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            templateValues.Clear();
            templateElements.Clear();
            templateAttributes.Clear();
            templateNodeOwners.Clear();
            templateSources.Clear();
        }

        /// <inheritdoc/>
        public void ResetBindings()
        {
            templateValues.ResetBindings();
            templateElements.ResetBindings();
            templateAttributes.ResetBindings();
            templateNodeOwners.ResetBindings();
            templateSources.ResetBindings();
        }
    }
}
