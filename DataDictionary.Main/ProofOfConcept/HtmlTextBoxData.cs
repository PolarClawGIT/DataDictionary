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
    partial class HtmlTextBoxData : UserControl, ISupportEditMenu
    {
        // Expose Header Properties
        public String HeaderText { get { return label.Text; } set { label.Text = value; } }

        public String DocumentText
        {
            get { return webBrowser.DocumentText; }
            set {
                String htmlValue = String.Format("<div id=\"editArea\" contenteditable=\"true\">{0}</div>", value);
                webBrowser.DocumentText = htmlValue; 
            }
        }

        public HtmlTextBoxData()
        {
            InitializeComponent();
        }

        //https://learn.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/platform-apis/ms533049(v=vs.85)?redirectedfrom=MSDN


#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public void Cut() { webBrowser.Document.ExecCommand("CUT", false, null); }


        public void Copy() { webBrowser.Document.ExecCommand("COPY", false, null); }

        public void Paste() { webBrowser.Document.ExecCommand("PASTE", false, null); }

        public void SelectAll() { }

        public void Undo() { webBrowser.Document.ExecCommand("UNDO", false, null); }

        private void toolStripBold_Click(object sender, EventArgs e)
        { webBrowser.Document.ExecCommand("BOLD", false, null); }

        private void toolStripItalic_Click(object sender, EventArgs e)

        { webBrowser.Document.ExecCommand("ITALIC", false, null); }


        private void toolStripUnderline_Click(object sender, EventArgs e)
        { webBrowser.Document.ExecCommand("UNDERLINE", false, null); }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        private void toolStripBulletList_Click(object sender, EventArgs e)
        {


        }

        private void toolStripStrikeThrough_Click(object sender, EventArgs e)
        { }

        private void toolStripClearFormating_Click(object sender, EventArgs e)
        { }

        private void toolStripCut_Click(object sender, EventArgs e)
        { Cut(); }

        private void toolStripCopy_Click(object sender, EventArgs e)
        { Copy(); }

        private void toolStripPaste_Click(object sender, EventArgs e)
        { Paste(); }
    }
}
