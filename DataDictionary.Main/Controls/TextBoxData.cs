// Ignore Spelling: Multiline

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
    /// Combination Control for a TextBox.
    /// </summary>
    /// <remarks>
    /// Wrappers the base control into a Table Layout with a Label and a spot to place to reference the Error Provider.
    /// Each property to be used from the base control has to be exposed. Same thing with events.
    /// </remarks>
    partial class TextBoxData : UserControl, ISupportEditMenu
    {
        // Expose Header Properties
        public String HeaderText { get { return label.Text; } set { label.Text = value; } }

        // Override of default properties
        public new ControlBindingsCollection DataBindings { get { return textBox.DataBindings; } }
        public new String Text { get { return textBox.Text; } set { textBox.Text = value; } }

        // Expose Control Properties
        public Boolean ReadOnly { get { return textBox.ReadOnly; } set { textBox.ReadOnly = value; } }
        public Boolean Multiline { get { return textBox.Multiline; } set { textBox.Multiline = value; } }
        public Boolean WordWrap { get { return textBox.WordWrap; } set { textBox.WordWrap = value; } }

        /// <summary>
        /// Control used to position the Error Provider Icon.
        /// </summary>
        /// <remarks>
        /// This is a panel in the upper right corner of the control.
        /// </remarks>
        [Browsable(false)]
        public Control ErrorControl { get { return errorLocation; } }

        public TextBoxData()
        { InitializeComponent(); }


        public void Cut() { textBox.Cut(); }

        public void Copy() { textBox.Copy(); }

        public void Paste() { textBox.Paste(); }

        public void SelectAll() { textBox.SelectAll(); }

        public void Undo() { textBox.Undo(); }

        public new event EventHandler? Validated;
        private void textBox_Validated(object sender, EventArgs e)
        { if (Validated is EventHandler handler) { handler(sender, e); } }

        public new event CancelEventHandler? Validating;
        private void textBox_Validating(object sender, CancelEventArgs e)
        { if (Validating is CancelEventHandler handler) { handler(sender, e); } }
    }
}
