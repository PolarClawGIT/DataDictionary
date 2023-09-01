using DataDictionary.DataLayer.DomainData;
using DataDictionary.Main.Messages;
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
    partial class DomainAttribute : ApplicationBase, IApplicationDataForm
    {
        class FormData
        {
            public DomainAttributeKey? AttributeKey { get; set; }
            public IDomainAttributeItem? DomainAttribute { get; set; }
            public IDomainAttributeItem? ParentAttribute { get; set; }
            public BindingView<DomainAttributePropertyItem>? AttributeProperties { get; set; }
            public BindingView<DomainAttributeAliasItem>? AttributeAlias { get; set; }
        }

        FormData data = new FormData();
        public Object? OpenItem { get; }

        public DomainAttribute() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DomainAttribute;
        }

        public DomainAttribute(IDomainAttributeItem domainAttributeItem) : this()
        {
            data.AttributeKey = new DomainAttributeKey(domainAttributeItem);
            OpenItem = domainAttributeItem;
        }

        private void DomainAttribute_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        {
            if (data.AttributeKey is not null)
            { data.DomainAttribute = Program.Data.DomainAttributes.FirstOrDefault(w => data.AttributeKey == w.AttributeKey); }

            if (data.DomainAttribute is not null && data.AttributeKey is not null)
            {
                this.Text = data.DomainAttribute.AttributeTitle;

                if (Program.Data.DomainAttributes.GetParentAttribute(data.DomainAttribute) is IDomainAttributeItem parent)
                { data.ParentAttribute = parent; }
                else { data.ParentAttribute = new DomainAttributeItem(); }

                data.AttributeProperties =
                    new BindingView<DomainAttributePropertyItem>(
                        Program.Data.DomainAttributeProperties,
                        w => data.AttributeKey == new DomainAttributeKey(w));
                data.AttributeAlias = new BindingView<DomainAttributeAliasItem>(
                    Program.Data.DomainAttributeAliases,
                    w => data.AttributeKey == new DomainAttributeKey(w));

                attributeTitleData.DataBindings.Add(new Binding(nameof(attributeTitleData.Text), data.DomainAttribute, nameof(data.DomainAttribute.AttributeTitle)));
                attributeDescriptionData.DataBindings.Add(new Binding(nameof(attributeDescriptionData.Text), data.DomainAttribute, nameof(data.DomainAttribute.AttributeDescription)));
                attributeParentTitleData.DataBindings.Add(new Binding(nameof(attributeParentTitleData.Text), data.ParentAttribute, nameof(data.ParentAttribute.AttributeTitle)));

                attributeAlaisData.AutoGenerateColumns = false;
                attributeAlaisData.DataSource = data.AttributeAlias;

                PropertyNameDataItems defaultItem = new PropertyNameDataItems();
                propertyNameData.DisplayMember = nameof(defaultItem.PropertyTitle);
                propertyNameData.ValueMember = nameof(defaultItem.PropertyId);
                propertyNameData.DataSource = PropertyNameDataItems.Create();

                attributePropertiesData.AutoGenerateColumns = false;
                attributePropertiesData.DataSource = data.AttributeProperties;
            }
        }

        // Because DataGridComboItem cannot correctly bind anything but a very simple object.
        record PropertyNameDataItems
        {// TODO: Make this into a generic
            public Guid? PropertyId { get; set; }
            public string? PropertyTitle { get; set; }

            public static IReadOnlyList<PropertyNameDataItems> Create()
            { return Program.Data.Properties.Select(s => new PropertyNameDataItems() { PropertyId = s.PropertyId, PropertyTitle = s.PropertyTitle }).ToList(); }
        }

        void UnBindData()
        {
            attributeTitleData.DataBindings.Clear();
            attributeDescriptionData.DataBindings.Clear();
            attributeParentTitleData.DataBindings.Clear();

            attributeAlaisData.DataSource = null;
            attributePropertiesData.DataSource = null;
        }

        private void attributePropertiesData_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (attributePropertiesData.Rows[e.RowIndex].DataBoundItem is DomainAttributePropertyItem item
                && data.AttributeKey is not null)
            { item.AttributeKey = data.AttributeKey; }
        }

        private void attributeAlaisData_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (attributeAlaisData.Rows[e.RowIndex].DataBoundItem is DomainAttributeAliasItem item
                && data.AttributeKey is not null)
            { item.AttributeKey = data.AttributeKey; }
        }

        #region IColleague
        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }
        #endregion
    }
}
