# CoreWCF Sample
This simple example demonstrates a client and server implementation of WCF services using [CoreWCF](https://github.com/CoreWCF/CoreWCF). This example includes Basic HTTP, TCP, and JSON endpoint examples for both the client and server applications. 

- **Client** - Console application demonstarting use of the `ClientFactory` and various endpoint behaviors. 
- **Interfaces** - Common object types and service interfaces
- **Server** - WCF service supporting Basic, TCP, and JSON endpoints and using DI for type instantiation.

## CoreWCF Packages Used

- **CoreWCF.Primitives** - Common/shared types
- **CoreWCF.NetTcp** - Supports NET.TCP bindings
- **CoreWCF.Http** - Supports HTTP bindings
- **CoreWCF.WebHttp** - Supports web/JSON bindings