using Grpc.Net.Client;
using gRPC;

var channel = GrpcChannel.ForAddress("http://localhost:5151");
var client = new Greeter.GreeterClient(channel);

var reply = await client.SayHelloAsync(
    new HelloRequest { Name = "This is the parameter that must return" });

Console.WriteLine("Greeting: " + reply.Message);
