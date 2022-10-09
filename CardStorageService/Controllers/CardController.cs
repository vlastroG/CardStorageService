using CardStorageService.Data;
using CardStorageService.Models;
using CardStorageService.Models.Requests;
using CardStorageService.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace CardStorageService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ILogger<CardController> _logger;
        private readonly ICardRepositoryService _cardRepositoryService;
        private readonly IValidator<CreateCardRequest> _createCardRequestValidator;

        public CardController(ILogger<CardController> logger,
            ICardRepositoryService cardRepositoryService,
            IValidator<CreateCardRequest> createCardRequestValidator)
        {
            _logger = logger;
            _cardRepositoryService = cardRepositoryService;
            _createCardRequestValidator = createCardRequestValidator;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateCardRequest request)
        {
            ValidationResult validationResult = _createCardRequestValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }
            try
            {
                var cardId = _cardRepositoryService.Create(new Card
                {
                    ClientId = request.ClientId,
                    CardN = request.CardN,
                    ExpDate = request.ExpDate,
                    CVV2 = request.CVV2,
                    Name = request.Name,
                });
                return Ok(new CreateCardResponse
                {
                    CardId = cardId.ToString()
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create card error.");
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 1012,
                    ErrorMessage = "Create card error."
                });
            }
        }

        [HttpGet("get-by-client-id")]
        [ProducesResponseType(typeof(GetCardsResponse), StatusCodes.Status200OK)]
        public IActionResult GetByClientId([FromQuery] int clientId)
        {
            try
            {
                var cards = _cardRepositoryService.GetByClientId(clientId);
                return Ok(new GetCardsResponse
                {
                    Cards = cards.Select(c => new CardDto
                    {
                        CardN = c.CardN,
                        CVV2 = c.CVV2,
                        Name = c.Name,
                        ExpDate = c.ExpDate.ToString("MM/yy")
                    }).ToList()
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get cards error.");
                return Ok(new GetCardsResponse
                {
                    ErrorCode = 1013,
                    ErrorMessage = "Get cards error."
                });
            }
        }
    }
}
