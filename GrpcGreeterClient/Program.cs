using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcGreeterClient;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7213");
var client = new Greeter.GreeterClient(channel);
var reply1 = await client.SayHelloAsync(
                  new HelloRequest { Name = "GreeterClient1" });
Console.WriteLine("Greeting: " + reply1.Message);


// Say random number
var reply2 = await client.SayRandomNumberAsync(new HelloRequest { Name = "GreeterClient2" });
Console.WriteLine("Random message server response : " + reply2.Message);


// End
Console.WriteLine("Success.");
// Console.WriteLine("Press any key to exit...");
// Console.ReadKey();
