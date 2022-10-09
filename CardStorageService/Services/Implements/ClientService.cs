using AutoMapper;
using CardStorageService.Controllers;
using CardStorageService.Data;
using CardStorageServiceProtos;
using FluentValidation;
using Grpc.Core;
using static CardStorageServiceProtos.ClientService;

namespace CardStorageService.Services.Implements
{
    public class ClientService : ClientServiceBase
    {
        private readonly IClientRepositoryService _clientRepositoryService;
        private readonly IMapper _mapper;

        public ClientService(
            IClientRepositoryService clientRepositoryService,
            IMapper mapper)
        {
            _clientRepositoryService = clientRepositoryService;
            _mapper = mapper;
        }



        public override Task<CreateClientResponse> Create(CreateClientRequest request, ServerCallContext context)
        {
            try
            {
                var clientId = _clientRepositoryService.Create(new Data.Client
                {
                    FirstName = request.FirstName,
                    Surname = request.Surname,
                    Patronymic = request.Patronymic
                });

                var response = new CreateClientResponse
                {
                    ClientId = clientId,
                };

                return Task.FromResult(response);
            }
            catch (Exception e)
            {
                return Task.FromException<CreateClientResponse>(e);
            }
        }
    }
}
