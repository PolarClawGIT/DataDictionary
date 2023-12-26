using DataDictionary.DataLayer;
using DataDictionary.Main.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.Control;

namespace DataDictionary.Main.Forms
{
    /// <summary>
    /// Base Interface for Application Data Forms.
    /// </summary>
    /// <remarks>This is a partial class that is intended to work with the ApplicationBase class.</remarks>
    interface IApplicationDataForm
    {
        /// <summary>
        /// Is the object passed the item the form is using for data.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <remarks>
        /// By default the function always returns true.
        /// Override this if the from is specific to data item.
        /// </remarks>
        Boolean IsOpenItem(Object? item) { return true; }

        /// <summary>
        ///  Collection of child controls.
        /// </summary>
        /// <remarks>Implemented by the Control class</remarks>
        ControlCollection Controls { get; }
    }

    /// <summary>
    /// Contains the Binding Methods
    /// </summary>
    interface IApplicationDataBind : IApplicationDataForm
    {
        /// <summary>
        /// Perform the Binding of the Data for the form. Called by BindData.
        /// </summary>
        /// <returns>True if the binding was successful.</returns>
        public Boolean BindDataCore();

        /// <summary>
        /// Performs the Unbinding of the Data for the Form. Called by UnbindData.
        /// </summary>
        public void UnbindDataCore();


    }

    /// <summary>
    /// Implementation of IApplicationDataBind.
    /// </summary>
    /// <remarks>
    /// This is only because calling interface methods result in code that looks like: (this as IApplicationDataBind).BindData();
    /// </remarks>
    static class ApplicationDataBind
    {
        /// <summary>
        /// Calls BindDataCore() and locks or unlocks the form accordingly.
        /// </summary>
        public static void BindData(this IApplicationDataBind dataForm)
        {
            if (dataForm.BindDataCore()) { dataForm.IsLocked(false); dataForm.IsWaitCursor(false); }
            else
            {
                dataForm.IsLocked(true);
                dataForm.IsWaitCursor(false);
            }
        }


        /// <summary>
        /// Class UnbindDataCore and locks the form.
        /// </summary>
        public static void UnbindData(this IApplicationDataBind dataForm)
        {
            dataForm.IsLocked(true);
            dataForm.IsWaitCursor(true);
            dataForm.UnbindDataCore();
        }

        /// <summary>
        /// Locks (disable) and Unlock (enable) the Form.
        /// </summary>
        /// <param name="dataForm"></param>
        /// <param name="newState">Sets the new Value. If Null, the value is not set, only returned.</param>
        /// <returns>
        /// True disables the top most controls.
        /// False enables the top most controls.
        /// </returns>
        public static Boolean IsLocked(this IApplicationDataForm dataForm, Boolean? newState = null)
        {
            if (newState is Boolean value)
            {
                foreach (Control item in dataForm.Controls)
                {
                    if (item is MdiClient) { } // Don't touch the MdiClient control as it will cause child forms to be disabled.
                    else { item.Enabled = !value; }
                }
            }

            return dataForm.Controls.Cast<Control>().Any(w => w.Enabled);
        }

        /// <summary>
        /// Controls the UseWaitCursor of the top most controls.
        /// </summary>
        /// <param name="dataForm"></param>
        /// <param name="newState"></param>
        /// <returns></returns>
        public static Boolean IsWaitCursor(this IApplicationDataForm dataForm, Boolean? newState = null)
        {
            if (newState is Boolean value)
            {
                foreach (Control item in dataForm.Controls)
                {
                    if (item is MdiClient) { } // Don't touch the MdiClient control as it will cause child forms to be disabled.
                    else { item.UseWaitCursor = value; }
                }
            }

            return dataForm.Controls.Cast<Control>().Any(w => w.UseWaitCursor);
        }

    }

    /// <summary>
    /// Interface for Application Data Forms.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    interface IApplicationDataForm<TKey> : IApplicationDataBind
        where TKey : class, IKey
    {
        /// <summary>
        /// Key used to get the Data for the Form.
        /// </summary>
        TKey DataKey { get; init; }
    }

}
