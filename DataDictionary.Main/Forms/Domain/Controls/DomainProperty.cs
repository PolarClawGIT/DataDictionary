using DataDictionary.BusinessLayer.Application;
using DataDictionary.Main.Forms.Domain.ComboBoxList;
using System.ComponentModel;
using IPropertyValue = DataDictionary.BusinessLayer.Domain.IPropertyValue;

namespace DataDictionary.Main.Forms.Domain.Controls
{
    partial class DomainProperty : UserControl
    {
        //Func<IPropertyValue?> GetCurrent = () => { return null; };
        public String ApplyText { get { return applyCommand.Text; } set { applyCommand.Text = value; } }
        public Image? ApplyImage { get { return applyCommand.Image; } set { applyCommand.Image = value; } }

        [Browsable(false)]
        public Guid PropertyId
        {
            get
            {
                if (propertyTypeData.SelectedValue is Guid value)
                { return value; }
                else { return Guid.Empty; }
            }
            set { propertyTypeData.SelectedValue = value; }
        }

        [Browsable(false)]
        public String PropertyValue
        {
            get { return propertyValueData.Text; }
            set { propertyValueData.Text = value; }
        }

        [Browsable(false)]
        public String DefinitionText
        {
            get { return propertyDefinitionData.Rtf ?? propertyDefinitionData.Text; }
            set { propertyDefinitionData.Rtf = value; }
        }
        public Boolean ReadOnly
        {
            get { return !propertyLayout.Enabled; }
            set { propertyLayout.Enabled = !value; }
        }

        public DomainProperty()
        {
            InitializeComponent();
            PropertyNameMember.Load(propertyTypeData);
        }

        public void RefreshControls()
        {
            if (propertyTypeData.SelectedItem is PropertyNameMember selected
                && new PropertyIndex(selected) is PropertyIndex key
                && BusinessData.ApplicationData.Properties.FirstOrDefault(w => key.Equals(w)) is PropertyValue property)
            {
                // Setup the Choice check-boxes
                List<String> selectedChoices = new List<String>();
                if (!String.IsNullOrWhiteSpace(propertyValueData.Text))
                { selectedChoices.AddRange(propertyValueData.Text.Split(",")); }

                foreach (PropertyValue.ChoiceItem choice in property.Choices)
                {
                    String newItem = choice.Choice;
                    propertyChoiceData.Items.Add(newItem);
                    Int32 index = propertyChoiceData.Items.IndexOf(newItem);
                    Boolean isChecked = selectedChoices.Contains(newItem);
                    propertyChoiceData.SetItemChecked(index, isChecked);
                }

                // Enabled or Disable controls based on select item
                propertyTypeData.Enabled = true;
                propertyValueData.Enabled = (property.IsExtendedProperty == true || property.IsFrameworkSummary == true);
                propertyChoiceData.Enabled = (property.IsChoice == true);
                propertyDefinitionData.Enabled = (property.IsDefinition == true);
                applyCommand.Enabled = true;

                if (propertyValueData.Enabled) { propertyTabs.SelectedTab = propertyValueTab; }
                else if (propertyChoiceData.Enabled) { propertyTabs.SelectedTab = propertyChoiceTab; }
                else if (propertyDefinitionData.Enabled) { propertyTabs.SelectedTab = propertyDefinitionTab; }
            }
            else
            {
                propertyTypeData.Enabled = true;
                propertyValueData.Enabled = false;
                propertyChoiceData.Enabled = false;
                propertyDefinitionData.Enabled = false;
                applyCommand.Enabled = false;
            }
        }

        private void propertyTypeData_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cleanup not performed by Binding
            propertyChoiceData.Items.Clear();
            propertyValueData.Text = String.Empty;
            propertyDefinitionData.Text = String.Empty;

            RefreshControls();
        }

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

            propertyValueData.Text = String.Join(", ", values);
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
            else if (lastReturn > 0 && value.Length > maxCutOff) { value = value.Substring(0, lastReturn); }
            else if (lastSpace > 0 && value.Length > maxCutOff) { value = value.Substring(0, lastSpace); }
            else { value = value.Substring(0, maxCutOff); }

            value = value.Replace(Environment.NewLine, " ");
            value = value.Replace("\t", " ");
            value = value.Replace("\n", " ");

            propertyValueData.Text = value.Trim();
        }

        public event EventHandler? OnApply;
        private void ApplyCommand_Click(object sender, EventArgs e)
        {
            if (OnApply is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
    }
}
