using DataDictionary.BusinessLayer.Model;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Model
{
    partial class Model : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingModel.Current is IModelValue current && ReferenceEquals(current, item); }

        public Model() : base()
        {
            InitializeComponent();
            toolStrip.TransferItems(modelToolStrip,0);
        }

        public Model(IModelValue? model) : this()
        {
            if (model is null)
            { // Should never occur
                model = new ModelValue();
                BusinessData.Models.Add(model);
            }

            ModelIndex key = new ModelIndex(model);
            this.Icon = ImageEnumeration.GetIcon(model.Scope);

            bindingModel.DataSource = new BindingView<ModelValue>(BusinessData.Models, w => key.Equals(w));
            bindingModel.Position = 0;

            Setup(bindingModel);
        }

        private void Model_Load(object sender, EventArgs e)
        {
            IModelValue nameBinding;
            DataBindings.Add(new Binding(nameof(Text), bindingModel, nameof(nameBinding.ModelTitle)));
            modelTitleData.DataBindings.Add(new Binding(nameof(modelTitleData.Text), bindingModel, nameof(nameBinding.ModelTitle)));
            modelDescriptionData.DataBindings.Add(new Binding(nameof(modelDescriptionData.Text), bindingModel, nameof(nameBinding.ModelDescription)));
        }

        private void openModelManagerCommand_Click(object sender, EventArgs e)
        { Activate(() => new Forms.Model.ModelManager()); }
    }
}
