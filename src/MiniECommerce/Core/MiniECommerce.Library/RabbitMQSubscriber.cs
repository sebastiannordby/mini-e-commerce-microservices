using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace MiniECommerce.Library
{
    public class RabbitMQSubscriber : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQSubscriber()
        {
            var factory = new ConnectionFactory() 
            { 
                HostName = "rabbitmq",
                Password = "guest",
                UserName = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Define your message processing logic here
            _channel.QueueDeclare(queue: "myQueue", durable: false, exclusive: false, autoDelete: false);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                // Process the message here

                Log.Information("Recieved message successfully: {0}", message);
            };

            _channel.BasicConsume(queue: "myQueue", autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
