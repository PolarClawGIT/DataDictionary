using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Mediator;

namespace DataDictionary.Main.Messages
{
    class FormAddMdiChild : MessageEventArgs
    {   public required Form ChildForm { get; init; } }
}
