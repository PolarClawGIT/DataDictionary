using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.Main.Controls;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Scripting.ComboBoxList
{
    record DefinitionNameMember : IDefinitionIndex, IDefinitionIndexName
    {
        /// <inheritdoc/>
        public Guid? SchemaId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String SchemaTitle { get; private set; } = String.Empty;

        public static void Load<T>(ComboBoxData control, IEnumerable<T> source, Guid? defaultSchemaId = null, String? defaultSchemaTitle = null)
            where T : IDefinitionIndex, IDefinitionIndexName
        {

            DefinitionNameMember memberItem = new DefinitionNameMember();
            BindingList<DefinitionNameMember> list = new BindingList<DefinitionNameMember>();
            list.Add(new DefinitionNameMember() { SchemaId = Guid.Empty, SchemaTitle = "(not specified)" });

            if (defaultSchemaId is Guid defaultId && defaultId != Guid.Empty && !String.IsNullOrWhiteSpace(defaultSchemaTitle) && source.Count(w => defaultId.Equals(w.SchemaId)) == 0)
            { list.Add(new DefinitionNameMember() { SchemaId = defaultId, SchemaTitle = defaultSchemaTitle }); }

            foreach (T item in source.OrderBy(o => o.SchemaTitle))
            {
                if (item.SchemaId is Guid SchemaId && SchemaId != Guid.Empty && item.SchemaTitle is String SchemaTitle)
                { list.Add(new DefinitionNameMember() { SchemaId = SchemaId, SchemaTitle = SchemaTitle }); }

            }

            control.DataSource = list;
            control.ValueMember = nameof(memberItem.SchemaId);
            control.DisplayMember = nameof(memberItem.SchemaTitle);
        }
    }
}
