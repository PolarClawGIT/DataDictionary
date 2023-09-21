using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataDictionary.Main.TextTemplates;
using DataDictionary.Main.Forms;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;

namespace DataDictionary.Main.Dialogs
{
    partial class ViewTextTemplate : ApplicationBase
    {
        class FormData // For Data Binding
        { public String? TextTemplateResult { get; set; } }

        FormData data = new FormData();

        public ViewTextTemplate() : base()
        {
            InitializeComponent();
        }

        private void ViewTextTemplate_Load(object sender, EventArgs e)
        {
            /* This functionally works.
             * Has extensibility issues. Not something I am going to try to resolve. 
             * Each template to be used would need to be pre-defined.
             * What I desired to do was put the templates in the database and be able to execute them in the application.
             * For now probably just use a simple Text Builder to generate scripts.
             */

            TextTemplates.CreateExtendedProperty template = new TextTemplates.CreateExtendedProperty(Program.Data);
            data.TextTemplateResult = template.TransformText();
            textTemplateResult.DataBindings.Add(new Binding(nameof(textTemplateResult.Text), data, nameof(data.TextTemplateResult)));

        }
    }
}
