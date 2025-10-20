using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.Main.Properties;
using DataDictionary.Resource;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateManager
    {
        class FormBinding
        {
            public required BindingSource ManagerBinding { private get; init; }
            BindingList<BindingValue> managerData { get; } = new BindingList<BindingValue>();

            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }
            public required Action OnRefresh { get; init; }

            public void Load(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                var templates = ITemplateData.Create();
                var sources = IDataSourceData.Create();
                ManagerBinding.RaiseListChangedEvents = false;

                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                if (Settings.Default.IsOnLineMode)
                {
                    work.Add(factory.OpenConnection());
                    work.AddRange(templates.Load(factory));
                    work.AddRange(sources.Load(factory));
                }

                DoWork(work, StartBinding);

                void StartBinding(RunWorkerCompletedEventArgs args)
                {
                    BuildData(templates, sources);

                    ManagerBinding.DataSource = managerData;
                    ManagerBinding.RaiseListChangedEvents = true;
                    ManagerBinding.ResetBindings(false);
                    if (onComplete is not null) { onComplete(args); }
                }

                void BuildData(ITemplateData templates, IDataSourceData sources)
                {
                    managerData.Clear();

                    BindingCompare bindingCompare = new BindingCompare();
                    managerData.AddRange(
                        BusinessData.Scripting.Templates.Select(s => new BindingValue(s)).
                        Union(templates.Select(s => new BindingValue(s)), bindingCompare).
                        Union(BusinessData.Scripting.DataSources.Select(s => new BindingValue(s)), bindingCompare).
                        Union(sources.Select(s => new BindingValue(s)), bindingCompare));

                    foreach (var item in managerData)
                    {
                        if (templates.Any(a => item.Equals(a))
                            || sources.Any(a => item.Equals(a)))
                        { item.InDatabase = true; }
                    }

                    RefreshInModel();
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

                if (binding.TryGetIndex(out TemplateIndex? template))
                { work.AddRange(BusinessData.Scripting.Load(factory, template)); }

                if (binding.TryGetIndex(out DataSourceIndex? dataSource))
                { work.AddRange(BusinessData.Scripting.Load(factory, dataSource)); }

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
                if (binding.TryGetIndex(out TemplateIndex? template))
                { BusinessData.Scripting.Remove(template); }

                if (binding.TryGetIndex(out DataSourceIndex? dataSource))
                { BusinessData.Scripting.Remove(dataSource); }

                RefreshInModel();
            }

            public void Save(BindingValue binding, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();
                work.Add(factory.OpenConnection());

                if (binding.TryGetIndex(out TemplateIndex? template))
                { work.AddRange(BusinessData.Scripting.Save(factory, template)); }

                if (binding.TryGetIndex(out DataSourceIndex? dataSource))
                { work.AddRange(BusinessData.Scripting.Save(factory, dataSource)); }

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

                if (binding.TryGetIndex(out TemplateIndex? template))
                {
                    work.AddRange(BusinessData.Scripting.Delete(template));
                    work.AddRange(BusinessData.Scripting.Save(factory, template));
                }

                if (binding.TryGetIndex(out DataSourceIndex? dataSource))
                {
                    work.AddRange(BusinessData.Scripting.Delete(dataSource));
                    work.AddRange(BusinessData.Scripting.Save(factory, dataSource));
                }

                DoWork(work, WorkComplete);

                void WorkComplete(RunWorkerCompletedEventArgs args)
                {
                    RefreshInModel();

                    if (onComplete is not null)
                    { onComplete(args); }
                }
            }

            private void RefreshInModel()
            {
                foreach (var item in managerData)
                {
                    if (BusinessData.Scripting.Templates.Any(a => item.Equals(a))
                        || BusinessData.Scripting.DataSources.Any(a => item.Equals(a)))
                    { item.InModel = true; }
                    else { item.InModel = false; }
                }
            }
        }

        class BindingValue : IBindingPropertyChanged,
            IDataSourceIndex, ITemplateIndex,
            IKeyEquality<IDataSourceIndex>, IKeyEquality<ITemplateIndex>
        {
            public Guid? Id
            {
                get
                {
                    if (dataSource is IDataSourceValue sourceValue)
                    { return sourceValue.DataSourceId; }
                    else if (template is ITemplateValue templateValue)
                    { return templateValue.TemplateId; }
                    else { return null; }
                }
            }

            public String? Title
            {
                get
                {
                    if (dataSource is IDataSourceValue sourceValue)
                    { return sourceValue.DataSourceTitle; }
                    else if (template is ITemplateValue templateValue)
                    { return templateValue.TemplateTitle; }
                    else { return null; }
                }
            }

            public String? Description
            {
                get
                {
                    if (dataSource is IDataSourceValue sourceValue)
                    { return sourceValue.DataSourceDescription; }
                    else if (template is ITemplateValue templateValue)
                    { return templateValue.TemplateDescription; }
                    else { return null; }
                }
            }

            public BindingValueType Type { get; private set; } = BindingValueType.Null;

            public Boolean InModel
            {
                get { return inModel; }
                set
                {
                    inModel = value;
                    IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, nameof(InModel));
                }
            }

            public Boolean InDatabase
            {
                get { return inDatabase; }
                set
                {
                    inDatabase = value;
                    IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, nameof(InDatabase));
                }
            }

            public Guid? DataSourceId
            {
                get
                {
                    if (dataSource is IDataSourceValue) { return dataSource.DataSourceId; }
                    else { return null; }
                }
            }

            public Guid? TemplateId
            {
                get
                {
                    if (template is ITemplateValue) { return template.TemplateId; }
                    else { return null; }
                }
            }

            IDataSourceValue? dataSource;
            ITemplateValue? template;
            private Boolean inModel;
            private Boolean inDatabase;

            public BindingValue(IDataSourceValue value)
            {
                dataSource = value;
                Type = BindingValueType.DataSource;
                value.PropertyChanged += Value_PropertyChanged;
            }

            public BindingValue(ITemplateValue value)
            {
                template = value;
                Type = BindingValueType.Template;
                value.PropertyChanged += Value_PropertyChanged;
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            private void Value_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
            {
                if (PropertyChanged is PropertyChangedEventHandler handler)
                {
                    if (dataSource is IDataSourceValue sourceValue)
                    {
                        if (e.PropertyName is nameof(IDataSourceValue.DataSourceTitle))
                        { handler(this, new PropertyChangedEventArgs(nameof(Title))); }

                        if (e.PropertyName is nameof(IDataSourceValue.DataSourceDescription))
                        { handler(this, new PropertyChangedEventArgs(nameof(Description))); }

                    }
                    else if (template is ITemplateValue templateValue)
                    {
                        if (e.PropertyName is nameof(ITemplateValue.TemplateTitle))
                        { handler(this, new PropertyChangedEventArgs(nameof(Title))); }

                        if (e.PropertyName is nameof(ITemplateValue.TemplateDescription))
                        { handler(this, new PropertyChangedEventArgs(nameof(Description))); }
                    }
                    else { }
                }
            }

            public Boolean TryGetIndex([NotNullWhen(true)] out DataSourceIndex? result)
            {
                if (dataSource is IDataSourceValue value)
                { result = new DataSourceIndex(value); return true; }
                else { result = null; return false; }
            }

            public Boolean TryGetIndex([NotNullWhen(true)] out TemplateIndex? result)
            {
                if (template is ITemplateValue value)
                { result = new TemplateIndex(value); return true; }
                else { result = null; return false; }
            }

            public Boolean TryGetValue([NotNullWhen(true)] out IDataSourceValue? result)
            {
                if (dataSource is IDataSourceValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryGetValue([NotNullWhen(true)] out ITemplateValue? result)
            {
                if (template is ITemplateValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            #region IEquatable
            public Boolean Equals(IDataSourceIndex? other)
            { return dataSource is IDataSourceIndex value && new DataSourceIndex(value).Equals(other); }

            public Boolean Equals(ITemplateIndex? other)
            { return template is ITemplateIndex value && new TemplateIndex(value).Equals(other); }

            public Boolean Equals(BindingValue? other)
            {
                return other is BindingValue value
                    && Id is Guid
                    && value.Id is Guid
                    && Guid.Equals(Id, value.Id);
            }

            /// <inheritdoc/>
            /// <remarks>Object</remarks>
            public override Boolean Equals(object? obj)
            {
                return obj is BindingValue value
                    && Id is Guid
                    && value.Id is Guid
                    && Guid.Equals(Id, value.Id);
            }

            /// <inheritdoc/>
            public static Boolean operator ==(BindingValue left, IDataSourceIndex right)
            { return left.Equals(right); }

            /// <inheritdoc/>
            public static Boolean operator !=(BindingValue left, IDataSourceIndex right)
            { return !left.Equals(right); }

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
                if (Id is Guid) { return Id.GetHashCode(); }
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

        enum BindingValueType
        {
            Null,
            Template,
            DataSource
        }
    }
}
