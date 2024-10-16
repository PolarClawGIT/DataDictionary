using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.Main.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Security.ComboBoxList
{
    record RoleNameList : IRoleIndex, IRoleIndexName
    {
        /// <inheritdoc/>
        public Guid? RoleId { get; private set; } = Guid.Empty;

        public String? RoleName { get; private set; } = String.Empty;

        public static void Load<TRole>(DataGridViewComboBoxColumn control, IEnumerable<TRole> roles)
            where TRole : IRoleIndex, IRoleIndexName
        {
            BindingList<RoleNameList> list = new BindingList<RoleNameList>();
            foreach (var item in roles)
            { list.Add(new RoleNameList() { RoleId = item.RoleId, RoleName = item.RoleName }); }

            control.ValueMember = nameof(RoleNameList.RoleId);
            control.DisplayMember = nameof(RoleNameList.RoleName);
            control.DataSource = list;
        }

        public static void Load<TRole>(ComboBoxData control, IEnumerable<TRole> roles)
            where TRole : IRoleIndex, IRoleIndexName
        {
            BindingList<RoleNameList> list = new BindingList<RoleNameList>();
            foreach (var item in roles)
            { list.Add(new RoleNameList() { RoleId = item.RoleId, RoleName = item.RoleName }); }

            control.ValueMember = nameof(RoleNameList.RoleId);
            control.DisplayMember = nameof(RoleNameList.RoleName);
            control.DataSource = list;
        }
    }
}
