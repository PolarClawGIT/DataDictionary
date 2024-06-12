using DataDictionary.BusinessLayer.Domain;
using DataDictionary.Main.Forms.Domain.ComboBoxList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Forms.Domain.Controls
{
    partial class DomainDefinition : UserControl
    {
        public String ApplyText { get { return applyCommand.Text; } set { applyCommand.Text = value; } }
        public Image? ApplyImage { get { return applyCommand.Image; } set { applyCommand.Image = value; } }

        [Browsable(false)]
        public IDefinitionIndex? Definition
        {
            get
            {
                if (definitionData.SelectedItem is IDefinitionIndex value)
                { return value; }
                else { return null; }
            }
        }

        [Browsable(false)]
        public Guid? DefinitionId
        {
            get
            {
                if (definitionData.SelectedItem is IDefinitionIndex item)
                { return item.DefinitionId; }
                else { return null; }
            }
            set
            {
                if (definitionData.DataSource is IList<IDefinitionIndex> items)
                {
                    if (items.FirstOrDefault(w => w.DefinitionId == value) is DefinitionNameList item)
                    { definitionData.SelectedItem = item; }
                }
            }
        }

        [Browsable(false)]
        public String DefinitionSummary
        {
            get { return definitionSummaryData.Text; }
            set { definitionSummaryData.Text = value; }
        }

        [Browsable(false)]
        public String? DefinitionText
        {
            get { return definitionTextData.Rtf; }
            set { definitionTextData.Rtf = value; }
        }

        public Boolean ReadOnly
        {
            get { return !definitionLayout.Enabled; }
            set { definitionLayout.Enabled = !value; }
        }

        public DomainDefinition()
        {
            InitializeComponent();
            DefinitionNameList.Load(definitionData);
        }

        public event EventHandler? OnApply;
        private void ApplyCommand_Click(object sender, EventArgs e)
        {
            if (OnApply is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }

        private void DefinitionTextData_Validated(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(definitionSummaryData.Text))
            { definitionSummaryData.Text = CleanUpText(definitionTextData.Text); }
            else if (priorText == definitionSummaryData.Text)
            { definitionSummaryData.Text = CleanUpText(definitionTextData.Text); }
        }

        String priorText = String.Empty;
        private void DefinitionTextData_Enter(object sender, EventArgs e)
        { priorText = CleanUpText(definitionTextData.Text); }

        String CleanUpText(String richTextValue)
        {
            String textValue = richTextValue;
            if (textValue.Length > 1000)
            {
                textValue = textValue.Substring(0, 1000); // Maximum Length allowed by DB.
                Int32 lastNewLine = textValue.LastIndexOf(Environment.NewLine);
                Int32 lastSpace = textValue.LastIndexOf(" ");
                Int32 lastPeriod = textValue.LastIndexOf("."); // Period

                if (lastPeriod > 0 && lastPeriod > lastNewLine)
                { textValue = textValue.Substring(0, lastPeriod).Trim(); }
                else if (lastNewLine > 0)
                { textValue = textValue.Substring(0, lastNewLine).Trim(); }
                else if (lastSpace > 0)
                { textValue = textValue.Substring(0, lastSpace).Trim(); }
            }

            //textValue = textValue.Replace(Environment.NewLine, " ");
            //textValue = textValue.Replace("\t", " ");
            textValue = textValue.Replace("\n", Environment.NewLine);

            return textValue;
        }

        private void DefinitionData_Validated(object sender, EventArgs e)
        {
            if (definitionData.SelectedValue is Guid value && value == Guid.Empty)
            { applyCommand.Enabled = false; }
            else { applyCommand.Enabled = true; }
        }

        private void definitionData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (definitionData.SelectedValue is Guid value && value != Guid.Empty)
            { applyCommand.Enabled = true; }
            else { applyCommand.Enabled = false; }
        }

        private void Sync_Click(object sender, EventArgs e)
        { definitionSummaryData.Text = CleanUpText(definitionTextData.Text); }
    }
}
