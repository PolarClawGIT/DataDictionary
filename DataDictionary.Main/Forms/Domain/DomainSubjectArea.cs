using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.DomainData.SubjectArea;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Domain
{
    partial class DomainSubjectArea : ApplicationBase, IApplicationDataForm<DomainSubjectAreaKey>
    {
        public required DomainSubjectAreaKey DataKey { get; init; }

        public DomainSubjectArea() : base()
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
            if (Program.Data.DomainSubjectAreas.FirstOrDefault(w => DataKey.Equals(w)) is DomainSubjectAreaItem data)
            {
                subjectAreaTitleData.DataBindings.Add(new Binding(nameof(subjectAreaTitleData.Text), data, nameof(data.SubjectAreaTitle)));
                subjectAreaDescriptionData.DataBindings.Add(new Binding(nameof(subjectAreaDescriptionData.Text), data, nameof(data.SubjectAreaDescription)));

                attributeData.AutoGenerateColumns = false;
                attributeData.DataSource = new BindingView<DomainAttributeItem>(Program.Data.DomainAttributes, w => DataKey.Equals(w));

                entityData.AutoGenerateColumns = false;
                entityData.DataSource = new BindingView<DomainEntityItem>(Program.Data.DomainEntities, w => DataKey.Equals(w));

                deleteItemCommand.Enabled = true;
                return true;
            }
            else
            {
                deleteItemCommand.Enabled = false;
                return false;
            }
        }

        public void UnbindDataCore()
        {
            subjectAreaTitleData.DataBindings.Clear();
            subjectAreaDescriptionData.DataBindings.Clear();
            attributeData.DataSource = null;
            entityData.DataSource = null;
        }

        private void DeleteItemCommand_Click(object? sender, EventArgs e)
        {
            if (Program.Data.DomainSubjectAreas.FirstOrDefault(w => DataKey.Equals(w)) is DomainSubjectAreaItem data)
            {
                DomainSubjectAreaKey key = new DomainSubjectAreaKey(data);
                this.UnbindData();
                this.IsLocked(true);

                foreach (DomainAttributeItem item in Program.Data.DomainAttributes.Where(w => key.Equals(w)).ToList())
                { item.SubjectAreaId = null; }

                foreach (DomainEntityItem item in Program.Data.DomainEntities.Where(w => key.Equals(w)).ToList())
                { item.SubjectAreaId = null; }

                Program.Data.DomainSubjectAreas.Remove(data);
            }
        }
    }
}
