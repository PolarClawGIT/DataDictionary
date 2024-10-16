using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Forms.Domain.Controls
{
    partial class SubjectArea : UserControl
    {
        Dictionary<ListViewItem, SubjectAreaValue> subjectItems = new Dictionary<ListViewItem, SubjectAreaValue>();
        BindingSource bindingSubjectArea = new BindingSource();

        public SubjectArea()
        {
            InitializeComponent();
            subjectAreaData.ResizeColumns();
        }

        /// <summary>
        /// Associates the BindingSource to the control so the control can respond to binding events.
        /// </summary>
        /// <param name="binding">IEnumerable of ISubjectAreaIndex</param>
        public void BindTo(BindingSource binding)
        {
            bindingSubjectArea = binding;

            foreach (SubjectAreaValue item in BusinessData.SubjectAreas.OrderBy(o => o.SubjectAreaTitle))
            {
                ListViewItem value = new ListViewItem(item.SubjectAreaTitle);
                value.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = ((IPathValue)item).Path.MemberFullPath });
                SubjectAreaIndex key = new SubjectAreaIndex(item);

                if (bindingSubjectArea.DataSource is IEnumerable<ISubjectAreaIndex> subject
                    && subject.FirstOrDefault(w => key.Equals(w)) is ISubjectAreaIndex)
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
        public event EventHandler<SubjectAreaValue>? OnSubjectAdd;

        /// <summary>
        /// Triggered when on Checked when the SubjectArea is in the list.
        /// </summary>
        public event EventHandler<SubjectAreaValue>? OnSubjectRemove;

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

                    if (e.Item.Checked && value is null && OnSubjectAdd is EventHandler<SubjectAreaValue> addHandler)
                    { addHandler(this, subjectItems[e.Item]); }

                    if (!e.Item.Checked && value is not null && OnSubjectRemove is EventHandler<SubjectAreaValue> removeHandler)
                    { removeHandler(this, subjectItems[e.Item]); }
                }
            }
        }
    }
}
