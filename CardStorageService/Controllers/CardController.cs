using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace CardStorageService.Controllers
{
    public class CardController : Controller
    {
        private readonly ILogger<CardController> _logger;

        public CardController(ILogger<CardController> logger)
        {
            _logger = logger;
        }

        [HttpGet("getAll")]
        public IActionResult GetByClientId(string clientId)
        {
            _logger.LogInformation($"GetByClientId : {clientId}");
            return Ok();
        }
    }
}
