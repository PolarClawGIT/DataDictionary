using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class ScriptingManager
    {
        class FormBinding : PresenterDatabase<TemplateIndex>
        {
            ITemplateData modelData = BusinessData.Templates;
            ITemplateData databaseData = ITemplateData.Create();
            BindingData managerValues = new BindingData();

            public DataBinding<BindingValue> ManagerData { get; }

            public FormBinding(BindingSource managerBinding) : base()
            { ManagerData = new DataBinding<BindingValue>(managerBinding, () => managerValues); }

            public void LoadValue()
            {
                managerValues.Clear();
                BindingCompare bindingCompare = new BindingCompare();

                managerValues.AddRange(
                    modelData.Select(s => new BindingValue(s)).
                    Union(databaseData.Select(s => new BindingValue(s)), bindingCompare));

                foreach (var item in managerValues)
                {
                    item.InDatabase = false;
                    item.InModel = false;

                    if (databaseData.Any(a => item.Equals(a)))
                    { item.InDatabase = true; }

                    if (modelData.Any(a => item.Equals(a)))
                    { item.InModel = true; }
                }

                ManagerData.LoadBinding();
            }

            public override void LoadValue(TemplateIndex key)
            {
                foreach (BindingValue item in managerValues.Where(w => key.Equals(w)))
                {
                    item.InDatabase = false;
                    item.InModel = false;

                    if (databaseData.Any(a => item.Equals(a)))
                    { item.InDatabase = true; }

                    if (modelData.Any(a => item.Equals(a)))
                    { item.InModel = true; }
                }
            }

            public virtual void LoadData(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(LoadWork(factory));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    LoadValue();
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            protected IReadOnlyList<WorkItem> LoadWork(IDatabaseWork factory)
            { return databaseData.Load(factory); }

            protected override IReadOnlyList<WorkItem> LoadWork(IDatabaseWork factory, TemplateIndex key)
            { return modelData.Load(factory, key); }

            protected override IReadOnlyList<WorkItem> TemporalWork(IDatabaseWork factory, TemplateIndex key, TemporalIndex temporal)
            { throw new NotImplementedException(); }

            protected override IReadOnlyList<WorkItem> SaveWork(IDatabaseWork factory, TemplateIndex key)
            { return modelData.Save(factory, key); }

            protected override IReadOnlyList<WorkItem> DeleteWork(TemplateIndex key)
            { return modelData.Delete(key); }

            public ITemporalData GetTemporal()
            { return databaseData.GetTemporal(); }
        }


        class BindingData : BindingList<BindingValue>, IBindingList<BindingValue>
        { }

        class BindingValue : IBindingPropertyChanged,
            ITemplateIndex, IKeyEquality<ITemplateIndex>,
            IBindingRowState
        {
            ITemplateValue dataSource;

            public Guid? TemplateId { get { return dataSource.TemplateId; } }

            /// <inheritdoc/>
            public virtual Boolean HasValue { get { return TemplateId.HasValue && TemplateId != Guid.Empty; } }

            public String? TemplateTitle { get { return dataSource.TemplateTitle; } }

            public String? TemplateDescription { get { return dataSource.TemplateDescription; } }

            public Boolean InModel
            {
                get { return field; }
                set
                {
                    field = value;
                    this.OnPropertyChanged(PropertyChanged, nameof(InModel));
                }
            }

            public Boolean InDatabase
            {
                get { return field; }
                set
                {
                    field = value;
                    this.OnPropertyChanged(PropertyChanged, nameof(InDatabase));
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;
            public event EventHandler<RowStateEventArgs>? RowStateChanged;

            public BindingValue(ITemplateValue value)
            {
                dataSource = value;
                value.PropertyChanged += Value_PropertyChanged;
            }

            private void Value_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
            {
                if (dataSource is ITemplateValue sourceValue)
                {
                    if (e.PropertyName is nameof(ITemplateValue.TemplateTitle))
                    { this.OnPropertyChanged(PropertyChanged, nameof(TemplateTitle)); }

                    if (e.PropertyName is nameof(ITemplateValue.TemplateDescription))
                    { this.OnPropertyChanged(PropertyChanged, nameof(TemplateDescription)); }
                }
                else { }
            }

            #region IEquatable
            public Boolean Equals(ITemplateIndex? other)
            { return dataSource is ITemplateIndex value && new TemplateIndex(value).Equals(other); }

            public Boolean Equals(BindingValue? other)
            {
                return other is BindingValue value
                    && TemplateId is Guid
                    && value.TemplateId is Guid
                    && Guid.Equals(TemplateId, value.TemplateId);
            }

            /// <inheritdoc/>
            /// <remarks>Object</remarks>
            public override Boolean Equals(object? obj)
            {
                return obj is BindingValue value
                    && TemplateId is Guid
                    && value.TemplateId is Guid
                    && Guid.Equals(TemplateId, value.TemplateId);
            }

            /// <inheritdoc/>
            public static Boolean operator ==(BindingValue left, ITemplateIndex right)
            { return left.Equals(right); }

            /// <inheritdoc/>
            public static Boolean operator !=(BindingValue left, ITemplateIndex right)
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
                if (TemplateId is Guid) { return TemplateId.GetHashCode(); }
                else { return Guid.Empty.GetHashCode(); }
            }
            #endregion

            public override String ToString()
            { return TemplateTitle ?? String.Empty; }

            public System.Data.DataRowState RowState()
            { throw new NotImplementedException(); }
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
