using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Mediator;

namespace DataDictionary.Main.Messages
{
    internal class FormOpenMessage : MessageEventArgs
    {   public required Form FormOpened { get; init; } }
}
