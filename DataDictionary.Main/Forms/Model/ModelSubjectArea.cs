﻿using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.ModelData.SubjectArea;
using DataDictionary.Main.Properties;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Domain
{
    partial class ModelSubjectArea : ApplicationBase, IApplicationDataForm<ModelSubjectAreaKey>
    {
        public required ModelSubjectAreaKey DataKey { get; init; }

        public ModelSubjectArea() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Diagram;

            deleteItemCommand.Click += DeleteItemCommand_Click;
            deleteItemCommand.Image = Resources.DeleteDiagram;
            deleteItemCommand.ToolTipText = "Remove the Subject Area";
        }

        private void DomainSubjectArea_Load(object sender, EventArgs e)
        {
            (this as IApplicationDataBind).BindData();
        }

        public Boolean BindDataCore()
        {
            bindingSubject.DataSource = new BindingView<ModelSubjectAreaItem>(Program.Data.ModelSubjectAreas, w => DataKey.Equals(w));
            bindingSubject.Position = 0;
            bindingSubject.CurrentItemChanged += DataChanged;

            if (Program.Data.ModelSubjectAreas.FirstOrDefault(w => DataKey.Equals(w)) is ModelSubjectAreaItem data)
            {
                subjectAreaTitleData.DataBindings.Add(new Binding(nameof(subjectAreaTitleData.Text), data, nameof(data.SubjectAreaTitle)));
                subjectAreaDescriptionData.DataBindings.Add(new Binding(nameof(subjectAreaDescriptionData.Text), data, nameof(data.SubjectAreaDescription)));

                attributeData.AutoGenerateColumns = false;
                attributeData.DataSource = new BindingView<DomainAttributeItem>(Program.Data.DomainAttributes, w => DataKey.Equals(w));

                entityData.AutoGenerateColumns = false;
                entityData.DataSource = new BindingView<DomainEntityItem>(Program.Data.DomainEntities, w => DataKey.Equals(w));

                deleteItemCommand.Enabled = true;

                RowState = data.RowState();
                return true;
            }
            else
            {
                deleteItemCommand.Enabled = false;
                this.IsLocked(true);
                return false;
            }
        }

        private void DataChanged(object? sender, EventArgs e)
        {
            if (bindingSubject.Current is ModelSubjectAreaItem data)
            { RowState = data.RowState(); }
        }

        public void UnbindDataCore()
        {
            bindingSubject.DataMemberChanged -= DataChanged;

            subjectAreaTitleData.DataBindings.Clear();
            subjectAreaDescriptionData.DataBindings.Clear();
            attributeData.DataSource = null;
            entityData.DataSource = null;
            bindingSubject.DataSource = null;
        }

        private void DeleteItemCommand_Click(object? sender, EventArgs e)
        {
            if (bindingSubject.Current is ModelSubjectAreaItem data)
            {
                this.IsLocked(true);
                ModelSubjectAreaKey key = new ModelSubjectAreaKey(data);

                Program.Data.ModelSubjectAreas.Remove(data);
                RowState = data.RowState();
            }
        }

    }
}
