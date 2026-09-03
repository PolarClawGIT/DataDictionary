using DataDictionary.BusinessLayer.AppModel;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record EntityNameList : IEntityIndex, IEntityIndexName, IBindingPropertyChanged
    {
        /// <inheritdoc/>
        public Guid? EntityId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String EntityTitle { get; private set; } = String.Empty;

        EntityNameList(IEntityValue value)
        {
            EntityId = value.EntityId;
            EntityTitle = value.EntityTitle ?? String.Empty;
        }

        EntityNameList(String? emptyText = "(n/a)")
        { EntityTitle = emptyText ?? "(n/a)"; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public static void Load(ComboBoxData control, String? emptyText = null)
        {
            BindingComboList<EntityNameList> comboList = BuildList(emptyText);
            comboList.BindTo(control, () => nameof(EntityId), () => nameof(EntityTitle));
        }

        public static void Load(DataGridViewComboBoxColumn control, String? emptyText = null)
        {
            BindingComboList<EntityNameList> comboList = BuildList(emptyText);
            comboList.BindTo(control, () => nameof(EntityId), () => nameof(EntityTitle));
        }

        static BindingComboList<EntityNameList> BuildList(String? emptyText = null)
        {
            BindingComboList<EntityNameList> comboList = new BindingComboList<EntityNameList>();

            comboList.BuildList(
                source: BusinessData.Model.Entity.Entities,
                constructor: (c) => new EntityNameList(c),
                onItemChanged: (s, t) =>
                {
                    t.EntityTitle = s.EntityTitle ?? String.Empty;
                    t.OnPropertyChanged(t.PropertyChanged, nameof(t.EntityTitle));
                },
                orderBy: (o) => o.EntityTitle,
                areEqual: (a, b) => new EntityIndex(a).Equals(b),
                emptyValue: () => new EntityNameList(emptyText));

            return comboList;
        }
    }
}
