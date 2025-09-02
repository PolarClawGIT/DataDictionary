using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateNode : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return true; } // TODO: rig to current value

        public TemplateNode()
        {
            InitializeComponent();
        }
    }
}
