using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.NamedScope
{
    /// <summary>
    /// Interface for the NamedScope Value.
    /// </summary>
    public interface INamedScopeValue : IScopeType, IOnTitleChanged, IPathIndex
    {
        /// <summary>
        /// The Index for the NamedScopeValue.
        /// </summary>
        NamedScopeIndex Index { get; }

        /// <summary>
        /// The Title for the Value
        /// </summary>
        String Title { get; }

        /// <summary>
        /// The Position to place the value. Overrides order by Title.
        /// </summary>
        Int32 OrdinalPosition { get; }

        /// <summary>
        /// The Data used to create the NamedScope Value
        /// </summary>
        INamedScopeSourceValue Source { get; }
    }


    /// <summary>
    /// Internal structure of a NamedScopeValue
    /// </summary>
    class NamedScopeValue : INamedScopeValue
    {
        /// <inheritdoc/>
        public NamedScopeIndex Index { get; } = new NamedScopeIndex(Guid.NewGuid());

        /// <inheritdoc/>
        public ScopeType Scope { get; init; } = ScopeType.Null;

        /// <inheritdoc/>
        public virtual PathIndex Path { get; protected set; } = new PathIndex();

        /// <inheritdoc/>
        public virtual String Title { get; protected set; } = String.Empty;

        /// <inheritdoc/>
        public virtual Int32 OrdinalPosition { get; init; } = 0;

        /// <summary>
        /// Get the current Path of the Value
        /// </summary>
        /// <remarks>
        /// Allows for overriding how Path is created.
        /// Path is updated when GetPath is set or on TitleChanged is called.
        /// </remarks>
        public Func<PathIndex> GetPath
        {
            get { return getPath; }
            init { getPath = value; Path = value(); }
        }
        Func<PathIndex> getPath = () => new PathIndex();

        /// <summary>
        /// Get the current Title of the Value
        /// </summary>
        /// <remarks>
        /// Allows for overriding how Title is created.
        /// Title is updated when GetTitle is set or on TitleChanged is called.
        /// </remarks>
        public Func<String> GetTitle
        {
            get { return getTitle; }
            init { getTitle = value; Title = value(); }
        }
        Func<String> getTitle = () => String.Empty;

        /// <inheritdoc/>
        public INamedScopeSourceValue Source { get; }

        /// <summary>
        /// Constructor for NamedScope Value.
        /// </summary>
        /// <param name="source"></param>
        public NamedScopeValue(INamedScopeSourceValue source) : base()
        {
            Scope = source.Scope;
            Source = source;
            GetTitle = source.GetTitle;
            GetPath = source.GetPath;

            if (source is IDbColumnPosition position)
            { OrdinalPosition = position.OrdinalPosition ?? 0; }

            if (source is IBindingPropertyChanged propertyChanged)
            { propertyChanged.PropertyChanged += PropertyChanged_PropertyChanged; }

            void PropertyChanged_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
            {
                if (source.IsTitleChanged(e) && OnTitleChanged is EventHandler handler)
                {
                    Title = GetTitle();
                    Path = GetPath();
                    handler(this, EventArgs.Empty);
                }
            }
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

        public override String ToString()
        { return Title; }
    }
}
