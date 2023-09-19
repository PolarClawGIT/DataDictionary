﻿using DataDictionary.DataLayer.DatabaseData.Catalog;
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
    record CatalogNameItem : IDbCatalogKeyUnique
    {
        public String CatalogName { get; set; } = String.Empty;

        protected CatalogNameItem(IDbCatalogKeyUnique source) : base()
        { if (source.CatalogName is String value) { CatalogName = value; } }

        public static void Bind(ComboBoxData control)
        {
            BindingList<CatalogNameItem> list = new BindingList<CatalogNameItem>();
            list.AddRange(Program.Data.DbCatalogs.
                Where(w => w.IsSystem == false).
                Select(s => new CatalogNameItem(s)));

            CatalogNameItem? selected = control.SelectedItem as CatalogNameItem;
            if (selected is null)
            { selected = list.FirstOrDefault(w => control.Text.ToUpper() == w.CatalogName.ToUpper()); }

            control.DataSource = list;
            control.ValueMember = nameof(selected.CatalogName);

            if (selected is not null) { control.SelectedItem = selected; }
        }
    }
}
