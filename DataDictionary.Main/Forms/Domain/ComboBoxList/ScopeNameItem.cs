
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
    [Obsolete("not used", true)]
    record ScopeNameItem : IScopeKeyName
    {
        public String ScopeName { get; init; } = String.Empty;

        public static ScopeNameItem Empty { get; } = new ScopeNameItem();

        protected ScopeNameItem() : base() { }

        public ScopeNameItem(IScopeKeyName source) : base()
        { if (source.ScopeName is String value) { ScopeName = value; } }
    }
}
