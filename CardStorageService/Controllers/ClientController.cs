using AutoMapper;
using CardStorageService.Data;
using CardStorageService.Models.Requests;
using CardStorageService.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardStorageService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientRepositoryService _clientRepositoryService;
        private readonly IValidator<CreateClientRequest> _createClientRequestValidator;
        private readonly IMapper _mapper;

        public ClientController(ILogger<ClientController> logger,
            IClientRepositoryService clientRepositoryService,
            IValidator<CreateClientRequest> createClientRequestValidator,
            IMapper mapper)
        {
            _logger = logger;
            _clientRepositoryService = clientRepositoryService;
            _createClientRequestValidator = createClientRequestValidator;
            _mapper = mapper;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateClientRequest request)
        {
            ValidationResult validationResult = _createClientRequestValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }
            try
            {
                var clientId = _clientRepositoryService.Create(_mapper.Map<Client>(request));
                //var clientId = _clientRepositoryService.Create(new Client
                //{
                //    FirstName = request.FirstName,
                //    Surname = request.Surname,
                //    Patronymic = request.Patronymic
                //});
                return Ok(new CreateClientResponse
                {
                    ClientId = clientId
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create client error.");
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 912,
                    ErrorMessage = "Create client error."
                });
            }
        }
    }
}
