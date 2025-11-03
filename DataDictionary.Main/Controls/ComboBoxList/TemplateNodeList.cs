using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record TemplateNodeList : ITemplateNodeIndex, ITemplateNodeIndexName, INotifyPropertyChanged
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

        public static void Load(ComboBoxData control, ITemplateIndex template, String? emptyText = null)
        {
            control.ValueMember = nameof(NodeId);
            control.DisplayMember = nameof(NodeName);
            control.DataSource = BuildList(template, emptyText);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged is PropertyChangedEventHandler handler)
            { handler(this, new PropertyChangedEventArgs(propertyName)); }
        }

        public static void Load(DataGridViewComboBoxColumn control, ITemplateIndex template, String? emptyText = null)
        {
            control.ValueMember = nameof(NodeId);
            control.DisplayMember = nameof(NodeName);
            control.DataSource = BuildList(template, emptyText);
        }

        public static void SelectValue(ComboBoxData control, ITemplateNodeIndex? select)
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

        public static Boolean TryGetSelected(ComboBox control, [NotNullWhen(true)] out TemplateNodeList? result)
        {
            if (control.SelectedItem is TemplateNodeList value)
            { result = value; return true; }
            else { result = null; return false; }
        }

        static BindingComboList<TemplateNodeList> BuildList(ITemplateIndex template, String? emptyText)
        {
            TemplateIndex templateKey = new TemplateIndex(template);

            BindingComboList<TemplateNodeList> comboList = new BindingComboList<TemplateNodeList>();
            if (!String.IsNullOrWhiteSpace(emptyText))
            { comboList.Add(new TemplateNodeList(emptyText)); }

            comboList.AddRange(
                BusinessData.Scripting.Nodes.
                Where(w => templateKey.Equals(w)).
                OrderBy(o => o.NodeName).
                Select(s => new TemplateNodeList(s)));

            BusinessData.Scripting.Nodes.ListChanged += Nodes_ListChanged;
            return comboList;

            void Nodes_ListChanged(Object? sender, ListChangedEventArgs e)
            {
                switch (e.ListChangedType)
                {
                    case ListChangedType.Reset:
                        comboList.RaiseListChangedEvents = false;

                        comboList.RemoveRange(comboList.
                            Where(w => w.NodeId != Guid.Empty));

                        comboList.AddRange(
                            BusinessData.Scripting.Nodes.
                            Where(w => templateKey.Equals(w)).
                            OrderBy(o => o.NodeName).
                            Select(s => new TemplateNodeList(s)));

                        comboList.RaiseListChangedEvents = true;
                        comboList.ResetBindings();

                        break;
                    case ListChangedType.ItemAdded:
                        var newItem = BusinessData.Scripting.Nodes[e.NewIndex];

                        comboList.Add(new TemplateNodeList(newItem));
                        comboList.SortBy(o => o.NodeName);

                        break;
                    case ListChangedType.ItemDeleted:
                        comboList.RemoveRange(comboList.
                            Where(w => comboList.
                                Where(w => w.NodeId != Guid.Empty).
                                Select(s => new TemplateNodeIndex(s)).
                                Except(BusinessData.Scripting.Nodes.Where(w => templateKey.Equals(w)).
                                    Select(s => new TemplateNodeIndex(s))).
                                Any(a => a.Equals(w))));
                        break;
                    case ListChangedType.ItemChanged:
                        var changedItem = BusinessData.Scripting.Nodes[e.NewIndex];
                        var changedKey = new TemplateNodeIndex(changedItem);

                        foreach (var item in comboList.Where(w => changedKey.Equals(w)))
                        {
                            item.NodeName = changedItem.NodeName ?? String.Empty;
                            item.OnPropertyChanged(nameof(NodeName));
                        }

                        comboList.SortBy(o => o.NodeName);

                        break;
                    case ListChangedType.ItemMoved:
                    case ListChangedType.PropertyDescriptorAdded:
                    case ListChangedType.PropertyDescriptorDeleted:
                    case ListChangedType.PropertyDescriptorChanged:
                    default:
                        Exception ex = new NotSupportedException("ListChangedType is not supported");
                        ex.Data.Add(nameof(e.ListChangedType), e.ListChangedType);
                        ex.Data.Add(nameof(e.NewIndex), e.NewIndex);
                        ex.Data.Add(nameof(e.OldIndex), e.OldIndex);
                        ex.Data.Add(nameof(e.PropertyDescriptor), e.PropertyDescriptor);
                        throw ex;
                }


            }
        }
    }
}
