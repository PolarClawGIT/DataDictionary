using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Mediator;

namespace DataDictionary.Main.Messages
{
    /// <summary>
    /// This represents common Windows commands such as Cut/Copy/Paste.
    /// The command is propagated from the form (normally Main) that caught it
    /// and propagates the message to the child forms.
    /// The active form (HandledBy) is expected to receive the message
    /// and act upon it. The bulk of the work is handled by the ApplicationFormBase.
    /// </summary>
    class WindowsCommand : MessageEventArgs
    {
        /// <summary>
        /// The Form that should handle the Message
        /// </summary>
        public required Form? HandledBy { get; init; }

        /// <summary>
        /// Has the Message been handled.
        /// False if no control has handled the Message.
        /// </summary>
        public Boolean IsHandled { get; set; } = false;
    }

    class WindowsCopyCommand : WindowsCommand { }

    class WindowsCutCommand : WindowsCommand { }

    class WindowsPasteCommand : WindowsCommand { }

    class WindowsUndoCommand : WindowsCommand { }

    class WindowsSelectAllCommand : WindowsCommand { }

    class WindowsHelpCommand : WindowsCommand { }
}
