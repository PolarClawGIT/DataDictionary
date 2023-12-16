using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Controls
{
    /// <summary>
    /// Combination Control for a ComboBox.
    /// </summary>
    /// <remarks>
    /// Wrappers the base control into a Table Layout with a Label and a spot to place to reference the Error Provider.
    /// Each property to be used from the base control has to be exposed. Same thing with events.
    /// </remarks>
    partial class ComboBoxData : UserControl, ISupportEditMenu
    {
        // Expose Header Properties
        public String HeaderText { get { return label.Text; } set { label.Text = value; } }

        // Override of default properties
        public new ControlBindingsCollection DataBindings { get { return comboBox.DataBindings; } }
        public new String Text { get { return comboBox.Text; } set { comboBox.Text = value; } }

        // Expose Control Properties
        public Boolean ReadOnly { get { return !comboBox.Enabled; } set { comboBox.Enabled = !value; } }
        public ComboBoxStyle DropDownStyle { get { return comboBox.DropDownStyle; } set { comboBox.DropDownStyle = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Object DataSource { get { return comboBox.DataSource; } set { comboBox.DataSource = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Object SelectedItem { get { return comboBox.SelectedItem; } set { comboBox.SelectedItem = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Object? SelectedValue { get { return comboBox.SelectedValue; } set { comboBox.SelectedValue = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Int32 SelectedIndex { get { return comboBox.SelectedIndex; } set { comboBox.SelectedIndex = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ComboBox.ObjectCollection Items { get { return comboBox.Items; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public String ValueMember { get { return comboBox.ValueMember; } set { comboBox.ValueMember = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public String DisplayMember { get { return comboBox.DisplayMember; } set { comboBox.DisplayMember = value; } }

        /// <summary>
        /// Control used to position the Error Provider Icon.
        /// </summary>
        /// <remarks>
        /// This is a panel in the upper right corner of the control.
        /// </remarks>
        [Browsable(false)]
        public Control ErrorControl { get { return errorLocation; } }

        public ComboBoxData()
        { InitializeComponent(); }

        public void Cut()
        {
            Clipboard.SetText(comboBox.SelectedText);
            comboBox.SelectedText = String.Empty;
        }

        public void Copy()
        { Clipboard.SetText(comboBox.SelectedText); }

        public void Paste()
        {
            if (!String.IsNullOrWhiteSpace(Clipboard.GetText()))
            { comboBox.SelectedText = Clipboard.GetText(); }
        }

        public void SelectAll()
        { comboBox.SelectAll(); }

        public void Undo() { }

        public event EventHandler? SelectedIndexChanged;
        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        { if (SelectedIndexChanged is EventHandler handler) { handler(sender, e); } }

        public new event EventHandler? Validated;
        private void comboBox_Validated(object sender, EventArgs e)
        { if (Validated is EventHandler handler) { handler(sender, e); } }

        public new event CancelEventHandler? Validating;
        private void comboBox_Validating(object sender, CancelEventArgs e)
        { if (Validating is CancelEventHandler handler) { handler(sender, e); } }
    }
}
