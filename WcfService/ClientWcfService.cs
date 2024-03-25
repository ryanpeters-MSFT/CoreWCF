using Interfaces;
using WcfService.Repositories;

namespace WcfService
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class ClientWcfService : IClientWcfService
    {
        private readonly IConfiguration configuration;
        private readonly IClientRepository clientRepository;

        public ClientWcfService(IConfiguration configuration, IClientRepository clientRepository)
        {
            this.configuration = configuration;
            this.clientRepository = clientRepository;
        }

        public ICollection<Client> GetClients()
        {
            return clientRepository.GetClients();
        }

        public bool AddClient(Client client)
        {
            var allowClientCreation = bool.Parse(configuration["AllowClientCreation"]);

            if (allowClientCreation) 
            {
                return clientRepository.AddClient(client);
            }

            return false;
        }
    }
}
