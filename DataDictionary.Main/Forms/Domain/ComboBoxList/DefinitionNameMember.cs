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
    record DefinitionNameMember : IDefinitionIndex, IDefinitionIndexName
    {
        /// <inheritdoc/>
        public Guid? DefinitionId { get; set; } = Guid.Empty;

        /// <inheritdoc/>
        public String DefinitionTitle { get; set; } = String.Empty;

        public static void Load(ComboBoxData control)
        {
            DefinitionNameMember DefinitionNameDataItem = new DefinitionNameMember();
            BindingList<DefinitionNameMember> list = new BindingList<DefinitionNameMember>();
            list.Add(new DefinitionNameMember() { DefinitionId = Guid.Empty, DefinitionTitle = "(select Definition Type)" });

            foreach (DefinitionValue item in BusinessData.DomainModel.Definitions)
            {
                if (item.DefinitionId is Guid DefinitionId && item.DefinitionTitle is String DefinitionTitle)
                { list.Add(new DefinitionNameMember() { DefinitionId = DefinitionId, DefinitionTitle = DefinitionTitle }); }
            }

            control.ValueMember = nameof(DefinitionNameDataItem.DefinitionId);
            control.DisplayMember = nameof(DefinitionNameDataItem.DefinitionTitle);
            control.DataSource = list;
        }

        public static void Load(DataGridViewComboBoxColumn control)
        {
            DefinitionNameMember DefinitionNameDataItem = new DefinitionNameMember();
            BindingList<DefinitionNameMember> list = new BindingList<DefinitionNameMember>();
            list.Add(new DefinitionNameMember() { DefinitionId = Guid.Empty, DefinitionTitle = "(select Definition Type)" });

            foreach (DefinitionValue item in BusinessData.DomainModel.Definitions)
            {
                if (item.DefinitionId is Guid DefinitionId && item.DefinitionTitle is String DefinitionTitle)
                { list.Add(new DefinitionNameMember() { DefinitionId = DefinitionId, DefinitionTitle = DefinitionTitle }); }
            }

            control.ValueMember = nameof(DefinitionNameDataItem.DefinitionId);
            control.DisplayMember = nameof(DefinitionNameDataItem.DefinitionTitle);
            control.DataSource = list;
        }
    }
}
