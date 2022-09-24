using CardStorageService.Data;
using Microsoft.Data.SqlClient;
using NLog.LayoutRenderers.Wrappers;

namespace CardStorageService.Services.Implements
{
    public class CardRepository : ICardRepositoryService
    {
        private readonly CardStorageServiceDbContext _dbContext;

        private readonly ILogger<CardRepository> _logger;


        public CardRepository(
            ILogger<CardRepository> logger,
            CardStorageServiceDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }


        public string Create(Card data)
        {
            var client = _dbContext.Clients.FirstOrDefault(client => client.ClientId == data.ClientId);
            if (client is null)
            {
                throw new Exception("Client not found");
            }

            _dbContext.Cards.Add(data);
            _dbContext.SaveChanges();
            return data.ClientId.ToString();
        }

        public int Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IList<Card> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<Card> GetByClientId(int id)
        {
            return _dbContext.Cards.Where(c => c.ClientId == id).ToList();
        }

        public Card GetById(string id)
        {
            throw new NotImplementedException();
        }

        public int Update(Card data)
        {
            throw new NotImplementedException();
        }
    }
}
