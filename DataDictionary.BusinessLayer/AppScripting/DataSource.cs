using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Interface representing Scripting Data Source
    /// </summary>
    public interface IDataSource :
        ILoadData<ITemplateIndex>,
        ILoadData<IDataSourceIndex>, ISaveData<IDataSourceIndex>, IDeleteData<IDataSourceIndex>,
        ILoadData<AppModel.IModelIndex>, ISaveData<AppModel.IModelIndex>,
        IGetTemporal<AppModel.IModelIndex>, IGetTemporal<IDataSourceIndex>,
        IBindListChanged
    {
        /// <summary>
        /// List of Scripting Data Sources
        /// </summary>
        IDataSourceData DataSources { get; }

        /// <summary>
        /// List of Scripting Data Objects within a Data Source.
        /// </summary>
        IDataObjectData DataObjects { get; }

        /// <summary>
        /// Creates an empty instance of IDataSource
        /// </summary>
        public static IDataSource Create()
        { return new DataSource(); }
    }

    class DataSource : IDataSource
    {
        /// <inheritdoc/>
        public IDataSourceData DataSources { get { return sourceValues; } }
        DataSourceData sourceValues = new DataSourceData();

        /// <inheritdoc/>
        public IDataObjectData DataObjects { get { return sourceObjects; } }
        DataObjectData sourceObjects = new DataObjectData();

        public DataSource() : base()
        {
            sourceValues.ListChanged += OnListChanged;
            sourceObjects.ListChanged += OnListChanged;

            void OnListChanged(Object? sender, ListChangedEventArgs e)
            {
                if (ListChanged is ListChangedEventHandler handler)
                { handler(sender, e); }
            }
        }

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public IReadOnlyList<DataTable> Export()
        {
            List<DataTable> result = new List<DataTable>();
            result.Add(sourceValues.ToDataTable());
            result.Add(sourceObjects.ToDataTable());
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public void Import(DataSet source)
        {
            sourceValues.Load(source);
            sourceObjects.Load(source);
        }

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, AppModel.IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sourceValues.Load(factory, dataKey));
            work.AddRange(sourceObjects.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, AppModel.IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sourceValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(sourceObjects.Load(factory, dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDataSourceIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sourceValues.Load(factory, dataKey));
            work.AddRange(sourceObjects.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDataSourceIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sourceValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(sourceObjects.Load(factory, dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sourceValues.Load(factory, dataKey));
            work.AddRange(sourceObjects.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sourceValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(sourceObjects.Load(factory, dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, AppModel.IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sourceValues.Save(factory, dataKey));
            work.AddRange(sourceObjects.Save(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDataSourceIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sourceValues.Save(factory, dataKey));
            work.AddRange(sourceObjects.Save(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public IReadOnlyList<WorkItem> Delete(AppModel.IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sourceValues.Delete(dataKey));
            work.AddRange(sourceObjects.Delete(dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public IReadOnlyList<WorkItem> Delete(IDataSourceIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sourceValues.Delete(dataKey));
            work.AddRange(sourceObjects.Delete(dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public IReadOnlyList<WorkItem> Delete(ITemplateIndex dataKey)
        { throw new InvalidOperationException("Delete by Template not supported"); }

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sourceValues.Delete());
            work.AddRange(sourceObjects.Delete());
            return work;
        }

        /// <inheritdoc/>
        public ITemporalData GetTemporal(IDataSourceIndex key)
        { return sourceValues.GetTemporal(key); }

        /// <inheritdoc/>
        public ITemporalData GetTemporal(AppModel.IModelIndex key)
        { return sourceValues.GetTemporal(key); }

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public void Remove(AppModel.IModelIndex dataKey)
        {
            sourceValues.Remove(dataKey);
            sourceObjects.Remove(dataKey);
        }

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public void Remove(IDataSourceIndex dataKey)
        {
            sourceValues.Remove(dataKey);
            sourceObjects.Remove(dataKey);
        }

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public void Remove(ITemplateIndex dataKey)
        { throw new InvalidOperationException("Remove by Template not supported"); }

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public void Clear()
        {
            sourceValues.Clear();
            sourceObjects.Clear();
        }

        #region IBindListChanged
        /// <inheritdoc/>
        public event ListChangedEventHandler? ListChanged;

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public void ResetBindings()
        {
            sourceValues.ResetBindings();
            sourceObjects.ResetBindings();
        }


        /// <inheritdoc/>
        public Boolean RaiseListChangedEvents
        {
            get
            {
                return sourceValues.RaiseListChangedEvents
                    && sourceObjects.RaiseListChangedEvents;
            }
            set
            {
                sourceValues.RaiseListChangedEvents = value;
                sourceObjects.RaiseListChangedEvents = value;
            }
        }
        #endregion

        public IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope, Func<DataSourceValue, INamedScopeSourceValue?>? getParent)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(NameSpaceSource.Load<DataSourceData, DataSourceValue>(sourceValues, addNamedScope, getParent));
            return work;
        }

    }
}
