using RabbitMQ.Client;
using System.Text;

namespace NewTask;

public class NewTask
{
    public static void Enviar(string[] args)
    {
        var factory = new ConnectionFactory { HostName = "localhost", Password = "guest", UserName = "guest" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "task_queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        string message = GetMessage(args);
        var body = Encoding.UTF8.GetBytes(message);

        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;

        channel.BasicPublish(exchange: string.Empty,
            routingKey: "task_queue",
            basicProperties: null,
            body: body);

        Console.WriteLine($" [x] Sent {message}");
    }

    public static string GetMessage(string[] args)
    {
        return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
    }
}