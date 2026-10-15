using DataDictionary.BusinessLayer.AppScripting;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record SchemaDefinitionList : ISchemaDefinitionIndex, ISchemaDefinitionIndexName, IBindingPropertyChanged
    {
        /// <inheritdoc/>
        public Guid? SchemaId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String SchemaTitle { get; private set; } = String.Empty;

        public static Guid? NullValue { get; } = Guid.Empty;

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        SchemaDefinitionList(ISchemaDefinitionValue value)
        {
            SchemaId = value.SchemaId;
            SchemaTitle = value.SchemaTitle ?? String.Empty;
        }

        SchemaDefinitionList(String? emptyText = "(n/a)")
        { SchemaTitle = emptyText ?? "(n/a)"; }


        public static void Load(ComboBoxData control, String? emptyText = null)
        {
            BindingComboList<SchemaDefinitionList> comboList = BuildList(BusinessData.Templates.Schemata, emptyText);
            comboList.BindTo(control, () => nameof(SchemaId), () => nameof(SchemaTitle));
        }

        public static void Load(ComboBoxData control, IBindingList<SchemaDefinitionValue> source, String? emptyText = null)
        {
            BindingComboList<SchemaDefinitionList> comboList = BuildList(source, emptyText);
            comboList.BindTo(control, () => nameof(SchemaId), () => nameof(SchemaTitle));
        }

        static BindingComboList<SchemaDefinitionList> BuildList<TData>(TData source, String? emptyText = null)
            where TData : IBindingList<SchemaDefinitionValue>
        {
            BindingComboList<SchemaDefinitionList> comboList = new BindingComboList<SchemaDefinitionList>();

            comboList.BuildList(
                source: source,
                constructor: (c) => new SchemaDefinitionList(c),
                onItemChanged: (s, t) =>
                {
                    t.SchemaTitle = s.SchemaTitle ?? String.Empty;
                    t.OnPropertyChanged(t.PropertyChanged, nameof(t.SchemaTitle));
                },
                orderBy: (o) => o.SchemaTitle,
                areEqual: (a, b) => new SchemaDefinitionIndex(a).Equals(b),
                emptyValue: () => new SchemaDefinitionList(emptyText));

            return comboList;
        }

    }
}
