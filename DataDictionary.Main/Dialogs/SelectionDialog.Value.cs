using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Dialogs
{

    class SelectionDialogGetValue
    { // So I can pass a Delegate by Reference
        public required Func<INamedScopeSourceValue, String> GetValue { get; set; }
    }

    class SelectionDialogValue : IBindingPropertyChanged
    {
        public NamedScopeIndex Index { get; }
        public INamedScopeValue NamedScope { get; }
        public INamedScopeSourceValue Source { get; }
        public ListViewItem ListView { get; }

        public String Title { get { return NamedScope.Title; } }
        public ScopeType Scope { get { return NamedScope.Scope; } }
        public String ScopeName { get { return ImageEnumeration.Cast(Scope).DisplayName; } }
        public NamedScopePath Path { get { return NamedScope.Path; } }
        public String PathName { get { return Path.MemberFullPath; } }

        Func<INamedScopeSourceValue, String> GetDescription { get; }
        public String Description { get { return GetDescription(Source); } }

        public SelectionDialogValue(NamedScopeIndex key, Func<INamedScopeSourceValue, String> getDescription)
        {
            this.Index = key;
            this.NamedScope = BusinessData.NamedScope.GetValue(key);
            this.Source = BusinessData.NamedScope.GetData(key);
            this.GetDescription = getDescription;

            ImageEnumeration scopeItem = ImageEnumeration.Cast(NamedScope.Scope);
            this.ListView = new ListViewItem(Title);
            this.ListView.ImageKey = scopeItem.Name;
        }

        /// <summary>
        /// PropertyChanged handler. This will not be called as all data is readonly.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
