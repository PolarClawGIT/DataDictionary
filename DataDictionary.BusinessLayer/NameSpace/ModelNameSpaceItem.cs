using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.ApplicationData.Model.SubjectArea;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
using System.ComponentModel;

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
    public class ModelNameSpaceItem : IModelNameSpaceItem, INotifyPropertyChanged
    {
        /// <summary>
        /// Container for the SystemId
        /// </summary>
        public ModelNameSpaceKey SystemKey { get; protected set; }

        /// <summary>
        /// Container for the SystemParentId
        /// </summary>
        public ModelNameSpaceKey? SystemParentKey { get; protected set; }

        /// <summary>
        /// Container for the MemberName.
        /// </summary>
        public ModelNameSpaceKeyMember NameKey { get; protected set; }

        /// <summary>
        /// Container for the Scope.
        /// </summary>
        public ScopeKey ScopeKey { get; protected set; }

        /// <inheritdoc/>
        public virtual String MemberName { get { return NameKey.MemberName; } }

        /// <inheritdoc/>
        public virtual String MemberPath { get { return NameKey.MemberPath; } }

        /// <inheritdoc/>
        public virtual String MemberFullName { get { return NameKey.MemberFullName; } }

        /// <inheritdoc/>
        public virtual ScopeType Scope { get { return ScopeKey.Scope; } }

        /// <inheritdoc/>
        public virtual Guid SystemId { get { return SystemKey.SystemId; } }

        /// <inheritdoc/>
        public virtual Guid SystemParentId
        { get { if (SystemParentKey is null) { return Guid.Empty; } else { return SystemParentKey.SystemId; } } }

        /// <inheritdoc/>
        public virtual Int32 OrdinalPosition { get; protected set; } = Int32.MaxValue;

        /// <summary>
        /// List of keys that are the children of this record.
        /// </summary>
        public virtual List<ModelNameSpaceKey> Children { get; } = new List<ModelNameSpaceKey>();

        /// <summary>
        /// Source of the Model NameSpace Item.
        /// </summary>
        public virtual Object? Source { get; init; } = null;

        /// <summary>
        /// Function that is used to reset the internal NameKey to what is returned by this value.
        /// </summary>
        /// <remarks>Used by OnPropertyChanged event</remarks>
        protected virtual Func<ModelNameSpaceKeyMember> GetNameKey { get; set; } = () => new ModelNameSpaceKeyMember(String.Empty);

        /// <summary>
        /// Constructor for a Model NameSpace, Base (blank item)
        /// </summary>
        public ModelNameSpaceItem() : base()
        {
            SystemKey = new ModelNameSpaceKey();
            NameKey = new ModelNameSpaceKeyMember(String.Empty);
            ScopeKey = new ScopeKey(ScopeType.Null);
        }

        /// <summary>
        /// Constructor for a Model NameSpace, DbCatalog
        /// </summary>
        /// <param name="data"></param>
        public ModelNameSpaceItem(IDbCatalogItem data) : base()
        {
            SystemKey = new ModelNameSpaceKey((IDbCatalogKey)data);
            Source = data;
            GetNameKey = () => new ModelNameSpaceKeyMember((IDbCatalogKeyName)data);
            NameKey = GetNameKey();
            ScopeKey = new ScopeKey(data);
            data.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Constructor for a Model NameSpace, DbSchema
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="data"></param>
        public ModelNameSpaceItem(IDbCatalogKey parent, IDbSchemaItem data) : base()
        {
            SystemKey = new ModelNameSpaceKey((IDbSchemaKey)data);
            SystemParentKey = new ModelNameSpaceKey(parent);
            Source = data;
            GetNameKey = () => new ModelNameSpaceKeyMember((IDbSchemaKeyName)data);
            NameKey = GetNameKey();
            ScopeKey = new ScopeKey(data);
            data.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Constructor for a Model NameSpace, DbTable
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="data"></param>
        public ModelNameSpaceItem(IDbSchemaKey parent, IDbTableItem data) : base()
        {
            SystemKey = new ModelNameSpaceKey((IDbTableKey)data);
            SystemParentKey = new ModelNameSpaceKey(parent);
            Source = data;
            GetNameKey = () => new ModelNameSpaceKeyMember((IDbTableKeyName)data);
            NameKey = GetNameKey();
            ScopeKey = new ScopeKey(data);
            data.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Constructor for a Model NameSpace, DbTableColumn
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="data"></param>
        public ModelNameSpaceItem(IDbTableKey parent, IDbTableColumnItem data) : base()
        {
            SystemKey = new ModelNameSpaceKey((IDbTableColumnKey)data);
            SystemParentKey = new ModelNameSpaceKey(parent);
            Source = data;
            GetNameKey = () => new ModelNameSpaceKeyMember((IDbTableColumnKeyName)data);
            NameKey = GetNameKey();
            ScopeKey = new ScopeKey(data);
            if (data.OrdinalPosition is Int32 position) { OrdinalPosition = position; }
            data.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Constructor for a Model NameSpace, DbConstraint
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="data"></param>
        public ModelNameSpaceItem(IDbTableKey parent, IDbConstraintItem data) : base()
        {
            SystemKey = new ModelNameSpaceKey((IDbConstraintKey)data);
            SystemParentKey = new ModelNameSpaceKey(parent);
            Source = data;
            GetNameKey = () => new ModelNameSpaceKeyMember((IDbConstraintKeyName)data);
            NameKey = GetNameKey();
            ScopeKey = new ScopeKey(data);
            data.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Constructor for a Model NameSpace, DbRoutine
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="data"></param>
        public ModelNameSpaceItem(IDbSchemaKey parent, IDbRoutineItem data) : base()
        {
            SystemKey = new ModelNameSpaceKey((IDbRoutineKey)data);
            SystemParentKey = new ModelNameSpaceKey(parent);
            Source = data;
            GetNameKey = () => new ModelNameSpaceKeyMember((IDbRoutineKeyName)data);
            NameKey = GetNameKey();
            ScopeKey = new ScopeKey(data);
            data.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Constructor for a Model NameSpace, DbRoutineParameter
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="data"></param>
        public ModelNameSpaceItem(IDbRoutineKey parent, IDbRoutineParameterItem data) : base()
        {
            SystemKey = new ModelNameSpaceKey((IDbRoutineParameterKey)data);
            SystemParentKey = new ModelNameSpaceKey(parent);
            Source = data;
            GetNameKey = () => new ModelNameSpaceKeyMember((IDbRoutineParameterKeyName)data);
            NameKey = GetNameKey();
            ScopeKey = new ScopeKey(data);
            if (data.OrdinalPosition is Int32 position) { OrdinalPosition = position; }
            data.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Constructor for a Model NameSpace, DbDomain
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="data"></param>
        public ModelNameSpaceItem(IDbSchemaKey parent, IDbDomainItem data) : base()
        {
            SystemKey = new ModelNameSpaceKey((IDbDomainKey)data);
            SystemParentKey = new ModelNameSpaceKey(parent);
            Source = data;
            GetNameKey = () => new ModelNameSpaceKeyMember((IDbDomainKeyName)data);
            NameKey = GetNameKey();
            ScopeKey = new ScopeKey(data);
            data.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Constructor for a Model NameSpace, LibrarySource
        /// </summary>
        /// <param name="data"></param>
        public ModelNameSpaceItem(ILibrarySourceItem data) : base()
        {
            SystemKey = new ModelNameSpaceKey((ILibrarySourceKey)data);
            Source = data;
            GetNameKey = () => new ModelNameSpaceKeyMember((ILibrarySourceKeyName)data);
            NameKey = GetNameKey();
            ScopeKey = new ScopeKey(data);
            data.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Constructor for a Model NameSpace, LibraryMember
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="data"></param>
        public ModelNameSpaceItem(ILibrarySourceKey parent, ILibraryMemberItem data) : base()
        {
            SystemKey = new ModelNameSpaceKey((ILibraryMemberKey)data);
            SystemParentKey = new ModelNameSpaceKey(parent);
            Source = data;
            GetNameKey = () => new ModelNameSpaceKeyMember((ILibraryMemberKeyName)data);
            NameKey = GetNameKey();
            ScopeKey = new ScopeKey(data);
            data.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Constructor for a Model NameSpace, LibraryMember
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="data"></param>
        public ModelNameSpaceItem(ILibraryMemberKey parent, ILibraryMemberItem data) : base()
        {
            SystemKey = new ModelNameSpaceKey((ILibraryMemberKey)data);
            SystemParentKey = new ModelNameSpaceKey(parent);
            Source = data;
            GetNameKey = () => new ModelNameSpaceKeyMember((ILibraryMemberKeyName)data);
            NameKey = GetNameKey();
            ScopeKey = new ScopeKey(data);
            data.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Constructor for a Model NameSpace, Model
        /// </summary>
        /// <param name="data"></param>
        public ModelNameSpaceItem(IModelItem data) : base()
        {
            SystemKey = new ModelNameSpaceKey((IModelKey)data);
            Source = data;
            GetNameKey = () => new ModelNameSpaceKeyMember((IModelItem)data);
            NameKey = GetNameKey();
            ScopeKey = new ScopeKey(ScopeType.Model);
            data.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Constructor for a Model NameSpace, ModelSubjectArea
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="data"></param>
        public ModelNameSpaceItem(IModelKey parent, IModelSubjectAreaItem data) : base()
        {
            SystemKey = new ModelNameSpaceKey((IModelSubjectAreaKey)data);
            SystemParentKey = new ModelNameSpaceKey(parent);
            Source = data;
            GetNameKey = () => new ModelNameSpaceKeyMember((IModelSubjectAreaItem)data);
            NameKey = GetNameKey();
            ScopeKey = new ScopeKey(ScopeType.ModelSubjectArea);
            data.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Constructor for a Model NameSpace, ModelSubjectArea
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="data"></param>
        public ModelNameSpaceItem(IModelSubjectAreaKey parent, IModelSubjectAreaItem data) : base()
        {
            SystemKey = new ModelNameSpaceKey((IModelSubjectAreaKey)data);
            SystemParentKey = new ModelNameSpaceKey(parent);
            Source = data;
            GetNameKey = () => new ModelNameSpaceKeyMember((IModelSubjectAreaItem)data);
            NameKey = GetNameKey();
            ScopeKey = new ScopeKey(ScopeType.ModelSubjectArea);
            data.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Constructor for a Model NameSpace, DomainAttribute
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="data"></param>
        public ModelNameSpaceItem(IModelKey parent, IDomainAttributeItem data) : base()
        {
            SystemKey = new ModelNameSpaceKey((IDomainAttributeKey)data);
            SystemParentKey = new ModelNameSpaceKey(parent);
            Source = data;
            GetNameKey = () => new ModelNameSpaceKeyMember((IDomainAttributeItem)data);
            NameKey = GetNameKey();
            ScopeKey = new ScopeKey(ScopeType.ModelAttribute);
            data.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Constructor for a Model NameSpace, DomainAttribute
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="data"></param>
        public ModelNameSpaceItem(IModelSubjectAreaKey parent, IDomainAttributeItem data) : base()
        {
            SystemKey = new ModelNameSpaceKey((IDomainAttributeKey)data);
            SystemParentKey = new ModelNameSpaceKey(parent);
            Source = data;
            GetNameKey = () => new ModelNameSpaceKeyMember((IDomainAttributeItem)data);
            NameKey = GetNameKey();
            ScopeKey = new ScopeKey(ScopeType.ModelAttribute);
            data.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Constructor for a Model NameSpace, DomainEntity
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="data"></param>
        public ModelNameSpaceItem(IModelKey parent, IDomainEntityItem data) : base()
        {
            SystemKey = new ModelNameSpaceKey((IDomainEntityKey)data);
            SystemParentKey = new ModelNameSpaceKey(parent);
            Source = data;
            GetNameKey = () => new ModelNameSpaceKeyMember((IDomainEntityItem)data);
            NameKey = GetNameKey();
            ScopeKey = new ScopeKey(ScopeType.ModelEntity);
            data.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Constructor for a Model NameSpace, DomainEntity
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="data"></param>
        public ModelNameSpaceItem(IModelSubjectAreaKey parent, IDomainEntityItem data) : base()
        {
            SystemKey = new ModelNameSpaceKey((IDomainEntityKey)data);
            SystemParentKey = new ModelNameSpaceKey(parent);
            Source = data;
            GetNameKey = () => new ModelNameSpaceKeyMember((IDomainEntityItem)data);
            NameKey = GetNameKey();
            ScopeKey = new ScopeKey(ScopeType.ModelEntity);
            data.PropertyChanged += OnPropertyChanged;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Responds to OnPropertyChanged event of the Source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler? handler = PropertyChanged;

            if (GetNameKey() is ModelNameSpaceKeyMember newNameKey && !newNameKey.Equals(NameKey))
            {
                NameKey = newNameKey;
                if (handler is PropertyChangedEventHandler)
                { handler(this, new PropertyChangedEventArgs(nameof(NameKey))); }
            }
        }

        /// <summary>
        /// Used to clear all subscriptions to the events.
        /// </summary>
        public void ClearEvents()
        { Delegate.RemoveAll(PropertyChanged, PropertyChanged); }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override String? ToString()
        { return MemberFullName; }
    }

}
