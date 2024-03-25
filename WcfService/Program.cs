using Interfaces;
using WcfService.Services;

var builder = WebApplication.CreateBuilder();

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddTransient<Service>(); // required for DI to work with service endpoint type
builder.Services.AddSingleton<IClientService, MockClientService>();

builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelWebServices(); // required for Web HTTP endpoint
builder.Services.AddServiceModelMetadata();

builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

// TCP only
//builder.WebHost.UseNetTcp(8111);

var app = builder.Build();

app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<Service>();

    // TCP endpoint
    //serviceBuilder.AddServiceEndpoint<Service, IService>(new NetTcpBinding(), "/Service.svc", config =>
    //{
    //    //config.EndpointBehaviors.Add(new MyServiceEndpointBehavior());
    //});

    // Basic HTTP endpoint
    serviceBuilder.AddServiceEndpoint<Service, IService>(new BasicHttpBinding(), "/Service.svc", config =>
    {
        config.EndpointBehaviors.Add(new MyServiceEndpointBehavior());
    });

    // Web HTTP endpoint (w/ JSON)
    serviceBuilder.AddServiceWebEndpoint<Service, IService>("json", config => config.DefaultOutgoingResponseFormat = CoreWCF.Web.WebMessageFormat.Json);

    var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
    serviceMetadataBehavior.HttpGetEnabled = true;
});

app.Run();
