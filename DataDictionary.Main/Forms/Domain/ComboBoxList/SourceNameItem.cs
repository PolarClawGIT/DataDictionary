using DataDictionary.DataLayer.DomainData.Alias;
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
    record SourceNameItem : IAliasKeySource
    {
        public String SourceName { get; init; } = String.Empty;

        public static SourceNameItem Empty { get; } = new SourceNameItem();

        protected SourceNameItem() : base() { }

        public static void Load(ComboBoxData control)
        {
            BindingList<IAliasKeySource> list = new BindingList<IAliasKeySource>();
            list.Add(Empty);

            list.AddRange(
                Program.Data.DbCatalogs.
                Select(s => new SourceNameItem() { SourceName = s.SourceDatabaseName ?? String.Empty }));

            list.AddRange(
                Program.Data.LibrarySources.
                Select(s => new SourceNameItem() { SourceName = s.AssemblyName ?? String.Empty }));

            IAliasKeySource? selected = control.SelectedItem as IAliasKeySource;
            if (selected is null)
            { selected = list.FirstOrDefault(w => w.SourceName is String value && control.Text.ToUpper() == value.ToUpper()); }

            control.DataSource = list;
            control.ValueMember = nameof(selected.SourceName);

            if (selected is not null) { control.SelectedItem = selected; }
        }
    }
}
