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
        {
            return true;
            //return bindingProcess.Current is IProcessValue current && ReferenceEquals(current, item); 
        }


        public Process()
        {
            InitializeComponent();

            formBinding = new FormBinding()
            {
                BindingAlias = bindingAlias,
                BindingProcess = bindingProcess,
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
            aliasAddCommand.Image = NavigationEnumeration.GetImage(ScopeType.ModelProcessAlias, CommandImageType.Add);
            aliasSelectCommand.Image = NavigationEnumeration.GetImage(ScopeType.ModelProcessAlias, CommandImageType.Select);
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
            ScopeNameList.Load(aliaseScopeColumn);

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

                // Alias Handling
                ScopeNameList.Load(aliaseScopeColumn);
                ScopeNameList.Load(aliasScopeData);

                aliasesData.AutoGenerateColumns = false;
                aliasesData.DataSource = bindingAlias;

                aliasScopeData.DataBindings.Add(new Binding(nameof(aliasScopeData.SelectedValue), bindingAlias, nameof(IProcessAliasValue.AliasScope), false, DataSourceUpdateMode.OnPropertyChanged) { DataSourceNullValue = ScopeNameList.NullValue });
                aliasNameData.DataBindings.Add(new Binding(nameof(aliasNameData.Text), bindingAlias, nameof(ProcessAliasValue.AliasPath), false, DataSourceUpdateMode.OnPropertyChanged));

                // Specialized Control Binding
                propertyData.BindTo(bindingProperty, formBinding.NewProperty);
                definitionData.BindTo(bindingDefinition, formBinding.NewDefinition);
                subjectArea.BindTo(formBinding.SubjectAreas.ToList, formBinding.AddSubjectArea, formBinding.RemoveSubjectArea);

                // Security
                IsLocked(formBinding.GetLocked());
                SetAuthorization(formBinding.GetAuthorization);
            }
        }
    }
}
