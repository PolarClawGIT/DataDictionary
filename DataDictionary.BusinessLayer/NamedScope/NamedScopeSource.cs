using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.NamedScope
{
    /// <summary>
    /// Interface for Data objects capable of generating NamedScope data.
    /// </summary>
    /// <remarks>
    /// This interface and the inherited interfaces are used with the internal data classes.
    /// They are not intended to be exposed to the UI layer.
    /// </remarks>
    interface INamedScopeSourceData
    {
        // These interfaces addresses language limitations on how interfaces & abstract classes are inherited and enforced.
        // These methods are hidden by the Data objects.
        // The Data objects do not expose the class, just an interface for the class.
        // In effect, this is used to implement multi-inheritance manually.
        // CS0737: Interface items must be public
        // CS0060: Base class must be at least as accessible then inherited classes.

        /// <summary>
        /// Creates WorkItems that invoke a method to add items to NamedScopes.
        /// </summary>
        /// <param name="addNamedScope"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope);

        //TODO: Update all object to use this method.
        //TODO: Remove old version

        /// <summary>
        /// Creates WorkItems that invoke a method to add items to NamedScopes, generic version.
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="data">A list of TValue</param>
        /// <param name="addNamedScope">Action that loads the Named Scope item. This is: NamedScopeData.Add.</param>
        /// <param name="getParent">Function that returns the Parent for the given TValue. Example: </param>
        /// <returns></returns>
        /// <remarks>
        /// This covers the most common use case.
        /// Each TValue in TData has a simple parent/child relationship.
        /// </remarks>
        static IReadOnlyList<WorkItem> LoadNamedScope<TData, TValue>(
            TData data, 
            Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope,
            Func<TValue, INamedScopeSourceValue?>? getParent = null)
            where TValue : INamedScopeSourceValue
            where TData : IList<TValue>, INamedScopeSourceData
        { 
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progressChanged = (completed, total) => { }; // Progress function.

            work.Add(new WorkItem(ref progressChanged)
            {
                WorkName = String.Format("Adding NamedScopes ({0})", typeof(TData).Name),
                DoWork = () =>
                {
                    Int32 completed = 0;
                    Int32 total = data.Count();
                    foreach (TValue item in data)
                    {
                        INamedScopeSourceValue? parent = null;
                        if(getParent is not null) { parent = getParent(item); }

                        NamedScopeValue newItem = new NamedScopeValue(item);
                        addNamedScope(parent, newItem);

                        progressChanged(completed++, total);
                    }
                }
            });

            return work;
        }
    }

    /// <summary>
    /// Interface for the NamedScope Source Value.
    /// </summary>
    /// <remarks>
    /// Returned to the UI layer.
    /// </remarks>
    public interface INamedScopeSourceValue : IScopeType
    {
        // Properties and Methods with default implementation are "hidden" in classes that inherit the interface.

        /// <summary>
        /// Index of the Source Value.
        /// </summary>
        DataLayerIndex Index { get { return GetIndex(); } }

        /// <summary>
        /// Title of the Source Value.
        /// </summary>
        String Title { get { return GetTitle(); } }

        /// <summary>
        /// Path of the Source Value.
        /// </summary>
        /// <remarks>This may not be a complete path.</remarks>
        NamedScopePath Path { get { return GetPath(); } }

        /// <summary>
        /// Gets the generic DataLayer Index from  the Value
        /// </summary>
        /// <returns></returns>
        DataLayerIndex GetIndex();

        /// <summary>
        /// Gets the generic Title from the Value
        /// </summary>
        /// <returns></returns>
        String GetTitle();

        /// <summary>
        /// Gets the generic NameScope Path from the Value
        /// </summary>
        /// <returns></returns>
        NamedScopePath GetPath();

        /// <summary>
        /// Condition to test if the Title or Path changed when the PropertyChanged event occurs.
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        Boolean IsTitleChanged(PropertyChangedEventArgs eventArgs);
    }

    /// <summary>
    /// Represents NameSpace items that does not have a specific Scope for the node.
    /// </summary>
    class NameSpaceSource : INamedScopeSourceValue
    {
        protected Guid SystemId;
        protected NamedScopePath SystemPath;
        public ScopeType Scope { get; } = ScopeType.ModelNameSpace;

        public DataLayerIndex GetIndex()
        { return new DataLayerIndex() { DataLayerId = SystemId }; }

        public NamedScopePath GetPath()
        { return SystemPath; }

        public String GetTitle()
        { return SystemPath.Member; }

        public NameSpaceSource(NamedScopePath path)
        {
            SystemId = Guid.NewGuid();
            SystemPath = path;
        }

        public override String ToString()
        { return SystemPath.MemberFullPath; }

        public Boolean IsTitleChanged(PropertyChangedEventArgs eventArgs)
        { return false; }
    }
}
