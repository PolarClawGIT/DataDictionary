using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Dialogs;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Model
{
    partial class Process : ApplicationData
    {
        public override Boolean IsOpenItem(object? item)
        { return item is IProcessIndex attribute && processIndex.Equals(attribute); }

        FormBinding formBinding;
        ProcessIndex processIndex = new ProcessIndex();
        TemporalIndex? temporalIndex = null; 

        public Process()
        {
            InitializeComponent();
            argumentLayout.Enabled = false;

            formBinding = new FormBinding()
            {
                BindingProcess = bindingProcess,
                BindingAlias = bindingAlias,
                BindingSubjectArea = bindingSubjectArea,
                BindingProperty = bindingProperty,
                BindingDefinition = bindingDefinition,
                BindingArgument = bindingArgument,
                DoWork = base.DoWork
            };

            SetIcon(ScopeType.ModelProcess);
            SetTitle(bindingProcess);

            SetRowState(
                bindingProcess,
                bindingProperty,
                bindingDefinition,
                bindingAlias,
                bindingSubjectArea,
                bindingArgument);

            SetCommand(
                ButtonType.Delete,
                ButtonType.OpenDatabase,
                ButtonType.SaveDatabase,
                ButtonType.DeleteDatabase,
                ButtonType.HistoryDatabase);

            argumentSelectCommand.Image = ScopeType.ModelProcessArgument.GetImage(ButtonType.Select);
            argumentNewCommand.Image = ScopeType.ModelProcessArgument.GetImage(ButtonType.Add);
        }

        public Process(IProcessIndex? process) : this()
        {
            if (process is IProcessIndex)
            { processIndex = new ProcessIndex(process); }
        }

        public Process(IProcessIndex process, ITemporalIndex temporal) : this(process)
        { temporalIndex = new TemporalIndex(); }

        private void Process_Load(object sender, EventArgs e)
        {

            if (temporalIndex is null)
            {
                if (processIndex.HasValue)
                { formBinding.Load(processIndex); }
                else
                {
                    if (formBinding.TryAddValue(out ProcessValue? value))
                    {
                        processIndex = new ProcessIndex(value);
                        formBinding.Load(processIndex);
                        SendMessage(new RefreshNavigation());
                    }
                }

                if (formBinding.TryGetValue(out ProcessValue? _))
                { DoBinding(); }
                else { IsLocked(true); }
            }
            else
            { formBinding.Load(processIndex, temporalIndex, onCompleting); }

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
                titleData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingProcess, nameof(IProcessValue.ProcessTitle)));
                descriptionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingProcess, nameof(IProcessValue.ProcessDescription)));

                memberNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingProcess, nameof(IProcessValue.ProcessName), false, DataSourceUpdateMode.OnPropertyChanged));

                // Argument Handling
                argumentData.AutoGenerateColumns = false;
                argumentData.DataSource = bindingArgument;

                argumentKnownAsData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingArgument, nameof(IProcessArgumentValue.ArgumentKnownAs)));
                argumentNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingArgument, nameof(IProcessArgumentValue.ArgumentName)));
                argumentOrdinalPositionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingArgument, nameof(IProcessArgumentValue.OrdinalPosition), true, DataSourceUpdateMode.OnPropertyChanged, String.Empty));

                argumentIsPassedData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingArgument, nameof(IProcessArgumentValue.IsPassed), true, DataSourceUpdateMode.OnPropertyChanged, false));
                argumentIsReturnedData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingArgument, nameof(IProcessArgumentValue.IsReturned), true, DataSourceUpdateMode.OnPropertyChanged, false));
                argumentIsContributorData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingArgument, nameof(IProcessArgumentValue.IsContributor), true, DataSourceUpdateMode.OnPropertyChanged, false));
                argumentIsAlteredData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingArgument, nameof(IProcessArgumentValue.IsAltered), true, DataSourceUpdateMode.OnPropertyChanged, false));
                argumentAsValueData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingArgument, nameof(IProcessArgumentValue.AsValue), true, DataSourceUpdateMode.OnPropertyChanged, false));
                argumentAsReferenceData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingArgument, nameof(IProcessArgumentValue.AsReference), true, DataSourceUpdateMode.OnPropertyChanged, false));

                argumentIsInputData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingArgument, nameof(IProcessArgumentValue.IsInput), true, DataSourceUpdateMode.OnPropertyChanged, false));
                argumentIsOutputData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingArgument, nameof(IProcessArgumentValue.IsOutput), true, DataSourceUpdateMode.OnPropertyChanged, false));

                // Specialized Control Binding
                propertyData.BindTo(bindingProperty, formBinding.NewProperty);
                definitionData.BindTo(bindingDefinition, formBinding.NewDefinition);
                subjectArea.BindTo(formBinding.SubjectAreas.ToList, formBinding.AddSubjectArea, formBinding.RemoveSubjectArea);
                aliasData.BindTo(bindingAlias, formBinding.NewAlias,
                    ScopeType.ModelProcess,
                    ScopeType.DatabaseFunction, ScopeType.DatabaseProcedure,
                    ScopeType.LibraryTypeEvent, ScopeType.LibraryTypeMethod);

                // Security
                IsLocked(formBinding.GetLocked());
                SetAuthorization(formBinding.GetAuthorization);
            }
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            formBinding.Remove(processIndex);
            IsLocked(formBinding.GetLocked());
        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);

            formBinding.Remove(processIndex);
            formBinding.Save(processIndex, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(formBinding.GetLocked()); }
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);

            formBinding.Load(processIndex, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(formBinding.GetLocked()); }
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);

            formBinding.Save(processIndex, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(formBinding.GetLocked()); }
        }

        protected override void HistoryCommand_Click(Object sender, EventArgs e)
        {
            base.HistoryCommand_Click(sender, e);

            Activate(() => new ApplicationWide.HistoryView(formBinding.GetTemporal(processIndex))
            {
                OpenForm = (temporal) =>
                {
                    if (temporal.TryGetValue(out ProcessValue? process))
                    { return new Process(process, new TemporalIndex(temporal)); }
                    else { throw new InvalidOperationException("Could not convert TemporalValue back to EntityValue"); }
                }
            });
        }

        private void ArgumentNewCommand_Click(object sender, EventArgs e)
        { bindingArgument.AddNew(); }

        private void ArgumentSelectCommand_Click(object sender, EventArgs e)
        {
            if (bindingArgument.DataSource is IList<ProcessArgumentValue> arguments)
            {
                using (SelectionDialog dialog = new SelectionDialog(this))
                {
                    dialog.FilterScopes.Add(ScopeType.ModelAttribute);
                    dialog.FilterScopes.Add(ScopeType.ModelEntity);
                    dialog.FilterScopes.Add(ScopeType.ModelEntityAttribute);
                    dialog.FilterScopes.Add(ScopeType.ModelProcess);
                    IEnumerable<PathIndex> selected = arguments.Select(s => s.ArgumentPath);

                    dialog.BuildData(selected, GetDescription);

                    if (dialog.ShowDialog(this) is DialogResult.OK)
                    {
                        foreach (INamedScopeValue item in dialog.SelectedByNamedScope())
                        { formBinding.AddArgument(item.Path); }

                        bindingArgument.ResetCurrentItem();
                    }
                }
            }

            String GetDescription(INamedScopeSourceValue value)
            {   // Needed a physical method rather then a Lambda expression.
                // Properties don't get passed as expected.
                // I needed the property passed by Reference and that did not work.
                if (value is AttributeValue attribute)
                { return attribute.AttributeDescription ?? String.Empty; }

                else if (value is EntityValue entity)
                { return entity.EntityDescription ?? String.Empty; }

                else if (value is EntityAttributeValue entityAttribute)
                { return entityAttribute.AttributeDescription ?? String.Empty; }

                else if (value is ProcessValue process)
                { return process.ProcessDescription ?? String.Empty; }

                else { return String.Empty; }
            }
        }

        private void ArgumentNameData_Validating(object sender, CancelEventArgs e)
        {
            if (formBinding.TryGetArgument(out ProcessArgumentValue? value))
            { value.ArgumentName = new PathIndex(PathIndex.Parse(argumentNameData.Text).ToArray()).MemberFullPath; }
        }

        private void BindingArgument_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = formBinding.NewArgument();
            argumentLayout.Enabled = true;
        }

        private void BindingArgument_CurrentChanged(object sender, EventArgs e)
        {
            if (formBinding.TryGetArgument(out ProcessArgumentValue? value))
            { argumentLayout.Enabled = true; }
            else { argumentLayout.Enabled = false; }
        }

        private void MemberNameData_Validating(object sender, CancelEventArgs e)
        {
            PathIndex path = new PathIndex(PathIndex.Parse(memberNameData.Text).ToArray());
            memberNameData.Text = path.MemberFullPath;
        }
    }
}
