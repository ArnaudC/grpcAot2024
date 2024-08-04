# Description
Encrypt and decrypt files and streams on the fly with GRPC.
The server will store files or proxy encrypted streams.
The client will send / recieve files or streams.

# 1. Run the Server
```powershell
cd GrpcGreeterServer
dotnet run -lp https
```

# 2. Run the client
```powershell
cd GrpcGreeterClient
Measure-Command { dotnet run | Out-Default }
```
