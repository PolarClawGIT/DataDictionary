using DataDictionary.DataLayer.DatabaseData.Catalog;
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
    record CatalogNameItem : IDbCatalogKeyUnique
    {
        public String DatabaseName { get; set; } = String.Empty;

        protected CatalogNameItem(IDbCatalogKeyUnique source) : base()
        { if (source.DatabaseName is String value) { DatabaseName = value; } }

        public static void Load(ComboBoxData control)
        {
            BindingList<CatalogNameItem> list = new BindingList<CatalogNameItem>();
            list.AddRange(Program.Data.DbCatalogs.
                Where(w => w.IsSystem == false).
                Select(s => new CatalogNameItem(s)));

            CatalogNameItem? selected = control.SelectedItem as CatalogNameItem;
            if (selected is null)
            { selected = list.FirstOrDefault(w => control.Text.ToUpper() == w.DatabaseName.ToUpper()); }

            control.DataSource = list;
            control.ValueMember = nameof(selected.DatabaseName);

            if (selected is not null) { control.SelectedItem = selected; }
        }
    }
}
