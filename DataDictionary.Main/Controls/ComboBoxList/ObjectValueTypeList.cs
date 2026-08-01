using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record ObjectValueTypeList
    {
        public ObjectValueType ValueMember { get; init; } = ObjectValueType.Null;
        public String DisplayMember { get { return ValueMember.GetName(); } }

        public static ObjectValueType NullValue { get; } = ObjectValueType.Null;

        static IReadOnlyList<ObjectValueTypeList> data = Enum.GetValues<ObjectValueType>().
            Select(s => new ObjectValueTypeList(s)).
            ToList();

        ObjectValueTypeList(ObjectValueType value) : base()
        { ValueMember = value; }

        public static void Load(ComboBoxData control)
        {
            control.ValueMember = nameof(ValueMember);
            control.DisplayMember = nameof(DisplayMember);
            control.DataSource = data;
        }
    }
}
