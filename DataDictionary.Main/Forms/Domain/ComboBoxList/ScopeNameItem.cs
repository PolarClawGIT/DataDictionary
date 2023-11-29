
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData;
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
    record ScopeNameItem : IDbScopeType
    {
        public String ScopeName { get; init; } = String.Empty;

        public static ScopeNameItem Empty { get; } = new ScopeNameItem();

        protected ScopeNameItem() : base() { }

        public ScopeNameItem(IDbScopeType source) : base()
        { if (source.ScopeName is String value) { ScopeName = value; } }

        public static void LoadEntity(ComboBoxData control)
        {
            BindingList<IDbScopeType> list = new BindingList<IDbScopeType>();
            list.Add(Empty);

            list.AddRange(Program.Data.Scopes.Where(
                    w => w.ToScopeType() is
                    ScopeType.DatabaseSchemaTable or 
                    ScopeType.DatabaseSchemaView or
                    ScopeType.LibraryType).
                    Select(s => new ScopeNameItem(s)));

            IDbScopeType? selected = control.SelectedItem as IDbScopeType;
            if (selected is null)
            { selected = list.FirstOrDefault(w => w.ScopeName is String value && control.Text.ToUpper() == value.ToUpper()); }

            control.DataSource = list;
            control.ValueMember = nameof(selected.ScopeName);

            if (selected is not null) { control.SelectedItem = selected; }
        }

        public static void LoadAttribute(ComboBoxData control)
        {
            BindingList<IDbScopeType> list = new BindingList<IDbScopeType>();
            list.Add(Empty);

            list.AddRange(Program.Data.Scopes.Where(
                    w => w.ToScopeType() is
                    ScopeType.DatabaseSchemaTableColumn or
                    ScopeType.DatabaseSchemaViewColumn or
                    ScopeType.LibraryField or
                    ScopeType.LibraryProperty).
                    Select(s => new ScopeNameItem(s)));

            IDbScopeType? selected = control.SelectedItem as IDbScopeType;
            if (selected is null)
            { selected = list.FirstOrDefault(w => w.ScopeName is String value && control.Text.ToUpper() == value.ToUpper()); }

            control.DataSource = list;
            control.ValueMember = nameof(selected.ScopeName);

            if (selected is not null) { control.SelectedItem = selected; }

        }
    }
}
