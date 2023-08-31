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
    /// This message should be sent any time the DbData is changed using any thread other then the Main thread.
    /// This will allow screens that use this data to disconnect from the data.
    /// Unbind DataGridViews specifically. They respond to a ListChanged event that will cause threading issues.
    /// </summary>
    class DbDataBatchStarting: MessageEventArgs { }

    /// <summary>
    /// This message should be sent any time the DbData is changed using any thread other then the Main thread.
    /// This will allow screens that use this data to disconnect to the data.
    /// </summary>
    class DbDataBatchCompleted : MessageEventArgs { }

    class DbApplicationBatchStarting : MessageEventArgs
    {
    }

    class DbApplicationBatchCompleted : MessageEventArgs
    {
     
    }
}
