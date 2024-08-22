using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.BusinessLayer.NamedScope
{
    /// <summary>
    /// Interface for Data objects capable of generating NamedScope data.
    /// </summary>
    /// <remarks>
    /// This interface and the inherited interfaces are used with the internal data classes.
    /// They are not intended to be exposed to the UI layer.
    /// </remarks>
    interface INamedScopeSource
    {
        // These interfaces addresses language limitations on how interfaces & abstract classes are inherited and enforced.
        // These methods are hidden by the Data objects.
        // The Data objects do not expose the class, just an interface for the class.
        // In effect, this is used to implement multi-inheritance manually.
        // CS0737: Interface items must be public
        // CS0060: Base class must be at least as accessible then inherited classes.

        /// <summary>
        /// Get the NamedScope Pairs that are used to build the values for NameScope Data.
        /// </summary>
        /// <returns></returns>
        IEnumerable<NamedScopePair> GetNamedScopes();
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
    /// Class to build NamedScope Pairs (Parent Key and Value).
    /// </summary>
    /// <remarks>This is just for constructing a list of parameters needed to load the NamedScopeData.</remarks>
    class NamedScopePair
    {
        public DataLayerIndex? ParentKey { get; } = null;
        public NamedScopeValue Value { get; }

        public NamedScopePair(NamedScopeValue value)
        { this.Value = value; }

        public NamedScopePair(DataLayerIndex parent, NamedScopeValue value) : this(value)
        { this.ParentKey = parent; }

        /// <summary>
        /// Returns the intermediary NameSpaces for the NamedScope.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<NamedScopePair> CreateNameSpace()
        {
            //TODO: Move Logic to NamedScopeData.AddRange?
            // Might be able to remove duplicates that way.

            List<NamedScopePair> result = new List<NamedScopePair>();
            INamedScopeSourceValue source = this.Value.Source;
            NamedScopeValue nameScope = this.Value;

            NamedScopePath parentPath;
            if (nameScope.Path.Group().
                Where(w => !source.Path.Group().
                    Any(n => w.MemberFullPath.EndsWith(n.MemberFullPath))).
                OrderBy(o => o.MemberFullPath.Length).
                LastOrDefault() is NamedScopePath path)
            { parentPath = path; }
            else { parentPath = nameScope.Path; }

            DataLayerIndex parentIndex;
            if (this.ParentKey is null)
            { throw new ArgumentNullException(nameof(this.ParentKey)); }
            else { parentIndex = this.ParentKey; }

            List<NameSpaceSource> nodes = source.
                GetPath().
                Group().
                OrderBy(o => o.MemberFullPath.Length).
                Select(s => new NameSpaceSource(s)).
                ToList();

            foreach (NameSpaceSource node in nodes)
            {
                if (node == nodes.Last())
                {
                    result.Add(new NamedScopePair(
                        parentIndex, new NamedScopeValue(source)
                        { GetPath = () => new NamedScopePath(parentPath, source.GetPath().Member) }));
                }
                else
                {
                    result.Add(new NamedScopePair(
                        parentIndex, new NamedScopeValue(node)
                        { GetPath = () => new NamedScopePath(parentPath, node.GetPath().Member) }));

                    parentIndex = node.GetIndex();
                    parentPath = new NamedScopePath(parentPath, node.GetPath().Member);
                }
            }

            return result;
        }
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
        { return new DataLayerIndex() { BusinessLayerId = SystemId }; }

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
