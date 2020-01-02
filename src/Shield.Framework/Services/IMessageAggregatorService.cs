#region Usings
using Shield.Framework.Services.Messaging;
#endregion

namespace Shield.Framework.Services
{
    public interface IMessageAggregatorService : IApplicationService
    {
        #region Methods
        bool MessageExists<T>() where T : IMessage, new();

        T GetMessage<T>() where T : IMessage, new();

        void RemoveMessage<T>() where T : IMessage, new();
        #endregion
    }
}