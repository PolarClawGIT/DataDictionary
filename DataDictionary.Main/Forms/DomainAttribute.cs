using DataDictionary.DataLayer.DomainData;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms
{
    partial class DomainAttribute : ApplicationFormBase
    {
        class FormData
        {
            public IDomainAttributeItem? DomainAttribute { get; set; }
            public IDomainAttributeItem? ParentAttribute { get; set; }
            public BindingList<DomainAttributePropertyItem> AttributeProperties { get; set; } = new BindingList<DomainAttributePropertyItem>();
            public BindingList<DomainAttributeAliasItem> AttributeAlias { get; set; } = new BindingList<DomainAttributeAliasItem>();
        }

        FormData data = new FormData();

        public DomainAttribute() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DomainAttribute;

            //propertyNameData.Items.AddRange(Program.DomainData.PropertyNames.Select(s => s.PropertyName).ToArray());
            propertyNameData.DataSource = Program.Data.DomainAttributeProperties.Select(s => s.PropertyName).ToList();
        }

        public DomainAttribute(IDomainAttributeItem domainAttributeItem) : this()
        {
            data.DomainAttribute = domainAttributeItem;
            if (Program.Data.DomainAttributes.GetParentAttribute(data.DomainAttribute) is IDomainAttributeItem parent)
            { data.ParentAttribute = parent; }
            else { data.ParentAttribute = new DomainAttributeItem(); }

            data.AttributeProperties.AddRange(Program.Data.DomainAttributeProperties.GetProperties(data.DomainAttribute));
            data.AttributeAlias.AddRange(Program.Data.DomainAttributeAliases.GetProperties(data.DomainAttribute));

        }

        private void DomainAttribute_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        {
            attributeTitleData.DataBindings.Add(new Binding(nameof(attributeTitleData.Text), data.DomainAttribute, nameof(data.DomainAttribute.AttributeTitle)));
            attributeDescriptionData.DataBindings.Add(new Binding(nameof(attributeDescriptionData.Text), data.DomainAttribute, nameof(data.DomainAttribute.AttributeDescription)));
            attributeParentTitleData.DataBindings.Add(new Binding(nameof(attributeParentTitleData.Text), data.ParentAttribute, nameof(data.ParentAttribute.AttributeTitle)));

            attributeAlaisData.AutoGenerateColumns = false;
            attributeAlaisData.DataSource = data.AttributeAlias;

            attributePropertiesData.AutoGenerateColumns = false;
            attributePropertiesData.DataSource = data.AttributeProperties;
        }

        void UnbindData()
        {
            attributeTitleData.DataBindings.Clear();
            attributeDescriptionData.DataBindings.Clear();
            attributeParentTitleData.DataBindings.Clear();

            attributeAlaisData = null;
            attributePropertiesData = null;
        }
    }
}
