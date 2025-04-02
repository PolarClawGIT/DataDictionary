using DataDictionary.BusinessLayer.AppGeneral;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.General
{
    partial class HelpSubject
    {
        class ControlItem : HelpControlValue
        {
            public ListViewItem? ListItem { get; set; }

            public ControlItem(Control source) : base(source)
            { }
        }
    }
}
