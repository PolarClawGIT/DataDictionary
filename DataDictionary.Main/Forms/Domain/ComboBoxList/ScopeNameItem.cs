
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
    record ScopeNameItem : IScopeKeyName
    {
        public ScopeType ScopeType { get; set; } = ScopeType.Null;
        public String ScopeName { get; init; } = String.Empty;

        public static ScopeNameItem Empty { get; } = new ScopeNameItem();

        protected ScopeNameItem() : base() { }

        public static void Load(ComboBoxData control)
        {
            ScopeNameItem scopeNameItem = new ScopeNameItem();
            BindingList<ScopeNameItem> list = new BindingList<ScopeNameItem>();

            foreach (ScopeType item in Enum.GetValues(typeof(ScopeType)))
            {
                String name = item.ToScopeName();
                if (!String.IsNullOrEmpty(name))
                { list.Add(new ScopeNameItem() { ScopeType = item, ScopeName = name }); }
            }

            control.DataSource = list;
            control.ValueMember = nameof(scopeNameItem.ScopeType);
            control.DisplayMember = nameof(scopeNameItem.ScopeName);
        }

        public static void Load(DataGridViewComboBoxColumn control)
        {
            ScopeNameItem scopeNameItem = new ScopeNameItem();
            BindingList<ScopeNameItem> list = new BindingList<ScopeNameItem>();

            foreach (ScopeType item in Enum.GetValues(typeof(ScopeType)))
            {
                String name = item.ToScopeName();
                if (!String.IsNullOrEmpty(name))
                { list.Add(new ScopeNameItem() { ScopeType = item, ScopeName = name }); }
            }

            control.DataSource = list;
            control.ValueMember = nameof(scopeNameItem.ScopeType);
            control.DisplayMember = nameof(scopeNameItem.ScopeName);
        }
    }
}
