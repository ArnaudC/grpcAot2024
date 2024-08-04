using Grpc.Core;
using grpcAot2024;

namespace grpcAot2024.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    private static readonly Random _global = new Random();
    [ThreadStatic] private static Random? _local;

    private int Next(int min, int max)
    {
        if (_local == null)
        {
            int seed;
            lock (_global)
            {
                seed = _global.Next();
            }
            _local = new Random(seed);
        }
        return _local.Next(min, max);
    }

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        _logger.LogInformation("SayHello! {0}", request.Name);
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }

    public override Task<HelloReply> SayRandomNumber(HelloRequest request, ServerCallContext context)
    {
        _logger.LogInformation("SayRandomNumber: {0}", request.Name);
        var randomNumber = Next(1, 100);
        return Task.FromResult(new HelloReply
        {
            Message = $"The random number is {randomNumber}"
        });
    }
}
