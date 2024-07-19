using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// The NameSpace for a Subject
    /// </summary>
    public class SubjectNameSpaceValue : INamedScopeSourceValue
    {
        /// <summary>
        /// Id Used for the NameSpace
        /// </summary>
        public Guid NameSpaceId { get; } = Guid.NewGuid();

        /// <summary>
        /// Path for the NameSpace
        /// </summary>
        public NamedScopePath NameSpacePath { get; }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.ModelNameSpace;

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new DataLayerIndex() { BusinessLayerId = NameSpaceId }; }

        /// <inheritdoc/>
        public NamedScopePath GetPath()
        { return NameSpacePath; }

        /// <inheritdoc/>
        public String GetTitle()
        { return NameSpacePath.Member; }

        /// <summary>
        /// Constructor for the Subject NameSpace
        /// </summary>
        /// <param name="path"></param>
        public SubjectNameSpaceValue(NamedScopePath path) : base()
        { NameSpacePath = path; }
    }
}
