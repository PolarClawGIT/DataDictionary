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
    partial class Process : ApplicationData, IApplicationDataForm
    {
        FormBinding formBinding;
        Boolean needsData = false;

        public Boolean IsOpenItem(object? item)
        { return item is IProcessValue value && formBinding is not null && formBinding.GetIsOpen(value); }

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
            formBinding.Init();

            SetRowState(
                bindingProcess,
                bindingProperty,
                bindingDefinition,
                bindingAlias,
                bindingSubjectArea,
                bindingArgument);

            SetTitle(bindingProcess);
            SetCommand(ScopeType.ModelProcess,
                CommandImageType.Delete,
                CommandImageType.OpenDatabase,
                CommandImageType.SaveDatabase,
                CommandImageType.DeleteDatabase,
                CommandImageType.HistoryDatabase);

            argumentSelectCommand.Image = NavigationEnumeration.GetImage(ScopeType.ModelProcessArgument, CommandImageType.Select);
            argumentNewCommand.Image = NavigationEnumeration.GetImage(ScopeType.ModelProcessArgument, CommandImageType.Add);
        }

        public Process(IProcessIndex? process) : this()
        {
            if (process is null)
            { process = formBinding.Create(); }
            else { formBinding.SetPosition(process); }
        }

        public Process(IProcessIndex process, ITemporalIndex temporal) : this(process)
        {
            formBinding.SetPosition(process, temporal);
            needsData = true;
        }

        private void Process_Load(object sender, EventArgs e)
        {
            if (needsData)
            { formBinding.Load(onCompleting); }
            else { DoBinding(); }

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
                titleData.DataBindings.Add(new Binding(nameof(titleData.Text), bindingProcess, nameof(IProcessValue.ProcessTitle)));
                descriptionData.DataBindings.Add(new Binding(nameof(descriptionData.Text), bindingProcess, nameof(IProcessValue.ProcessDescription)));

                memberNameData.DataBindings.Add(new Binding(nameof(memberNameData.Text), bindingProcess, nameof(IProcessValue.ProcessName), false, DataSourceUpdateMode.OnPropertyChanged));

                // Argument Handling
                argumentData.AutoGenerateColumns = false;
                argumentData.DataSource = bindingArgument;

                argumentKnownAsData.DataBindings.Add(new Binding(nameof(argumentKnownAsData.Text), bindingArgument, nameof(IProcessArgumentValue.ArgumentKnownAs)));
                argumentNameData.DataBindings.Add(new Binding(nameof(argumentNameData.Text), bindingArgument, nameof(IProcessArgumentValue.ArgumentName)));
                argumentOrdinalPositionData.DataBindings.Add(new Binding(nameof(argumentOrdinalPositionData.Text), bindingArgument, nameof(IProcessArgumentValue.OrdinalPosition), true, DataSourceUpdateMode.OnPropertyChanged, String.Empty));

                argumentIsPassedData.DataBindings.Add(new Binding(nameof(argumentIsPassedData.Checked), bindingArgument, nameof(IProcessArgumentValue.IsPassed), true, DataSourceUpdateMode.OnPropertyChanged, false));
                argumentIsReturnedData.DataBindings.Add(new Binding(nameof(argumentIsReturnedData.Checked), bindingArgument, nameof(IProcessArgumentValue.IsReturned), true, DataSourceUpdateMode.OnPropertyChanged, false));
                argumentIsContributorData.DataBindings.Add(new Binding(nameof(argumentIsContributorData.Checked), bindingArgument, nameof(IProcessArgumentValue.IsContributor), true, DataSourceUpdateMode.OnPropertyChanged, false));
                argumentIsAlteredData.DataBindings.Add(new Binding(nameof(argumentIsAlteredData.Checked), bindingArgument, nameof(IProcessArgumentValue.IsAltered), true, DataSourceUpdateMode.OnPropertyChanged, false));
                argumentAsValueData.DataBindings.Add(new Binding(nameof(argumentAsValueData.Checked), bindingArgument, nameof(IProcessArgumentValue.AsValue), true, DataSourceUpdateMode.OnPropertyChanged, false));
                argumentAsReferenceData.DataBindings.Add(new Binding(nameof(argumentAsReferenceData.Checked), bindingArgument, nameof(IProcessArgumentValue.AsReference), true, DataSourceUpdateMode.OnPropertyChanged, false));

                argumentIsInputData.DataBindings.Add(new Binding(nameof(argumentIsOutputData.Checked), bindingArgument, nameof(IProcessArgumentValue.IsInput), true, DataSourceUpdateMode.OnPropertyChanged, false));
                argumentIsOutputData.DataBindings.Add(new Binding(nameof(argumentIsOutputData.Checked), bindingArgument, nameof(IProcessArgumentValue.IsOutput), true, DataSourceUpdateMode.OnPropertyChanged, false));

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

            formBinding.Remove();
            IsLocked(formBinding.GetLocked());
        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);

            formBinding.Remove();
            formBinding.Save(onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(formBinding.GetLocked()); }
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);

            formBinding.Load(onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(formBinding.GetLocked()); }
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);

            formBinding.Save(onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(formBinding.GetLocked()); }
        }

        protected override void HistoryCommand_Click(Object sender, EventArgs e)
        {
            base.HistoryCommand_Click(sender, e);

            Activate(() => new ApplicationWide.HistoryView(formBinding.GetTemporal())
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
