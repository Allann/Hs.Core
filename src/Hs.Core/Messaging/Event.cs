using MediatR;
using System;

namespace Hs.Core.Messaging
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; } = DateTime.Now;

        protected Event()
        { }
    }
}
