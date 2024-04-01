using Interfaces;
using System.ServiceModel;

// wait for WCF service to spin-up
Console.WriteLine("Waiting 5 seconds for service to become available...\n");
Thread.Sleep(5000);

var httpClient = new HttpClient();
var baseUri = new Uri("http://127.0.0.1:5189");

// TCP ENDPOINT
//var binding = new NetTcpBinding();
//var endpoint = new EndpointAddress("net.tcp://127.0.0.1:8111/service.svc");

// HTTP ENDPOINT
var binding = new BasicHttpBinding
{
    MaxReceivedMessageSize = 200000
};

var endpoint = new EndpointAddress(new Uri(baseUri, "service.svc"));

var factory = new ChannelFactory<IClientWcfService>(binding, endpoint);

// add custom client behaviors
factory.Endpoint.EndpointBehaviors.Add(new CustomEndpointBehavior());

/***************************************************************/

var channel = factory.CreateChannel();

#region Invoke as RPC

var newClient = new Client
{
    Id = Guid.NewGuid(),
    Name = "John",
    BirthDate = new DateTime(1975, 6, 12)
};

channel.AddClient(newClient);

var clients = channel.GetClients();

foreach (var client in clients)
{
    Console.WriteLine($"[{client.Id}] {client.Name}, aged {client.Age}");
}

#endregion

#region Read as JSON using WebGet

// get clients using JSON endpoint
var clientsJson = await httpClient.GetStringAsync(new Uri(baseUri, "json/clients"));

Console.WriteLine($"\n{clientsJson}");

#endregion

#region Read a file stream (RPC)

// get a file
var file = channel.GetDocument();

using (var stream = new MemoryStream())
{
    file.CopyTo(stream);
    Console.WriteLine($"\nReceived file (RPC) w/ a length of {stream.Length} bytes");
}

#endregion

#region Read a file stream (REST)

// get clients using JSON endpoint
var fileBytes = await httpClient.GetByteArrayAsync(new Uri(baseUri, "json/file"));

Console.WriteLine($"\nReceived file (REST) w/ a length of {fileBytes.Length} bytes");

#endregion