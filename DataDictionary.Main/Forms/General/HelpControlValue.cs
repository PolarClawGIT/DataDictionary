using DataDictionary.BusinessLayer.AppGeneral;
using DataDictionary.Main.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.General
{
    class HelpControlValue
    {
        public String ControlType { get; protected set; }
        public HelpSubjectIndexPath Path { get; protected set; }
        public String ControlName { get { return Path.Member; } }
        public Boolean IsForm { get; protected set; }

        public HelpControlValue(Control source) 
        {
            Path = source.ToHelpSubjectPath();

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
        { return Path.MemberFullPath; }
    }

    static class HelpControlExtension
    {
        public static void Load<THelpControl>(this IList<THelpControl> target, Form source, Func<Control, THelpControl> constructor)
            where THelpControl: HelpControlValue
        {
            List<Control> values = source.ToControlList()
                .Where(w => !String.IsNullOrWhiteSpace(w.Name)
                            && w is not Form
                            && !(w is Panel or ToolStrip or MenuStrip or SplitContainer or Splitter))
                .OrderBy(o => o is not Form)
                .ThenBy(o => o.ToHelpSubjectPath())
                .ToList();

            String controlType;
            if (source.GetType().BaseType is Type baseType)
            { controlType = baseType.Name; }
            else { controlType = source.GetType().Name; }

            THelpControl baseForm = constructor(source);
            target.Add(baseForm);

            foreach (Control item in values)
            {
                THelpControl newControl = constructor(item);
                target.Add(newControl);
            }
        }
    }
}
