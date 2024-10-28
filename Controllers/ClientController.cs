using Microsoft.AspNetCore.Mvc;
using ReserveHub.DTO_s;
using ReserveHub.Filters;
using ReserveHub.Responces;
using ReserveHub.Services;

namespace ReserveHub.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public sealed class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetClients([FromQuery] ClientFilter filter)
        {
            var result = _clientService.GetClients(filter);
            return Ok(ApiResponse<PaginationResponse<IEnumerable<ClientReadInfo>>>.Success(null, result));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetClientById(int id)
        {
            var client = _clientService.GetClientById(id);
            return client != null
                ? Ok(ApiResponse<ClientReadInfo>.Success(null, client))
                : NotFound(ApiResponse<ClientReadInfo?>.Fail(null, null, "Client not found"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateClient([FromBody] ClientReadInfo client)
        {
            bool result = _clientService.CreateClient(client);
            return result
                ? Ok(ApiResponse<bool>.Success(null, result))
                : BadRequest(ApiResponse<bool>.Fail(null, result));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateClient([FromBody] ClientUpdateInfo client)
        {
            bool result = _clientService.UpdateClient(client);
            return result
                ? Ok(ApiResponse<bool>.Success(null, result))
                : NotFound(ApiResponse<bool>.Fail(null, result));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteClient(int id)
        {
            bool result = _clientService.DeleteClient(id);
            return result
                ? Ok(ApiResponse<bool>.Success(null, result))
                : NotFound(ApiResponse<bool>.Fail(null, result));
        }
    }
}