using DataDictionary.BusinessLayer.Model;
using DataDictionary.Main.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Domain.ComboBoxList
{
    record class SubjectAreaNameMember : ISubjectAreaIndex, ISubjectAreaIndexName
    {
        /// <inheritdoc/>
        public Guid? SubjectAreaId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String SubjectAreaTitle { get; private set; } = String.Empty;

        public static void Load(ComboBoxData control)
        {
            SubjectAreaNameMember propertyNameDataItem = new SubjectAreaNameMember();
            BindingList<SubjectAreaNameMember> list = new BindingList<SubjectAreaNameMember>();
            list.Add(new SubjectAreaNameMember() { SubjectAreaId = Guid.Empty, SubjectAreaTitle = "(select subject area)" });

            foreach (SubjectAreaValue item in BusinessData.ModelSubjectAreas)
            {
                if (item.SubjectAreaId is Guid subjectId && item.SubjectAreaTitle is String subjectTitle)
                { list.Add(new SubjectAreaNameMember() { SubjectAreaId = subjectId, SubjectAreaTitle = subjectTitle }); }
            }

            control.DataSource = list;
            control.ValueMember = nameof(propertyNameDataItem.SubjectAreaId);
            control.DisplayMember = nameof(propertyNameDataItem.SubjectAreaTitle);
        }

    }
}
