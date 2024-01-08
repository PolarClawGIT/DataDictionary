using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Alias;

namespace DataDictionary.BusinessLayer.NameSpace
{
    /// <summary>
    /// Interface for a Model NameSpace Item.
    /// </summary>
    public interface IModelNameSpaceItem : IModelNameSpaceKeyMember, IModelNameSpaceKey, IScopeKey
    {
        /// <summary>
        /// Parent System Id
        /// </summary>
        Guid SystemParentId { get; }

        /// <summary>
        /// The Position/Order of the Item within Like Items
        /// </summary>
        Int32 OrdinalPosition { get; }
    }

    /// <summary>
    /// Implementation for Model NameSpace Item.
    /// </summary>
    public class ModelNameSpaceItem : IModelNameSpaceItem
    {
        /// <inheritdoc/>
        public virtual String MemberName { get { return MemberNameKey.MemberName; } }

        /// <inheritdoc/>
        public virtual String MemberPath { get { return MemberNameKey.MemberPath; } }

        /// <inheritdoc/>
        public virtual String MemberFullName { get { return MemberNameKey.MemberFullName; } }

        /// <inheritdoc/>
        public virtual ScopeType ScopeId { get { return MemberScopeKey.ScopeId; } }

        /// <inheritdoc/>
        public virtual Guid SystemId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual Guid SystemParentId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual Int32 OrdinalPosition { get; init; } = Int32.MaxValue;

        /// <summary>
        /// Scope Key for the Member
        /// </summary>
        public virtual ScopeKey MemberScopeKey {get; init;} = new ScopeKey(ScopeType.Null);

        /// <summary>
        /// Name Key for the Member
        /// </summary>
        public virtual ModelNameSpaceKeyMember MemberNameKey { get; init; } = new ModelNameSpaceKeyMember(String.Empty);

        /// <summary>
        /// List of keys that are the children of this record.
        /// </summary>
        public virtual List<ModelNameSpaceKey> Children { get; } = new List<ModelNameSpaceKey>();

        /// <summary>
        /// Source of the Model NameSpace Item.
        /// </summary>
        public virtual Object? Source { get; init; } = null;

        /// <summary>
        /// Constructor for a ModelNameSpaceItem
        /// </summary>
        protected internal ModelNameSpaceItem() : base() { }

        /// <summary>
        /// Constructor for a ModelNameSpaceItem
        /// </summary>
        /// <param name="source"></param>
        protected internal ModelNameSpaceItem(IModelNameSpaceItem source) : this()
        {
            SystemId = source.SystemId;
            SystemParentId = source.SystemParentId;
            OrdinalPosition = source.OrdinalPosition;
            MemberNameKey = new ModelNameSpaceKeyMember(source);
            MemberScopeKey = new ScopeKey(source);
            Source = source;
        }



        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override String? ToString()
        { return MemberFullName; }
    }

}
