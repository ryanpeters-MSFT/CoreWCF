# CoreWCF Sample

This simple example demonstrates a client and server implementation of WCF services using [CoreWCF](https://github.com/CoreWCF/CoreWCF). This example includes Basic HTTP (`BasicHttpBinding`), WS HTTP (`WSHttpBinding`), TCP (`NetTcpBinding`), and RESTful/JSON (`WebHttpBinding`) endpoint binding examples for both the client and server applications. 

- **Client** - Console application demonstarting use of the `ClientFactory` and a custom client endpoint behavior. 
- **Interfaces** - Common object types and service interfaces
- **Server** - WCF service supporting Basic, TCP, and JSON endpoints and using DI for type instantiation and a custom service endpoint behavior.

## CoreWCF Packages Used

- **CoreWCF.Primitives** - Common/shared types
- **CoreWCF.NetTcp** - Supports NET.TCP bindings
- **CoreWCF.Http** - Supports HTTP bindings
- **CoreWCF.WebHttp** - Supports web/JSON bindings

## Installation

```powershell
dotnet new --install CoreWCF.Templates
dotnet new corewcf -n MyWcfService
```

## Dependency Injection

CoreWCF supports the same constructor injection or method-level injection (e.g., `[FromServices]`) as typical "core" applications. In this example, the [ClientWcfService.cs](./WcfService/ClientWcfService.cs) class injects an instance of [IClientRepository](./WcfService/Repositories/IClientRepository.cs) via constructor injection, while the `AddClient` method accepts an instance of IConfiguration using the `[FromServices]` attribute. 

For the latter scenario, the `partial` modifier is required for the `ClientWcfService` class and is used by the code-gen support in CoreWCF to create a version of the implementation method that matches the interface, thus allowing for a "clean" interface/contract without the additional need of supporting the additional argument(s).

## Links

- [CoreWCF Samples](https://github.com/CoreWCF/samples/tree/main)
- [Upgrading a WCF service to .NET 6 with CoreWCF](https://devblogs.microsoft.com/dotnet/upgrading-a-wcf-service-to-dotnet-6/)