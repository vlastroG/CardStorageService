using CardStorageService.Data;

namespace CardStorageService.Services.Implements
{
    public class ClientRepository : IClientRepositoryService
    {
        private readonly CardStorageServiceDbContext _dbContext;

        private readonly ILogger<ClientRepository> _logger;


        public ClientRepository(
            ILogger<ClientRepository> logger,
            CardStorageServiceDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        public int Create(Client data)
        {
            _dbContext.Clients.Add(data);
            _dbContext.SaveChanges();
            return data.ClientId;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Client> GetAll()
        {
            throw new NotImplementedException();
        }

        public Client GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Client data)
        {
            throw new NotImplementedException();
        }
    }
}
