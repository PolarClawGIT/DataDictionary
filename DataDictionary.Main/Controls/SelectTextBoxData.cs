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
    partial class SelectTextBoxData : UserControl, ISupportEditMenu
    {
        // Expose Header Properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public String HeaderText { get { return label.Text; } set { label.Text = value; } }

        // Override of default properties
        public new ControlBindingsCollection DataBindings { get { return textBox.DataBindings; } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new String Text { get { return textBox.Text; } set { textBox.Text = value; } }

        // Expose Control Properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Boolean ReadOnly { get { return textBox.ReadOnly; } set { textBox.ReadOnly = value; selectCommand.Enabled = !value; } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image? SelectIcon { get { return selectCommand.Image; } set { selectCommand.Image = value; } }

        /// <summary>
        /// Control used to position the Error Provider Icon.
        /// </summary>
        /// <remarks>
        /// This is a panel in the upper right corner of the control.
        /// </remarks>
        [Browsable(false)]
        public Control ErrorControl { get { return errorLocation; } }

        public SelectTextBoxData()
        { InitializeComponent(); }

        public void Cut() { textBox.Cut(); }

        public void Copy() { textBox.Copy(); }

        public void Paste() { textBox.Paste(); }

        public void SelectAll() { textBox.SelectAll(); }

        public void Undo() { textBox.Undo(); }

        public new event EventHandler? Validated;
        private void TextBox_Validated(object sender, EventArgs e)
        { if (Validated is EventHandler handler) { handler(sender, e); } }

        public new event CancelEventHandler? Validating;
        private void TextBox_Validating(object sender, CancelEventArgs e)
        { if (Validating is CancelEventHandler handler) { handler(sender, e); } }

        public event EventHandler? SelectCommand;
        private void SelectCommand_Click(object sender, EventArgs e)
        { if (SelectCommand is EventHandler handler) { handler(sender, e); } }
    }
}
