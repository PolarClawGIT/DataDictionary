using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.Main.Properties;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
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

            public Boolean TryGetValue([NotNullWhen(true)] out BindingValue? result)
            {
                if (ManagerBinding.Position >= 0 && ManagerBinding.Current is BindingValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public void Load(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                var templates = ITemplateData.Create();
                ManagerBinding.RaiseListChangedEvents = false;

                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                if (Settings.Default.IsOnLineMode)
                {
                    work.Add(factory.OpenConnection());
                    work.AddRange(templates.Load(factory));
                }

                DoWork(work, StartBinding);

                void StartBinding(RunWorkerCompletedEventArgs args)
                {
                    managerData.Clear();

                    BindingCompare bindingCompare = new BindingCompare();
                    managerData.AddRange(
                        BusinessData.Templates.Select(s => new BindingValue(s)).
                        Union(templates.Select(s => new BindingValue(s)), bindingCompare));

                    foreach (var item in managerData)
                    {
                        if (templates.Any(a => item.Equals(a)))
                        { item.InDatabase = true; }
                    }

                    RefreshInModel();

                    ManagerBinding.DataSource = managerData;
                    ManagerBinding.RaiseListChangedEvents = true;
                    ManagerBinding.ResetBindings(false);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public void Remove(BindingValue value)
            {
                TemplateIndex key = new TemplateIndex(value);
                BusinessData.Templates.Remove(key);

                RefreshInModel();
                OnRefresh();
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
        }

        class BindingValue : IBindingPropertyChanged,
            ITemplateIndex,
            IKeyEquality<ITemplateIndex>
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
