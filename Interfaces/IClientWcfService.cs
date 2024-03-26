using CoreWCF.Web;
using System.ServiceModel;

namespace Interfaces
{
    [ServiceContract]
    public interface IClientWcfService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/clients")]
        ICollection<Client> GetClients();

        [OperationContract]
        bool AddClient(Client client);

        [OperationContract]
        [WebGet(UriTemplate = "/file")]
        Stream GetDocument();
    }
}
