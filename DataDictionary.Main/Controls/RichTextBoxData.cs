// Ignore Spelling: Rtf

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DataDictionary.Main.Controls
{
    /// <summary>
    /// Combination Control for a RichTextBox.
    /// </summary>
    /// <remarks>
    /// Wrappers the base control into a Table Layout with a Label and a spot to place to reference the Error Provider.
    /// Each property to be used from the base control has to be exposed. Same thing with events.
    /// </remarks>
    [DefaultBindingProperty("Rtf")]
    public partial class RichTextBoxData : UserControl, ISupportEditMenu
    {

        // Expose Header Properties
        public String HeaderText { get { return label.Text; } set { label.Text = value; } }

        // Override of default properties
        public new ControlBindingsCollection DataBindings { get { return richTextBox.DataBindings; } }
        public new String Text { get { return richTextBox.Text; } set { richTextBox.Text = value; } }

        // Expose Control Properties
        public Boolean ReadOnly { get { return richTextBox.ReadOnly; } set { richTextBox.ReadOnly = value; } }

        /// <summary>
        /// Exposes the Rich Text attribute.
        /// </summary>
        /// <remarks>
        /// The name appears to be very touchy. Changing it to "RichText" causes an error during binding while "Rtf" does not.
        /// </remarks>
        [Browsable(false), RefreshProperties(RefreshProperties.All), SettingsBindable(true), DefaultValue(""), Category("Appearance")]
        public String Rtf
        {
            get
            { return richTextBox.Rtf; }
            set
            {
                try
                { richTextBox.Rtf = value; }
                catch (Exception)
                { richTextBox.Text = value; }

            }
        }

        /// <summary>
        /// Control used to position the Error Provider Icon.
        /// </summary>
        /// <remarks>
        /// This is a panel in the upper right corner of the control.
        /// </remarks>
        [Browsable(false)]
        public Control ErrorControl { get { return errorLocation; } }

        public RichTextBoxData()
        { InitializeComponent(); }

        private void richTextBox_ReadOnlyChanged(object sender, EventArgs e)
        {
            toolStripBold.Enabled = !richTextBox.ReadOnly;
            toolStripItalic.Enabled = !richTextBox.ReadOnly;
            toolStripUnderline.Enabled = !richTextBox.ReadOnly;
            toolStripBulletList.Enabled = !richTextBox.ReadOnly;
            toolStripClearFormating.Enabled = !richTextBox.ReadOnly;
            toolStripCut.Enabled = !richTextBox.ReadOnly;
            toolStripCopy.Enabled = !richTextBox.ReadOnly;
            toolStripPaste.Enabled = !richTextBox.ReadOnly;
        }

        private void toolStripBold_Click(object sender, EventArgs e)
        {
            if (richTextBox.SelectionFont.Bold)
            { richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, FontStyle.Regular); }
            else { richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, FontStyle.Bold); }
        }

        private void toolStripItalic_Click(object sender, EventArgs e)
        {
            if (richTextBox.SelectionFont.Italic)
            { richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, FontStyle.Regular); }
            else { richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, FontStyle.Italic); }
        }

        private void toolStripUnderline_Click(object sender, EventArgs e)
        {
            if (richTextBox.SelectionFont.Underline)
            { richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, FontStyle.Regular); }
            else { richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, FontStyle.Underline); }
        }


        private void toolStripStrikeThrough_Click(object sender, EventArgs e)
        {
            if (richTextBox.SelectionFont.Strikeout)
            { richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, FontStyle.Regular); }
            else { richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, FontStyle.Strikeout); }
        }

        private void toolStripBulletList_Click(object sender, EventArgs e)
        {
            richTextBox.SelectionBullet = !richTextBox.SelectionBullet;
        }

        private void toolStripClearFormating_Click(object sender, EventArgs e)
        {
            // Stupid Cheat way of clearing selected text.
            using (RichTextBox temp = new RichTextBox())
            {
                temp.Text = richTextBox.SelectedText;
                temp.SelectAll();

                richTextBox.SelectedRtf = temp.SelectedRtf;
            }
        }

        public void Cut() { richTextBox.Cut(); }

        public void Copy() { richTextBox.Copy(); }

        public void Paste() { richTextBox.Paste(); }

        public void SelectAll() { richTextBox.SelectAll(); }

        public void Undo() { richTextBox.Undo(); }

        private void toolStripCut_Click(object sender, EventArgs e) { Cut(); }

        private void toolStripCopy_Click(object sender, EventArgs e) { Copy(); }

        private void toolStripPaste_Click(object sender, EventArgs e) { Paste(); }

        public new event EventHandler? Validated;
        private void richTextBox_Validated(object sender, EventArgs e)
        { if (Validated is EventHandler handler) { handler(sender, e); } }

        public new event CancelEventHandler? Validating;
        private void richTextBox_Validating(object sender, CancelEventArgs e)
        { if (Validating is CancelEventHandler handler) { handler(sender, e); } }
    }
}
