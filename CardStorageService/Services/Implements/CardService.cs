using AutoMapper;
using CardStorageService.Models;
using CardStorageServiceProtos;
using Grpc.Core;
using static CardStorageServiceProtos.CardService;

namespace CardStorageService.Services.Implements
{
    public class CardService : CardServiceBase
    {
        private readonly ICardRepositoryService _cardRepositoryService;
        private readonly IMapper _mapper;

        public CardService(
            ICardRepositoryService cardRepositoryService,
            IMapper mapper)
        {
            _cardRepositoryService = cardRepositoryService;
            _mapper = mapper;
        }


        public override Task<GetByClientIdResponse> GetByClientId(GetByClientIdRequest request, ServerCallContext context)
        {
            var response = new GetByClientIdResponse();

            response.Cards.AddRange(_cardRepositoryService.GetByClientId(request.ClientId)
                .Select(card => new Card
                {
                    CardN = card.CardN,
                    CVV2 = card.CVV2,
                    ExpDate = card.ExpDate.ToShortDateString(),
                    Name = card.Name
                }).ToList());

            return Task.FromResult(response);
        }
    }
}
