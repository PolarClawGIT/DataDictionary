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

        public Label Label { get { return label; } }
        public TextBox TextBox { get { return textBox; } }

        public TextBoxData()
        { InitializeComponent(); }

        public void Cut() { textBox.Cut(); }

        public void Copy() { textBox.Copy(); }

        public void Paste() { textBox.Paste(); }

        public void SelectAll() { textBox.SelectAll(); }

        public void Undo() { textBox.Undo(); }
    }
}
