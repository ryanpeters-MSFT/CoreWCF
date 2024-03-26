using Interfaces;
using Microsoft.AspNetCore.Mvc;
using WcfService.Repositories;

namespace WcfService
{
    // the "partial" class keyword is required for code generation
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public partial class ClientWcfService : IClientWcfService
    {
        private readonly IClientRepository clientRepository;

        public ClientWcfService(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public ICollection<Client> GetClients()
        {
            return clientRepository.GetClients();
        }

        public bool AddClient(Client client, [FromServices] IConfiguration configuration)
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
