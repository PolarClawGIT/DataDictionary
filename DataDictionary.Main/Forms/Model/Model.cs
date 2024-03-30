﻿using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Model
{
    partial class Model : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingModel.Current is IModelItem current && ReferenceEquals(current, item); }

        public Model() : base()
        {
            InitializeComponent();
            toolStrip.TransferItems(modelToolStrip,0);
        }

        public Model(IModelItem data) : this()
        {
            bindingModel.AllowNew = false;
            bindingModel.DataSource = new BindingList<IModelItem>() { data };
            bindingModel.Position = 0;

            Setup(bindingModel);
        }

        private void Model_Load(object sender, EventArgs e)
        {
            IModelItem nameBinding;
            DataBindings.Add(new Binding(nameof(Text), bindingModel, nameof(nameBinding.ModelTitle)));
            modelTitleData.DataBindings.Add(new Binding(nameof(modelTitleData.Text), bindingModel, nameof(nameBinding.ModelTitle)));
            modelDescriptionData.DataBindings.Add(new Binding(nameof(modelDescriptionData.Text), bindingModel, nameof(nameBinding.ModelDescription)));
        }

        private void openModelManagerCommand_Click(object sender, EventArgs e)
        { Activate(() => new Forms.Model.ModelManager()); }
    }
}
