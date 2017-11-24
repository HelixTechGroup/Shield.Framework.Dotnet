using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shield.Framework.Messaging
{
    public interface IMessageAggregator
    {
        bool MessageExists<T>() where T : IMessage, new();

        T GetMessage<T>() where T : IMessage, new();

        void RemoveMessage<T>() where T : IMessage, new();
    }
}
