using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
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
    partial class DataSource
    {
        class FormBinding
        {
            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            IDataSource data = BusinessData.ScriptingDataSource;

            public required BindingSource DataSourceBinding { private get; init; }
            BindingView<DataSourceValue> DataSources =
                new BindingView<DataSourceValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource DataObjectBinding { private get; init; }
            BindingView<DataObjectValue> DataObjects =
                new BindingView<DataObjectValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };


            public FormBinding() : base()
            { }

            public void Load(DataSourceIndex dataSource)
            {
                DataSourceBinding.RaiseListChangedEvents = false;
                DataObjectBinding.RaiseListChangedEvents = false;
                DataSources = new BindingView<DataSourceValue>(data.DataSources, w => dataSource.Equals(w));
                DataObjects = new BindingView<DataObjectValue>(data.DataObjects, w => dataSource.Equals(w));
                DataSourceBinding.DataSource = DataSources;
                DataObjectBinding.DataSource = DataObjects;
                DataSourceBinding.RaiseListChangedEvents = true;
                DataObjectBinding.RaiseListChangedEvents = true;
                DataSourceBinding.ResetBindings(false);
                DataObjectBinding.ResetBindings(false);
            }

            public void Load(DataSourceIndex dataSource, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();
                DataSourceBinding.RaiseListChangedEvents = false;

                work.Add(factory.OpenConnection());
                work.Add(new WorkItem() { DoWork = () => { data = BusinessData.ScriptingDataSource; } });
                work.AddRange(data.Delete(dataSource));
                work.AddRange(data.Load(factory, dataSource));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(dataSource);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public void Load(DataSourceIndex dataSource, TemporalIndex temporal, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();
                DataSourceBinding.RaiseListChangedEvents = false;

                work.Add(factory.OpenConnection());
                work.Add(new WorkItem() { DoWork = () => { data = IDataSource.Create(); } });
                work.AddRange(data.Load(factory, dataSource, temporal));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(dataSource);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public DataSourceValue NewValue()
            {
                DataSourceValue result = new DataSourceValue();
                data.DataSources.Add(result);

                return result;
            }

            public DataObjectValue NewObject(PathIndex? path = null)
            {
                if (TryGetValue(out DataSourceValue? value))
                { return new DataObjectValue(value) { ObjectPath = path ?? new PathIndex() }; }
                else { throw new InvalidOperationException("Current DataSourceValue not defined"); }
            }

            public Boolean TryGetValue([NotNullWhen(true)] out DataSourceValue? result)
            {
                if (DataSourceBinding.Position >= 0
                    && DataSourceBinding.Current is DataSourceValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryGetValue([NotNullWhen(true)] out DataObjectValue? result)
            {
                if (DataObjectBinding.Position >= 0
                    && DataObjectBinding.Current is DataObjectValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryGetValue([NotNullWhen(true)] out INamedScopeValue? result)
            {
                if (TryGetValue(out DataObjectValue? value)
                    && BusinessData.NamedScope.PathKeys(value.ObjectPath).FirstOrDefault() is NamedScopeIndex index)
                { result = BusinessData.NamedScope.GetValue(index); return true; }
                else { result = null; return false; }
            }

            public IEnumerable<PathIndex> GetObjectPaths()
            { return DataObjects.OfType<IPathIndex>().Select(s => s.Path); }

            public void SetObjectPaths(IEnumerable<PathIndex> newValues)
            {
                var current = DataObjects.OfType<IPathIndex>().Select(s => s.Path);

                foreach (DataObjectValue deleteItem in
                    DataObjects.
                    Where(w => current.Except(newValues).Any(a => w.ObjectPath.Equals(a))).
                    ToList())
                { DataObjects.Remove(deleteItem); }

                foreach (PathIndex addItem in newValues.Except(current))
                { DataObjects.Add(NewObject(addItem)); }
            }
        }

    }
}
