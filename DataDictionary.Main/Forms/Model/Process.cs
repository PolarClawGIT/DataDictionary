using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
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
            { process = formBinding.NewValue(); }
            else { formBinding.SetPosition(process); }
        }

        public Process(IProcessIndex process, ITemporalIndex temporal) : this(process)
        { formBinding.SetPosition(process, temporal); needsData = true; }

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

                argumentTitleData.DataBindings.Add(new Binding(nameof(argumentTitleData.Text), bindingArgument, nameof(IProcessArgumentValue.ArgumentTitle)));
                argumentDescriptionData.DataBindings.Add(new Binding(nameof(argumentDescriptionData.Text), bindingArgument, nameof(IProcessArgumentValue.ArgumentDescription)));
                argumentNameData.DataBindings.Add(new Binding(nameof(argumentNameData.Text), bindingArgument, nameof(IProcessArgumentValue.ArgumentName)));
                argumentTypeData.DataBindings.Add(new Binding(nameof(argumentTypeData.Text), bindingArgument, nameof(IProcessArgumentValue.ArgumentType)));
                argumentOrdinalPositionData.DataBindings.Add(new Binding(nameof(argumentOrdinalPositionData.Text), bindingArgument, nameof(IProcessArgumentValue.OrdinalPosition)));

                argumentIsInputData.DataBindings.Add(new Binding(nameof(argumentIsOutputData.Checked), bindingArgument, nameof(IProcessArgumentValue.IsOutput), true, DataSourceUpdateMode.OnValidation, false));
                argumentIsOutputData.DataBindings.Add(new Binding(nameof(argumentIsOutputData.Checked), bindingArgument, nameof(IProcessArgumentValue.IsInput), true, DataSourceUpdateMode.OnValidation, false));

                argumentLayout.Enabled = false;

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
        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);
        }

        private void ArgumentNewCommand_Click(object sender, EventArgs e)
        { bindingArgument.AddNew(); }

        private void ArgumentSelectCommand_Click(object sender, EventArgs e)
        {

        }

        private void ArgumentNameData_Validating(object sender, CancelEventArgs e)
        {
            if (formBinding.TryGetArgument(out ProcessArgumentValue? value))
            { value.ArgumentName = new PathIndex(PathIndex.Parse(argumentNameData.Text).ToArray()).MemberFullPath; }
        }

        private void ArgumentTypeData_Validating(object sender, CancelEventArgs e)
        {
            if (formBinding.TryGetArgument(out ProcessArgumentValue? value))
            { value.ArgumentType = new PathIndex(PathIndex.Parse(argumentTypeData.Text).ToArray()).MemberFullPath; }
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
    }
}
