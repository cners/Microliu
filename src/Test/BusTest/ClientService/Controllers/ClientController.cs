using ClientService.Models;
using EasyNetQ;
using Microliu.Core.EventBusRabbitMQ;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientService.Controllers
{
    [Produces("application/json")]
    [Route("api/client")]
    public class ClientController : Controller
    {
        //private readonly IClientService clientService;
        private readonly IBus bus;

        public ClientController(IBus _bus)
        {
            //clientService = _clientService;
            bus = _bus;
        }

        [HttpPost]
        public async Task<string> Post([FromBody] ClientDTO clientDto)
        {
            ClientMessage message = new ClientMessage
            {
                ClientId = clientDto.Id.Value,
                ClientName = clientDto.Name,
                Sex = clientDto.Sex,
                Age = 29,
                SmokerCode = "N",
                Education = "Master",
                YearIncome = 100000
            };
            await bus.PublishAsync(message);

            return "Add Client Success! You will receive some letter later.";
        }
    }
}