using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class DataSource : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return true; } // TODO: rig to current value

        FormBinding formBinding;
        DataSourceIndex dataSourceIndex = new DataSourceIndex();
        TemporalIndex? temporalIndex = null;

        public DataSource() : base()
        {
            InitializeComponent();

            formBinding = new FormBinding()
            {
                DataSourceBinding = bindingDataSource,
                DoWork = base.DoWork
            };

            SetIcon(ScopeType.ScriptingData);
            SetTitle(bindingDataSource);
            SetRowState(bindingDataSource);

            SetCommand(ScopeType.ScriptingData,
                CommandImageType.Delete,
                CommandImageType.OpenDatabase,
                CommandImageType.SaveDatabase,
                CommandImageType.DeleteDatabase,
                CommandImageType.HistoryDatabase);
        }

        public DataSource(IDataSourceIndex? dataSource) : this()
        {
            if (dataSource is IDataSourceIndex)
            { dataSourceIndex = new DataSourceIndex(dataSource); }
            else { dataSourceIndex = new DataSourceIndex(formBinding.NewValue()); }
        }

        public DataSource(IDataSourceIndex dataSource, ITemporalIndex temporal) : this(dataSource)
        { temporalIndex = new TemporalIndex(); }

        private void DataSource_Load(object sender, EventArgs e)
        {
            if (temporalIndex is null)
            {
                formBinding.Load(dataSourceIndex);
                DoBinding();
            }
            else
            { formBinding.Load(dataSourceIndex, temporalIndex, onCompleting); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is null)
                {
                    DoBinding();
                    SendMessage(new RefreshNavigation());
                }
            }

            void DoBinding()
            {
                titleData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDataSource, nameof(IDataSourceValue.DataSourceTitle)));
                descriptionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDataSource, nameof(IDataSourceValue.DataSourceDescription)));

            }
        }
    }
}
