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

namespace DataDictionary.Main.Controls
{
    public partial class RichTextBoxData : UserControl, ISupportEditMenu
    {
        public String HeaderText { get { return label.Text; } set { label.Text = value; } }
        public Boolean ReadOnly { get { return richTextBox.ReadOnly; } set { richTextBox.ReadOnly = value; } }

        public Label Label { get { return label; } }
        public RichTextBox TextBox { get { return richTextBox; } }

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

    }
}
