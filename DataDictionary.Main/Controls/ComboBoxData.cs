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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public String HeaderText { get { return label.Text; } set { label.Text = value; } }

        // Override of default properties
        public new ControlBindingsCollection DataBindings { get { return comboBox.DataBindings; } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new String Text { get { return comboBox.Text; } set { comboBox.Text = value; } }

        // Expose Control Properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Boolean ReadOnly
        {
            get { return readOnly; }
            set
            {
                readOnly = value;
                comboBox.Enabled = !value;

                //Issue-
                //The base Combobox does not change appearance based on enabled & readonly options like a textbox
                //There is no easy solution.
                //The most direct option is to play with the margins and the background color.

                if (this.Enabled && !readOnly)
                { controlLayout.BackColor = SystemColors.ControlDarkDark; }
                else { controlLayout.BackColor = SystemColors.Control; }
            }
        }
        Boolean readOnly;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ComboBoxStyle DropDownStyle { get { return comboBox.DropDownStyle; } set { comboBox.DropDownStyle = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Object? DataSource { get { return comboBox.DataSource; } set { comboBox.DataSource = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Object? SelectedItem
        {
            get { return comboBox.SelectedItem; }
            set { comboBox.SelectedIndex = comboBox.Items.IndexOf(value); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Object? SelectedValue
        {
            get { return comboBox.SelectedValue; }
            set { comboBox.SelectedIndex = comboBox.Items.IndexOf(value); }
        }

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

        /// <inheritdoc cref="ComboBox.SelectionChangeCommitted"/>
        public event EventHandler? SelectionChangeCommitted
        {
            add { comboBox.SelectionChangeCommitted += value; }
            remove { comboBox.SelectionChangeCommitted -= value; }
        }

        /// <inheritdoc cref="ComboBox.SelectedIndexChanged"/>
        /// <remarks>Triggered by Binding</remarks>
        public event EventHandler? SelectedIndexChanged
        {
            add { comboBox.SelectedIndexChanged += value; }
            remove { comboBox.SelectedIndexChanged -= value; }
        }

        /// <summary>
        /// Event that fires as part of the control Validating event.
        /// If the Selected Item is null and text exists in the control this event fires.
        /// Upon completion, the Validating event is fired.
        /// </summary>
        /// <remarks>
        /// The real purpose of this event is to handle the scenario when the user is trying to add something to the list.
        /// This gives a chance to add something and select it before the control is validated.
        /// </remarks>
        public event EventHandler? TextUpdated;

        /// <inheritdoc cref="Control.Validating"/>
        public new event CancelEventHandler? Validating;
        private void comboBox_Validating(object sender, CancelEventArgs e)
        {
            if (comboBox.SelectedItem is null
                && !String.IsNullOrWhiteSpace(comboBox.Text)
                && TextUpdated is EventHandler textEvent)
            { textEvent(sender, new EventArgs()); }

            if (Validating is CancelEventHandler handler) { handler(sender, e); }
        }

        /// <inheritdoc cref="Control.Validated"/>
        public new event EventHandler? Validated
        {
            add { comboBox.Validated += value; }
            remove { comboBox.Validated -= value; }
        }

        private void comboBoxLayout_EnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled && !readOnly)
            { controlLayout.BackColor = SystemColors.ControlDarkDark; }
            else { controlLayout.BackColor = SystemColors.Control; }
        }
    }
}
