using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.Main.Controls;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Scripting.ComboBoxList
{
    record TransformNameMember : ITransformIndex, ITransformIndexName
    {
        /// <inheritdoc/>
        public Guid? TransformId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String TransformTitle { get; private set; } = String.Empty;

        public static void Load<T>(ComboBoxData control, IEnumerable<T> source, Guid? defaultTransformId = null, String? defaultTransformTitle = null)
            where T : ITransformIndex, ITransformIndexName
        {

            TransformNameMember memberItem = new TransformNameMember();
            BindingList<TransformNameMember> list = new BindingList<TransformNameMember>();
            list.Add(new TransformNameMember() { TransformId = Guid.Empty, TransformTitle = "(not specified)" });

            if (defaultTransformId is Guid defaultId && defaultId != Guid.Empty && !String.IsNullOrWhiteSpace(defaultTransformTitle) && source.Count(w => defaultId.Equals(w.TransformId)) == 0)
            { list.Add(new TransformNameMember() { TransformId = defaultId, TransformTitle = defaultTransformTitle }); }

            foreach (T item in source.OrderBy(o => o.TransformTitle))
            {
                if (item.TransformId is Guid TransformId && TransformId != Guid.Empty && item.TransformTitle is String TransformTitle)
                { list.Add(new TransformNameMember() { TransformId = TransformId, TransformTitle = TransformTitle }); }

            }

            control.DataSource = list;
            control.ValueMember = nameof(memberItem.TransformId);
            control.DisplayMember = nameof(memberItem.TransformTitle);
        }
    }
}
