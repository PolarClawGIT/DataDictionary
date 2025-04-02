namespace DataDictionary.Main.Forms.General
{
    partial class HelpSubject
    {
        class ControlValue : HelpControlValue
        {
            public ListViewItem? ListItem { get; set; }

            public ControlValue(Control source) : base(source)
            { }

            public ControlValue(HelpControlValue source) : base(source)
            { }

            public static new IEnumerable<ControlValue> Create(Form source)
            { return Create(HelpControlValue.Create(source)); }

            public static IEnumerable<ControlValue> Create(IEnumerable<HelpControlValue> source)
            { return source.Select(s => new ControlValue(s)); }
        }
    }
}
