using Interfaces;

namespace WcfService.Services
{
    public class MockClientService : IClientService
    {
        private static readonly ICollection<Client> _clients =
        [
            new()
            {
                Id = Guid.Parse("a97ca022-e690-43a2-b739-fc9944e2e5ec"),
                Name = "Ryan",
                BirthDate = new DateTime(1983, 11, 20)
            },
            new()
            {
                Id = Guid.Parse("26bb5efa-924a-4db0-9a63-1cdbc0f8a331"),
                Name = "Krystle",
                BirthDate = new DateTime(1986, 3, 24)
            }
        ];

        public bool AddClient(Client client)
        {
            _clients.Add(client);

            return true;
        }

        public Client GetClient(Guid id) => _clients.FirstOrDefault(c => c.Id == id);

        public ICollection<Client> GetClients() => _clients;
    }
}
