using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.NameSpace;
using DataDictionary.DataLayer;
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
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDataTableFile
    {
        /// <summary>
        /// Reference to the containing Model
        /// </summary>
        public required IModelData<ModelValue> Models { get; init; }

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

        /// <inheritdoc/>
        /// <remarks>Model</remarks>
        public IReadOnlyList<WorkItem> Build(INamedScopeDictionary target)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem()
            {
                WorkName = "Build NamedScope Subject Areas",
                DoWork = () =>
                {
                    if (Models.FirstOrDefault() is IModelItem model)
                    {
                        ModelKey modelKey = new ModelKey(model);

                        foreach (ModelSubjectAreaItem item in this.OrderBy(o => new NameSpaceKey(o).MemberPath))
                        {
                            target.Remove(new NamedScopeKey(item));
                            target.Add(new NamedScopeItem(modelKey, item));
                        }

                        //TODO: Need to build this as a hierarchy, like how help subjects are done.
                        /*
                        var nameList = this.Select(s => new NameSpaceItem(new NameSpaceKey(s))).
                            GroupBy(g => g.MemberPath).
                            OrderBy(o => o.Key).
                            ToList();

                        foreach (ModelSubjectAreaItem item in this.OrderBy(o => new NameSpaceKey(o).MemberPath))
                        {
                            target.Remove(new NamedScopeKey(item));
                            NameSpaceKey nameSpaceKey = new NameSpaceKey(item);

                            if (this.FirstOrDefault(w => new NameSpaceKey(w).MemberFullName == nameSpaceKey.MemberPath) is ModelSubjectAreaItem parent)
                            { target.Add(new NamedScopeItem(parent, item)); }

                            // Handle name not in list

                            else { target.Add(new NamedScopeItem(modelKey, item)); }*/
                    }
                }

            });

            return work;
        }

    }
}
