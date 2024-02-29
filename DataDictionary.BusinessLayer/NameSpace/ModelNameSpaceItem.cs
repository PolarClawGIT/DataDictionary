using DataDictionary.DataLayer;
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
using DataDictionary.DataLayer.ModelData;
using DataDictionary.DataLayer.ModelData.SubjectArea;
using System.ComponentModel;

namespace DataDictionary.BusinessLayer.NameSpace
{
    /// <summary>
    /// Interface for a Model NameSpace Item.
    /// </summary>
    public interface IModelNameSpaceItem : IModelNameSpaceKey, INameSpaceKey, IScopeKey
    {
        /// <summary>
        /// Parent System Id
        /// </summary>
        Guid SystemParentId { get; }

        /// <summary>
        /// The Position/Order of the Item within Like Items
        /// </summary>
        Int32 OrdinalPosition { get; }

        /// <summary>
        /// The Title for the Item (what is displayed)
        /// </summary>
        String MemberTitle { get; }
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
        public ModelNameSpaceKey? SystemParentKey { get; internal protected set; }

        /// <summary>
        /// List of keys that are the children of this record.
        /// </summary>
        public virtual List<ModelNameSpaceKey> Children { get; } = new List<ModelNameSpaceKey>();

        /// <summary>
        /// Container for the Scope.
        /// </summary>
        public ScopeKey ScopeKey { get; protected set; }

        /// <inheritdoc/>
        public String MemberTitle
        {
            get { return memberTitle; }
            set { memberTitle = value; OnPropertyChanged(nameof(MemberTitle)); }
        }
        private String memberTitle = String.Empty;

        /// <inheritdoc/>
        public virtual String MemberName { get { return nameSpaceKey.MemberName; } }

        /// <inheritdoc/>
        public virtual String MemberPath { get { return nameSpaceKey.MemberPath; } }

        /// <inheritdoc/>
        public virtual String MemberFullName { get { return nameSpaceKey.MemberFullName; } }

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
        /// Source of the Model NameSpace Item.
        /// </summary>
        public virtual Object? Source { get; init; } = null;

        /// <summary>
        /// Function that is used to reset the MemberName, MemberPath and MemberFullName
        /// </summary>
        /// <remarks>Used by OnPropertyChanged event</remarks>
        protected virtual Func<NameSpaceKey> GetNameSpaceKey
        {
            get { return funcGetNameSpaceKey; }
            private set
            {
                funcGetNameSpaceKey = value;
                nameSpaceKey = value();
                OnPropertyChanged(nameof(MemberName));
                OnPropertyChanged(nameof(MemberPath));
                OnPropertyChanged(nameof(MemberFullName));
            }
        }
        private Func<NameSpaceKey> funcGetNameSpaceKey = () => new NameSpaceKey(String.Empty);
        private NameSpaceKey nameSpaceKey = new NameSpaceKey(String.Empty);

        /// <summary>
        /// Function that is used to set the Title.
        /// </summary>
        /// <remarks>Used by OnPropertyChanged event</remarks>
        protected virtual Func<String> GetTitle
        {
            get { return funcGetTitle; }
            private set
            {
                funcGetTitle = value;
                MemberTitle = value();
            }
        }
        private Func<String> funcGetTitle = () => String.Empty;


        #region Constrictors
        /// <summary>
        /// Constructor for a Model NameSpace, Base (blank item)
        /// </summary>
        public ModelNameSpaceItem() : base()
        {
            SystemKey = new ModelNameSpaceKey();
            ScopeKey = new ScopeKey(ScopeType.Null);
        }

