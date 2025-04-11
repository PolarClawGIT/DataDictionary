using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.Main.Forms.Model.ComboBoxList;
using System;
using System.Collections;
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

        /// <summary>
        /// The currently Selected Definition.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IDefinitionIndex? SelectedDefinition
        {
            get
            {
                if (definitionData.SelectedValue is DefinitionNameList value)
                { return value; }
                else { return null; }
            }
            set
            {
                if (value is null) { definitionData.SelectedIndex = 0; }
                else
                {
                    DefinitionIndex index = new DefinitionIndex(value);
                    if (definitionData.Items is IEnumerable<DefinitionNameList> items &&
                        items.FirstOrDefault(w => index.Equals(w)) is DefinitionNameList item)
                    { definitionData.SelectedIndex = items.ToList().IndexOf(item); }
                    else { definitionData.SelectedIndex = 0; }
                }
            }
        }

        public Definition()
        {
            InitializeComponent();
            definitionTextData.AddTools(fullTextTools);
        }

        /// <summary>
        /// Sets up the Control.
        /// </summary>
        public void BindTo(BindingSource binding, IEnumerable<IDefinitionValue> values)
        {
            dataBinding = binding;
            DefinitionNameList.Load(definitionData, values);

            definitionData.DataBindings.Add(new Binding(nameof(definitionData.SelectedValue), binding, nameof(IDefinitionSubType.DefinitionId), false, DataSourceUpdateMode.OnPropertyChanged));
            definitionTextData.DataBindings.Add(new Binding(nameof(definitionTextData.RichText), binding, nameof(IDefinitionSubType.DefinitionText), false, DataSourceUpdateMode.OnPropertyChanged));
            definitionSummaryData.DataBindings.Add(new Binding(nameof(definitionSummaryData.Text), binding, nameof(IDefinitionSubType.DefinitionSummary), false, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void DefinitionData_SelectedIndexChanged(object sender, EventArgs e)
        { } // Gets called multiple times.

        private void DefinitionData_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (definitionData.SelectedValue is Guid value && value != Guid.Empty)
            {
                definitionTextData.Enabled = true;
                definitionSummaryData.Enabled = true;

                if (dataBinding is not null &&
                    dataBinding.DataSource is IList data &&
                    definitionData.SelectedItem is IDefinitionIndex selectedValue)
                {
                    DefinitionIndex key = new DefinitionIndex(selectedValue);
                    var currentValues = data.OfType<IDefinitionSubType>().ToList();

                    if (currentValues.FirstOrDefault(w => key.Equals(w)) is IDefinitionSubType currentValue)
                    { dataBinding.Position = currentValues.IndexOf(currentValue); }
                    else
                    {
                        if (dataBinding.AddNew() is IDefinitionSubType newItem)
                        { newItem.DefinitionId = value; }
                        else { throw new InvalidOperationException("AddNew did not create a IDefinitionSubType"); }
                    }
                }
            }
            else
            {
                definitionTextData.Enabled = false;
                definitionSummaryData.Enabled = false;
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

        private void SyncTextToSummary_Click(object sender, EventArgs e)
        {
            // Button does not cause definitionTextData to loose focus.
            // Thus does not post back to the data object.
            // Values had to be forced to get it to work.
            ActiveControl = null;

            if (dataBinding is not null && dataBinding.Current is IDefinitionSubType value)
            {
                value.DefinitionSummary = CleanUpText(definitionTextData.Text);
                dataBinding.ResetCurrentItem();
            }
        }


    }
}
