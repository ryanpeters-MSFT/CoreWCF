using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

public class CustomEndpointBehavior : IEndpointBehavior, IClientMessageInspector
{
    public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
    {
        //
    }

    public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
    {
        clientRuntime.ClientMessageInspectors.Add(this);
    }

    public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
    {
        //
    }

    public void AfterReceiveReply(ref Message reply, object correlationState)
    {
        //
    }

    public object BeforeSendRequest(ref Message request, IClientChannel channel)
    {
        return default;
    }

    public void Validate(ServiceEndpoint endpoint)
    {
        //
    }
}