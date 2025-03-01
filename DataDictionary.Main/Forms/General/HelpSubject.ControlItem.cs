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
        class ControlItem
        {
            public ListViewItem? ListItem { get; set; }
            public String ControlType { get; private set; }
            public HelpSubjectIndexPath ControlName { get; private set; }
            public Boolean IsForm { get; private set; }

            public ControlItem(Control source)
            {
                ControlName = source.ToNameSpaceKey();

                if (source is Form)
                {
                    if (source.GetType().BaseType is Type baseType)
                    { ControlType = baseType.Name; }
                    else { ControlType = source.GetType().Name; }

                    IsForm = true;
                }
                else
                {
                    Control root = source;
                    while (root is not Form && root.Parent is not null)
                    { root = root.Parent; }

                    ControlType = source.GetType().Name;
                    IsForm = false;
                }
            }

            public override string ToString()
            { return ControlName.MemberFullPath; }
           
        }
    }
}
