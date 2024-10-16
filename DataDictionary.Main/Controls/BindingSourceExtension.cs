using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Controls
{
    static class BindingSourceExtension
    {
        public static void BindComplete(this BindingSource binding,  object sender, BindingCompleteEventArgs e)
        { // This is to help detecting binding errors and provide something meaningful.
            Control? rootUserControl = null; // If this is a User Control, what is the control.

            if (e.Binding is not null)
            { rootUserControl = e.Binding.Control.FindUserControl(); }

            if (e.Exception is not null)
            {
                if (e.Binding is not null)
                {
                    e.Exception.Data.Add(nameof(e.Binding.Control), e.Binding.Control.GetType().Name);
                    if (rootUserControl is not null) { e.Exception.Data.Add(nameof(rootUserControl), rootUserControl.Name); }
                }

                Program.ShowException(e.Exception);
            }
        }

    }
}
