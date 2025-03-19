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
    [DefaultBindingProperty("RichText")]
    partial class RichTextBoxData : UserControl, ISupportEditMenu, INotifyPropertyChanged
    {

        /// <summary>
        /// Gets/Sets the Text for the Header.
        /// </summary>
        public String HeaderText { get { return label.Text; } set { label.Text = value; } }

        // Override of default properties
        public new ControlBindingsCollection DataBindings { get { return richTextBox.DataBindings; } }
        public new String Text { get { return richTextBox.Text; } set { richTextBox.Text = value; } }

        /// <summary>
        /// Makes the control ReadOnly or Read/Write. Changes the color of the control.
        /// </summary>
        public Boolean ReadOnly { get { return richTextBox.ReadOnly; } set { richTextBox.ReadOnly = value; } }

        /// <summary>
        /// Makes the Header Visible or hidden
        /// </summary>
        public Boolean HeaderVisible { get { return label.Visible; } set { label.Visible = value; } }

        /// <summary>
        /// Makes the Tool Strip Visible or hidden
        /// </summary>
        public Boolean ToolStripVisible { get { return toolStrip.Visible; } set { toolStrip.Visible = value; } }

        /// <summary>
        /// Exposes the Rich Text attribute.
        /// </summary>
        /// <remarks>
        /// ISSUE: Binding does not actual connect to this property.
        /// It connects to the RTF of the RichTextBox control directly.
        /// As such, the logic in the property is never called.
        /// This causes problems with Threading and cleaning the value before it is used.
        /// The root RTF property can also throw errors if the text is
        /// not Rich Text.
        /// </remarks>
        [Browsable(false), DefaultValue(""), Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public String? Rtf
        {
            get { return RichText; }
            set
            {
                RichText = value;

                if (PropertyChanged is PropertyChangedEventHandler handler)
                { handler(this, new PropertyChangedEventArgs(nameof(Rtf))); }

                if (RtfChanged is EventHandler eventHandler)
                { eventHandler(this, new EventArgs()); }

                // This is never called with Binding. It may be called in the designer.
                throw new InvalidOperationException("Debug: This does not occur.");
            }
        }
        // Based on: https://learn.microsoft.com/en-us/dotnet/desktop/winforms/change-notification-in-windows-forms-data-binding?view=netframeworkdesktop-4.8&redirectedfrom=MSDN
        // This does not work either.
        public event EventHandler? RtfChanged;
        public event EventHandler? RichTextChanged;


        [Browsable(false), DefaultValue(""), Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public String? RichText
        {   // This is the intended Property for Data Binding.
            // It does not work.
            // A runtime Invalid Augment exception occurs when creating the Binding.
            get
            { return richTextBox.Rtf; }
            set
            {
                try
                {
                    if (this.IsHandleCreated)
                    { Invoke(() => { richTextBox.Clear(); richTextBox.Rtf = value; }); }
                    else { richTextBox.Clear(); richTextBox.Rtf = value; }

                    if (PropertyChanged is PropertyChangedEventHandler handler)
                    { handler(this, new PropertyChangedEventArgs(nameof(RichText))); }

                    if (RichTextChanged is EventHandler eventHandler)
                    { eventHandler(this, new EventArgs()); }
                }
                catch (Exception)
                {
                    if (this.IsHandleCreated)
                    { Invoke(() => { richTextBox.Clear(); richTextBox.Text = value; }); }
                    else { richTextBox.Clear(); richTextBox.Text = value; }
                }


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

        //public EventHandler TextChanged;

        public RichTextBoxData()
        {
            InitializeComponent();
            this.TextChanged += RichTextBoxData_TextChanged;
        }

        private void RichTextBoxData_TextChanged(Object? sender, EventArgs e)
        {   // Nope, does not bind to the Text property of the User Control either.
            var x = this.Text;
            //throw new NotImplementedException();
        }

        private void RichTextBox_ReadOnlyChanged(object sender, EventArgs e)
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
            if (richTextBox.SelectionFont is Font value)
            {
                if (value.Bold)
                { richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, FontStyle.Regular); }
                else { richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, FontStyle.Bold); }
            }
        }

        private void ToolStripItalic_Click(object sender, EventArgs e)
        {
            if (richTextBox.SelectionFont is Font value)
            {
                if (value.Italic)
                { richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, FontStyle.Regular); }
                else { richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, FontStyle.Italic); }
            }
        }

        private void ToolStripUnderline_Click(object sender, EventArgs e)
        {
            if (richTextBox.SelectionFont is Font value)
            {
                if (value.Underline)
                { richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, FontStyle.Regular); }
                else { richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, FontStyle.Underline); }
            }
        }

        private void ToolStripStrikeThrough_Click(object sender, EventArgs e)
        {
            if (richTextBox.SelectionFont is Font value)
            {
                if (value.Strikeout)
                { richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, FontStyle.Regular); }
                else { richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, FontStyle.Strikeout); }
            }
        }

        private void ToolStripBulletList_Click(object sender, EventArgs e)
        {
            richTextBox.SelectionBullet = !richTextBox.SelectionBullet;
        }

        private void ToolStripClearFormating_Click(object sender, EventArgs e)
        {
            // Stupid Cheat way of clearing selected text.
            using (RichTextBox temp = new RichTextBox())
            {
                temp.Text = richTextBox.SelectedText;
                temp.SelectAll();

                richTextBox.SelectedRtf = temp.SelectedRtf;
            }
        }

        public void AddTools(ToolStrip tools)
        { ToolStripManager.Merge(tools, toolStrip); }

        public void Cut() { richTextBox.Cut(); }

        public void Copy() { richTextBox.Copy(); }

        public void Paste() { richTextBox.Paste(); }

        public void SelectAll() { richTextBox.SelectAll(); }

        public void Undo() { richTextBox.Undo(); }

        private void ToolStripCut_Click(object sender, EventArgs e) { Cut(); }

        private void ToolStripCopy_Click(object sender, EventArgs e) { Copy(); }

        private void ToolStripPaste_Click(object sender, EventArgs e) { Paste(); }

        public new event EventHandler? Validated;
        private void richTextBox_Validated(object sender, EventArgs e)
        { if (Validated is EventHandler handler) { handler(sender, e); } }

        public new event CancelEventHandler? Validating;
        public event PropertyChangedEventHandler? PropertyChanged;

        private void richTextBox_Validating(object sender, CancelEventArgs e)
        { if (Validating is CancelEventHandler handler) { handler(sender, e); } }

    }
}
