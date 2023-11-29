using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Schema;
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
    [Obsolete()]
    record SchemaNameItem : CatalogNameItem, IDbSchemaKey
    {
        public String SchemaName { get; set; } = String.Empty;

        protected SchemaNameItem(IDbSchemaKey source) : base(source)
        { if (source.SchemaName is String value) { SchemaName = value; } }

        public static void Load(ComboBoxData control, IDbCatalogKeyUnique key)
        {
            DbCatalogKeyUnique currentKey = new DbCatalogKeyUnique(key);
            BindingList<SchemaNameItem> list = new BindingList<SchemaNameItem>();

            list.AddRange(Program.Data.DbSchemta.
                Where(w => currentKey.Equals(w) && w.IsSystem == false).
                Select(s => new SchemaNameItem(s)));

            SchemaNameItem? selected = control.SelectedItem as SchemaNameItem;
            if (selected is null)
            { selected = list.FirstOrDefault(w => currentKey.Equals(w) && control.Text.ToUpper() == w.SchemaName.ToUpper()); }

            control.DataSource = list;
            control.ValueMember = nameof(selected.SchemaName);

            if (selected is not null) { control.SelectedItem = selected; }
        }
    }
}
