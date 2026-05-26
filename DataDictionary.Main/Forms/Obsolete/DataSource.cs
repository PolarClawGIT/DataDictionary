using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Obsolete;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Dialogs;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Obsolete
{
    [Obsolete]
    partial class DataSource : ApplicationData
    {
        public override Boolean IsOpenItem(object? item)
        { return dataSourceIndex.Equals(item); }

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

            SetCommand(
                ButtonType.Delete,
                ButtonType.OpenDatabase,
                ButtonType.SaveDatabase,
                ButtonType.DeleteDatabase,
                ButtonType.HistoryDatabase);

            newObjectCommand.Image = ScopeType.ScriptingDataObject.GetImage(ButtonType.Add);
            selectObjectCommand.Image = ScopeType.ScriptingDataObject.GetImage(ButtonType.Select);
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

                objectPathData.DataBindings.Add(
                    new Binding(nameof(TextBox.Text),
                    bindingDataObject,
                    nameof(IDataObjectValue.ObjectPath))
                    .WithParse<PathIndex, String>(
                        (p) => p.MemberFullPath,
                        (s) => new PathIndex(PathIndex.Parse(s))));

                objectPathData.ReadOnly = true;

                // Security
                IsLocked(formBinding.GetLocked());
                SetAuthorization(formBinding.GetAuthorization);
            }
        }

        private void BindingDataObject_AddingNew(object sender, AddingNewEventArgs e)
        { e.NewObject = formBinding.NewObject(); }

        private void BindingDataObject_CurrentItemChanged(object sender, EventArgs e)
        {
            if (formBinding.TryGetValue(out INamedScopeValue? value))
            {
                isInModelData.Checked = true;
                objectPathData.ReadOnly = true;
                objectTitleData.Text = value.Title;
            }
            else
            {
                isInModelData.Checked = false;
                objectPathData.ReadOnly = false;
                objectTitleData.Text = String.Empty;
            }
        }

        private void NewObjectCommand_Click(object sender, EventArgs e)
        {
            bindingDataObject.AddNew();
            isInModelData.Checked = false;
            objectPathData.ReadOnly = false;
            objectTitleData.Text = String.Empty;
        }

        private void SelectObjectCommand_Click(object sender, EventArgs e)
        {
            using (SelectionDialog dialog = new SelectionDialog(this))
            {
                dialog.FilterScopes.Add(ScopeType.ModelAttribute);
                dialog.FilterScopes.Add(ScopeType.ModelEntity);
                dialog.FilterScopes.Add(ScopeType.ModelProcess);

                dialog.BuildData(formBinding.GetObjectPaths());

                if (dialog.ShowDialog(this) is DialogResult.OK)
                { formBinding.AddObjectPaths(dialog.SelectedByNamedScope().Select(s => s.Path)); }
            }
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);
            formBinding.Load(dataSourceIndex, onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            { }
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);
            formBinding.Delete(dataSourceIndex);
            IsLocked(formBinding.GetLocked());
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);
            formBinding.Save(dataSourceIndex, onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            { }
        }

        protected override void HistoryCommand_Click(Object sender, EventArgs e)
        {
            base.HistoryCommand_Click(sender, e);

            Activate(() => new ApplicationWide.HistoryView(formBinding.GetTemporal(dataSourceIndex))
            {
                OpenForm = (temporal) =>
                {
                    if (temporal.TryGetValue(out DataSourceValue? value))
                    { return new DataSource(value, new TemporalIndex(temporal)); }
                    else { throw new InvalidOperationException("Could not convert TemporalValue back to DataSourceValue"); }
                }
            });

        }
    }
}
