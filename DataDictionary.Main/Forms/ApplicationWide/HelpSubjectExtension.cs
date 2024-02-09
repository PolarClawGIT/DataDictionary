using DataDictionary.BusinessLayer.NameSpace;
using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.DataLayer.ApplicationData.Help;
using DataDictionary.Main.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.ApplicationWide
{
    static class HelpSubjectExtension
    {

        /// <summary>
        /// Looks up the Tool Tip data from the HelpData and returns the value.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <remarks>
        /// Important: The name in the HelpData and the Name in the control must match exactly (C# String.Equels compare).
        /// Renames of the control does not update the HelpData.
        /// </remarks>
        public static String ToToolTipText(this Control source)
        {
            NameSpaceKey key = source.ToNameSpaceKey();
            if (Program.Data.HelpSubjects.FirstOrDefault(w => key.Equals(new NameSpaceKey(w))) is HelpItem item
                && item.HelpToolTip is String toolTip)
            { return toolTip; }
            else { return String.Empty; }
        }

        /// <summary>
        /// Reads the HelpData and loads the Tool Tip control with the values.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <remarks>
        /// This can load tool tips for any control in the Controls structure.
        /// That includes controls nested inside User Controls.
        /// This works because the Form.Controls structure includes all controls that appear in the form (built in and User Controls).
        /// Not all control types display Tool Tips. The key is that they are the top-most visible control.
        /// Otherwise, the hidden control does not receive the event.
        /// </remarks>
        public static void LoadToolTips(this ToolTip target, Control source)
        {
            if (source.ToToolTipText() is String value && !String.IsNullOrWhiteSpace(value))
            { target.SetToolTip(source, value); }

            foreach (Control child in source.Controls)
            { LoadToolTips(target, child); }
        }
    }
}
