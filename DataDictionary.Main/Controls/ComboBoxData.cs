﻿using System;
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
    public partial class ComboBoxData : UserControl, ISupportEditMenu
    {
        public String HeaderText { get { return label.Text; } set { label.Text = value; } }
        public Boolean ReadOnly { get { return !comboBox.Enabled; } set { comboBox.Enabled = !value; } }

        public new ControlBindingsCollection DataBindings { get { return comboBox.DataBindings; } }
        public new String Text { get { return comboBox.Text; } set { comboBox.Text = value; } }
        public Object DataSource { get { return comboBox.DataSource; } set { comboBox.DataSource = value; } }
        public Object SelectedItem { get { return comboBox.SelectedItem; } set { comboBox.SelectedItem = value; } }
        public Object? SelectedValue { get { return comboBox.SelectedValue; } set { comboBox.SelectedValue = value; } }
        public Int32 SelectedIndex { get { return comboBox.SelectedIndex; } set { comboBox.SelectedIndex = value; } }
        public ComboBox.ObjectCollection Items { get { return comboBox.Items; } }


        public ComboBoxData()
        { InitializeComponent(); }

        public void Cut()
        {
            Clipboard.SetText(comboBox.SelectedText);
            comboBox.SelectedText = String.Empty;
        }

        public void Copy()
        { Clipboard.SetText(comboBox.SelectedText); }

        public void Paste()
        {
            if (!String.IsNullOrWhiteSpace(Clipboard.GetText()))
            { comboBox.SelectedText = Clipboard.GetText(); }
        }

        public void SelectAll()
        { comboBox.SelectAll(); }

        public void Undo() { }

        public event EventHandler? SelectedIndexChanged;
        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        { if (SelectedIndexChanged is EventHandler handler) { handler(sender, e); } }

        public new event EventHandler? Validated;
        private void comboBox_Validated(object sender, EventArgs e)
        { if (Validated is EventHandler handler) { handler(sender, e); } }

        public new event CancelEventHandler? Validating;
        private void comboBox_Validating(object sender, CancelEventArgs e)
        { if (Validating is CancelEventHandler handler) { handler(sender, e); } }
    }
}
