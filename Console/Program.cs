using Interfaces;
using System.ServiceModel;

// TCP ENDPOINT
//var binding = new NetTcpBinding();
//var endpoint = new EndpointAddress("net.tcp://127.0.0.1:8111/service.svc");

// HTTP ENDPOINT
var binding = new BasicHttpBinding();
var endpoint = new EndpointAddress("http://127.0.0.1:5189/service.svc");

var factory = new ChannelFactory<IService>(binding, endpoint);

// add custom client behaviors
factory.Endpoint.EndpointBehaviors.Add(new CustomEndpointBehavior());

/***************************************************************/

var channel = factory.CreateChannel();

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