using DataDictionary.BusinessLayer.AppModel;
using System.ComponentModel;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record class SubjectAreaNameList : ISubjectAreaIndex, ISubjectAreaIndexName
    {
        /// <inheritdoc/>
        public Guid? SubjectAreaId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String SubjectAreaTitle { get; private set; } = String.Empty;

        public static void Load(ComboBoxData control)
        {
            SubjectAreaNameList propertyNameDataItem = new SubjectAreaNameList();
            BindingList<SubjectAreaNameList> list = new BindingList<SubjectAreaNameList>();
            list.Add(new SubjectAreaNameList() { SubjectAreaId = Guid.Empty, SubjectAreaTitle = "(select subject area)" });

            foreach (SubjectAreaValue item in BusinessData.Model.SubjectAreas)
            {
                if (item.SubjectAreaId is Guid subjectId && item.SubjectAreaTitle is String subjectTitle)
                { list.Add(new SubjectAreaNameList() { SubjectAreaId = subjectId, SubjectAreaTitle = subjectTitle }); }
            }

            control.DataSource = list;
            control.ValueMember = nameof(propertyNameDataItem.SubjectAreaId);
            control.DisplayMember = nameof(propertyNameDataItem.SubjectAreaTitle);
        }

    }
}
