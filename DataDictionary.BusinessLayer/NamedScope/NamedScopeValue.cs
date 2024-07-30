using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.NamedScope
{
    /// <summary>
    /// Interface for the NamedScope Value.
    /// </summary>
    public interface INamedScopeValue : IScopeType, IOnTitleChanged
    {
        /// <summary>
        /// The Index for the NamedScopeValue.
        /// </summary>
        NamedScopeIndex Index { get; }

        /// <summary>
        /// The NamedPath for the Value
        /// </summary>
        NamedScopePath Path { get; }

        /// <summary>
        /// The Title for the Value
        /// </summary>
        String Title { get; }

        /// <summary>
        /// The Position to place the value. Overrides order by Title.
        /// </summary>
        Int32 OrdinalPosition { get; }
    }


    /// <summary>
    /// Internal structure of a NamedScopeValue
    /// </summary>
    class NamedScopeValue : INamedScopeValue, INamedScopeSourceValue
    {
        /// <inheritdoc/>
        public NamedScopeIndex Index { get; } = new NamedScopeIndex(Guid.NewGuid());

        /// <inheritdoc/>
        public ScopeType Scope { get; init; } = ScopeType.Null;

        /// <inheritdoc/>
        public virtual NamedScopePath Path { get; protected set; } = new NamedScopePath();

        /// <inheritdoc/>
        public virtual String Title { get; protected set; } = String.Empty;

        /// <inheritdoc/>
        public virtual Int32 OrdinalPosition { get; init; } = 0;

        /// <summary>
        /// Get the current Path of the Value
        /// </summary>
        /// <remarks>
        /// Allows for overriding how NamedPath is created.
        /// NamedPath is updated when GetPath is set or on TitleChanged is called.
        /// </remarks>
        public Func<NamedScopePath> GetPath
        {
            get { return getPath; }
            init { getPath = value; Path = value(); }
        }
        Func<NamedScopePath> getPath = () => new NamedScopePath();

        /// <summary>
        /// The Data used to create the NamedScope Value
        /// </summary>
        public INamedScopeSourceValue Source { get; }

        /// <summary>
        /// Constructor for NamedScope Value.
        /// </summary>
        /// <param name="source"></param>
        public NamedScopeValue(INamedScopeSourceValue source) : base()
        {
            Scope = source.Scope;
            Source = source;
            Title = source.Title;
            GetPath = source.GetPath;
        }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;

        /// <summary>
        /// Trigger the OnTitleChanged event.
        /// </summary>
        public virtual void TitleChanged()
        {
            if (OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }

        public DataLayerIndex GetIndex()
        { return new DataLayerIndex() { BusinessLayerId = Index.NamedScopeId }; }

        public String GetTitle()
        { return Title; }

        /// <inheritdoc/>
        NamedScopePath INamedScopeSourceValue.GetPath()
        { return this.GetPath(); }
    }
}
