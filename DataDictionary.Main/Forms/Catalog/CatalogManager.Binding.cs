using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.Main.Properties;
using DataDictionary.Resource;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Catalog
{
    partial class CatalogManager
    {
        class FormBinding
        {
            public required BindingSource ManagerBinding { private get; init; }

            BindingList<BindingValue> managerData { get; } = new BindingList<BindingValue>();

            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }
            public required Action OnRefresh { get; init; }

            public void Load(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                var catalogs = ICatalogData.Create();
                ManagerBinding.RaiseListChangedEvents = false;

                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                if (Settings.Default.IsOnLineMode)
                {
                    work.Add(factory.OpenConnection());
                    work.AddRange(catalogs.Load(factory));
                }

                DoWork(work, StartBinding);

                void StartBinding(RunWorkerCompletedEventArgs args)
                {
                    managerData.Clear();

                    BindingCompare bindingCompare = new BindingCompare();
                    managerData.AddRange(
                        BusinessData.CatalogModel.DbCatalogs.Select(s => new BindingValue(s)).
                        Union(catalogs.Select(s => new BindingValue(s)), bindingCompare));

                    foreach (var item in managerData)
                    {
                        if (catalogs.Any(a => item.Equals(a)))
                        { item.InDatabase = true; }
                    }

                    RefreshInModel();

                    ManagerBinding.DataSource = managerData;
                    ManagerBinding.RaiseListChangedEvents = true;
                    ManagerBinding.ResetBindings(false);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public Boolean TryGetValue([NotNullWhen(true)] out BindingValue? result)
            {
                if (ManagerBinding.Position >= 0 && ManagerBinding.Current is BindingValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public void Load(BindingValue binding, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();
                work.Add(factory.OpenConnection());

                if (binding.TryGetIndex(out CatalogIndex? catalog))
                { work.AddRange(BusinessData.CatalogModel.Load(factory, catalog)); }

                DoWork(work, WorkComplete);

                void WorkComplete(RunWorkerCompletedEventArgs args)
                {
                    RefreshInModel();

                    if (onComplete is not null)
                    { onComplete(args); }
                }
            }

            public void Remove(BindingValue binding)
            {
                if (binding.TryGetIndex(out CatalogIndex? catalog))
                { BusinessData.CatalogModel.Remove(catalog); }

                RefreshInModel();
                OnRefresh();
            }

            public void Save(BindingValue binding, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();
                work.Add(factory.OpenConnection());

                if (binding.TryGetIndex(out CatalogIndex? catalog))
                { work.AddRange(BusinessData.CatalogModel.Save(factory, catalog)); }

                DoWork(work, WorkComplete);

                void WorkComplete(RunWorkerCompletedEventArgs args)
                {
                    RefreshInModel();

                    if (onComplete is not null)
                    { onComplete(args); }
                }
            }

            public void Delete(BindingValue binding, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();
                work.Add(factory.OpenConnection());

                if (binding.TryGetIndex(out CatalogIndex? catalog))
                {
                    work.AddRange(BusinessData.CatalogModel.Delete(catalog));
                    work.AddRange(BusinessData.CatalogModel.Save(factory, catalog));
                }

                DoWork(work, WorkComplete);

                void WorkComplete(RunWorkerCompletedEventArgs args)
                {
                    RefreshInModel();
                    OnRefresh();

                    if (onComplete is not null)
                    { onComplete(args); }
                }
            }

            public void Import(DbSchemaContext db, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                List<WorkItem> work = new List<WorkItem>();

                work.AddRange(BusinessData.CatalogModel.Import(db));

                DoWork(work, WorkComplete);

                void WorkComplete(RunWorkerCompletedEventArgs args)
                {
                    BindingCompare bindingCompare = new BindingCompare();

                    managerData.AddRange(
                        BusinessData.CatalogModel.DbCatalogs.Select(s => new BindingValue(s)).
                        Except(managerData, bindingCompare));
                    
                    RefreshInModel();
                    OnRefresh();

                    if (onComplete is not null)
                    { onComplete(args); }
                }
            }

            private void RefreshInModel()
            {
                foreach (var item in managerData)
                {
                    if (BusinessData.CatalogModel.DbCatalogs.Any(a => item.Equals(a))
                        || BusinessData.CatalogModel.DbCatalogs.Any(a => item.Equals(a)))
                    { item.InModel = true; }
                    else { item.InModel = false; }
                }
            }

            public Boolean GetAuthorization(Enumerations.ButtonType command)
            {
                switch (command)
                {
                    case Enumerations.ButtonType.Default: return true;
                    case Enumerations.ButtonType.Add: return BusinessData.Authorization.IsCatalogAdmin;
                    case Enumerations.ButtonType.Delete: return BusinessData.Authorization.IsCatalogAdmin;
                    case Enumerations.ButtonType.OpenDatabase: return BusinessData.Authorization.IsCatalogAdmin;
                    case Enumerations.ButtonType.SaveDatabase: return BusinessData.Authorization.IsCatalogAdmin;
                    case Enumerations.ButtonType.DeleteDatabase: return BusinessData.Authorization.IsCatalogAdmin;
                    case Enumerations.ButtonType.HistoryDatabase: return BusinessData.Authorization.IsCatalogAdmin;
                    default: return false;
                }
            }
        }

        class BindingValue : IBindingPropertyChanged,
            ICatalogIndex,
            IKeyEquality<ICatalogIndex>
        {

            public Guid? CatalogId
            {
                get
                {
                    if (dataSource is ICatalogValue sourceValue)
                    { return sourceValue.CatalogId; }
                    else { return null; }
                }
            }

            /// <inheritdoc/>
            public virtual Boolean HasValue { get { return CatalogId.HasValue && CatalogId != Guid.Empty; } }

            public String? CatalogTitle
            {
                get
                {
                    if (dataSource is ICatalogValue value)
                    { return value.CatalogTitle; }
                    else { return null; }
                }
            }

            public String? CatalogDescription
            {
                get
                {
                    if (dataSource is ICatalogValue value)
                    { return value.CatalogDescription; }
                    else { return null; }
                }
            }

            public String? ServerName
            {
                get
                {
                    if (dataSource is ICatalogValue value)
                    { return value.ServerName; }
                    else { return null; }
                }
            }

            public String? DatabaseName
            {
                get
                {
                    if (dataSource is ICatalogValue value)
                    { return value.DatabaseName; }
                    else { return null; }
                }
            }

            public DateTime? SourceDate
            {
                get
                {
                    if (dataSource is ICatalogValue value)
                    { return value.SourceDate; }
                    else { return null; }
                }
            }

            public Boolean InModel
            {
                get { return inModel; }
                set
                {
                    inModel = value;
                    this.OnPropertyChanged(PropertyChanged, nameof(InModel));
                }
            }
            private Boolean inModel;

            public Boolean InDatabase
            {
                get { return inDatabase; }
                set
                {
                    inDatabase = value;
                    this.OnPropertyChanged(PropertyChanged, nameof(InDatabase));
                }
            }
            private Boolean inDatabase;

            ICatalogValue? dataSource;

            public BindingValue(ICatalogValue value)
            {
                dataSource = value;
                value.PropertyChanged += Value_PropertyChanged;
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            private void Value_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
            {
                if (dataSource is ICatalogValue sourceValue)
                {
                    if (e.PropertyName is nameof(ICatalogValue.CatalogTitle))
                    { this.OnPropertyChanged(PropertyChanged, nameof(CatalogTitle)); }

                    if (e.PropertyName is nameof(ICatalogValue.CatalogDescription))
                    { this.OnPropertyChanged(PropertyChanged, nameof(CatalogDescription)); }

                    if (e.PropertyName is nameof(ICatalogValue.ServerName))
                    { this.OnPropertyChanged(PropertyChanged, nameof(ServerName)); }

                    if (e.PropertyName is nameof(ICatalogValue.DatabaseName))
                    { this.OnPropertyChanged(PropertyChanged, nameof(DatabaseName)); }

                    if (e.PropertyName is nameof(ICatalogValue.SourceDate))
                    { this.OnPropertyChanged(PropertyChanged, nameof(SourceDate)); }
                }
                else { }
            }

            public Boolean TryGetIndex([NotNullWhen(true)] out CatalogIndex? result)
            {
                if (dataSource is ICatalogValue value)
                { result = new CatalogIndex(value); return true; }
                else { result = null; return false; }
            }

            public Boolean TryGetValue([NotNullWhen(true)] out ICatalogValue? result)
            {
                if (dataSource is ICatalogValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            #region IEquatable
            public Boolean Equals(ICatalogIndex? other)
            { return dataSource is ICatalogIndex value && new CatalogIndex(value).Equals(other); }

            public Boolean Equals(BindingValue? other)
            {
                return other is BindingValue value
                    && CatalogId is Guid
                    && value.CatalogId is Guid
                    && Guid.Equals(CatalogId, value.CatalogId);
            }

            /// <inheritdoc/>
            /// <remarks>Object</remarks>
            public override Boolean Equals(object? obj)
            {
                return obj is BindingValue value
                    && CatalogId is Guid
                    && value.CatalogId is Guid
                    && Guid.Equals(CatalogId, value.CatalogId);
            }

            /// <inheritdoc/>
            public static Boolean operator ==(BindingValue left, ICatalogIndex right)
            { return left.Equals(right); }

            /// <inheritdoc/>
            public static Boolean operator !=(BindingValue left, ICatalogIndex right)
            { return !left.Equals(right); }

            /// <inheritdoc/>
            public static Boolean operator ==(BindingValue left, BindingValue right)
            { return left.Equals(right); }

            /// <inheritdoc/>
            public static Boolean operator !=(BindingValue left, BindingValue right)
            { return !left.Equals(right); }

            /// <inheritdoc/>
            public override Int32 GetHashCode()
            {
                if (CatalogId is Guid) { return CatalogId.GetHashCode(); }
                else { return Guid.Empty.GetHashCode(); }
            }
            #endregion
        }

        class BindingCompare : IEqualityComparer<BindingValue>
        {
            public Boolean Equals(BindingValue? x, BindingValue? y)
            { return x is BindingValue left && y is BindingValue right && left.Equals(right); }

            public Int32 GetHashCode([DisallowNull] BindingValue obj)
            { return obj.GetHashCode(); }
        }
    }
}
