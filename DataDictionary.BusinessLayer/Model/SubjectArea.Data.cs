using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.Domain;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ModelData;
using System.ComponentModel;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Model
{
    /// <summary>
    /// Interface component for the Model SubjectArea
    /// </summary>
    public interface ISubjectAreaData : IBindingData<SubjectAreaValue>
    { }

    class SubjectAreaData : ModelSubjectAreaCollection<SubjectAreaValue>, ISubjectAreaData,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDataTableFile
    {
        /// <summary>
        /// Reference to the containing Model
        /// </summary>
        public required IModelData Models { get; init; }

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

        /// <inheritdoc/>
        /// <remarks>SubjectArea</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Subject Area", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>SubjectArea</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }

        /// <summary>
        /// Creates WorkItems that invoke a method to add Subjects to NamedScopes.
        /// </summary>
        /// <param name="addNamedScope"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> AddNamedScopes(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progressChanged = (completed, total) => { };

            WorkItem newWork = new WorkItem(ref progressChanged)
            {
                WorkName = "Adding NamedScopes (Subject Area)",
                DoWork = () =>
                {
                    Int32 completed = 0;
                    Int32 total = this.Count();

                    ModelValue? model = Models.FirstOrDefault();
                    foreach (SubjectAreaValue subject in this)
                    {
                        NamedScopeValue newItem = new NamedScopeValue(subject);
                        addNamedScope(model, newItem);
                        progressChanged(completed++, total);
                    }
                }
            };

            work.Add(newWork);

            return work;
        }
    }
}
