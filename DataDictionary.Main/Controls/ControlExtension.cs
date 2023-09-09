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
    }
}
