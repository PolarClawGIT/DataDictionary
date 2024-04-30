using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ScriptingData.Selection;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface component for the Scripting Engine Selection
    /// </summary>
    public interface ISelectionData :
        IBindingData<SelectionValue>
    { }


    class SelectionData : SelectionCollection<SelectionValue>, ISelectionData, IGetNamedScopes
    {
        /// <summary>
        /// Reference to the containing ScriptingEngine
        /// </summary>
        public required ScriptingEngine Scripting { get; init; }

        /// <inheritdoc/>
        /// <remarks>Selection</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ISelectionKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Selection</remarks>

        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Selection</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ISelectionKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Selection</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Selection</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        { return this.Select(s => new NamedScopePair(s)); }
    }
}
