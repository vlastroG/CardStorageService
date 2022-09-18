using CardStorageService.Data;
using CardStorageService.Models;
using CardStorageService.Models.Requests;
using CardStorageService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace CardStorageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : Controller
    {
        private readonly ILogger<CardController> _logger;
        private readonly ICardRepositoryService _cardRepositoryService;

        public CardController(ILogger<CardController> logger,
            ICardRepositoryService cardRepositoryService)
        {
            _logger = logger;
            _cardRepositoryService = cardRepositoryService;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateCardRequest request)
        {
            try
            {
                var cardId = _cardRepositoryService.Create(new Card
                {
                    ClientId = request.ClientId,
                    CardN = request.CardN,
                    ExpDate = request.ExpDate,
                    CVV2 = request.CVV2
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
