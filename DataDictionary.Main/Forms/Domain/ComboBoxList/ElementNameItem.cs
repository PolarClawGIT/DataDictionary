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

namespace DataDictionary.Main.Forms.Domain.ComboBoxList
{
    record ElementNameItem : ObjectNameItem, IDomainAttributeAliasKey
    {
        public string ElementName { get; set; } = string.Empty;

        protected ElementNameItem(IDomainAttributeAliasKey source) : base(source)
        { if (source.ElementName is string value) { ElementName = value; } }

        protected ElementNameItem(IDbTableColumnKey source) : base(source)
        { if (source.ColumnName is string value) { ElementName = value; } }

        public static void Bind(ComboBoxData control, IDomainEntityAliasKey key)
        {
            DbTableKey currentKey = new DbTableKey(key);
            BindingList<ElementNameItem> list = new BindingList<ElementNameItem>();

            ElementNameItem? selected = control.SelectedItem as ElementNameItem;
            if (selected is null)
            { selected = list.FirstOrDefault(w => currentKey.Equals(w) && control.Text.ToUpper() == w.ElementName.ToUpper()); }

            list.AddRange(Program.Data.DbTableColumns.
                Where(w => currentKey.Equals(w)).
                Select(s => new ElementNameItem(s)));

            control.DataSource = list;
            control.ValueMember = nameof(selected.ElementName);

            if (selected is not null) { control.SelectedItem = selected; }
        }
    }
}
