using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Controls
{
    static class ControlExtension
    {
        /// <summary>
        /// Finds the user control the current control is part of.
        /// </summary>
        /// <param name="source"></param>
        /// <returns>null = no UserControl was found. Otherwise, the UserControl that this control is part of.</returns>
        /// <remarks>
        /// This is intended to help with user controls where the sub-control is the one causing an event to fire.
        /// This way, I can find out what the control is on the form that I am working with.
        /// </remarks>
        public static Control? FindUserControl(this Control? source)
        {
            if (source is null) { return null; }
            else if (source is UserControl user) { return user; }
            else if (source.Parent is Control parent) { return FindUserControl(parent); }
            else { return null; }
        }

        /// <summary>
        /// Walks up the Parent Controls to work out the Name of the control.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static String ToFullControlName(this Control source)
        {
            String result = String.Empty;
            Control? current = source;

            if (source is Form && source.GetType().FullName is String formType)
            { result = formType; current = null; }
            else { result = source.Name; }

            while (current is not null)
            {
                if (!String.IsNullOrWhiteSpace(current.Name))
                {
                    if (current is UserControl)
                    { result = String.Format("{0}.{1}", current.Name, result); }
                    else if (current is Form && current.GetType().FullName is String fullName)
                    { result = String.Format("{0}.{1}", fullName, result); }
                }

                if (current is Form)
                { current = null; }
                else { current = current.Parent; }
            }

            return result;
        }

        /// <summary>
        /// Flattens Control collections into a simple List
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IReadOnlyList<Control> ToControlList(this Control source)
        {
            List<Control> result = new List<Control>();

            if (!source.HasChildren && !String.IsNullOrWhiteSpace(source.Name))
            { result.Add(source); }

            foreach (Control child in source.Controls)
            { result.AddRange(ToControlList(child)); }

            return result;
        }
    }
}
