using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Controls.ComboBoxList;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Controls
{
    partial class DefinitionData : UserControl
    {
        BindingSource? dataBinding; // Pointer to the BindingSource.

        public DefinitionData()
        {
            InitializeComponent();

            syncTextToSummary.Image = ScopeType.ModelDefinition.GetImage(ButtonType.Sync);

            definitionTextData.AddTools(fullTextTools);
        }

        public void BindTo(BindingSource binding, Func<IDefinitionSubType> newDefinition)
        {
            dataBinding = binding;
            DefinitionNameList.Load(definitionTypeData);
            DefinitionNameList.Load(definitionColumn);

            definitionTypeData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedValue), binding, nameof(IDefinitionSubType.DefinitionId), false, DataSourceUpdateMode.OnPropertyChanged));
            definitionTextData.DataBindings.Add(new Binding(nameof(RichTextBoxData.RichText), binding, nameof(IDefinitionSubType.DefinitionText), false, DataSourceUpdateMode.OnPropertyChanged));
            definitionSummaryData.DataBindings.Add(new Binding(nameof(TextBox.Text), binding, nameof(IDefinitionSubType.DefinitionSummary), false, DataSourceUpdateMode.OnPropertyChanged));
            
            definitionGrid.AutoGenerateColumns = false;
            definitionGrid.DataSource = dataBinding;

            dataBinding.AddingNew += DataBinding_AddingNew;

            void DataBinding_AddingNew(Object? sender, AddingNewEventArgs e)
            { e.NewObject = newDefinition(); }

        }

        private void DefinitionData_SelectedIndexChanged(object sender, EventArgs e)
        { } // Gets called multiple times.

        private void DefinitionData_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (definitionTypeData.SelectedValue is Guid value && value != Guid.Empty)
            {
                definitionTextData.Enabled = true;
                definitionSummaryData.Enabled = true;

                if (dataBinding is not null &&
                    //dataBinding.DataSource is IList data &&
                    definitionTypeData.SelectedItem is IDefinitionIndex selectedValue)
                {
                    DefinitionIndex key = new DefinitionIndex(selectedValue);
                    var currentValues = dataBinding.List.OfType<IDefinitionSubType>().ToList();

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
