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
    record PrincipalLoginList : IPrincipalIndex, IPrincipalIndexName
    {
        /// <inheritdoc/>
        public Guid? PrincipalId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String? PrincipalLogin { get; private set; } = String.Empty;

        public static void Load<TPrincipal>(DataGridViewComboBoxColumn control, IEnumerable<TPrincipal> Principals)
            where TPrincipal : IPrincipalIndex, IPrincipalIndexName
        {
            BindingList<PrincipalLoginList> list = new BindingList<PrincipalLoginList>();
            foreach (var item in Principals)
            { list.Add(new PrincipalLoginList() { PrincipalId = item.PrincipalId, PrincipalLogin = item.PrincipalLogin }); }

            control.ValueMember = nameof(PrincipalLoginList.PrincipalId);
            control.DisplayMember = nameof(PrincipalLoginList.PrincipalLogin);
            control.DataSource = list;
        }

        public static void Load<TPrincipal>(ComboBoxData control, IEnumerable<TPrincipal> Principals)
            where TPrincipal : IPrincipalIndex, IPrincipalIndexName
        {
            BindingList<PrincipalLoginList> list = new BindingList<PrincipalLoginList>();
            foreach (var item in Principals)
            { list.Add(new PrincipalLoginList() { PrincipalId = item.PrincipalId, PrincipalLogin = item.PrincipalLogin }); }

            control.ValueMember = nameof(PrincipalLoginList.PrincipalId);
            control.DisplayMember = nameof(PrincipalLoginList.PrincipalLogin);
            control.DataSource = list;
        }
    }
}
