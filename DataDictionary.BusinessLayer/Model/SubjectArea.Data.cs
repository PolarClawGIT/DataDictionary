using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.DataLayer.ModelData.SubjectArea;
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
        IDataTableFile, INamedScopeSource
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

        /// <inheritdoc/>
        /// <remarks>SubjectArea</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        {
            List<NamedScopePair> result = new List<NamedScopePair>();

            DataLayerIndex parentIndex;
            if (Models.FirstOrDefault() is ModelValue model)
            { parentIndex = model.GetIndex(); }
            else { throw new InvalidOperationException("Could not find the Model"); }

            foreach (SubjectAreaValue item in this)
            {
                NamedScopeValue value = new NamedScopeValue(item);
                result.Add(new NamedScopePair(parentIndex, value));
            }

            return result;
        }

        [Obsolete]
        public IEnumerable<NamedScopePair> GetNamedScopes_Old()
        {
            List<NamedScopePair> result = new List<NamedScopePair>();

            ModelValue? model = Models.FirstOrDefault();
            List<NameSpaceSource> nodes = NamedScopePath.Group(this.Select(s => s.GetPath())).OrderBy(o => o.MemberFullPath.Length).Select(s => new NameSpaceSource(s)).ToList();

            foreach (NameSpaceSource item in nodes)
            {
                SubjectAreaValue? subject = this.FirstOrDefault(w => item.GetPath().Equals(w.GetPath()));
                SubjectAreaValue? parentSubject = this.FirstOrDefault(w => item.GetPath().ParentPath is NamedScopePath subjectPath && subjectPath.Equals(w.GetPath()));
                NameSpaceSource? parentNode = nodes.FirstOrDefault(w => item.GetPath().ParentPath is NamedScopePath nodePath && nodePath.Equals(w.GetPath()));

                if (parentSubject is not null && subject is not null)
                { result.Add(new NamedScopePair(parentSubject.GetIndex(), GetValue(subject))); }

                else if (parentNode is not null && subject is not null)
                { result.Add(new NamedScopePair(parentNode.GetIndex(), GetValue(subject))); }

                else if (model is not null && subject is not null)
                { result.Add(new NamedScopePair(model.GetIndex(), GetValue(subject))); }

                else if (parentSubject is not null && subject is null)
                { result.Add(new NamedScopePair(parentSubject.GetIndex(), new NamedScopeValue(item))); }

                else if (parentNode is not null && subject is null)
                { result.Add(new NamedScopePair(parentNode.GetIndex(), new NamedScopeValue(item))); }

                else if (model is not null && subject is null)
                { result.Add(new NamedScopePair(model.GetIndex(), new NamedScopeValue(item))); }

                else { result.Add(new NamedScopePair(new NamedScopeValue(item))); }
            }

            return result;

            NamedScopeValue GetValue(SubjectAreaValue source)
            {
                NamedScopeValue result = new NamedScopeValue(source);
                source.PropertyChanged += Source_PropertyChanged;

                return result;

                void Source_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
                {
                    if (e.PropertyName is nameof(source.SubjectAreaTitle) or nameof(source.SubjectName))
                    { result.TitleChanged(); }
                }
            }
        }

    }
}
