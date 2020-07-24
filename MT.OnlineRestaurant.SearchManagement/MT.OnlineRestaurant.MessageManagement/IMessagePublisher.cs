using MT.OnlineRestaurant.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.MessageManagement
{
    public interface IMessagePublisher
    {
        //Task Publish<T>(T obj);
        //Task Publish(string raw);
        void RegisterOnMessageHandlerAndReceiveMessages();
        void SendMessagesAsync(string msg);
        Task SendMessageAsync(UpdatePriceEntity updatepricemessage);

    }
}
