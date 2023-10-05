using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Controls
{
    static class ErrorProviderExtension
    {
        /// <summary>
        /// Searches the control passed and all child controls for an error associated with the ErrorProvider and return the text.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="rootControl"></param>
        /// <returns>List of controls and the error text that goes with them.</returns>
        /// <remarks>
        /// The error provider does not contain this function.
        /// Also, for some reason the Error Provider can return HasErrors = true when no control on the form has an error.
        /// This way, I can search for controls within a specific scope looking for errors.
        /// </remarks>
        public static Dictionary<Control, string> GetAllErrors(this ErrorProvider provider, Control rootControl)
        {
            Dictionary<Control, string> errors = new Dictionary<Control, string>();
            string errorText = provider.GetError(rootControl);

            if (!string.IsNullOrWhiteSpace(errorText))
            { errors.Add(rootControl, errorText); }

            foreach (Control item in rootControl.Controls)
            {
                Dictionary<Control, string> child = provider.GetAllErrors(item);
                foreach (var childItem in child)
                { errors.Add(childItem.Key, childItem.Value); }
            }

            return errors;
        }
    }
}
