using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using System.ComponentModel;
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
            SetRowState(bindingModel);
            SetTitle(bindingModel);
        }

        public Model(IModelValue? model) : this()
        {
            if (model is null)
            { // Should never occur
                model = new ModelValue();
                BusinessData.Model.Models.Add(model);
            }

            ModelIndex key = new ModelIndex(model);
            IBindingList data = new BindingView<ModelValue>(BusinessData.Model.Models, w => key.Equals(w));
            data.ListChanged += ListChanged;

            bindingModel.DataSource = data;
            bindingModel.Position = 0;

            void ListChanged(Object? sender, ListChangedEventArgs e)
            {
                // This addresses an invalid operation exception fired by CurrencyManager.FindGoodRow on an empty list
                if (e.ListChangedType is ListChangedType.ItemDeleted
                    && sender is IBindingList values
                    && values.Count is 0)
                { bindingModel.RaiseListChangedEvents = false; }
            }
        }

        private void Model_Load(object sender, EventArgs e)
        {
            IModelValue nameBinding;
            DataBindings.Add(new Binding(nameof(Form.Text), bindingModel, nameof(nameBinding.ModelTitle)));
            modelTitleData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingModel, nameof(nameBinding.ModelTitle)));
            modelDescriptionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingModel, nameof(nameBinding.ModelDescription)));
        }
    }
}
