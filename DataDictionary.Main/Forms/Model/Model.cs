using DataDictionary.DataLayer.ModelData;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Model
{
    partial class Model : ApplicationBase, IApplicationDataForm<ModelKey>
    {
        public ModelKey DataKey { get; private set; }

        public bool IsOpenItem(object? item)
        { return DataKey.Equals(item); }

        public Model() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_SoftwareDefinitionModel;
            bindingModel.AllowNew = false;

            ModelItem data = new ModelItem();
            DataKey = new ModelKey(data);
            bindingModel.DataSource = new BindingList<ModelItem>() { data };
        }

        public Model(ModelItem data): this()
        {
            DataKey = new ModelKey(data);
            bindingModel.DataSource = new BindingView<ModelItem>(Program.Data.Models, w => DataKey.Equals(w));
        }

        private void Model_Load(object sender, EventArgs e)
        {
            bindingModel.CurrentItemChanged += DataChanged;
            UpdateRowState();

            ModelItem? nameBinding = null;
            DataBindings.Add(new Binding(nameof(Text), bindingModel, nameof(nameBinding.ModelTitle)));
            modelTitleData.DataBindings.Add(new Binding(nameof(modelTitleData.Text), bindingModel, nameof(nameBinding.ModelTitle)));
            modelDescriptionData.DataBindings.Add(new Binding(nameof(modelDescriptionData.Text), bindingModel, nameof(nameBinding.ModelDescription)));
        }

        public bool BindDataCore()
        {
            UpdateRowState();
            return true;
        }

        public void UnbindDataCore()
        {
            //throw new NotImplementedException();
        }

        private void DataChanged(object? sender, EventArgs e)
        { UpdateRowState(); }

        void UpdateRowState()
        {
            if (bindingModel.Current is ModelItem data)
            { RowState = data.RowState(); }
        }
    }
}
