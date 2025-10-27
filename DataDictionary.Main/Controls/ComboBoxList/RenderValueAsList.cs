using DataDictionary.Main.Controls;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record RenderValueAsList
    {
        public NodeRenderAsType ValueMember { get; init; } = NodeRenderAsType.none;
        public String DisplayMember { get { return ValueMember.GetEnumeration().DisplayName; } }

        static IReadOnlyList<RenderValueAsList> data = 
            [..Enum.GetValues<NodeRenderAsType>().
               Select(s => new RenderValueAsList() { ValueMember = s })];

        public static void Load(ComboBoxData control)
        {
            control.ValueMember = nameof(ValueMember);
            control.DisplayMember = nameof(DisplayMember);
            control.DataSource = data;
        }
    }
}
