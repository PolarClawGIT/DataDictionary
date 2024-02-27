using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ModelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Model
{
    /// <summary>
    /// Interface component for the Domain Model 
    /// </summary>
    public interface IModelData :
        IBindingData<ModelItem>
    {
        /// <summary>
        /// Create WorkItem that create a new Model instance.
        /// </summary>
        /// <returns></returns>
        IEnumerable<WorkItem> Create();
    }

    class ModelData : ModelCollection, IModelData,
        ILoadData<IModelKey>, ISaveData<IModelKey>, IDataTableFile
    {
        /// <inheritdoc/>
        /// <remarks>Model</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Model</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Model</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        { return this.ToDataTable().ToList(); ; }

        /// <inheritdoc/>
        /// <remarks>Model</remarks>
        public void Import(System.Data.DataSet source)
        { Load(source); }

        public IReadOnlyList<WorkItem> Remove()
        { return new WorkItem() { WorkName = "Remove Model", DoWork = () => { Clear(); } }.ToList(); }

        public IEnumerable<WorkItem> Create()
        { return new WorkItem() { WorkName = "Create Model", DoWork = () => { Add(new ModelItem()); } }.ToList(); }
    }
}
