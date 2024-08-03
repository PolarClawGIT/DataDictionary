using DataDictionary.Main.Messages;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Data;
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
    interface IApplicationDataForm : IApplicationForm
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
        /// The RowState of the related data.
        /// </summary>
        public DataRowState? RowState { get; set; }

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
            if (dataForm.BindDataCore())
            {
                if (dataForm.RowState is DataRowState.Detached or DataRowState.Deleted)
                { dataForm.IsLocked(true); }
                else { dataForm.IsLocked(false); }

                dataForm.IsWaitCursor(false);
            }
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
        TKey DataKey { get; }
    }

}
