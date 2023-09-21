using DataDictionary.Main.Forms;
using RtfPipe;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.ProofOfConcept
{
    partial class TextEditor : ApplicationBase
    {

        class DataItem : INotifyPropertyChanged
        {
            String dataValue = String.Empty;
            public String DataValue
            {
                get { return dataValue; }
                set
                {
                    dataValue = value;
                    OnPropertyChanged(nameof(DataValue));
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;
            public virtual void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged is PropertyChangedEventHandler handler)
                { handler(this, new PropertyChangedEventArgs(propertyName)); }
            }
        }


        DataItem richText = new DataItem();
        DataItem richTextCode = new DataItem();
        DataItem plainText = new DataItem();
        DataItem htmlText = new DataItem();

        public TextEditor()
        {
            InitializeComponent();
            toolStrip.Hide(); // Hide base ToolStrip


            richText.PropertyChanged += RichTextControl_PropertyChanged;
        }

        private void TextEditor_Load(object sender, EventArgs e)
        {
            richTextBoxData.DataBindings.Add(new Binding(nameof(richTextBoxData.Rtf), richText, nameof(richText.DataValue), true, DataSourceUpdateMode.OnPropertyChanged));
            asRichTextCode.DataBindings.Add(new Binding(nameof(asRichTextCode.Text), richTextCode, nameof(richTextCode.DataValue)));
            asPlainText.DataBindings.Add(new Binding(nameof(asPlainText.Text), plainText, nameof(plainText.DataValue)));
            asHtml.DataBindings.Add(new Binding(nameof(asHtml.Text), htmlText, nameof(htmlText.DataValue)));
            //asRendederHtml.DataBindings.Add(new Binding(nameof(asRendederHtml.DocumentText), htmlText, nameof(htmlText.DataValue)));

        }

        private void RichTextControl_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            richTextCode.DataValue = richTextBoxData.Rtf;
            plainText.DataValue = richTextBoxData.Text;
            htmlText.DataValue = ToHtml(richTextBoxData.Rtf);

            htmlTextBoxData.DocumentText = htmlText.DataValue;

            //asRendederHtml.DocumentText = htmlText.DataValue;
        }

        String ToHtml(String text)
        {
            // Uses the RtfPipe tool. https://github.com/erdomke/RtfPipe
            return Rtf.ToHtml(text);
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }


    }
}
