using DataDictionary.BusinessLayer.Domain;
using DataDictionary.Main.Controls;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Domain.ComboBoxList
{
    record EntityNameList : IEntityIndex, IEntityIndexName
    {
        /// <inheritdoc/>
        public Guid? EntityId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String? EntityTitle { get; private set; } = String.Empty;

        /// <summary>
        /// Loads the ComboBoxData with Entities.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="source"></param>
        /// <param name="defaultEntityId"></param>
        /// <param name="defaultEntityTitle"></param>
        public static void Load<T>(ComboBoxData control, IEnumerable<T> source, Guid? defaultEntityId = null, String? defaultEntityTitle = null)
            where T : IEntityIndex, IEntityIndexName
        {
            EntityNameList propertyNameDataItem = new EntityNameList();
            BindingList<EntityNameList> list = new BindingList<EntityNameList>();
            list.Add(new EntityNameList() { EntityId = Guid.Empty, EntityTitle = "(not specified)" });

            if (defaultEntityId is Guid defaultId && defaultId != Guid.Empty && !String.IsNullOrWhiteSpace(defaultEntityTitle) && source.Count(w => defaultId.Equals(w.EntityId)) == 0)
            { list.Add(new EntityNameList() { EntityId = defaultId, EntityTitle = defaultEntityTitle }); }

            foreach (T item in source.OrderBy(o => o.EntityTitle))
            {
                if (item.EntityId is Guid EntityId && EntityId != Guid.Empty && item.EntityTitle is String EntityTitle)
                { list.Add(new EntityNameList() { EntityId = EntityId, EntityTitle = EntityTitle }); }

            }

            control.DataSource = list;
            control.ValueMember = nameof(propertyNameDataItem.EntityId);
            control.DisplayMember = nameof(propertyNameDataItem.EntityTitle);
        }
    }
}
