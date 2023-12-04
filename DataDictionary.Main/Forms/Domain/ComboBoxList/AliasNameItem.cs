using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
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
    record AliasNameItem : IAliasKeyName
    {
        public String AliasName { get; init; } = String.Empty;

        public static AliasNameItem Empty { get; } = new AliasNameItem();

        protected AliasNameItem() : base() { }

        public static void LoadEntity(ComboBoxData control, IAliasKeySource source, IDbScopeType scope)
        {
            BindingList<IAliasKeyName> list = new BindingList<IAliasKeyName>();
            list.Add(Empty);

            var x = control.SelectedItem;
            var y = control.SelectedValue;
            
            if(Program.Data.DbCatalogs.FirstOrDefault(w => new AliasKeySource(w).Equals(source)) is DbCatalogItem catalogItem)
            {
                DbCatalogKey key = new DbCatalogKey(catalogItem);

                list.AddRange(Program.Data.DbTables.Where(
                w => w.ToScopeType() == scope.ToScopeType() &&
                    key.Equals(w)).
                    Select(s => new AliasNameItem() { AliasName = s.ToAliasName() }).
                    OrderBy(o => o.AliasName));
            }

            if(Program.Data.LibrarySources.FirstOrDefault(w => new AliasKeySource(w).Equals(source)) is LibrarySourceItem libraryItem)
            {
                LibrarySourceKey key = new LibrarySourceKey(libraryItem);

                list.AddRange(Program.Data.LibraryMembers.Where(
                w => w.ToScopeType() == scope.ToScopeType() &&
                    key.Equals(w)).
                    Select(s => new AliasNameItem() { AliasName = new LibrarySourceKeyName(s).ToAliasName() }).
                    OrderBy(o => o.AliasName));
            }

            IAliasKeyName? selected = control.SelectedItem as IAliasKeyName;
            if (selected is null)
            { selected = list.FirstOrDefault(w => w.AliasName is String value && control.Text.ToUpper() == value.ToUpper()); }

            control.DataSource = list;
            control.ValueMember = nameof(selected.AliasName);

            if (selected is not null) { control.SelectedItem = selected; }
        }

        public static void LoadAttribute(ComboBoxData control, IAliasKeySource source, IDbScopeType scope)
        {
            BindingList<IAliasKeyName> list = new BindingList<IAliasKeyName>();
            list.Add(Empty);

            if (Program.Data.DbCatalogs.FirstOrDefault(w => new AliasKeySource(w).Equals(source)) is DbCatalogItem catalogItem)
            {
                DbCatalogKey key = new DbCatalogKey(catalogItem);

                list.AddRange(Program.Data.DbTableColumns.Where(
                w => w.ToScopeType() == scope.ToScopeType() &&
                    key.Equals(w)).
                    Select(s => new AliasNameItem() { AliasName = s.ToAliasName() }
                ));
            }

            if (Program.Data.LibrarySources.FirstOrDefault(w => new AliasKeySource(w).Equals(source)) is LibrarySourceItem libraryItem)
            {
                LibrarySourceKey key = new LibrarySourceKey(libraryItem);

                list.AddRange(Program.Data.LibraryMembers.Where(
                w => w.ToScopeType() == scope.ToScopeType() &&
                    key.Equals(w)).
                    Select(s => new AliasNameItem() { AliasName = new LibraryMemberKeyName(s).ToAliasName() }
                ));
            }

            IAliasKeyName? selected = control.SelectedItem as IAliasKeyName;
            if (selected is null)
            { selected = list.FirstOrDefault(w => w.AliasName is String value && control.Text.ToUpper() == value.ToUpper()); }

            control.DataSource = list;
            control.ValueMember = nameof(selected.AliasName);

            if (selected is not null) { control.SelectedItem = selected; }
        }

    }
}
