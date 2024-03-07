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
        ILoadData<IModelKey>, ISaveData<IModelKey>, IDataTableFile,
        INamedScopeData<IModelKey>
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

        public IReadOnlyList<WorkItem> Export(IList<NamedScopeItem> target, Func<IModelKey?> parent)
        {

            return new WorkItem()
            {
                WorkName = "Load NameScope, Subject Areas",
                DoWork = BuildList
            }.ToList();

            void BuildList()
            {
                List<NameSpaceKey> grouping = BuildNameSpace(this.Select(s => new NameSpaceKey(s))).Distinct().ToList();
                List<NameSpaceItem> nameSpaces = new List<NameSpaceItem>();

                if (parent() is IModelKey modelKey)
                {
                    foreach (NameSpaceKey item in grouping)
                    {
                        List<ModelSubjectAreaItem> currentSubject = this.Where(w => item.Equals(new NameSpaceKey(w))).ToList();
                        ModelSubjectAreaItem? parentSubject = this.FirstOrDefault(w => item.ParentKey is not null && item.ParentKey.Equals(new NameSpaceKey(w)));
                        NameSpaceItem? parentNameSpace = nameSpaces.FirstOrDefault(w => item.ParentKey is not null && item.ParentKey.Equals(w));

                        if (currentSubject.Count == 0 && parentNameSpace is null && parentSubject is null)
                        {
                            NameSpaceItem newNameSpace = new NameSpaceItem(item);

                            nameSpaces.Add(newNameSpace);
                            target.Add(new NamedScopeItem(modelKey, newNameSpace));
                        }
                        else
                        if (currentSubject.Count == 0 && parentNameSpace is null && parentSubject is not null)
                        {
                            NameSpaceItem newNameSpace = new NameSpaceItem(item);

                            nameSpaces.Add(newNameSpace);
                            target.Add(new NamedScopeItem(parentSubject, newNameSpace));
                        }
                        else
                        if (currentSubject.Count == 0 && parentNameSpace is not null && parentSubject is null)
                        {
                            NameSpaceItem newNameSpace = new NameSpaceItem(item);

                            nameSpaces.Add(newNameSpace);
                            target.Add(new NamedScopeItem(parentNameSpace, newNameSpace));
                        }
                        else
                        if (currentSubject.Count == 0 && parentNameSpace is not null && parentSubject is not null)
                        {
                            NameSpaceItem newNameSpace = new NameSpaceItem(item);

                            nameSpaces.Add(newNameSpace);
                            target.Add(new NamedScopeItem(parentNameSpace, newNameSpace));
                        }
                        else
                        if (currentSubject.Count > 0 && parentNameSpace is null && parentSubject is null)
                        {
                            foreach (ModelSubjectAreaItem current in currentSubject)
                            { target.Add(new NamedScopeItem(modelKey, current)); }
                        }
                        else
                        if (currentSubject.Count > 0 && parentNameSpace is null && parentSubject is not null)
                        {
                            foreach (ModelSubjectAreaItem current in currentSubject)
                            { target.Add(new NamedScopeItem(parentSubject, current)); }
                        }
                        else
                        if (currentSubject.Count > 0 && parentNameSpace is not null && parentSubject is null)
                        {
                            foreach (ModelSubjectAreaItem current in currentSubject)
                            { target.Add(new NamedScopeItem(parentNameSpace, current)); }
                        }
                        else
                        if (currentSubject.Count > 0 && parentNameSpace is not null && parentSubject is not null)
                        {
                            foreach (ModelSubjectAreaItem current in currentSubject)
                            { target.Add(new NamedScopeItem(parentSubject, current)); }
                        }
                    }

                }

                IEnumerable<NameSpaceKey> BuildNameSpace(IEnumerable<NameSpaceKey> group)
                {
                    List<NameSpaceKey> result = new List<NameSpaceKey>();
                    List<NameSpaceKey> data = group.Select(s => s.ParentKey).OfType<NameSpaceKey>().Distinct().ToList();

                    if (data.Count > 0) { result.AddRange(BuildNameSpace(data)); }

                    result.AddRange(group.Distinct());

                    return result;
                }
            }
        }
    }
}
