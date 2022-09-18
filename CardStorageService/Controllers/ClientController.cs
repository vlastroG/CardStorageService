using Microsoft.AspNetCore.Mvc;

namespace CardStorageService.Controllers
{
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }
    }
}
