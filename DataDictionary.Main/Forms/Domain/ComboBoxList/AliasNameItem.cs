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
    record AliasNameItem : IDomainAliasNameKey
    {
        public String AliasName { get; init; } = String.Empty;

        public static AliasNameItem Empty { get; } = new AliasNameItem();

        protected AliasNameItem() : base() { }

        public static void LoadEntity(ComboBoxData control, IDomainAliasSourceKey source, IDbScopeType scope)
        {
            BindingList<IDomainAliasNameKey> list = new BindingList<IDomainAliasNameKey>();
            list.Add(Empty);

            var x = control.SelectedItem;
            var y = control.SelectedValue;
            
            if(Program.Data.DbCatalogs.FirstOrDefault(w => new DomainAliasSourceKey(w).Equals(source)) is DbCatalogItem catalogItem)
            {
                DbCatalogKey key = new DbCatalogKey(catalogItem);

                list.AddRange(Program.Data.DbTables.Where(
                w => w.ToScopeType() == scope.ToScopeType() &&
                    key.Equals(w)).
                    Select(s => new AliasNameItem() { AliasName = s.ToAliasName() }).
                    OrderBy(o => o.AliasName));
            }

            if(Program.Data.LibrarySources.FirstOrDefault(w => new DomainAliasSourceKey(w).Equals(source)) is LibrarySourceItem libraryItem)
            {
                LibrarySourceKey key = new LibrarySourceKey(libraryItem);

                list.AddRange(Program.Data.LibraryMembers.Where(
                w => w.ToScopeType() == scope.ToScopeType() &&
                    key.Equals(w)).
                    Select(s => new AliasNameItem() { AliasName = new LibrarySourceUniqueKey(s).ToAliasName() }).
                    OrderBy(o => o.AliasName));
            }

            IDomainAliasNameKey? selected = control.SelectedItem as IDomainAliasNameKey;
            if (selected is null)
            { selected = list.FirstOrDefault(w => w.AliasName is String value && control.Text.ToUpper() == value.ToUpper()); }

            control.DataSource = list;
            control.ValueMember = nameof(selected.AliasName);

            if (selected is not null) { control.SelectedItem = selected; }
        }

        public static void LoadAttribute(ComboBoxData control, IDomainAliasSourceKey source, IDbScopeType scope)
        {
            BindingList<IDomainAliasNameKey> list = new BindingList<IDomainAliasNameKey>();
            list.Add(Empty);

            if (Program.Data.DbCatalogs.FirstOrDefault(w => new DomainAliasSourceKey(w).Equals(source)) is DbCatalogItem catalogItem)
            {
                DbCatalogKey key = new DbCatalogKey(catalogItem);

                list.AddRange(Program.Data.DbTableColumns.Where(
                w => w.ToScopeType() == scope.ToScopeType() &&
                    key.Equals(w)).
                    Select(s => new AliasNameItem() { AliasName = s.ToAliasName() }
                ));
            }

            if (Program.Data.LibrarySources.FirstOrDefault(w => new DomainAliasSourceKey(w).Equals(source)) is LibrarySourceItem libraryItem)
            {
                LibrarySourceKey key = new LibrarySourceKey(libraryItem);

                list.AddRange(Program.Data.LibraryMembers.Where(
                w => w.ToScopeType() == scope.ToScopeType() &&
                    key.Equals(w)).
                    Select(s => new AliasNameItem() { AliasName = new LibraryMemberAlternateKey(s).ToAliasName() }
                ));
            }

            IDomainAliasNameKey? selected = control.SelectedItem as IDomainAliasNameKey;
            if (selected is null)
            { selected = list.FirstOrDefault(w => w.AliasName is String value && control.Text.ToUpper() == value.ToUpper()); }

            control.DataSource = list;
            control.ValueMember = nameof(selected.AliasName);

            if (selected is not null) { control.SelectedItem = selected; }
        }

    }
}
