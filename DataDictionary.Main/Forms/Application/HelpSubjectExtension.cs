using DataDictionary.BusinessLayer.NameSpace;
using DataDictionary.DataLayer.ApplicationData.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Application
{
    static class HelpSubjectExtension
    {
        public static ModelNameSpaceKeyMember ToNameSpaceKey(this Control source)
        {
            if (source is Form && source.GetType().FullName is String fullName)
            { return new ModelNameSpaceKeyMember(fullName); }
            else if (GetRoot(source).GetType().FullName is String rootName)
            { return new ModelNameSpaceKeyMember(String.Format("{0}.{1}", rootName, source.Name)); }
            else { return new ModelNameSpaceKeyMember(source.Name); }

            Control GetRoot(Control item)
            {
                if (item is Form) { return item; }
                else if (item.Parent is null) { return item; }
                else { return GetRoot(item.Parent); }
            }
        }

        public static String ToToolTipText(this Control source)
        {
            ModelNameSpaceKeyMember key = source.ToNameSpaceKey();
            if (Program.Data.HelpSubjects.FirstOrDefault(w => key.Equals(new ModelNameSpaceKeyMember(w))) is HelpItem item && item.HelpToolTip is String toolTip)
            { return toolTip; }
            else { return String.Empty; }
        }

        public static void LoadToolTips(this ToolTip target, Control source)
        {
            if (source.ToToolTipText() is String value && !String.IsNullOrWhiteSpace(value))
            { target.SetToolTip(source, value); }

            foreach (Control child in source.Controls)
            { LoadToolTips(target, child); }
        }
    }
}
