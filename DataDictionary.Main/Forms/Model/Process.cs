using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;

namespace DataDictionary.Main.Forms.Model
{
    partial class Process : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        {
            return true;
            //return bindingEntity.Current is IEntityValue current && ReferenceEquals(current, item); 
        }


        public Process()
        {
            InitializeComponent();
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
