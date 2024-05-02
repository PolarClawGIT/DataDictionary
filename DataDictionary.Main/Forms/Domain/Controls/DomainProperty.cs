using DataDictionary.DataLayer.DomainData;
using DataDictionary.Main.Forms.Domain.ComboBoxList;
using DataDictionary.DataLayer.ApplicationData.Property;

namespace DataDictionary.Main.Forms.Domain.Controls
{
    partial class DomainProperty : UserControl
    {
        Func<IDomainProperty?> GetCurrent = () => { return null; };
        public String ApplyText { get { return applyCommand.Text; } set { applyCommand.Text = value; } }
        public Image? ApplyImage { get { return applyCommand.Image; } set { applyCommand.Image = value; } }

        public Boolean ReadOnly
        {
            get { return !propertyLayout.Enabled; }
            set { propertyLayout.Enabled = !value; }
        }

        public DomainProperty()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Performs the Bind of the Data fields.
        /// </summary>
        /// <remarks>
        /// UserControl.Load event is fired at DESIGN time.
        /// This causes issues with Data Binding.
        /// Call this method to bind the data
        /// </remarks>
        public void BindData(BindingSource propertyBinding)
        {
            PropertyNameMember.Load(propertyTypeData);
            IDomainProperty nameOfValues;
            GetCurrent = () => { return propertyBinding.Current as IDomainProperty; };

            propertyTypeData.DataBindings.Add(new Binding(nameof(propertyTypeData.SelectedValue), propertyBinding, nameof(nameOfValues.PropertyId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty));
            propertyValueData.DataBindings.Add(new Binding(nameof(propertyValueData.Text), propertyBinding, nameof(nameOfValues.PropertyValue)));
            propertyDefinitionData.DataBindings.Add(new Binding(nameof(propertyDefinitionData.Rtf), propertyBinding, nameof(nameOfValues.DefinitionText)));
        }

        public void RefreshControls()
        {
            if (propertyTypeData.SelectedItem is PropertyNameMember selected
                && GetCurrent() is IDomainProperty currentRow)
            {
                if (BusinessData.ApplicationData.Properties.FirstOrDefault(w => w.PropertyId == selected.PropertyId) is PropertyItem property)
                {
                    // Cleanup not performed by Binding
                    propertyChoiceData.Items.Clear();
                    propertyValueData.Text = String.Empty;

                    // Setup the Choice check-boxes
                    List<String> selectedChoices = new List<String>();
                    if (currentRow.PropertyValue is not null)
                    { selectedChoices.AddRange(currentRow.PropertyValue.Split(",")); }

                    foreach (PropertyItem.ChoiceItem choice in property.Choices)
                    {
                        String newItem = choice.Choice;
                        propertyChoiceData.Items.Add(newItem);
                        Int32 index = propertyChoiceData.Items.IndexOf(newItem);
                        Boolean isChecked = selectedChoices.Contains(newItem);
                        propertyChoiceData.SetItemChecked(index, isChecked);
                    }

                    // Enabled or Disable controls based on select item
                    propertyTypeData.Enabled = true;
                    propertyChoiceData.Enabled = (property.IsChoice == true);
                    propertyValueData.Enabled = (property.IsExtendedProperty == true || property.IsFrameworkSummary == true);
                    propertyDefinitionData.Enabled = (property.IsDefinition == true);
                }
                else
                {
                    propertyTypeData.Enabled = true;
                    propertyChoiceData.Enabled = false;
                    propertyValueData.Enabled = false;
                    propertyDefinitionData.Enabled = false;
                }
            }
            else
            {
                propertyChoiceData.Items.Clear();
                propertyTypeData.Enabled = false;
                propertyChoiceData.Enabled = false;
                propertyValueData.Enabled = false;
                propertyDefinitionData.Enabled = false;
            }
        }

        private void propertyTypeData_SelectedIndexChanged(object sender, EventArgs e)
        { RefreshControls(); }

        private void PropertyChoiceData_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            List<String> values = new List<String>();

            foreach (var item in propertyChoiceData.CheckedItems)
            {
                if (item is String value)
                { values.Add(value); }
            }

            if (e.NewValue == CheckState.Checked)
            {
                if (propertyChoiceData.Items[e.Index] is String addValue)
                { values.Add(addValue); }
            }
            else
            {
                if (propertyChoiceData.Items[e.Index] is String deletValue && values.Contains(deletValue))
                { values.Remove(deletValue); }
            }

            if (GetCurrent() is IDomainProperty current)
            { current.PropertyValue = String.Join(", ", values); }
        }

        private void PropertyChoiceData_EnabledChanged(object sender, EventArgs e)
        {
            if (propertyChoiceData.Enabled)
            { propertyChoiceData.ResetBackColor(); }
            else { propertyChoiceData.BackColor = SystemColors.Control; }
        }

        private void PropertyDefinitionData_Validated(object sender, EventArgs e)
        {
            String value = propertyDefinitionData.Text.Trim();
            Int32 maxCutOff = value.Length;
            if (maxCutOff > 4000) { maxCutOff = 4000; }

            Int32 firstBreak = value.Substring(0, maxCutOff).IndexOf("\n\n");
            Int32 lastReturn = value.Substring(0, maxCutOff).LastIndexOf("\n");
            Int32 lastSpace = value.Substring(0, maxCutOff).LastIndexOf(" ");

            if (firstBreak > 0) { value = value.Substring(0, firstBreak); }
            else if (lastReturn > 0) { value = value.Substring(0, lastReturn); }
            else if (lastSpace > 0) { value = value.Substring(0, lastSpace); }
            else { value = value.Substring(0, maxCutOff); }

            value = value.Replace(Environment.NewLine, " ");
            value = value.Replace("\t", " ");
            value = value.Replace("\n", " ");

            if (GetCurrent() is IDomainProperty current)
            { current.PropertyValue = value.Trim(); }
        }

        public event EventHandler? OnApply;
        private void ApplyCommand_Click(object sender, EventArgs e)
        {
            if (OnApply is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
    }
}
