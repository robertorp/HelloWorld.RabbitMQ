using System.Text;
using RabbitMQ.Client;

namespace Publish;

public static class EmitLogger
{
    public static void Enviar(string[] args)
    {
        var factory = new ConnectionFactory { HostName = "localhost", Password = "guest", UserName = "guest" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

        string message = GetMessage(args);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "logs",
            routingKey: string.Empty,
            basicProperties: null,
            body: body);

        Console.WriteLine($" [x] Sent {message}");
    }

    public static string GetMessage(string[] args)
    {
        return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
    }
}