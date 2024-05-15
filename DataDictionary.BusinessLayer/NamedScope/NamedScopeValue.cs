using DataDictionary.DataLayer.ApplicationData.Scope;

namespace DataDictionary.BusinessLayer.NamedScope
{
    /// <summary>
    /// Interface for the NamedScope Value.
    /// </summary>
    public interface INamedScopeValue : IScopeKey
    {
        /// <summary>
        /// The NamedPath for the Value
        /// </summary>
        NamedScopePath NamedPath { get; }

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
    /// Value for a NamedScope
    /// </summary>
    public abstract class NamedScopeValue : INamedScopeValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get; init; } = ScopeType.Null;

        /// <inheritdoc/>
        public virtual NamedScopePath NamedPath { get; protected set; } = new NamedScopePath();

        /// <inheritdoc/>
        public virtual String Title { get; protected set; } = String.Empty;

        /// <inheritdoc/>
        public virtual Int32 OrdinalPosition { get; init; } = 0;
    }

    /// <summary>
    /// Internal structure of a NamedScopeValue
    /// </summary>
    class NamedScopeValueCore : NamedScopeValue, IOnTitleChanged
    {
        /// <summary>
        /// Internal Index for the NameScopeValue
        /// </summary>
        public NamedScopeIndex Index = new NamedScopeIndex(Guid.NewGuid());

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
            init { getPath = value; NamedPath = value(); }
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
        public NamedScopeValueCore(INamedScopeSourceValue source) : base()
        {
            Scope = source.Scope;
            Source = source;
            Title = source.Title;
            GetPath = source.GetPath;
        }

        /// <summary>
        /// Updates the Title and Path of the NamedScope Value.
        /// </summary>
        /// <remarks>
        /// Call this method when the source title or path changes.
        /// Triggers OnTitleChanged
        /// Can be Triggered by Source, such as when hooked up to PropertyChanged.
        /// </remarks>
        public void TitleChanged()
        {
            Title = Source.Title;
            NamedPath = GetPath();

            if (OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
    }
}