        /// <summary>
        /// Constructor for a Model NameSpace, DbCatalog
        /// </summary>
        /// <param name="data"></param>
        public ModelNameSpaceItem(IDbCatalogItem data) : base()
        {
            SystemKey = new ModelNameSpaceKey((IDbCatalogKey)data);
            ScopeKey = new ScopeKey(data);
            Source = data;

            GetNameSpaceKey = () => new NameSpaceKey((IDbCatalogKeyName)data);
            GetTitle = () => new DbCatalogKeyName(data).DatabaseName;

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
            ScopeKey = new ScopeKey(data);
            Source = data;

            GetNameSpaceKey = () => new NameSpaceKey((IDbSchemaKeyName)data);
            GetTitle = () => new DbSchemaKeyName(data).SchemaName;

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
            ScopeKey = new ScopeKey(data);
            Source = data;

            GetNameSpaceKey = () => new NameSpaceKey((IDbTableKeyName)data);
            GetTitle = () => new DbTableKeyName(data).TableName;

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
            ScopeKey = new ScopeKey(data);
            Source = data;

            GetNameSpaceKey = () => new NameSpaceKey((IDbTableColumnKeyName)data);
            GetTitle = () => new DbTableColumnKeyName(data).TableName;

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
            ScopeKey = new ScopeKey(data);
            Source = data;

            GetNameSpaceKey = () => new NameSpaceKey((IDbConstraintKeyName)data);
            GetTitle = () => new DbConstraintKeyName(data).ConstraintName;

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
            ScopeKey = new ScopeKey(data);
            Source = data;

            GetNameSpaceKey = () => new NameSpaceKey((IDbRoutineKeyName)data);
            GetTitle = () => new DbRoutineKeyName(data).RoutineName;

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
            ScopeKey = new ScopeKey(data);

            GetNameSpaceKey = () => new NameSpaceKey((IDbRoutineParameterKeyName)data);
            GetTitle = () => new DbRoutineParameterKeyName(data).RoutineName;

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
            ScopeKey = new ScopeKey(data);

            GetNameSpaceKey = () => new NameSpaceKey((IDbDomainKeyName)data);
            GetTitle = () => new DbDomainKeyName(data).DomainName;

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
            ScopeKey = new ScopeKey(data);

            GetNameSpaceKey = () => new NameSpaceKey((ILibrarySourceKeyName)data);
            GetTitle = () => new LibrarySourceKeyName(data).AssemblyName;

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
            ScopeKey = new ScopeKey(data);

            GetNameSpaceKey = () => new NameSpaceKey((ILibraryMemberKeyName)data);
            GetTitle = () => new LibraryMemberKeyName(data).MemberName;

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
            ScopeKey = new ScopeKey(data);

            GetNameSpaceKey = () => new NameSpaceKey((ILibraryMemberKeyName)data);
            GetTitle = () => new LibraryMemberKeyName(data).MemberName;

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
            ScopeKey = new ScopeKey(ScopeType.Model);

            GetNameSpaceKey = () => new NameSpaceKey((IModelItem)data);
            GetTitle = () => data.ModelTitle ?? String.Empty;

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
            ScopeKey = new ScopeKey(ScopeType.ModelSubjectArea);

            GetNameSpaceKey = () => new NameSpaceKey((IModelSubjectAreaItem)data);
            GetTitle = () => data.SubjectAreaTitle ?? String.Empty;

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
            ScopeKey = new ScopeKey(ScopeType.ModelSubjectArea);

            GetNameSpaceKey = () => new NameSpaceKey((IModelSubjectAreaItem)data);
            GetTitle = () => data.SubjectAreaTitle ?? String.Empty;

            data.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// /// Constructor for a Model NameSpace, Model/NameSpace
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="data"></param>
        /// <param name="dataKey"></param>
        public ModelNameSpaceItem(IModelKey parent, INameSpaceKey data, IModelNameSpaceKey dataKey)
        {
            SystemKey = new ModelNameSpaceKey(dataKey);
            SystemParentKey = new ModelNameSpaceKey(parent);
            Source = data;
            ScopeKey = new ScopeKey(ScopeType.ModelNameSpace);

            GetNameSpaceKey = () => new NameSpaceKey(data);
            GetTitle = () => data.MemberName ?? String.Empty;
        }

        /// <summary>
        /// /// /// Constructor for a Model NameSpace, NameSpace/NameSpace
        /// </summary>
        /// <param name="parentKey"></param>
        /// <param name="data"></param>
        /// <param name="dataKey"></param>
        public ModelNameSpaceItem(IModelNameSpaceKey parentKey, INameSpaceKey data, IModelNameSpaceKey dataKey)
        {
            SystemKey = new ModelNameSpaceKey(dataKey);
            SystemParentKey = new ModelNameSpaceKey(parentKey);
            Source = data;
            ScopeKey = new ScopeKey(ScopeType.ModelNameSpace);

            GetNameSpaceKey = () => new NameSpaceKey(data);
            GetTitle = () => data.MemberName ?? String.Empty;
        }

        /// <summary>
        /// Constructor for a Model NameSpace, SubjectArea/NameSpace
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="data"></param>
        /// <param name="dataKey"></param>
        public ModelNameSpaceItem(IModelSubjectAreaItem parent, INameSpaceKey data, IModelNameSpaceKey dataKey)
        {
            SystemKey = new ModelNameSpaceKey(dataKey);
            SystemParentKey = new ModelNameSpaceKey(parent);
            Source = data;
            ScopeKey = new ScopeKey(ScopeType.ModelNameSpace);

            GetNameSpaceKey = () => new NameSpaceKey(data);
            GetTitle = () => data.MemberName ?? String.Empty;
        }

        /// <summary>
        /// Constructor for a Model NameSpace, NameSpace/SubjectArea
        /// </summary>
        /// <param name="parentKey"></param>
        /// <param name="data"></param>
        public ModelNameSpaceItem(IModelNameSpaceKey parentKey, IModelSubjectAreaItem data)
        {
            SystemKey = new ModelNameSpaceKey((INameSpaceKey)data);
            SystemParentKey = new ModelNameSpaceKey(parentKey);
            Source = data;
            ScopeKey = new ScopeKey(ScopeType.ModelSubjectArea);

            GetNameSpaceKey = () => new NameSpaceKey((IModelSubjectAreaItem)data);
            GetTitle = () => data.SubjectAreaTitle ?? String.Empty;

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
            ScopeKey = new ScopeKey(ScopeType.ModelAttribute);

            GetNameSpaceKey = () => new NameSpaceKey((IDomainAttributeItem)data);
            GetTitle = () => data.AttributeTitle ?? String.Empty;

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
            ScopeKey = new ScopeKey(ScopeType.ModelAttribute);

            GetNameSpaceKey = () => new NameSpaceKey((IDomainAttributeItem)data);
            GetTitle = () => data.AttributeTitle ?? String.Empty;

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
            ScopeKey = new ScopeKey(ScopeType.ModelEntity);

            GetNameSpaceKey = () => new NameSpaceKey((IDomainEntityItem)data);
            GetTitle = () => data.EntityTitle ?? String.Empty;

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
            ScopeKey = new ScopeKey(ScopeType.ModelEntity);

            GetNameSpaceKey = () => new NameSpaceKey((IDomainEntityItem)data);
            GetTitle = () => data.EntityTitle ?? String.Empty;

            data.PropertyChanged += OnPropertyChanged;
        }
        #endregion

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Responds to OnPropertyChanged event of the Source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (GetNameSpaceKey() is NameSpaceKey newNameKey && !newNameKey.Equals(nameSpaceKey))
            { nameSpaceKey = newNameKey; }

            if (GetTitle() is String newTitle && !newTitle.Equals(MemberTitle))
            { MemberTitle = newTitle; }
        }

        /// <summary>
        /// Reports a OnPropertyChanged event.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler? handler = PropertyChanged;
            if (handler is PropertyChangedEventHandler)
            { handler(this, new PropertyChangedEventArgs(propertyName)); }
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
