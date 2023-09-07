using DataDictionary.DataLayer.ApplicationData;
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
        { DataKey = new DomainAttributeKey(domainAttributeItem); }

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

                bindingDefinition.DataSource =
                    new BindingView<DomainAttributeDefinitionItem>(
                        Program.Data.DomainAttributeDefinitions,
                        w => DataKey.Equals(w));

                attributeDefinitionNavigation.AutoGenerateColumns = false;
                attributeDefinitionNavigation.DataSource = bindingDefinition;

                IEnumerable<DefinitionNameDataItem> definitionTypeColumnData = DefinitionNameDataItem.Create();
                DefinitionNameDataItem definitionTypeFirst = definitionTypeColumnData.First(); // Used only to assign member column names
                DomainAttributeDefinitionItem definitionAttribute = new DomainAttributeDefinitionItem(); // Used only to assign member column names

                definitionTypeColumn.ValueMember = nameof(definitionTypeFirst.DefinitionId);
                definitionTypeColumn.DisplayMember = nameof(definitionTypeFirst.DefinitionTitle);
                definitionTypeColumn.DefaultCellStyle.DataSourceNullValue = definitionTypeFirst.DefinitionId;
                definitionTypeColumn.DefaultCellStyle.NullValue = definitionTypeFirst.DefinitionTitle;
                definitionTypeColumn.DataSource = definitionTypeColumnData;

                attributeDefinitionTypeData.ValueMember = nameof(definitionTypeFirst.DefinitionId);
                attributeDefinitionTypeData.DisplayMember = nameof(definitionTypeFirst.DefinitionTitle);
                attributeDefinitionTypeData.DataSource = definitionTypeColumnData;

                attributeDefinitionTypeData.DataBindings.Add(new Binding(nameof(attributeDefinitionTypeData.SelectedValue), bindingDefinition, nameof(definitionAttribute.DefinitionId), true));
                attributeDefinitionData.DataBindings.Add(new Binding(nameof(attributeDefinitionData.Rtf), bindingDefinition, nameof(definitionAttribute.DefinitionText)));


                /*

                attributeAlaisData.AutoGenerateColumns = false;
                attributeAlaisData.DataSource =
                    new BindingView<DomainAttributePropertyItem>(
                        Program.Data.DomainAttributeProperties,
                        w => DataKey.Equals(w));

                PropertyNameDataItem defaultItem = new PropertyNameDataItem();
                propertyNameData.DisplayMember = nameof(defaultItem.PropertyTitle);
                propertyNameData.ValueMember = nameof(defaultItem.PropertyId);
                propertyNameData.DataSource = PropertyNameDataItem.Create();

                attributePropertiesData.AutoGenerateColumns = false;
                attributePropertiesData.DataSource =
                    new BindingView<DomainAttributeAliasItem>(
                    Program.Data.DomainAttributeAliases,
                    w => DataKey.Equals(w));*/
            }
        }

        void UnBindData()
        {
            attributeTitleData.DataBindings.Clear();
            attributeDescriptionData.DataBindings.Clear();
            attributeParentTitleData.DataBindings.Clear();

            attributeAlaisData.DataSource = null;
            attributePropertiesData.DataSource = null;
        }

        // Because DataGridComboItem cannot correctly bind anything but a very simple object.
        record PropertyNameDataItem
        {// TODO: Make this into a generic
            public Guid? PropertyId { get; set; }
            public string? PropertyTitle { get; set; }

            public static IReadOnlyList<PropertyNameDataItem> Create()
            { return Program.Data.Properties.Select(s => new PropertyNameDataItem() { PropertyId = s.PropertyId, PropertyTitle = s.PropertyTitle }).ToList(); }
        }

        record DefinitionNameDataItem
        {
            public required Guid DefinitionId { get; init; }
            public required String DefinitionTitle { get; init; }

            public static IReadOnlyList<DefinitionNameDataItem> Create()
            {
                List<DefinitionNameDataItem> result = new List<DefinitionNameDataItem>();

                result.Add(new DefinitionNameDataItem() { DefinitionId = Guid.Empty, DefinitionTitle = "(select type)" });

                foreach (DefinitionItem item in Program.Data.Definitions)
                {
                    if (item.DefinitionId is Guid id && item.DefinitionTitle is String title)
                    { result.Add(new DefinitionNameDataItem() { DefinitionId = id, DefinitionTitle = title }); }
                }

                return result;
            }
        }

        private void bindingDefinition_AddingNew(object sender, AddingNewEventArgs e)
        {
            DomainAttributeDefinitionItem newItem = new DomainAttributeDefinitionItem()
            { AttributeId = DataKey.AttributeId };

            if (bindingDefinition.Current is null || attributeDefinitionNavigation.CurrentRow is null)
            {
                if (attributeDefinitionTypeData.SelectedValue is Guid guidValue)
                { newItem.DefinitionId = guidValue; }

                if (!String.IsNullOrEmpty(attributeDefinitionData.Text))
                { newItem.DefinitionText = attributeDefinitionData.Rtf; }
            }

            e.NewObject = newItem;

            if (attributeDefinitionNavigation.Focused) { attributeDefinitionTypeData.Focus(); }
        }

        private void attributeDefinitionTypeData_Validated(object sender, EventArgs e)
        {
            if (bindingDefinition.Current is null || attributeDefinitionNavigation.CurrentRow is null)
            { bindingDefinition.AddNew(); }
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
