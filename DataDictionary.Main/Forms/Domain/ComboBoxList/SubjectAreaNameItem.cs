using DataDictionary.DataLayer.DomainData.SubjectArea;
using DataDictionary.Main.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Domain.ComboBoxList
{
    record SubjectAreaNameItem
    {
        public Guid SubjectAreaId { get; set; } = Guid.Empty;
        public String SubjectAreaTitle { get; set; } = String.Empty;

        public static void Load(ComboBoxData control)
        {
            SubjectAreaNameItem nameOf;

            BindingList<SubjectAreaNameItem> list = new BindingList<SubjectAreaNameItem>();
            list.Add(new SubjectAreaNameItem() { SubjectAreaId = Guid.Empty, SubjectAreaTitle = "(not assigned)" });

            foreach (DomainSubjectAreaItem item in Program.Data.DomainSubjectAreas)
            {
                if (item.SubjectAreaId is Guid subjectAreaId && item.SubjectAreaTitle is String subjectAreaTitle)
                { list.Add(new SubjectAreaNameItem() { SubjectAreaId = subjectAreaId, SubjectAreaTitle = subjectAreaTitle }); }
            }

            control.DataSource = list;
            control.ValueMember = nameof(nameOf.SubjectAreaId);
            control.DisplayMember = nameof(nameOf.SubjectAreaTitle);
        }
    }
}
