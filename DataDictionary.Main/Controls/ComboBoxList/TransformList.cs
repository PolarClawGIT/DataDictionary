using DataDictionary.BusinessLayer.AppScripting;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record TransformList : ITransformIndex, ITransformIndexName, IBindingPropertyChanged
    {
        /// <inheritdoc/>
        public Guid? TransformId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String TransformTitle { get; private set; } = String.Empty;

        public static Guid? NullValue { get; } = Guid.Empty;

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        TransformList(ITransformValue value)
        {
            TransformId = value.TransformId;
            TransformTitle = value.TransformTitle ?? String.Empty;
        }

        TransformList(String? emptyText = "(n/a)")
        { TransformTitle = emptyText ?? "(n/a)"; }


        public static void Load(ComboBoxData control, String? emptyText = null)
        {
            BindingComboList<TransformList> comboList = new BindingComboList<TransformList>();

            comboList.BuildList(
                source: BusinessData.Templates.Transforms,
                constructor: (c) => new TransformList(c),
                areEqual: (a, b) => new TransformIndex(a).Equals(b),
                emptyValue: () => new TransformList(emptyText),
                onItemChanged: (s, t) =>
                {
                    t.TransformTitle = s.TransformTitle ?? String.Empty;
                    t.OnPropertyChanged(t.PropertyChanged, nameof(t.TransformTitle));
                },
                orderBy: (o) => o.TransformTitle);
            comboList.BindTo(control, () => nameof(TransformId), () => nameof(TransformTitle));
        }

        public static void Load(ComboBoxData control, IBindingList<TransformValue> source, String? emptyText = null)
        {
            BindingComboList<TransformList> comboList = new BindingComboList<TransformList>();

            comboList.BuildList(
                source: source,
                constructor: (c) => new TransformList(c),
                areEqual: (a, b) => new TransformIndex(a).Equals(b),
                emptyValue: () => new TransformList(emptyText),
                onItemChanged: (s, t) =>
                {
                    t.TransformTitle = s.TransformTitle ?? String.Empty;
                    t.OnPropertyChanged(t.PropertyChanged, nameof(t.TransformTitle));
                },
                orderBy: (o) => o.TransformTitle);

            comboList.BindTo(control, () => nameof(TransformId), () => nameof(TransformTitle));
        }

        public override Int32 GetHashCode() // Needed by BindingComboList.
        { return TransformId.GetHashCode(); }
    }
}
