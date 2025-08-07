using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using System;
using System.Collections.Generic;
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
    }

    class DataSource : IDataSource
    {
        /// <inheritdoc/>
        public IDataSourceData DataSources { get { return sourceValues; } }
        DataSourceData sourceValues = new DataSourceData();

        /// <inheritdoc/>
        public IDataObjectData DataObjects { get { return sourceObjects; } }
        DataObjectData sourceObjects = new DataObjectData();

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public IReadOnlyList<DataTable> Export()
        {
            List<DataTable> work = new List<DataTable>();
            work.Add(sourceValues.ToDataTable());
            work.Add(sourceObjects.ToDataTable());
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>DataSource</remarks>
        public void Import(DataSet source)
        {
            sourceValues.Load(GetTable(sourceValues.BindingName));
            sourceObjects.Load(GetTable(sourceObjects.BindingName));

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
    }
}
