using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using System.Data;
using Toolbox.BindingTable;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Model.Controls
{
    partial class SubjectAreaData : UserControl
    {
        Dictionary<ListViewItem, ISubjectAreaValue> subjectItems = new Dictionary<ListViewItem, ISubjectAreaValue>();

        Func<IEnumerable<ISubjectAreaIndex>>? onGetSelected;
        Action<ISubjectAreaIndex>? onAddSubject;
        Action<ISubjectAreaValue>? onRemoveSubject;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BindingView<SubjectAreaValue> SubjectAreas { get; private set; } =
            new BindingView<SubjectAreaValue>(BusinessData.Model.SubjectAreas)
            { AllowEdit = false, AllowNew = false, AllowRemove = false };

        public SubjectAreaData()
        {
            InitializeComponent();
            subjectAreaList.ResizeColumns();
        }

        public void BindTo(
            Func<IEnumerable<ISubjectAreaIndex>> getSelected,
            Action<ISubjectAreaIndex> addSubject,
            Action<ISubjectAreaValue> removeSubject)
        {
            onGetSelected = getSelected;
            onAddSubject = addSubject;
            onRemoveSubject = removeSubject;
            IEnumerable<ISubjectAreaIndex> selected = onGetSelected();

            foreach (SubjectAreaValue item in SubjectAreas.OrderBy(o => o.SubjectAreaTitle))
            {
                ListViewItem value = new ListViewItem(item.SubjectAreaTitle);
                value.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = ((IPathValue)item).Path.MemberFullPath });
                SubjectAreaIndex key = new SubjectAreaIndex(item);

                if (selected.FirstOrDefault(w => key.Equals(w)) is ISubjectAreaIndex)
                { value.Checked = true; }
                else { value.Checked = false; }

                subjectItems.Add(value, item);
                subjectAreaList.Items.Add(value);
            }
        }

        private void subjectAreaData_Resize(object sender, EventArgs e)
        { subjectAreaList.ResizeColumns(); }

        private void SubjectAreaData_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (ActiveControl == subjectAreaList
                && onGetSelected is not null
                && onAddSubject is not null
                && onRemoveSubject is not null
                && subjectItems.TryGetValue(e.Item, out ISubjectAreaValue? subjectArea))
            {
                SubjectAreaIndex key = new SubjectAreaIndex(subjectArea);
                ISubjectAreaIndex? value = onGetSelected().FirstOrDefault(w => key.Equals(w));

                if (e.Item.Checked && value is null)
                { onAddSubject(subjectArea); }
                else if (!e.Item.Checked && value is not null)
                { onRemoveSubject(subjectArea); }
            }
        }
    }
}
