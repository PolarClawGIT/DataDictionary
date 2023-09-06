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

namespace DataDictionary.Main.Forms.Domain
{
    partial class DomainAttribute : ApplicationBase, IApplicationDataForm
    {
        public DomainAttributeKey DataKey { get; private set; }

        public DomainAttribute() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DomainAttribute;
            DataKey = new DomainAttributeKey(new DomainAttributeItem());
        }

        public DomainAttribute(IDomainAttributeKey domainAttributeItem) : this()
        {
            DataKey = new DomainAttributeKey(domainAttributeItem);
        }

        public Boolean IsOpenItem(Object? item)
        { return DataKey.Equals(item); }

        private void DomainAttribute_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        {
            DomainAttributeItem? data = Program.Data.DomainAttributes.FirstOrDefault(w => DataKey.Equals(w));

            if (data is not null)
            {
                this.Text = data.AttributeTitle;
                IDomainAttributeItem? parent = Program.Data.DomainAttributes.GetParentAttribute(data);
                if (parent is null) { parent = new DomainAttributeItem(); } //TODO: need to re-look at this.

                attributeTitleData.DataBindings.Add(new Binding(nameof(attributeTitleData.Text), data, nameof(data.AttributeTitle)));
                attributeDescriptionData.DataBindings.Add(new Binding(nameof(attributeDescriptionData.Text), data, nameof(data.AttributeDescription)));
                attributeParentTitleData.DataBindings.Add(new Binding(nameof(attributeParentTitleData.Text), parent, nameof(parent.AttributeTitle)));

                attributeAlaisData.AutoGenerateColumns = false;
                attributeAlaisData.DataSource =
                    new BindingView<DomainAttributePropertyItem>(
                        Program.Data.DomainAttributeProperties,
                        w => DataKey.Equals(w));

                PropertyNameDataItems defaultItem = new PropertyNameDataItems();
                propertyNameData.DisplayMember = nameof(defaultItem.PropertyTitle);
                propertyNameData.ValueMember = nameof(defaultItem.PropertyId);
                propertyNameData.DataSource = PropertyNameDataItems.Create();

                attributePropertiesData.AutoGenerateColumns = false;
                attributePropertiesData.DataSource =
                    new BindingView<DomainAttributeAliasItem>(
                    Program.Data.DomainAttributeAliases,
                    w => DataKey.Equals(w));
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

        }

        private void attributeAlaisData_RowValidated(object sender, DataGridViewCellEventArgs e)
        {

        }

        #region IColleague
        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }
        #endregion
    }
}
