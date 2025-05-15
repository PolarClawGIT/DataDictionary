using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Forms.Model
{
    partial class Process : ApplicationData, IApplicationDataForm
    {
        FormBinding formBinding;

        public Boolean IsOpenItem(object? item)
        {
            return true;
            //return bindingEntity.Current is IEntityValue current && ReferenceEquals(current, item); 
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

        public Process(IProcessIndex? entity) : this()
        {
            //if (entity is null)
            //{ entity = formBinding.NewValue(); }
            //else { formBinding.SetPosition(entity); }
        }

        public Process(IProcessIndex entity, ITemporalIndex temporal) : this(entity)
        { 
            //formBinding.SetPosition(entity, temporal); needsData = true; 
        }


    }
}
