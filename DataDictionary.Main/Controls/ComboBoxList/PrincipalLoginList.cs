using DataDictionary.BusinessLayer.AppSecurity;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record PrincipalLoginList : IPrincipalIndex, IPrincipalIndexName, IBindingPropertyChanged
    {
        /// <inheritdoc/>
        public Guid? PrincipalId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String PrincipalLogin { get; private set; } = String.Empty;

        PrincipalLoginList(IPrincipalValue value)
        {
            PrincipalId = value.PrincipalId;
            PrincipalLogin = value.PrincipalLogin ?? String.Empty;
        }

        PrincipalLoginList(String? emptyText = "(n/a)")
        { PrincipalLogin = emptyText ?? "(n/a)"; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public static void Load(ComboBoxData control, IPrincipalData data, String? emptyText = null)
        {
            BindingComboList<PrincipalLoginList> comboList = BuildList(data, emptyText);
            comboList.BindTo(control, () => nameof(PrincipalId), () => nameof(PrincipalLogin));
        }

        public static void Load(DataGridViewComboBoxColumn control, IPrincipalData data, String? emptyText = null)
        {
            BindingComboList<PrincipalLoginList> comboList = BuildList(data, emptyText);
            comboList.BindTo(control, () => nameof(PrincipalId), () => nameof(PrincipalLogin));
        }

        static BindingComboList<PrincipalLoginList> BuildList(IPrincipalData data, String? emptyText = null)
        {
            BindingComboList<PrincipalLoginList> comboList = new BindingComboList<PrincipalLoginList>();

            comboList.BuildList(
                source: data,
                constructor: (c) => new PrincipalLoginList(c),
                onItemChanged: (s, t) =>
                {
                    t.PrincipalLogin = s.PrincipalLogin ?? String.Empty;
                    t.OnPropertyChanged(t.PropertyChanged, nameof(t.PrincipalLogin));
                },
                orderBy: (o) => o.PrincipalLogin,
                areEqual: (a, b) => new PrincipalIndex(a).Equals(b),
                emptyValue: () => new PrincipalLoginList(emptyText));

            return comboList;
        }

    }
}
