using DataDictionary.DataLayer;
using DataDictionary.Main.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms
{
    interface IApplicationDataForm
    {
        Boolean IsOpenItem(Object? item);
    }

    /// <summary>
    /// Application Base Class for Forms that have a DataKey.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    abstract class ApplicationBase<TKey>: ApplicationBase
        where TKey: class, IKey
    {
        public virtual required TKey DataKey { get; init; }

        public virtual Boolean IsOpenItem(Object? item)
        { return DataKey.Equals(item); }

        /// <summary>
        /// Perform the Binding of Data. This is called by BindData();
        /// </summary>
        /// <returns>True if the data was successfully completed data binding.</returns>
        protected abstract Boolean BindDataCore();

        /// <summary>
        /// Perform the Unbinding of Data. This is called by UnbindData();
        /// </summary>
        protected abstract void UnbindDataCore();

        /// <summary>
        /// Calls BindDataCore() and locks or unlocks the form accordingly.
        /// </summary>
        protected void BindData()
        {
            if (BindDataCore()) { IsLocked = false; }
            else
            {
                IsLocked = true;
                IsWaitCursor = false;
            }
        }

        /// <summary>
        /// Class UnbindDataCore and locks the form.
        /// </summary>
        protected void UnbindData()
        {
            IsLocked = true;
            UnbindDataCore();
        }

        /// <summary>
        /// Message sent when all forms should call the UnBindData method.
        /// This method call the UnbindData of all forms EXCEPT the form that sent the message.
        /// </summary>
        /// <param name="message"></param>
        protected virtual void HandleMessage(DoUnbindData message)
        { UnbindData(); }

        /// <summary>
        /// Message sent when all forms should call the BindData method.
        /// This method calls the BindData of all forms EXCEPT the form that sent the message.
        /// </summary>
        /// <param name="message"></param>
        protected virtual void HandleMessage(DoBindData message)
        { BindData(); }
    }

}
