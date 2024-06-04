using DataDictionary.BusinessLayer.Domain;
using DataDictionary.DataLayer.DomainData.Property;
using DataDictionary.Main.Forms.Domain.ComboBoxList;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Domain.Controls
{
    partial class DomainProperty : UserControl
    {
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
                && BusinessData.DomainModel.Properties.FirstOrDefault(w => key.Equals(w)) is PropertyValue property)
            {
                // Setup the Choice check-boxes
                List<String> selectedChoices = new List<String>();
                if (!String.IsNullOrWhiteSpace(propertyValueData.Text))
                { selectedChoices.AddRange(propertyValueData.Text.Split(",")); }

                foreach (String value in property.Choices)
                {
                    propertyChoiceData.Items.Add(value);
                    Int32 index = propertyChoiceData.Items.IndexOf(value);
                    Boolean isChecked = selectedChoices.Contains(value);
                    propertyChoiceData.SetItemChecked(index, isChecked);
                }

                // Enabled or Disable controls based on select item
                propertyTypeData.Enabled = true;
                propertyValueData.Enabled = !(property.PropertyType is DomainPropertyType.List);
                propertyChoiceData.Enabled = (property.PropertyType is DomainPropertyType.List);
                applyCommand.Enabled = true;

                if (propertyValueData.Enabled) { propertyTabs.SelectedTab = propertyValueTab; }
                else if (propertyChoiceData.Enabled) { propertyTabs.SelectedTab = propertyChoiceTab; }
            }
            else
            {
                propertyTypeData.Enabled = true;
                propertyValueData.Enabled = false;
                propertyChoiceData.Enabled = false;
                applyCommand.Enabled = false;
            }
        }

        private void propertyTypeData_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cleanup not performed by Binding
            propertyChoiceData.Items.Clear();
            propertyValueData.Text = String.Empty;

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

        public event EventHandler? OnApply;
        private void ApplyCommand_Click(object sender, EventArgs e)
        {
            if (OnApply is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
    }
}
