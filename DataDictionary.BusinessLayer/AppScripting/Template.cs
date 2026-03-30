using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using System.Data;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Interface for the Templates
    /// </summary>
    [Obsolete("Do not think this is needed")]
    public interface ITemplate :
        ILoadData<IModelIndex>, ISaveData<IModelIndex>,
        ILoadData<ITemplateIndex>, ISaveData<ITemplateIndex>,
        IDeleteData
    {
        /// <summary>
        /// Templates within the Model
        /// </summary>
        //ITemplateData Templates { get; }

        //TODO: Add rest of Template objects
    }

    [Obsolete("Do not think this is needed")]
    class Template : ITemplate, IDataTableFile
    {
        /// <inheritdoc/>
        public ITemplateData Templates { get { return templateValues; } }
        TemplateData templateValues;

        public Template() : base()
        {
            templateValues = new TemplateData();
        }

        #region ILoadData, ISaveData, IDeleteData
        /// <inheritdoc/>
        public void Clear()
        {
            templateValues.Clear();
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Delete(dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Delete(dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Delete());
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Load(factory, dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Load(factory, dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        public void Remove(IModelIndex dataKey)
        {
            templateValues.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Remove(ITemplateIndex dataKey)
        {
            templateValues.Remove(dataKey);
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Save(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Save(factory, dataKey));
            return work;
        }
        #endregion

        #region IDataTableFile
        /// <inheritdoc/>
        public IReadOnlyList<DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();
            result.AddRange(templateValues.Export());
            return result;

            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Import(DataSet source)
        {
            templateValues.Import(source);
        }


        #endregion
    }
}
