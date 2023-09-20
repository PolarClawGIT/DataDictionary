// Ignore Spelling: Indices

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.CheckedListBox;

namespace DataDictionary.Main.Controls
{
    /// <summary>
    /// Combination Control for Checked List Box
    /// </summary>
    /// <remarks>
    /// Wrappers the base control into a Table Layout with a Label and a spot to place to reference the Error Provider.
    /// Each property to be used from the base control has to be exposed. Same thing with events.
    /// </remarks>
    partial class CheckedListBoxData : UserControl
    {
        // Expose Header Properties
        public String HeaderText { get { return label.Text; } set { label.Text = value; } }

        // Override of default properties
        public new ControlBindingsCollection DataBindings { get { return checkedListBox.DataBindings; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new String Text { get { return checkedListBox.Text; } set { checkedListBox.Text = value; } }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Object? DataSource { get { return checkedListBox.DataSource; } set { checkedListBox.DataSource = value; } }

        public string DisplayMember { get { return checkedListBox.DisplayMember; } set { checkedListBox.DisplayMember = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Object? SelectedItem { get { return checkedListBox.SelectedItem; } set { checkedListBox.SelectedItem = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Object? SelectedValue { get { return checkedListBox.SelectedValue; } set { checkedListBox.SelectedValue = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Int32 SelectedIndex { get { return checkedListBox.SelectedIndex; } set { checkedListBox.SelectedIndex = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CheckedIndexCollection CheckedIndices { get { return checkedListBox.CheckedIndices; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CheckedItemCollection CheckedItems { get { return checkedListBox.CheckedItems; } }

        /// <inheritdoc cref="CheckedListBox.CheckOnClick"/>
        public bool CheckOnClick { get { return checkedListBox.CheckOnClick; } set { checkedListBox.CheckOnClick = value; } }

        public ObjectCollection Items { get { return checkedListBox.Items; } }

        /// <summary>
        /// Control used to position the Error Provider Icon.
        /// </summary>
        /// <remarks>
        /// This is a panel in the upper right corner of the control.
        /// </remarks>
        [Browsable(false)]
        public Control ErrorControl { get { return errorLocation; } }

        public CheckedListBoxData()
        { InitializeComponent(); }


        public void ClearSelected() { checkedListBox.ClearSelected(); }

        /// <inheritdoc cref="CheckedListBox.SetItemChecked"/>
        public void SetItemChecked(int index, bool value)
        { checkedListBox.SetItemChecked(index, value); }

        public new event EventHandler? Validated;
        private void checkedListBox_Validated(object sender, EventArgs e)
        { if (Validated is EventHandler handler) { handler(sender, e); } }

        public new event CancelEventHandler? Validating;
        private void checkedListBox_Validating(object sender, CancelEventArgs e)
        { if (Validating is CancelEventHandler handler) { handler(sender, e); } }

        public event EventHandler? SelectedIndexChanged;
        private void checkedListBox_SelectedIndexChanged(object sender, EventArgs e)
        { if (SelectedIndexChanged is EventHandler handler) { handler(sender, e); } }

        public event ItemCheckEventHandler? ItemCheck;
        private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        { if (ItemCheck is ItemCheckEventHandler handler) { handler(sender, e); } }

        private void CheckedListBoxData_EnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled)
            { checkedListBox.ResetBackColor(); }
            else { checkedListBox.BackColor = SystemColors.Control; }

            checkedListBox.Enabled = this.Enabled;
        }
    }
}
