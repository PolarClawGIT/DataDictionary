using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.Main.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record RoleNameList : IRoleIndex, IRoleIndexName, IBindingPropertyChanged
    {
        /// <inheritdoc/>
        public Guid? RoleId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String RoleName { get; private set; } = String.Empty;

        RoleNameList(IRoleValue value)
        {
            RoleId = value.RoleId;
            RoleName = value.RoleName ?? String.Empty;
        }

        RoleNameList(String? emptyText = "(n/a)")
        { RoleName = emptyText ?? "(n/a)"; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public static void Load(ComboBoxData control, IRoleData data, String? emptyText = null)
        {
            BindingComboList<RoleNameList> comboList = BuildList(data, emptyText);
            comboList.BindTo(control, () => nameof(RoleId), () => nameof(RoleName));
        }

        public static void Load(DataGridViewComboBoxColumn control, IRoleData data, String? emptyText = null)
        {
            BindingComboList<RoleNameList> comboList = BuildList(data, emptyText);
            comboList.BindTo(control, () => nameof(RoleId), () => nameof(RoleName));
        }

        static BindingComboList<RoleNameList> BuildList(IRoleData data, String? emptyText = null)
        {
            BindingComboList<RoleNameList> comboList = new BindingComboList<RoleNameList>();

            comboList.BuildList(
                source: data,
                constructor: (c) => new RoleNameList(c),
                onItemChanged: (s, t) =>
                {
                    t.RoleName = s.RoleName ?? String.Empty;
                    t.OnPropertyChanged(t.PropertyChanged, nameof(t.RoleName));
                },
                orderBy: (o) => o.RoleName,
                areEquel: (a, b) => new RoleIndex(a).Equals(b),
                emptyValue: () => new RoleNameList(emptyText));

            return comboList;
        }
    }
}
