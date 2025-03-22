using System;
using System.Threading.Tasks;
using CustomerService.DTOs;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace CustomerService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _config;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration config)
        {
            _config = config;
            var factory = new ConnectionFactory(){
                HostName = _config["RabbitMQHost"],
                Port = int.Parse(_config["RabbitMQPort"])
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Console.WriteLine($"Connected to RabbitMQ Message Bus");

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Could not connect to the Message Bus: {ex.Message}");
            }
        }
        public void PublishNewCustomer(CustomerPublishedForCreation customerPublishedForCreation)
        {
            throw new System.NotImplementedException();
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs args)
        {
                Console.WriteLine($"RabbitMQ connection shutdown: {args}");
        }
    }
}