﻿using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.NamedScope
{
    /// <summary>
    /// Interface for the NamedScope Value.
    /// </summary>
    public interface INamedScopeValue : IScopeKey, IOnTitleChanged
    {
        /// <summary>
        /// The Index for the NamedScopeValue.
        /// </summary>
        NamedScopeIndex Index { get; }

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
        public NamedScopeIndex Index { get; } = new NamedScopeIndex(Guid.NewGuid());

        /// <inheritdoc/>
        public ScopeType Scope { get; init; } = ScopeType.Null;

        /// <inheritdoc/>
        public virtual NamedScopePath NamedPath { get; protected set; } = new NamedScopePath();

        /// <inheritdoc/>
        public virtual String Title { get; protected set; } = String.Empty;

        /// <inheritdoc/>
        public virtual Int32 OrdinalPosition { get; init; } = 0;

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
    }

    /// <summary>
    /// Internal structure of a NamedScopeValue
    /// </summary>
    class NamedScopeValueCore : NamedScopeValue
    {
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

        /// <inheritdoc/>
        /// <remarks>
        /// Title and NamedPath are updated.
        /// May be called by Source of the Value.
        /// </remarks>
        public override void TitleChanged()
        {
            base.TitleChanged();

            Title = Source.Title;
            NamedPath = GetPath();
        }
    }
}
