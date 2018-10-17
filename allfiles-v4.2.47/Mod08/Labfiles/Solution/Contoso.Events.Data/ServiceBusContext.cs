﻿using Contoso.Events.Models;
using Microsoft.Extensions.Options;
using Microsoft.Azure.ServiceBus;
using System.Threading.Tasks;
using System.Text;

namespace Contoso.Events.Data
{
    public class ServiceBusContext : IQueueContext
    {
        protected ServiceBusSettings ServiceBusSettings;

        public ServiceBusContext(IOptions<ServiceBusSettings> serviceBusSettings)
        {
            ServiceBusSettings = serviceBusSettings.Value;
        }

        public async Task SendQueueMessageAsync(string eventKey)
        {
            QueueClient queueClient = new QueueClient(ServiceBusSettings.ConnectionString, ServiceBusSettings.QueueName);

            byte[] messageContent = Encoding.UTF8.GetBytes(eventKey);
            Message message = new Message(messageContent);
            message.Label = eventKey;

            await queueClient.SendAsync(message);
        }
    }
}