using System;
using System.Threading;
using Confluent.Kafka;

enum EventType
{
    Mx,
    Br,
    Co
}

class Program
{
    static void Main(string[] args)
    {
        var bootstrapServers = "localhost:9092";

        // Produce messages
        ProduceMessages(bootstrapServers);

        // Consume messages
        ConsumeMessages(bootstrapServers);
    }

    static void ProduceMessages(string bootstrapServers)
    {
        var config = new ProducerConfig { BootstrapServers = bootstrapServers };
        using var producer = new ProducerBuilder<string, string>(config).Build();
        var random = new Random();
        int initialId = random.Next(1, 5000); // Random initial ID for message keys

        for (int i = initialId * 10000; i < (initialId * 10000) + 9800; i++)
        {
            // Randomly select an event type
            var eventType = (EventType)random.Next(Enum.GetValues(typeof(EventType)).Length);
            var topic = $"topic-{eventType.ToString().ToLower()}";
            var key = $"key-{i}";
            //var value = $"message-{eventType}-{i}";
            var sb = new System.Text.StringBuilder();
            for (int j = 0; j < 5; j++)  // Adjust size as needed
            {
                sb.Append($"Message-{eventType}-{i}-DataChunk-{j} ");
            }
            var value = sb.ToString();  // Large message payload

            producer.Produce(new TopicPartition(topic, new Partition(i % 5)),
                new Message<string, string> { Key = key, Value = value },
                (deliveryReport) =>
                {
                    //Console.WriteLine($"Delivered: {deliveryReport.Value} to {deliveryReport.TopicPartitionOffset}");
                });

        }

        producer.Flush(TimeSpan.FromSeconds(50));
        Console.WriteLine("Messages sent.");
    }

    static void ConsumeMessages(string bootstrapServers)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = bootstrapServers,
            GroupId = "test-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<string, string>(config).Build();

        // Subscribe to all event topics
        consumer.Subscribe(new[] { "topic-mx", "topic-br", "topic-co" });

        Console.WriteLine("Consuming messages...");
        try
        {
            while (true)
            {
                var consumeResult = consumer.Consume(timeout: TimeSpan.FromSeconds(5));
                Console.WriteLine($"Consumed: {consumeResult.Message.Value} from {consumeResult.TopicPartitionOffset}");
            }
        }
        catch (OperationCanceledException)
        {
            consumer.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error consuming messages: {ex.Message}");
            consumer.Close();
        }
        finally
        {
            Console.WriteLine("Consumer closed.");
        }
    }
}
