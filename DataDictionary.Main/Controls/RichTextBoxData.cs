// Ignore Spelling: Rtf

using System.ComponentModel;

namespace DataDictionary.Main.Controls
{
    /// <summary>
    /// Combination Control for a RichTextBox.
    /// </summary>
    /// <remarks>
    /// Wrappers the base control into a Table Layout with a Label and a spot to place to reference the Error Provider.
    /// </remarks>
    [DefaultBindingProperty("RichText")]
    partial class RichTextBoxData : UserControl, ISupportEditMenu
    {
        // This control uses two-way binding using the base RichText control as the base.
        // Additional handling on the Rtf property of the RichText control is needed.
        // As such, pass-threw binding does not work.
        // Pass-threw binding overrides the DataBindings of the User control and points it to the base control.
        // 
        // This control needs full two way binding.
        // To get this to work, several things need to occur.
        //
        // * Add the property for Data Binding. This wrappers the base controls property.
        // * Add the attribute to the property:  [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        // * Add the change event named based on the property suffixed with "Changed".
        // * Set the Default Biding property for the control: [DefaultBindingProperty("RichText")]
        // * Because this is wrapped, the change event needs to fire on the base change event (not the property changed).
        // 
        // The Get is called when the change event for the property is triggered.
        // This is triggered by the change event of the wrapped property. 
        // The value returned by the Get is sent to the data object.
        //
        // The Set is called when the notify property change event occurs on
        // the data object (possibly multiple times).
        // The added code is fired and the value is passed to the wrapped controls property.


        /// <summary>
        /// Gets/Sets the Text for the Header.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public String HeaderText { get { return label.Text; } set { label.Text = value; } }

        // Override of default properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new String Text { get { return richTextBox.Text; } set { richTextBox.Text = value; } }

        /// <summary>
        /// Makes the control ReadOnly or Read/Write. Changes the color of the control.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Boolean ReadOnly { get { return richTextBox.ReadOnly; } set { richTextBox.ReadOnly = value; } }

        /// <summary>
        /// Makes the Header Visible or hidden
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Boolean HeaderVisible { get { return label.Visible; } set { label.Visible = value; } }

        /// <summary>
        /// Makes the Tool Strip Visible or hidden
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Boolean ToolStripVisible { get { return toolStrip.Visible; } set { toolStrip.Visible = value; } }

        /// <summary>
        /// Exposes the Rich Text attribute.
        /// </summary>
        /// <remarks>
        /// Additional handling to deal with values that are plain text.
        /// </remarks>
        [Browsable(false), DefaultValue(""), Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public String? RichText
        {
            get
            {
                if (String.IsNullOrWhiteSpace(richTextBox.Text))
                { return String.Empty; }
                else { return richTextBox.Rtf; }
            }
            set
            {
                try
                {
                    // If the value is not RTF, setting the Rtf property throws an exception.
                    if (this.IsHandleCreated)
                    { Invoke(() => { richTextBox.Rtf = value; }); }
                    else { richTextBox.Rtf = value; }
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

        public RichTextBoxData()
        {
            InitializeComponent();
            richTextBox.TextChanged += RichTextBox_TextChanged;
        }

        // Based on: https://learn.microsoft.com/en-us/dotnet/desktop/winforms/change-notification-in-windows-forms-data-binding?view=netframeworkdesktop-4.8&redirectedfrom=MSDN
        // This causes the post-back to the data and must be named after the property.
        // Needed for two-way binding.
        public event EventHandler? RichTextChanged;

        private void RichTextBox_TextChanged(Object? sender, EventArgs e)
        {
            // Needed for two-way binding.
            // Because this is a wrapped control,
            // the post back need to be triggered when the base control changes.
            if (RichTextChanged is EventHandler rtfHandler)
            { rtfHandler(this, new EventArgs()); }

            //Text changed of the UserControl changed because it was overridden.
            OnTextChanged(new EventArgs()); 
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

        private void ToolStripBold_Click(object sender, EventArgs e)
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

        private void richTextBox_Validating(object sender, CancelEventArgs e)
        { if (Validating is CancelEventHandler handler) { handler(sender, e); } }

    }
}
