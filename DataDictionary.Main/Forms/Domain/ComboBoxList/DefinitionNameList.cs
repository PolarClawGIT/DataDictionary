using DataDictionary.BusinessLayer.Domain;
using DataDictionary.Main.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Domain.ComboBoxList
{
    record DefinitionNameList : IDefinitionIndex, IDefinitionIndexName
    {
        /// <inheritdoc/>
        public Guid? DefinitionId { get; set; } = Guid.Empty;

        /// <inheritdoc/>
        public String DefinitionTitle { get; set; } = String.Empty;

        public static void Load(ComboBoxData control)
        {
            DefinitionNameList DefinitionNameDataItem = new DefinitionNameList();
            BindingList<DefinitionNameList> list = new BindingList<DefinitionNameList>();
            list.Add(new DefinitionNameList() { DefinitionId = Guid.Empty, DefinitionTitle = "(select Definition Type)" });

            foreach (DefinitionValue item in BusinessData.DomainModel.Definitions)
            {
                if (item.DefinitionId is Guid DefinitionId && item.DefinitionTitle is String DefinitionTitle)
                { list.Add(new DefinitionNameList() { DefinitionId = DefinitionId, DefinitionTitle = DefinitionTitle }); }
            }

            control.ValueMember = nameof(DefinitionNameDataItem.DefinitionId);
            control.DisplayMember = nameof(DefinitionNameDataItem.DefinitionTitle);
            control.DataSource = list;
        }

        public static void Load(DataGridViewComboBoxColumn control)
        {
            DefinitionNameList DefinitionNameDataItem = new DefinitionNameList();
            BindingList<DefinitionNameList> list = new BindingList<DefinitionNameList>();
            list.Add(new DefinitionNameList() { DefinitionId = Guid.Empty, DefinitionTitle = "(select Definition Type)" });

            foreach (DefinitionValue item in BusinessData.DomainModel.Definitions)
            {
                if (item.DefinitionId is Guid DefinitionId && item.DefinitionTitle is String DefinitionTitle)
                { list.Add(new DefinitionNameList() { DefinitionId = DefinitionId, DefinitionTitle = DefinitionTitle }); }
            }

            control.ValueMember = nameof(DefinitionNameDataItem.DefinitionId);
            control.DisplayMember = nameof(DefinitionNameDataItem.DefinitionTitle);
            control.DataSource = list;
        }
    }
}
