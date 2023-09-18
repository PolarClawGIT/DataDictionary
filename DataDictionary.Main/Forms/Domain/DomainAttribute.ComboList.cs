using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.Main.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Domain
{
    partial class DomainAttribute
    {        // This partial class contains the data structures used in the ComboBoxs or ComboLists.

        record PropertyNameDataItem
        {
            public Guid? PropertyId { get; set; }
            public String? PropertyTitle { get; set; }

            public static void Bind(ComboBoxData control)
            {
                PropertyNameDataItem propertyNameDataItem = new PropertyNameDataItem();
                BindingList<PropertyNameDataItem> list = new BindingList<PropertyNameDataItem>();
                list.Add(new PropertyNameDataItem() { PropertyId = Guid.Empty, PropertyTitle = "(select property Type)" });
                list.AddRange(Program.Data.Properties.Select(s => new PropertyNameDataItem() { PropertyId = s.PropertyId, PropertyTitle = s.PropertyTitle }).ToList());

                control.DataSource = list;
                control.ValueMember = nameof(propertyNameDataItem.PropertyId);
                control.DisplayMember = nameof(propertyNameDataItem.PropertyTitle);
            }

            public static void Bind(DataGridViewComboBoxColumn control)
            {
                PropertyNameDataItem propertyNameDataItem = new PropertyNameDataItem();
                BindingList<PropertyNameDataItem> list = new BindingList<PropertyNameDataItem>();
                list.Add(new PropertyNameDataItem() { PropertyId = Guid.Empty, PropertyTitle = "(select property Type)" });
                list.AddRange(Program.Data.Properties.Select(s => new PropertyNameDataItem() { PropertyId = s.PropertyId, PropertyTitle = s.PropertyTitle }).ToList());

                control.DataSource = list;
                control.ValueMember = nameof(propertyNameDataItem.PropertyId);
                control.DisplayMember = nameof(propertyNameDataItem.PropertyTitle);
            }
        }

        record CatalogNameDataItem : IDbCatalogKeyUnique
        {
            static ComboBoxData? bindingControl;
            public String CatalogName { get; set; } = String.Empty;

            protected CatalogNameDataItem() { }

            protected CatalogNameDataItem(IDbCatalogKeyUnique source) : this()
            { if (source.CatalogName is String value) { CatalogName = value; } }

            public static void Bind(ComboBoxData control)
            {
                bindingControl = control;
                BindingList<CatalogNameDataItem> list = new BindingList<CatalogNameDataItem>();
                list.AddRange(Program.Data.DbCatalogs.
                    Where(w => w.IsSystem == false).
                    Select(s => new CatalogNameDataItem(s)));

                CatalogNameDataItem? selected = control.SelectedItem as CatalogNameDataItem;
                if (selected is null)
                { selected = list.FirstOrDefault(w => control.Text.ToUpper() == w.CatalogName.ToUpper()); }

                CatalogNameDataItem members = new CatalogNameDataItem();
                control.DataSource = list;
                control.ValueMember = nameof(members.CatalogName);

                if (selected is not null) { control.SelectedItem = selected; }
            }
        }

        record SchemaNameDataItem : CatalogNameDataItem, IDbSchemaKey
        {
            static ComboBoxData? bindingControl;
            static DbCatalogKeyUnique? currentKey;
            public String SchemaName { get; set; } = String.Empty;

            protected SchemaNameDataItem() { }

            protected SchemaNameDataItem(IDbSchemaKey source) : base(source)
            { if (source.SchemaName is String value) { SchemaName = value; } }

            public static void Bind(ComboBoxData control, IDbCatalogKeyUnique key)
            {
                bindingControl = control;
                currentKey = new DbCatalogKeyUnique(key);
                BindingList<SchemaNameDataItem> list = new BindingList<SchemaNameDataItem>();

                list.AddRange(Program.Data.DbSchemta.
                    Where(w => currentKey.Equals(w) && w.IsSystem == false).
                    Select(s => new SchemaNameDataItem(s)));

                SchemaNameDataItem? selected = control.SelectedItem as SchemaNameDataItem;
                if (selected is null)
                { selected = list.FirstOrDefault(w => currentKey.Equals(w) && control.Text.ToUpper() == w.SchemaName.ToUpper()); }

                SchemaNameDataItem members = new SchemaNameDataItem();
                control.DataSource = list;
                control.ValueMember = nameof(members.SchemaName);

                if (selected is not null) { control.SelectedItem = selected; }
            }
        }

        record ObjectNameDataItem : SchemaNameDataItem, IDomainEntityAliasKey
        {
            static ComboBoxData? bindingControl;
            static DbSchemaKey? currentKey;
            public String ObjectName { get; set; } = String.Empty;

            protected ObjectNameDataItem() { }

            protected ObjectNameDataItem(IDomainEntityAliasKey source) : base(source)
            { if (source.ObjectName is String value) { ObjectName = value; } }

            protected ObjectNameDataItem(IDbTableKey source) : base(source)
            { if (source.TableName is String value) { ObjectName = value; } }

            public static void Bind(ComboBoxData control, IDbSchemaKey key)
            {
                bindingControl = control;
                currentKey = new DbSchemaKey(key);
                BindingList<ObjectNameDataItem> list = new BindingList<ObjectNameDataItem>();

                list.AddRange(Program.Data.DbTables.
                    Where(w => currentKey.Equals(w) && w.IsSystem == false).
                    Select(s => new ObjectNameDataItem(s)));

                ObjectNameDataItem? selected = control.SelectedItem as ObjectNameDataItem;
                if (selected is null)
                { selected = list.FirstOrDefault(w => currentKey.Equals(w) && control.Text.ToUpper() == w.ObjectName.ToUpper()); }

                ObjectNameDataItem members = new ObjectNameDataItem();
                control.DataSource = list;
                control.ValueMember = nameof(members.ObjectName);

                if (selected is not null) { control.SelectedItem = selected; }
            }
        }

        record ElementNameDataItem : ObjectNameDataItem, IDomainAttributeAliasKey
        {
            static ComboBoxData? bindingControl;
            static DbSchemaKey? currentKey;
            public String ElementName { get; set; } = String.Empty;

            protected ElementNameDataItem() { }

            protected ElementNameDataItem(IDomainAttributeAliasKey source) : base(source)
            { if (source.ElementName is String value) { ElementName = value; } }

            protected ElementNameDataItem(IDbTableColumnKey source) : base(source)
            { if (source.ColumnName is String value) { ElementName = value; } }

            public static void Bind(ComboBoxData control, IDomainEntityAliasKey key)
            {
                bindingControl = control;
                currentKey = new DbTableKey(key);
                BindingList<ElementNameDataItem> list = new BindingList<ElementNameDataItem>();

                ElementNameDataItem? selected = control.SelectedItem as ElementNameDataItem;
                if (selected is null)
                { selected = list.FirstOrDefault(w => currentKey.Equals(w) && control.Text.ToUpper() == w.ElementName.ToUpper()); }

                list.AddRange(Program.Data.DbTableColumns.
                    Where(w => currentKey.Equals(w)).
                    Select(s => new ElementNameDataItem(s)));

                ElementNameDataItem members = new ElementNameDataItem();
                control.DataSource = list;
                control.ValueMember = nameof(members.ElementName);

                if (selected is not null) { control.SelectedItem = selected; }
            }
        }
    }
}
