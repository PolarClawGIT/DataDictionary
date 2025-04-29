using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using System.Data;
using Toolbox.BindingTable;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Model.Controls
{
    partial class SubjectArea : UserControl
    {
        Dictionary<ListViewItem, ISubjectAreaValue> subjectItems = new Dictionary<ListViewItem, ISubjectAreaValue>();
        BindingSource bindingSubjectArea = new BindingSource();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BindingView<SubjectAreaValue> SubjectAreas { get; private set; } =
            new BindingView<SubjectAreaValue>(BusinessData.Model.SubjectAreas)
            { AllowEdit = false, AllowNew = false, AllowRemove = false };


        //TODO: Repeat changes to Properties to Subject Areas.

        public SubjectArea()
        {
            InitializeComponent();
            subjectAreaData.ResizeColumns();
        }

        /// <summary>
        /// Associates the BindingSource to the control so the control can respond to binding events.
        /// </summary>
        /// <param name="binding">IEnumerable of ISubjectAreaIndex</param>
        /// <param name="values"></param>
        public void BindTo(BindingSource binding, IEnumerable<ISubjectAreaValue> values)
        {
            bindingSubjectArea = binding;

            foreach (ISubjectAreaValue item in values.OrderBy(o => o.SubjectAreaTitle))
            {
                ListViewItem value = new ListViewItem(item.SubjectAreaTitle);
                value.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = ((IPathValue)item).Path.MemberFullPath });
                SubjectAreaIndex key = new SubjectAreaIndex(item);

                if (binding.DataSource is IEnumerable<ISubjectAreaIndex> selected &&
                    selected.FirstOrDefault(w => key.Equals(w)) is ISubjectAreaIndex)
                { value.Checked = true; }
                else { value.Checked = false; }

                subjectItems.Add(value, item);
                subjectAreaData.Items.Add(value);
            }
        }

        private void subjectAreaData_Resize(object sender, EventArgs e)
        { subjectAreaData.ResizeColumns(); }

        /// <summary>
        /// Triggered when on Checked when the SubjectArea is not in the list.
        /// </summary>
        public event EventHandler<ISubjectAreaValue>? OnSubjectAdd;

        /// <summary>
        /// Triggered when on Checked when the SubjectArea is in the list.
        /// </summary>
        public event EventHandler<ISubjectAreaValue>? OnSubjectRemove;

        private void SubjectAreaData_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            // The checked event can be fired by something other then the user clicking the CheckBox (such as OnVisible).
            // This trap is to catch the checked event only when the control is the active control.
            if (ActiveControl == subjectAreaData)
            {
                if (subjectItems.ContainsKey(e.Item) && bindingSubjectArea.DataSource is IEnumerable<ISubjectAreaIndex> data)
                {
                    SubjectAreaIndex selectedKey = new SubjectAreaIndex(subjectItems[e.Item]);

                    ISubjectAreaIndex? value = data.FirstOrDefault(w => selectedKey.Equals(w));

                    if (e.Item.Checked && value is null && OnSubjectAdd is EventHandler<ISubjectAreaValue> addHandler)
                    { addHandler(this, subjectItems[e.Item]); }

                    if (!e.Item.Checked && value is not null && OnSubjectRemove is EventHandler<ISubjectAreaValue> removeHandler)
                    { removeHandler(this, subjectItems[e.Item]); }
                }
            }
        }
    }
}
