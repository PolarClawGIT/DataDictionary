using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.Main.Properties;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateManager
    {
        class FormBinding
        {
            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            BindingList<BindingValue> Bindings = new BindingList<BindingValue>();

            public void Load(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                var templates = ITemplateData.Create();
                var sources = IDataSourceData.Create();

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
                    BindingCompare bindingCompare = new BindingCompare();
                    Bindings.AddRange(
                        BusinessData.ScriptingTemplate.Templates.Select(s => new BindingValue(s)).
                        Union(templates.Select(s => new BindingValue(s)), bindingCompare).
                        Union(BusinessData.ScriptingDataSource.DataSources.Select(s => new BindingValue(s)), bindingCompare).
                        Union(sources.Select(s => new BindingValue(s)), bindingCompare));

                    foreach (var item in Bindings)
                    {
                        if (BusinessData.ScriptingTemplate.Templates.Any(a => item.Equals(a))
                            || BusinessData.ScriptingDataSource.DataSources.Any(a => item.Equals(a)))
                        { item.InModel = true; }

                        if (templates.Any(a => item.Equals(a))
                            || sources.Any(a => item.Equals(a)))
                        { item.InDatabase = true; }
                    }

                    if (onComplete is not null) { onComplete(args); }
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
