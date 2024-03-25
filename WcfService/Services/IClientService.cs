using Interfaces;

namespace WcfService.Services
{
    public interface IClientService
    {
        Client GetClient(Guid id);

        ICollection<Client> GetClients();

        bool AddClient(Client client);
    }
}
