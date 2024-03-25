using Interfaces;
using WcfService.Services;

namespace WcfService
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Service : IService
    {
        private readonly IConfiguration configuration;
        private readonly IClientService clientService;

        public Service(IConfiguration configuration, IClientService clientService)
        {
            this.configuration = configuration;
            this.clientService = clientService;
        }

        public ICollection<Client> GetClients()
        {
            return clientService.GetClients();
        }

        public bool AddClient(Client client)
        {
            var allowClientCreation = bool.Parse(configuration["AllowClientCreation"]);

            if (allowClientCreation) 
            {
                return clientService.AddClient(client);
            }

            return false;
        }
    }
}
