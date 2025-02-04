using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.Main.Forms.Model.ComboBoxList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Model.Controls
{
    partial class Definition : UserControl
    {
        BindingSource? dataBinding; // Pointer to the BindingSource.

        public Definition()
        { InitializeComponent(); }

        /// <summary>
        /// Sets up the Control.
        /// </summary>
        public void LoadControl(BindingSource binding, IEnumerable<IDefinitionValue> values)
        {
            dataBinding = binding;
            DefinitionNameList.Load(definitionData, values);

            definitionData.DataBindings.Add(new Binding(nameof(definitionData.SelectedValue), binding, nameof(IDefinitionSubType.DefinitionId), false, DataSourceUpdateMode.OnPropertyChanged));
            definitionTextData.DataBindings.Add(new Binding(nameof(definitionTextData.Rtf), binding, nameof(IDefinitionSubType.DefinitionText), false, DataSourceUpdateMode.OnPropertyChanged));
            definitionSummaryData.DataBindings.Add(new Binding(nameof(definitionTextData.Text), binding, nameof(IDefinitionSubType.DefinitionSummary), false, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void DefinitionData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (definitionData.SelectedValue is Guid value && value != Guid.Empty)
            {
                definitionTextData.Enabled = true;
                definitionSummaryData.Enabled = true;
                syncSummaryTextCommand.Enabled = true;

                if ((dataBinding is not null && dataBinding.Current is null) ||
                    (dataBinding is not null && dataBinding.Current is IDefinitionSubType current && current.DefinitionId != value))
                {
                    if (dataBinding.AddNew() is IDefinitionSubType newItem)
                    { newItem.DefinitionId = value; }
                }
            }
            else
            {
                definitionTextData.Enabled = false;
                definitionSummaryData.Enabled = false;
                syncSummaryTextCommand.Enabled = false;
            }
        }

        private void DefinitionTextData_Validated(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(definitionSummaryData.Text))
            { definitionSummaryData.Text = CleanUpText(definitionTextData.Text); }
            else if (priorText == definitionSummaryData.Text)
            { definitionSummaryData.Text = CleanUpText(definitionTextData.Text); }
        }

        String priorText = String.Empty;

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

            textValue = textValue.Replace("\n", Environment.NewLine);

            return textValue;
        }

        private void SyncSummaryTextCommand_Click(object sender, EventArgs e)
        { definitionSummaryData.Text = CleanUpText(definitionTextData.Text); }
    }
}
