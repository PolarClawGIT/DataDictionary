using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.DataLayer.DatabaseData;
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
        {
            // one time bindings
            PropertyNameDataItem propertyNameDataItem = new PropertyNameDataItem();
            propertyTypeData.DataSource = PropertyNameDataItem.Create();
            propertyTypeData.ValueMember = nameof(propertyNameDataItem.PropertyId);
            propertyTypeData.DisplayMember = nameof(propertyNameDataItem.PropertyTitle);

            propertyTypeColumn.DataSource = PropertyNameDataItem.Create();
            propertyTypeColumn.ValueMember = nameof(propertyNameDataItem.PropertyId);
            propertyTypeColumn.DisplayMember = nameof(propertyNameDataItem.PropertyTitle);

            BindData();
        }

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

                bindingProperties.DataSource =
                    new BindingView<DomainAttributePropertyItem>(
                        Program.Data.DomainAttributeProperties,
                        w => DataKey.Equals(w));
                propertyNavigation.AutoGenerateColumns = false;
                propertyNavigation.DataSource = bindingProperties;

                DomainAttributePropertyItem propertyMembers = new DomainAttributePropertyItem();
                propertyTypeData.DataBindings.Add(new Binding(nameof(propertyTypeData.SelectedValue), bindingProperties, nameof(propertyMembers.PropertyId), true));
                propertyValueData.DataBindings.Add(new Binding(nameof(propertyValueData.Text), bindingProperties, nameof(propertyMembers.PropertyValue)));
                //propertyChoiceData.DataBindings.Add(new Binding(nameof(propertyChoiceData.SelectedValue, bindingProperties, nameof(propertyNames.))));
                propertyDefinitionData.DataBindings.Add(new Binding(nameof(propertyDefinitionData.Rtf), bindingProperties, nameof(propertyMembers.DefinitionText)));

                bindingDatabaseAlias.DataSource =
                    new BindingView<DomainAttributeAliasItem>(
                        Program.Data.DomainAttributeAliases,
                        w => DataKey.Equals(w));

                attributeAlaisData.AutoGenerateColumns = false;
                attributeAlaisData.DataSource = bindingDatabaseAlias;

                DomainAttributeAliasItem aliasMembers = new DomainAttributeAliasItem();
                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.SelectedValue), bindingDatabaseAlias, nameof(aliasMembers.CatalogName)));
                schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.SelectedValue), bindingDatabaseAlias, nameof(aliasMembers.SchemaName)));
                objectNameData.DataBindings.Add(new Binding(nameof(objectNameData.SelectedValue), bindingDatabaseAlias, nameof(aliasMembers.ObjectName)));
                elementNameData.DataBindings.Add(new Binding(nameof(elementNameData.SelectedValue), bindingDatabaseAlias, nameof(aliasMembers.ElementName)));

                if(bindingDatabaseAlias.Current is DomainAttributeAliasItem aliasItem)
                {
                    CatalogNameDataItem catalogMembers = new CatalogNameDataItem();
                    catalogNameData.DataSource = CatalogNameDataItem.Create();
                    catalogNameData.ValueMember = nameof(catalogMembers.CatalogName);

                    SchemaNameDataItem schemaMembers = new SchemaNameDataItem();
                    schemaNameData.DataSource = SchemaNameDataItem.Create(new DbCatalogKeyUnique(aliasItem));
                    schemaNameData.ValueMember = nameof(schemaMembers.SchemaName);

                    ObjectNameDataItem objectMembers = new ObjectNameDataItem();
                    objectNameData.DataSource = ObjectNameDataItem.Create(new DbSchemaKey(aliasItem));
                    objectNameData.ValueMember = nameof(ObjectNameDataItem.ObjectName);

                    ElementNameDataItem elementMembers = new ElementNameDataItem();
                    elementNameData.DataSource =  ElementNameDataItem.Create(new DbTableKey(aliasItem));
                    elementNameData.ValueMember = nameof(ElementNameDataItem.ElementName);
                }


            }
        }

        void UnBindData()
        {
            attributeTitleData.DataBindings.Clear();
            attributeDescriptionData.DataBindings.Clear();
            attributeParentTitleData.DataBindings.Clear();

            attributeAlaisData.DataSource = null;
        }

        // Because DataGridComboItem cannot correctly bind anything but a very simple object.
        record PropertyNameDataItem
        {// TODO: Make this into a generic
            public Guid? PropertyId { get; set; }
            public String? PropertyTitle { get; set; }

            public static IReadOnlyList<PropertyNameDataItem> Create()
            {
                List<PropertyNameDataItem> results = new List<PropertyNameDataItem>();
                results.Add(new PropertyNameDataItem() { PropertyId = Guid.Empty, PropertyTitle = "(select property Type)" });
                results.AddRange(Program.Data.Properties.Select(s => new PropertyNameDataItem() { PropertyId = s.PropertyId, PropertyTitle = s.PropertyTitle }).ToList());
                return results;
            }
        }

        record CatalogNameDataItem
        {
            public String? CatalogName { get; set; }

            public static IReadOnlyList<CatalogNameDataItem> Create()
            {
                List<CatalogNameDataItem> results = new List<CatalogNameDataItem>();
                results.AddRange(Program.Data.DbCatalogs.Where(w => w.IsSystem == false).Select(s => new CatalogNameDataItem() { CatalogName = s.CatalogName }));
                return results;
            }
        }

        record SchemaNameDataItem
        {
            public String? SchemaName { get; set; }

            public static IReadOnlyList<SchemaNameDataItem> Create(DbCatalogKeyUnique key)
            {
                List<SchemaNameDataItem> results = new List<SchemaNameDataItem>();
                results.AddRange(Program.Data.DbSchemta.Where(w => key.Equals(w) && w.IsSystem == false).Select(s => new SchemaNameDataItem() { SchemaName = s.SchemaName }));
                return results;
            }
        }

        record ObjectNameDataItem
        {
            public String? ObjectName { get; set; }

            public static IReadOnlyList<ObjectNameDataItem> Create(DbSchemaKey key)
            {
                List<ObjectNameDataItem> results = new List<ObjectNameDataItem>();
                results.AddRange(Program.Data.DbTables.Where(w => key.Equals(w) && w.IsSystem == false).Select(s => new ObjectNameDataItem() { ObjectName = s.TableName }));
                return results;
            }
        }

        record ElementNameDataItem
        {
            public String? ElementName { get; set; }

            public static IReadOnlyList<ElementNameDataItem> Create(DbTableKey key)
            {
                List<ElementNameDataItem> results = new List<ElementNameDataItem>();
                results.AddRange(Program.Data.DbColumns.Where(w => key.Equals(w)).Select(s => new ElementNameDataItem() { ElementName = s.ColumnName }));
                return results;
            }
        }


        #region IColleague
        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }

        protected override void HandleMessage(DbApplicationBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbApplicationBatchCompleted message)
        { BindData(); }
        #endregion

    }
}
