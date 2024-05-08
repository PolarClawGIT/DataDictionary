using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.NameSpace;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Scope;
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

        public IReadOnlyList<WorkItem> Remove()
        { return new WorkItem() { WorkName = "Remove Subject Area", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>SubjectArea</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        {
            List<NamedScopePair> result = new List<NamedScopePair>();

            ModelValue? model = Models.FirstOrDefault();
            List<SubjectNameSpace> nodes = NamedScopePath.Group(this.Select(s => s.GetPath())).Select(s => new SubjectNameSpace(s)).ToList();

            foreach (SubjectNameSpace item in nodes)
            {
                SubjectAreaValue? subject = this.FirstOrDefault(w => item.GetPath().Equals(w.GetPath()));
                SubjectAreaValue? parentSubject = this.FirstOrDefault(w => item.ParentPath is not null && item.ParentPath.Equals(w.GetPath()));
                SubjectNameSpace? parentNode = nodes.FirstOrDefault(w => item.ParentPath is not null && item.ParentPath.Equals(w.GetPath()));

                if (parentSubject is not null && subject is not null)
                { result.Add(new NamedScopePair(parentSubject.GetKey(), subject)); }

                else if (parentNode is not null && subject is not null)
                { result.Add(new NamedScopePair(parentNode.GetKey(), subject)); }

                else if (model is not null && subject is not null)
                { result.Add(new NamedScopePair(model.GetKey(), subject)); }

                else if (parentSubject is not null && subject is null)
                { result.Add(new NamedScopePair(parentSubject.GetKey(), item)); }

                else if (parentNode is not null && subject is null)
                { result.Add(new NamedScopePair(parentNode.GetKey(), item)); }

                else if (model is not null && subject is null)
                { result.Add(new NamedScopePair(model.GetKey(), item)); }

                else { result.Add(new NamedScopePair(item)); }
            }

            return result;
        }

        /// <summary>
        /// Represents NameSpace items within the Subject that do not have Subject associated with them.
        /// </summary>
        class SubjectNameSpace : INamedScopeValue
        {
            protected Guid SystemId;
            protected NamedScopePath SystemPath;

            public ScopeType Scope { get; } = ScopeType.ModelNameSpace;

            public String Title { get { return SystemPath.Member; } }

            public String NameSpace { get { return SystemPath.MemberFullPath; } }

            public NamedScopePath? ParentPath { get { return SystemPath.ParentKey; } }

            public event EventHandler? OnTitleChanged;

            public NamedScopeKey GetKey()
            { return new NamedScopeKey(SystemId); }

            public NamedScopePath GetPath()
            { return SystemPath; }

            public String GetTitle()
            { return SystemPath.Member; }

            public SubjectNameSpace(NamedScopePath path)
            {
                SystemId = Guid.NewGuid();
                SystemPath = path;
            }

            public override String ToString()
            { return SystemPath.MemberFullPath; }
        }
    }
}
