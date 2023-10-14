using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Mediator;

namespace DataDictionary.Main.Messages
{
    /// <summary>
    /// Tells all forms that they should unbind the data.
    /// This is general done when the application data is about to be changed
    /// and the current bindings will no longer be valid.
    /// </summary>
    class DoUnbindData: MessageEventArgs { }

    /// <summary>
    /// Tells all forms that they should re-bind the data.
    /// This is general done when the application data has completed its changes
    /// and the current bindings will no longer be valid.
    /// </summary>
    /// <remarks>
    /// Because this event calls this.BindData(), do not repeat the call or errors will occur.
    /// </remarks>
    class DoBindData : MessageEventArgs { }

    /// <summary>
    /// Tells all forms that the Application Data has been changed. Application Data should be UnBindData.
    /// </summary>
    class DbApplicationBatchStarting : MessageEventArgs
    { }

    /// <summary>
    /// Tells all forms that the Application Data has been changed. Application Data should be BindData.
    /// </summary>
    class DbApplicationBatchCompleted : MessageEventArgs
    { }
}
