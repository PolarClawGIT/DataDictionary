using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Main.Controls;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Scripting.ComboBoxList
{
    record TemplateNameList : ITemplateIndex, ITemplateIndexName
    {
        /// <inheritdoc/>
        public Guid? TemplateId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String TemplateTitle { get; private set; } = String.Empty;

        public static void Load(DataGridViewComboBoxColumn control)
        {
            BindingList<TemplateNameList> list = new BindingList<TemplateNameList>();

            foreach (TemplateNameList item in BusinessData.ScriptingTemplate.Templates.
                Select(s => new TemplateNameList()
                {
                    TemplateId = s.TemplateId,
                    TemplateTitle = s.TemplateTitle ?? String.Empty
                }).OrderBy(o => o.TemplateTitle))
            { list.Add(item); }

            control.ValueMember = nameof(TemplateId);
            control.DisplayMember = nameof(TemplateTitle);
            control.DataSource = list;
        }

        public static void Load(ComboBoxData control)
        {
            BindingList<TemplateNameList> list = new BindingList<TemplateNameList>();

            foreach (TemplateNameList item in BusinessData.ScriptingTemplate.Templates.
                Select(s => new TemplateNameList()
                {
                    TemplateId = s.TemplateId,
                    TemplateTitle = s.TemplateTitle ?? String.Empty
                }))
            { list.Add(item); }

            control.ValueMember = nameof(TemplateId);
            control.DisplayMember = nameof(TemplateTitle);
            control.DataSource = list;
        }

    }
}
