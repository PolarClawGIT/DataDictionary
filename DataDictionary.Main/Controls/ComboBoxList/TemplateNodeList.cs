using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record TemplateNodeList : ITemplateNodeIndex, ITemplateNodeIndexName, IBindingPropertyChanged
    {
        /// <inheritdoc/>
        public Guid? NodeId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String NodeName { get; private set; } = String.Empty;

        TemplateNodeList(String emptyText = "(n/a)")
        { NodeName = emptyText; }

        TemplateNodeList(ITemplateNodeValue value)
        {
            NodeId = value.NodeId;
            NodeName = value.NodeName ?? String.Empty;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged is PropertyChangedEventHandler handler)
            { handler(this, new PropertyChangedEventArgs(propertyName)); }
        }

        public static void Load(ComboBoxData control, ITemplateIndex template, String? emptyText = null)
        {
            BindingComboList<TemplateNodeList> comboList = new BindingComboList<TemplateNodeList>();
            TemplateIndex templateKey = new TemplateIndex(template);

            comboList.BuildList(
                source: BusinessData.Scripting.Nodes,
                constructor: (c) => new TemplateNodeList(c),
                onItemChanged: (s, t) =>
                {
                    t.NodeName = s.NodeName ?? String.Empty;
                    t.OnPropertyChanged(nameof(t.NodeName));
                },
                filterBy: (f) => templateKey.Equals(f),
                orderBy: (o) => o.NodeName,
                areEquel: (a, b) => new TemplateNodeIndex(a).Equals(b),
                emptyValue: () => new TemplateNodeList());

            comboList.BindTo(control, () => nameof(NodeId), () => nameof(NodeName));
        }

        public static void Load(DataGridViewComboBoxColumn control, ITemplateIndex template, String? emptyText = null)
        {
            BindingComboList<TemplateNodeList> comboList = new BindingComboList<TemplateNodeList>();
            TemplateIndex templateKey = new TemplateIndex(template);

            comboList.BuildList(
                source: BusinessData.Scripting.Nodes,
                constructor: (c) => new TemplateNodeList(c),
                onItemChanged: (s, t) =>
                {
                    t.NodeName = s.NodeName ?? String.Empty;
                    t.OnPropertyChanged(nameof(NodeName));
                },
                filterBy: (f) => templateKey.Equals(f),
                orderBy: (o) => o.NodeName,
                areEquel: (a, b) => new TemplateNodeIndex(a).Equals(b),
                emptyValue: () => new TemplateNodeList());

            comboList.BindTo(control, () => nameof(NodeId), () => nameof(NodeName));
        }

        [Obsolete]
        static void SelectValue(ComboBoxData control, ITemplateNodeIndex? select)
        {
            if (control.Items is IList<TemplateNodeList> comboList)
            {
                if (select is ITemplateNodeIndex)
                {
                    TemplateNodeIndex key = new TemplateNodeIndex(select);
                    if (comboList.FirstOrDefault(w => key.Equals(w)) is TemplateNodeList selectValue)
                    { control.SelectedItem = selectValue; }
                    else { control.SelectedIndex = -1; }
                }
                else
                {
                    if (comboList.FirstOrDefault(w => w.NodeId == Guid.Empty) is TemplateNodeList emptyValue)
                    { control.SelectedItem = emptyValue; }
                    else { control.SelectedIndex = -1; }
                }
            }
        }

        [Obsolete]
        static Boolean TryGetSelected(ComboBox control, [NotNullWhen(true)] out TemplateNodeList? result)
        {
            if (control.SelectedItem is TemplateNodeList value)
            { result = value; return true; }
            else { result = null; return false; }
        }
 
    }
}
