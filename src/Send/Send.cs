﻿using RabbitMQ.Client;
using System.Text;

namespace Send;

public class Send
{
    public void Enviar()
    {
        var factory = new ConnectionFactory { HostName = "localhost", Password = "guest", UserName = "guest"};
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "hello",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        const string message = "Hello World!";
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: string.Empty,
            routingKey: "hello",
            basicProperties: null,
            body: body);

        Console.WriteLine($" [x] Sent {message}");
    }
}