using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.DbWorkItem;
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

            public FormBinding() : base()
            { }

            public void Load(DataSourceIndex dataSource)
            {
                DataSourceBinding.RaiseListChangedEvents = false;
                DataSources = new BindingView<DataSourceValue>(data.DataSources, w => dataSource.Equals(w));
                DataSourceBinding.DataSource = DataSources;
                DataSourceBinding.RaiseListChangedEvents = true;
                DataSourceBinding.ResetBindings(false);
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

            public Boolean TryGetValue([NotNullWhen(true)] out DataSourceValue? result)
            {
                if (DataSourceBinding.Position >= 0
                    && DataSourceBinding.Current is DataSourceValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }
        }

    }
}
