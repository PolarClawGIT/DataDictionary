using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.Main.Properties;
using DataDictionary.Resource;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Model
{
    partial class ModelManager
    {
        class FormBinding
        {
            public required BindingSource ManagerBinding { private get; init; }

            BindingList<BindingValue> managerData { get; } = new BindingList<BindingValue>();

            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }
            public required Action OnRefresh { get; init; }

            public void Load(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                var models = IModelData.Create();
                ManagerBinding.RaiseListChangedEvents = false;

                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                if (Settings.Default.IsOnLineMode)
                {
                    work.Add(factory.OpenConnection());
                    work.AddRange(models.Load(factory));
                }

                DoWork(work, StartBinding);

                void StartBinding(RunWorkerCompletedEventArgs args)
                {
                    managerData.Clear();

                    BindingCompare bindingCompare = new BindingCompare();
                    managerData.AddRange(
                        BusinessData.Model.Models.Select(s => new BindingValue(s)).
                        Union(models.Select(s => new BindingValue(s)), bindingCompare));

                    foreach (var item in managerData)
                    {
                        if (models.Any(a => item.Equals(a)))
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

            public void Create()
            { BusinessData.Create(); }

            public void Load(BindingValue binding, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();
                work.Add(factory.OpenConnection());

                if (binding.TryGetIndex(out ModelIndex? model))
                { work.AddRange(BusinessData.Load(factory, model)); }

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
                if (binding.TryGetIndex(out ModelIndex? model))
                { BusinessData.Remove(model); }

                RefreshInModel();
                OnRefresh();
            }

            public void Save(BindingValue binding, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();
                work.Add(factory.OpenConnection());

                if (binding.TryGetIndex(out ModelIndex? model))
                { work.AddRange(BusinessData.Save(factory, model)); }

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

                if (binding.TryGetIndex(out ModelIndex? model))
                {
                    work.AddRange(BusinessData.Delete(model));
                    work.AddRange(BusinessData.Model.Save(factory, model));
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

            private void RefreshInModel()
            {
                foreach (var item in managerData)
                {
                    if (BusinessData.Model.Models.Any(a => item.Equals(a))
                        || BusinessData.Model.Models.Any(a => item.Equals(a)))
                    { item.InModel = true; }
                    else { item.InModel = false; }
                }
            }

            public Boolean GetAuthorization(Enumerations.ButtonType command)
            {
                switch (command)
                {
                    case Enumerations.ButtonType.Default: return true;
                    case Enumerations.ButtonType.Add: return BusinessData.Authorization.IsModelAdmin;
                    case Enumerations.ButtonType.Delete: return BusinessData.Authorization.IsModelAdmin;
                    case Enumerations.ButtonType.OpenDatabase: return BusinessData.Authorization.IsModelAdmin;
                    case Enumerations.ButtonType.SaveDatabase: return BusinessData.Authorization.IsModelAdmin;
                    case Enumerations.ButtonType.DeleteDatabase: return BusinessData.Authorization.IsModelAdmin;
                    case Enumerations.ButtonType.HistoryDatabase: return BusinessData.Authorization.IsModelAdmin;
                    default: return false;
                }
            }
        }

        class BindingValue : IBindingPropertyChanged,
            IModelIndex,
            IKeyEquality<IModelIndex>
        {

            public Guid? ModelId
            {
                get
                {
                    if (dataSource is IModelValue sourceValue)
                    { return sourceValue.ModelId; }
                    else { return null; }
                }
            }

            public String? ModelTitle
            {
                get
                {
                    if (dataSource is IModelValue value)
                    { return value.ModelTitle; }
                    else { return null; }
                }
            }

            public String? ModelDescription
            {
                get
                {
                    if (dataSource is IModelValue value)
                    { return value.ModelDescription; }
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

            IModelValue? dataSource;

            public BindingValue(IModelValue value)
            {
                dataSource = value;
                value.PropertyChanged += Value_PropertyChanged;
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            private void Value_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
            {
                if (dataSource is IModelValue sourceValue)
                {
                    if (e.PropertyName is nameof(IModelValue.ModelTitle))
                    { this.OnPropertyChanged(PropertyChanged, nameof(ModelTitle)); }

                    if (e.PropertyName is nameof(IModelValue.ModelDescription))
                    { this.OnPropertyChanged(PropertyChanged, nameof(ModelDescription)); }
                }
                else { }
            }

            public Boolean TryGetIndex([NotNullWhen(true)] out ModelIndex? result)
            {
                if (dataSource is IModelValue value)
                { result = new ModelIndex(value); return true; }
                else { result = null; return false; }
            }

            public Boolean TryGetValue([NotNullWhen(true)] out IModelValue? result)
            {
                if (dataSource is IModelValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            #region IEquatable
            public Boolean Equals(IModelIndex? other)
            { return dataSource is IModelIndex value && new ModelIndex(value).Equals(other); }

            public Boolean Equals(BindingValue? other)
            {
                return other is BindingValue value
                    && ModelId is Guid
                    && value.ModelId is Guid
                    && Guid.Equals(ModelId, value.ModelId);
            }

            /// <inheritdoc/>
            /// <remarks>Object</remarks>
            public override Boolean Equals(object? obj)
            {
                return obj is BindingValue value
                    && ModelId is Guid
                    && value.ModelId is Guid
                    && Guid.Equals(ModelId, value.ModelId);
            }

            /// <inheritdoc/>
            public static Boolean operator ==(BindingValue left, IModelIndex right)
            { return left.Equals(right); }

            /// <inheritdoc/>
            public static Boolean operator !=(BindingValue left, IModelIndex right)
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
                if (ModelId is Guid) { return ModelId.GetHashCode(); }
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
