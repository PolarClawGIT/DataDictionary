using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record XScopeList
    {
        public ScopeType ScopeType { get; set; } = ScopeType.Null;
        public String ScopeName { get; init; } = String.Empty;

        public List<XPropertyList> Properties { get; } = new List<XPropertyList>();
        public static XScopeList Empty { get; } = new XScopeList();
        public static ScopeType NullValue { get; } = ScopeType.Null;

        protected XScopeList() : base() { }

        public record XPropertyList
        {
            public String PropertyName { get; init; } = String.Empty;
            public String PropertyDisplay { get; init; } = String.Empty;
        }

        public static void Load(
            ComboBoxData scopeControl,
            ComboBoxData propertyControl,
            IXElementBuilderList builders,
            String? emptyText = null)
        {
            List<XScopeList> list = BuildList(builders, emptyText);

            scopeControl.ValueMember = nameof(XScopeList.ScopeType);
            scopeControl.DisplayMember = nameof(XScopeList.ScopeName);
            scopeControl.DataSource = list;

            scopeControl.SelectedIndexChanged += ScopeChanged;

            void ScopeChanged(Object? sender, EventArgs e)
            {
                if (scopeControl.SelectedItem is XScopeList selectedValue
                    && selectedValue.Properties.Count > 0)
                {
                    propertyControl.Enabled = true;
                    propertyControl.ValueMember = nameof(XPropertyList.PropertyName);
                    propertyControl.DisplayMember = nameof(XPropertyList.PropertyDisplay);
                    propertyControl.DataSource = selectedValue.Properties;
                }
                else
                {
                    propertyControl.Enabled = false;
                    propertyControl.DataSource = null;
                }
            }
        }


        static List<XScopeList> BuildList(IXElementBuilderList builders, String? emptyText = null)
        {
            List<XScopeList> result = new List<XScopeList>();

            if (emptyText is String)
            { result.Add(new XScopeList() { ScopeType = ScopeType.Null, ScopeName = emptyText }); }

            foreach (var scopeItem in builders.GroupBy(g => g.Key))
            {
                String name = scopeItem.Key.GetEnumeration().DisplayName;
                if (!String.IsNullOrEmpty(name))
                {
                    XScopeList newItem =
                        new XScopeList()
                        {
                            ScopeType = scopeItem.Key,
                            ScopeName = name
                        };

                    if (emptyText is String)
                    {
                        newItem.Properties.Add(
                        new XPropertyList()
                        {
                            PropertyName = String.Empty,
                            PropertyDisplay = emptyText
                        });
                    }

                    foreach (var propertyItem in scopeItem.
                        SelectMany(s => s.Value.Where(w => w.NodeValueAs is not NodeRenderAsType.none)))
                    {
                        newItem.Properties.Add(
                        new XPropertyList()
                        {
                            PropertyName = propertyItem.PropertyName,
                            PropertyDisplay = propertyItem.PropertyName
                        });
                    }

                    result.Add(newItem);
                }
            }

            return result;
        }

    }
}
