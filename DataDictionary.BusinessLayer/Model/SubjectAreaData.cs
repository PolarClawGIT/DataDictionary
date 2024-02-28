using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NameScope;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.DataLayer.ModelData.SubjectArea;
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
    /// Interface component for the Model SubjectArea
    /// </summary>
    public interface ISubjectAreaData :
        IBindingData<ModelSubjectAreaItem>,
        ILoadData<IModelSubjectAreaKey>, ISaveData<IModelSubjectAreaKey>
    { }

    class SubjectAreaData : ModelSubjectAreaCollection, ISubjectAreaData,
        ILoadData<IModelKey>, ISaveData<IModelKey>, IDataTableFile,
        INameScopeData<IModelKey>
    {
        /// <inheritdoc/>
        /// <remarks>SubjectArea</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelSubjectAreaKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>SubjectArea</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>SubjectArea</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelSubjectAreaKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>SubjectArea</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>SubjectArea</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        { return this.ToDataTable().ToList(); ; }


        /// <inheritdoc/>
        /// <remarks>SubjectArea</remarks>
        public void Import(System.Data.DataSet source)
        { Load(source); }

        public IReadOnlyList<WorkItem> Remove()
        { return new WorkItem() { WorkName = "Remove Subject Area", DoWork = () => { Clear(); } }.ToList(); }

        public IReadOnlyList<WorkItem> Export(IList<NameScopeItem> target, Func<IModelKey?> parent)
        {
            return new WorkItem()
            {
                WorkName = "Load NameScope, Subject Areas",
                DoWork = () =>
                {
                    if (parent() is IModelKey modelKey)
                    {
                        foreach (ModelSubjectAreaItem item in this)
                        { target.Add(new NameScopeItem(modelKey, item)); }
                    }
                }
            }.ToList();
        }
    }
}
