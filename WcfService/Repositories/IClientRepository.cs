using Interfaces;

namespace WcfService.Repositories
{
    public interface IClientRepository
    {
        Client GetClient(Guid id);

        ICollection<Client> GetClients();

        bool AddClient(Client client);
    }
}
