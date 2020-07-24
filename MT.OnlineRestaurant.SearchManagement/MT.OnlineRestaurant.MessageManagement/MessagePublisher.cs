using Microsoft.Azure.ServiceBus;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MT.OnlineRestaurant.MessageManagement
{
    public class MessagePublisher : IMessagePublisher
    {
        string ServiceBusConnectionString = "Endpoint=sb://pricechangeservice.servicebus.windows.net/;SharedAccessKeyName=ManageSAP;SharedAccessKey=EZvdXr3cFbdD9oz5STD+AV9R+uDwMk2l18QSJcCe9VM=";
        //Endpoint=sb://pricechangeservice.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=OW/CTtodnk6gMzAMl0U6nIyfFi4Qn9V9bXC8zBjHXHs=
         string TopicName = "pricechangetopic";
         string SubscriptionName = "pricechangesubscription";
        static  ISubscriptionClient subscriptionClient;
        public IRestaurantBusiness _restaurantBusiness { get; set; }


        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            subscriptionClient = new SubscriptionClient(ServiceBusConnectionString, TopicName, SubscriptionName);
            
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {                
                MaxConcurrentCalls = 1,                
                AutoComplete = false
            };            
            subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }
        async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {

            string msg = Encoding.UTF8.GetString(message.Body);
            _restaurantBusiness.UpdateFromReceivedMessage(msg);
            await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
        public async void SendMessagesAsync(string msg)
        {
            ITopicClient topicClient;
            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);
            try
            {
                
                Message message = new Message(Encoding.UTF8.GetBytes(msg));
                await topicClient.SendAsync(message);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }

        public async Task SendMessageAsync(UpdatePriceEntity updatepricemessage)
        {
            ITopicClient topicClient;
            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);
            
            string data = JsonConvert.SerializeObject(updatepricemessage);
            Message message = new Message(Encoding.UTF8.GetBytes(data));          

            try
            {
                await topicClient.SendAsync(message);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
