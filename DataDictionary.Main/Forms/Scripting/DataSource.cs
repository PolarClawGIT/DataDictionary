using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Dialogs;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

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
                DataObjectBinding = bindingDataObject,
                DoWork = base.DoWork
            };

            SetIcon(ScopeType.ScriptingData);
            SetTitle(bindingDataSource);
            SetRowState(bindingDataSource, bindingDataObject);

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

                objectData.AutoGenerateColumns = false;
                objectData.DataSource = bindingDataObject;
                objectPathData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDataObject, nameof(IDataObjectValue.DataPath)));
            }
        }

        private void ObjectPathData_Validating(object sender, CancelEventArgs e)
        {
            PathIndex path = new PathIndex(PathIndex.Parse(objectPathData.Text).ToArray());
            objectPathData.Text = path.MemberFullPath;
        }

        private void ObjectPathData_SelectCommand(object sender, EventArgs e)
        {
            using (SelectionDialog dialog = new SelectionDialog(this))
            {
                dialog.FilterScopes.Add(ScopeType.ModelAttribute);
                dialog.FilterScopes.Add(ScopeType.ModelEntity);
                dialog.FilterScopes.Add(ScopeType.ModelProcess);

                dialog.BuildData(formBinding.GetObjectPaths());

                if (dialog.ShowDialog(this) is DialogResult.OK)
                { formBinding.SetObjectPaths(dialog.SelectedByNamedScope().Select(s => s.Path)); }
            }
        }

        private void BindingDataObject_AddingNew(object sender, AddingNewEventArgs e)
        { e.NewObject = formBinding.NewObject(); }

        private void BindingDataObject_CurrentItemChanged(object sender, EventArgs e)
        { isInModelData.Checked = formBinding.TryGetValue(out INamedScopeValue? value); }
    }
}
