using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public event PropertyChangedEventHandler? PropertyChanged;

        void OnChanged(ITemplateNodeValue value)
        {
            NodeName = value.NodeName ?? String.Empty;

            if (PropertyChanged is PropertyChangedEventHandler handler)
            { handler(this, new PropertyChangedEventArgs(nameof(NodeName))); }
        }

        public static void Load(DataGridViewComboBoxColumn control, ITemplateIndex template, String? emptyText = null)
        {
            control.ValueMember = nameof(NodeId);
            control.DisplayMember = nameof(NodeName);
            control.DataSource = BuildList(template, emptyText);
        }

        public static void Load(ComboBoxData control, ITemplateIndex template, String? emptyText = null)
        {
            control.ValueMember = nameof(NodeId);
            control.DisplayMember = nameof(NodeName);
            control.DataSource = BuildList(template, emptyText);
        }

        public static void SelectValue(ComboBoxData control, ITemplateNodeIndex? select)
        {
            if(control.Items is IList<TemplateNodeList> comboList)
            {
                if(select is ITemplateNodeIndex)
                {
                    TemplateNodeIndex key = new TemplateNodeIndex(select);
                    if(comboList.FirstOrDefault(w => key.Equals(w)) is TemplateNodeList selectValue) 
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

        static BindingList<TemplateNodeList> BuildList(ITemplateIndex template, String? emptyText)
        {
            TemplateIndex templateKey = new TemplateIndex(template);

            BindingList<TemplateNodeList> comboList = new BindingList<TemplateNodeList>();
            if (!String.IsNullOrWhiteSpace(emptyText))
            { comboList.Add(new TemplateNodeList()); }

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
                        var newIndex = comboList.Count;

                        if (comboList.LastOrDefault(w => String.Compare(w.NodeName, newItem.NodeName ?? String.Empty, true) < 0) is TemplateNodeList priorItem)
                        { newIndex = comboList.IndexOf(priorItem); }

                        if (templateKey.Equals(newItem)
                            && !comboList.Any(w => new TemplateNodeIndex(newItem).Equals(w)))
                        { comboList.Insert(newIndex, new TemplateNodeList(newItem)); }

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
                    case ListChangedType.ItemMoved:
                        break;
                    case ListChangedType.ItemChanged:
                        var changedItem = BusinessData.Scripting.Nodes[e.NewIndex];
                        var changedKey = new TemplateNodeIndex(changedItem);

                        if (templateKey.Equals(changedItem))
                        {
                            comboList.RaiseListChangedEvents = false;

                            comboList.RemoveRange(w => changedKey.Equals(w));
                            var changedIndex = comboList.Count;

                            if (comboList.LastOrDefault(w => String.Compare(w.NodeName, changedItem.NodeName ?? String.Empty, true) < 0) is TemplateNodeList priorChangedItem)
                            { changedIndex = comboList.IndexOf(priorChangedItem); }

                            if (templateKey.Equals(changedItem)
                                && !comboList.Any(w => new TemplateNodeIndex(changedItem).Equals(w)))
                            { comboList.Insert(changedIndex, new TemplateNodeList(changedItem)); }

                            comboList.RaiseListChangedEvents = true;
                            comboList.ResetBindings();


                            //TODO: Discover if the combobox control will reset to the choosen item or stick to the index of the item.
                            // can the binding list Move event be fired? Would that handle this scenario?
                        }
                        break;
                    case ListChangedType.PropertyDescriptorAdded:
                    case ListChangedType.PropertyDescriptorDeleted:
                    case ListChangedType.PropertyDescriptorChanged:
                    default:
                        break;
                }


            }
        }
    }
}
