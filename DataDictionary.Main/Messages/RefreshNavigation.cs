using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Mediator;

namespace DataDictionary.Main.Messages
{
    /// <summary>
    /// The Navigation data has changed significantly and needs to be refreshed.
    /// </summary>
    class RefreshNavigation:MessageEventArgs { }
}
