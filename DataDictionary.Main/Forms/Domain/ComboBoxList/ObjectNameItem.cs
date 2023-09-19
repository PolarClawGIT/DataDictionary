using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
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
    record ObjectNameItem : SchemaNameItem, IDomainEntityAliasKey
    {
        public String ObjectName { get; set; } = String.Empty;

        protected ObjectNameItem(IDomainEntityAliasKey source) : base(source)
        { if (source.ObjectName is String value) { ObjectName = value; } }

        protected ObjectNameItem(IDbTableKey source) : base(source)
        { if (source.TableName is String value) { ObjectName = value; } }

        public static void Bind(ComboBoxData control, IDbSchemaKey key)
        {
            DbSchemaKey currentKey = new DbSchemaKey(key);
            BindingList<ObjectNameItem> list = new BindingList<ObjectNameItem>();

            list.AddRange(Program.Data.DbTables.
                Where(w => currentKey.Equals(w) && w.IsSystem == false).
                Select(s => new ObjectNameItem(s)));

            ObjectNameItem? selected = control.SelectedItem as ObjectNameItem;
            if (selected is null)
            { selected = list.FirstOrDefault(w => currentKey.Equals(w) && control.Text.ToUpper() == w.ObjectName.ToUpper()); }

            control.DataSource = list;
            control.ValueMember = nameof(selected.ObjectName);

            if (selected is not null) { control.SelectedItem = selected; }
        }
    }
}
