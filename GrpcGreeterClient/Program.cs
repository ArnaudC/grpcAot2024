using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcGreeterClient;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7213");
var client = new Greeter.GreeterClient(channel);
var reply1 = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient1" });
Console.WriteLine("Greeting: " + reply1.Message);

// Say random number
var reply2 = await client.SayRandomNumberAsync(new HelloRequest { Name = "GreeterClient2" });
Console.WriteLine("Random message server response : " + reply2.Message);

// Multiple calls to the server
var max1 = 10;
for (var i = 1; i <= max1; i++) {
    var reply3 = await client.SayRandomNumberAsync(new HelloRequest { Name = "GreeterClient2" });
    Console.WriteLine($"Loop {i} : {reply3.Message}");
}

// Multiple calls to the server with random inputs
var max2 = 10_000;
const string allowedCaracters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
var random1 = new Random();
for (var i = 1; i <= max2; i++) {
    var length = (5 + i) % 500;
    var randomInput = new string(Enumerable.Repeat(allowedCaracters, length).Select(s => s[random1.Next(s.Length)]).ToArray());
    var reply3 = await client.SayRandomNumberAsync(new HelloRequest { Name = randomInput });
    if (i % 1_000 == 0)
        Console.WriteLine($"Loop {i} : {randomInput} {reply3.Message}");
}

// End
Console.WriteLine("Success.");
