using DataDictionary.BusinessLayer.AppLibrary;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.Main.Properties;
using DataDictionary.Resource;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Library
{
    partial class LibraryManager
    {
        class FormBinding
        {
            public required BindingSource ManagerBinding { private get; init; }

            BindingList<BindingValue> managerData { get; } = new BindingList<BindingValue>();

            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }
            public required Action OnRefresh { get; init; }

            public void Load(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                var catalogs = ILibrarySourceData.Create();
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
                        BusinessData.LibraryModel.LibrarySources.Select(s => new BindingValue(s)).
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

                if (binding.TryGetIndex(out LibrarySourceIndex? library))
                { work.AddRange(BusinessData.LibraryModel.Load(factory, library)); }

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
                if (binding.TryGetIndex(out LibrarySourceIndex? library))
                { BusinessData.LibraryModel.Remove(library); }

                RefreshInModel();
                OnRefresh();
            }

            public void Save(BindingValue binding, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();
                work.Add(factory.OpenConnection());

                if (binding.TryGetIndex(out LibrarySourceIndex? library))
                { work.AddRange(BusinessData.LibraryModel.Save(factory, library)); }

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

                if (binding.TryGetIndex(out LibrarySourceIndex? library))
                {
                    work.AddRange(BusinessData.LibraryModel.Delete(library));
                    work.AddRange(BusinessData.LibraryModel.Save(factory, library));
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

            public void Import(IEnumerable<FileInfo> file, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                List<WorkItem> work = new List<WorkItem>();

                foreach (var item in file)
                { work.AddRange(BusinessData.LibraryModel.Import(item)); }

                DoWork(work, WorkComplete);

                void WorkComplete(RunWorkerCompletedEventArgs args)
                {
                    BindingCompare bindingCompare = new BindingCompare();

                    managerData.AddRange(
                        BusinessData.LibraryModel.LibrarySources.Select(s => new BindingValue(s)).
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
                    if (BusinessData.LibraryModel.LibrarySources.Any(a => item.Equals(a))
                        || BusinessData.LibraryModel.LibrarySources.Any(a => item.Equals(a)))
                    { item.InModel = true; }
                    else { item.InModel = false; }
                }
            }

            public Boolean GetAuthorization(Enumerations.ButtonType command)
            {
                switch (command)
                {
                    case Enumerations.ButtonType.Default: return true;
                    case Enumerations.ButtonType.Add: return BusinessData.Authorization.IsLibraryAdmin;
                    case Enumerations.ButtonType.Delete: return BusinessData.Authorization.IsLibraryAdmin;
                    case Enumerations.ButtonType.OpenDatabase: return BusinessData.Authorization.IsLibraryAdmin;
                    case Enumerations.ButtonType.SaveDatabase: return BusinessData.Authorization.IsLibraryAdmin;
                    case Enumerations.ButtonType.DeleteDatabase: return BusinessData.Authorization.IsLibraryAdmin;
                    case Enumerations.ButtonType.HistoryDatabase: return BusinessData.Authorization.IsLibraryAdmin;
                    default: return false;
                }
            }
        }

        class BindingValue : IBindingPropertyChanged,
            ILibrarySourceIndex,
            IKeyEquality<ILibrarySourceIndex>
        {

            public Guid? LibraryId
            {
                get
                {
                    if (dataSource is ILibrarySourceValue sourceValue)
                    { return sourceValue.LibraryId; }
                    else { return null; }
                }
            }

            /// <inheritdoc/>
            public virtual Boolean HasValue { get { return LibraryId.HasValue && LibraryId != Guid.Empty; } }

            public String? LibraryTitle
            {
                get
                {
                    if (dataSource is ILibrarySourceValue value)
                    { return value.LibraryTitle; }
                    else { return null; }
                }
            }

            public String? LibraryDescription
            {
                get
                {
                    if (dataSource is ILibrarySourceValue value)
                    { return value.LibraryDescription; }
                    else { return null; }
                }
            }

            public String? AssemblyName
            {
                get
                {
                    if (dataSource is ILibrarySourceValue value)
                    { return value.AssemblyName; }
                    else { return null; }
                }
            }

            public String? SourceFile
            {
                get
                {
                    if (dataSource is ILibrarySourceValue value)
                    { return value.SourceFile; }
                    else { return null; }
                }
            }

            public DateTime? SourceDate
            {
                get
                {
                    if (dataSource is ILibrarySourceValue value)
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

            ILibrarySourceValue? dataSource;

            public BindingValue(ILibrarySourceValue value)
            {
                dataSource = value;
                value.PropertyChanged += Value_PropertyChanged;
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            private void Value_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
            {
                if (dataSource is ILibrarySourceValue sourceValue)
                {
                    if (e.PropertyName is nameof(ILibrarySourceValue.LibraryTitle))
                    { this.OnPropertyChanged(PropertyChanged,nameof(LibraryTitle)); }

                    if (e.PropertyName is nameof(ILibrarySourceValue.LibraryDescription))
                    { this.OnPropertyChanged(PropertyChanged, nameof(LibraryDescription)); }

                    if (e.PropertyName is nameof(ILibrarySourceValue.SourceFile))
                    { this.OnPropertyChanged(PropertyChanged, nameof(SourceFile)); }

                    if (e.PropertyName is nameof(ILibrarySourceValue.AssemblyName))
                    { this.OnPropertyChanged(PropertyChanged, nameof(AssemblyName)); }

                    if (e.PropertyName is nameof(ILibrarySourceValue.SourceDate))
                    { this.OnPropertyChanged(PropertyChanged, nameof(SourceDate)); }
                }
                else { }
            }

            public Boolean TryGetIndex([NotNullWhen(true)] out LibrarySourceIndex? result)
            {
                if (dataSource is ILibrarySourceValue value)
                { result = new LibrarySourceIndex(value); return true; }
                else { result = null; return false; }
            }

            public Boolean TryGetValue([NotNullWhen(true)] out ILibrarySourceValue? result)
            {
                if (dataSource is ILibrarySourceValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            #region IEquatable
            public Boolean Equals(ILibrarySourceIndex? other)
            { return dataSource is ILibrarySourceIndex value && new LibrarySourceIndex(value).Equals(other); }

            public Boolean Equals(BindingValue? other)
            {
                return other is BindingValue value
                    && LibraryId is Guid
                    && value.LibraryId is Guid
                    && Guid.Equals(LibraryId, value.LibraryId);
            }

            /// <inheritdoc/>
            /// <remarks>Object</remarks>
            public override Boolean Equals(object? obj)
            {
                return obj is BindingValue value
                    && LibraryId is Guid
                    && value.LibraryId is Guid
                    && Guid.Equals(LibraryId, value.LibraryId);
            }

            /// <inheritdoc/>
            public static Boolean operator ==(BindingValue left, ILibrarySourceIndex right)
            { return left.Equals(right); }

            /// <inheritdoc/>
            public static Boolean operator !=(BindingValue left, ILibrarySourceIndex right)
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
                if (LibraryId is Guid) { return LibraryId.GetHashCode(); }
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
