using Interfaces;
using WcfService.Repositories;

var builder = WebApplication.CreateBuilder();

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddTransient<ClientWcfService>(); // required for DI to work with service endpoint type
builder.Services.AddSingleton<IClientRepository, MockClientRepository>();

builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelWebServices(); // required for Web HTTP endpoint
builder.Services.AddServiceModelMetadata();

// Applies additional WSDL behavior
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();
builder.Services.AddSingleton<IServiceBehavior, CustomServiceBehavior>();

// TCP only
//builder.WebHost.UseNetTcp(8111);

var app = builder.Build();

app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<ClientWcfService>();

    #region Endpoints and Bindings

    // TCP endpoint
    //serviceBuilder.AddServiceEndpoint<ClientWcfService, IClientWcfService>(new NetTcpBinding(), "/Service.svc", config =>
    //{
    //    //config.EndpointBehaviors.Add(new MyServiceEndpointBehavior());
    //});

    // Basic HTTP endpoint
    serviceBuilder.AddServiceEndpoint<ClientWcfService, IClientWcfService>(new BasicHttpBinding(), "/Service.svc", config =>
    {
        config.EndpointBehaviors.Add(new CustomEndpointBehavior());
    });

    // Web HTTP endpoint (w/ JSON)
    serviceBuilder.AddServiceWebEndpoint<ClientWcfService, IClientWcfService>("json", config => config.DefaultOutgoingResponseFormat = CoreWCF.Web.WebMessageFormat.Json);

    #endregion

    #region Metadata and Behaviors

    // Added via Services.AddServiceModelMetadata();
    var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();

    // Configure WSDL to be available over http
    serviceMetadataBehavior.HttpGetEnabled = true;
    //serviceMetadataBehavior.HttpGetUrl = new Uri("/meta", UriKind.Relative); // change WSDL metadata URL

    serviceBuilder.ConfigureServiceHostBase<ClientWcfService>(serviceHostBase =>
    {
        // Optionally configure service behaviors
        var behavior = serviceHostBase.Description.Behaviors.Find<CustomServiceBehavior>();
    }); 

    #endregion
});

app.Run();
