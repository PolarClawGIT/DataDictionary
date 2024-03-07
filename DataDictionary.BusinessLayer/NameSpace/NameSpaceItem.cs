using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Scope;
using System.ComponentModel;

namespace DataDictionary.BusinessLayer.NameSpace
{
    /// <summary>
    /// Interface for a NameSpace item within a NameScope
    /// </summary>
    public interface INameSpaceItem : INamedScopeKey, INameSpaceKey, IScopeKey
    { }

    /// <summary>
    /// Implementation for a NameSpace item within a NameScope
    /// </summary>
    public class NameSpaceItem : NameSpaceKey, INameSpaceItem, INotifyPropertyChanged
    {
        /// <inheritdoc/>
        public Guid SystemId { get; } = Guid.NewGuid();

        /// <inheritdoc/>
        public virtual ScopeType Scope
        {
            get { return scope; }
            set
            {
                scope = value;
                OnPropertyChanged(nameof(Scope));
            }
        }
        private ScopeType scope;

        /// <inheritdoc/>
        public override string MemberName
        {
            get { return base.MemberName; }
            set
            {
                base.MemberName = value;
                OnPropertyChanged(nameof(MemberName));
                OnPropertyChanged(nameof(MemberFullName));
            }
        }

        /// <inheritdoc/>
        public override string MemberPath
        {
            get { return base.MemberPath; }
            set
            {
                base.MemberPath = value;
                OnPropertyChanged(nameof(MemberPath));
                OnPropertyChanged(nameof(MemberFullName));
            }
        }

        /// <inheritdoc/>
        public override string MemberFullName
        {
            get { return base.MemberFullName; }
            set
            {
                base.MemberFullName = value;
                OnPropertyChanged(nameof(MemberName));
                OnPropertyChanged(nameof(MemberPath));
                OnPropertyChanged(nameof(MemberFullName));
            }
        }

        /// <summary>
        /// Constructor for NameSpace item
        /// </summary>
        public NameSpaceItem() : base()
        { Scope = ScopeType.Null; }

        /// <summary>
        /// Constructor for NameSpace item
        /// </summary>
        /// <param name="source"></param>
        /// <param name="scope"></param>
        public NameSpaceItem(INameSpaceKey source, ScopeType scope = ScopeType.ModelNameSpace) : base(source)
        { Scope = scope; }

        /// <summary>
        /// Constructor for NameSpace item
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceItem(INameSpaceItem source): base (source)
        { Scope = source.Scope; }

        #region INotifyPropertyChanged
        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged is PropertyChangedEventHandler handler)
            { handler(this, new PropertyChangedEventArgs(propertyName)); }
        }
        #endregion
    }
}
