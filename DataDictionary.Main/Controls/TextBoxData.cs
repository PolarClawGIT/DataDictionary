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
    public partial class TextBoxData : UserControl, ISupportEditMenu
    {

        public String HeaderText { get { return label.Text; } set { label.Text = value; } }
        public Boolean ReadOnly { get { return textBox.ReadOnly; } set { textBox.ReadOnly = value; } }

        public new ControlBindingsCollection DataBindings { get { return textBox.DataBindings; } }
        public new String Text { get { return textBox.Text; } set { textBox.Text = value; } }
        public Boolean Multiline { get { return textBox.Multiline; } set { textBox.Multiline = value; } }

        /// <summary>
        /// Control used to position the Error Provider Icon.
        /// </summary>
        /// <remarks>
        /// This is a panel in the upper right corner of the control.
        /// </remarks>
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
