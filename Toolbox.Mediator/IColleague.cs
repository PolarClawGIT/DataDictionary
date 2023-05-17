using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Mediator
{
    /// <summary>
    /// A object that can send or receive message from the Mediator.
    /// </summary>
    public interface IColleague : IDisposable
    {
        void RecieveMessage(object? sender, MessageEventArgs message);
        event EventHandler<MessageEventArgs> OnSendMessage;
        event EventHandler Disposed;
    }
}
